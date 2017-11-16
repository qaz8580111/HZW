using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 生成案件处理审批表
        /// </summary>
        /// <param name="dic"></param>
        public static string BuildAJCLSPB(string regionName, string ajbh,
            AJCLSPB ajclspb)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "案件处理审批表", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$案由$", ajclspb.AY);
            dic.Add("$案件来源$", ajclspb.AJLY);
            dic.Add("$名称$", ajclspb.MC);
            dic.Add("$组织机构代码证编号$", ajclspb.ZZJGDMZBH);
            dic.Add("$法定代表人姓名$", ajclspb.FDDBRXM);
            dic.Add("$职务$", ajclspb.ZW);
            dic.Add("$姓名$", ajclspb.XM);
            dic.Add("$性别$", ajclspb.XB);
            dic.Add("$出生年月$", ajclspb.CSNY);
            dic.Add("$民族$", ajclspb.MZ);
            dic.Add("$身份证号$", ajclspb.SFZH);
            dic.Add("$工作单位$", ajclspb.GZDW);
            dic.Add("$住所地$", ajclspb.ZSD);
            dic.Add("$联系电话$", ajclspb.LXDH);
            dic.Add("$认定的违法事实$", ajclspb.RDDWFSS);
            dic.Add("$证据$", ajclspb.ZJ);
            dic.Add("$违法的法律法规和规律$", ajclspb.WFDFLFGHGZ);
            dic.Add("$处罚依据$", ajclspb.CFYJ);

            dic.Add("$主办队员意见$", ajclspb.DCJBHCBRYJ);
            dic.Add("$主办队员签章$", ajclspb.DCJBHCBRZ);
            dic.Add("$主办队员签章日期$", ajclspb.DCJBHCBRQ == null ?
                "     年  月  日" : ajclspb.DCJBHCBRQ.Value.ToString("yyyy年MM月dd日"));

            dic.Add("$协办队员意见$", ajclspb.XBDYYJ);
            dic.Add("$协办队员签章$", ajclspb.XBDYQZ);
            dic.Add("$协办队员签章日期$", ajclspb.XBDYCLSJ == null ?
                "     年  月  日" : ajclspb.XBDYCLSJ.Value.ToString("yyyy年MM月dd日"));

            dic.Add("$办案意见$", ajclspb.BADWYJ);
            dic.Add("$办案签章$", ajclspb.BADWQZ);
            dic.Add("$办案签章日期$", ajclspb.BADWRQ == null ?
                "     年  月  日" : ajclspb.BADWRQ.Value.ToString("yyyy年MM月dd日"));


            dic.Add("$法制机构意见$", ajclspb.FZCYJ);
            dic.Add("$法制机构签章$", ajclspb.FZCQZ);
            dic.Add("$法制机构签章日期$", ajclspb.FZCQZRQ == null ?
                "     年  月  日" : ajclspb.FZCQZRQ.Value.ToString("yyyy年MM月dd日"));

            dic.Add("$分管领导意见$", ajclspb.FGLDYJ);
            dic.Add("$分管领导签章$", ajclspb.FGLDQZ);
            dic.Add("$分管领导签章日期$", ajclspb.FGLDQZRQ == null ?
                "     年  月  日" : ajclspb.FGLDQZRQ.Value.ToString("yyyy年MM月dd日"));

            dic.Add("$备注$", ajclspb.BZ);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
