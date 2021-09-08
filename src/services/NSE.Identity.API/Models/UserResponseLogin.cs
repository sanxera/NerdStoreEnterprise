using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Identity.API.Models
{
    public class UserResponseLogin
    {
        public string AccessToken { get; set; }

        public double ExpiresIn { get; set; }

        public UserToken UserToken { get; set; }
    }
}
