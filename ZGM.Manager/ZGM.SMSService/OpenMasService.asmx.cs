using OpenMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using ZGM.Model;
using ZGM.BLL.XTBGBLL;
using ZGM.Model.XTBGModels;
using System.IO;
using System.Text;
using ZGM.BLL.UserBLLs;

namespace ZGM.SMSService
{
    /// <summary>
    /// OpenMasService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://openmas.chinamobile.com/pulgin")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class OpenMasService : System.Web.Services.WebService
    {
        [WebMethod]
        [SoapDocumentMethodAttribute("urn:NotifySms", RequestNamespace = "http://openmas.chinamobile.com/pulgin", OneWay = true, Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void NotifySms(string messageId)
        {
            try
            {
                //调用上行短信获取接口获取短消息
                string _SmsServiceUrl = System.Configuration.ConfigurationManager.AppSettings["OpenMasUrl"];
                OpenMas.Sms _Sms = new OpenMas.Sms(_SmsServiceUrl);
                SmsMessage message = _Sms.GetMessage(messageId);

                //业务逻辑，短信内容可以从message中获取
                //string json = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                //SMS_MESSAGES sms = new SMS_MESSAGES();
                //sms.REMARK = json;
                //SMS_MESSAGESBLL.AddMessages(sms);                
            }
            catch (Exception ex)
            {
                //处理异常信息
                //SMS_MESSAGES sms = new SMS_MESSAGES();
                //sms.REMARK = ex.Message;                
                //SMS_MESSAGESBLL.AddMessages(sms); 
            }
        }

        [WebMethod]
        [SoapDocumentMethodAttribute("urn:NotifySmsDeliveryReport", RequestNamespace = "http://openmas.chinamobile.com/pulgin", OneWay = true, Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void NotifySmsDeliveryReport(OpenMas.Proxy.DeliveryReport deliveryReport)
        {
            try
            {
                //内容提取
                //string MessageID = deliveryReport.messageId;        //短信ID
                //string ReceivedAddress = deliveryReport.receivedAddress; //接收号码，通常为手机号码
                //string StatusCode = deliveryReport.statusCode;//返回的结果代码，0表示成功
                //int MessageDeliveryStatus = Convert.ToInt32(deliveryReport.messageDeliveryStatus);//结果状态

                //业务逻辑处理
                //string json = Newtonsoft.Json.JsonConvert.SerializeObject(deliveryReport);
                string receiveusersname = "";
                SYS_USERS usermodel = UserBLL.GetUserModel(deliveryReport.receivedAddress);
                if (usermodel!=null)
                {
                    if (deliveryReport.messageDeliveryStatus == "Delivered")
                    {
                        receiveusersname = usermodel.USERNAME + "[" + usermodel.PHONE + " 发送成功]";
                    }
                    else
                    {
                        receiveusersname = usermodel.USERNAME + "[" + usermodel.PHONE + " 发送失败]";
                    }
                }
                else
                {
                    receiveusersname = "[" + deliveryReport.receivedAddress + " 发送成功]";
                }
                
                SMS_MESSAGES sms =SMS_MESSAGESBLL.GetMessagesModel(deliveryReport.messageId);
                if (sms!=null)
                {
                    if (string.IsNullOrEmpty(sms.REMARK))
                        sms.REMARK = receiveusersname;
                    else
                        sms.REMARK = sms.REMARK + "," + receiveusersname;

                    sms.MESSAGEID = deliveryReport.messageId;
                    SMS_MESSAGESBLL.AlterMessages(sms);
                }
                
            }
            catch (Exception ex)
            {
                //处理异常信息
                //SMS_MESSAGES sms = new SMS_MESSAGES();
                //sms.REMARK = ex.Message;
                //sms.MESSAGEID = deliveryReport.messageId;
                //SMS_MESSAGESBLL.AddMessages(sms); 
            }
        }


    }
}
