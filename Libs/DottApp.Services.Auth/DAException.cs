using System;
using System.Collections.Generic;
using System.Text;

namespace DottApp.Services.Auth
{
    public class DAException
    {
        public int ErrCode { get; set; }

        public string ErrMessage { get; set; }

        public DAException(int errCode, string errMessage = "Errors happen :/")
        {
            ErrCode = errCode;
            ErrMessage = errMessage;
        }
    }
}
