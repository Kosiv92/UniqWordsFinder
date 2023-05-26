using FileSupport.Core;
using Microsoft.AspNetCore.Mvc;
using WebInterractionLib;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextController : ControllerBase
    {
        [HttpGet]
        public ActionResult IsAlive()
        {
            return Ok("Web service is running");
        }

        [HttpPost]
        public ActionResult<WordsDto> GetUniqueWords([FromBody] StringsDto requestObject)
        {
            var dataHandler = new RawDataHandler(requestObject.Strings);
            
            var dictionary = dataHandler.HandleData();

            var response = new WordsDto();

            response.WordsCount = dictionary;

            return response;
        }
    }
}