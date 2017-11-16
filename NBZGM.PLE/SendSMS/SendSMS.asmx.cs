using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace SendSMS
{
    /// <summary>
    /// SendSMS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SendSMS : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SendMessages(string SRC_TELE_NUM, string DEST_TELE_NUM, string MSG)
        {
            bool flag = true;
            //连接数据库字符串
            string connectionString = "Data Source=211.140.178.217/tzdb19;User Id=TZ066156;Password=TZ066156;";
            //创建一个新的连接
            OracleConnection conn = new OracleConnection(connectionString);

            try
            {
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    @"insert into sm(user_name,SRC_TELE_NUM,DEST_TELE_NUM,MSG,
sm_seq_id,SET_SEND_TIME) values('joke','" + SRC_TELE_NUM + "','" +
DEST_TELE_NUM + "','" + MSG + "',SM_SEQ.nextval,sysdate)";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                flag = false;
                DateTime errorDateTime = DateTime.Now;
                string errorPath = @"C:\SendSMSErrorLogs\";

                if(!Directory.Exists(errorPath))
                {
                    Directory.CreateDirectory(errorPath);
                    StreamWriter errorSW = new StreamWriter(errorPath+errorDateTime.ToString("yyyy-MM-dd")+".txt",true);
                    errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
                    errorSW.WriteLine("ExceptionMessage:" + e.Message);
                    errorSW.WriteLine("ExceptionSource:" + e.Source);

                    if (e.InnerException != null)
                    {
                        errorSW.WriteLine("InnerExceptionMessage:" + e.InnerException.Message);
                        errorSW.WriteLine("InnerExceptionSource:" + e.InnerException.Source);

                    }

                    errorSW.Close();
                }
            }
            finally
            {
                conn.Close();
            }

            return flag;
        }
    }
}
