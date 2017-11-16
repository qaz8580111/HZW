using ESRI.ArcGIS.Client.Geometry;
//using HZCG.EC.ICS.Com.Configs;
//using HZCG.EC.ICS.Managers;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZGM.WUA.ImgConfig;

namespace ZGM.WUA.Maker.Tips
{
    public partial class SurfaceTips : UserControl
    {
        public SurfaceTips(string tagName) {
            InitializeComponent();
            this.TbName.Text = tagName;
        }
       
    }
}
