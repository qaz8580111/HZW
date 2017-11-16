using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class MapElementModel
    {
        public string Id { get; set; }
        /// <summary>
        /// 人员-UserModel，车辆-CarModel，案件-TaskModel
        /// </summary>
        public string Type { get; set; }
        public string Name { get; set; }
        public string PartsType { get; set; }
        public string Circum { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public string Lines { get; set; }
        public string Areas { get; set; }
        public string IsOnline { get; set; }
        public string IsAlarm { get; set; }
        //周边选择的类型 人员-UserModel，车辆-CarModel，案件-TaskModel
        public List<string> SelectTypes { get; set; }
        //由周边功能使用
        public decimal? XMax { get; set; }
        public decimal? XMin { get; set; }
        public decimal? YMax { get; set; }
        public decimal? YMin { get; set; }
        public string Note { set; get; }
        public double Distance { get; set; }
        public string Node { get; set; }
        public CameraInfoModel Content { get; set; }
        /// <summary>
        /// 是否在视频区域 1-在视频区域内   0-不在视频区域内 
        /// </summary>
        public int IsVideoArea { get {
            if (this.isVideoArea)
            {
                return 1;
            }
            else {
                return 0;
            }
        } }
        public bool isVideoArea = false;
    }
}
