using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Model;
using System.Data.SqlClient;
namespace Web.Controllers
{
    public class TZZFGK
    {
        public static PLEEntities db = new PLEEntities();
        public static void getuser()
        {
            SqlConnection sqlcon = new SqlConnection("server=218.108.93.246;password=Password@1;uid=sa;database=TZ_ZFDC");
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            Guid UserRoleRelationguid = Guid.NewGuid();
            List<USER> users = db.USERS.Where(t => t.UNITID == 350).ToList();
            sqlcon.Open();
            foreach (var item in users)
            {
                if (item.ACCOUNT.Substring(0, 1).ToString() == "T")
                {
                    Guid UserGuid = Guid.NewGuid();
                    string sqltext = "Insert into USERS values ('" + UserGuid.ToString() + "','B21124F1-8A3B-4AC8-9565-FF09AC2797B5','','" + item.ACCOUNT + "','" + item.PASSWORD + "','" + item.USERNAME + "',1,null,null,510,0,1,'')";
                    sqlcom.CommandText = sqltext;
                    int i = sqlcom.ExecuteNonQuery();
                    string sqltext1 = "Insert into UserRoleRelations values ('" + UserGuid.ToString() + "',21)";
                    sqlcom.CommandText = sqltext1;
                    int b = sqlcom.ExecuteNonQuery();
                }
            }
        }
    }
    public class userzfgk
    {
        public string userid { get; set; }
    }
}