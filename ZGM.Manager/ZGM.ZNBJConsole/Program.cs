using Apache.NMS;
using Apache.NMS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ZGM.Model;

namespace ZGM.ZNBJConsole
{
    class Program
    {
        private static IConnectionFactory connFac;

        private static IConnection connection;
        private static ISession session;
        private static IDestination destination;
        private static IMessageProducer producer;
        private static IMessageConsumer consumer;

        static void Main(string[] args)
        {
            string strTopicName = "alarm.msg.topic";
            try
            {
                //172.16.2.35
                connFac = new NMSConnectionFactory(new Uri("activemq:failover:(tcp://172.172.100.10:61616)"));
                //新建连接  
                //connection = connFac.CreateConnection("oa","oa");//设置连接要用的用户名、密码 
                //如果你是缺省方式启动Active MQ服务，则不需填用户名、密码 
                using (connection = connFac.CreateConnection())
                {
                    //如果你要持久“订阅”，则需要设置ClientId，这样程序运行当中被停止，恢复运行时，能拿到没接收到的消息！  
                    connection.ClientId = "testing listener";
                    connection.Start();
                    //创建Session  
                    using (session = connection.CreateSession())
                    {
                        //发布/订阅模式，适合一对多的情况  
                        destination = SessionUtil.GetDestination(session, "topic://" + strTopicName);
                        //新建生产者对象  
                        producer = session.CreateProducer(destination);
                        producer.DeliveryMode = MsgDeliveryMode.NonPersistent;//ActiveMQ服务器停止工作后，消息不再保留  
                        //新建消费者对象:普通“订阅”模式  
                        //consumer = session.CreateConsumer(destination);//不需要持久“订阅”         

                        //新建消费者对象:持久"订阅"模式：  
                        //    持久“订阅”后，如果你的程序被停止工作后，恢复运行，  
                        //从第一次持久订阅开始，没收到的消息还可以继续收  
                        consumer = session.CreateDurableConsumer(
                            session.GetTopic(strTopicName)
                            , connection.ClientId, null, false);
                        //设置消息接收事件  
                        consumer.Listener += consumer_Listener;
                        Console.ReadLine();
                    }
                    connection.Stop();
                    connection.Close();
                }
            }
            catch (Exception)
            {


            }
            //启动来自Active MQ的消息侦听  
        }

        static void consumer_Listener(IMessage message)
        {
            ITextMessage request = message as ITextMessage;
            // Console.WriteLine(request.Text);
            XmlDocument xd = new XmlDocument();
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xd.LoadXml(request.Text);
            ZGM.Model.XTGL_ZNBJSJS model = new Model.XTGL_ZNBJSJS();
            model.EVENTID = decimal.Parse(xd.GetElementsByTagName("EventId")[0].InnerText);
            model.EVENTTYPE = decimal.Parse(xd.GetElementsByTagName("EventType")[0].InnerText);
            model.INPUTSOURCE = decimal.Parse(xd.GetElementsByTagName("InputSource")[0].InnerText);
            model.INPUTSOURCEINDEXCODE = xd.GetElementsByTagName("InputSourceIndexcode")[0].InnerText;
            model.EVENTNAME = xd.GetElementsByTagName("EventName")[0].InnerText;
            model.LOGID = xd.GetElementsByTagName("LogId")[0].InnerText;
            model.STATUS = decimal.Parse(xd.GetElementsByTagName("Status")[0].InnerText);
            model.HAPPENTIME = DateTime.Parse(xd.GetElementsByTagName("HappenTime")[0].InnerText);
            string EndTime = xd.GetElementsByTagName("EndTime")[0].InnerText;
            if (!string.IsNullOrEmpty(EndTime))
            {
                model.ENDTIME = DateTime.Parse(EndTime);
            }
            XTGL_ZNBJSJS dbmodel = ZGM.BLL.XTGLBLL.ZNBJBLL.GetZNBJSJByLogID(model.LOGID);
            if (dbmodel == null)
            {
                if (ZGM.BLL.XTGLBLL.ZNBJBLL.AddZNBJ(model) > 0)
                {
                    Console.WriteLine("同步成功LogID=" + model.LOGID);
                }
                else
                {
                    Console.WriteLine("同步失败LogID=" + model.LOGID);
                }
            }
            else
            {
                if (model.STATUS == 0 && !string.IsNullOrEmpty(EndTime))
                {
                    if (ZGM.BLL.XTGLBLL.ZNBJBLL.UpdateZNBJSJByZNBJID(model, dbmodel.ZNBJID) > 0)
                    {
                        Console.WriteLine("更新成功LOGID=" + model.LOGID);
                    }
                    else
                    {
                        Console.WriteLine("更新失败LOGID=" + model.LOGID);
                    }
                }
            }
        }
    }
}
