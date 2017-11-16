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
using System.Windows.Shapes;
using Taizhou.PLE.LawCom.Helpers;
using Taizhou.PLE.LawCom.Web;

namespace Taizhou.PLE.LawCom.Controls.MapComponents
{
    public partial class XZSPInfoPanel : UserControl
    {
        public XZSPInfoPanel()
        {
            InitializeComponent();
        }

        public XZSPInfoPanel(XZSPWFIST xzsp)
        {
            InitializeComponent();

            this.zfzd.Text = xzsp.ZFZDNAME;
            this.createTime.Text = xzsp.CREATEDTIME.ToString();

            this.eTitle.Text = xzsp.ZFZDNAME + " " + UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)xzsp.CREATEDTIME);
        }
    }
}
