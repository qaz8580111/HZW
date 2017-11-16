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
using Taizhou.PLE.LawCom.Helpers;

namespace Taizhou.PLE.LawCom.Controls
{
    public partial class ObjImageToggle : UserControl
    {
        public const string RootPath = "/Taizhou.PLE.LawCom;component/";

        #region 基本属性

        private string unCheckedUrl;
        private string checkedUrl;

        public string UnCheckedUrl
        {
            get
            {
                return this.unCheckedUrl;
            }
            set
            {
                value = RootPath + value;
                this.unCheckedUrl = value;
            }
        }

        public string CheckedUrl
        {
            get
            {
                return this.checkedUrl;
            }
            set
            {
                value = RootPath + value;
                this.checkedUrl = value;
            }
        }

        public string Imgtxt
        {
            get
            {
                return this.txtImg.Text;
            }
            set
            {
                this.txtImg.Text = value;
            }
        }

        public string UnCheckedToolTip
        {
            get;
            set;
        }

        public string CheckedToolTip
        {
            get;
            set;
        }

        #endregion

        #region 依赖属性

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ObjImageToggle), null);

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        #endregion

        #region 事件属性

        public event EventHandler Checked;
        public event EventHandler UnChecked;

        #endregion

        #region 切换事件

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            this.SetValue(ToolTipService.ToolTipProperty, CheckedToolTip);
            //this.imgBackground.Source = new BitmapImage(new Uri(this.CheckedUrl, UriKind.RelativeOrAbsolute));
            Color c = UtilityTools.ConvertColorCodeToColor("#FF8F2A");
            this.txtImg.Foreground = new SolidColorBrush(c);

            if (this.Checked != null)
            {
                this.Checked(this, null);
            }
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            this.SetValue(ToolTipService.ToolTipProperty, UnCheckedToolTip);
            //this.imgBackground.Source = new BitmapImage(new Uri(this.unCheckedUrl, UriKind.RelativeOrAbsolute));
            this.txtImg.Foreground = new SolidColorBrush(Colors.Gray);

            if (this.UnChecked != null)
            {
                this.UnChecked(this, null);
            }
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            //this.imgBackground.Stretch = this.Stretch;
            this.SetValue(ToolTipService.ToolTipProperty, UnCheckedToolTip);
            //this.imgBackground.Source = new BitmapImage(new Uri(this.unCheckedUrl, UriKind.RelativeOrAbsolute));
        }

        #endregion
        public ObjImageToggle()
        {
            InitializeComponent();
        }
    }
}
