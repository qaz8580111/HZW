using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        ///其他事项内部审批表
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildQTSXNBSPB(string regionName, string ajbh,
             QTSXNBSPB qtsxnbspb)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "其他事项内部审批表", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$申请事项$", qtsxnbspb.SQSX);
            dic.Add("$文书编号$", qtsxnbspb.WSBH);
            dic.Add("$案由$", qtsxnbspb.AY);
            dic.Add("$立案日期$", string.Format("{0:yyyy年MM月dd日}", qtsxnbspb.LARQ));
            dic.Add("$姓名$", qtsxnbspb.XM);
            dic.Add("$性别$", qtsxnbspb.XB);
            dic.Add("$职业$", qtsxnbspb.ZY);
            dic.Add("$民族$", qtsxnbspb.MZ);
            dic.Add("$身份证号码$", qtsxnbspb.SFZHM);
            dic.Add("$名称$", qtsxnbspb.MC);
            dic.Add("$法定代表人$", qtsxnbspb.FDDBR);
            dic.Add("$职务$", qtsxnbspb.ZW);
            dic.Add("$工作单位$", qtsxnbspb.GZDW);
            dic.Add("$电话$", qtsxnbspb.DH);
            dic.Add("$住址$", qtsxnbspb.ZZ);
            dic.Add("$邮政编码$", qtsxnbspb.YZBM);
            dic.Add("$简要案情$", qtsxnbspb.JYAQ);
            dic.Add("$承办人意见$", qtsxnbspb.CBRYJ);
            dic.Add("$承办人签章$", qtsxnbspb.CBRQZ);
            dic.Add("$承办人日期$", qtsxnbspb.CBRQZRQ);

            dic.Add("$承办机构审核意见$", qtsxnbspb.CBJGSHYJ);
            dic.Add("$承办机构签章$", qtsxnbspb.CBJGSHQZ);
            dic.Add("$承办机构日期$", qtsxnbspb.CBJGSHQZRQ);

            dic.Add("$行政机关负责人审批意见$", qtsxnbspb.XZJGFZRSPYJ);
            dic.Add("$行政机关签章$", qtsxnbspb.XZJGFZRSPQZ);
            dic.Add("$行政机关日期$", qtsxnbspb.XZJGFZRSPQZRQ);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 获取其他事项相关审批编号
        /// </summary>
        /// <returns>其他事项相关审批编号</returns>
        public static string GetQTSXNBSPBCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.QTSXNBSPB).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("QTSXNBSPB{0:D5}号", ++count);
        }
    }
}
