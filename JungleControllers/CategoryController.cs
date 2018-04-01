using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JungleStore.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class CategoryController : ControllerBase
    {
        //// GET: api/Default
        //[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Default/5
        [Produces("application/json")]
        [HttpGet("{id:int}", Name = "Get")]
        public IActionResult Get(int id,string query)
        {
            return Ok(new Value { Id=id,Text="value from " + id});
        }
        
        // POST: api/Default
        [HttpPost]
        public IActionResult Post([FromBody]Value value)
        {
            if (ModelState.IsValid==false)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtAction("Get", new { id = value.Id }, value);
        }
        
        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Value
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Text { get; set; }
    }
}
