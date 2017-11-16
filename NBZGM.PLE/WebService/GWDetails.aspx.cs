using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taizhou.PLE.BLL.DBHelper;
using Taizhou.PLE.Model.GSGGModels;

namespace WebService
{
    public partial class GWDetails : System.Web.UI.Page
    {
        public GWDetail result { get; set; }
        protected string account { get; set; }

        //局领导意见
        protected string JLDYJ { get; set; }
        //相关科室意见
        protected string XGKSYJ { get; set; }
        //相关单位情况确认
        protected string XGDWQKQR { get; set; }
        //相关科室情况确认
        protected string XGKSQKQR { get; set; }
        //相关单位办理结果反馈
        protected string XGDWBLJGFK { get; set; }
        //相关科室办理结果反馈
        protected string XGKSBLJGFK { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string strRun_ID = this.Request["RUN_ID"];
            string strFlow_ID = this.Request["FLOW_ID"];
            account = this.Request["Account"];

            int run_ID, flow_ID = 0;
            if (int.TryParse(strRun_ID, out run_ID) &&
                int.TryParse(strFlow_ID, out flow_ID))
            {
                if (flow_ID == 102)
                {
                    this.form2.Visible = true;
                }
                else if (flow_ID == 103)
                {
                    this.form1.Visible = true;
                }

                result = DBHelper.GetGWDetail(run_ID, flow_ID);

                List<GWFeedBack> lists = DBHelper.GetGWFeedBackList(run_ID);

                foreach (GWFeedBack list in lists)
                {
                    string temp = list.Edit_Time.ToString()
                            + list.Dept_Name + list.User_Name
                            + ":" + list.Content + "</br>";

                    //局领导意见
                    if (list.Flow_Prcs == 3)
                    {
                        JLDYJ += temp;
                    }
                    //相关科室意见
                    else if (list.Flow_Prcs == 5)
                    {
                        XGKSYJ += temp;
                    }
                    //相关单位情况确认
                    else if (list.Flow_Prcs >= 30 && list.Flow_Prcs <= 38)
                    {
                        XGDWQKQR += temp;
                    }
                    //相关科室情况确认
                    else if (list.Flow_Prcs >= 6 && list.Flow_Prcs <= 16)
                    {
                        XGKSQKQR += temp;
                    }
                    //相关单位办理结果反馈
                    else if (list.Flow_Prcs >= 39 && list.Flow_Prcs <= 47)
                    {
                        XGDWBLJGFK += temp;
                    }
                    //相关科室办理结果反馈
                    else if (list.Flow_Prcs >= 17 && list.Flow_Prcs <= 27)
                    {
                        XGKSBLJGFK += temp;
                    }
                }
            }
        }
    }
}