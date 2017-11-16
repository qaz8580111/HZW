using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.SimpleCaseModels;

namespace Taizhou.PLE.BLL.CaseBLLs
{
    public class SimpleCaseBLL
    {
        /// <summary>
        /// 新增简易案件
        /// </summary>
        /// <param name="simpleCase">简易案件对象</param>
        /// <param name="files">简易案件图片文件</param>
        /// <param name="pictureTypes">简易案件图片类型数组</param>
        public static void AddSimpleCase(SimpleCase simpleCase, HttpFileCollectionBase files,
            string[] pictureTypes)
        {
            PLEEntities db = new PLEEntities();

            string sql = "SELECT SEQ_SIMPLECASEID.NEXTVAL FROM DUAL";
            decimal simpleCaseID = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(simpleCase.DTMD))
            {
                string[] DTMD = simpleCase.DTMD.Split(',');
                simpleCase.Lon = decimal.Parse(DTMD[0]);
                simpleCase.Lat = decimal.Parse(DTMD[1]);
            }
            SIMPLECAS simpleCas = new SIMPLECAS
            {
                SIMPLECASEID = simpleCaseID,
                JDSBH = simpleCase.JDSBH,
                DSRNAME = simpleCase.DSRName,
                DSRGENDER = simpleCase.DSRGender,
                DSRIDNUMBER = simpleCase.DSRIDNumber,
                FZRNAME = simpleCase.FZRName,
                FZRPOSITION = simpleCase.FZRPosition,
                FZRADDRESS = simpleCase.FZRAddress,
                CASETIME = simpleCase.CaseTime,
                CASEADDRESS = simpleCase.CaseAddress,
                ILLEGALITEMID = simpleCase.IllegalItemID,
                FKJE = simpleCase.FKJE,
                JKYH = simpleCase.JKYH,
                BANKACCOUNT = simpleCase.BankAccount,
                BANKACCOUNTNAME = simpleCase.BankAccountName,
                ZFRNAME = simpleCase.ZFRName,
                ZFZH = simpleCase.ZFZH,
                ZFSJ = simpleCase.ZFSJ,
                LON = simpleCase.Lon,
                LAT = simpleCase.Lat,
                DSRLX = simpleCase.DSRLX
            };

            for (int i = 0; i < files.Keys.Count; i++)
            {
                HttpPostedFileBase file = files[files.Keys[i]];

                if (file == null || file.ContentLength <= 0)
                {
                    continue;
                };

                int upPhotoLength = file.ContentLength;
                string upPhotoname = file.FileName;
                string uphototype = file.ContentType;

                if (uphototype.Equals("image/x-png")
                    || uphototype.Equals("image/png")
                    || uphototype.Equals("image/gif")
                    || uphototype.Equals("image/peg")
                    || uphototype.Equals("image/jpeg"))
                {
                    byte[] PhotoArray = new Byte[upPhotoLength];
                    Stream PhotoStream = file.InputStream;
                    PhotoStream.Read(PhotoArray, 0, upPhotoLength);

                    string str = "SELECT SEQ_SIMPLECASEPICTUREID.NEXTVAL FROM DUAL";
                    decimal pictureID = db.Database.SqlQuery<decimal>(str).FirstOrDefault();

                    SIMPLECASEPICTURE scp = new SIMPLECASEPICTURE
                    {
                        SIMPLECASEPICTUREID = pictureID,
                        SIMPLECASEID = simpleCaseID,
                        PICTURETYPE = decimal.Parse(pictureTypes[i]),
                        PICTURE = PhotoArray
                    };

                    db.SIMPLECASEPICTURES.Add(scp);
                }
                //Image image = new System.Drawing.Bitmap(file.InputStream);
            }

            db.SIMPLECASES.Add(simpleCas);
            db.SaveChanges();
        }

        /// <summary>
        /// 获取所有简易案件列表
        /// </summary>
        /// <returns>简易案件列表</returns>
        public static IQueryable<SimpleCase> GetSimpleCases()
        {
            PLEEntities db = new PLEEntities();

            var results = from sc in db.SIMPLECASES
                          from ii in db.ILLEGALITEMS
                          where sc.ILLEGALITEMID == ii.ILLEGALITEMID
                          select new SimpleCase
                          {
                              SimpleCaseID = sc.SIMPLECASEID,
                              JDSBH = sc.JDSBH,
                              WFXWName = ii.ILLEGALCODE + ii.ILLEGALITEMNAME,
                              CaseTime = sc.CASETIME,
                              DSRName = sc.DSRNAME,
                              FZRName = sc.FZRNAME,
                              DSRLX = sc.DSRLX
                          };

            return results;
        }

        /// <summary>
        /// 根据简易案件标识获取简易案件对象
        /// </summary>
        /// <param name="simpleCaseID">简易案件标识</param>
        /// <returns>简易案件对象</returns>
        public static SimpleCase GetSimpleCaseBySimpleCaseID(decimal simpleCaseID)
        {
            PLEEntities db = new PLEEntities();

            SimpleCase simpleCase = (from sc in db.SIMPLECASES
                                     from ii in db.ILLEGALITEMS
                                     where sc.ILLEGALITEMID == ii.ILLEGALITEMID
                                        && sc.SIMPLECASEID == simpleCaseID
                                     select new SimpleCase
                                     {
                                         JDSBH = sc.JDSBH,
                                         DSRName = sc.DSRNAME,
                                         DSRGender = sc.DSRGENDER,
                                         DSRIDNumber = sc.DSRIDNUMBER,
                                         FZRName = sc.FZRNAME,
                                         FZRPosition = sc.FZRPOSITION,
                                         FZRAddress = sc.FZRADDRESS,
                                         CaseTime = sc.CASETIME,
                                         CaseAddress = sc.CASEADDRESS,
                                         IllegalCode = ii.ILLEGALCODE,
                                         WFXWName = ii.ILLEGALITEMNAME,
                                         WEIZE = ii.WEIZE,
                                         FAZE = ii.FZZE,
                                         FKJE = sc.FKJE,
                                         JKYH = sc.JKYH,
                                         BankAccount = sc.BANKACCOUNT,
                                         BankAccountName = sc.BANKACCOUNTNAME,
                                         ZFRName = sc.ZFRNAME,
                                         ZFZH = sc.ZFZH,
                                         ZFSJ = sc.ZFSJ,
                                         Lon = sc.LON,
                                         Lat = sc.LAT,
                                         DSRLX = sc.DSRLX
                                     }).SingleOrDefault();

            return simpleCase;
        }
        /// <summary>
        /// 获取简易事件的条数
        /// </summary>
        /// <returns></returns>
        public static int simpCaseCount()
        {
            PLEEntities db = new PLEEntities();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return db.SIMPLECASES.Where(t => t.CASETIME > dt).Count();
        }
        /// <summary>
        /// 获得简易事件每月每天的数量
        /// </summary>
        /// <returns></returns>
        public static List<SIMPLECAS> GetSIMPLECASByMum()
        {
            PLEEntities db = new PLEEntities();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            List<SIMPLECAS> listc = db.SIMPLECASES.Where(t => t.CASETIME > dt).ToList();
            return listc;
        }
    }
}
