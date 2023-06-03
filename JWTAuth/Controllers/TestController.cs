using JWTAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public String GetData()
        {
            return "Authenticated with jwt";
        }
        [HttpGet]
        [Route("GetData2")]
        public String GetData2()
        {
            return "withoud un Authenticated with jwt";
        }

        [HttpPost]
        [Authorize]
        [Route("AddUser")]
        public String AddUser(User user)
        {
            return "Authenticated with jwt " + user.Username;
        }
    }
}
