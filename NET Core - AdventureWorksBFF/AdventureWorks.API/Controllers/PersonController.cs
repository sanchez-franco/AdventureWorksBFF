using AdventureWorks.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AdventureWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : BaseController
    {
        private readonly IWebApiProxy _webApiProxy;

        public PersonController(IWebApiProxy webApiProxy)
        {
            _webApiProxy = webApiProxy;
        }

        [HttpGet]
        public async Task<ActionResult<PersonDetail[]>> Get()
        {
            try
            {
                var token = GetToken();
                var response = await _webApiProxy.GetPersonDetails(token);

                if (response == null)
                    return NotFound();

                return response;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> GetVersion2()
        {
            try
            {
                var token = GetToken();
                var response = await _webApiProxy.GetPersonDetails(token);

                if (response == null)
                    return NotFound();

                var wrapper = new
                {
                    response.Length,
                    Persons = response
                };

                return wrapper;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("{personId:int}")]
        public async Task<ActionResult<PersonDetail>> Get(int personId)
        {
            try
            {
                var token = GetToken();
                var response = await _webApiProxy.GetPersonDetail(token, personId);

                if (response == null)
                    return NotFound();

                return response;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
