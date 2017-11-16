using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Taizhou.PLE.BLL.PublicService;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Taizhou.PLE.BLL.ZFSJBLL;
using System.IO;
using System.Collections;
using System.Configuration;

namespace Taizhou.PLE.BLL.AidDecisionMaking
{
    /// <summary>
    /// 统计
    /// </summary>
    public class ReportStatisticsBLL
    {
        /// <summary>
        /// 获得一般案件的数据
        /// </summary>
        /// <returns></returns>
        public static List<CaseCount> GetCaseCount()
        {
            
            PLEEntities db = new PLEEntities();
            int yy = DateTime.Now.Year;
            int mm = DateTime.Now.Month;
            DateTime datetimenow = new DateTime(yy, 1, 1);
            string sqltext = "select UNITID,UNITNAME, ";
            for (int i = 1; i < 13; i++)
            {
                if (i == 12)
                {
                    sqltext += "(case when 1=1 then (select count(1) from workflowinstances where workflowinstances.unitid=units.unitid and createdtime between ('1-" + i + "月 -" + yy + "' ) and  ('1-" + 1 + "月 -" + (yy + 1) + "')) else 0 end) " + GetMonth(i) + " " + ",";
                }
                else
                {
                    sqltext += "(case when 1=1 then (select count(1) from workflowinstances where workflowinstances.unitid=units.unitid and createdtime between ('1-" + i + "月 -" + yy + "' ) and  ('1-" + (i + 1) + "月 -" + yy + "')) else 0 end) " + GetMonth(i) + " " + ",";
                }
            }
            sqltext += "(case when 1=1 then (select count(1) from workflowinstances where workflowinstances.unitid=units.unitid and createdtime between ('1-1月 -" + yy + "' ) and  ('1-1月 -" + (yy + 1) + "') group by workflowinstances.unitid) else 0 end) counts from units where unittypeid = 5 order by SEQNO";
            List<CaseCount> list = db.Database.SqlQuery<CaseCount>(sqltext).ToList();
            return list;
        }
        /// <summary>
        /// 获得简易事件的数据
        /// </summary>
        /// <returns></returns>
        public static List<CaseCount> GetSimpleCase()
        {
            PLEEntities db = new PLEEntities();
            int yy = DateTime.Now.Year;
            int mm = DateTime.Now.Month;
            DateTime datetimenow = new DateTime(yy, 1, 1);
            string sqltext = "select UNITID,UNITNAME, ";
            for (int i = 1; i < 13; i++)
            {

                if (i == 12)
                {
                    sqltext += "(case when 1=1 then (select count(1) from Simplecases where Simplecases.untiid=units.unitid and CaseTime between ('1-" + i + "月 -" + yy + "' ) and  ('1-" + 1 + "月 -" + (yy + 1) + "')) else 0 end) " + GetMonth(i) + " " + ",";
                }
                else
                {
                    sqltext += "(case when 1=1 then (select count(1) from Simplecases where Simplecases.untiid=units.unitid and CaseTime between ('1-" + i + "月 -" + yy + "' ) and  ('1-" + (i + 1) + "月 -" + yy + "')) else 0 end) " + GetMonth(i) + " " + ",";
                }
            }
            sqltext += "(case when 1=1 then (select count(1) from Simplecases where Simplecases.untiid=units.unitid and CaseTime between ('1-1月 -" + yy + "' ) and  ('1-1月 -" + (yy + 1) + "') group by Simplecases.untiid) else 0 end) counts from units where unittypeid = 5 order by SEQNO";
            List<CaseCount> list = db.Database.SqlQuery<CaseCount>(sqltext).ToList();
            return list;

        }
        /// <summary>
        /// 获得执法事件的数据
        /// </summary>
        /// <returns></returns>
        public static List<CaseCount> GetZFSJCount()
        {
            PLEEntities db = new PLEEntities();
            int yy = DateTime.Now.Year;
            int mm = DateTime.Now.Month;
            DateTime datetimenow = new DateTime(yy, 1, 1);
            string sqltext = "select UNITID,UNITNAME, ";
            for (int i = 1; i < 13; i++)
            {
                if (i == 12)
                {
                    sqltext += "(case when 1=1 then (select count(1) from ZFSJWorkflowInstances where ZFSJWorkflowInstances.untiid=units.unitid and createtime between ('1-" + i + "月 -" + yy + "' ) and  ('1-" + 1 + "月 -" + (yy + 1) + "')) else 0 end) " + GetMonth(i) + " " + ",";
                }
                else
                {
                    sqltext += "(case when 1=1 then (select count(1) from ZFSJWorkflowInstances where ZFSJWorkflowInstances.untiid=units.unitid and createtime between ('1-" + i + "月 -" + yy + "' ) and  ('1-" + (i + 1) + "月 -" + yy + "')) else 0 end) " + GetMonth(i) + " " + ",";
                }
            }
            sqltext += "(case when 1=1 then (select count(1) from ZFSJWorkflowInstances where ZFSJWorkflowInstances.untiid=units.unitid and createtime between ('1-1月 -" + yy + "' ) and  ('1-1月 -" + (yy + 1) + "') group by ZFSJWorkflowInstances.untiid) else 0 end) counts from units where unittypeid = 5 order by SEQNO";

            List<CaseCount> list = db.Database.SqlQuery<CaseCount>(sqltext).ToList();
            return list;

        }



