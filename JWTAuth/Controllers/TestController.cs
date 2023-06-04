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
        [Route("Gehlgh_hgn")]
        public IActionResult AddUser([FromHeader] string xppkey)
        {
          
            return Ok(new { message = "Authenticated withoud jwt " + xppkey
            }) ;
        }

        [HttpPost]
        [Authorize]
        [Route("AddUser")]
        public String AddUser2(User user)
        {
            return "Authenticated with jwt " + user.Username;
        }
        [HttpPost]
        [Route("uploadfile")]
        [Authorize]
        public Response Upload([FromForm] FileModel fileModel)
        {
            Response response = new Response();
            try
            {
                string path = Path.Combine(@"D:\dotnet", fileModel.FileName);
                using(Stream stream=new FileStream(path,FileMode.Create))
                {
                    fileModel.File.CopyTo(stream);
                }
                response.StatusCode = 200;
                response.message = "Image updated successfully";

            }catch (Exception ex)
            {
                response.StatusCode = 500;
                response.message = ex.Message;
            }

            return response;
        }
    }
}
