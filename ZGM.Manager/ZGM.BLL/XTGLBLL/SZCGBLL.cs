using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using ZGM.Model.PhoneModel;

namespace ZGM.BLL.XTGLBLL
{
    public class SZCGBLL
    {
        /// <summary>
        /// 更新数字城管案件数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateSZCG(SZZTModel model)
        {

            try
            {
                DAL.ZHCGEntities db = new DAL.ZHCGEntities();

                db.FI_ZHCGDISPOSALS.Add(new DAL.FI_ZHCGDISPOSALS
                {
                    TASKNUM = model.TaskNum,
                    TYPE = "3",
                    INFO = model.Content,
                    MEMO = model.Content,
                    //UNITID = "318",
                    CREATETIME = model.DealTime
                });
                db.SaveChanges();
                foreach (ZHCGMedia media in model.MediaList)
                {
                    db.FI_ZHCGMEDIAS.Add(new DAL.FI_ZHCGMEDIAS
                    {
                        TASKNUM = media.TASKNUM,
                        MEDIATYPE = media.MEDIATYPE,
                        MEDIANUM = media.MEDIANUM,
                        MEDIAORDER = media.MEDIAORDER,
                        NAME = media.NAME,
                        URL = media.URL,
                        CREATETIME = media.CREATETIME,
                       // IMGCODE = media.IMGCODE,
                        ISUSED = media.ISUSED
                    });
                    db.SaveChanges();
                }

                List<DAL.FI_ZHCGTASKS> listTask = db.FI_ZHCGTASKS.Where(t => t.TASKNUM == model.TaskNum && t.STATE != "3").ToList();
                foreach (DAL.FI_ZHCGTASKS task in listTask)
                {
                    DAL.FI_ZHCGTASKS taskd = db.FI_ZHCGTASKS.FirstOrDefault(a => a.TASKID == task.TASKID);
                    if (taskd != null)
                    {
                        taskd.STATE = "8";
                        taskd.DISPOSEDATE = model.DealTime;
                        taskd.DISPOSEMEMO = model.Content;
                        taskd.DISPOSEID = model.UserId;
                        taskd.DISPOSENAME = model.UserName;
                       // taskd.DEALUNIT = "318";
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                FileStream fs = new FileStream("D:\\U.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(e.Message);
                sw.Close();
                fs.Close();
                return false;
            }


            //List<Dictionary<string, OracleParameter[]>> list = new List<Dictionary<string, OracleParameter[]>>();
            //Dictionary<string, OracleParameter[]> dic = new Dictionary<string, OracleParameter[]>();

            //            string sql = string.Format(@"update FI_ZHCGTASKS set STATE=3,DISPOSEDATE=:DISPOSEDATE,DISPOSEMEMO=:DISPOSEMEMO,DISPOSEID=:DISPOSEID
            //            ,DISPOSENAME=:DISPOSENAME,DEALUNIT=:DEALUNIT where TASKNUM=:TASKNUM and STATE<>3"
            //                , model.DealTime, model.Content, model.UserId, model.UserName, model.DeptId, model.TaskNum);
            //            OracleParameter[] parameterValue1 = {
            //            new OracleParameter(":DISPOSEDATE",OracleType.DateTime),
            //            new OracleParameter(":DISPOSEMEMO",OracleType.VarChar),
            //            new OracleParameter(":DISPOSEID",OracleType.VarChar),
            //            new OracleParameter(":DISPOSENAME",OracleType.VarChar),
            //            new OracleParameter(":DEALUNIT",OracleType.VarChar),
            //            new OracleParameter(":TASKNUM",OracleType.VarChar),
            //              };
            //            parameterValue1[0].Value = model.DealTime;
            //            parameterValue1[1].Value = '"' + model.Content + '"';
            //            parameterValue1[2].Value = '"' + model.UserId + '"';
            //            parameterValue1[3].Value = '"' + model.UserName + '"';
            //            parameterValue1[4].Value = '"' + model.DeptId + '"';
            //            parameterValue1[5].Value = '"' + model.TaskNum + '"';
            //            dic.Add(sql, parameterValue1);
            //            list.Add(dic);


            //            dic = new Dictionary<string, OracleParameter[]>();

            //            string sqlInsert = string.Format(@"insert into FI_ZHCGDISPOSALS(TASKNUM,TYPE,INFO,MEMO,UNITID,CREATETIME) 
            //values(:TASKNUM,:TYPE,:INFO,:MEMO,:UNITID,:CREATETIME)");
            //            OracleParameter[] parameterValue = {
            //            new OracleParameter(":TASKNUM",OracleType.VarChar),
            //            new OracleParameter(":TYPE",OracleType.VarChar),
            //            new OracleParameter(":INFO",OracleType.VarChar),
            //            new OracleParameter(":MEMO",OracleType.VarChar),
            //            new OracleParameter(":UNITID",OracleType.VarChar),
            //            new OracleParameter(":CREATETIME",OracleType.DateTime),
            //                                                               };
            //            parameterValue[0].Value = '"' + model.TaskNum + '"';
            //            parameterValue[1].Value = '3';
            //            parameterValue[2].Value = '"' + model.Content + '"';
            //            parameterValue[3].Value = '"' + model.Content + '"';
            //            parameterValue[4].Value = '"' + model.DeptId + '"';
            //            parameterValue[5].Value = model.DealTime;
            //            dic.Add(sqlInsert, parameterValue);
            //            list.Add(dic);

            //            foreach (ZHCGMedia item in model.MediaList)
            //            {
            //                dic = new Dictionary<string, OracleParameter[]>();

            //                string sqlMedia = string.Format(@"insert into FI_ZHCGMEDIAS(TASKNUM,MEDIATYPE,MEDIANUM,MEDIAORDER,NAME,URL,CREATETIME,IMGCODE,ISUSED) 
            //            values(:TASKNUM,:MEDIATYPE,:MEDIANUM,:MEDIAORDER,:NAME,:URL,:CREATETIME,:IMGCODE,:ISUSED)");
            //                OracleParameter[] parameterValue2 = {
            //            new OracleParameter(":TASKNUM",OracleType.VarChar),
            //            new OracleParameter(":MEDIATYPE",OracleType.VarChar),
            //            new OracleParameter(":MEDIANUM",OracleType.Number),
            //            new OracleParameter(":MEDIAORDER",OracleType.Number),
            //            new OracleParameter(":NAME",OracleType.VarChar),
            //            new OracleParameter(":URL",OracleType.VarChar),
            //            new OracleParameter(":CREATETIME",OracleType.DateTime),
            //            new OracleParameter(":IMGCODE",OracleType.Clob),
            //            new OracleParameter(":ISUSED",OracleType.VarChar),
            //            };
            //                parameterValue2[0].Value = '"' + item.TASKNUM + '"';
            //                parameterValue2[1].Value = '"' + item.MEDIATYPE + '"';
            //                parameterValue2[2].Value = item.MEDIANUM;
            //                parameterValue2[3].Value = item.MEDIAORDER;
            //                parameterValue2[4].Value = '"' + item.NAME + '"';
            //                parameterValue2[5].Value = '"' + item.URL + '"';
            //                parameterValue2[6].Value = item.CREATETIME;
            //                parameterValue2[7].Value = item.IMGCODE;
            //                parameterValue2[8].Value = '"' + item.ISUSED + '"';
            //                dic.Add(sqlMedia, parameterValue2);
            //                list.Add(dic);
            //            }

            //  bool flag = OracleDBHelper.ExecuteSqlTran(list);

        }
    }
}
