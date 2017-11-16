using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controls
{
    /// <summary> 此部分代码在 静态 HTML 最终确认后还需再调准
    /// 生成一级、二级、三级功能项等 HTML 代码
    /// 如果用户没有该菜单或者功能项的权限，则返回 display 属性为 none 的 HTML代码或者空的代码
    /// </summary>
    public class FunctionControl
    {
        /// <summary>
        /// 生成一级菜单项 HTML
        /// </summary>
        public static MvcHtmlString PrimaryMenuItem(string functionID,
            string functionCode, string functionName)
        {
            string html = "<li class='primaryMenuItem' id='{0}' code='{1}'>{2}</li>";

            string primaryMenuItemHtml = string.Format(html,
                functionID, functionCode, functionName);

            return MvcHtmlString.Create(primaryMenuItemHtml);
        }

        /// <summary>
        /// 生成二级菜单项 HTML
        /// </summary>
        public static MvcHtmlString SecondaryMenuItem()
        {
            return MvcHtmlString.Create("");
        }

        /// <summary>
        /// 生成三级菜单项 HTML
        /// </summary>
        public static MvcHtmlString ThirdMenuItem()
        {
            return MvcHtmlString.Create("");
        }

        /// <summary>
        /// 生成功能按钮 HTML
        /// </summary>
        public static MvcHtmlString FunctionButton()
        {
            return MvcHtmlString.Create("<button></button>");
        }

        /// <summary>
        /// 生成功能链接 HTML
        /// </summary>
        public static MvcHtmlString FunctionHref()
        {
            return MvcHtmlString.Create("<a></a>");
        }
    }
}