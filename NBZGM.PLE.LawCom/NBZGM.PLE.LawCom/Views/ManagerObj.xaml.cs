using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Taizhou.PLE.LawCom.Helpers;
using Taizhou.PLE.LawCom.Web;
using Taizhou.PLE.LawCom.Web.Complex;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Taizhou.PLE.LawCom.Views
{
    public partial class ManagerObj : UserControl
    {
        public class MenuConfig
        {
            public string Header { get; set; }
            public string IconUri { get; set; }
            public string Icon { get; set; }
        }

        private NEWPLEDomainContext pledb = null;

        private bool isPosByPersonDoubleClicked = true;
        private bool isPosByCarDoubleClicked = true;
        private bool isPosByEventLawDoubleClicked = true;

        public event EventHandler PosClicked;
        public event EventHandler HistoryLayerClicked;
        public event EventHandler AllPosClicked;

        public RadContextMenu CarContextMenu { get; set; }
        public RadContextMenu PersonContextMenu { get; set; }
        public RadContextMenu EventLawContextMenu { get; set; }

        private List<Unit> Units { get; set; }
        private List<Unit> Units4LawCars { get; set; }
        private List<Unit> Units4LawEvents { get; set; }
        private List<Car> Cars { get; set; }
        private List<Person> Persons { get; set; }
        private List<EventLaw> EventLaws { get; set; }


        #region 菜单
        private List<MenuConfig> lstOtherMenu = new List<MenuConfig>()
        {
            new MenuConfig(){ 
                Header="全部定位" , 
                IconUri="/NBZGM.PLE.LawCom;component/Images/position.png", 
                Icon=  "i"}
        };

        private List<MenuConfig> lstCarMenu = new List<MenuConfig>()
        {
            new MenuConfig(){ 
                Header="车辆定位" ,
                IconUri="/NBZGM.PLE.LawCom;component/Images/position.png",
                Icon = "i" },
            new MenuConfig(){ Header="轨迹回放" ,
                IconUri="/NBZGM.PLE.LawCom;component/Images/history.png",
                Icon = "l" },
        };

        private List<MenuConfig> lstPersonMenu = new List<MenuConfig>()
        {
            new MenuConfig(){ 
                Header="人员定位" ,
                IconUri="/NBZGM.PLE.LawCom;component/Images/position.png",
                Icon = "i" },
            new MenuConfig(){ Header="轨迹回放" ,
                IconUri="/NBZGM.PLE.LawCom;component/Images/history.png",
                Icon = "l" }
        };

        private List<MenuConfig> lstEventLawMenu = new List<MenuConfig>()
        {
            new MenuConfig(){
                Header="事件定位",
                IconUri="/NBZGM.PLE.LawCom;component/Images/position.png",
                Icon="i"
            }
        };

        #endregion

        public ManagerObj()
        {
            InitializeComponent();

            pledb = new NEWPLEDomainContext();

            pledb.Load(pledb.GetAllUnitsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                //只取“台州市城市管理行政执法局”的部门
                this.Units = t.Entities.Where(s => s.ParentID == 10000).OrderBy(a => a.SeqNo).ToList();
            }, null);

            pledb.Load(pledb.GetAllUnits4LawCarsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                this.Units4LawCars = t.Entities.OrderBy(a => a.SeqNo).ToList();
            }, null);

            pledb.Load(pledb.GetAllUnits4EventLawsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                this.Units4LawEvents = t.Entities.OrderBy(a => a.SeqNo).ToList();
            }, null);

            pledb.Load(pledb.GetAllCarsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                this.Cars = t.Entities.ToList();
            }, null);

            pledb.Load(pledb.GetAllPersonsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                //只取“台州市城市管理行政执法局”的人员
                this.Persons = t.Entities.ToList();
            }, null);

            pledb.Load(pledb.GetAllEventLawsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                this.EventLaws = t.Entities.ToList();
            }, null);

            this.LoadedCars();
            this.LoadedPersons();
            this.LoadedEvents();
        }

        //private void PersonToMKC() 
        //{
        //    foreach (var person in this.Persons)
        //    {
        //        if (person.Lon != null && person.Lat != null)
        //        {
        //            double x, y;
        //            UtilityTools.WGS84ToMercator(Convert.ToDouble(person.Lon), Convert.ToDouble(person.Lat), out x, out y);
        //            person.Lon = x;
        //            person.Lat = y;
        //        }
        //    }
        //}

        /// <summary>
        /// 加载车辆
        /// </summary>
        public void LoadedCars()
        {
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //timer.Tick += (s, e) =>
            //{
            //    if ((this.Units4LawCars == null || this.Units4LawCars.Count < 1) ||
            //        (this.Cars == null || this.Cars.Count < 1))
            //        return;
            //    timer.Stop();

            //    #region 加载车辆树
            //    this.rtvCar.ItemsSource = this.BindingUnitItem(PositionType.Car,
            //        this.Units4LawCars, null, rtviUnit =>
            //        {
            //            Unit unit = (rtviUnit.DataContext as Unit);

            //            this.BindOtherMenuClicked(rtviUnit, PositionType.Car);
            //            this.BindingCarItem(unit, rtviUnit, rtvicar =>
            //            {
            //                this.CarContextMenu = this.GenerateContextMenu(this.lstCarMenu);
            //                this.CarContextMenu.ItemClick += ContextMenuClick;
            //                rtvicar.SetValue(RadContextMenu.ContextMenuProperty, this.CarContextMenu);
            //            });
            //        });
            //    #endregion

            //    this.busyCar.Visibility = Visibility.Collapsed;
            //};
            //timer.Start();
        }

        /// <summary>
        /// 加载人员
        /// </summary>
        public void LoadedPersons()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += (s, e) =>
            {
                if ((this.Units == null || this.Units.Count < 1) ||
                    (this.Persons == null || this.Persons.Count < 1))
                    return;
                timer.Stop();

                #region 加载人员树
                this.rtvPerson.ItemsSource = this.BindingUnitItem(PositionType.Person,
                    this.Units, null, rtviUnit =>
                    {
                        Unit unit = (rtviUnit.DataContext as Unit);
                        this.BindOtherMenuClicked(rtviUnit, PositionType.Person);
                        this.BindingPersonItem(unit, rtviUnit, rtviperson =>
                        {
                            this.PersonContextMenu = this.GenerateContextMenu(this.lstPersonMenu);
                            this.PersonContextMenu.ItemClick += ContextMenuClick;
                            rtviperson.SetValue(RadContextMenu.ContextMenuProperty, this.PersonContextMenu);
                        });
                    });
                #endregion

                this.busyPerson.Visibility = Visibility.Collapsed;
            };
            timer.Start();
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        public void LoadedEvents()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += (s, e) =>
            {
                if ((this.Units4LawEvents == null || this.Units4LawEvents.Count < 1) ||
                    (this.EventLaws == null || this.EventLaws.Count < 1))
                    return;
                timer.Stop();

                this.rtvEvent.ItemsSource = this.BindingUnitItem4Events(this.Units4LawEvents, null, rtviUnit =>
                {
                    Unit unit = (rtviUnit.DataContext as Unit);
                    this.BindOtherMenuClicked(rtviUnit, PositionType.EventLaw);
                    this.BindingEventLawItem(unit, rtviUnit, rtvieventLaw =>
                    {
                        this.EventLawContextMenu = this.GenerateContextMenu(this.lstEventLawMenu);
                        this.EventLawContextMenu.ItemClick += ContextMenuClick;
                        rtvieventLaw.SetValue(RadContextMenu.ContextMenuProperty, this.EventLawContextMenu);
                    });
                });

                this.busyEvent.Visibility = Visibility.Collapsed;
            };
            timer.Start();
        }

        private void ContextMenuClick(object sender, RadRoutedEventArgs e)
        {
            RadContextMenu radContextMenu = sender as RadContextMenu;
            RadTreeViewItem rtvi = radContextMenu.UIElement as RadTreeViewItem;
            string header = (e.OriginalSource as RadMenuItem).Header as string;

            if (header == "车辆定位" || header == "人员定位" || header == "事件定位")
            {
                if (this.PosClicked != null)
                {
                    this.PosClicked(rtvi.DataContext, null);
                    rtvi.IsSelected = true;
                }
            }
            else if (header == "轨迹回放")
            {
                if (this.HistoryLayerClicked != null)
                {
                    this.HistoryLayerClicked(rtvi.DataContext, null);
                }
            }
        }

        #region 绑定列表
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <param name="positionType">定位类型</param>
        /// <param name="units">单位集合</param>
        /// <param name="parent">父级节点</param>
        /// <param name="callBack">回调函数</param>
        /// <returns></returns>
        public List<RadTreeViewItem> BindingUnitItem(PositionType? positionType,
            List<Unit> units, RadTreeViewItem parent, Action<RadTreeViewItem> callBack)
        {
            List<Unit> latestUnits = units;
            if (parent != null)
            {
                Unit unit = (parent.DataContext as Unit);
                latestUnits = latestUnits.Where(t => t.ParentID == unit.ID).ToList();
            }

            if (latestUnits.Count < 1 && callBack != null)
            {
                callBack(parent);
                return null;
            }

            List<RadTreeViewItem> lst = new List<RadTreeViewItem>();
            foreach (var item in latestUnits)
            {
                string count = null;
                switch (positionType)
                {
                    case PositionType.Car:
                        int online = 0;
                        List<Car> cars = this.Cars.Where(t => t.UnitID == item.ID).ToList();
                        foreach (Car car in cars)
                        {
                            //判断是否有定位信息
                            if (car.X.HasValue && car.Y.HasValue)
                            {
                                TimeSpan tsT = DateTime.Now - Convert.ToDateTime(car.PositionDateTime);
                                if (tsT.Days < 1)
                                    online++;
                            }
                        }

                        count = online.ToString();
                        count += "/";
                        count += cars.Count.ToString();
                        break;
                    case PositionType.Person:
                        online = 0;
                        List<Person> persons = this.Persons.Where(t => t.UnitID == item.ID).ToList();
                        foreach (Person person in persons)
                        {
                            if (person.Lon.HasValue && person.Lat.HasValue)
                            {
                                TimeSpan tsT = DateTime.Now - Convert.ToDateTime(person.PositionTime);
                                if (tsT.Days < 1)
                                    online++;
                            }

                            count = online.ToString();
                            count += "/";
                            count += persons.Count.ToString();
                        }
                        break;
                    case PositionType.EventLaw:
                        online = 0;
                        List<EventLaw> eventLaws = this.EventLaws.Where(t => t.SSZD == item.Name).ToList();
                        break;
                }

                RadTreeViewItem rtv = new RadTreeViewItem()
                {
                    Header = item.AbbrName + "(" + count + ")",
                    //Header = item.Name + "(" + count + ")",//全名显示
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    DataContext = item
                };

                if (parent != null)
                {
                    parent.Items.Add(rtv);
                }

                lst.Add(rtv);
                this.BindingUnitItem(null, units, rtv, callBack);
            }

            if (lst.Count == 1)
            {
                lst[0].IsExpanded = true;
            }

            return lst;
        }

        /// <summary>
        /// 绑定事件列表
        /// </summary>
        /// <param name="units"></param>
        /// <param name="parent"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public List<RadTreeViewItem> BindingUnitItem4Events(List<Unit> units, RadTreeViewItem parent, Action<RadTreeViewItem> callBack)
        {
            List<Unit> latestUnits = units;

            if (parent != null)
            {
                Unit unit = (parent.DataContext as Unit);
                latestUnits = latestUnits.Where(t => t.ParentID == unit.ID).ToList();
            }

            if (latestUnits.Count < 1 && callBack != null)
            {
                callBack(parent);
                return null;
            }

            List<RadTreeViewItem> lst = new List<RadTreeViewItem>();

            foreach (var item in latestUnits)
            {
                string count = null;
                int hasPos = 0;
                List<EventLaw> eventLaws = this.EventLaws.Where(t => t.SSZD == item.Name).ToList();
                foreach (EventLaw eventLaw in eventLaws)
                {
                    if (!string.IsNullOrWhiteSpace(eventLaw.Geometry))
                        hasPos++;
                }

                count = hasPos.ToString();
                count += "/";
                count += eventLaws.Count.ToString();

                RadTreeViewItem rtv = new RadTreeViewItem()
                {
                    Header = item.AbbrName + "(" + count + ")",
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    DataContext = item
                };

                if (parent != null)
                {
                    parent.Items.Add(rtv);
                }

                lst.Add(rtv);
                this.BindingUnitItem4Events(units, rtv, callBack);
            }

            if (lst.Count == 1)
            {
                lst[0].IsExpanded = true;
            }

            return lst;
        }
        #endregion

        #region 绑定数据
        #region 绑定车辆

        private class CarItem
        {
            public Image Image { get; set; }
            public TextBlock txtImg { get; set; }
            public Car car { get; set; }
        }
        List<CarItem> carItems = new List<CarItem>();
        public void BindingCarItem(Unit unit, RadTreeViewItem rtvEntity
            , Action<RadTreeViewItem> callBack = null)
        {
            if (this.Cars == null || this.Cars.Count < 1)
                return;

            List<Car> carResults = this.Cars.Where(s => s.UnitID == unit.ID).ToList();

            foreach (var item in carResults)
            {
                Image img = new Image();
                img.Stretch = Stretch.Uniform;
                img.Width = 16;
                TextBlock txt = new TextBlock();
                txt.FontFamily = new FontFamily("/NBZGM.PLE.LawCom;component/Fonts/icomoon.ttf#icomoon");
                txt.FontSize = 18;
                txt.Text = "c";
                //判断是否在线
                if (item.X != null && item.Y != null)
                {
                    TimeSpan tsT = DateTime.Now - Convert.ToDateTime(item.PositionDateTime);
                    if (tsT.Days < 1)
                    {
                        img.Source = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/MapPosition/Normal/poi_truck_east.png", UriKind.RelativeOrAbsolute));
                        Color c = UtilityTools.ConvertColorCodeToColor("#5497FF");
                        txt.Foreground = new SolidColorBrush(c);
                    }
                    else
                    {
                        img.Source = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/MapPosition/Offline/poi_truck_east.png", UriKind.RelativeOrAbsolute));
                        Color c = UtilityTools.ConvertColorCodeToColor("#B6B1B1");
                        txt.Foreground = new SolidColorBrush(c);
                    }
                }
                else
                {
                    img.Source = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/MapPosition/Offline/poi_truck_east.png", UriKind.RelativeOrAbsolute));
                    Color c = UtilityTools.ConvertColorCodeToColor("#B6B1B1");
                    txt.Foreground = new SolidColorBrush(c);
                }

                carItems.Add(new CarItem()
                {
                    Image = img,
                    txtImg = txt,
                    car = item
                });
                TextBlock tb = new TextBlock
                {
                    Text = item.CarNumber,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    Margin = new Thickness { Left = 5 }
                };
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(txt);
                sp.Children.Add(tb);

                RadTreeViewItem rtv = new RadTreeViewItem()
                {
                    Header = sp,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    DataContext = item,
                };

                if (this.isPosByCarDoubleClicked)
                {
                    rtv.DoubleClick += (s, e) =>
                    {
                        if (this.PosClicked != null)
                        {
                            this.PosClicked(item, null);
                        }
                    };
                }

                rtvEntity.Items.Add(rtv);

                if (callBack != null)
                    callBack(rtv);
            }
        }
        #endregion
        #region 绑定人员
        private class PersonItem
        {
            public Image Image { get; set; }
            public TextBlock txtImg { get; set; }
            public Person person { get; set; }
        }
        List<PersonItem> personItems = new List<PersonItem>();

        public void BindingPersonItem(Unit unit, RadTreeViewItem rtventity,
            Action<RadTreeViewItem> callBack = null)
        {
            if (this.Persons == null || this.Persons.Count < 1)
                return;

            List<Person> personResults = this.Persons.Where(s => s.UnitID == unit.ID).ToList();

            foreach (var item in personResults)
            {
                TextBlock txt = new TextBlock()
                {
                    FontFamily = new FontFamily("/NBZGM.PLE.LawCom;component/Fonts/icomoon.ttf#icomoon"),
                    FontSize = 18,
                    Text = "u"
                };
                //判断是否在线
                if (item.Lon.HasValue && item.Lat.HasValue)
                {
                    TimeSpan tsT = DateTime.Now - Convert.ToDateTime(item.PositionTime);
                    if (tsT.Days < 1)
                    {
                        Color c = UtilityTools.ConvertColorCodeToColor("#5497FF");
                        txt.Foreground = new SolidColorBrush(c);
                    }
                    else
                    {
                        Color c = UtilityTools.ConvertColorCodeToColor("#B6B1B1");
                        txt.Foreground = new SolidColorBrush(c);
                    }
                }
                else
                {
                    Color c = UtilityTools.ConvertColorCodeToColor("#B6B1B1");
                    txt.Foreground = new SolidColorBrush(c);
                }
                personItems.Add(new PersonItem()
                {
                    txtImg = txt,
                    person = item
                });
                TextBlock tb = new TextBlock
                {
                    Text = item.UserName,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    Margin = new Thickness { Left = 5 }
                };
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(txt);
                sp.Children.Add(tb);

                RadTreeViewItem rtv = new RadTreeViewItem()
                {
                    Header = sp,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    DataContext = item,
                };

                if (this.isPosByPersonDoubleClicked)
                {
                    rtv.DoubleClick += (s, e) =>
                    {
                        if (this.PosClicked != null)
                        {
                            this.PosClicked(item, null);
                        }
                    };
                }

                rtventity.Items.Add(rtv);

                if (callBack != null)
                    callBack(rtv);
            }
        }
        #endregion
        #region 绑定事件
        private class EventLawItem
        {
            public Image Image { get; set; }
            public TextBlock txtImg { get; set; }
            public EventLaw eventLaw { get; set; }
        }
        List<EventLawItem> eventLawItems = new List<EventLawItem>();

        public void BindingEventLawItem(Unit unit, RadTreeViewItem rtventity,
            Action<RadTreeViewItem> callBack = null)
        {
            if (this.EventLaws == null || this.EventLaws.Count < 1)
                return;

            List<EventLaw> eventLawResults = this.EventLaws.Where(s => s.SSZD == unit.Name).ToList();

            foreach (var item in eventLawResults)
            {
                Image img = new Image();
                img.Stretch = Stretch.Uniform;
                img.Width = 16;
                TextBlock txt = new TextBlock()
                {
                    FontFamily = new FontFamily("/NBZGM.PLE.LawCom;component/Fonts/icomoon.ttf#icomoon"),
                    FontSize = 18,
                    Text = "v"
                };
                //判断是否有定位
                if (!string.IsNullOrWhiteSpace(item.Geometry))
                {
                    img.Source = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/position.png", UriKind.RelativeOrAbsolute));
                    Color c = UtilityTools.ConvertColorCodeToColor("#5497FF");
                    txt.Foreground = new SolidColorBrush(c);
                }
                else
                {
                    img.Source = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/details.png", UriKind.RelativeOrAbsolute));
                    Color c = UtilityTools.ConvertColorCodeToColor("#B6B1B1");
                    txt.Foreground = new SolidColorBrush(c);
                }
                eventLawItems.Add(new EventLawItem()
                {
                    Image = img,
                    txtImg = txt,
                    eventLaw = item
                });
                TextBlock tb = new TextBlock
                {
                    Text = item.EventTitle,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    Margin = new Thickness { Left = 5 }
                };
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(txt);
                sp.Children.Add(tb);

                RadTreeViewItem rtv = new RadTreeViewItem()
                {
                    Header = sp,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    DataContext = item,
                };

                if (this.isPosByEventLawDoubleClicked)
                {
                    rtv.DoubleClick += (s, e) =>
                    {
                        if (this.PosClicked != null)
                        {
                            this.PosClicked(item, null);
                        }
                    };
                }

                rtventity.Items.Add(rtv);

                if (callBack != null)
                    callBack(rtv);
            }
        }
        #endregion

        #endregion

        public void BindOtherMenuClicked(RadTreeViewItem menuType, PositionType positionType)
        {
            RadContextMenu otherContextMenu = this.GenerateContextMenu(this.lstOtherMenu);
            otherContextMenu.ItemClick += (s, e) =>
            {
                if (this.AllPosClicked != null)
                {
                    OtherMenuPos otherMenuPos = new OtherMenuPos()
                    {
                        MenuType = menuType.DataContext,
                        PositionType = positionType
                    };
                    object positions = null;

                    if (menuType.DataContext is Unit)
                    {
                        Unit unit = menuType.DataContext as Unit;

                        switch (positionType)
                        {
                            case PositionType.Car:
                                positions = this.Cars.Where(t => t.UnitID == unit.ID).ToList();
                                break;
                            case PositionType.Person:
                                positions = this.Persons.Where(t => t.UnitID == unit.ID).ToList();
                                break;
                            case PositionType.EventLaw:
                                positions = this.EventLaws.Where(t => t.UnitID == unit.ID).ToList();
                                break;
                        }
                    }

                    otherMenuPos.Postions = positions;
                    this.AllPosClicked(otherMenuPos, null);
                }
                menuType.IsSelected = true;
            };
            menuType.SetValue(RadContextMenu.ContextMenuProperty, otherContextMenu);
        }

        #region 生成菜单
        public RadContextMenu GenerateContextMenu(List<MenuConfig> objs)
        {
            RadContextMenu radContextMenu = new RadContextMenu();
            List<RadMenuItem> items = new List<RadMenuItem>();
            StyleManager.SetTheme(radContextMenu, new Windows8Theme());

            foreach (var item in objs)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(item.IconUri, UriKind.RelativeOrAbsolute));
                img.Stretch = Stretch.Uniform;
                img.Width = 16;
                TextBlock txt = new TextBlock()
                {
                    FontSize = 14,
                    Text = item.Icon,
                    Foreground = new SolidColorBrush(Colors.Black),
                };

                items.Add(new RadMenuItem()
                {
                    Header = item.Header,
                    FontSize = 14,
                    Icon = img,
                });
            }
            radContextMenu.ItemsSource = items;
            return radContextMenu;
        }
        #endregion

        #region 名称查询
        private void txtQueryInfo_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Name == "txtQueryPersonInfo")
            {
                if (e.Key == Key.Enter)
                {
                    this.QueryPersons();
                    this.btnQueryPersonInfo.Content = "取消";
                }
            }
            //else if (txt.Name == "txtQueryCarInfo")
            //{
            //    if (e.Key == Key.Enter)
            //    {
            //        this.QueryCars();
            //        this.btnQueryCarInfo.Content = "取消";
            //    }
            //}
            else if (txt.Name == "txtQueryEventInfo")
            {
                if (e.Key == Key.Enter)
                {
                    this.QueryEvents();
                    this.btnQueryEventInfo.Content = "取消";
                }
            }
        }

        private void btnQueryInfo_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string text = btn.Content.ToString();

            if (btn.Name == "btnQueryPersonInfo")
            {
                if (text == "查询")
                {
                    this.QueryPersons();
                    this.btnQueryPersonInfo.Content = "取消";
                }
                else
                {
                    btn.Content = "查询";
                    this.rtvPerson.Visibility = Visibility.Visible;
                    this.rtvPersonQuery.Visibility = Visibility.Collapsed;
                    this.txtQueryPersonInfo.Text = "";
                }
            }
            //else if (btn.Name == "btnQueryCarInfo")
            //{
            //    if (text == "查询")
            //    {
            //        this.QueryCars();
            //        this.btnQueryCarInfo.Content = "取消";
            //    }
            //    else
            //    {
            //        btn.Content = "查询";
            //        this.rtvCar.Visibility = Visibility.Visible;
            //        this.rtvCarQuery.Visibility = Visibility.Collapsed;
            //        this.txtQueryCarInfo.Text = "";
            //    }
            //}
            else if (btn.Name == "btnQueryEventInfo")
            {
                if (text == "查询")
                {
                    this.QueryEvents();
                    this.btnQueryEventInfo.Content = "取消";
                }
                else
                {
                    btn.Content = "查询";
                    this.rtvEvent.Visibility = Visibility.Visible;
                    this.rtvEventQuery.Visibility = Visibility.Collapsed;
                    this.txtQueryEventInfo.Text = "";
                }
            }
        }
        #endregion

        /// <summary>
        /// 根据名字查询人员列表
        /// </summary>
        public void QueryPersons()
        {
            this.rtvPerson.Visibility = Visibility.Collapsed;
            this.rtvPersonQuery.Visibility = Visibility.Visible;

            string txtQueryPersonInfo = this.txtQueryPersonInfo.Text.Trim();
            List<Person> personResults = this.Persons.Where(t => t.UserName.Contains(txtQueryPersonInfo)).ToList();

            List<RadTreeViewItem> results = new List<RadTreeViewItem>();
            foreach (var item in personResults)
            {
                TextBlock txt = new TextBlock()
                {
                    FontFamily = new FontFamily("/NBZGM.PLE.LawCom;component/Fonts/icomoon.ttf#icomoon"),
                    FontSize = 18,
                    Text = "u"
                };

                //判断是否在线
                if (item.Lon.HasValue && item.Lat.HasValue)
                {
                    Color c = UtilityTools.ConvertColorCodeToColor("#5497FF");
                    txt.Foreground = new SolidColorBrush(c);
                }
                else
                {
                    Color c = UtilityTools.ConvertColorCodeToColor("#B6B1B1");
                    txt.Foreground = new SolidColorBrush(c);
                }

                int count = this.personItems.Where(t => t.person.UserID == item.UserID).Count();
                if (count < 1)
                {
                    this.personItems.Add(new PersonItem
                    {
                        txtImg = txt,
                        person = item
                    });
                }

                TextBlock tb = new TextBlock
                {
                    Text = item.UserName,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    Margin = new Thickness { Left = 5 }
                };

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(txt);
                sp.Children.Add(tb);

                RadTreeViewItem rtv = new RadTreeViewItem()
                {
                    Header = sp,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    DataContext = item,
                };

                if (this.isPosByPersonDoubleClicked)
                {
                    rtv.DoubleClick += (s, e) =>
                    {
                        if (this.PosClicked != null)
                        {
                            this.PosClicked(item, null);
                        }
                    };
                }

                this.PersonContextMenu = this.GenerateContextMenu(this.lstPersonMenu);
                this.PersonContextMenu.ItemClick += ContextMenuClick;
                rtv.SetValue(RadContextMenu.ContextMenuProperty, this.PersonContextMenu);
                results.Add(rtv);
            }

            this.rtvPersonQuery.ItemsSource = results;
        }

        /// <summary>
        /// 根据车牌名称查询车辆
        /// </summary>
        public void QueryCars()
        {
            //this.rtvCar.Visibility = Visibility.Collapsed;
            //this.rtvCarQuery.Visibility = Visibility.Visible;

            //string txtQueryCarInfo = this.txtQueryCarInfo.Text.Trim().ToUpper();
            //List<Car> carResults = this.Cars.Where(t => t.CarNumber.Contains(txtQueryCarInfo)).ToList();

            //List<RadTreeViewItem> results = new List<RadTreeViewItem>();
            //foreach (var item in carResults)
            //{
            //    TextBlock txt = new TextBlock();
            //    txt.FontFamily = new FontFamily("/NBZGM.PLE.LawCom;component/Fonts/icomoon.ttf#icomoon");
            //    txt.FontSize = 18;
            //    txt.Text = "c";

            //    if (item.X.HasValue && item.Y.HasValue)
            //    {
            //        Color c = UtilityTools.ConvertColorCodeToColor("#5497FF");
            //        txt.Foreground = new SolidColorBrush(c);
            //    }
            //    else
            //    {
            //        Color c = UtilityTools.ConvertColorCodeToColor("#B6B1B1");
            //        txt.Foreground = new SolidColorBrush(c);
            //    }

            //    int count = this.carItems.Where(t => t.car.ID == item.ID).Count();
            //    if (count < 1)
            //    {
            //        this.carItems.Add(new CarItem
            //        {
            //            txtImg = txt,
            //            car = item
            //        });
            //    }

            //    TextBlock tb = new TextBlock
            //    {
            //        Text = item.CarNumber,
            //        FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
            //        FontSize = 14,
            //        Margin = new Thickness { Left = 5 }
            //    };

            //    StackPanel sp = new StackPanel();
            //    sp.Orientation = Orientation.Horizontal;
            //    sp.Children.Add(txt);
            //    sp.Children.Add(tb);

            //    RadTreeViewItem rtv = new RadTreeViewItem()
            //    {
            //        Header = sp,
            //        FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
            //        FontSize = 14,
            //        DataContext = item,
            //    };

            //    if (this.isPosByCarDoubleClicked)
            //    {
            //        rtv.DoubleClick += (s, e) =>
            //        {
            //            if (this.PosClicked != null)
            //            {
            //                this.PosClicked(item, null);
            //            }
            //        };
            //    }

            //    this.CarContextMenu = this.GenerateContextMenu(this.lstCarMenu);
            //    this.CarContextMenu.ItemClick += ContextMenuClick;
            //    rtv.SetValue(RadContextMenu.ContextMenuProperty, this.CarContextMenu);
            //    results.Add(rtv);
            //}

            //this.rtvCarQuery.ItemsSource = results;
        }

        /// <summary>
        /// 根据事件名称查询事件
        /// </summary>
        public void QueryEvents()
        {
            this.rtvEvent.Visibility = Visibility.Collapsed;
            this.rtvEventQuery.Visibility = Visibility.Visible;

            string txtQueryEventInfo = this.txtQueryEventInfo.Text.Trim().ToUpper();
            List<EventLaw> eventResults = this.EventLaws.Where(t => t.EventTitle != null).Where(t => t.EventTitle.Contains(txtQueryEventInfo)).ToList();

            List<RadTreeViewItem> results = new List<RadTreeViewItem>();
            foreach (var item in eventResults)
            {
                TextBlock txt = new TextBlock()
                {
                    FontFamily = new FontFamily("/NBZGM.PLE.LawCom;component/Fonts/icomoon.ttf#icomoon"),
                    FontSize = 18,
                    Text = "v"
                };
                //判断是否有定位
                if (!string.IsNullOrWhiteSpace(item.Geometry))
                {
                    Color c = UtilityTools.ConvertColorCodeToColor("#5497FF");
                    txt.Foreground = new SolidColorBrush(c);
                }
                else
                {
                    Color c = UtilityTools.ConvertColorCodeToColor("#B6B1B1");
                    txt.Foreground = new SolidColorBrush(c);
                }
                eventLawItems.Add(new EventLawItem()
                {
                    txtImg = txt,
                    eventLaw = item
                });
                TextBlock tb = new TextBlock
                {
                    Text = item.EventTitle,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    Margin = new Thickness { Left = 5 }
                };
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(txt);
                sp.Children.Add(tb);

                RadTreeViewItem rtv = new RadTreeViewItem()
                {
                    Header = sp,
                    FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                    FontSize = 14,
                    DataContext = item,
                };

                if (this.isPosByEventLawDoubleClicked)
                {
                    rtv.DoubleClick += (s, e) =>
                    {
                        if (this.PosClicked != null)
                        {
                            this.PosClicked(item, null);
                        }
                    };
                }

                this.EventLawContextMenu = this.GenerateContextMenu(this.lstEventLawMenu);
                this.EventLawContextMenu.ItemClick += ContextMenuClick;
                rtv.SetValue(RadContextMenu.ContextMenuProperty, this.EventLawContextMenu);
                results.Add(rtv);
            }
            this.rtvEventQuery.ItemsSource = results;
        }

        private void radWindow_WindowStateChanged(object sender, EventArgs e)
        {
            if (this.radTab != null)
            {
                this.radTab.Visibility = Visibility.Visible;
            }
        }
    }
    public enum PositionType
    {
        Car,
        Person,
        EventLaw
    }

    public class OtherMenuPos
    {
        public object MenuType { get; set; }

        public PositionType PositionType { get; set; }

        public object Postions { get; set; }
    }
}
