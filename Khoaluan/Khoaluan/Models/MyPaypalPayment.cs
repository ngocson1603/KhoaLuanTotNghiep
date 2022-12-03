using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Khoaluan.Models
{
    public class MyPaypalPayment

    {
        /// <summary>
        /// Khởi tạo Paypal client
        /// </summary>
        /// <param name="paypalEnvironment">
        /// - Value "sandbox" cho testing
        /// - Value "live" cho live environments
        /// </param>            
        /// <returns>PayPalHttp.HttpClient</returns>
        public static PayPalHttpClient client(MyPaypalSetup paypalEnvironment)
        {
            PayPalEnvironment environment = null;

            if (paypalEnvironment.Environment == "live")
            {
                environment = new LiveEnvironment(paypalEnvironment.ClientId, paypalEnvironment.Secret);
            }
            else
            {
                environment = new SandboxEnvironment(paypalEnvironment.ClientId, paypalEnvironment.Secret);
            }

            PayPalHttpClient client = new PayPalHttpClient(environment);
            return client;
        }


        // Tạo Order với gói nạp (fund)
        public async static Task<HttpResponse> createOrder(MyPaypalSetup paypalSetup, Fund fund)
        {
            HttpResponse response = null;

            try
            {
                // Số tiền tax và total của gói nạp (fund) phía trên
                decimal tax = Math.Round((decimal)(fund.Price * fund.Tax), 2);
                decimal total = tax + fund.Price;

                // Khởi tạo request object và các tham số cần thiết
                // OrdersCreateRequest() tạo một POST request đến /v2/checkout/orders
                var order = new OrderRequest()
                {
                    CheckoutPaymentIntent = "CAPTURE",
                    PurchaseUnits = new List<PurchaseUnitRequest>()
                        {
                            new PurchaseUnitRequest()
                            {
                                Items = new List<PayPalCheckoutSdk.Orders.Item>()
                                {
                                    new PayPalCheckoutSdk.Orders.Item()
                                    {
                                        Quantity = "1",
                                        Name = fund.Name,
                                        Sku = "sku",
                                        Tax = new PayPalCheckoutSdk.Orders.Money(){ CurrencyCode = "USD", Value = tax.ToString() },
                                        UnitAmount = new PayPalCheckoutSdk.Orders.Money(){ CurrencyCode = "USD", Value = fund.Price.ToString() }
                                    }
                                },

                                AmountWithBreakdown = new AmountWithBreakdown()
                                {
                                    CurrencyCode = "USD",
                                    Value = total.ToString(),

                                    AmountBreakdown = new AmountBreakdown()
                                    {
                                        TaxTotal = new PayPalCheckoutSdk.Orders.Money()
                                        {
                                            CurrencyCode = "USD",
                                            Value = tax.ToString()
                                        },
                                        ItemTotal = new PayPalCheckoutSdk.Orders.Money()
                                        {
                                            CurrencyCode = "USD",
                                            Value = fund.Price.ToString()
                                        }
                                    }
                                }
                            }
                        },
                    ApplicationContext = new ApplicationContext()
                    {
                        ReturnUrl = paypalSetup.RedirectUrl,
                        CancelUrl = paypalSetup.RedirectUrl + "&Cancel=true"
                    }
                };

                // IMPORTANT
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                // Call API
                var request = new OrdersCreateRequest();
                request.Prefer("return=representation");
                request.RequestBody(order);
                PayPalHttpClient paypalHttpClient = client(paypalSetup);
                // Get response
                response = await paypalHttpClient.Execute(request);

            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }


        // Trước khi capture order, order cần được approve bởi người mua thông qua link approve trong create order response
        public async static Task<HttpResponse> captureOrder(MyPaypalSetup paypalSetup)
        {
            // Khởi tạo request object và các tham số cần thiết
            // Thay đổi ORDER-ID của create order bằng approved order id
            var request = new OrdersCaptureRequest(paypalSetup.PayerApprovedOrderId);
            request.RequestBody(new OrderActionRequest());
            PayPalHttpClient paypalHttpClient = client(paypalSetup);
            HttpResponse response = await paypalHttpClient.Execute(request);
            return response;
        }

        public class MyPaypalSetup
        {
            /// <summary>
            /// Provide value sandbox for testing,  provide value live for real money
            /// </summary>
            public String Environment { get; set; }
            /// <summary>
            /// Client id as provided by Paypal on dashboard. Ensure you use correct value based on your environment selection
            /// Use sandbox accounts for sandbox testing
            /// </summary>
            public String ClientId { get; set; }
            /// <summary>
            /// Secret as provided by Paypal on dashboard. Ensure you use correct value based on your environment selection
            /// Use sandbox accounts for sandbox testing
            /// </summary>
            public String Secret { get; set; }

            /// <summary>
            /// This is the URL that you will pass to paypal which paypal will use to redirect payer back to your website.
            /// So essentially it is the same controller URL that you must pass
            /// </summary>
            public String RedirectUrl { get; set; }

            /// <summary>
            /// Once order is created on Paypal, it redirects control to your app with a URL that shows order details. Your website must take the payer to this page
            /// so the payer approved the payment. Store this URL in this property
            /// </summary>
            public String ApproveUrl { get; set; }

            /// <summary>
            /// When paypal redirects control to your website it provides a Approved Order ID which we then pass it back to paypal to execute the order.
            /// Store this approved order ID in this property
            /// </summary>
            public String PayerApprovedOrderId { get; set; }
        }
    }
}