        /// <summary>
        /// 获取执法事件 96310事件每天的数剧
        /// </summary>
        /// <param name="zfsjcounts"></param>
        /// <returns></returns>
        public static List<ZFSJCHARTBYQL> GetZFSJBYQL(decimal classid)
        {
            PLEEntities db = new PLEEntities();
            //获取当前年份
            int yy = DateTime.Now.Year;
            //获取当前月份
            int mm = DateTime.Now.Month;
            //转换为当前月份的第一天
            DateTime datetimenow = new DateTime(yy, mm, 1);
            //当前月份下个月的第一天
            DateTime datetimenext = datetimenow.AddMonths(1);
            string sqltext = "select * from zfsjchartbyql  where statusid=1 and classid=" + classid + " and dttime>=to_date('" + datetimenow + "','yyyy-mm-dd hh24:mi:ss') and dttime<=to_date('" + datetimenext + "','yyyy-mm-dd hh24:mi:ss') order by dttime asc";
            List<ZFSJCHARTBYQL> list = db.Database.SqlQuery<ZFSJCHARTBYQL>(sqltext).ToList();
            return list;
        }


        /// <summary>
        /// ZFSJChartByQL表中导入数据
        /// </summary>
        public static void InsertZFSJChartByQL()
        {
            
            PLEEntities db = new PLEEntities();
            string sqlText = "";
            ////文件夹路径
            //string flieWName = string.Format(@"D:\ZFSJSJ");

            //文件路径
            string fileName = ConfigurationSettings.AppSettings["NewFile"].ToString();

            if (!File.Exists(fileName))//表示第一次进行导入数据（文件是没有创建的）
            {
                sqlText = "select * from ggfwevents where  createtime<=to_date('"+DateTime.Now+" ','yyyy-mm-dd hh24:mi:ss') order by createtime  desc";
                //判断文件夹是否存在
                //if (!Directory.Exists(flieWName))
                //    Directory.CreateDirectory(flieWName);
                //第一次时，把zfsjchartbyql里的数据删除，重新导入
                InsertZFSJChartByQLBLL.DeleteZFSJChartByQL();
                //创建文件
                WriteList(DateTime.Now);
            }
            else
            {
                ArrayList ReadListTxt = ReadList();
                string BeinTime = ReadListTxt[0].ToString();
                sqlText = "select * from ggfwevents where createtime>=to_date('" + BeinTime + "','yyyy-mm-dd hh24:mi:ss') order by createtime desc";
            }

            //获得ZFSJWORKFLOWINSTANCE的所有数据
            IList<GGFWEVENT> list = db.Database.SqlQuery<GGFWEVENT>(sqlText).ToList();
            ZFSJCHARTBYQL model = new Model.ZFSJCHARTBYQL();
            decimal QuestionDLID = 0;
            foreach (var item in list)
            {
                //反序列化json
                //   ZFSJForm Ilist = JsonHelper
                //.JsonDeserialize<ZFSJForm>(item.WDATA);

                //获取创建事件
                DateTime? Time = item.CREATETIME;

                //获取大类ID
                if (item.CLASSBID != null)
                {
                    QuestionDLID = decimal.Parse(item.CLASSBID.ToString());
                }
                else
                {
                    continue;
                }

                //获取事件来源
                string EventSourceID = item.EVENTSOURCE;

                //根据获得的大类ID跟事件来源ID查询数据库
                IList<ZFSJCHARTBYQL> SJlist = PDYOU(QuestionDLID, Time);

                if (SJlist.Count == 0) //表示数据库没有值，则在数据中添加一条数据
                {
                    model.DTTIME = Time;
                    model.CLASSID = QuestionDLID;
                    if (ZFSJQuestionClassBLL.GetZFSHQuestionByID(QuestionDLID) != null)
                    {
                        model.CLASSNAME = ZFSJQuestionClassBLL.GetZFSHQuestionByID(QuestionDLID).CLASSNAME.ToString();
                    }
                    else
                    {
                        continue;
                    }
                    if (EventSourceID == "5")
                    {
                        //表示获取的这条数据是96310事件，则加入的新数据，事件96310的值为1
                        model.SJ96310 = 1;
                        model.ZFSJCOUNTS = 0;
                        model.STATUSID = 1;
                    }
                    else
                    {
                        //表示获取的这条数据是执法事件数据，则加入的新数据，执法事件的值为1
                        model.SJ96310 = 0;
                        model.ZFSJCOUNTS = 1;
                        model.STATUSID = 1;
                    }
                    InsertZFSJChartByQLBLL.InsertZFSJChartByQLS(model);
                }
                else
                {
                    //表示数据库中有值，做修改数据操作
                    model.ZCID = SJlist.FirstOrDefault().ZCID;
                    if (EventSourceID == "5")
                    {
                        //表示获取的数据是96310事件，则在原来的数值上+1
                        model.SJ96310 = SJlist.FirstOrDefault().SJ96310 + 1;
                        model.ZFSJCOUNTS = SJlist.FirstOrDefault().ZFSJCOUNTS;
                    }
                    else
                    {
                        //表示获取的数据是执法事件，则在原来的数值上+1
                        model.ZFSJCOUNTS = SJlist.FirstOrDefault().ZFSJCOUNTS + 1;
                        model.SJ96310 = SJlist.FirstOrDefault().SJ96310;
                    }

                    InsertZFSJChartByQLBLL.UpdateZFSJChartByQL(model);
                }

            }
            //获取数据修改后把最新的时间放进文件里
            WriteList(DateTime.Now);
        }

