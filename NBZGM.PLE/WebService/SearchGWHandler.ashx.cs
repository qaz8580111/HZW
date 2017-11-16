using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.DBHelper;
using Taizhou.PLE.Model.GSGGModels;

namespace WebService
{
    /// <summary>
    /// SearchGWHandler 的摘要说明
    /// </summary>
    public class SearchGWHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int curpage, type = 0;
            int.TryParse(context.Request["curpage"], out curpage);
            int.TryParse(context.Request["GWType"], out type);

            string searchName = context.Request["searchName"];
            string account = context.Request["account"];

            if (string.IsNullOrWhiteSpace(account))
                return;

            List<GWDetail> results = DBHelper.GetGWInfoList(account, type);

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                searchName = searchName.Trim();
                results = results.Where(t =>
                    t.Run_Name.Contains(searchName)).ToList();
            }

            int count = results.Count();
            int length = 5;
            int pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count) / length));

            string start = "<div style='margin-top:8px;' id='{0}' flow_id='{1}' onclick='Details({0})'>";
            string end = "</div>";
            string text = "";

            List<GWDetail> lists = results
                .Skip((curpage - 1) * length)
                .Take(length).ToList();

            foreach (GWDetail list in lists)
            {
                text += start + list.Run_Name
                    + " " + list.Begin_Time + end;

                text = string.Format(text, list.Run_ID, list.Flow_ID);
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