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
    public partial class KPIPanel : UserControl
    {
        public event EventHandler Clicked;

        public string bg
        {
            get
            {
                return this.LayoutRoot.Background.ToString();
            }
            set
            {

                Color c = UtilityTools.ConvertColorCodeToColor(value);
                this.LayoutRoot.Background = new SolidColorBrush(c);
            }
        }

        public string bgPosition
        {
            get
            {
                return this.positionPanel.Background.ToString();
            }
            set
            {
                Color c = UtilityTools.ConvertColorCodeToColor(value);
                this.positionPanel.Background = new SolidColorBrush(c);
            }
        }

        public string ImgText
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

        public string NumStr
        {
            get
            {
                return this.txtNum.Text;
            }
            set
            {
                this.txtNum.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return this.txtTitle.Text;
            }
            set
            {
                this.txtTitle.Text = value;
            }
        }

        public String Tip
        {
            get
            {
                return ToolTipService.GetToolTip(this.LayoutRoot) as string;
            }
            set
            {
                ToolTipService.SetToolTip(this.LayoutRoot, value);
            }
        }

        public KPIPanel()
        {
            InitializeComponent();
        }

        private void positionPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.Clicked != null)
            {
                this.Clicked(this, null);
            }
        }
    }
}
