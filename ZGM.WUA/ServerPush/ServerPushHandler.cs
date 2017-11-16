using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
#region 命名空间
using System.Web;
using System.Threading;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;
#endregion

namespace ServerPush
{
    public class ServerPushHandler
    {
        #region 全局变量
        HttpContext m_Context;
        static List<decimal> currentPersonId = new List<decimal>();
        //推送结果
        ServerPushResult _IAsyncResult;
        MessageBLL bll = new MessageBLL();
        //声明一个集合
        static Dictionary<string, ServerPushResult> dict = new Dictionary<string, ServerPushResult>();
        //sdk对外接口
        //WebChat sdk = new WebChat();
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public ServerPushHandler(HttpContext context, ServerPushResult _IAsyncResult)
        {
            this.m_Context = context;
            this._IAsyncResult = _IAsyncResult;
        }
        #endregion

        #region 执行操作
        /// <summary>
        /// 根据Action判断执行方法
        /// </summary>
        /// <returns></returns>
        public ServerPushResult ExecAction()
        {
           
            switch (m_Context.Request["Action"])
            {
                case "Test":
                    test();
                    break;
                case "GetMessageNoRead":
                    GetMessageNoRead();
                    break;
                case "GetMessageNoReadStat":
                    GetMessageNoReadStat();
                    break;
            }
            return _IAsyncResult;
        }

        private void GetMessageNoReadStat()
        {
            if (string.IsNullOrWhiteSpace(m_Context.Request["ReceiverId"]))
            {
                _IAsyncResult.Result = new { IsFinish = 1 };
                _IAsyncResult.Send();
                return;
            }
            decimal ReceiverId = Convert.ToDecimal(m_Context.Request["ReceiverId"]);
           List<MessageNoReadModel> result=new List<MessageNoReadModel>();
           while (result.Count == 0)
           {
               Thread.Sleep(500);              
               result = bll.GetMessageNoReadStat(ReceiverId);
           }
           _IAsyncResult.Result = result;
           _IAsyncResult.Send();   
        }

        private void GetMessageNoRead()
        {
            MessageModel message = new MessageModel();
            message.ReceiverId =Convert.ToDecimal( m_Context.Request["ReceiverId"]);
            message.SendId = Convert.ToDecimal(m_Context.Request["SendId"]);
            currentPersonId.Add((decimal)message.SendId);
            List<MessageModel> result = new List<MessageModel>();
            while (result.Count==0)
            {
                if (currentPersonId.Count>=2)
                {
                    if (message.SendId != currentPersonId[currentPersonId.Count-1])
                    {
                        _IAsyncResult.Result =new {IsFinish=1};
                        _IAsyncResult.Send();
                        return;
                    }
                }
                Thread.Sleep(500);
                try
                {
                    result = bll.GetMessageNoRead(message);
                }
                catch (Exception e) {
                    Debug.WriteLine("exception:" + e.Message);
                    _IAsyncResult.Result = new { IsFinish = 1 };
                    _IAsyncResult.Send();
                    return;
                }
               Debug.WriteLine("SendId:" + message.SendId);
            }
            _IAsyncResult.Result = result;
            _IAsyncResult.Send();           
        }

        private void test()
        {
            _IAsyncResult.Result = "返回" + DateTime.Now.ToString("yyyyMMdd hh:mm:ss");
            DateTime dt = Convert.ToDateTime("2016-06-24 09:42:00");
            while (DateTime.Now < dt)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss"));
            };
            Thread.Sleep(300);
            _IAsyncResult.Result = "{返回:\"" + DateTime.Now.ToString("yyyyMMdd hh:mm:ss")+"\"}";
            _IAsyncResult.Send();
           
        }
        #endregion 
    }
}