        /// <summary>
        /// 判断数据库是否有值
        /// </summary>
        /// <param name="ClassDL"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<ZFSJCHARTBYQL> PDYOU(decimal ClassDL, DateTime? dt)
        {
            PLEEntities db = new PLEEntities();
            string sqlText = "select * from zfsjChartBYQL  where statusid=1 and classID='" + ClassDL + "' and dttime>=to_date('" + dt + "','yyyy-mm-dd hh24:mi:ss') and dttime<=to_date('" + DateTime.Parse(dt.ToString()).AddDays(1) + "','yyyy-mm-dd hh24:mi:ss')";
            //根据条件判断数据库里是否有值
            IList<ZFSJCHARTBYQL> list = db.Database.SqlQuery<ZFSJCHARTBYQL>(sqlText).ToList();

            return list;
        }

        /// <summary>
        /// 读取定义文件里的数据
        /// </summary>
        /// <returns></returns>
        public static ArrayList ReadList()
        {//文件路径
            string fileName = ConfigurationSettings.AppSettings["NewFile"].ToString();
            //string fileName = string.Format(@"D:\ZFSJSJ\Time.txt");
            //读取数据
            StreamReader objReader = new StreamReader(fileName);
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            return LineList;

        }

        /// <summary>
        /// 查询完后把最获得的最新时间放入文件中
        /// </summary>
        public static void WriteList(DateTime dt)
        {
            //文件路径
            string fileName = ConfigurationSettings.AppSettings["NewFile"].ToString();
            //string fileName = string.Format(@"D:\Time.txt");
            //写文件：
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);

