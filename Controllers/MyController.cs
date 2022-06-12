using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        IDbService db;
       [HttpGet]
       [Route("{id}")]
       public IActionResult GetMusician(int id)
        {
            var x = db.GetMusician(id);
            if (x != null)
            {
                return Ok(x);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteMusician(int id)
        {
            if(db.DeleteMusician(id))
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
        }
    }
}
