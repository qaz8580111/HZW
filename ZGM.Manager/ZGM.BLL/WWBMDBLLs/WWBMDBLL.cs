using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Common.Enums;
using ZGM.Model;
using ZGM.Model.WWBMDModels;

namespace ZGM.BLL.WWBMDBLLs
{
    public class WWBMDBLL
    {
        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns>类型列表</returns>
        public static IQueryable<BMD_BMDTYPE> GetAllBMDType()
        {
            Entities db = new Entities();

            IQueryable<BMD_BMDTYPE> result = db.BMD_BMDTYPE.OrderBy(t => t.TYPEID);

            return result;
        }


        /// <summary>
        /// 获取一个新的单位标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewWWBMDID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_BMDID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }


        /// <summary>
        /// 根据ID获取实体类
        /// </summary>
        /// <returns></returns>
        public static BMD_WWBMD GetWWBMDByID(decimal ID)
        {
            Entities db = new Entities();
            BMD_WWBMD _model = db.BMD_WWBMD.FirstOrDefault(t => t.BMDID == ID);
            return _model;
        }


        /// <summary>
        /// 获取全部白名单列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<WWBMDModel> GetAllWWBMD()
        {
            Entities db = new Entities();
            IQueryable<WWBMDModel> WWBMDs = from s in db.BMD_WWBMD
                                            where s.STATUS == (decimal)StatusEnum.Normal
                                            select new WWBMDModel
                                            {
                                                BMDID = s.BMDID,
                                                TYPEID = s.TYPEID,
                                                CORRECTUNIT = s.CORRECTUNIT,
                                                NUMBER = s.NUMBER,
                                                CORRECTDATE = s.CORRECTDATE,
                                                NAME = s.NAME,
                                                OTHERNAME = s.OTHERNAME,
                                                SEX = s.SEX,
                                                NATION = s.NATION,
                                                BIRTHDAY = s.BIRTHDAY,
                                                EDU = s.EDU,
                                                JOB = s.JOB,
                                                POLITICAL = s.POLITICAL,
                                                IDCARD = s.IDCARD,
                                                HEADIMGNAME = s.HEADIMGNAME,
                                                HEADIMGPATH = s.HEADIMGPATH,
                                                BIRTHPLACE = s.BIRTHPLACE,
                                                DOMICILEPLACE = s.DOMICILEPLACE,
                                                MARRIAGE = s.MARRIAGE,
                                                FIXEDPLACE = s.FIXEDPLACE,
                                                COMMONPLACE = s.COMMONPLACE,
                                                SENTENCENUMBER = s.SENTENCENUMBER,
                                                SENTENCEUNIT = s.SENTENCEUNIT,
                                                SENTENCEDATE = s.SENTENCEDATE,
                                                CHARGE = s.CHARGE,
                                                SENTENCETERM = s.SENTENCETERM,
                                                SENTENCEADD = s.SENTENCEADD,
                                                SENTENCESTATRTIME = s.SENTENCESTATRTIME,
                                                SENTENCEENDTIME = s.SENTENCEENDTIME,
                                                SENTENCECHANG = s.SENTENCECHANG,
                                                REWARD = s.REWARD,
                                                CORRECTSTARTTIME = s.CORRECTSTARTTIME,
                                                CORRECTENDTIME = s.CORRECTENDTIME,
                                                SENTENCETYPE = s.SENTENCETYPE,
                                                CONTENT = s.CONTENT,
                                                FILENAME = s.FILENAME,
                                                FILEPATH = s.FILEPATH,
                                                CREATETIME = s.CREATETIME,
                                                CREATEUSER = s.CREATEUSER,
                                                STATUS = s.STATUS,
                                                TypeName = s.BMD_BMDTYPE.TYPENAME
                                            };
            return WWBMDs;
        }


