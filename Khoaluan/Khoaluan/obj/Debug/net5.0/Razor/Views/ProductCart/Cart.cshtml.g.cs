#pragma checksum "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7d11e272963e29701a8853b0e26507a36bddae65"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ProductCart_Cart), @"mvc.1.0.view", @"/Views/ProductCart/Cart.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\_ViewImports.cshtml"
using Khoaluan;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\_ViewImports.cshtml"
using Khoaluan.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d11e272963e29701a8853b0e26507a36bddae65", @"/Views/ProductCart/Cart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03f3678a84bfaa5a1015b6e961ae71778d9f87eb", @"/Views/_ViewImports.cshtml")]
    public class Views_ProductCart_Cart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Khoaluan.ModelViews.CartItem>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("115"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nk-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nk-btn nk-btn-rounded nk-btn-color-main-1 float-right"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Checkout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Check", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nk-page-background-top"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/images/bg-top-4.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nk-page-background-bottom"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/images/bg-bottom.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""nk-main"">

    <!-- START: Breadcrumbs -->
    <div class=""nk-gap-1""></div>
    <div class=""container"">
        <ul class=""nk-breadcrumbs"">


            <li><a href=""index.html"">Home</a></li>


            <li><span class=""fa fa-angle-right""></span></li>

            <li><a href=""store.html"">Store</a></li>


            <li><span class=""fa fa-angle-right""></span></li>

            <li><span>Cart</span></li>

        </ul>
    </div>
    <div class=""nk-gap-1""></div>
    <!-- END: Breadcrumbs -->




    <div class=""container"">
        <div class=""nk-store nk-store-cart"">
");
#nullable restore
#line 32 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
             if (Model != null && Model.Count() > 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"table-responsive\">\r\n\r\n                    <!-- START: Products in Cart -->\r\n\r\n                    <table class=\"table nk-store-cart-products\">\r\n\r\n                        <tbody>\r\n");
#nullable restore
#line 41 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
                             if (Model != null && Model.Count() > 0)
                            {
                                foreach (var item in Model)
                                {
                                    string url = $"/Product/HomePage/{item.product.Id}.html";

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td class=\"nk-product-cart-thumb\">\r\n                                            <a");
            BeginWriteAttribute("href", " href=\"", 1395, "\"", 1406, 1);
#nullable restore
#line 48 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
WriteAttributeValue("", 1402, url, 1402, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"nk-image-box-1 nk-post-image\">\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "7d11e272963e29701a8853b0e26507a36bddae6510381", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1505, "~/Images/", 1505, 9, true);
#nullable restore
#line 49 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
AddHtmlAttributeValue("", 1514, Url.Content(item.product.Image), 1514, 32, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 49 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
AddHtmlAttributeValue("", 1553, item.product.Name, 1553, 18, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                            </a>
                                        </td>
                                        <td class=""nk-product-cart-title"">
                                            <h5 class=""h6"">Product:</h5>
                                            <div class=""nk-gap-1""></div>

                                            <h2 class=""nk-post-title h4"">
                                                <a");
            BeginWriteAttribute("href", " href=\"", 2035, "\"", 2046, 1);
#nullable restore
#line 57 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
WriteAttributeValue("", 2042, url, 2042, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 57 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
                                                          Write(item.product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
                                            </h2>
                                        </td>
                                        <td class=""nk-product-cart-price"">
                                            <h5 class=""h6"">Price:</h5>
                                            <div class=""nk-gap-1""></div>

                                            <strong>€ ");
#nullable restore
#line 64 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
                                                 Write(item.product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</strong>
                                        </td>
                                        <td class=""nk-product-cart-quantity"">
                                            <h5 class=""h6"">Quantity:</h5>
                                            <div class=""nk-gap-1""></div>

                                            <div class=""nk-form"">
                                                <input type=""number"" class=""form-control"" value=""1"" min=""1"" max=""1"">
                                            </div>
                                        </td>
                                        <td class=""nk-product-cart-total"">
                                            <h5 class=""h6"">Total:</h5>
                                            <div class=""nk-gap-1""></div>

                                            <strong>€ ");
#nullable restore
#line 78 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
                                                 Write(item.product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n                                        </td>\r\n                                        <td class=\"nk-product-cart-remove\"><a href=\"#\"><span class=\"ion-android-close\"></span></a></td>\r\n                                    </tr>\r\n");
#nullable restore
#line 82 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
                                }
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


                        </tbody>


                    </table>
                    <!-- END: Products in Cart -->

                </div>
                <div class=""nk-gap-1""></div>
                <a class=""nk-btn nk-btn-rounded nk-btn-color-white float-right"" href=""#"">Update Cart</a>
");
            WriteLiteral(@"                <div class=""clearfix""></div>
                <div class=""nk-gap-2""></div>
                <div class=""row vertical-gap"">
                    <div class=""col-md-6"">

                        <!-- START: Calculate Shipping -->
                        <h3 class=""nk-title h4"">Calculate Shipping</h3>
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d11e272963e29701a8853b0e26507a36bddae6516603", async() => {
                WriteLiteral("\r\n                            <label for=\"country-sel\">Country <span class=\"text-main-1\">*</span>:</label>\r\n                            <select name=\"country\" class=\"form-control required\" id=\"country-sel\">\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d11e272963e29701a8853b0e26507a36bddae6517114", async() => {
                    WriteLiteral("Select a country...");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                            </select>

                            <div class=""nk-gap-1""></div>
                            <div class=""row vertical-gap"">
                                <div class=""col-sm-6"">
                                    <label for=""state"">State / Country <span class=""text-main-1"">*</span>:</label>
                                    <input type=""text"" class=""form-control required"" name=""state"" id=""state"">
                                </div>
                                <div class=""col-sm-6"">
                                    <label for=""zip"">Postcode / ZIP <span class=""text-main-1"">*</span>:</label>
                                    <input type=""tel"" class=""form-control required"" name=""zip"" id=""zip"">
                                </div>
                            </div>

                            <div class=""nk-gap-1""></div>
                            <a class=""nk-btn nk-btn-rounded nk-btn-color-white float-right"" href=""#"">Update Totals</a>
         ");
                WriteLiteral("               ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        <!-- END: Calculate Shipping -->

                    </div>
                    <div class=""col-md-6"">
                        <!-- START: Cart Totals -->
                        <h3 class=""nk-title h4"">Cart Totals</h3>
                        <table class=""nk-table nk-table-sm"">
                            <tbody>
                                <tr class=""nk-store-cart-totals-subtotal"">
                                    <td>
                                        Subtotal
                                    </td>
                                    <td>
                                        ");
#nullable restore
#line 138 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
                                   Write(Model.Sum(x => x.product.Price).ToString("#,##0"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                    </td>
                                </tr>
                                <tr class=""nk-store-cart-totals-shipping"">
                                    <td>
                                        Shipping
                                    </td>
                                    <td>
                                        Free Shipping
                                    </td>
                                </tr>
                                <tr class=""nk-store-cart-totals-total"">
                                    <td>
                                        Total
                                    </td>
                                    <td>
                                        € 52.00
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <!-- END: Cart Totals -->
                    </div>
                </div");
            WriteLiteral(">\r\n");
            WriteLiteral("                <div class=\"nk-gap-2\"></div>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d11e272963e29701a8853b0e26507a36bddae6522709", async() => {
                WriteLiteral("Proceed to Checkout");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <div class=\"clearfix\"></div>\r\n");
#nullable restore
#line 166 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>Chưa có hàng hóa trong giỏ hàng.Bạn hãy tích cực mua sắm đi nào</p>\r\n");
#nullable restore
#line 170 "D:\DULIEUHOCTAP\KhoaLuan\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\ProductCart\Cart.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n\r\n<!-- START: Page Background -->\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "7d11e272963e29701a8853b0e26507a36bddae6525075", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "7d11e272963e29701a8853b0e26507a36bddae6526274", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<!-- END: Page Background -->\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
    $(function () {
        function loadHeaderCart() {
            $('#miniCart').load(""/AjaxContent/HeaderCart"");
            $('#numberCart').load(""/AjaxContent/NumberCart"");
        }
        $("".removecart"").click(function () {
            var productid = $(this).attr(""data-mahh"");
            $.ajax({
                url: ""api/cart/remove"",
                type: ""POST"",
                dataType: ""JSON"",
                data: { productID: productid },
                success: function (result) {
                    if (result.success) {
                        loadHeaderCart();//Reload lai gio hang
                        location.reload();
                    }
                },
                error: function (rs) {
                    alert(""Remove Cart Error !"")
                }
            });
        });
        $("".cartItem"").click(function () {
            var productid = $(this).attr(""data-mahh"");
            var soluong = parseInt($(this).val());
    ");
                WriteLiteral(@"        $.ajax({
                url: ""api/cart/update"",
                type: ""POST"",
                dataType: ""JSON"",
                data: {
                    productID: productid,
                    amount: soluong
                },
                success: function (result) {
                    if (result.success) {
                        loadHeaderCart();//Reload lai gio hang
                        window.location = 'cart.html';
                    }
                },
                error: function (rs) {
                    alert(""Cập nhật Cart Error !"")
                }
            });
        });
    });
    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Khoaluan.ModelViews.CartItem>> Html { get; private set; }
    }
}
#pragma warning restore 1591
