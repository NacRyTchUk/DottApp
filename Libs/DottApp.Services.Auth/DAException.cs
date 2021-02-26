using System;
using System.Collections.Generic;
using System.Text;

namespace DottApp.Services.Auth
{
    public enum DAExceptionType
    {
        UnknownError = -1,
        MethodIsNotImplemented,
        BadRequestParameters,
        UnknownMethod,
        BadToken,
        BadPublicKey
    }
    public class DAException
    {
        public DAExceptionType ErrCode { get; set; }
        public string ErrType { get; set; }
        public string ErrMessage { get; set; }

        public DAException(DAExceptionType errCode, string errMessage = "Errors happen :/")
        {
            ErrCode = errCode;
            ErrType = errCode.ToString();
            ErrMessage = errMessage;
        }
    }
}