        /// <summary>
        /// 添加白名单
        /// </summary>
        /// <param name="WWBMD"></param>
        /// <returns></returns>
        public static int AddWWBMD(WWBMDModel WWBMD)
        {
            Entities db = new Entities();
            BMD_WWBMD _model = new BMD_WWBMD();
            _model.BMDID = WWBMD.BMDID;
            _model.TYPEID = WWBMD.TYPEID;
            _model.CORRECTUNIT = WWBMD.CORRECTUNIT;
            _model.NUMBER = WWBMD.NUMBER;
            _model.CORRECTDATE = WWBMD.CORRECTDATE;
            _model.NAME = WWBMD.NAME;
            _model.OTHERNAME = WWBMD.OTHERNAME;
            _model.SEX = WWBMD.SEX;
            _model.NATION = WWBMD.NATION;
            _model.BIRTHDAY = WWBMD.BIRTHDAY;
            _model.EDU = WWBMD.EDU;
            _model.JOB = WWBMD.JOB;
            _model.POLITICAL = WWBMD.POLITICAL;
            _model.IDCARD = WWBMD.IDCARD;
            _model.HEADIMGNAME = WWBMD.HEADIMGNAME;
            _model.HEADIMGPATH = WWBMD.HEADIMGPATH;
            _model.BIRTHPLACE = WWBMD.BIRTHPLACE;
            _model.DOMICILEPLACE = WWBMD.DOMICILEPLACE;
            _model.MARRIAGE = WWBMD.MARRIAGE;
            _model.FIXEDPLACE = WWBMD.FIXEDPLACE;
            _model.COMMONPLACE = WWBMD.COMMONPLACE;
            _model.SENTENCENUMBER = WWBMD.SENTENCENUMBER;
            _model.SENTENCEUNIT = WWBMD.SENTENCEUNIT;
            _model.SENTENCEDATE = WWBMD.SENTENCEDATE;
            _model.CHARGE = WWBMD.CHARGE;
            _model.SENTENCETERM = WWBMD.SENTENCETERM;
            _model.SENTENCEADD = WWBMD.SENTENCEADD;
            _model.SENTENCESTATRTIME = WWBMD.SENTENCESTATRTIME;
            _model.SENTENCEENDTIME = WWBMD.SENTENCEENDTIME;
            _model.SENTENCECHANG = WWBMD.SENTENCECHANG;
            _model.REWARD = WWBMD.REWARD;
            _model.CORRECTSTARTTIME = WWBMD.CORRECTSTARTTIME;
            _model.CORRECTENDTIME = WWBMD.CORRECTENDTIME;
            _model.SENTENCETYPE = WWBMD.SENTENCETYPE;
            _model.CONTENT = WWBMD.CONTENT;
            _model.FILENAME = WWBMD.FILENAME;
            _model.FILEPATH = WWBMD.FILEPATH;
            _model.STATUS = 1;
            _model.CREATETIME = WWBMD.CREATETIME;
            _model.CREATEUSER = WWBMD.CREATEUSER;
            db.BMD_WWBMD.Add(_model);
            return db.SaveChanges();
        }


        /// <summary>
        /// 修改白名单
        /// </summary>
        /// <returns></returns>
        public static int EditWWBMD(BMD_WWBMD WWBMD)
        {
            Entities db = new Entities();
            BMD_WWBMD _model = db.BMD_WWBMD.FirstOrDefault(t => t.BMDID == WWBMD.BMDID && t.STATUS == (decimal)StatusEnum.Normal);
            if (_model != null)
            {
                _model.TYPEID = WWBMD.TYPEID;
                _model.CORRECTUNIT = WWBMD.CORRECTUNIT;
                _model.NUMBER = WWBMD.NUMBER;
                _model.CORRECTDATE = WWBMD.CORRECTDATE;
                _model.NAME = WWBMD.NAME;
                _model.OTHERNAME = WWBMD.OTHERNAME;
                _model.SEX = WWBMD.SEX;
                _model.NATION = WWBMD.NATION;
                _model.BIRTHDAY = WWBMD.BIRTHDAY;
                _model.EDU = WWBMD.EDU;
                _model.JOB = WWBMD.JOB;
                _model.POLITICAL = WWBMD.POLITICAL;
                _model.IDCARD = WWBMD.IDCARD;
                _model.HEADIMGNAME = WWBMD.HEADIMGNAME;
                _model.HEADIMGPATH = WWBMD.HEADIMGPATH;
                _model.BIRTHPLACE = WWBMD.BIRTHPLACE;
                _model.DOMICILEPLACE = WWBMD.DOMICILEPLACE;
                _model.MARRIAGE = WWBMD.MARRIAGE;
                _model.FIXEDPLACE = WWBMD.FIXEDPLACE;
                _model.COMMONPLACE = WWBMD.COMMONPLACE;
                _model.SENTENCENUMBER = WWBMD.SENTENCENUMBER;
                _model.SENTENCEUNIT = WWBMD.SENTENCEUNIT;
                _model.SENTENCEDATE = WWBMD.SENTENCEDATE;
                _model.CHARGE = WWBMD.CHARGE;
                _model.SENTENCETERM = WWBMD.SENTENCETERM;
                _model.SENTENCEADD = WWBMD.SENTENCEADD;
                _model.SENTENCESTATRTIME = WWBMD.SENTENCESTATRTIME;
                _model.SENTENCEENDTIME = WWBMD.SENTENCEENDTIME;
                _model.SENTENCECHANG = WWBMD.SENTENCECHANG;
                _model.REWARD = WWBMD.REWARD;
                _model.CORRECTSTARTTIME = WWBMD.CORRECTSTARTTIME;
                _model.CORRECTENDTIME = WWBMD.CORRECTENDTIME;
                _model.SENTENCETYPE = WWBMD.SENTENCETYPE;
                _model.CONTENT = WWBMD.CONTENT;
                _model.FILENAME = WWBMD.FILENAME;
                _model.FILEPATH = WWBMD.FILEPATH;
                //_model.CREATETIME = WWBMD.CREATETIME;
                //_model.CREATEUSER = WWBMD.CREATEUSER;
            }
            return db.SaveChanges();
        }


