using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Enums;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.VNPayOthers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
                _notyfService.Warning("Đang có giao dịch được thực hiện!");
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
            _notyfService.Information("Bạn đã có thể tạo giao dịch mới!");
        }

        public async Task<ActionResult> Paypalvtwo(
            [FromQuery(Name = "paymentId")] string paymentId,
            [FromQuery(Name = "payerID")] string payerId,
            [FromQuery(Name = "token")] string token,
            string Cancel = null,
            string id = null
        )
        {
            #region local_variables
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
                _notyfService.Error("Không thể lấy id gói nạp hoặc id tài khoản!");
                return RedirectToAction("AddFunds");
            }

            fund = _unitOfWork.FundRepository.GetById(Convert.ToInt32(fundId));

            if (fund == null)
            {
                _notyfService.Error("Không thể lấy thông tin gói nạp!");
                return RedirectToAction("AddFunds");
            }
            #endregion

            #region check_payment_cancellation
            // Kiểm tra nếu payer hủy giao dịch
            if (!string.IsNullOrEmpty(Cancel) && Cancel.Trim().ToLower() == "true")
            {
                paymentResultList.Add("You cancelled the transaction.");
                _notyfService.Information("Đã hủy giao dịch!");
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
                    _notyfService.Warning("Đang có giao dịch được thực hiện!");
                    return RedirectToAction("AddFunds");
                }

                #region order_creation
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
                #region order_execution
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
                        _unitOfWork.SaveChange();
                        _notyfService.Success("Thanh toán thành công!");
                        _notyfService.Information("Cập nhật số dư ví thành công!");
                        paymentResultList.Add("Payment Successful. Thank you.");

                        //switch (kq)
                        //{
                        //    case -1:
                        //        _notyfService.Information("Không tìm thấy tài khoản!");
                        //        break;
                        //    case 0:
                        //        _notyfService.Error("Có lỗi trong quá trình cập nhật số dư ví!");
                        //        break;
                        //    case 1:
                        //        _unitOfWork.SaveChange();
                        //        _notyfService.Information("Cập nhật số dư ví thành công!");
                        //        break;

                        //    default:
                        //        _notyfService.Error("Lỗi không xác định");
                        //        break;
                        //}

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
                    _notyfService.Error("Thanh toán thất bại");
                    paymentResultList.Add("There was an error in processing your payment");
                    paymentResultList.Add("Details: " + ex.Message);
                }
                #endregion
            }

            return RedirectToAction("AddFunds");
        }

        public ActionResult VNPayvtwo(string id = null)
        {
            string url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string redirectUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/Fund/PaymentConfirm";

            PayLib pay = new PayLib();
            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", _tmnCode);
            pay.AddRequestData("vnp_Amount", "1000000"); // Số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", "");
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            // pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); // Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); // Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); // Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", redirectUrl); // URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); // Mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, _hashSecret);

            return Redirect(paymentUrl);
        }
    }
}
