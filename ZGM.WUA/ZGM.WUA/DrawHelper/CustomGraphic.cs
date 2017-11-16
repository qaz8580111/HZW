using ESRI.ArcGIS.Client.Geometry;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZGM.WUA;

namespace ZGM.WUA.DrawHelper
{
    public class CustomGraphic : ESRI.ArcGIS.Client.Graphic
    {
        public event EventHandler MapClick;
        public event EventHandler DblClick;
        public event EventHandler PressEvent;
        private DispatcherTimer refreshTimer;
        public string GraphicID { get; set; }

        public CustomGraphic()
        {
            bool IsStartDbClick = false;
            bool IsStartPress = false;
            DateTime dtStartDateTime = DateTime.Now;
            DateTime dtEndTime = DateTime.Now;
            DateTime dtStartDateTime_Press = DateTime.Now;
            DateTime dtEndTime_Press = DateTime.Now;
            //double zoomfactor = 0;
            //Envelope envelope = null;
            refreshTimer = new DispatcherTimer();
            refreshTimer.Interval = new TimeSpan(0, 0, 1);
            #region 双击事件
            refreshTimer.Tick += delegate {
                if (IsStartPress == true)
                {
                    if (((TimeSpan)(DateTime.Now - dtStartDateTime_Press)).TotalSeconds >= 2)
                    {
                        if (PressEvent != null)
                            PressEvent(this, null);

                        IsStartPress = false;
                        refreshTimer.Stop();
                        MainPage.IsStopClick = true;
                    }
                }
            };

            base.MouseLeftButtonDown += delegate {
                IsStartPress = true;
                dtStartDateTime_Press = DateTime.Now;
                refreshTimer.Start();
                
                if (((TimeSpan)(DateTime.Now - dtStartDateTime)).TotalMilliseconds > 500)
                {
                    IsStartDbClick = false;
                }

                if (!IsStartDbClick)
                {
                    IsStartDbClick = true;
                    dtStartDateTime = DateTime.Now;
                    dtEndTime = DateTime.Now;
                }
            
            };
            base.MouseLeftButtonUp += (ds, de) =>
            {
                IsStartPress = false;
                refreshTimer.Stop();
                dtEndTime_Press = DateTime.Now;
                if (((TimeSpan)(dtEndTime_Press - dtStartDateTime_Press)).TotalSeconds >= 2)
                {
                    if (PressEvent != null)
                        PressEvent(ds,de);
                }              
                if (IsStartDbClick)
                {
                    if (dtStartDateTime.CompareTo(dtEndTime) != 0)
                    {
                        if (((TimeSpan)(DateTime.Now - dtStartDateTime)).TotalMilliseconds <= 500)
                        {
                            if (DblClick != null)
                            {
                                DblClick(ds, de);
                                
                            }
                        }
                    }
                    else
                    {
                        dtEndTime = DateTime.Now;
                    }
                    
                }
            };
            #endregion           
        }


        void Map_ExtentChanging(object sender, ESRI.ArcGIS.Client.ExtentEventArgs e)
        {
           //e.NewExtent = e.OldExtent;
            MainPage.Map.Extent = e.OldExtent;
        }
        
    }
}