            //开始写入 把最新的时间放进去
            sw.Write(DateTime.Now);

            //清空缓冲区
            sw.Flush();

            //关闭流
            sw.Close();
            fs.Close();

        }


        /// <summary>
        /// 返回英文月份名称
        /// </summary>
        /// <param name="mon">数字</param>
        /// <returns></returns>
        public static string GetMonth(int mon)
        {
            string month = "";
            if (mon == 1)
            {
                month = "January ";
            }
            else if (mon == 2)
            {
                month = "February";
            }
            else if (mon == 3)
            {
                month = "March";
            }
            else if (mon == 4)
            {
                month = "April";
            }
            else if (mon == 5)
            {
                month = "May ";
            }
            else if (mon == 6)
            {
                month = "June";
            }
            else if (mon == 7)
            {
                month = "July";
            }
            else if (mon == 8)
            {
                month = "August ";
            }
            else if (mon == 9)
            {
                month = "September";
            }
            else if (mon == 10)
            {
                month = "October";
            }
            else if (mon == 11)
            {
                month = "November";
            }
            else if (mon == 12)
            {
                month = "December";
            }
            return month;
        }

        /// <summary>
        /// 导入公共服务月报数据
        /// </summary>
        public static void GetGGFWChart(DateTime firstdate, DateTime lastdate)
        {
            PLEEntities db = new PLEEntities();
            DateTime now = DateTime.Now;
            //DateTime firstdate = new DateTime(now.Year, now.Month, 1);
            //DateTime lastdate = now.AddMonths(1).AddDays(-1);
            //案件分类
            string AJFLsql = "Select Count(Tt.Sourcename) FRACTION,Tt.Classid CID,tt.sourceid GGFWSID,tt.Classname cname From (Select  Ggfwsources.Sourcename,Zfsjquestionclasses.Classname,Ggfwevents.Createtime,Zfsjquestionclasses.Classid,Ggfwsources.Sourceid From Ggfwevents  Inner Join Ggfwsources  On Ggfwevents.Eventsource=Ggfwsources.Sourceid Inner Join Zfsjquestionclasses On Ggfwevents.Classbid=Zfsjquestionclasses.Classid) tt Where Tt.Createtime >=to_date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) And Tt.Createtime<=to_date('" + lastdate + "','yyyy-mm-dd hh24:mi:ss')  Group By Tt.Sourcename,Tt.Classname,Tt.Classid,Tt.Sourceid";
            List<GGFWReport> list = db.Database.SqlQuery<GGFWReport>(AJFLsql).ToList();
            GGFWMONTHLYREPORT model = new GGFWMONTHLYREPORT();
            foreach (var item in list)
            {
                decimal cid = 0;
                decimal.TryParse(item.CID, out cid);
                model.CID = cid;
                model.FRACTION = item.FRACTION.ToString();
                model.GGFWSID = item.GGFWSID;
                model.CREATETIME = firstdate;
                new GGFWMONTHLYREPORTSBLL().AddGGFWMONTHLYREPORT(model);
            }
            //上月转结
            string SYZJsql = "Select Count(Tt.Sourcename) FRACTION,tt.sourceid GGFWSID from (Select Ggfwsources.Sourcename,Ggfwevents.Createtime,Ggfwsources.Sourceid From Ggfwevents Inner Join Zfsjworkflowinstances On Ggfwevents.Wiid=Zfsjworkflowinstances.Wiid Inner Join Ggfwsources  On Ggfwevents.Eventsource=Ggfwsources.Sourceid Where  To_Char(Zfsjworkflowinstances.UPDATETIME, 'mm')=To_Char(Sysdate, 'mm')And To_Char(Overtime, 'mm')<To_Char(Sysdate, 'mm') And Ggfwevents.Statue In (5,6))tt Where Tt.Createtime >=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) And Tt.Createtime<=To_Date('" + lastdate + "','yyyy-mm-dd hh24:mi:ss') Group By Tt.Sourcename,Tt.Sourceid";
            List<GGFWReport> SYZJlist = db.Database.SqlQuery<GGFWReport>(SYZJsql).ToList();
            foreach (var item in SYZJlist)
            {
                model.CID = 1;
                model.FRACTION = item.FRACTION.ToString();
                model.GGFWSID = item.GGFWSID;
                model.CREATETIME = firstdate;
                new GGFWMONTHLYREPORTSBLL().AddGGFWMONTHLYREPORT(model);
            }
            //本月受理
            string BYSLsql = "Select Count(Tt.Sourcename) FRACTION,tt.sourceid GGFWSID From (Select Ggfwsources.Sourcename,Ggfwevents.Createtime,Ggfwsources.Sourceid From Ggfwevents Inner Join Ggfwsources  On Ggfwevents.Eventsource=Ggfwsources.Sourceid ) Tt Where Tt.Createtime >=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) And Tt.Createtime<=To_Date('" + lastdate + "','yyyy-mm-dd hh24:mi:ss') Group By Tt.Sourcename,Tt.Sourceid";
            List<GGFWReport> BYSLlist = db.Database.SqlQuery<GGFWReport>(BYSLsql).ToList();
            foreach (var item in BYSLlist)
            {
                model.CID = 2;
                model.FRACTION = item.FRACTION.ToString();
                model.GGFWSID = item.GGFWSID;
                model.CREATETIME = firstdate;
                new GGFWMONTHLYREPORTSBLL().AddGGFWMONTHLYREPORT(model);
            }
            //本月办结
            string BYBJsql = "Select Count(Tt.Sourcename) FRACTION ,Tt.Sourceid GGFWSID From ( Select Ggfwsources.Sourcename,Ggfwsources.Sourceid From Ggfwevents Inner Join Ggfwsources On Ggfwevents.Eventsource=Ggfwsources.Sourceid  Where Ggfwevents.Createtime >=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' )And Ggfwevents.Createtime<=To_Date('" + lastdate + "','yyyy-mm-dd hh24:mi:ss') And Statue=6 And To_Char(Createtime,'mm')=to_char(sysdate,'mm')   ) tt group by tt.Sourcename,tt.Sourceid";
            List<GGFWReport> BYBJlist = db.Database.SqlQuery<GGFWReport>(BYBJsql).ToList();
            foreach (var item in BYBJlist)
            {
                model.CID = 3;
                model.FRACTION = item.FRACTION.ToString();
                model.GGFWSID = item.GGFWSID;
                model.CREATETIME = firstdate;
                new GGFWMONTHLYREPORTSBLL().AddGGFWMONTHLYREPORT(model);
            }
            //结转下月
            string JZXYsql = "Select Count(Tt.Sourcename) FRACTION ,Tt.Sourceid GGFWSID From ( Select Ggfwsources.Sourcename,Ggfwsources.Sourceid From Ggfwevents Inner Join Ggfwsources On Ggfwevents.Eventsource=Ggfwsources.Sourceid Where Ggfwevents.Createtime >=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' )And Ggfwevents.Createtime<=To_Date('" + lastdate + "','yyyy-mm-dd hh24:mi:ss') And Statue<=4 And to_char(Overtime,'mm')>To_Char(Sysdate, 'dy')  ) tt group by tt.Sourcename,tt.Sourceid";
            List<GGFWReport> JZXYLlist = db.Database.SqlQuery<GGFWReport>(JZXYsql).ToList();
            foreach (var item in JZXYLlist)
            {
                model.CID = 4;
                model.FRACTION = item.FRACTION.ToString();
                model.GGFWSID = item.GGFWSID;
                model.CREATETIME = firstdate;
                new GGFWMONTHLYREPORTSBLL().AddGGFWMONTHLYREPORT(model);
            }
            //未按时办理
            string WASBLsql = " select Count(1) FRACTION ,Tt.Sourceid GGFWSID from (Select *  From Zfsjworkflowinstances Inner Join Zfsjactivityinstances On Zfsjworkflowinstances.Currentaiid=Zfsjactivityinstances.Aiid Inner Join Ggfwevents On Ggfwevents.Wiid = Zfsjworkflowinstances.Wiid inner join Ggfwsources on Ggfwevents.Eventsource=Ggfwsources.Sourceid Where Zfsjworkflowinstances.Eventsourceid Is Not Null And Zfsjworkflowinstances.Eventsourceid<>16  And Zfsjworkflowinstances.Eventsourcepkid Is Not Null And Zfsjworkflowinstances.Statusid=1 And Zfsjactivityinstances.Adid <=4 And Zfsjactivityinstances.Createtime>Zfsjactivityinstances.Sjtimelimit And Ggfwevents.Createtime >=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' )And Ggfwevents.Createtime<=To_Date('" + lastdate + "','yyyy-mm-dd hh24:mi:ss')) Tt group by Tt.Sourceid";
            List<GGFWReport> WASBLlist = db.Database.SqlQuery<GGFWReport>(WASBLsql).ToList();
            foreach (var item in WASBLlist)
            {
                model.CID = 5;
                model.FRACTION = item.FRACTION.ToString();
                model.GGFWSID = item.GGFWSID;
                model.CREATETIME = firstdate;
                new GGFWMONTHLYREPORTSBLL().AddGGFWMONTHLYREPORT(model);
            }

        }

