#pragma checksum "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\Designation\ViewAll.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "26697c4d9c1cef8469c738a9327b27ea404a72bf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Designation_ViewAll), @"mvc.1.0.view", @"/Views/Designation/ViewAll.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Designation/ViewAll.cshtml", typeof(AspNetCore.Views_Designation_ViewAll))]
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
#line 1 "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\_ViewImports.cshtml"
using ProjectManagementWebApp;

#line default
#line hidden
#line 2 "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\_ViewImports.cshtml"
using ProjectManagementWebApp.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26697c4d9c1cef8469c738a9327b27ea404a72bf", @"/Views/Designation/ViewAll.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32910fd44e5903024c93318d617829575c518c83", @"/Views/_ViewImports.cshtml")]
    public class Views_Designation_ViewAll : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\Designation\ViewAll.cshtml"
  
    ViewData["Title"] = "ViewAll";
    ViewData["ViewAllDesignation"] = "active";

#line default
#line hidden
            BeginContext(93, 71, true);
            WriteLiteral("\r\n<ol class=\"breadcrumb\">\r\n    <li class=\"breadcrumb-item\">\r\n        <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 164, "\"", 198, 1);
#line 9 "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\Designation\ViewAll.cshtml"
WriteAttributeValue("", 171, Url.Action("Index","Home"), 171, 27, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(199, 516, true);
            WriteLiteral(@"><i class=""fas fa-home""></i> Home</a>
    </li>
    <li class=""breadcrumb-item active""><i class=""fas fa-th-list""></i> View All Designation</li>
</ol>

<h4><i class=""fas fa-th-list"" style=""color: #1B5E20""></i> View All Designation</h4><hr />

<table class=""table"">
    <thead style=""background-color: #263238; color: white"">
        <tr>
            <td>
                Designation
            </td>
            <td>
                Action
            </td>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 28 "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\Designation\ViewAll.cshtml"
         foreach (Designation designation in ViewBag.Designations)
        {

#line default
#line hidden
            BeginContext(794, 38, true);
            WriteLiteral("            <tr>\r\n                <td>");
            EndContext();
            BeginContext(833, 27, false);
#line 31 "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\Designation\ViewAll.cshtml"
               Write(designation.DesignationName);

#line default
#line hidden
            EndContext();
            BeginContext(860, 42, true);
            WriteLiteral("</td>\r\n                <td><a title=\"Edit\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 902, "\"", 967, 1);
#line 32 "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\Designation\ViewAll.cshtml"
WriteAttributeValue("", 909, Url.Action("Edit","Designation", new {id=designation.Id}), 909, 58, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(968, 81, true);
            WriteLiteral("><i class=\"fas fa-edit\" style=\"color: #0D47A1\"></i></a></td>\r\n            </tr>\r\n");
            EndContext();
#line 34 "F:\ASP.NET CORE\Project\ProjectManagementWebApplication\ProjectManagementWebApp\ProjectManagementWebApp\Views\Designation\ViewAll.cshtml"
        }

#line default
#line hidden
            BeginContext(1060, 26, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
            EndContext();
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
