#pragma checksum "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5e7cc6d5749aab310b991cb7e80787495eb0407b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__BooksSliderPartial), @"mvc.1.0.view", @"/Views/Shared/_BooksSliderPartial.cshtml")]
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
#line 1 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\_ViewImports.cshtml"
using Pustok.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\_ViewImports.cshtml"
using Pustok.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5e7cc6d5749aab310b991cb7e80787495eb0407b", @"/Views/Shared/_BooksSliderPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87243a1568112d80dfa095d1530320a6b18ee896", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__BooksSliderPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Book>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<div class=""product-slider multiple-row  slider-border-multiple-row  sb-slick-slider""
     data-slick-setting='{
                            ""autoplay"": true,
                            ""autoplaySpeed"": 8000,
                            ""slidesToShow"": 5,
                            ""rows"":1,
                            ""dots"":true
                        }' data-slick-responsive='[
                            {""breakpoint"":1200, ""settings"": {""slidesToShow"": 3} },
                            {""breakpoint"":768, ""settings"": {""slidesToShow"": 2} },
                            {""breakpoint"":480, ""settings"": {""slidesToShow"": 1} },
                            {""breakpoint"":320, ""settings"": {""slidesToShow"": 1} }
                        ]'>
");
#nullable restore
#line 15 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"single-slide\">\r\n            <div class=\"product-card\">\r\n                <div class=\"product-header\">\r\n                    <a href=\"#\" class=\"author\">\r\n                        ");
#nullable restore
#line 21 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                   Write(item.Author.Fullname.Split(" ")[1]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </a>\r\n                    <h3>\r\n                        <a href=\"product-details.html\">\r\n                            ");
#nullable restore
#line 25 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                       Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </a>\r\n                    </h3>\r\n                </div>\r\n                <div class=\"product-card--body\">\r\n                    <div class=\"card-image\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "5e7cc6d5749aab310b991cb7e80787495eb0407b5729", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1408, "~/assets/image/products/", 1408, 24, true);
#nullable restore
#line 31 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
AddHtmlAttributeValue("", 1432, item.BookPhotos.FirstOrDefault(x=>x.PosterStatus==true).Name, 1432, 61, false);

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
            WriteLiteral("\r\n                        <div class=\"hover-contents\">\r\n                            <a href=\"product-details.html\" class=\"hover-image\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "5e7cc6d5749aab310b991cb7e80787495eb0407b7563", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1681, "~/assets/image/products/", 1681, 24, true);
#nullable restore
#line 34 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
AddHtmlAttributeValue("", 1705, item.BookPhotos.FirstOrDefault(x=>x.PosterStatus==false).Name, 1705, 62, false);

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
                            <div class=""hover-btns"">
                                <a href=""cart.html"" class=""single-btn"">
                                    <i class=""fas fa-shopping-basket""></i>
                                </a>
                                <a href=""wishlist.html"" class=""single-btn"">
                                    <i class=""fas fa-heart""></i>
                                </a>
                                <a href=""compare.html"" class=""single-btn"">
                                    <i class=""fas fa-random""></i>
                                </a>
                                <a href=""#"" data-toggle=""modal"" data-target=""#quickModal""
                                   class=""single-btn"">
                                    <i class=""fas fa-eye""></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class=""price-bl");
            WriteLiteral("ock\">\r\n");
#nullable restore
#line 54 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                         if (item.DiscountPercent > 0)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span class=\"price\">£");
#nullable restore
#line 56 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                                            Write(item.DiscountedPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                            <del class=\"price-old\">£");
#nullable restore
#line 57 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                                               Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</del>\r\n                            <span class=\"price-discount\">");
#nullable restore
#line 58 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                                                    Write(item.DiscountPercent);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</span>\r\n");
#nullable restore
#line 59 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span class=\"price\">£");
#nullable restore
#line 62 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                                            Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 63 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 68 "C:\Users\Admin\Desktop\p120_be_15-03-2021-Javidan1997\Pustok\Pustok\Views\Shared\_BooksSliderPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Book>> Html { get; private set; }
    }
}
#pragma warning restore 1591