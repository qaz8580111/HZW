using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class FunctionItemExtersions
    {
        public static MvcHtmlString GetPrimaryMenu(this HtmlHelper helper,
            string functionID, string functionCode, string functionName)
        {
            TagBuilder functionItem = new TagBuilder("div");
            functionItem.MergeAttribute("id", "functionID" + functionID, true);
            functionItem.MergeAttribute("functionCode", functionCode, true);
            functionItem.SetInnerText(functionName);
            functionItem.AddCssClass("primaryMenuItem");    // 为标题添加样式

            return MvcHtmlString.Create(functionItem.ToString());
        }
    }
}