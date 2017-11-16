using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections;

namespace Taizhou.PLE.Common
{
    public class WordUtility
    {
        //private object tempFile = null;
        //private object saveWordFile = null;
        //private string savePDFFile = null;
        //private static Word._Document wDoc = null; //word 文档
        //private static Word._Application wApp = null; //word 进程
        //private object missing = System.Reflection.Missing.Value;

        ///// <summary>
        ///// 自动生成 Word 文件工具类
        ///// </summary>
        ///// <param name="tempFile">Word 模版路径</param>
        ///// <param name="saveWordFile">生成的 Word 文件路径</param>
        ///// <param name="savePDFFile">生成的 PDF 文件路径</param>
        //public WordUtility(string tempFile, string saveWordFile, string savePDFFile)
        //{
        //    this.tempFile = tempFile;
        //    this.saveWordFile = saveWordFile;
        //    this.savePDFFile = savePDFFile;

        //    try
        //    {
        //        wApp = new Word.Application();
        //        wApp.Visible = false;

        //        #region 若需要保存 Word 格式的文书,则需使用此段代码

        //        wDoc = wApp.Documents.Add(ref this.tempFile, ref missing,
        //            ref missing, ref missing);
        //        wDoc.Activate();// 当前文档置前

        //        wDoc.SaveAs2(ref this.saveWordFile, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing,
        //            ref missing);

        //        #endregion

        //        #region 如果不需要保存 Word 格式的文书,则需使用此段代码

        //        //object readOnly = false;
        //        //object isVisible = false;

        //        //wDoc = wApp.Documents.Add(ref missing, ref missing,
        //        //    ref missing, ref missing);

        //        //Word._Document openWord = wApp.Documents.Open(ref tempFile,
        //        //    ref missing, ref readOnly, ref missing, ref missing,
        //        //    ref missing, ref missing, ref missing, ref missing,
        //        //    ref missing, ref missing, ref isVisible, ref missing,
        //        //    ref missing, ref missing, ref missing);
        //        //openWord.Select();
        //        //openWord.Sections[1].Range.Copy();

        //        //object start = 0;
        //        //Word.Range newRang = wDoc.Range(ref start, ref start);

        //        //wDoc.Sections[1].Range.PasteAndFormat(
        //        //    Word.WdRecoveryType.wdPasteDefault);
        //        //openWord.Close(ref missing, ref missing, ref missing);

        //        #endregion
        //    }
        //    catch (Exception e)
        //    {
        //        this.DisposeWord();

        //        DateTime errorDateTime = DateTime.Now;
        //        string errorPath = @"C:\WordErrorLogs\";

        //        if (!Directory.Exists(errorPath))
        //        {
        //            Directory.CreateDirectory(errorPath);
        //        }

        //        StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);

        //        errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
        //        errorSW.WriteLine("ExceptionMessage:" + e.Message);
        //        errorSW.WriteLine("ExceptionSource:" + e.Source);
        //        errorSW.WriteLine("文书模版路径:" + this.tempFile);
        //        errorSW.WriteLine("Word 文书路径:" + this.saveWordFile);
        //        errorSW.WriteLine("PDF 文书路径:" + this.savePDFFile);

        //        if (e.InnerException != null)
        //        {
        //            errorSW.WriteLine("InnerExceptionMessage:" + e.InnerException.Message);
        //            errorSW.WriteLine("InnerExceptionSource:" + e.InnerException.Source);
        //        }

        //        errorSW.Close();
        //    }
        //}

        ///// <summary>
        ///// 替换 Word 模版中的多个变量
        ///// </summary>
        ///// <param name="dic"></param>
        //public void ReplaceRangs(Dictionary<string, string> dic)
        //{
        //    foreach (string key in dic.Keys)
        //    {
        //        object objReplaceKey = key;
        //        object objReplaceValue = dic[key];
        //        object replaceArea = Word.WdReplace.wdReplaceAll;
        //        wApp.Selection.Find.ClearFormatting();

        //        try
        //        {
        //            wApp.Selection.Find.Execute(ref objReplaceKey, ref missing,
        //                ref missing, ref missing, ref missing, ref missing, ref missing,
        //                ref missing, ref missing, ref objReplaceValue, ref replaceArea,
        //                ref missing, ref missing, ref missing, ref missing);
        //        }
        //        catch (Exception e)
        //        {
        //            this.DisposeWord();

        //            DateTime errorDateTime = DateTime.Now;
        //            string errorPath = @"C:\WordErrorLogs\";

        //            if (!Directory.Exists(errorPath))
        //            {
        //                Directory.CreateDirectory(errorPath);
        //            }

        //            StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);

