using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
//using HZCG.EC.ICS.Com.Configs;
//using HZCG.EC.ICS.Managers;
//using HZCG.EC.ICS.Map;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using ZGM.WUA.Maker;
using ZGM.WUA;

namespace ZGM.WUA.DrawHelper
{
    public enum MenuDrawToolType
    {
        /// <summary>测距 </summary>
        MeasureDistance,
        /// <summary>测面 </summary>
        MeasureFace,
        /// <summary>画点</summary>
        Point,
        /// <summary>线</summary>
        Line,
        /// <summary>面</summary>
        Surface,
        /// <summary>框选</summary>
        AreaSelect
    }

    public class MenuDrawElement
    {
        public MenuDrawToolType ToolType { get; set; }
        public List<BaseMarker> Markers { get; set; }
        public List<Graphic> Graphics { get; set; }
        public BaseMarker OptionMarker { get; set; }
        public bool IsHiden { get; set; }
        //工程id
        public string GroupID { get; set; }
    }

    /// <summary>
    /// 显示隐藏删除
    /// </summary>
    public class MenuToolControl
    {
       
        public Dictionary<string, MenuDrawElement> DrawElemnetList = new Dictionary<string, MenuDrawElement>();

        #region 添加、删除，隐藏元素
        public void AddOptionElement(string Key, BaseMarker Marker, bool IsHide)
        {
            MenuDrawElement element = GetElement(Key);
            element.OptionMarker = Marker;
            if (!IsHide)
            {
                MainPage.AppendToolMark(Marker);
            }
        }
        public void AddElement(string Key, BaseMarker Marker)
        {
            MenuDrawElement element = GetElement(Key);
            //RemoveAll();
            AddElement(element, Marker);
        }

        public void AddElement(string Key, Graphic graphic)
        {
            MenuDrawElement element = GetElement(Key);
            AddElement(element, graphic);
        }

        public void AddElement(string Key,string GroupID ,Graphic graphic)
        {
            MenuDrawElement element = GetElement(Key, GroupID);            
            AddElement(element, graphic);
        }

        public void AddElement(MenuDrawElement element, BaseMarker Marker)
        {
            if (!element.Markers.Contains(Marker))
                element.Markers.Add(Marker);
            MainPage.AppendToolMark(Marker);
        }

        public void AddElement(MenuDrawElement element, Graphic graphic)
        {            
            if (!element.Graphics.Contains(graphic))
                element.Graphics.Add(graphic);
            MainPage.GraphicsLayer.Graphics.Add(graphic);
        }
        public void RemoveOptionElement(string Key, bool IsHide)
        {
            MenuDrawElement element = GetElement(Key);

            if (element.OptionMarker != null)
            {
                MainPage.RemoveToolMark(element.OptionMarker);
            }

            if (!IsHide)
            {
                element.OptionMarker = null;
            }
        }
        public void RemoveElement(string Key, BaseMarker Marker, bool IsHide)
        {
            MenuDrawElement element = GetElement(Key);
            if (!IsHide)
            {
                if(element.Markers.Contains(Marker))
                    element.Markers.Remove(Marker);
            }
            MainPage.RemoveToolMark(Marker);
        }

        public void RemoveElement(string Key, Graphic graphic, bool IsHide)
        {
            MenuDrawElement element = GetElement(Key);
            RemoveElement(element, graphic, IsHide);
        }

        public void RemoveElement(MenuDrawElement element, BaseMarker Marker, bool IsHide)
        {
            if (!IsHide)
            {
                element.Markers.Remove(Marker);
            }
            MainPage.RemoveToolMark(Marker);
        }

        public void RemoveElement(MenuDrawElement element, Graphic graphic, bool IsHide)
        {
            if (!IsHide)
            {
                element.Graphics.Remove(graphic);
            }
            MainPage.GraphicsLayer.Graphics.Remove(graphic);
        }

        #endregion
        public BaseMarker GetOptionMarker(string Key)
        {
            MenuDrawElement element = GetElement(Key);
            return element.OptionMarker;
        }

        public void ShowOptionMarker(string key)
        {
            MenuDrawElement element = GetElement(key);
            AddOptionElement(key, element.OptionMarker, false);
        }

        public void HideOptionMarker(string key)
        {
            RemoveOptionElement(key, true);
        }

        public void ShowAll()
        {
            var keys = DrawElemnetList.Where(p => p.Value.IsHiden == true).Select(a => a.Key);
            foreach (var item in keys)
            {
                Show(item);
            }
        }

        public void HideAll()
        {
            var keys = DrawElemnetList.Where(p => p.Value.IsHiden == false).Select(a => a.Key);
            foreach (var item in keys)
            {
                Hide(item);
            }
        }

        public void Show(string key)
        {
            MenuDrawElement element = GetElement(key);
            element.IsHiden = false;
            foreach (var item in element.Graphics)
            {
                AddElement(element, item);
            }

            foreach (var item in element.Markers)
            {
                AddElement(element, item);
            }
        }

        public void Hide(string key)
        {
            MenuDrawElement element = GetElement(key);
            element.IsHiden = true;
            List<Graphic> list = element.Graphics;
            foreach (var item in list)
            {
                RemoveElement(element, item, true);
            }

            List<BaseMarker> markerlist = element.Markers;
            foreach (var item in markerlist)
            {
                RemoveElement(element, item, true);
            }

            RemoveOptionElement(key, true);
        }

        public void RemoveAll()
        {
            foreach (var item in DrawElemnetList)
            {
                Remove(item.Key);
            }

            DrawElemnetList.Clear();
        }

        public void Remove(string key)
        {
            MenuDrawElement element = GetElement(key);

            List<Graphic> list = element.Graphics;
            foreach (var item in list)
            {
                RemoveElement(element, item, true);
            }
            element.Graphics.Clear();

            List<BaseMarker> markerlist = element.Markers;
            foreach (var item in markerlist)
            {
                RemoveElement(element, item, true);
            }
            element.Markers.Clear();

            RemoveOptionElement(key, false);
        }

        public void CreateDrwaElemnet(string key, MenuDrawToolType type)
        {
            MenuDrawElement element = GetElement(key);
            element.ToolType = type;
        }

        public MenuDrawElement GetElement(string key)
        {
            MenuDrawElement element;
            if (DrawElemnetList.ContainsKey(key))
            {
                element = DrawElemnetList[key];
            }
            else
            {
                element = new MenuDrawElement();
                element.Markers = new List<BaseMarker>();
                element.Graphics = new List<Graphic>();
                DrawElemnetList.Add(key, element);
            }

            return element;
        }

        public MenuDrawElement GetElement(string Key,string GroupID)
        {
            MenuDrawElement element;
            if (DrawElemnetList.ContainsKey(Key))
            {
                element = DrawElemnetList[Key];
            }
            else
            {
                element = new MenuDrawElement();
                element.GroupID = GroupID;
                element.Markers = new List<BaseMarker>();
                element.Graphics = new List<Graphic>();
                DrawElemnetList.Add(Key, element);
            }

            return element;
        }
    }
}
