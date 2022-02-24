using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchApiController : ControllerBase
    {
        private static readonly string _path = "../../files";

        [HttpGet("{query}")]
        public IEnumerable<int> Get(string query)
        {
            foreach(var item in new SampleLibrary.Controller().Run(_path, query))
            {
                yield return item;
            }
        }
    }
}