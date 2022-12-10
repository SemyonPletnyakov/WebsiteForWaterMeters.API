using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteForWaterMeters.API.EFCore.Tables;
using WebsiteForWaterMeters.API.Services;

namespace WebsiteForWaterMeters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly CheckServices checkServices;
        public CheckController(CheckServices cs)
        {
            checkServices = cs;
        }
        //тест коммент 2
        [HttpPost]
        public int? RegisterCheck(int id)
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            Response.Headers["Access-Control-Allow-Headers"] = "*";

            return checkServices.RegisterCheck(id);
        }
        [HttpGet]
        public Check GetCheck(int hash)
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            Response.Headers["Access-Control-Allow-Headers"] = "*";

            return checkServices.GetCheck(hash);
        }
    }
}
