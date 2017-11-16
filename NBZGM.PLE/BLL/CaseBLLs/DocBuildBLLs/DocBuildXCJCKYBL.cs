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
        ///现场检查勘验笔录
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildXCJCKYBL(string regionName, string ajbh,
             XCJCKYBL xcjckybl)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "现场检查（勘验）笔录", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();


            dic.Add("$年$", xcjckybl.StartTimeYMD.Split('-')[0]);
            dic.Add("$月$", xcjckybl.StartTimeYMD.Split('-')[1]);
            dic.Add("$日$", xcjckybl.StartTimeYMD.Split('-')[2]);
            dic.Add("$开时$", xcjckybl.StartKCSJ.Split(':')[0]);
            dic.Add("$开分$", xcjckybl.StartKCSJ.Split(':')[1]);
            dic.Add("$结时$", xcjckybl.EndKCSJ.Split(':')[0]);
            dic.Add("$结分$", xcjckybl.EndKCSJ.Split(':')[1]);
            dic.Add("$检查勘验地点$", xcjckybl.JCKYDD);
            dic.Add("$其他见证人$", xcjckybl.QTJZR);
            dic.Add("$单位或住址$", xcjckybl.DWHZZ);
            dic.Add("$检查勘验人1$", GetJCRBYJCRBH(xcjckybl.JCKYR1));
            dic.Add("$检查勘验人2$", GetJCRBYJCRBH(xcjckybl.JCKYR2));
            dic.Add("$记录人$", xcjckybl.JLR);
            dic.Add("$工作单位2$", xcjckybl.GZDW2);
            //单位
            //if (!string.IsNullOrWhiteSpace(xcjckybl.BJCKYRMC))
            if (xcjckybl.BJCDXTyle == "dw")
            {
                dic.Add("$被检查勘验人名称$", xcjckybl.BJCKYRMC);
                dic.Add("$法定代表人$", xcjckybl.FDDBRFZR);
                dic.Add("$被检查人姓名$", "");
                dic.Add("$性别$", "");
                dic.Add("$族$", "");
                dic.Add("$身份证号码$", "");
                dic.Add("$工作单位1$", "");
                dic.Add("$职务或职业$", "");
                dic.Add("$电话$", "");
                dic.Add("$住址$", "");
                dic.Add("$邮编$", "");
                dic.Add("$现场负责人$", xcjckybl.XCFZR);
                dic.Add("$职务$", xcjckybl.ZW);
                dic.Add("$身份证号$", xcjckybl.SFZH);
                dic.Add("$本案关系$", xcjckybl.BAGX);
                dic.Add("$被检查勘验人名称或姓名$", xcjckybl.BJCKYRMC);

            }
            else
            {
                dic.Add("$被检查勘验人名称$", "");
                dic.Add("$法定代表人$", "");
                dic.Add("$被检查人姓名$", xcjckybl.BJCKYRXM);
                dic.Add("$性别$", xcjckybl.XB);
                dic.Add("$族$", xcjckybl.MZ);
                dic.Add("$身份证号码$", xcjckybl.SFZHM);
                dic.Add("$工作单位1$", xcjckybl.GZDW1);
                dic.Add("$职务或职业$", xcjckybl.ZWHZY);
                dic.Add("$电话$", xcjckybl.DH);
                dic.Add("$住址$", xcjckybl.ZZ);
                dic.Add("$邮编$", xcjckybl.YB);
                dic.Add("$现场负责人$", "");
                dic.Add("$职务$", "");
                dic.Add("$身份证号$", "");
                dic.Add("$本案关系$", "");
                dic.Add("$被检查勘验人名称或姓名$", xcjckybl.BJCKYRXM);
            }

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath,
                saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            wordUtility.ReplaceAdvancedRang("$勘测正文内容$", xcjckybl.Content, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
        /// <summary>
        /// 根据文书里面检查人拼接检查人与执法证编号
        /// </summary>
        /// <param name="JCRBH"></param>
        /// <returns></returns>
        public static string GetJCRBYJCRBH(string JCRBH)
        {
            string res = "";
            if (!string.IsNullOrEmpty(JCRBH))
            {
                //1为执法人名称，2为执法证编号，3执法队员标识
                string[] str = JCRBH.Split(',');
                res = str[1] + "，《行政执法证》编号：" + str[2];
            }
            return res;
        }

        /// <summary>
        /// 根据工作流标示返回现场勘验笔录实体
        /// </summary>
        /// <param name="WIID">工作流标识</param>
        /// <returns></returns>
        public static XCJCKYBL GetXCJCCYBL(string WIID)
        {
            PLEEntities db = new PLEEntities();
            XCJCKYBL xcjckybl = new XCJCKYBL();
            DOCINSTANCE docinstance = db.DOCINSTANCES.FirstOrDefault(t => t.WIID == WIID && t.DDID == DocDefinition.XCJCKYBL);
            if (docinstance != null)
            {
                xcjckybl = (XCJCKYBL)Serializer.Deserialize(docinstance.ASSEMBLYNAME, docinstance.TYPENAME, docinstance.VALUE);
            }
            return xcjckybl;
        }
    }
}
