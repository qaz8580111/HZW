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

namespace ZGM.WUA.Helper
{
    public class DataTools
    {
        public event DownloadStringCompletedEventHandler GetDataCompleted;
        /// <summary>
        ///  获取数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="t"></param>
        public void GetData<T>(string uri)
        {
            string api = Application.Current.Resources["api"] as string;

            api = api + uri;

            var uriStr = new Uri(api);
            WebClient wc = new WebClient();

            wc.DownloadStringCompleted += (s1,e)=>{
                try
                {
                    if (this.GetDataCompleted != null)
                    {
                        this.GetDataCompleted(this, e);
                    }
                }
                catch {
                    throw new Exception(api);
                }
            };
            wc.DownloadStringAsync(uriStr);
        }

        /// <summary>
        /// 数据获取完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (this.GetDataCompleted != null)
            {
                this.GetDataCompleted(this, e);
            }
        }
    }
}
