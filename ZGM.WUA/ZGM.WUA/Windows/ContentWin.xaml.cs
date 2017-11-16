using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZGM.WUA.Windows
{
    public partial class ContentWin : UserControl
    {
        /// <summary>
        /// 窗口容器
        /// </summary>
        public Canvas WinContainer;
        public delegate void MsgHandler(object sender, EventArgs e);
        public event MsgHandler Closed;
        public string WinID { get; set; }
        public delegate void closeDialog();

        public int DisplayIndex
        {
            get { return this.DisplayIndex; }
            set { this.DisplayIndex = value; }
        }

        public double width
        {
            get { return this.Container.Width; }
            set { this.Container.Width = value; }
        }

        public double height
        {
            get { return this.Container.Height; }
            set { this.Container.Height = value; }
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public Uri IconSource
        {
            get { return (Uri)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(Uri), typeof(ContentWin), new PropertyMetadata(new Uri("", UriKind.RelativeOrAbsolute),
            (s, e) =>
            {
                ContentWin win = s as ContentWin;
                if (win.TitleIcon as Image != null)
                    (win.TitleIcon as Image).Source = new BitmapImage { UriSource = e.NewValue as Uri };
            }));

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ContentWin), new PropertyMetadata(false,
                (s, e) =>
                {
                    ContentWin win = s as ContentWin;
                    bool isOpen = (bool)e.NewValue;
                    if (isOpen)
                    {
                        // 添加到内容中后运行显示动画
                        if (win.WinContainer != null)
                        {
                            if (!win.WinContainer.Children.Contains(win))
                            {
                                win.WinContainer.Children.Add(win);
                            }
                        }
                        win.OpenBorad.Begin();
                    }
                    else
                    {
                        win.CloseBorad.Begin();
                    }
                }));

        private bool isDraging;
        private Point mousePoint;

        public ContentWin()
        {
            InitializeComponent();
            //this.CloseButton.Click += Close_Click;
        }

        private void TitleContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDraging = true;
            this.mousePoint = e.GetPosition(TitleContainer);
            TitleContainer.CaptureMouse();
        }

        private void TitleContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDraging = false;
            TitleContainer.ReleaseMouseCapture();
        }

        private void TitleContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraging)
            {
                Point curMousePoint = e.GetPosition(TitleContainer);
                double offsetX = curMousePoint.X - this.mousePoint.X;
                double offsetY = curMousePoint.Y - this.mousePoint.Y;

                Canvas.SetLeft(Container, Canvas.GetLeft(Container) + offsetX);



                Canvas.SetTop(Container, Canvas.GetTop(Container) + offsetY);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {          
            this.IsOpen = false;
            if (Closed != null)
            {
                Closed(this, e);
            }
        }


        /// <summary>
        /// 设置窗体内容
        /// </summary>
        /// <param name="content">显示在窗体中的内容</param>
        public void SetContent(FrameworkElement content, string strWinTitle)
        {
            tbWinTitle.Text = strWinTitle;
            ContentBorder.Child = content;
        }

        private void CloseBorad_Completed(object sender, EventArgs e)
        {
            // 关闭动画完成后移除当前对象
            if (this.Parent is Grid || this.Parent is Canvas || this.Parent is StackPanel)
            {
                (this.Parent as Panel).Children.Remove(this);
            }

            if (this.Parent is Border)
            {
                (this.Parent as Border).Child = null;
            }

            // 一处窗体内容
            ContentBorder.Child = null;

            //if (Closed != null)
            //{
            //    Closed(this, e);
            //}
        }
    }
}
