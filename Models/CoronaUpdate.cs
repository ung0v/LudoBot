using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webhooks.Models
{
    public class CoronaUpdate
    {
        public string success { set; get; }
        public _Data data { set; get; }

        public class _Data
        {
            public _Global global { set; get; }
            public _Vietnam vietnam { set; get; }
        }
        public class _Global
        {
            public string cases { set; get; }
            public string deaths { set; get; }
            public string recovered { set; get; }
        }
        public class _Vietnam
        {
            public string cases { set; get; }
            public string deaths { set; get; }
            public string recovered { set; get; }
        }

    }
}