using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteForWaterMeters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public VirtualFileResult GetImg()
        {
            string path = "imgTest1.jpg";
            return File(path, "image/jpeg");
        }
        /*[HttpGet]
        public VirtualFileResult[] GetImg()
        {
            string path = "imgTest1.jpg";
            return new VirtualFileResult[] { File(path, "image/jpeg"), File(path, "image/jpeg") };
        }*/
    }
}
