using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestWebAPI.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public string Get()
        {
            return "Identity Get";
        }

    }
}