using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string GetToken()
        {
            // Get the access token.
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString();
            return accessToken;
        }
    }
}