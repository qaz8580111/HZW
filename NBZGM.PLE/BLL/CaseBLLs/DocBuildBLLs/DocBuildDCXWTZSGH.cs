using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.CMS.BLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        ///调查询问通知书规划
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildDCXWTZSGH(string regionName, string ajbh,
             DCXWTZSGH dcxwtzsgh)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "调查询问通知书（规划）", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$案件编号$", dcxwtzsgh.BH);
            dic.Add("$当事人$", dcxwtzsgh.DSR);
            dic.Add("$发案时间$", dcxwtzsgh.FASJ != null ? DateTime.Parse(dcxwtzsgh.FASJ).ToString("yyyy-MM-dd HH:mm") : "    年  月  日");
            dic.Add("$发案地点$", dcxwtzsgh.FADD);
            dic.Add("$违法行为$", dcxwtzsgh.WFXW);
            dic.Add("$调查询问时间年$", Convert.ToDateTime(dcxwtzsgh.DCXWSJ).Year.ToString());
            dic.Add("$调查询问时间月$", Convert.ToDateTime(dcxwtzsgh.DCXWSJ).Month.ToString());
            dic.Add("$调查询问时间日$", Convert.ToDateTime(dcxwtzsgh.DCXWSJ).Day.ToString());
            dic.Add("$星期几$", HelpDocument.GetDayOfWeek(
               Convert.ToDateTime(dcxwtzsgh.DCXWSJ).DayOfWeek));
            dic.Add("$上午或下午$", HelpDocument.GetMorningOrAfternoon(
               Convert.ToDateTime(dcxwtzsgh.DCXWSJ).Hour.ToString()));
            dic.Add("$调查询问时间时$", Convert.ToDateTime(dcxwtzsgh.DCXWSJ).Hour.ToString());
            dic.Add("$调查询问地点$", dcxwtzsgh.DCXWDD);
            dic.Add("$联系人$", dcxwtzsgh.LXR);
            dic.Add("$地址$", dcxwtzsgh.DZ);
            dic.Add("$电话$", dcxwtzsgh.DH);
            dic.Add("$发出日期$", dcxwtzsgh.FCRQ != null ? dcxwtzsgh.FCRQ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$当事人签章$", dcxwtzsgh.DSRSJQZ);
            dic.Add("$当事人电话$", dcxwtzsgh.DSRDH);
            dic.Add("$当事人收件日期$", dcxwtzsgh.DSRSJRQ != null ? dcxwtzsgh.DSRSJRQ.Value.ToString("yyyy年MM月dd日") : "    年  月 日");

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);

            if (dcxwtzsgh.DSRSFZM)
            {
                wordUtility.ReplaceAdvancedRang("$当事人身份证明$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$当事人身份证明$", "□", null);
            }

            if (dcxwtzsgh.WTTRBLD)
            {
                wordUtility.ReplaceAdvancedRang("$委托他人办理$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$委托他人办理$", "□", null);
            }

            if (dcxwtzsgh.JSXMPZWJ)
            {
                wordUtility.ReplaceAdvancedRang("$建设项目$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$建设项目$", "□", null);
            }

            if (dcxwtzsgh.JSYDGHXKZ)
            {
                wordUtility.ReplaceAdvancedRang("$建设用地$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$建设用地$", "□", null);
            }

            if (dcxwtzsgh.JSGCGHXKZ)
            {
                wordUtility.ReplaceAdvancedRang("$建设工程$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$建设工程$", "□", null);
            }

            if (dcxwtzsgh.JSYDXKZORJSYDPZS)
            {
                wordUtility.ReplaceAdvancedRang("$建设用地许可证或$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$建设用地许可证或$", "□", null);
            }

            if (dcxwtzsgh.TDSYZFYJ)
            {
                wordUtility.ReplaceAdvancedRang("$土地使用证$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$土地使用证$", "□", null);
            }

            if (dcxwtzsgh.JGFHTYJ)
            {
                wordUtility.ReplaceAdvancedRang("$竣工复核图$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$竣工复核图$", "□", null);
            }

            if (dcxwtzsgh.FCCHCGYJ)
            {
                wordUtility.ReplaceAdvancedRang("$房产测绘$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$房产测绘$", "□", null);
            }

            if (dcxwtzsgh.XGBMYJYJ)
            {
                wordUtility.ReplaceAdvancedRang("$相关部门$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$相关部门$", "□", null);
            }

            if (dcxwtzsgh.YGXYHHT)
            {
                wordUtility.ReplaceAdvancedRang("$有关协议$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$有关协议$", "□", null);
            }

            if (dcxwtzsgh.QT)
            {
                wordUtility.ReplaceAdvancedRang("$其他$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$其他$", "□", null);
            }

            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 返回调查询问通知书（规划,市容）编号
        /// </summary>
        /// <returns></returns>
        public static string GetDCXWTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.DCXWTZS || t.DDID == DocDefinition.DCXWTZSGH).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("DCXWTZS{0:D5}号", ++count);
        }
    }
}
