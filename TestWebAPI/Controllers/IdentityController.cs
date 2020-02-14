using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestWebAPI.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(User.Claims.Select(claims => new { claims.Type, claims.Value }));
        }

    }
}