using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Models.ResponseErrorViewModel
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Errors = new ResponseErrorMessages();
        }
        public string Title { get; set; }

        public int Status { get; set; }

        public ResponseErrorMessages Errors { get; set; }
    }
}
