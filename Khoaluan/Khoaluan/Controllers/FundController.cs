using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Enums;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.VNPayOthers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class FundController : Controller
    {
        public INotyfService _notyfService { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService _service;

        // Paypal Settings
        private string _clientID;
        private string _secretKey;
        private string _paypalEnvironment = "sandbox";

        // VNPay Settings
        private string _tmnCode;
        private string _hashSecret;

        public FundController(INotyfService notyfService, IUnitOfWork unitOfWork, IService service, IConfiguration config)
        {
            _notyfService = notyfService;
            _unitOfWork = unitOfWork;
            _service = service;

            _clientID = config["PaypalSettings:ClientID"];
            _secretKey = config["PaypalSettings:SecretKey"];

            _tmnCode = config["VNPaySettings:TmnCode"];
            _hashSecret = config["VNPaySettings:HashSecret"];
        }

        [HttpGet]
        [Route("add-funds.html", Name = "AddFunds")]
        public IActionResult AddFunds()
        {
            var accountId = HttpContext.Session.GetString("CustomerId");
            var token = HttpContext.Session.GetString("SS_Token");

            if (accountId == null)
            {
                return RedirectToAction("Homepage", "Product");
            }

            ViewBag.Token = "null";

            if (token != null)
                ViewBag.Token = token.ToString();

            var funds = _unitOfWork.FundRepository.GetAll();
            return View(funds);
        }

        [HttpGet]
        [Route("check-out.html", Name = "CheckOut")]
        public IActionResult CheckOut(int id)
        {
            var accountId = HttpContext.Session.GetString("CustomerId");
            var paypalTransaction = HttpContext.Session.GetString("SS_Paypal");
            if (paypalTransaction == null) { }
            else
            {
                _notyfService.Warning("Transactions in progress!");
                return RedirectToAction("AddFunds");
            }

            if (accountId != null)
            {
                var fund = _unitOfWork.FundRepository.GetById(id);
                var account = _unitOfWork.UserRepository.GetById(Convert.ToInt32(accountId));

                CheckOutViewModel checkOut = new CheckOutViewModel()
                {
                    Account = account,
                    Fund = fund
                };

                return View(checkOut);
            }

            return RedirectToAction("Homepage", "Product");
        }

        public void NewTransaction()
        {
            HttpContext.Session.Remove("SS_Token");
            HttpContext.Session.Remove("SS_Paypal");
            _notyfService.Information("You can now create a new transaction!");
        }

        public async Task<ActionResult> Paypalvtwo(
            [FromQuery(Name = "paymentId")] string paymentId,
            [FromQuery(Name = "payerID")] string payerId,
            [FromQuery(Name = "token")] string token,
            string Cancel = null,
            string id = null
        )
        {
            #region Local variables

            // Setup paypal environment
            MyPaypalPayment.MyPaypalSetup payPalSetup = new MyPaypalPayment.MyPaypalSetup()
            {
                Environment = _paypalEnvironment,
                ClientId = _clientID,
                Secret = _secretKey
            };

            List<string> paymentResultList = new List<string>();

            // Session gói nạp, tài khoản
            var fundId = id;
            if (id == null)
                fundId = HttpContext.Session.GetString("SS_FundId");

            var accountId = HttpContext.Session.GetString("CustomerId");
            Fund fund = null;

            if (fundId == null || accountId == null)
            {
                _notyfService.Error("Can't get recharge pack id or account id!");
                return RedirectToAction("AddFunds");
            }

            fund = _unitOfWork.FundRepository.GetById(Convert.ToInt32(fundId));

            if (fund == null)
            {
                _notyfService.Error("Unable to get loaded package information!");
                return RedirectToAction("AddFunds");
            }

            #endregion

            #region Check payment cancellation

            // Kiểm tra nếu payer hủy giao dịch
            if (!string.IsNullOrEmpty(Cancel) && Cancel.Trim().ToLower() == "true")
            {
                paymentResultList.Add("You cancelled the transaction.");
                _notyfService.Information("Canceled transaction!");
                HttpContext.Session.Remove("SS_Token");
                HttpContext.Session.Remove("SS_Paypal");
                return RedirectToAction("AddFunds");
            }

            #endregion

            payPalSetup.PayerApprovedOrderId = token;
            string PayerID = payerId;

            // Nếu payerID bằng null thì order đó chưa được approve bởi khách hàng   
            if (string.IsNullOrEmpty(PayerID))
            {
                var paypalTransaction = HttpContext.Session.GetString("SS_Paypal");
                if (paypalTransaction == null) { }
                else
                {
                    _notyfService.Warning("Transactions in progress!");
                    return RedirectToAction("AddFunds");
                }

                #region Order creation

                // Lưu session Paypal, gói nạp: SS_Paypal, SS_FundId
                HttpContext.Session.SetString("SS_Paypal", "order_creation");
                HttpContext.Session.SetString("SS_FundId", id);

                // Tạo mới một order và hiển thị cho payer để approve
                try
                {
                    // Khi approve hay cancel thì PayPal sử dụng RedirectUrl này để redirect đến app/website project
                    payPalSetup.RedirectUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/Fund/Paypalvtwo?";

                    // Tạo order với gói nạp (fund)
                    PayPalHttp.HttpResponse response = await MyPaypalPayment.createOrder(payPalSetup, fund);

                    var statusCode = response.StatusCode;
                    PayPalCheckoutSdk.Orders.Order result = response.Result<PayPalCheckoutSdk.Orders.Order>();
                    HttpContext.Session.SetString("SS_Token", result.Id);

                    foreach (PayPalCheckoutSdk.Orders.LinkDescription link in result.Links)
                    {
                        if (link.Rel.Trim().ToLower() == "approve")
                        {
                            payPalSetup.ApproveUrl = link.Href;
                        }
                    }

                    if (!string.IsNullOrEmpty(payPalSetup.ApproveUrl))
                        return Redirect(payPalSetup.ApproveUrl);
                }
                catch (Exception ex)
                {
                    paymentResultList.Add("There was an error in processing your payment");
                    paymentResultList.Add("Details: " + ex.Message);
                }

                #endregion
            }
            else
            {
                #region Order execution

                // Lưu session Paypal: SS_Paypal
                HttpContext.Session.SetString("SS_Paypal", "order_execution");

                // Capture order -> khi này người dùng đã approve nên lúc này sẽ thực hiện order
                PayPalHttp.HttpResponse response = await MyPaypalPayment.captureOrder(payPalSetup);
                try
                {
                    var statusCode = response.StatusCode;
                    PayPalCheckoutSdk.Orders.Order result = response.Result<PayPalCheckoutSdk.Orders.Order>();
                    HttpContext.Session.SetString("SS_Token", result.Id);

                    // Thanh toán thành công -> khi này cập nhật wallet balance của tài khoản nạp
                    if (result.Status.Trim().ToUpper() == "COMPLETED")
                    {
                        _unitOfWork.UserRepository.updateBalance(Convert.ToInt32(accountId), fund.Price, (int)marketType.paypal);

                        AddFundTransaction add = new AddFundTransaction()
                        {
                            TransactionId = result.PurchaseUnits[0].Payments.Captures[0].Id,
                            OrderId = result.Id,
                            FundId = int.Parse(HttpContext.Session.GetString("SS_FundId")),
                            UserId = int.Parse(accountId),
                            PaymentMethod = "PayPal",
                            CreateOn = DateTime.Now,
                        };
                        _unitOfWork.AddFundTransactionRepository.Create(add);

                        _unitOfWork.SaveChange();
                        _notyfService.Success("Payment success!");
                        _notyfService.Information("Successful wallet balance update!");
                        paymentResultList.Add("Payment Successful. Thank you.");

                        HttpContext.Session.Remove("SS_Token");
                        HttpContext.Session.Remove("SS_Paypal");
                    }

                    paymentResultList.Add("Payment State: " + result.Status);
                    paymentResultList.Add("Payment ID: " + result.Id);

                    #region null_checks
                    if (result.PurchaseUnits != null && result.PurchaseUnits.Count > 0 &&
                        result.PurchaseUnits[0].Payments != null && result.PurchaseUnits[0].Payments.Captures != null &&
                        result.PurchaseUnits[0].Payments.Captures.Count > 0)
                        #endregion
                        paymentResultList.Add("Transaction ID: " + result.PurchaseUnits[0].Payments.Captures[0].Id);
                }
                catch (Exception ex)
                {
                    _notyfService.Error("Payment failed");
                    paymentResultList.Add("There was an error in processing your payment");
                    paymentResultList.Add("Details: " + ex.Message);
                }

                #endregion
            }

            return RedirectToAction("AddFunds");
        }

        public ActionResult VNPayvtwo(string id = null)
        {
            var fund = _unitOfWork.FundRepository.GetById(Convert.ToInt32(id));

            if (fund == null)
            {
                _notyfService.Error("Unable to get loaded package information!");
                return RedirectToAction("AddFunds");
            }

            decimal tax = Math.Round((decimal)(fund.Price * fund.Tax), 2);
            int total = Convert.ToInt32((tax + fund.Price) * 24000 * 100);

            string url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string redirectUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + $"/Fund/PaymentConfirm?id={id}";

            // Lấy địa chỉ IP của người dùng hiện hành
            IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            string ipAddress = "";

            if (remoteIpAddress != null)
            {
                if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    try
                    {
                        remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                        ipAddress = remoteIpAddress.ToString();
                    }
                    catch
                    {
                        ipAddress = "192.168.1.251";
                    }
                }

                
            }

            PayLib pay = new PayLib();
            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", _tmnCode);
            pay.AddRequestData("vnp_Amount", total.ToString());
            pay.AddRequestData("vnp_BankCode", "");
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", ipAddress);
            pay.AddRequestData("vnp_Locale", "vn"); // Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); // Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", redirectUrl); // URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); // Mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, _hashSecret);

            return Redirect(paymentUrl);
        }
        public ActionResult Momovtwo(string id = null)
        {
            var fund = _unitOfWork.FundRepository.GetById(Convert.ToInt32(id));

            if (fund == null)
            {
                _notyfService.Error("Unable to get loaded package information!");
                return RedirectToAction("AddFunds");
            }

            decimal tax = Math.Round((decimal)(fund.Price * fund.Tax), 2);
            string total = Convert.ToInt32((tax + fund.Price) * 24000).ToString();

            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "Checkout";
            string returnUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + $"/Fund/MomoConfirm?id={id}";
            string notifyurl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + $"/Fund/MomoConfirm?id={id}"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = total;
            string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult MomoConfirm(
            [FromQuery(Name = "orderId")] string orderId,
            [FromQuery(Name = "transId")] string momoTranId,
            [FromQuery(Name = "responseTime")] DateTime dateTime,
            string id = null)
        {
            var accountId = HttpContext.Session.GetString("CustomerId");
            var fund = _unitOfWork.FundRepository.GetById(Convert.ToInt32(id));
            if (fund == null)
            {
                _notyfService.Error("Unable to get loaded package information!");
                return RedirectToAction("AddFunds");
            }
            int errorCode = int.Parse(HttpContext.Request.Query["errorCode"]);
            if (!errorCode.Equals(0))
            {
                _notyfService.Error("Transaction error!");
                return RedirectToAction("AddFunds");
            }
            else
            {
                _unitOfWork.UserRepository.updateBalance(Convert.ToInt32(accountId), fund.Price, (int)marketType.paypal);
                AddFundTransaction add = new AddFundTransaction()
                {
                    TransactionId = momoTranId,
                    OrderId = orderId,
                    FundId = int.Parse(id),
                    UserId = int.Parse(accountId),
                    PaymentMethod = "Momo",
                    CreateOn = dateTime,
                };
                _unitOfWork.AddFundTransactionRepository.Create(add);
                _unitOfWork.SaveChange();
                _notyfService.Information("Payment successful Order ID " + orderId + " Transaction ID " + momoTranId);
                _notyfService.Success("Update balance successful");
            }
            return RedirectToAction("AddFunds");
        }

        public ActionResult PaymentConfirm(
            [FromQuery(Name = "vnp_Amount")] string vnp_Amount,
            [FromQuery(Name = "vnp_BankCode")] string vnp_BankCode,
            [FromQuery(Name = "vnp_BankTranNo")] string vnp_BankTranNo,
            [FromQuery(Name = "vnp_CardType")] string vnp_CardType,
            [FromQuery(Name = "vnp_OrderInfo")] string vnp_OrderInfo,
            [FromQuery(Name = "vnp_PayDate")] string vnp_PayDate,
            [FromQuery(Name = "vnp_ResponseCode")] string vnp_ResponseCode,
            [FromQuery(Name = "vnp_TmnCode")] string vnp_TmnCode,
            [FromQuery(Name = "vnp_TransactionNo")] string vnp_TransactionNo,
            [FromQuery(Name = "vnp_TransactionStatus")] string vnp_TransactionStatus,
            [FromQuery(Name = "vnp_TxnRef")] string vnp_TxnRef,
            [FromQuery(Name = "vnp_SecureHash")] string vnp_SecureHash,
            string id = null
            )
        {
            if (vnp_ResponseCode == "24")
            {
                _notyfService.Information("Canceled transaction!");
                return RedirectToAction("AddFunds");
            }

            PayLib pay = new PayLib();
            string hashString = $"vnp_Amount={vnp_Amount}&vnp_BankCode={vnp_BankCode}&vnp_BankTranNo={vnp_BankTranNo}&vnp_CardType={vnp_CardType}&vnp_OrderInfo=Thanh+toan+don+hang&vnp_PayDate={vnp_PayDate}&vnp_ResponseCode={vnp_ResponseCode}&vnp_TmnCode={vnp_TmnCode}&vnp_TransactionNo={vnp_TransactionNo}&vnp_TransactionStatus={vnp_TransactionStatus}&vnp_TxnRef={vnp_TxnRef}";

            long orderId = Convert.ToInt64(vnp_TxnRef); // Mã hóa đơn
            long vnpayTranId = Convert.ToInt64(vnp_TransactionNo); // Mã giao dịch tại hệ thống VNPAY

            bool checkSignature = pay.ValidateSignature(vnp_SecureHash, _hashSecret, hashString); // check chữ ký đúng hay không?

            var accountId = HttpContext.Session.GetString("CustomerId");
            var fund = _unitOfWork.FundRepository.GetById(Convert.ToInt32(id));

            if (fund == null)
            {
                _notyfService.Error("Unable to get loaded package information!");
                return RedirectToAction("AddFunds");
            }

            if (checkSignature)
            {
                if (vnp_ResponseCode == "00")
                {
                    _unitOfWork.UserRepository.updateBalance(Convert.ToInt32(accountId), fund.Price, (int)marketType.paypal);
                    AddFundTransaction add = new AddFundTransaction()
                    {
                        TransactionId = vnpayTranId.ToString(),
                        OrderId = orderId.ToString(),
                        FundId = int.Parse(id),
                        UserId = int.Parse(accountId),
                        PaymentMethod = "VNPay",
                        CreateOn = DateTime.Now,
                    };
                    _unitOfWork.AddFundTransactionRepository.Create(add);
                    _unitOfWork.SaveChange();
                    _notyfService.Information("Payment successful\nOrder ID " + orderId + "Transaction ID " + vnpayTranId);
                    _notyfService.Success("Update balance successful");
                }
                else
                {
                    _notyfService.Error("An error occurred during invoice processing " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode);
                }
            }
            else
            {
                _notyfService.Error("An error occurred during processing");
            }

            return RedirectToAction("AddFunds");
        }
    }
}
