using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;
using System.Windows.Media.Imaging;
using ZGM.WUA;
using ZGM.WUA.ImgConfig;

namespace ZGM.WUA.DrawHelper
{
    public enum CustomCursorType
    {
        /// <summary>尺子</summary>
        Ruler = 0,
        /// <summary>线</summary>
        Line = 1,
        /// <summary>面</summary>
        Surface = 2,
        /// <summary>框选使用</summary>
        Area = 3,
        CenterPoint =4,
        SelectEvent = 5,
        Position = 6,
    }
    public class CursorEntity
    {
        public Uri Uri { get; set; }
        public Thickness Thickness { get; set; }
    }
    public class ImageCursor
    {
        private static Popup _cursorPopup;
        private static DispatcherTimer _mouseLeaveTimer;
        private static object _syncRoot = new object();
        private static GeneralTransform _generalTransform;
        private static Point _mousePoint;
        private static UIElement _popupChild;
        private static FrameworkElement _shownElement;
        private static FrameworkElement _capturingElement;
        private static CursorEntity[] Images = new CursorEntity[] { 
            new CursorEntity() { Uri=IconSourceConfig.Tool_Ruler, Thickness=new Thickness(-2,-5,0,0)},
            new CursorEntity() {Uri=IconSourceConfig.Tool_Line, Thickness=new Thickness(0)},
            new CursorEntity() {Uri=IconSourceConfig.Tool_Face, Thickness=new Thickness(0)},
            new CursorEntity() {Uri=IconSourceConfig.Tool_Area, Thickness=new Thickness(0)},
            new CursorEntity() {Uri=IconSourceConfig.Tool_Line, Thickness=new Thickness(0)},
            new CursorEntity() {Uri=IconSourceConfig.Tool_Line, Thickness=new Thickness(0)},
             new CursorEntity() {Uri=IconSourceConfig.Position, Thickness=new Thickness(-14,-33,0,0)}
        };
        private static Popup CursorPopup
        {
            get
            {
                if (_cursorPopup == null)
                {
                    lock (_syncRoot)
                    {
                        if (_cursorPopup == null)
                        {
                            _cursorPopup = new Popup();
                            _cursorPopup.IsHitTestVisible = false;
                            _cursorPopup.IsOpen = true;
                        }
                    }
                }
                return _cursorPopup;
            }
        }

        /// <summary>
        /// MouseLeave後啟動
        /// </summary>
        private static DispatcherTimer MouseLeaveTimer
        {
            get
            {
                if (_mouseLeaveTimer == null)
                {
                    lock (_syncRoot)
                    {
                        if (_mouseLeaveTimer == null)
                        {
                            _mouseLeaveTimer = new DispatcherTimer();
                            _mouseLeaveTimer.Interval = TimeSpan.FromMilliseconds(10);
                            _mouseLeaveTimer.Tick += new EventHandler(OnMouseLeaveTimerTick);

                        }
                    }
                }
                return _mouseLeaveTimer;
            }
        }

        #region dependency property

        #region 自定義鼠標

        public static UIElement GetCustomCursor(DependencyObject obj) { return (UIElement)obj.GetValue(CustomCursorProperty); }

        public static void SetCursor(CustomCursorType type)
        {            
            Image img = new Image();
            img.Source = new BitmapImage { UriSource = Images[(int)type].Uri };
            img.Margin = Images[(int)type].Thickness;
            ImageCursor.SetCustomCursor(MainPage.Map, img);
        }

        public static void ClearCursor()
        {
            ImageCursor.SetCustomCursor(MainPage.Map, null);
        }

        public static void SetCustomCursor(DependencyObject obj, UIElement value) { obj.SetValue(CustomCursorProperty, value); }

        public static readonly DependencyProperty CustomCursorProperty =
            DependencyProperty.RegisterAttached("CustomCursor", typeof(FrameworkElement), typeof(ImageCursor), new PropertyMetadata(OnCursorChanged));
        #endregion

        #region 是否使用默認鼠標
        public static bool GetUseOriginalCursor(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseOriginalCursorProperty);
        }

        public static void SetUseOriginalCursor(DependencyObject obj, bool value)
        {
            obj.SetValue(UseOriginalCursorProperty, value);
        }

        public static readonly DependencyProperty UseOriginalCursorProperty =
            DependencyProperty.RegisterAttached("UseOriginalCursor", typeof(bool), typeof(ImageCursor), new PropertyMetadata(OnUseOriginalCursorChanged));
        #endregion

        #region 本來的鼠標
        private static Cursor GetOriginalCursor(DependencyObject obj)
        {
            return (Cursor)obj.GetValue(OriginalCursorProperty);
        }

        private static void SetOriginalCursor(DependencyObject obj, Cursor value)
        {
            obj.SetValue(OriginalCursorProperty, value);
        }

        public static readonly DependencyProperty OriginalCursorProperty =
            DependencyProperty.RegisterAttached("OriginalCursor", typeof(Cursor), typeof(ImageCursor), null);
        #endregion

        #endregion

        #region 響應事件

