﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaRest.Controllers 
{
    class UserController : BaseController 
    {
        public override string Parse (string method, NameValueCollection paramters) 
        {
            return "HELLO USERS";
        }
    }
}
