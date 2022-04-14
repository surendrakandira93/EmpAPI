using EMP.Data;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {


            // Initialize the default app
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(DBKeys.GoogleCredential),
            });
            Console.WriteLine(defaultApp.Name); // "[DEFAULT]"

            // Retrieve services by passing the defaultApp variable...
            var defaultAuth = FirebaseAuth.GetAuth(defaultApp);

            // ... or use the equivalent shorthand notation
            defaultAuth = FirebaseAuth.DefaultInstance;

            var stream = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjZhNGY4N2ZmNWQ5M2ZhNmVhMDNlNWM2ZTg4ZWVhMGFjZDJhMjMyYTkiLCJ0eXAiOiJKV1QifQ.eyJuYW1lIjoiU3VyZW5kcmEgS2FuZGlyYSIsInBpY3R1cmUiOiJodHRwczovL2xoMy5nb29nbGV1c2VyY29udGVudC5jb20vYS0vQU9oMTRHZ0t1cldTODdKY043d3dRNkRRaWRfOG5pamZjNmlrdmtXQjV6U0JtZz1zOTYtYyIsImlzcyI6Imh0dHBzOi8vc2VjdXJldG9rZW4uZ29vZ2xlLmNvbS9jb21tb24tYXV0aC1jb3JlNSIsImF1ZCI6ImNvbW1vbi1hdXRoLWNvcmU1IiwiYXV0aF90aW1lIjoxNjQ5ODQzMjU1LCJ1c2VyX2lkIjoiNzZqa1A0VEFqZFl6b2JZeEtqNUU5c2c3WkVFMiIsInN1YiI6Ijc2amtQNFRBamRZem9iWXhLajVFOXNnN1pFRTIiLCJpYXQiOjE2NDk4NDMyNTUsImV4cCI6MTY0OTg0Njg1NSwiZW1haWwiOiJzdXJlbmRyYWthbmRpcmFAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsImZpcmViYXNlIjp7ImlkZW50aXRpZXMiOnsiZ29vZ2xlLmNvbSI6WyIxMTA5MDk5NjgxOTI5MjUwMzMxNzAiXSwiZW1haWwiOlsic3VyZW5kcmFrYW5kaXJhQGdtYWlsLmNvbSJdfSwic2lnbl9pbl9wcm92aWRlciI6Imdvb2dsZS5jb20ifX0.o2F6TrZIauAHL8hFY8ZcK9E8pa0WtuaoAaQYm-KnOS5251nhtAr6Q5SPQ_GS5WlB8htDokJ7G10NZwdqDZBKVstuwU3Cp52ZjLL9c1C4GJvppuaw2XfVR4r6zFUwgTrw9137FzctUc-XB2lsdiQh7MUqSvGJCrFKAH0Vm4VG41t1eVCRHp8E1fgJ8mLzVbMyf7LABNymTh7ElVNrYgZtJfs3nUinYcrTtFxyDRAFqOEyAJW4FBPqfjnd0_OdJA7CXwUmYDLOGT5r4YsAk9VkfgMbS3FxraO_hzSpzI3XbHv_D8yr56VAoIhk-AQiRWlNQr5XFaUbpZgRTwXE4GOGWw";
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;


            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