        private static void OnUseOriginalCursorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = obj as FrameworkElement;
            if (element != null)
                SetCusorToUIElement(element);
        }


        private static void OnCursorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = obj as FrameworkElement;
            if (element != null)
                SetCusorToUIElement(element);
        }

        private static void OnMouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            _generalTransform = element.TransformToVisual(App.Current.RootVisual);
            OnMouseMove(element, _generalTransform.Transform(e.GetPosition(element)));
        }


        private static void OnMouseLeave(object sender, MouseEventArgs e)
        {
            var popup = CursorPopup;
            FrameworkElement element = sender as FrameworkElement;
            var child = GetCustomCursor(element);
            if (child != null)
                child.Visibility = Visibility.Collapsed;

            if (_capturingElement == element)
                MouseLeaveTimer.Start();
            else
                _shownElement = null;
        }

        private static void OnMouseLeaveTimerTick(object sender, EventArgs e)
        {
            if (_capturingElement == null || CheckIsCapturing(_capturingElement) == false)
            {
                MouseLeaveTimer.Stop();
                _shownElement = null;
                UpdateCurrentChild();
            }
        }
        #endregion


        private static void SetCusorToUIElement(FrameworkElement element)
        {
            var customCurosr = GetCustomCursor(element);
            var userOriginalCursor = GetUseOriginalCursor(element);
            if (customCurosr != null || userOriginalCursor)
            {
                if (customCurosr != null)
                {
                    customCurosr.IsHitTestVisible = false;
                    if (_shownElement == element && CursorPopup.Child != null)
                    {
                        customCurosr.Visibility = CursorPopup.Child.Visibility;
                        CursorPopup.Child = customCurosr;
                    }
                }
                if (userOriginalCursor == false)
                    element.Cursor = Cursors.None;
                DetachEvent(element);
                AttachEvent(element);
                //if (_mousePoint != null && VisualTreeHelper.FindElementsInHostCoordinates(_mousePoint, element).Contains(element))
                //    OnMouseMove(element, _mousePoint);
            }
            else
            {
                SetIsHandeld(element, false);
                element.Cursor = GetOriginalCursor(element);
                UpdateCurrentChild();
            }

            if (GetOriginalCursor(element) == null && element.Cursor != Cursors.None)
                SetOriginalCursor(element, element.Cursor);
        }

        private static void AttachEvent(FrameworkElement element)
        {
            element.MouseLeave += new MouseEventHandler(OnMouseLeave);
            element.MouseMove += new MouseEventHandler(OnMouseMove);
        }

        private static void DetachEvent(FrameworkElement element)
        {
            element.MouseLeave -= new MouseEventHandler(OnMouseLeave);
            element.MouseMove -= new MouseEventHandler(OnMouseMove);
        }


        private static bool CheckIsHandled(FrameworkElement element)
        {
            if (_shownElement == element)
                return false;
            FrameworkElement parent = _shownElement;
            while (parent != null && parent != element)
                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
            if (parent != null)
                return true;
            else
                return false;
        }

        private static void SetIsHandeld(FrameworkElement element, bool value)
        {
            if (value)
                _shownElement = element;
            else
            {
                if (_shownElement == element)
                    _shownElement = null;
                DetachEvent(element);
            }
        }

        private static void OnMouseMove(FrameworkElement element, Point mousePoint)
        {
            _mousePoint = mousePoint;
            if (element == null || CheckIsHandled(element))
                return;
            SetIsHandeld(element, true);
            _popupChild = GetCustomCursor(element);

            if (_popupChild != CursorPopup.Child)
                CursorPopup.Child = _popupChild;
            if (_popupChild == null)
                return;

            CursorPopup.HorizontalOffset = _mousePoint.X;
            CursorPopup.VerticalOffset = _mousePoint.Y;

            if (_popupChild.Visibility != Visibility.Visible)
                _popupChild.Visibility = Visibility.Visible;

            if (CheckIsCapturing(element))
                _capturingElement = element;
            else if (_capturingElement == element)
                _capturingElement = null;
            GetToppestElement(_mousePoint);
        }


        private static void UpdateCurrentChild()
        {
            if (_mousePoint == null)
                return;
            var pointElement = GetToppestElement(_mousePoint);
            if (pointElement == null)
                CursorPopup.Child = null;
            else
                OnMouseMove(pointElement, _mousePoint);
        }

        private static FrameworkElement GetToppestElement(Point point)
        {
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(point, App.Current.RootVisual);
            return elements.Where(es => (es is FrameworkElement)
                && (GetCustomCursor(es as FrameworkElement) != null || GetUseOriginalCursor(es as FrameworkElement) == true))
                .ElementAtOrDefault(0) as FrameworkElement;
        }


        private static bool CheckIsCapturing(FrameworkElement element)
        {
            bool isRootCapturingMouse = App.Current.RootVisual.CaptureMouse();
            App.Current.RootVisual.ReleaseMouseCapture();
            if (isRootCapturingMouse)
                return false;
            else
            {
                if (element.CaptureMouse())
                    return true;
                else
                    return false;
            }
        }
    }
}
