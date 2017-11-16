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

namespace MapCtrl
{
    public partial class MapToggleButton : UserControl
    {
        private Brush checkedBackground = new SolidColorBrush(Color.FromArgb(255, 255, 143, 42));
        private Brush uncheckedBackground = new SolidColorBrush(Color.FromArgb(255, 37, 160, 218));
        private bool isChecked = false;

        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                if (value)
                    this.border.Background = this.checkedBackground;
                else
                    this.border.Background = this.uncheckedBackground;

                this.isChecked = value;
            }
        }

        public string Text
        {
            get { return this.textBlock.Text; }
            set { this.textBlock.Text = value; }
        }

        public double Size
        {
            get
            {
                return this.textBlock.FontSize;
            }
            set
            {
                this.textBlock.FontSize = value;
            }
        }

        public event EventHandler Checked;
        public event EventHandler Unchecked;

        public MapToggleButton()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsChecked)
            {
                this.IsChecked = false;

                if (this.Unchecked != null)
                    this.Unchecked(this, null);
            }
            else
            {
                this.IsChecked = true;

                if (this.Checked != null)
                    this.Checked(this, null);
            }
        }

        public void ToogleChecked(bool state)
        {
            if (!state)
            {
                this.border.Background = this.uncheckedBackground;

                this.IsChecked = false;

                if (this.Unchecked != null)
                    this.Unchecked(this, null);
            }
            else
            {
                this.border.Background = this.checkedBackground;

                this.IsChecked = true;

                if (this.Checked != null)
                    this.Checked(this, null);
            }
        }
    }
}
