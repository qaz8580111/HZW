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
        ///查封(扣押)决定书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildCFKYJDS(string regionName, string ajbh,
             CFKYJDS cfkyjds)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;
            //台城执抽证通字编号
            string Code = CYQZTZSCode();

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "查封（扣押）决定书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$编号$", cfkyjds.BH);
            dic.Add("$当事人$", cfkyjds.DSR);
            dic.Add("$地址$", cfkyjds.DZ);
            dic.Add("$违法行为$", cfkyjds.WFXW);
            //dic.Add("$接受调查处理时间年$", cfkyjds.JSDCCLSJ.Year.ToString());
            //dic.Add("$接受调查处理时间月$", cfkyjds.JSDCCLSJ.Month.ToString());
            //dic.Add("$接受调查处理时间日$", cfkyjds.JSDCCLSJ.Day.ToString());
            //dic.Add("$处理时间时$", cfkyjds.JSDCCLSJ.Hour.ToString());
            //dic.Add("$处理地点$", cfkyjds.JSDCCLDD);
            //dic.Add("$日$", cfkyjds.KYQXTS);
            //dic.Add("$年1$", cfkyjds.StartTime.Year.ToString());
            //dic.Add("$月1$", cfkyjds.StartTime.Month.ToString());
            //dic.Add("$日1$", cfkyjds.StartTime.Day.ToString());
            //dic.Add("$年2$", cfkyjds.EndTime.Year.ToString());
            //dic.Add("$月2$", cfkyjds.EndTime.Month.ToString());
            //dic.Add("$日2$", cfkyjds.EndTime.Day.ToString());
            //dic.Add("$物品存放地点$", cfkyjds.CFKYWPCFDD);
            //dic.Add("$联系人$", cfkyjds.LXR);
            //dic.Add("$联系电话$", cfkyjds.LXDH);
            //dic.Add("$执法人员$", cfkyjds.ZFRY);
            //dic.Add("$执法证号$", cfkyjds.ZFZH);
            //dic.Add("$当事人签名$", cfkyjds.DSRQM);
            //dic.Add("$年3$", cfkyjds.DSRQMRQ.Year.ToString());
            //dic.Add("$月3$", cfkyjds.DSRQMRQ.Month.ToString());
            //dic.Add("$日3$", cfkyjds.DSRQMRQ.Day.ToString());
            //dic.Add("$见证人签名$", cfkyjds.JZRQM);
            //dic.Add("$年4$", cfkyjds.JZRQMRQ.Year.ToString());
            //dic.Add("$月4$", cfkyjds.JZRQMRQ.Month.ToString());
            //dic.Add("$日4$", cfkyjds.JZRQMRQ.Day.ToString());
            //dic.Add("$查封扣押决定时间$", cfkyjds.CFKYJDSJ.ToString("yyyy年MM月dd日"));

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            

            wordUtility.InsertTable(1, cfkyjds.WPQDLBS);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 返回查封（扣押）通知书 据定数编号
        /// </summary>
        /// <returns></returns>
        public static string GetCFKYJDSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.CFKYJDS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("CFKYTZS{0:D5}号", time1.ToString("yyyy"), ++count);
        }
    }
}
