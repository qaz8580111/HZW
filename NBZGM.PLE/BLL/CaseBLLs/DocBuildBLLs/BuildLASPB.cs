using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;
using System.Data;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 生成立案审批表
        /// </summary>
        /// <param name="dic"></param>
        public static string BuildLASPB(string regionName, string ajbh,
            LASPB laspb)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "立案审批表", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            //立案审批表编号

            dic.Add("$文书编号$", laspb.WSBH);
            dic.Add("$案由$", laspb.AY);
            dic.Add("$发案地点$", laspb.FADD);
            dic.Add("$发案时间$", laspb.FASJ);
            dic.Add("$案件来源$", laspb.AJLY);
            dic.Add("$名称$", laspb.MC);
            dic.Add("$组织机构代码证编号$", laspb.ZZJGDMZBH);
            dic.Add("$法定代表人$", laspb.FDDBRXM);
            dic.Add("$职务$", laspb.ZW);
            dic.Add("$姓名$", laspb.XM);
            dic.Add("$性别$", laspb.XB);
            dic.Add("$出生年月$", laspb.CSNY);
            dic.Add("$名族$", laspb.MZ);
            dic.Add("$身份证号$", laspb.SFZH);
            dic.Add("$工作单位$", laspb.GZDW);
            dic.Add("$住所地$", laspb.ZSD);
            dic.Add("$联系电话$", laspb.LXDH);
            dic.Add("$案情摘要$", laspb.AQZY);
            dic.Add("$立案理由$", laspb.LALY);
            dic.Add("$拟办意见$", laspb.NBYJ);
            dic.Add("$拟办意见签名$", laspb.NBDYNAME1+"、"+laspb.NBDYNAME2);
            dic.Add("$拟办意见签名日期$", laspb.NBYJQMRQ);
            dic.Add("$承办人员$", laspb.CBRY);
            dic.Add("$领导审批意见签名$", laspb.LDSPYJQM);
            dic.Add("$领导审批意见签名日期$", laspb.LDSPYJQMRQ);
            dic.Add("$法制处审批意见$", laspb.FZCSPYJ);
            dic.Add("$法制处签名日期$", laspb.FZCSPYJQMRQ);
            dic.Add("$法制机构签名$", laspb.FZCQM);
            dic.Add("$分管副局长审批意见$", laspb.FGFJZSPYJ);
            dic.Add("$分管副局长签名日期$", laspb.FGFJZSPRQ);
            dic.Add("$分管副局长签名$", laspb.FGLD);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);

            if (laspb.Approve == null)
            {
                wordUtility.ReplaceAdvancedRang("$同意$", "□", null);
                wordUtility.ReplaceAdvancedRang("$不同意$", "□", null);
            }
            else if (laspb.Approve.Value)
            {
                wordUtility.ReplaceAdvancedRang("$同意$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$不同意$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$同意$", "□", null);
                wordUtility.ReplaceAdvancedRang("$不同意$", "R", "Wingdings 2");
            }
            //法制处同意与不同意
            if (laspb.FZCSFTY == null)
            {
                wordUtility.ReplaceAdvancedRang("$法制机构同意$", "□", null);
                wordUtility.ReplaceAdvancedRang("$法制机构不同意$", "□", null);
            }
            else if (laspb.FZCSFTY.Value)
            {
                wordUtility.ReplaceAdvancedRang("$法制机构同意$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$法制机构不同意$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$法制机构同意$", "□", null);
                wordUtility.ReplaceAdvancedRang("$法制机构不同意$", "R", "Wingdings 2");
            }
            //分管局长同意与不同意
            if (laspb.FGFJZSFTY == null)
            {
                wordUtility.ReplaceAdvancedRang("$分管副局长同意$", "□", null);
                wordUtility.ReplaceAdvancedRang("$分管副局长不同意$", "□", null);
            }
            else if (laspb.FGFJZSFTY.Value)
            {
                wordUtility.ReplaceAdvancedRang("$分管副局长同意$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$分管副局长不同意$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$分管副局长同意$", "□", null);
                wordUtility.ReplaceAdvancedRang("$分管副局长不同意$", "R", "Wingdings 2");
            }
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 立案审批表编号
        /// </summary>
        /// <returns></returns>
        public static string GetLASPBCode()
        {
            PLEEntities db = new PLEEntities();
            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);
            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.LASPB).Count() + 1;

            return string.Format("台城执立[ {0} ]第{1:D5}", time1.ToString("yyyy"), count);
        }
    }
}
