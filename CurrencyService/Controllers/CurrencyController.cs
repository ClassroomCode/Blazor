using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        [HttpGet]
        public CurrencyInfo Get(string code)
        {
            var info = new CurrencyInfo() { Code = code };
            info.Rate = code.GetHashCode();
            return info;
        }
    }

    public class CurrencyInfo
    {
        public string Code { get; set; }
        public double Rate { get; set; }
    }
}
