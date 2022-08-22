using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ProcessPensionAPI.Filters;
using ProcessPensionAPI.Model;
using ProcessPensionAPI.Provider;
using System;

namespace ProcessPensionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProcessPensionAuthorization]
    public class ProcessPensionController : ControllerBase
    {
        readonly ILog _log;
        readonly IRequestProvider _provider;
        public ProcessPensionController(IRequestProvider provider)
        {
            _log = LogManager.GetLogger(typeof(ProcessPensionController));
            _provider = provider;
        }

        [HttpPost()]
        public IActionResult Post(ProcessPensionInput input)
        {
            _log.Info("Http Get Process Request");

            try
            {
                var token = this.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                return Ok(_provider.ProcessPension(input, token));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
