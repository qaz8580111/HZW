
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZGM.WUA.Windows
{
    public enum IconConfirm
    {
        Success,
        Warning,
        Error
    }

    public class WinFactory
    {
        public static Canvas LeftTopContainer2;//事件地图分布容器

        public static Canvas LeftTopContainer;
        public static Canvas LeftCenterContainer;
        public static Canvas LeftBottomContainer;
        public static Canvas CenterTopContainer;
        public static Canvas CenterCenterContainer;
        public static Canvas CenterBottomContainer;
        public static Canvas RightTopContainer;
        public static Canvas RightCenterContainer;
        public static Canvas RightBottomContainer;
        public static Grid FullContaiener;
       // public static Dictionary<string, ConfirmWin> ConfirmWinList = new Dictionary<string, ConfirmWin>();
        public static Dictionary<string, ContentWin> ContentWinList = new Dictionary<string, ContentWin>();
       // public static Dictionary<string, ContentWinMap> ContentWinMapList = new Dictionary<string, ContentWinMap>();



        /// <summary>
        /// 创建窗体
        /// </summary>
        /// <param name="content">窗口内容</param>
        /// <param name="horizontalAlignment">做对齐</param>
        /// <param name="verticalAlignment">右对齐</param>
        /// <param name="left">左偏移</param>
        /// <param name="top">上偏移</param>
        /// <returns>包含内容的窗体</returns>
        public static ContentWin CreateContentWin(FrameworkElement content, string Title, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, double left, double top, double width, double height, Uri icon = null)
        {
            ContentWin contetnWin = new ContentWin();
            contetnWin.WinID = Guid.NewGuid().ToString();
            contetnWin.Closed += contetnWin_Closed;
            if (icon != null)
            {
                contetnWin.IconSource = icon;
            }
            ContentWinList.Add(contetnWin.WinID, contetnWin);

            #region 添加窗体到容器中
            // 横向判断为主,选择容器
            Canvas container = CenterCenterContainer;
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    switch (verticalAlignment)
                    {
                        case VerticalAlignment.Top:
                            container = LeftTopContainer;
                            break;

                        case VerticalAlignment.Center:
                            container = LeftCenterContainer;
                            break;

                        case VerticalAlignment.Bottom:
                            container = LeftBottomContainer;
                            break;
                    }
                    break;

                case HorizontalAlignment.Center:
                    switch (verticalAlignment)
                    {
                        case VerticalAlignment.Top:
                            container = CenterTopContainer;
                            break;

                        case VerticalAlignment.Center:
                            container = CenterCenterContainer;
                            break;

                        case VerticalAlignment.Bottom:
                            container = CenterBottomContainer;
                            break;
                    }
                    break;

                case HorizontalAlignment.Right:
                    switch (verticalAlignment)
                    {
                        case VerticalAlignment.Top:
                            container = RightTopContainer;
                            break;

                        case VerticalAlignment.Center:
                            container = RightCenterContainer;
                            break;

                        case VerticalAlignment.Bottom:
                            container = RightBottomContainer;
                            break;
                    }
                    break;
            }
            #endregion

            contetnWin.Height = height;
            contetnWin.Width = width;

            contetnWin.WinContainer = container;

            contetnWin.SetContent(content, Title);

            container.Children.Add(contetnWin);

            // 设置偏移坐标
            Canvas.SetLeft(contetnWin, left);
            Canvas.SetTop(contetnWin, top);

            contetnWin.IsOpen = true;

            return contetnWin;
        }

        static void contetnWin_Closed(object sender, EventArgs e)
        {
            try
            {
                ContentWin cw = (ContentWin)sender;
                if (ContentWinList.ContainsKey(cw.WinID))
                {
                    ContentWinList.Remove(cw.WinID);
                }
            }
            catch { }
        }
     

        public static T CreateContent<T>() where T : FrameworkElement, new()
        {
            T t = new T();
            return t;
        }
    }
}
