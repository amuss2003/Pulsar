using System;
using System.Collections.Generic;
using System.Web;

namespace GlobalInfoProtocol.Authentication
{
    public class RequestAuthentication
    {
        //TODO: Do proper authentication
        public static bool Authenticate(HttpRequest request)
        {
            var loginKey = request["LoginKey"];
            return loginKey != null && loginKey == "xezp3avnniqyjf45wso0ot45";
        }
    }
}