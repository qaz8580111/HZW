using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;


namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 生成行政处罚决定书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildXZCFSJDS(string regionName, string ajbh,
           XZCFJDS xzcfjds)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;
            //台城执罚先告子编号
            string Code = XZCFSJDSCode();

            //生成存放生成文书的路径
            string WSMC = "";
            if (xzcfjds.DSRLX == "gr")
            {
                WSMC = "行政处罚决定书_个人";
            }
            else if (xzcfjds.DSRLX == "dw")
            {
                WSMC = "行政处罚决定书_单位";
            }

            BuildDocPaths(regionName, ajbh, WSMC, out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$行政处罚决定书编号$", xzcfjds.BH);

            if (xzcfjds.DSRLX == "dw" && xzcfjds.orgForm != null)
            {
                dic.Add("$当事人$", xzcfjds.orgForm.MC);
                dic.Add("$代码证编号$", xzcfjds.orgForm.ZZJGDMZBH);
                dic.Add("$职务$", xzcfjds.orgForm.ZW);
                dic.Add("$法定代表人$", xzcfjds.orgForm.FDDBRXM);
            }
            else if (xzcfjds.DSRLX == "gr" && xzcfjds.personForm != null)
            {
                dic.Add("$当事人$", xzcfjds.personForm.XM);
                dic.Add("$性别$", xzcfjds.personForm.XB);
                dic.Add("$出生年月$", xzcfjds.personForm.CSNY);
                dic.Add("$民族$", xzcfjds.personForm.MZ);
                dic.Add("$身份证号$", xzcfjds.personForm.SFZH);
                dic.Add("$职业$", xzcfjds.GRZY);
            }
            dic.Add("$住所地$", xzcfjds.ZSD);
            dic.Add("$联系电话$", xzcfjds.LXDH);
            dic.Add("$行政处罚时间$", xzcfjds.XZCFSJ != null ? xzcfjds.XZCFSJ.Value.ToString("yyyy-MM-dd") : "    年  月  日");

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            wordUtility.ReplaceAdvancedRang("$内容说明$", xzcfjds.CONTENT, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 生成行政处罚事先告知书编号
        /// </summary>
        /// <returns></returns>
        public static string XZCFSJDSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.XZCFSXGZS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("台城执罚决字[ {0} ]第{1:D5}号", time1.ToString("yyyy"), ++count);
        }

        /// <summary>
        /// 查询处罚决定书返回处罚决定书编号
        /// </summary>
        /// <param name="WIID">流程标识</param>
        /// <returns>处罚决定书编号</returns>
        public static string GetXZCFJDSBHByWIID(string WIID)
        {
            PLEEntities db = new PLEEntities();
            return db.DOCINSTANCES.First(t => t.WIID == WIID && t.DDID == DocDefinition.CFJDS).DOCBH;
        }
    }
}
