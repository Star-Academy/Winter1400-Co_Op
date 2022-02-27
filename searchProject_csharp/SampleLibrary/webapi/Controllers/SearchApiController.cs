using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchApiController : ControllerBase
    {
        private static readonly string _path = "../../files";

        [HttpGet("{query}")]
        public HashSet<int> Get(string query)
        {
            return new SampleLibrary.Controller().Run(_path, query);
         
        }
    }
}