        //            errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
        //            errorSW.WriteLine("替换 Word 模版变量出错");
        //            errorSW.WriteLine("替换的 Key:" + objReplaceValue);
        //            errorSW.WriteLine("替换的 Value:" + objReplaceValue);
        //            errorSW.WriteLine("ExceptionMessage:" + e.Message);
        //            errorSW.WriteLine("ExceptionSource:" + e.Source);

        //            if (e.InnerException != null)
        //            {
        //                errorSW.WriteLine("InnerExceptionMessage:" + e.InnerException.Message);
        //                errorSW.WriteLine("InnerExceptionSource:" + e.InnerException.Source);
        //            }
        //            errorSW.Close();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 替换 Word 模版中的变量(可替换超过500个字符的变量，可设置字体样式)
        ///// </summary>
        ///// <param name="replaceKey">要替换的变量</param>
        ///// <param name="replaceValue">替换后的值</param>
        ///// <param name="fontName">字体名称(为空表示不设置字体)</param>
        //public void ReplaceAdvancedRang(string replaceKey,
        //    string replaceValue, string fontName)
        //{
        //    wApp.Options.ReplaceSelection = true;
        //    wApp.Selection.Find.ClearFormatting();
        //    wApp.Selection.Find.Text = replaceKey;
        //    wApp.Selection.Find.Replacement.Text = "";
        //    wApp.Selection.Find.Forward = true;
        //    wApp.Selection.Find.Wrap = Word.WdFindWrap.wdFindContinue;
        //    wApp.Selection.Find.Format = false;
        //    wApp.Selection.Find.MatchCase = false;
        //    wApp.Selection.Find.MatchWholeWord = false;
        //    wApp.Selection.Find.MatchByte = true;
        //    wApp.Selection.Find.MatchWildcards = false;
        //    wApp.Selection.Find.MatchSoundsLike = false;
        //    wApp.Selection.Find.MatchAllWordForms = false;

        //    try
        //    {

        //        wApp.Selection.Find.Execute(ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing);

        //        //设置替换变量的字体
        //        if (!string.IsNullOrWhiteSpace(fontName))
        //        {
        //            wApp.Selection.Font.Name = fontName;
        //        }

        //        wApp.Selection.TypeText(replaceValue);
        //    }
        //    catch (Exception e)
        //    {
        //        this.DisposeWord();

        //        DateTime errorDateTime = DateTime.Now;
        //        string errorPath = @"C:\WordErrorLogs\";

        //        if (!Directory.Exists(errorPath))
        //        {
        //            Directory.CreateDirectory(errorPath);
        //        }

        //        StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);

        //        errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
        //        errorSW.WriteLine("替换 Word 模版变量出错");
        //        errorSW.WriteLine("替换的 Key:" + replaceKey);
        //        errorSW.WriteLine("替换的 Value:" + replaceValue);
        //        errorSW.WriteLine("ExceptionMessage:" + e.Message);
        //        errorSW.WriteLine("ExceptionSource:" + e.Source);
        //        errorSW.WriteLine("ExceptionMessage:" + e.Message);
        //        errorSW.WriteLine("ExceptionSource:" + e.Source);

        //        if (e.InnerException != null)
        //        {
        //            errorSW.WriteLine("InnerExceptionMessage:" + e.InnerException.Message);
        //            errorSW.WriteLine("InnerExceptionSource:" + e.InnerException.Source);
        //        }

        //        errorSW.Close();
        //    }
        //}

        ///// <summary>
        ///// 替换 Word 页脚中的所有变量
        ///// </summary>
        ///// <param name="dic"></param>
        //public void RepalceFooterRanges(Dictionary<string, string> dic)
        //{
        //    foreach (string key in dic.Keys)
        //    {
        //        this.ReplaceFooterRange(key, dic[key]);
        //    }
        //}

        ///// <summary>
        ///// 替换 Word 页脚中的变量
        ///// </summary>
        ///// <param name="replaceKey"></param>
        ///// <param name="replaceValue"></param>
        //public void ReplaceFooterRange(string replaceKey, string replaceValue)
        //{
        //    Word.Range footerRange =
        //        wDoc.StoryRanges[Word.WdStoryType.wdPrimaryFooterStory];

        //    object replaceAll = Word.WdReplace.wdReplaceAll;

        //    footerRange.Find.ClearFormatting();
        //    footerRange.Find.Text = replaceKey;
        //    footerRange.Find.Replacement.ClearFormatting();
        //    footerRange.Find.Replacement.Text = replaceValue;

        //    try
        //    {
        //        footerRange.Find.Execute(ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref replaceAll, ref missing,
        //            ref missing, ref missing, ref missing);
        //    }
        //    catch (Exception e)
        //    {
        //        this.DisposeWord();

        //        DateTime errorDateTime = DateTime.Now;
        //        string errorPath = @"C:\WordErrorLogs\";