        /// <summary>
        /// 读取公共服务月报数据
        /// </summary>
        public static IList<GGFWReport> GetGGFWRChart(DateTime firstdate)
        {
            PLEEntities db = new PLEEntities();
            DateTime now = DateTime.Now;
            //DateTime firstdate = new DateTime(now.Year, now.Month, 1);
            //DateTime lastdate = now.AddMonths(1).AddDays(-1);
            string sql = "Select Ggfws.Sourcename Sname,Tall1.Fraction Syzj,Tall2.Fraction Bysl ,Tall3.Fraction Bybj,Tall4.Fraction Jzxy,Tall5.Fraction Wasbl,Tall6.Fraction Cbsl,Tall7.Fraction Dbsl,Tall8.Fraction Srhw,Tall9.Fraction Cxgh,Tall10.Fraction Cslh,Tall11.Fraction Szgy,Tall12.Fraction Hjbh,Tall13.Fraction Gsxz,Tall14.Fraction GAJT,Tall15.Fraction CSHD,Tall16.Fraction WJJB,Tall17.Fraction QT From Ggfwsources Ggfws left Join Ggfwmonthlyreports Tall1  On Ggfws.Sourceid=Tall1.Ggfwsid And Tall1.Ggfwsid=Ggfws.Sourceid And Tall1.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall1.Cid=1 left Join Ggfwmonthlyreports Tall2  On Ggfws.Sourceid=Tall2.Ggfwsid And Tall2.Ggfwsid=Ggfws.Sourceid  And Tall2.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall2.Cid=2 left Join Ggfwmonthlyreports Tall3  On Ggfws.Sourceid=Tall3.Ggfwsid  And  Tall3.Ggfwsid=Ggfws.Sourceid   And Tall3.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) And Tall3.Cid=3 left Join Ggfwmonthlyreports Tall4   On Ggfws.Sourceid=Tall4.Ggfwsid  And  Tall4.Ggfwsid=Ggfws.Sourceid  And Tall4.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall4.Cid=4  left Join Ggfwmonthlyreports Tall5  On Ggfws.Sourceid=Tall5.Ggfwsid  And  Tall5.Ggfwsid=Ggfws.Sourceid  And Tall5.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall5.Cid=5 left Join Ggfwmonthlyreports Tall6   On Ggfws.Sourceid=Tall6.Ggfwsid  And  Tall6.Ggfwsid=Ggfws.Sourceid  And Tall6.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall6.Cid=6 left Join Ggfwmonthlyreports Tall7  On Ggfws.Sourceid=Tall7.Ggfwsid  And  Tall7.Ggfwsid=Ggfws.Sourceid  And Tall7.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall7.Cid=7 left Join Ggfwmonthlyreports Tall8  On Ggfws.Sourceid=Tall8.Ggfwsid  And  Tall8.Ggfwsid=Ggfws.Sourceid  And Tall8.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall8.Cid=27311 left Join Ggfwmonthlyreports Tall9  On Ggfws.Sourceid=Tall9.Ggfwsid  And  Tall8.Ggfwsid=Ggfws.Sourceid  And Tall9.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) And Tall9.Cid=27312  Left Join Ggfwmonthlyreports Tall10  On Ggfws.Sourceid=Tall10.Ggfwsid  And  Tall10.Ggfwsid=Ggfws.Sourceid  And Tall10.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall10.Cid=27313 Left Join Ggfwmonthlyreports Tall11  On Ggfws.Sourceid=Tall11.Ggfwsid  And  Tall11.Ggfwsid=Ggfws.Sourceid  And Tall11.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall11.Cid=27314 Left Join Ggfwmonthlyreports Tall12   On Ggfws.Sourceid=Tall12.Ggfwsid  And  Tall12.Ggfwsid=Ggfws.Sourceid  And Tall12.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall12.Cid=27315 Left Join Ggfwmonthlyreports Tall13   On Ggfws.Sourceid=Tall13.Ggfwsid  And  Tall13.Ggfwsid=Ggfws.Sourceid  And Tall13.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall13.Cid=27316 Left Join Ggfwmonthlyreports Tall14  On Ggfws.Sourceid=Tall14.Ggfwsid  And  Tall14.Ggfwsid=Ggfws.Sourceid  And Tall14.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall14.Cid=27317 Left Join Ggfwmonthlyreports Tall15  On Ggfws.Sourceid=Tall15.Ggfwsid  And  Tall15.Ggfwsid=Ggfws.Sourceid  And Tall15.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall15.Cid=27318 Left Join Ggfwmonthlyreports Tall16  On Ggfws.Sourceid=Tall16.Ggfwsid  And  Tall16.Ggfwsid=Ggfws.Sourceid  And Tall16.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall16.Cid=27355 Left Join Ggfwmonthlyreports Tall17  On Ggfws.Sourceid=Tall17.Ggfwsid  And  Tall17.Ggfwsid=Ggfws.Sourceid  And Tall17.Createtime=To_Date('" + firstdate + "','yyyy-mm-dd hh24:mi:ss' ) and Tall17.Cid=27354 order by ggfws.SOURCEID asc";
            List<GGFWReport> list = db.Database.SqlQuery<GGFWReport>(sql).ToList();
            return list;
        }


    }
}
