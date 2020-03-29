using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Webhooks.Models;
using static LudoBot.Controllers.HomeController;

namespace Webhooks.Dao
{
    public class FacebookDao
    {
        public string SendMessage(string senderId, string text)
        {
            const string access_token = "Your_token";
        
            Sender sender_action = new Sender();
            sender_action.recipient = new LudoBot.Controllers.HomeController._Recipient();
            sender_action.recipient.id = senderId;
            sender_action.sender_action = "typing_on";

            Sender sender = new Sender();
            sender.recipient = new LudoBot.Controllers.HomeController._Recipient();
            sender.message = new LudoBot.Controllers.HomeController._Message();
            sender.recipient.id = senderId;
            sender.message.text = text;

            string mess_action = JsonConvert.SerializeObject(sender_action);
            string mess_content = JsonConvert.SerializeObject(sender);

            string base_url = "https://graph.facebook.com/v6.0/me/messages?access_token=" + access_token;
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(base_url);

            var mess_action_post = new StringContent(mess_action, System.Text.Encoding.UTF8, "application/json");
            var mess_content_post = new StringContent(mess_content, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage respons = httpClient.PostAsync(base_url, mess_action_post).Result;
            HttpResponseMessage response = httpClient.PostAsync(base_url, mess_content_post).Result;
            
            var result = response.Content.ReadAsStringAsync();
            var res = result.Result;
            return res;
        }
        public string GetUpdateCorona()
        {
            var httpClient = new HttpClient();
            HttpResponseMessage rep = httpClient.GetAsync("https://code.junookyo.xyz/api/ncov-moh/data.json").Result;
            var repp = rep.Content;
            var reppp = repp.ReadAsStringAsync();
            var js = reppp.Result;
            var rlt = JsonConvert.DeserializeObject<CoronaUpdate>(js);

            var crnText = "Cập nhật thời điểm hiện tại\n🌏 Thế giới\n▪️Số ca nhiễm: " + rlt.data.global.cases + "\n▪️Đã tử vong: " + rlt.data.global.deaths + "\n▪️Đã phục hồi: " + rlt.data.global.recovered + "\n\n🇻🇳 Việt Nam\n▪️Số ca nhiễm: " + rlt.data.vietnam.cases + "\n▪️Đã tử vong: " + rlt.data.vietnam.deaths + "\n▪️Đã phục hồi: " + rlt.data.vietnam.recovered;
            return crnText;
        }
        public string getVietnamCorona()
        {
            var httpClient = new HttpClient();
            HttpResponseMessage rep = httpClient.GetAsync("https://www.otheah.com/api/covid19/vncovid19").Result;
            var repp = rep.Content;
            var reppp = repp.ReadAsStringAsync();
            var js = reppp.Result;
            var rlt = JsonConvert.DeserializeObject<VietNamCorona>(js);
            var covid = rlt.coronavirus.covidcountry;
            var text = "🇻🇳 Chi tiết các tỉnh thành bị nhiễm tại Việt Nam 🇻🇳\n";
            foreach (var cod in covid)
            {
                text += cod.province + ": " + cod.confirmed + "🤦‍♀️ - Phục hồi: " + cod.recovered + " ✔️\n";
            }
            return text;
        }
    }
}