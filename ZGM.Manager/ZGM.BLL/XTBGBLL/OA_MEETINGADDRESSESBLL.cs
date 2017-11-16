using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.XTBGBLL
{
   public class OA_MEETINGADDRESSESBLL
    {

       /// <summary>
       /// 添加会议地址
       /// </summary>
       /// <param name="model"></param>
       public static void AddMEETINGADDRESS(OA_MEETINGADDRESSES model) {
           Entities db = new Entities();
           if (model != null)
           {
               db.OA_MEETINGADDRESSES.Add(model);
           }
           db.SaveChanges();
       }
       /// <summary>
       /// 获取所有会议地址
       /// </summary>
       /// <returns></returns>
       public static IEnumerable<OA_MEETINGADDRESSES> GetAllMeetingAddressList()
       {
           Entities db = new Entities();
           return db.OA_MEETINGADDRESSES.OrderBy(a=>a.SEQ);
       }

       /// <summary>
       /// 删除会议地址
       /// </summary>
       /// <param name="date"></param>
       /// <param name="userid"></param>
       public static void DeleteMeetingAddress(string id)
       {
           decimal Ids = 0;
           decimal.TryParse(id, out Ids);
           Entities db = new Entities();
           IQueryable<OA_MEETINGADDRESSES> ma = db.OA_MEETINGADDRESSES.Where(t => t.ADDRESSID == Ids);
           foreach (OA_MEETINGADDRESSES item in ma)
           {
               db.OA_MEETINGADDRESSES.Remove(item);
           }
           db.SaveChanges();
       }

       /// <summary>
       /// 根据会议地址ID获取会议地址详情
       /// </summary>
       public static OA_MEETINGADDRESSES GetMeetingAddressDetail(decimal ADDRESSID)
       {
           Entities db = new Entities();
           OA_MEETINGADDRESSES model = db.OA_MEETINGADDRESSES.SingleOrDefault(t => t.ADDRESSID == ADDRESSID);
           return model;
       }


       /// <summary>
       /// 修改会议地址
       /// </summary>
       /// <param name="model"></param>
       public static void ModifyMeetingAddress(OA_MEETINGADDRESSES model)
       {
           Entities db = new Entities();
           OA_MEETINGADDRESSES models = db.OA_MEETINGADDRESSES.FirstOrDefault(t => t.ADDRESSID == model.ADDRESSID);
           if (models != null)
           {
               models.ADDRESSNAME = model.ADDRESSNAME;
               models.SEQ = model.SEQ;
           }
           db.SaveChanges();

       }
    }
}
