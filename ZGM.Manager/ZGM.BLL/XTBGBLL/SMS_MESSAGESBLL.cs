using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.XTBGModels;

namespace ZGM.BLL.XTBGBLL
{
    public class SMS_MESSAGESBLL
    {
        /// <summary>
        /// 将消息添加到数据库
        /// </summary>
        /// <param name="model"></param>
        public static void AddMessages(SMS_MESSAGES model)
        {
            Entities db = new Entities();
            if (model != null)
            {
                db.SMS_MESSAGES.Add(model);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取所有消息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SMS_MESSAGES> GetAllMessagesList()
        {
            Entities db = new Entities();
            return db.SMS_MESSAGES;
        }

        /// <summary>
        /// 获取所有消息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IQueryable<SMSMessageModel> GetMessagesList()
        {
            Entities db = new Entities();
            IQueryable<SMSMessageModel> list = (from sm in db.SMS_MESSAGES
                                                from su in db.SYS_USERS
                                                where sm.SENDUSERID == su.USERID
                                                select new SMSMessageModel
                                                {
                                                    SMSID = sm.SMSID,
                                                    SMSTYPE = sm.SMSTYPE,
                                                    RECEIVEUSERS = sm.RECEIVEUSERS,
                                                    SENDUSERID = sm.SENDUSERID,
                                                    CONTENT = sm.CONTENT,
                                                    SENDTIME = sm.SENDTIME,
                                                    UserName = su.USERNAME,
                                                    RECEIVEUSERSNAME = sm.RECEIVEUSERSNAME,
                                                    REMARK=sm.REMARK,
                                                    PHONES = sm.PHONES,
                                                    ISAUDIT = sm.ISAUDIT,
                                                    SENDIDENTITY = sm.SENDIDENTITY,
                                                    AUDITUSER = sm.AUDITUSER,
                                                    AUDITTIME = sm.AUDITTIME,
                                                    SOURCE=sm.SOURCE
                                                }).OrderByDescending(a => a.SENDTIME);
            return list;
        }

        /// <summary>
        /// 根据ID获取详情
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static SMSMessageModel GetMessages(decimal SMSID)
        {

            SMSMessageModel models = GetMessagesList().SingleOrDefault(a => a.SMSID == SMSID);
           
            return models;
        }

        /// <summary>
        /// 根据MESSAGEID获取详情
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static SMS_MESSAGES GetMessagesModel(string MESSAGEID)
        {
            Entities db = new Entities();
            SMS_MESSAGES models = db.SMS_MESSAGES.FirstOrDefault(a => a.MESSAGEID == MESSAGEID);

            return models;
        }
        /// <summary>
        /// 获取当前登陆人的所有信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IQueryable<SMSMessageModel> GetUserMessagesList(decimal userid)
        {

            return GetMessagesList().Where(t => t.SENDUSERID == userid);
        }
        /// <summary>
        /// 获取当前登陆人的所有信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IQueryable<SMSMessageModel> GetDSHUserMessagesList(int ISAUDIT)
        {

            return GetMessagesList().Where(t => t.ISAUDIT == ISAUDIT);
        }

        /// <summary>
        /// 获取当前登陆人的所有信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IQueryable<SMSMessageModel> GetYSHUserMessagesList(int ISAUDIT,decimal user)
        {

            return GetMessagesList().Where(t => t.ISAUDIT == ISAUDIT &&t.AUDITUSER==user);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static void modifyMessages(SMS_MESSAGES model)
        {
            Entities db = new Entities();
            SMS_MESSAGES models = db.SMS_MESSAGES.SingleOrDefault(t => t.SMSID == model.SMSID);
            models.AUDITUSER = model.AUDITUSER;
            models.CONTENT = model.CONTENT;
            models.AUDITTIME = DateTime.Now;
            models.ISAUDIT = 1;
            db.SaveChanges();
        }

        /// <summary>
        /// 回馈信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static void AlterMessages(SMS_MESSAGES model)
        {
            Entities db = new Entities();
            SMS_MESSAGES models = db.SMS_MESSAGES.SingleOrDefault(t => t.MESSAGEID == model.MESSAGEID);
            models.REMARK = model.REMARK;
            db.SaveChanges();
        }
    }
}
