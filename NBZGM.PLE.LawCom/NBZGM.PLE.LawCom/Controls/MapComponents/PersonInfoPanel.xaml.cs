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
using Taizhou.PLE.LawCom.Helpers;
using Taizhou.PLE.LawCom.Web.Complex;

namespace Taizhou.PLE.LawCom.Controls.MapComponents
{
    public partial class PersonInfoPanel : UserControl
    {
        private static HtmlWindow htmlWindow = HtmlPage.Window;

        public Person Person { get; set; }

        public event EventHandler PatrolAreaClicked;
        public event EventHandler PatrolAreaUnClicked;
        public event EventHandler PatrolRouteClicked;
        public event EventHandler PatrolRouteUnClicked;
        public event EventHandler HistoryClicked;

        public PersonInfoPanel()
        {
            InitializeComponent();
        }

        public PersonInfoPanel(Person person)
        {
            InitializeComponent();

            this.Person = person;

            this.pName.Text = person.UserName;
            this.pUnit.Text = person.UnitName;
            this.ptimeSpan.Text = " " + UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)person.PositionTime);

            this.personName.Text = person.UserName;
            this.unit.Text = person.UnitName;
            this.smsNumber.Text = person.SmsNumber;
            this.positionTime.Text = person.PositionTime.ToString();
        }

        private void xcqy_Checked(object sender, EventArgs e)
        {
            if (this.PatrolAreaClicked != null)
            {
                this.PatrolAreaClicked(this.Person, null);
            }
        }

        private void xcqy_UnChecked(object sender, EventArgs e)
        {
            if (this.PatrolAreaUnClicked != null)
            {
                this.PatrolAreaUnClicked(this.Person, null);
            }
        }

        private void xclx_Checked(object sender, EventArgs e)
        {
            if (this.PatrolRouteClicked != null)
            {
                this.PatrolRouteClicked(this.Person, null);
            }
        }

        private void xclx_UnChecked(object sender, EventArgs e)
        {
            if (this.PatrolRouteUnClicked != null)
            {
                this.PatrolRouteUnClicked(this.Person, null);
            }
        }

        private void imgHistory_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.HistoryClicked != null)
            {
                this.HistoryClicked(this.Person, null);
            }
        }
    }
}