        /// <summary>
        /// 删除白名单
        /// </summary>
        /// <returns></returns>
        public static int DeleteWWBMD(BMD_WWBMD WWBMD)
        {
            Entities db = new Entities();
            BMD_WWBMD _model = db.BMD_WWBMD.FirstOrDefault(t => t.BMDID == WWBMD.BMDID && t.STATUS == (decimal)StatusEnum.Normal);
            if (_model != null)
            {
                _model.STATUS = (decimal)StatusEnum.Deleted;
            }
            return db.SaveChanges();
        }


        /// <summary>
        /// 获取白名单详情
        /// </summary>
        /// <returns></returns>
        public static WWBMDModel GetWWBMDDelByID(decimal ID)
        {
            Entities db = new Entities();
            IQueryable<WWBMDModel> WWBMD = from s in db.BMD_WWBMD
                                           where s.STATUS == (decimal)StatusEnum.Normal && s.BMDID == ID
                                           select new WWBMDModel
                                                {
                                                    BMDID = s.BMDID,
                                                    TYPEID = s.TYPEID,
                                                    CORRECTUNIT = s.CORRECTUNIT,
                                                    NUMBER = s.NUMBER,
                                                    CORRECTDATE = s.CORRECTDATE,
                                                    NAME = s.NAME,
                                                    OTHERNAME = s.OTHERNAME,
                                                    SEX = s.SEX,
                                                    NATION = s.NATION,
                                                    BIRTHDAY = s.BIRTHDAY,
                                                    EDU = s.EDU,
                                                    JOB = s.JOB,
                                                    POLITICAL = s.POLITICAL,
                                                    IDCARD = s.IDCARD,
                                                    HEADIMGNAME = s.HEADIMGNAME,
                                                    HEADIMGPATH = s.HEADIMGPATH,
                                                    BIRTHPLACE = s.BIRTHPLACE,
                                                    DOMICILEPLACE = s.DOMICILEPLACE,
                                                    MARRIAGE = s.MARRIAGE,
                                                    FIXEDPLACE = s.FIXEDPLACE,
                                                    COMMONPLACE = s.COMMONPLACE,
                                                    SENTENCENUMBER = s.SENTENCENUMBER,
                                                    SENTENCEUNIT = s.SENTENCEUNIT,
                                                    SENTENCEDATE = s.SENTENCEDATE,
                                                    CHARGE = s.CHARGE,
                                                    SENTENCETERM = s.SENTENCETERM,
                                                    SENTENCEADD = s.SENTENCEADD,
                                                    SENTENCESTATRTIME = s.SENTENCESTATRTIME,
                                                    SENTENCEENDTIME = s.SENTENCEENDTIME,
                                                    SENTENCECHANG = s.SENTENCECHANG,
                                                    REWARD = s.REWARD,
                                                    CORRECTSTARTTIME = s.CORRECTSTARTTIME,
                                                    CORRECTENDTIME = s.CORRECTENDTIME,
                                                    SENTENCETYPE = s.SENTENCETYPE,
                                                    CONTENT = s.CONTENT,
                                                    FILENAME = s.FILENAME,
                                                    FILEPATH = s.FILEPATH,
                                                    CREATETIME = s.CREATETIME,
                                                    CREATEUSER = s.CREATEUSER,
                                                    STATUS = s.STATUS,
                                                    TypeName = s.BMD_BMDTYPE.TYPENAME
                                                };
            return WWBMD.FirstOrDefault();
        }

        /// <summary>
        /// 删除所有白名单区域根据白名单ID
        /// </summary>
        /// <param name="BMDID"></param>
        public static void DeleteBMD_USERAREA(decimal BMDID)
        {
            Entities db = new Entities();
            IQueryable<BMD_USERAREA> list = db.BMD_USERAREA.Where(t => t.BMDID == BMDID);
            foreach (BMD_USERAREA item in list)
            {
                db.BMD_USERAREA.Remove(item);
                db.SaveChanges();
            }

        }

        /// <summary>
        /// 编号唯一校验
        /// </summary>
        /// <param name=""></param>
        public static string CheckNumber(string Number)
        {
            Entities db = new Entities();
            IQueryable<BMD_WWBMD> list = db.BMD_WWBMD.Where(t => t.NUMBER == Number);
            if (list.Count() == 0)
                return "0";
            else
                return "1";
        }
    }
}
