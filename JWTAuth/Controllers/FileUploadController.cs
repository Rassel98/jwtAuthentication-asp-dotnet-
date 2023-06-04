using JWTAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace JWTAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        [HttpPost]
        [Route("uploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file,CancellationToken cancellationToken)
        {
            var result = await Writefile(file);
            return Ok(result);
        }
        private async Task<string>Writefile(IFormFile file)
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
              //  filename += extension;
                filename = "uploadfile"+ extension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
                if(!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                var exactPath= Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files",filename);

                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                   await file.CopyToAsync(stream);
                }

            }
            catch (Exception ex)
            {

            }
            return filename;
        }

        [HttpGet]
        [Route("Download")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            var exactPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(filename, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes=await System.IO.File.ReadAllBytesAsync(exactPath);
            return File(bytes, contentType,Path.GetFileName(filename));
        }


    }
}