        //        if (!Directory.Exists(errorPath))
        //        {
        //            Directory.CreateDirectory(errorPath);
        //        }

        //        StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);

        //        errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
        //        errorSW.WriteLine("替换 Word 页脚变量出错");
        //        errorSW.WriteLine("替换的 Key:" + replaceKey);
        //        errorSW.WriteLine("替换的 Value:" + replaceValue);
        //        errorSW.WriteLine("ExceptionMessage:" + e.Message);
        //        errorSW.WriteLine("ExceptionSource:" + e.Source);
        //        errorSW.WriteLine("ExceptionMessage:" + e.Message);
        //        errorSW.WriteLine("ExceptionSource:" + e.Source);

        //        if (e.InnerException != null)
        //        {
        //            errorSW.WriteLine("InnerExceptionMessage:" + e.InnerException.Message);
        //            errorSW.WriteLine("InnerExceptionSource:" + e.InnerException.Source);
        //        }

        //        errorSW.Close();
        //    }
        //}

        ///// <summary>
        ///// 向 Word 文件中添加多张图片
        ///// </summary>
        ///// <param name="dic">图片变量和图片路径对应列表</param>
        //public void AddPictures(Dictionary<string, string> dic)
        //{
        //    foreach (string key in dic.Keys)
        //    {
        //        this.AddPicture(key, dic[key]);
        //    }
        //}


        ///// <summary>
        ///// 向 Word 文件中添加单张图片
        ///// </summary>
        ///// <param name="variableName">word 模版中的变量</param>
        ///// <param name="fileFullName">图片文件的全路径名</param>
        //public void AddPicture(string variableName, string fileFullName)
        //{
        //    //object replaceAll = Word.WdReplace.wdReplaceAll;
        //    wApp.Options.ReplaceSelection = true;
        //    wApp.Selection.Find.Forward = true;
        //    wApp.Selection.Find.Wrap = Word.WdFindWrap.wdFindContinue;
        //    wApp.Selection.Find.Format = false;
        //    wApp.Selection.Find.MatchCase = false;
        //    wApp.Selection.Find.MatchWholeWord = false;
        //    wApp.Selection.Find.MatchByte = true;
        //    wApp.Selection.Find.MatchWildcards = false;
        //    wApp.Selection.Find.MatchSoundsLike = false;
        //    wApp.Selection.Find.MatchAllWordForms = false;
        //    wApp.Selection.Find.ClearFormatting();
        //    wApp.Selection.Find.Text = variableName;
        //    wApp.Selection.Find.Replacement.Text = "";

        //    wApp.Selection.Find.Execute(ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing);
        //    wApp.Selection.InlineShapes.AddPicture(fileFullName,
        //         ref missing, ref missing, ref missing);
        //}


        ///// <summary>
        ///// 向 Word 文件中添加单张图片
        ///// </summary>
        ///// <param name="variableName">word 模版中的变量</param>
        ///// <param name="fileFullName">图片文件的全路径名</param>
        ///// <param name="width">要替换图片宽</param>
        ///// <param name="height">要替换图片的高</param>
        //public void AddPicture(string variableName, string fileFullName, int width, int height)
        //{
        //    //object replaceAll = Word.WdReplace.wdReplaceAll;
        //    wApp.Options.ReplaceSelection = true;
        //    wApp.Selection.Find.Forward = true;
        //    wApp.Selection.Find.Wrap = Word.WdFindWrap.wdFindContinue;
        //    wApp.Selection.Find.Format = false;
        //    wApp.Selection.Find.MatchCase = false;
        //    wApp.Selection.Find.MatchWholeWord = false;
        //    wApp.Selection.Find.MatchByte = true;
        //    wApp.Selection.Find.MatchWildcards = false;
        //    wApp.Selection.Find.MatchSoundsLike = false;
        //    wApp.Selection.Find.MatchAllWordForms = false;
        //    wApp.Selection.Find.ClearFormatting();
        //    wApp.Selection.Find.Text = variableName;
        //    wApp.Selection.Find.Replacement.Text = "";

        //    wApp.Selection.Find.Execute(ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing);
        //    Word.InlineShape rang = wApp.Selection.InlineShapes.AddPicture(fileFullName,
        //         ref missing, ref missing, ref missing);
        //    rang.Width = width;
        //    rang.Height = height;

        //}

