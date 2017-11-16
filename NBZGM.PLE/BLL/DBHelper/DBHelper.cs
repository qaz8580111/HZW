using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using Taizhou.PLE.Model.GSGGModels;
using Taizhou.PLE.BLL;
using Taizhou.PLE.Model;
using System.Data.OleDb;
using Taizhou.PLE.BLL.UserBLLs;
using System.Text.RegularExpressions;

namespace Taizhou.PLE.BLL.DBHelper
{
    public class DBHelper
    {
        /// <summary>
        /// 建立mysql数据库链接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection getMySqlCon()
        {
            String mysqlStr = "Database=TD_OA;Data Source=10.1.1.11;User Id=root1;Password=root1;pooling=false;CharSet=utf8;port=3336";
            MySqlConnection mysql = new MySqlConnection(mysqlStr);
            if (mysql.State == System.Data.ConnectionState.Open)
            {
                mysql.Close();
                mysql.Open();
            }
            else if (mysql.State == System.Data.ConnectionState.Closed)
            {
                mysql.Open();
            }
            else if (mysql.State == System.Data.ConnectionState.Broken)
            {
                mysql.Open();
            }
            return mysql;
        }
        /// <summary>
        /// 建立执行命令语句对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mysql"></param>
        /// <returns></returns>
        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
            return mySqlCommand;
        }
        /// <summary>
        /// 查询并获得结果集
        /// </summary>
        /// <param name="mySqlCommand"></param>
        public static List<GSGGPendModels> GetGSGGList()
        {
            MySqlConnection mysql = getMySqlCon();
            List<GSGGPendModels> GGList = new List<GSGGPendModels>();
            String sqlSearch = "select * from user u join notify n on u.user_id = n.From_id limit 10";
            MySqlCommand mscom = new MySqlCommand(sqlSearch, getMySqlCon());
            MySqlDataReader ds = mscom.ExecuteReader();
            while (ds.Read())
            {
                GSGGPendModels gg = new GSGGPendModels();
                gg.NotifyID = ds["Notify_ID"].ToString();
                gg.SubJect = ds["SubJect"].ToString();
                gg.SendTime = DateTime.Parse(ds["Send_Time"].ToString());
                GGList.Add(gg);
            }
            ds.Close();
            return GGList;
        }
        /// <summary>
        /// 获得公示公告的详情
        /// </summary>
        public static GSGGPendModels ShowGSGGList(string NotifyID)
        {
            String sqlSearch = "select * from user u join notify n on u.user_id = n.From_id and  n.Notify_ID='" + NotifyID + "'";
            GSGGPendModels gg = new GSGGPendModels();
            MySqlConnection mysql = getMySqlCon();
            MySqlCommand mscom = new MySqlCommand(sqlSearch, getMySqlCon());
            MySqlDataReader ds = mscom.ExecuteReader();
            while (ds.Read())
            {
                gg.User_Name = ds["User_Name"].ToString();
                gg.NotifyID = ds["Notify_ID"].ToString();
                gg.SubJect = ds["SubJect"].ToString();
                gg.User_Priv_Name = ds["User_Priv_Name"].ToString();
                //string result = Regex.Replace( ds["Content"].ToString(), @"\s+", " ");
                //gg.Content = result.Replace("&nbsp;", "");
                string result = ds["Content"].ToString();
                gg.Content = result.Replace("\r\t", "<br/>");
                gg.SendTime = DateTime.Parse(ds["Send_Time"].ToString());
            }
            ds.Close();
            return gg;
        }

        //<summary>
        //获取远程数据库数据
        //</summary>
        public static void GETUSERID()
        {
            PLEEntities db = new PLEEntities();
            String sqlSearch = "select USER_ID,USER_NAME from user";
            MySqlConnection mysql = getMySqlCon();
            MySqlCommand mscom = new MySqlCommand(sqlSearch, getMySqlCon());
            MySqlDataReader ds = mscom.ExecuteReader();
            List<Taizhou.PLE.Model.CustomModels.UserInfo> UIF = new List<Model.CustomModels.UserInfo>();

            while (ds.Read())
            {
                Taizhou.PLE.Model.CustomModels.UserInfo ui = new Model.CustomModels.UserInfo();
                ui.RegionName = ds["USER_ID"].ToString();
                ui.UserName = ds["USER_NAME"].ToString();
                UIF.Add(ui);
            }
            ds.Close();
            List<USER> uin = db.USERS.ToList();
            int j = 0;
            int u;
            //for (int i = 0; i < UIF.Count; i++)
            //{

            //    USER uis = uin.FirstOrDefault(t => t.STATUSID == 1 && t.USERNAME == UIF[i].UserName);
            //    if (uis != null)
            //    {
            //        uis.ACCOUNT = UIF[i].RegionName;
            //    }


            //}
            for (int i = 0; i < UIF.Count; i++)
            {
                USER uist = uin.FirstOrDefault(t => t.STATUSID == 1 && t.ACCOUNT == UIF[i].RegionName);
                if (uist == null)
                {
                    USER uis = uin.FirstOrDefault(t => t.STATUSID == 1 && t.USERNAME == UIF[i].UserName);
                    if (uis != null)
                    {
                        uis.ACCOUNT = UIF[i].RegionName;
                    }
                    else
                    {
                        List<Taizhou.PLE.Model.CustomModels.UserInfo> ddy = new List<Model.CustomModels.UserInfo>();
                        Taizhou.PLE.Model.CustomModels.UserInfo dy = new Model.CustomModels.UserInfo();
                        dy.PositionName = UIF[i].UserName;
                        dy.RegionName = UIF[i].RegionName;
                        ddy.Add(dy);
                    }
                }
            }

            u = j;
            db.SaveChanges();
        }


