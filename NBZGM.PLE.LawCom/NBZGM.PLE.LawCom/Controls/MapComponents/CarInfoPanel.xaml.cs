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
    public partial class CarInfoPanel : UserControl
    {
        private static HtmlWindow htmlWindow = HtmlPage.Window;

        public Car Car { get; set; }

        public event EventHandler PatrolAreaClicked;
        public event EventHandler PatrolAreaUnClicked;
        public event EventHandler PatrolRouteClicked;
        public event EventHandler PatrolRouteUnClicked;
        public event EventHandler HistoryClicked;

        public CarInfoPanel()
        {
            InitializeComponent();
        }

        public CarInfoPanel(Car car)
        {
            InitializeComponent();

            this.Car = car;

            this.carName.Text = car.CarNumber;
            this.carTimeSpan.Text = " "+UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)car.PositionDateTime);

            this.carNumber.Text = car.CarNumber;
            this.unit.Text = car.UnitName;
            this.positionTime.Text = car.PositionDateTime.ToString();
        }

        private void xcqy_Checked(object sender, EventArgs e)
        {
            if (this.PatrolAreaClicked != null)
            {
                this.PatrolAreaClicked(this.Car, null);
            }
        }

        private void xcqy_UnChecked(object sender, EventArgs e)
        {
            if (this.PatrolAreaUnClicked != null)
            {
                this.PatrolAreaUnClicked(this.Car, null);
            }
        }

        private void xclx_Checked(object sender, EventArgs e)
        {
            if (this.PatrolRouteClicked != null)
            {
                this.PatrolRouteClicked(this.Car, null);
            }
        }

        private void xclx_UnChecked(object sender, EventArgs e)
        {
            if (this.PatrolRouteUnClicked != null)
            {
                this.PatrolRouteUnClicked(this.Car, null);
            }
        }

        private void imgHistory_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.HistoryClicked != null)
            {
                this.HistoryClicked(this.Car, null);
            }
        }
    }
}
