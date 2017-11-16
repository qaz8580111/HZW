using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.Model;

namespace WebService
{
    /// <summary>
    /// SearchFLASHXHandler 的摘要说明
    /// </summary>
    public class SearchFLASHXHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int curpage = 0;
            int.TryParse(context.Request["curpage"], out curpage);

            string searchName = context.Request["searchName"];

            IQueryable<ILLEGALITEM> results = IllegalItemBLL
                .GetAllIllegalClassItem();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                searchName = searchName.Trim();
                results = results.Where(t =>
                    (t.ILLEGALITEMNAME.Contains(searchName) ||
                     t.ILLEGALCODE.Contains(searchName)));
            }

            int count = results.Count();
            int length = 10;
            int pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count) / length));

            string start = "<div style='margin-top:8px;' id='{0}' wz='{1}' fz='{2}' onclick='Details({0})'>";
            string end = "</div>";
            string text = "";

            List<ILLEGALITEM> lists = results
                .Skip((curpage - 1) * length)
                .Take(length).ToList();

            foreach (ILLEGALITEM list in lists)
            {
                text += start + list.ILLEGALCODE
                    + " " + list.ILLEGALITEMNAME + end;

                text = string.Format(text, list.ILLEGALITEMID, list.WEIZE, list.FZZE);
            }

            if (curpage == pages)
                curpage = 0;
            else
                curpage++;

            string result = "{\"curpage\":\"" + curpage
                + "\",\"text\":\"" + text + "\"}";

            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}