        /// <summary>
        /// 导入excel
        /// </summary>
        /// <returns></returns>
        public static DataSet ExcelToDS()
        {
            string strSheetName = "sheet1";
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"C:\额问问\没有.xls" + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            //Sql语句      
            string strExcel = "select * from  [" + strSheetName + "$] ";
            DataSet ds = new DataSet();
            //连接数据源      
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();

            //适配到数据源      
            OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, conn);
            adapter.Fill(ds);
            return ds;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        public static void insert()
        {
            int j = 1088;
            DataSet ds = ExcelToDS();
            PLEEntities ple = new PLEEntities();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                j++;
                USER u = new USER();
                u.USERID = j;
                u.USERNAME = ds.Tables[0].Rows[i]["UserName"].ToString();
                if (ds.Tables[0].Rows[i]["UNITID"].ToString() != "")
                {
                    u.UNITID = decimal.Parse(ds.Tables[0].Rows[i]["UNITID"].ToString());
                }
                u.ACCOUNT = ds.Tables[0].Rows[i]["ACCOUNT"].ToString();
                u.PASSWORD = ds.Tables[0].Rows[i]["PASSWORD"].ToString();
                if (ds.Tables[0].Rows[i]["USERPOSITIONID"].ToString() != "")
                {
                    u.USERPOSITIONID = decimal.Parse(ds.Tables[0].Rows[i]["USERPOSITIONID"].ToString());
                }

                u.STATUSID = decimal.Parse(ds.Tables[0].Rows[i]["STATUSID"].ToString());
                if (ds.Tables[0].Rows[i]["SEQNO"].ToString() != "")
                {
                    u.SEQNO = decimal.Parse(ds.Tables[0].Rows[i]["SEQNO"].ToString());
                }
                if (ds.Tables[0].Rows[i]["RTXACCOUNT"].ToString() != "")
                {
                    u.RTXACCOUNT = ds.Tables[0].Rows[i]["RTXACCOUNT"].ToString();
                }
                if (ds.Tables[0].Rows[i]["USERCATEGORYID"].ToString() != "")
                {
                    u.USERCATEGORYID = decimal.Parse(ds.Tables[0].Rows[i]["USERCATEGORYID"].ToString());
                }
                if (ds.Tables[0].Rows[i]["CATEGORYID"].ToString() != "")
                {
                    u.CATEGORYID = decimal.Parse(ds.Tables[0].Rows[i]["CATEGORYID"].ToString());
                }
                if (ds.Tables[0].Rows[i]["REGIONID"].ToString() != "")
                {
                    u.REGIONID = decimal.Parse(ds.Tables[0].Rows[i]["REGIONID"].ToString());
                }
                if (ds.Tables[0].Rows[i]["ZFZBH"].ToString() != "")
                {
                    u.ZFZBH = ds.Tables[0].Rows[i]["ZFZBH"].ToString();
                }
                ple.USERS.Add(u);
            }
            ple.SaveChanges();

        }

        /// <summary>
        /// 执法事件大类表插入数据
        /// </summary>
        public static void insertZFSJ()
        {
            DataSet ds = ExcelToDS();
            PLEEntities db = new PLEEntities();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ZFSJQUESTIONCLASS zfsj = new ZFSJQUESTIONCLASS();
                zfsj.CLASSID = ds.Tables[0].Rows[i]["ILLEGALCLASSID"].ToString();
                zfsj.CLASSTYPEID = decimal.Parse(ds.Tables[0].Rows[i]["ILLEGALCLASSTYPEID"].ToString());
                if (ds.Tables[0].Rows[i]["PARENTID"].ToString() != "")
                {
                    zfsj.PARENTID = decimal.Parse(ds.Tables[0].Rows[i]["PARENTID"].ToString());
                }
                zfsj.CLASSNAME = ds.Tables[0].Rows[i]["ILLEGALCLASSNAME"].ToString();
                zfsj.CLASSCODE = decimal.Parse(ds.Tables[0].Rows[i]["ILLEGALCODE"].ToString());
                zfsj.PATH = ds.Tables[0].Rows[i]["PATH"].ToString();
                db.ZFSJQUESTIONCLASSES.Add(zfsj);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 根据登陆用户名获取该用户涉及到的所有公文
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <returns>该用户涉及到的所有公文</returns>
        public static List<GWDetail> GetGWInfoList(string userID, int type)
        {
            MySqlConnection conn = getMySqlCon();

            List<GWDetail> GWList = new List<GWDetail>();

            String sql =
@"SELECT * FROM TD_OA.FLOW_RUN WHERE RUN_ID IN 
(SELECT RUN_ID FROM TD_OA.FLOW_RUN_PRCS WHERE USER_ID='{0}' GROUP BY RUN_ID
) AND FLOW_ID = {1} ORDER BY BEGIN_TIME DESC";

            sql = string.Format(sql, userID, type);

            MySqlCommand com = new MySqlCommand(sql, conn);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                GWDetail detail = new GWDetail();

                detail.Run_ID = decimal.Parse(dr["RUN_ID"].ToString());
                detail.Flow_ID = decimal.Parse(dr["FLOW_ID"].ToString());
                detail.Run_Name = dr["RUN_NAME"].ToString();
                detail.Begin_Time = DateTime.Parse(dr["BEGIN_TIME"].ToString());

                GWList.Add(detail);
            }

            dr.Close();

            return GWList;
        }

        /// <summary>
        /// 根据流程编号获取该流程的详情
        /// </summary>
        /// <param name="run_ID">流程编号</param>
        /// <returns>该流程的详情</returns>
        public static GWDetail GetGWDetail(int run_ID, int flow_ID)
        {
            MySqlConnection conn = getMySqlCon();

            string sql = "";

            if (flow_ID == 102)
            {
                sql = "SELECT * FROM TD_OA.FLOW_DATA_102 WHERE RUN_ID = {0}";
            }
            else if (flow_ID == 103)
            {
                sql = "SELECT * FROM TD_OA.FLOW_DATA_103 WHERE RUN_ID = {0}";
            }

            sql = string.Format(sql, run_ID);

            MySqlCommand com = new MySqlCommand(sql, conn);
            MySqlDataReader dr = com.ExecuteReader();

            GWDetail detail = new GWDetail();

            while (dr.Read())
            {
                detail.Run_ID = decimal.Parse(dr["RUN_ID"].ToString());
                detail.Run_Name = dr["RUN_NAME"].ToString();
                detail.Begin_Time = DateTime.Parse(dr["BEGIN_TIME"].ToString());
                detail.Data_1 = dr["DATA_1"].ToString();
                detail.Data_3 = dr["DATA_3"].ToString();
                detail.Data_5 = dr["DATA_5"].ToString();
                detail.Data_28 = dr["DATA_28"].ToString();
                detail.Data_29 = dr["DATA_29"].ToString();
                detail.Data_27 = dr["DATA_27"].ToString();
                detail.Data_10 = dr["DATA_10"].ToString();
                detail.Data_15 = dr["DATA_15"].ToString();
                detail.Data_18 = dr["DATA_18"].ToString();
                detail.Data_19 = dr["DATA_19"].ToString();
                detail.Data_30 = dr["DATA_30"].ToString();
                detail.Data_31 = dr["DATA_31"].ToString();

                if (flow_ID == 102)
                {
                    detail.Data_4 = dr["DATA_4"].ToString();
                }
                else if (flow_ID == 103)
                {
                    detail.Data_32 = dr["DATA_32"].ToString();
                    detail.Data_33 = dr["DATA_33"].ToString();
                    detail.Data_34 = dr["DATA_34"].ToString();
                }
            }

            return detail;
        }

        /// <summary>
        /// 获取公文反馈意见列表
        /// </summary>
        /// <param name="run_ID">流程编号</param>
        /// <returns>公文反馈意见列表</returns>
        public static List<GWFeedBack> GetGWFeedBackList(int run_ID)
        {
            MySqlConnection conn = getMySqlCon();

            List<GWFeedBack> feedBackList = new List<GWFeedBack>();

            String sql =
@"SELECT FRF.EDIT_TIME,D.DEPT_NAME,U.USER_NAME,FRF.CONTENT,FRF.FLOW_PRCS
FROM TD_OA.FLOW_RUN_FEEDBACK FRF,TD_OA.USER U,TD_OA.DEPARTMENT D
WHERE FRF.RUN_ID = {0}
AND FRF.USER_ID = U.USER_ID
AND U.DEPT_ID = D.DEPT_ID
ORDER BY FRF.FLOW_PRCS";

            sql = string.Format(sql, run_ID);

            MySqlCommand com = new MySqlCommand(sql, conn);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                GWFeedBack feedBack = new GWFeedBack();

                feedBack.Edit_Time = DateTime.Parse(dr["EDIT_TIME"].ToString());
                feedBack.Dept_Name = dr["DEPT_NAME"].ToString();
                feedBack.User_Name = dr["USER_NAME"].ToString();
                feedBack.Content = dr["CONTENT"].ToString();
                feedBack.Flow_Prcs = decimal.Parse(dr["FLOW_PRCS"].ToString());

                feedBackList.Add(feedBack);
            }

            dr.Close();

            return feedBackList;
        }
    }
}
