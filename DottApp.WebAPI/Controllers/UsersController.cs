using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DottApp.Api.Rest.Request_Response;

namespace DottApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("GetUserInfo")]
        public UserInfo GetUserInfo(string sid)
        {
            return new UserInfo();
        }


    }
}
