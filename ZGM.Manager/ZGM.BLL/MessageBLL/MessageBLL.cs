/*类名：MessageBLL
 *功能：消息模块的信息列表(查询)
 *创建时间:2016-5-10 20:40:15
 *版本：VS 1.1.0
 *作者:方勇
 *完成时间:2016-5-10 20:40:23
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.PhoneModel;



namespace ZGM.BLL.MessageBLL
{
    #region //获取消息列表
    /// <summary>
    /// 获取消息列表
    /// </summary>
    public class MessageBLL
    {
//        /// <summary>
//        /// 获取所有给当前用户发送消息的列表
//        /// </summary>
//        /// <returns></returns>
//        public static List<MessageModel> GetAllMessageListBySendPerson(MessageModel mmodel)
//        {
//            Entities db = new Entities();
//            string sqllist = @"select m.*,nvl(c.mcounts,'0')mcounts,us.username as sendmessagename,us.avatar as suserimg from 
//                            (select k.*,row_number() over(partition by k.senderid order by sendtime desc) as new_index2 from 
//                             (select * from 
//                            (select b.* ,row_number() over(partition by b.senderid
//                            order by b.sendtime desc) as new_index 
//                            from ms_messages b where b.receiverid=" + mmodel.RECEIVERID + @") c where c.new_index=1 
//                            union all
//                            select * from 
//                            (select a.msid,a.receiverid,a.senderid,a.content,a.sendtime,a.receivetime,a.resourceurl,a.remark,a.isreader,a.smkind,a.sourceid ,row_number() over(partition by a.receiverid
//                            order by a.receivetime ) as new_index 
//                            from ms_messages a where a.senderid=" + mmodel.RECEIVERID + @") d where d.new_index=1)k)m 
//                            join sys_users us on m.senderid=us.userid
//                            join sys_users uss on m.receiverid=uss.userid
//                            left join 
//                            (select distinct(senderid) as s1,count(*) as mcounts from ms_messages where receiverid=" + mmodel.RECEIVERID + @" and isreader=0 group by senderid) c
//                            on m.senderid =c.s1 
//                            where m.new_index2=1
//                            order by m.sendtime desc";
//            List<MessageModel> list = db.Database.SqlQuery<MessageModel>(sqllist).ToList();
//            if (!string.IsNullOrEmpty(mmodel.SendMessageName))
//                list = list.Where(t => t.SendMessageName.IndexOf(mmodel.SendMessageName) != -1).ToList();
//            list = list.Skip(mmodel.Page * 10).Take(10).ToList();
//            return list;
//        }



        /// <summary>
        /// 获取所有给当前用户发送消息的列表
        /// </summary>
        /// <returns></returns>
        public static List<MessageModel> GetAllMessageListBySendPerson(MessageModel mmodel)
        {
            Entities db = new Entities();
            //string sqllist = "select * from (select b.* ,row_number() over(partition by b.senderid                      order by b.sendtime desc) as new_index from ms_messages b where (b.receiverid=137 or b.senderid=" + mmodel.RECEIVERID + ")) c where c.new_index=1  order by sendtime desc";
            string sqllist = "select m.*,nvl(c.mcounts,'0')mcounts,us.username as sendmessagename,us.avatar as suserimg from (select k.*,row_number() over(partition by k.senderid order by sendtime desc) as new_index2 from (select * from (select b.* ,row_number() over(partition by b.senderid order by b.sendtime desc) as new_index from ms_messages b where (b.receiverid=" + mmodel.RECEIVERID + ") or (b.senderid=" + mmodel.RECEIVERID + ")) c where c.new_index=1  union all select * from (select a.msid,a.receiverid,a.senderid,a.content,a.sendtime,a.receivetime,a.resourceurl,a.remark,a.isreader,a.smkind,a.sourceid ,row_number() over(partition by a.receiverid order by a.receivetime ) as new_index from ms_messages a where a.senderid=" + mmodel.RECEIVERID + ") d where d.new_index=1)k)m join sys_users us on m.senderid=us.userid join sys_users uss on m.receiverid=uss.userid left join (select distinct(senderid) as s1,count(*) as mcounts from ms_messages where receiverid=" + mmodel.RECEIVERID + " and isreader=0 group by senderid) c on m.senderid =c.s1 where m.new_index2=1 order by m.sendtime desc";

            List<MessageModel> list = db.Database.SqlQuery<MessageModel>(sqllist).ToList();
            //foreach (var item in list)
            //{
            //    if (item.SENDERID== mmodel.RECEIVERID)
            //    {
            //        var itemreceiverid = item.RECEIVERID;//70
            //        foreach (var itemb in list)
            //        {
            //            if (itemb.RECEIVERID == mmodel.RECEIVERID && itemb.SENDERID == itemreceiverid)
            //            {
            //                list.Remove(itemb);
            //            }
            //        }
            //    }
            //}
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SENDERID == mmodel.RECEIVERID)
                {
                    var itemreceiverid = list[i].RECEIVERID;//70
                    for (int b = i + 1; b < list.Count; b++)
                    {
                        if (list[b].RECEIVERID == mmodel.RECEIVERID && list[b].SENDERID == itemreceiverid)
                        {
                            list[b].CONTENT = list[i].CONTENT;
                            list.Remove(list[i]);
                        }
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].RECEIVERID == mmodel.RECEIVERID)
                {
                    var itemreceiverid = list[i].SENDERID;//70
                    for (int b = i + 1; b < list.Count; b++)
                    {
                        if (list[b].SENDERID == mmodel.RECEIVERID && list[b].RECEIVERID == itemreceiverid)
                        {
                            list.Remove(list[b]);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(mmodel.SendMessageName))
                list = list.Where(t => t.SendMessageName.IndexOf(mmodel.SendMessageName) != -1).ToList();
            list = list.Skip(mmodel.Page * 10).Take(10).ToList();
            return list;
        }











        /// <summary>
        /// 获取当前两个人的对话信息
        /// </summary>
        /// <param name="mmodel"></param>
        /// <returns></returns>
        public static List<MessageModel> GetAllMessageListByRUIDAndSUID(MessageModel mmodel)
        {
            Entities db = new Entities();
            string sqllist = @"select 
            tab.msid,
            tab.senderid,
            us.username as sendername,
            us.avatar as suserimg,
            tab.receiverid,
            uss.username as receivename,
            uss.avatar as ruserimg,
            tab.content,
            tab.sendtime,
            tab.receivetime,
            tab.resourceurl,
            tab.remark,
            tab.isreader,
            tab.smkind,
            tab.sourceid
            from  ms_messages  tab
            join SYS_USERS us on tab.senderid=us.userid
            join SYS_USERS uss on tab.RECEIVERID=uss.userid        
            where (receiverid=" + mmodel.RECEIVERID + "or receiverid=" + mmodel.SENDERID + ")  and (senderid=" + mmodel.RECEIVERID + " or senderid=" + mmodel.SENDERID + ") order by tab.sendtime";
            List<MessageModel> list = db.Database.SqlQuery<MessageModel>(sqllist).ToList();
            //list = list.Skip(mmodel.Page * 10).Take(10).ToList();
            return list;
        }

        /// <summary>
        /// 获取当前两个人的对话信息
        /// </summary>
        /// <param name="mmodel"></param>
        /// <returns></returns>
        public static List<MessageModel> GetCHAJUAllMessageByRUIDAndSUID(MessageModel mmodel)
        {
            Entities db = new Entities();
            string sqllist = @"select 
            tab.msid,
            tab.senderid,
            us.username as sendername,
            us.avatar as suserimg,
            tab.receiverid,
            uss.username as receivename,
            uss.avatar as ruserimg,
            tab.content,
            tab.sendtime,
            tab.receivetime,
            tab.resourceurl,
            tab.remark,
            tab.isreader,
            tab.smkind,
            tab.sourceid
            from  ms_messages  tab
            join SYS_USERS us on tab.senderid=us.userid
            join SYS_USERS uss on tab.RECEIVERID=uss.userid        
            where (receiverid=" + mmodel.RECEIVERID + "or receiverid=" + mmodel.SENDERID + ")  and (senderid=" + mmodel.RECEIVERID + " or senderid=" + mmodel.SENDERID + ") ";
            if (mmodel.SENDTIME!=null)
            {
                sqllist += "and SENDTIME>  to_date('" + mmodel.SENDTIME + "','yyyy-mm-dd hh24:mi:ss')";
            }
            sqllist+="order by tab.sendtime";
            List<MessageModel> list = db.Database.SqlQuery<MessageModel>(sqllist).ToList();
            //list = list.Skip(mmodel.Page * 10).Take(10).ToList();
            return list;
        }


        /// <summary>
        /// 向消息列表添加信息
        /// </summary>
        /// <param name="mmodel"></param>
        /// <returns></returns>
        public static int AddMessagesBySenderID(MS_MESSAGES mmodel)
        {
            Entities db = new Entities();
            mmodel.MSID =decimal.Parse(GetNewId());
            db.MS_MESSAGES.Add(mmodel);
            db.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 获取当前用户的未读信息
        /// </summary>
        /// <param name="mmodel"></param>
        /// <returns></returns>
        public static int GetIsReaderMessageCounts(MS_MESSAGES mmodel)
        {
            Entities db = new Entities();
            var listcounts = (from aa in db.MS_MESSAGES
                     where aa.RECEIVERID == mmodel.RECEIVERID && aa.ISREADER == 0
                     select aa).Count();
            return listcounts;
        }

        /// <summary>
        /// 根据接受人和发送人更新消息状态
        /// </summary>
        /// <param name="mmodel"></param>
        /// <returns></returns>
        public static int UpdateIsReaderMessageCounts(MS_MESSAGES mmodel)
        {
            Entities db = new Entities();
            string sqllist = "update ms_messages set isreader=1 where receiverid=" + mmodel.RECEIVERID + " and senderid=" + mmodel.SENDERID + "";
            List<MessageModel> listcounts = db.Database.SqlQuery<MessageModel>(sqllist).ToList();
            return  listcounts.Count();

        }


        /// <summary>
        /// 获取的编号
        /// </summary>
        private static  string GetNewId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(10000, 99999);
        }
    }
    #endregion
}
