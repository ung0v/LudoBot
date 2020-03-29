using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Webhooks.Models;
using Newtonsoft.Json;
using static LudoBot.Controllers.HomeController;
using Webhooks.Dao;

namespace Webhooks.Controllers
{
    public class WebhooksController : ApiController
    {

        #region Get Request  
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(HttpContext.Current.Request.QueryString["hub.challenge"])
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;
        }
        #endregion Get Request  

        #region Post Request  

        [HttpPost]
        public HttpResponseMessage Post([FromBody] FallBackFacebook data)
        {
            try
            {
                var entries = data.entry.FirstOrDefault();
                var messaging = entries.messaging.FirstOrDefault();
                var senderId = messaging.sender.id;
                if (messaging.message != null)
                {
                    var method = new FacebookDao();
                    if (messaging.message.text.ToLower().Contains("yêu"))
                    {
                        method.SendMessage(senderId, "yêu đương gì tầm này  🤷‍♀️ ? Sự nghiệp dang dở thì đừng có yêu 🤷‍♀️");
                    }
                    else if (messaging.message.text.ToLower().Contains("ngu"))
                    {
                        method.SendMessage(senderId, "Mày ngu ấy <3");
                    }
                    else if (messaging.message.text.ToLower().Contains("bot"))
                    {
                        method.SendMessage(senderId, "Bạn nói gì về tôi đấy ? Thích lên phường hông ? ");
                    }
                    else if (messaging.message.text.ToLower().Contains("vương"))
                    {
                        method.SendMessage(senderId, "Đừng nhắc về anh ấy nữa 🖕");
                    }
                    else if (messaging.message.text.ToLower().Contains("?"))
                    {
                        method.SendMessage(senderId, "? cái gì, thích ăn xiên 🔪 ?");
                    }
                    //if (messaging.message.attachments.FirstOrDefault().payload.sticker_id == "369239263222822")
                    //{
                    //    method.SendMessage(senderId, "Like dick head :)");
                    //}
                    else
                    switch (messaging.message.text.ToLower())
                    {
                        case "corona":
                            {
                                method.SendMessage(senderId, method.GetUpdateCorona());
                            }
                            break;
                        case "vncorona":
                            {
                                method.SendMessage(senderId, method.getVietnamCorona());
                            }
                            break;
                        case "hello":
                            {
                                method.SendMessage(senderId, "Lô con c* <3");
                            }
                            break;
                        default:
                            {
                                method.SendMessage(senderId, "Hế lô mai phen <3, hãy cho tôi biết bạn muốn điều gì 👀");
                            }
                            break;
                    }
                }
                //You got the data do whatever you want here!!!Happy programming!!  
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
      
        #endregion
    }
}
