using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrismSample.Web.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/Values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var blog = Request.Cookies["custom_blog"];
            var youtube = Request.Cookies["custom_youtube"];
            Debug.WriteLine($"Request custom_blog : {blog}");
            Debug.WriteLine($"Request custom_youtube : {youtube}");

            return new string[] { blog, youtube };
        }

        // GET: api/Values/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        [HttpPost]
        public void Post([FromBody] IList<string> values)
        {
            if (values.Any() == false
                || values.Count != 2) return;

            Debug.WriteLine($"Request custom_blog : {Request.Cookies["custom_blog"]}");
            Debug.WriteLine($"Request custom_youtube : {Request.Cookies["custom_youtube"]}");

            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10)
            };
            Response.Cookies.Append("custom_blog", values.First(), option);
            Response.Cookies.Append("custom_youtube", values.Last(), option);
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
