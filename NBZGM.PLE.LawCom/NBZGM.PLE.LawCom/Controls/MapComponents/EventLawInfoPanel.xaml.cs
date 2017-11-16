using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Taizhou.PLE.LawCom.Web.Complex;

namespace Taizhou.PLE.LawCom.Controls.MapComponents
{
    public partial class EventLawInfoPanel : UserControl
    {
        private static HtmlWindow htmlWindow = HtmlPage.Window;

        public EventLaw EventLaw { get; set; }

        public EventLawInfoPanel()
        {
            InitializeComponent();
        }

        public EventLawInfoPanel(EventLaw eventLaw)
        {
            InitializeComponent();

            this.EventLaw = eventLaw;

            this.eTitle.Text = eventLaw.EventTitle;

            this.eventTitle.Text = eventLaw.EventTitle;
            this.eventAddress.Text = eventLaw.EventAddress;
            this.eventSource.Text = eventLaw.EventSource;
            this.ssdd.Text = eventLaw.SSDD;
            this.sszd.Text = eventLaw.SSZD;
            this.reportTime.Text = eventLaw.ReportTime.ToString();
            this.reportPerson.Text = eventLaw.ReportPerson;
        }
    }
}
