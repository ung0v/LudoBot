using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webhooks.Models
{
    public class FallBackFacebook
    {
        [JsonProperty("object")]
        public string @object { set; get; }

        public List<_Entry> entry { set; get; }

    }
    public class _Entry
    {
        public string id { set; get; }
        public string time { set; get; }
        public List<_Messaging> messaging { set; get; }

    }
    public class _Messaging
    {
        public _Sender sender {set;get;}
        public _Recipient recipient { set; get; }
        public string timestamp { set; get; }
        public _Message message { set; get; }
    }
    public class _Message
    {
        public string mid { set; get; }
        public string text { set; get; }
        public _Quick_Reply quick_reply { set; get; }
        public _Reply_To reply_to { set; get; }
        public List<_Attachments> attachments { set; get; }
       
    }
    public class _Sender
    {
        public string id { set; get; }
    }
    public class _Recipient
    {
        public string id { set; get; }
    }
    public class _Attachments
    {
        public string type { set; get; }
        public _Payload payload { set; get; }
    }
    public class _Payload
    {
        public string url { set; get;}
        public string title { set; get; }
        public string sticker_id { set; get; }

        [JsonProperty("coordinates.lat")]
        public string coordinates_lat { set; get; }

        [JsonProperty("coordinates.long")]
        public string coordinates_long { set; get; }
    }
    public class _Reply_To
    {
        public string mid { set; get; }
    }
    public class _Quick_Reply
    {
        public string payload { set; get;}
    }
}