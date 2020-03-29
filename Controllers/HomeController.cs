using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webhooks.Models;

namespace LudoBot.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public class Sender
        {
            public _Recipient recipient { set; get; }
            public string sender_action { set; get; }
            public _Message message { set; get; }
        };
        public class _Recipient
        {
            public string id { set; get; }
        };
        public class _Message
        {
            public string text { set; get; }
        };
        public JsonResult SendMessage()
        {
            const string access_token = "Your_token";
            Sender sender = new Sender();
            sender.recipient = new _Recipient();
            sender.message = new _Message();
            sender.recipient.id = "Your_id";
            sender.message.text = "Your_text";
            string json = JsonConvert.SerializeObject(sender);

            string base_url = "https://graph.facebook.com/v6.0/me/messages?access_token=" + access_token;
            var httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri(base_url);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync(base_url , content).Result;   
            var result = response.Content.ReadAsStringAsync();
            var res = result.Result;

            return Json(new
            {
                res,
                success = "Sended"
            });
        }
    }
}
