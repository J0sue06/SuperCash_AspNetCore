#pragma checksum "D:\Trabajos\SuperCash_AspNetCore\SuperCash\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6775d56da215188e9a2f89aa021b055a788c2da2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\Trabajos\SuperCash_AspNetCore\SuperCash\Views\_ViewImports.cshtml"
using SuperCash;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Trabajos\SuperCash_AspNetCore\SuperCash\Views\_ViewImports.cshtml"
using SuperCash.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6775d56da215188e9a2f89aa021b055a788c2da2", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"309e11f87acbff43fb38652788e38aeaef800381", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/profile-user.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("user icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Chat.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Trabajos\SuperCash_AspNetCore\SuperCash\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"  
        <!--component-->
        <div class=""dash d-flex flex-col mx-auto responsive-width primary-bg"">
            <div class="" row d-flex justify-content-start small-side-padding mt-5 mb-5 rounded"">
                <div class=""user-briefing d-flex"">
                    <div class=""icon"">                        
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6775d56da215188e9a2f89aa021b055a788c2da25025", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
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
                    <div class=""id d-flex flex-col pr-22"">
                        <h3 class=""mb-0 switch-font"">ID</h3>
                        <h2 class=""color-primary"">1</h2>
                    </div>
                    <div class=""rank d-flex flex-col pr-22"">
                        <h3 class=""mb-0 switch-font"">Rank</h3>
                        <h2 class=""bold-2"">Corporate II</h2>
                    </div>
                </div>
                <div class=""d-flex flex-col pr-22"">
                    <h3 class=""mb-0 switch-font"">Balance</h3>
                    <h2 class=""color-primary"">25,537 TRX</h2>
                </div>
            </div>
            <div class=""dash-section row mb-5"">
                <div class=""col-lg-7 flex-col"">
                    <h2 class=""bold small-side-padding switch-font"">My affiliate licenses</h2>
                    <!--div para comprar licencias-->
                    <div id=""non-licensed-user"" class=""licenses-wrap mb-5 ");
            WriteLiteral(@"row small-side-padding"">
                        <div class=""col-lg-12 license "">
                            <div class=""buy-license-cta"">
                                <h2>You're just one step from making money</h2>
                                <h3>Get your official license and let the fun begin!</h3>
                                <div class=""buy-license-button-wrap"">
                                    <button class=""btn btn-success width-100 mb-2"">I want to buy my license</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--cuando las licencias ya estan compradas-->
                    <div id=""licensed-user"" class=""licenses-wrap row small-side-padding"">
                        <div class=""license col-lg-6"">
                            <div class=""d-flex rounded full-height flex-col third-bg license-inner"">
                                <div class=""d-flex license-type-and");
            WriteLiteral(@"-level justify-content-between"">
                                    <div class=""license-type switch-font"">
                                        <h3>DIRECT</h3>
                                        <h4>80% COMMISSION</h4>
                                        <br>
                                    </div>
                                    <div class=""license-level d-flex align-items-end flex-col switch-font"">
                                        <h3>LEVEL 1</h3>
                                        <button>Upgrade</button>
                                    </div>
                                </div>
                                <div class=""d-flex license-price full-height"">
                                    <div class=""price full-height"">
                                        <h1>200 <span class=""minified"">TRX</span></h1>
                                    </div>
                                </div>
                            </div>
                        </di");
            WriteLiteral(@"v>
                        <div class=""license rounded col-lg-6"">
                            <div class=""d-flex rounded full-height flex-col third-bg license-inner"">
                                <div class=""d-flex license-type-and-level justify-content-between"">
                                    <div class=""license-type switch-font"">
                                        <h3>TEAM</h3>
                                        <h4>20% COMMISSION</h4>
                                        <h4>x 4 LEVELS</h4>
                                    </div>
                                    <div class=""license-level d-flex align-items-end flex-col switch-font"">
                                        <h3>LEVEL 1</h3>
                                        <button>Upgrade</button>
                                    </div>
                                </div>
                                <div class=""d-flex license-price full-height"">
                                    <div class=""price f");
            WriteLiteral(@"ull-height"">
                                        <h1>200 <span class=""minified"">TRX</span></h1>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h3 class=""small-side-padding"">My affiliate link: <a href=""#"">https://thislink/ref?=45 <i class=""bi bi-files""></i></a></h3>
                    <hr id=""vertical-screen-divisor"">
                </div>
                <div class=""col-lg-5 small-side-padding"">
                    <h2 class=""bold switch-font small-side-padding"">Total earnings</h2>
                    <div class=""earnings-history mb-4 small-side-padding"">
                        <h1 class=""mb-0 accent"">256,890 TRX</h1>
                        <h3 class=""subtle"">USD$45,000</h3>
                    </div>
                    <div class=""direct-and-team row mb-4 small-side-padding"">
                        <div class=""col-xs-6 pr-22"">
       ");
            WriteLiteral(@"                     <h3 class=""mb-0"">Direct</h3>
                            <h3 class=""accent"">56,977 TRX</h3>
                        </div>
                        <div class=""col-xs-6"">
                            <h3 class=""mb-0"">Team</h3>
                            <h3 class=""accent"">176,267 TRX</h3>
                        </div>
                    </div>
                    <div class=""buttons row small-side-padding"">
                        <div class=""col-lg-6 pr-15"">
                            <button class=""btn btn-success width-100 mb-2"">Deposit</button>
                        </div>
                        <div class=""col-lg-6 pr-15"">
                            <button class=""btn btn-outline-success width-100"">Withdraw</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--component end-->
        <!--component-->
        <div class=""complementary responsive-width mx-auto"">
            <d");
            WriteLiteral(@"iv class=""d-flex row mt-5"">
                <div class=""col-lg-7  mb-4"">
                    <div class=""complementary-info-wrap secondary-bg d-flex full-height flex-col"">
                        <div class=""d-flex justify-content-between align-items-center small-side-padding"">
                            <h2 class=""bold switch-font"">My affiliates</h2>
                            <a");
            BeginWriteAttribute("class", " class=\"", 6978, "\"", 6986, 0);
            EndWriteAttribute();
            WriteLiteral(@" href=""#"">
                                View all
                                <i class=""bi bi-arrow-right""></i>
                            </a>
                        </div>
                        <div class=""affiliate-info-wrap row small-side-padding"">
                            <div class=""col-lg-3"">
                                <h3>Direct</h3>
                                <div class=""direct-affiliates d-flex align-items-center"">
                                    <i class=""bi bi-person""></i>
                                    <h1 class=""accent"">216</h1>
                                </div>
                            </div>
                            <div class=""col-lg-3"">
                                <h3>Team</h3>
                                <div class=""direct-affiliates d-flex align-items-center"">
                                    <i class=""bi bi-people""></i>
                                    <h1 class=""accent"">789</h1>
                                </d");
            WriteLiteral(@"iv>
                            </div>
                            <div class=""col-lg-6"">
                                <h3>Top members</h3>
                                <div class=""top-members-list"">
                                    <table class=""table table-borderless table-hover"">
                                        <thead>
                                            <tr>
                                                <th scope=""col"">ID</th>
                                                <th class=""text-right"" scope=""col"">Earned (TRX)</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th scope=""row"">89</th>
                                                <td class=""text-right"">156,000 TRX</td>
                                            </tr>
                                      ");
            WriteLiteral(@"      <tr>
                                                <th scope=""row"">114</th>
                                                <td class=""text-right"">89,780 TRX</td>
                                            </tr>
                                            <tr>
                                                <th scope=""row"">39</th>
                                                <td class=""text-right"">38,550 TRX</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-5 secondary-bg  mb-4"">
                    <div class=""d-flex justify-content-between align-items-center small-side-padding"">
                        <h2 class=""bold switch-font"">Recent payments</h2>
                        <a");
            BeginWriteAttribute("class", " class=\"", 10042, "\"", 10050, 0);
            EndWriteAttribute();
            WriteLiteral(@" href=""#"">
                            View all
                            <i class=""bi bi-arrow-right""></i>
                        </a>
                    </div>
                    <h3 class=""subtle small-side-padding "">See where your commission is coming from</h3>
                    <div class=""recent-payment-list small-side-padding"">
                        <table class=""table table-borderless table-hover"">
                            <thead>
                                <tr>
                                    <th scope=""col"">Amount</th>
                                    <th>Type</th>
                                    <th class=""text-right"" scope=""col"">ID</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th scope=""row"">12,600 TRX</th>
                                    <td>Team member</td>
                                    <td class=""");
            WriteLiteral(@"text-right"">456</td>
                                </tr>
                                <tr>
                                    <th scope=""row"">8,400 TRX</th>
                                    <td>Direct</td>
                                    <td class=""text-right"">79</td>
                                </tr>
                                <tr>
                                    <th scope=""row"">12,600 TRX</th>
                                    <td>Direct</td>
                                    <td class=""text-right"">724</td>
                                </tr>
                                <tr>
                                    <th scope=""row"">6,300 TRX</th>
                                    <td>Direct</td>
                                    <td class=""text-right"">322</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6775d56da215188e9a2f89aa021b055a788c2da218558", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6775d56da215188e9a2f89aa021b055a788c2da219598", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
