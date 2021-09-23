using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Models.ResponseErrorViewModel
{
    public class ResponseErrorMessages
    {
        public ResponseErrorMessages()
        {
            Messages = new List<string>();
        }

        public List<string> Messages { get; set; }
    }
}
