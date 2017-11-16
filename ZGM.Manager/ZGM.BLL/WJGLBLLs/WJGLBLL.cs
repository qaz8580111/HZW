using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ZGM.Model;
using ZGM.Model.ViewModels;
using ZGM.Model.CustomModels;

namespace ZGM.BLL.WJGLBLLs
{
    public class WJGLBLL
    {
        /// <summary>
        /// 添加新建违建建筑
        /// </summary>
        /// <returns>1:成功，0：失败</returns>
        public static int AddWJGL(WJGL_NONBUILDINGS model, List<FileClass> list)
        {
            Entities db = new Entities();
            using (System.Transactions.TransactionScope transaction = new System.Transactions.TransactionScope())
            {
                try
                {
                    //添加违建基础信息
                    if (model != null)
                    {
                        db.WJGL_NONBUILDINGS.Add(model);
                        db.SaveChanges();
                    }
                    //添加改造前图片
                    for (int i = 0; i < list.Count; i++)
                    {
                        WJGL_PICTURES pmodel = new WJGL_PICTURES();
                        pmodel.PICTUREID = i;
                        pmodel.WJID = model.WJID;
                        pmodel.SSWJ = model.WJTYPE;
                        pmodel.WJPICTURETYPE = 1;
                        pmodel.FILENAME = list[i].FilesName;
                        pmodel.FILEPATH = list[i].FilesPath;
                        pmodel.CREATETIME = DateTime.Now;
                        db.WJGL_PICTURES.Add(pmodel);
                        db.SaveChanges();
                    }
                    transaction.Complete();
                    return 1;
                }
                catch {
                    transaction.Dispose();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 修改违建建筑
        /// </summary>
        /// <returns>1:成功，0：失败</returns>
        public static int EditWJGL(string wjid, WJGL_NONBUILDINGS model, string oldimages, List<FileClass> list)
        {
            Entities db = new Entities();
            WJGL_NONBUILDINGS mmodel = db.WJGL_NONBUILDINGS.FirstOrDefault(t => t.WJID == wjid);
            //orderby排序对应已操作的图片
            List<WJGL_PICTURES> plist = db.WJGL_PICTURES.Where(t => t.WJID == wjid && t.WJPICTURETYPE == 2).OrderBy(t => t.PICTUREID).ToList();

            using (System.Transactions.TransactionScope transaction = new System.Transactions.TransactionScope())
            {
                try
                {
                    //删除改造后的图片
                    if (!string.IsNullOrEmpty(oldimages) && oldimages != "0")
                    {
                        //对前台传过来的图片操作位置进行处理
                        string[] images = oldimages.Split(',');
                        for (int i = 0; i < images.Length; i++)
                        {
                            //得到具体操作图片的ID标识
                            decimal pid = plist[int.Parse(images[i]) - 1].PICTUREID;
                            //获取前台修改后上传的图片，对原来相应的图片做删除操作
                            WJGL_PICTURES pmodel = db.WJGL_PICTURES.FirstOrDefault(t => t.PICTUREID == pid);
                            if (pmodel != null)
                            {
                                db.WJGL_PICTURES.Remove(pmodel);
                            }
                            db.SaveChanges();
                        }
                    }
                    

                    //修改违建信息表
                    if (model != null)
                    {
                        mmodel.WJUNIT = model.WJUNIT;
                        mmodel.STATE = model.STATE;
                        mmodel.IDENTITYID = model.IDENTITYID;
                        mmodel.ZONEID = model.ZONEID;
                        mmodel.TEL = model.TEL;
                        mmodel.MAPLOACTION = model.MAPLOACTION;
                        mmodel.WJADDRESS = model.WJADDRESS;
                        mmodel.WJTIME = model.WJTIME;
                        mmodel.WJTYPE = model.WJTYPE;
                        mmodel.WJFRAME = model.WJFRAME;
                        mmodel.WJFLOOR = model.WJFLOOR;
                        mmodel.LANDTYPE = model.LANDTYPE;
                        mmodel.WJUSE = model.WJUSE;
                        mmodel.LANDAREA = model.LANDAREA;
                        mmodel.BUILDINGAREA = model.BUILDINGAREA;
                        mmodel.REMOVETIME = model.REMOVETIME;
                        mmodel.REMOVEAREA = model.REMOVEAREA;
                        mmodel.CONSTRUCTIONPROJECT = model.CONSTRUCTIONPROJECT;
                        mmodel.SIXCASE = model.SIXCASE;
                        mmodel.RECTIFICATIONCASE = model.RECTIFICATIONCASE;
                        mmodel.RECTIFICATIONTIME = model.RECTIFICATIONTIME;
                        mmodel.REMARK1 = model.REMARK1;
                        mmodel.X2000 = model.X2000;
                        mmodel.Y2000 = model.Y2000;
                        db.SaveChanges();
                    }

                    //上传改造后的操作图片
                    for (int i = 0; i < list.Count; i++)
                    {
                        WJGL_PICTURES pmodel = new WJGL_PICTURES();
                        pmodel.PICTUREID = i;
                        pmodel.WJID = wjid;
                        pmodel.SSWJ = model.WJTYPE;
                        pmodel.WJPICTURETYPE = 2;
                        pmodel.FILENAME = list[i].FilesName;
                        pmodel.FILEPATH = list[i].FilesPath;
                        pmodel.CREATETIME = DateTime.Now;
                        db.WJGL_PICTURES.Add(pmodel);
                        db.SaveChanges();
                    }
                    transaction.Complete();
                    return 1;
                }
                catch {
                    transaction.Dispose();
                    return 0;
                }
                
            }
        }

        /// <summary>
        /// 根据图片路径删除图片
        /// </summary>
        /// <returns></returns>
        public static int DeleteImageByFilePath(string FilePath)
        {
            Entities db = new Entities();
            WJGL_PICTURES model = db.WJGL_PICTURES.FirstOrDefault(t => t.FILEPATH == FilePath);            
            if (model != null)
            {
                db.WJGL_PICTURES.Remove(model);                
            }
            return db.SaveChanges();
        }

        /// <summary>
        /// 查询违法建筑列表
        /// </summary>
        /// <returns></returns>
        public static List<VMWJGL> GetSearchData(string WJUnit, string WJAddress, string STime, string ETime, string State)
        {
            Entities db = new Entities();
            IQueryable<VMWJGL> list = from wjnb in db.WJGL_NONBUILDINGS
                                      from zone in db.SYS_ZONES
                                      where wjnb.ZONEID == zone.ZONEID
                                      select new VMWJGL
                                      {
                                          WJID = wjnb.WJID,
                                          WJUNIT = wjnb.WJUNIT,
                                          WJTYPE = wjnb.WJTYPE,
                                          WJTIME = wjnb.WJTIME,
                                          ZoneName = zone.ZONENAME,
                                          WJADDRESS = wjnb.WJADDRESS,
                                          STATE = wjnb.STATE
                                      };
            if (!string.IsNullOrEmpty(WJUnit))
                list = list.Where(t => t.WJUNIT.Contains(WJUnit));
            if (!string.IsNullOrEmpty(WJAddress))
                list = list.Where(t => t.WJADDRESS.Contains(WJAddress));
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.WJTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.WJTIME < endTime);
            }
            if (decimal.Parse(State) != 0)
            {
                decimal SState = decimal.Parse(State);
                list = list.Where(t => t.STATE == SState);
            }

            return list.ToList();
        }

        /// <summary>
        /// 根据违建标识获取违建信息
        /// </summary>
        /// <returns></returns>
        public static VMWJGL GetWJGLByWJID(string Id)
        {
            Entities db = new Entities();
            WJGL_NONBUILDINGS model = db.WJGL_NONBUILDINGS.FirstOrDefault(t => t.WJID == Id);
            VMWJGL vmodel = new VMWJGL();
            if (model != null)
            {
                vmodel.WJID = model.WJID;
                vmodel.WJUNIT = model.WJUNIT;
                vmodel.STATE = model.STATE;
                vmodel.IDENTITYID = model.IDENTITYID;
                vmodel.ZONEID = model.ZONEID;
                vmodel.TEL = model.TEL;
                vmodel.MAPLOACTION = model.MAPLOACTION;
                vmodel.WJADDRESS = model.WJADDRESS;
                vmodel.WJTIMEStr = ((DateTime)model.WJTIME).ToString("yyyy-MM-dd");
                vmodel.WJTYPE = model.WJTYPE;
                vmodel.WJFRAME = model.WJFRAME;
                vmodel.WJFLOOR = model.WJFLOOR;
                vmodel.LANDTYPE = model.LANDTYPE;
                vmodel.WJUSE = model.WJUSE;
                vmodel.LANDAREA = model.LANDAREA;
                vmodel.BUILDINGAREA = model.BUILDINGAREA;
                if (model.REMOVETIME != null)
                    vmodel.REMOVETIMEStr = ((DateTime)model.REMOVETIME).ToString("yyyy-MM-dd");
                vmodel.REMOVEAREA = model.REMOVEAREA;
                vmodel.CONSTRUCTIONPROJECT = model.CONSTRUCTIONPROJECT;
                vmodel.SIXCASE = model.SIXCASE;
                vmodel.RECTIFICATIONCASE = model.RECTIFICATIONCASE;
                if (model.RECTIFICATIONTIME != null)
                    vmodel.RECTIFICATIONTIMEStr = ((DateTime)model.RECTIFICATIONTIME).ToString("yyyy-MM-dd");
                vmodel.REMARK1 = model.REMARK1;
            }

            //获取改造前的图片
            IQueryable<WJGL_PICTURES> blist = db.WJGL_PICTURES.Where(t => t.WJID == Id && t.WJPICTURETYPE == 1).OrderBy(t => t.PICTUREID);            
            //获取改造后的图片
            IQueryable<WJGL_PICTURES> alist = db.WJGL_PICTURES.Where(t => t.WJID == Id && t.WJPICTURETYPE == 2).OrderBy(t=>t.PICTUREID);
            if (blist.Count() != 0)
            {
                foreach (WJGL_PICTURES item in blist)
                {
                    vmodel.BeforePic += item.FILEPATH + "|";
                }
                vmodel.BeforePic = vmodel.BeforePic.Substring(0, vmodel.BeforePic.Length - 1);
            }
            if (alist.Count() != 0)
            {
                foreach (WJGL_PICTURES item in alist)
                {
                    vmodel.AfterPic += item.FILEPATH + "|";
                }
                vmodel.AfterPic = vmodel.AfterPic.Substring(0, vmodel.AfterPic.Length - 1);
            }

            return vmodel;
        }

    }
}
