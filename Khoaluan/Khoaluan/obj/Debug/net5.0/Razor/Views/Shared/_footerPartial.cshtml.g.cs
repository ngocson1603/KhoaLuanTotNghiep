#pragma checksum "C:\Users\Admins\OneDrive\Máy tính\khoaluantest\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\Shared\_footerPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99b9707a762255b2449d716e577d46108e3070ff"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__footerPartial), @"mvc.1.0.view", @"/Views/Shared/_footerPartial.cshtml")]
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
#line 1 "C:\Users\Admins\OneDrive\Máy tính\khoaluantest\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\_ViewImports.cshtml"
using Khoaluan;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admins\OneDrive\Máy tính\khoaluantest\KhoaLuanTotNghiep\Khoaluan\Khoaluan\Views\_ViewImports.cshtml"
using Khoaluan.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99b9707a762255b2449d716e577d46108e3070ff", @"/Views/Shared/_footerPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03f3678a84bfaa5a1015b6e961ae71778d9f87eb", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__footerPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/php/ajax-contact-form.php"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nk-form nk-form-ajax"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/images/post-1-sm.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/images/post-2-sm.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<footer class=""nk-footer"">

    <div class=""container"">
        <div class=""nk-gap-3""></div>
        <div class=""row vertical-gap"">
            <div class=""col-md-6"">
                <div class=""nk-widget"">
                    <h4 class=""nk-widget-title""><span class=""text-main-1"">Contact</span> With Us</h4>
                    <div class=""nk-widget-content"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99b9707a762255b2449d716e577d46108e3070ff5761", async() => {
                WriteLiteral(@"
                            <div class=""row vertical-gap sm-gap"">
                                <div class=""col-md-6"">
                                    <input type=""email"" class=""form-control required"" name=""email""
                                           placeholder=""Email *"">
                                </div>
                                <div class=""col-md-6"">
                                    <input type=""text"" class=""form-control required"" name=""name""
                                           placeholder=""Name *"">
                                </div>
                            </div>
                            <div class=""nk-gap""></div>
                            <textarea class=""form-control required"" name=""message"" rows=""5""
                                      placeholder=""Message *""></textarea>
                            <div class=""nk-gap-1""></div>
                            <button class=""nk-btn nk-btn-rounded nk-btn-color-white"">
                           ");
                WriteLiteral(@"     <span>Send</span>
                                <span class=""icon""><i class=""ion-paper-airplane""></i></span>
                            </button>
                            <div class=""nk-form-response-success""></div>
                            <div class=""nk-form-response-error""></div>
                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </div>
                </div>
            </div>
            <div class=""col-md-6"">
                <div class=""nk-widget"">
                    <h4 class=""nk-widget-title""><span class=""text-main-1"">Latest</span> Posts</h4>
                    <div class=""nk-widget-content"">
                        <div class=""row vertical-gap sm-gap"">

                            <div class=""col-lg-6"">
                                <div class=""nk-widget-post-2"">
                                    <a href=""/Home/BlogArticle"" class=""nk-post-image"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "99b9707a762255b2449d716e577d46108e3070ff9216", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
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
                                    </a>
                                    <div class=""nk-post-title"">
                                        <a href=""/Home/BlogArticle"">
                                            Smell magic in the
                                            air. Or maybe barbecue
                                        </a>
                                    </div>
                                    <div class=""nk-post-date"">
                                        <span class=""fa fa-calendar""></span> Sep 18, 2018
                                        <span class=""fa fa-comments""></span> <a href=""#"">4</a>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-lg-6"">
                                <div class=""nk-widget-post-2"">
                                    <a href=""/Home/BlogArticle"" class=""nk-post-image"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "99b9707a762255b2449d716e577d46108e3070ff11365", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                    </a>
                                    <div class=""nk-post-title"">
                                        <a href=""/Home/BlogArticle"">
                                            Grab your sword and
                                            fight the Horde
                                        </a>
                                    </div>
                                    <div class=""nk-post-date"">
                                        <span class=""fa fa-calendar""></span> Sep 5, 2018
                                        <span class=""fa fa-comments""></span> <a href=""#"">7</a>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class=""nk-widget"">
                    <h4 class=""nk-widget-title""><span class=""text-main-1"">In</span> Twitter</h4>
                    <div class=""nk");
            WriteLiteral(@"-widget-content"">
                        <div class=""nk-twitter-list"" data-twitter-count=""1""></div>
                    </div>
                </div>
            </div>
        </div>
        <div class=""nk-gap-3""></div>
    </div>


    <div class=""container"">
        <div class=""row"">
            <div class=""col-md-4 col-sm-6"">
                <h6>Địa Chỉ Liên Hệ</h6>
                <iframe src=""https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.0573415705794!2d106.6266457147187!3d10.806920292300836!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752be27ea41e05%3A0xfa77697a39f13ab0!2zMTQwIEzDqiBUcuG7jW5nIFThuqVuLCBUw6J5IFRo4bqhbmgsIFTDom4gUGjDuiwgVGjDoG5oIHBo4buRIEjhu5MgQ2jDrSBNaW5oLCBWaeG7h3QgTmFt!5e0!3m2!1svi!2s!4v1665981497696!5m2!1svi!2s"" width=""300"" height=""350"" style=""border:0;""");
            BeginWriteAttribute("allowfullscreen", " allowfullscreen=\"", 5401, "\"", 5419, 0);
            EndWriteAttribute();
            WriteLiteral(@" loading=""lazy"" referrerpolicy=""no-referrer-when-downgrade""></iframe>
            </div>

            <div class=""col-md-2 col-sm-6"">
                <h6>Categories</h6>
                <ul class=""footer-links"">
                    <li><a href=""http://scanfcode.com/category/c-language/"">C</a></li>
                    <li><a href=""http://scanfcode.com/category/front-end-development/"">UI Design</a></li>
                    <li><a href=""http://scanfcode.com/category/back-end-development/"">PHP</a></li>
                    <li><a href=""http://scanfcode.com/category/java-programming-language/"">Java</a></li>
                    <li><a href=""http://scanfcode.com/category/android/"">Android</a></li>
                    <li><a href=""http://scanfcode.com/category/templates/"">Templates</a></li>
                </ul>
            </div>

            <div class=""col-md-4 col-sm-6"">
                <h6>Quick Links</h6>
                <div class=""cube"" style=""margin-left: 20%; margin-top: 20%;"">
            ");
            WriteLiteral(@"        <div class=""top""></div>
                    <div>
                        <span style=""--i:0;"">
                            <h2 style=""text-align: center; font-family: 'Lemonada', sans-serif;"">Game</h2>
                            <h2 style=""text-align: center; font-family: 'Lemonada', sans-serif;"">Game</h2>
                        </span>
                        <span style=""--i:1;"">
                            <h2 style=""text-align: center; font-family: 'Lemonada', sans-serif;"">Game</h2>
                            <h2 style=""text-align: center; font-family: 'Lemonada', sans-serif;"">Game</h2>
                        </span>
                        <span style=""--i:2;"">
                            <h2 style=""text-align: center; font-family: 'Lemonada', sans-serif;"">Game</h2>
                            <h2 style=""text-align: center; font-family: 'Lemonada', sans-serif;"">Game</h2>
                        </span>
                        <span style=""--i:3;"">
                            <");
            WriteLiteral(@"h2 style=""text-align: center; font-family: 'Lemonada', sans-serif;"">Game</h2>
                            <h2 style=""text-align: center; font-family: 'Lemonada', sans-serif;"">Game</h2>
                        </span>
                    </div>

                </div>
            </div>
            <div class=""col-md-2 col-sm-6"">
                <h6>Fanpage</h6>
                <iframe src=""https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2Fcongtyitnm&tabs=timeline&width=300&height=200&small_header=false&adapt_container_width=true&hide_cover=false&show_facepile=true&appId"" width=""300"" height=""200"" style=""border:none;overflow:hidden"" scrolling=""no"" frameborder=""0"" allowfullscreen=""true"" allow=""autoplay; clipboard-write; encrypted-media; picture-in-picture; web-share""></iframe>
            </div>
        </div>
        <hr>
    </div>
    <!-- End Contact Container -->
    <div class=""nk-copyright"">
        <div class=""container"">
            <div class=""nk-copyright");
            WriteLiteral(@"-left"">
                <a target=""_blank"" href=""#"">Cypy right || 2022 by Du An Game</a>
            </div>
            <div class=""nk-copyright-right"">
                <ul class=""nk-social-links-2"">
                    <li><a class=""nk-social-rss"" href=""#""><span class=""fa fa-rss""></span></a></li>
                    <li><a class=""nk-social-twitch"" href=""#""><span class=""fab fa-twitch""></span></a></li>
                    <li><a class=""nk-social-steam"" href=""#""><span class=""fab fa-steam""></span></a></li>
                    <li><a class=""nk-social-facebook"" href=""#""><span class=""fab fa-facebook""></span></a></li>
                    <li>
                        <a class=""nk-social-google-plus"" href=""#""><span class=""fab fa-google-plus""></span></a>
                    </li>
                    <li>
                        <a class=""nk-social-twitter"" href=""#"" target=""_blank"">
                            <span class=""fab fa-twitter""></span>
                        </a>
                    </li>
  ");
            WriteLiteral("                  <li>\r\n                        <a class=\"nk-social-pinterest\" href=\"#\"><span class=\"fab fa-pinterest-p\"></span></a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</footer>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
