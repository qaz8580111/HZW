using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class BMDDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 分页获取白名单列表
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<BMDModel> GetBMDsByPage(string name, decimal? skipNum, decimal? takeNum)
        {
            IQueryable<BMD_WWBMD> bmd = db.BMD_WWBMD.Where(t => t.STATUS == 1);
            if (!string.IsNullOrEmpty(name))
                bmd = bmd.Where(t => t.NAME.Contains(name));
            bmd = bmd.OrderBy(t => t.NAME);
            if (skipNum != null && takeNum != null)
                bmd = bmd.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<BMDModel> result = from b in bmd
                                          from t in db.BMD_BMDTYPE
                                          where b.TYPEID == t.TYPEID
                                          select new BMDModel
                                          {
                                              BMDId = b.BMDID,
                                              TypeId = b.TYPEID,
                                              TypeName = t.TYPENAME,
                                              CorrectUnit = b.CORRECTUNIT,
                                              Number = b.NUMBER,
                                              CorrectDate = b.CORRECTDATE,
                                              Name = b.NAME,
                                              OtherName = b.OTHERNAME,
                                              Sex = b.SEX == 1 ? "女" : "男",
                                              Nation = b.NATION,
                                              Birthday = b.BIRTHDAY,
                                              EDU = b.EDU,
                                              Job = b.JOB,
                                              Political = b.POLITICAL,
                                              IDCard = b.IDCARD,
                                              HeadIMGName = b.HEADIMGNAME,
                                              HeadIMGPath = b.HEADIMGPATH,
                                              BirthPlace = b.BIRTHPLACE,
                                              DomicilePlace = b.DOMICILEPLACE,
                                              Marriage = b.MARRIAGE == 1 ? "已婚" : "未婚",
                                              FixedPlace = b.FIXEDPLACE,
                                              CommonPlace = b.COMMONPLACE,
                                              SentenceNumber = b.SENTENCENUMBER,
                                              SentenceUnit = b.SENTENCEUNIT,
                                              SentenceDate = b.SENTENCEDATE,
                                              Charge = b.CHARGE,
                                              SentenceTerm = b.SENTENCETERM,
                                              SentenceAdd = b.SENTENCEADD,
                                              SentenceStartTime = b.SENTENCESTATRTIME,
                                              SentenceEndTime = b.SENTENCEENDTIME,
                                              SentenceChange = b.SENTENCECHANG,
                                              Reward = b.REWARD,
                                              CorrectStartTime = b.CORRECTSTARTTIME,
                                              CorrectEndTime = b.CORRECTENDTIME,
                                              SentenceType = b.SENTENCETYPE,
                                              Content = b.CONTENT,
                                              FileName = b.FILENAME,
                                              FilePath = b.FILEPATH,
                                              CreateTime = b.CREATETIME,
                                              CreateUser = b.CREATEUSER,
                                              Status = b.STATUS
                                          };
            return result.ToList();
        }
        /// <summary>
        /// 获取白名单数量
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetBMDsCount(string name)
        {
            IQueryable<BMD_WWBMD> bmd = db.BMD_WWBMD.Where(t => t.STATUS == 1);
            if (!string.IsNullOrEmpty(name))
                bmd = bmd.Where(t => t.NAME.Contains(name));
            int count = bmd.Count();
            return count;
        }
        /// <summary>
        /// 获取白名单区域
        /// </summary>
        /// <param name="BMDId"></param>
        /// <returns></returns>
        public List<BMDAreaModel> GetBMDAreas(decimal BMDId)
        {
            IQueryable<BMDAreaModel> result = db.BMD_USERAREA
                .Where(t => t.BMDID == BMDId)
                .Select(t => new BMDAreaModel
                {
                    UAId = t.UAID,
                    BMDId = t.BMDID,
                    AddressName = t.ADDRESSNAME,
                    Geometry = t.GEOMETRY
                });
            return result.ToList();
        }
    }
}