        ///// <summary>
        ///// 向 Word 文件中添加单张图片（现场照片图片证据）
        ///// </summary>
        ///// <param name="variableName">word 模版中的变量</param>
        ///// <param name="fileFullName">图片文件的全路径名</param>
        ///// <param name="width">图片宽度</param>
        ///// <param name="height">图片高度</param>
        //public void AddPictureXCZPTPZJ(string variableName, string fileFullName
        //    , int width, int height)
        //{
        //    //object replaceAll = Word.WdReplace.wdReplaceAll;
        //    wApp.Options.ReplaceSelection = true;
        //    wApp.Selection.Find.Forward = true;
        //    wApp.Selection.Find.Wrap = Word.WdFindWrap.wdFindContinue;
        //    wApp.Selection.Find.Format = false;
        //    wApp.Selection.Find.MatchCase = false;
        //    wApp.Selection.Find.MatchWholeWord = false;
        //    wApp.Selection.Find.MatchByte = true;
        //    wApp.Selection.Find.MatchWildcards = false;
        //    wApp.Selection.Find.MatchSoundsLike = false;
        //    wApp.Selection.Find.MatchAllWordForms = false;
        //    wApp.Selection.Find.ClearFormatting();
        //    wApp.Selection.Find.Text = variableName;
        //    wApp.Selection.Find.Replacement.Text = "";

        //    wApp.Selection.Find.Execute(ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing);
        //    Word.InlineShape rang = wApp.Selection.InlineShapes.AddPicture(fileFullName,
        //         ref missing, ref missing, ref missing);
        //    rang.Width = width;
        //    rang.Height = height;
        //}

        ///// <summary>
        ///// 将 Word 文件另存为 PDF 文件
        ///// </summary>
        //public void ExportPDF()
        //{
        //    Word.WdExportFormat exportFormat =
        //        Word.WdExportFormat.wdExportFormatPDF;

        //    wDoc.ExportAsFixedFormat(this.savePDFFile, exportFormat);

        //}

        //public void DisposeWord()
        //{
        //    //关闭当前打开的 Word 文档
        //    if (wDoc != null)
        //    {
        //        object saveOption = Word.WdSaveOptions.wdSaveChanges;
        //        wDoc.Close(ref saveOption, ref missing, ref missing);
        //        wDoc = null;
        //    }

        //    //关闭 Word 进程
        //    if (wApp != null)
        //    {
        //        object saveOption = Word.WdSaveOptions.wdDoNotSaveChanges;
        //        wApp.Quit(ref saveOption, ref missing, ref missing);
        //    }

        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //}

        ///// <summary>
        ///// 往 Word 文件中的 Table 插入数据
        ///// </summary>
        ///// <param name="tableIndex">Word 文档中 Table 的下标（从 1 开始）</param>
        ///// <param name="dtSouce">要插入的数据源（DataTable 类型）</param>
        //public void InsertTable(int tableIndex, DataTable dtSouce)
        //{
        //    object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;
        //    Word.Table table = wDoc.Tables[tableIndex];
        //    object miss = System.Reflection.Missing.Value;

        //    for (int i = 1; i <= dtSouce.Rows.Count; i++)
        //    {
        //        table.Rows.Add(ref miss);
        //        for (int j = 1; j <= dtSouce.Columns.Count; j++)
        //        {
        //            wDoc.Content.Tables[tableIndex].Cell(i + 1, j).Range.Text = dtSouce.Rows[i - 1][j - 1].ToString();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 往 Word 文件中的 Table 插入数据
        ///// </summary>
        ///// <param name="tableIndex">Word 文档中 Table 的下标（从 1 开始）</param>
        ///// <param name="objectList">要插入的数据源（List 集合类型）</param>
        //public void InsertTable(int tableIndex, IList objectList)
        //{
        //    DataTable dtSouce = ToDataTable(objectList);
        //    object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;
        //    Word.Table table = wDoc.Tables[tableIndex];
        //    object miss = System.Reflection.Missing.Value;

        //    for (int i = 1; i <= dtSouce.Rows.Count; i++)
        //    {
        //        table.Rows.Add(ref miss);
        //        for (int j = 1; j <= dtSouce.Columns.Count; j++)
        //        {
        //            wDoc.Content.Tables[tableIndex].Cell(i + 1, j).Range.Text = dtSouce.Rows[i - 1][j - 1].ToString();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 把 List 集合转化为 DataTable 对象
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //private static DataTable ToDataTable(IList list)
        //{
        //    DataTable result = new DataTable();

        //    if (list.Count <= 0)
        //    {
        //        return result;
        //    }

        //    PropertyInfo[] propertys = list[0].GetType().GetProperties();
        //    foreach (PropertyInfo pi in propertys)
        //    {
        //        result.Columns.Add(pi.Name, pi.PropertyType);
        //    }

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        ArrayList tempList = new ArrayList();
        //        foreach (PropertyInfo pi in propertys)
        //        {
        //            object obj = pi.GetValue(list[i], null);
        //            tempList.Add(obj);
        //        }
        //        object[] array = tempList.ToArray();
        //        result.LoadDataRow(array, true);
        //    }
        //    return result;
        //}
    }
}