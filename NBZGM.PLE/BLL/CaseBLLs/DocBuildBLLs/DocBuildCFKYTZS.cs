using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 查封（扣押）通知书
        /// </summary>
        /// <param name="regionName"></param>
        /// <param name="ajbh"></param>
        /// <param name="cfkytzs"></param>
        /// <returns></returns>
        public static string DocBuildCFKYTZS(string regionName, string ajbh, CFKYTZS cfkytzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "查封（扣押）通知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$编号$", cfkytzs.BH);
            dic.Add("$当事人名称或姓名$", cfkytzs.DSR);
            dic.Add("$违法行为$", cfkytzs.WFXW);
            dic.Add("$法律法规$", cfkytzs.FVFG);
            dic.Add("$违法地点$", cfkytzs.DD);
            dic.Add("$执法队员1$", GetZFDYName(cfkytzs.ZFRY1));
            dic.Add("$执法队员2$", GetZFDYName(cfkytzs.ZFRY2));
            dic.Add("$执法队员编号1$", GetZFDYZFBH(cfkytzs.ZFRY1));
            dic.Add("$执法队员编号2$", GetZFDYZFBH(cfkytzs.ZFRY2));

            dic.Add("$查封扣押通知时间$", cfkytzs.CFKYTZSJ.Value.ToString("yyyy年MM月dd日"));

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.InsertTable(1, cfkytzs.CFKYWPQDList);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 返回查封（扣押）通知书编号
        /// </summary>
        /// <returns></returns>
        //public static string GetCFKYTZSCode()
        //{
        //    PLEEntities db = new PLEEntities();

        //    int count = db.DOCINSTANCES
        //        .Where(t => t.DDID == DocDefinition.CFKYTZS).Count();

        //    DateTime time = DateTime.Now;
        //    DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

        //    return string.Format("台城查封（扣押）通决字[ {0} ]第{1:D5}号", time1.ToString("yyyy"), ++count);
        //}

        /// <summary>
        /// 返回当前流程下的查封（扣押通知书）
        /// </summary>
        /// <param name="WIID">工作流标识</param>
        /// <returns></returns>
        public static IQueryable<DOCINSTANCE> GetCFKYTZSList(string WIID)
        {
            PLEEntities db = new PLEEntities();
            var list = db.DOCINSTANCES.Where(t => t.WIID == WIID && t.DDID == DocDefinition.CFKYTZS);
            return list;
        }

        /// <summary>
        /// 返回查封（扣押）通知书 据定数编号
        /// </summary>
        /// <returns></returns>
        public static string GetCFKYTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.CFKYTZS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("CFKYTZS{0:D5}号", ++count);
        }

        /// <summary>
        /// 返回执法队员姓名
        /// </summary>
        /// <param name="zfdy"></param>
        /// <returns></returns>
        private static string GetZFDYName(string zfdy)
        {
            string Name = "";
            if (!string.IsNullOrWhiteSpace(zfdy))
            {
                string[] res = zfdy.Split(',');
                if (res.Length >= 3)
                {
                    Name = res[0];
                }
            }
            return Name;
        }

        /// <summary>
        /// 返回执法队员编号
        /// </summary>
        /// <param name="zfdy"></param>
        /// <returns></returns>
        private static string GetZFDYZFBH(string zfdy)
        {
            string BH = "";
            if (!string.IsNullOrWhiteSpace(zfdy))
            {
                string[] res = zfdy.Split(',');
                if (res.Length >= 3)
                {
                    BH = res[1];
                }
            }
            return BH;
        }
    }
}
