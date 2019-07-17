using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClaimsAPI.Data;
using ClaimsAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaimsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetClaims()
        {
            var claims = await _context.Claims.ToListAsync();
            return Ok(claims);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CrawfordClaim>> Get(int id)
        {
            var claim = await _context.Claims.FirstOrDefaultAsync(x=>x.Id==id);
            return Ok(claim);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
