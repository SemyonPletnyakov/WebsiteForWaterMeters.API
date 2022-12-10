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
    public class MetersController : ControllerBase
    {
        private readonly MetersService _metersService;
        public MetersController(MetersService metersService)
        {
            _metersService = metersService;
        }
        [HttpGet]
        public Person GetUserById(int id)
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            Response.Headers["Access-Control-Allow-Headers"] = "*";
            
            return _metersService.GetMetersById(id);
        }
        [HttpGet("Adres")]
        public Person GetUserByAdres(string gorod, string ulica, string dom, string kvartira)
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            Response.Headers["Access-Control-Allow-Headers"] = "*";

            return _metersService.GetUserByAdres(gorod, ulica, dom, kvartira);
        }
        [HttpGet("Goroda")]
        public List<string> GetGoroda()
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            Response.Headers["Access-Control-Allow-Headers"] = "*";

            return _metersService.GetGoroda();
        }
        [HttpGet("Ulici")]
        public List<string> GetUlici(string gorod)
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            Response.Headers["Access-Control-Allow-Headers"] = "*";

            return _metersService.GetUlici(gorod);
        }
        [HttpGet("Doma")]
        public List<string> GetDoma(string gorod, string ulica)
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            Response.Headers["Access-Control-Allow-Headers"] = "*";

            return _metersService.GetDoma(gorod, ulica);
        }
        [HttpGet("Kvartiry")]
        public List<string> GetKvartiry(string gorod, string ulica, string dom)
        {
            Response.Headers["Access-Control-Allow-Origin"] = "*";
            Response.Headers["Access-Control-Allow-Headers"] = "*";

            return _metersService.GetKvartiry(gorod, ulica, dom);
        }
    }
}
