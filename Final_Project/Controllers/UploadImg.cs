using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    public class UploadImg : Controller
    {
        // to run add services.AddSingleton<UploadImgController>();
        private readonly IWebHostEnvironment webHostEnvironment;
        private IHostingEnvironment _hostingEnvironment;
        public UploadImg(IWebHostEnvironment _webHostEnvironment, IHostingEnvironment hostingEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
            _hostingEnvironment = hostingEnvironment;
        }



        [HttpPost]
        public async Task<IActionResult> UploadLogo([FromForm] IFormFile file)
        {
            var fileName = "";
            var fullPath = "";
            try
            {
                if (file.Length > 0)
                {
                    //string webRootPath = _hostingEnvironment.WebRootPath + "\\ProfileImages\\";
                    string webRootPath = _hostingEnvironment.WebRootPath + "\\img\\Uploads";
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    fullPath = Path.Combine(webRootPath, fileName);
                    using (FileStream fileStream = System.IO.File.Create(fullPath))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                    }
                }
                var imagePath = "/ProfileImages/" + fileName;
                // return Ok(imagePath)                               
                return Json(new { success = true, imageURL = fullPath, imagename = fileName});
            }
            catch (Exception ex)
            {
                //return Ok(fileName);
                return Json(new { Success = false, Message = ex.Message });
            }
        }




    }
}
