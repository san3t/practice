using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int ind, string name)
        {
            if (ind < 0 || ind >= Summaries.Count)
            {
                return BadRequest("Нет такого индекса!");
            }

            Summaries[ind] = name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int ind)
        {
            if (ind < 0 || ind >= Summaries.Count)
            {
                return BadRequest("Нет такого индекса!");
            }

            Summaries.RemoveAt(ind);
            return Ok();
        }

        [HttpGet("find-by-index")]
        public IActionResult FindWithInd(int ind)
        {
            if (ind < 0 || ind >= Summaries.Count)
            {
                return BadRequest("Нет такого индекса!");
            }

            return Ok(Summaries[ind]);
        }

        [HttpGet("find-by-name")]
        public IActionResult FindByName(string name)
        {
            int count = 0;

            for (int i = 0; i < Summaries.Count; i++)
            {
                if (Summaries[i].ToLower().Trim() == name.ToLower().Trim())
                {
                    count++;
                }
            }

            return Ok(count);
        }

        [HttpGet("get-with-sort")]
        public IActionResult GetWithSort(int? sort)
        {
            if(sort == null)
            {
                return Ok(Summaries);
            }

            else if(sort == 1)
            {
                Summaries.Sort();
                return Ok(Summaries);
            }

            else if(sort == -1)
            {
                Summaries.Sort();
                Summaries.Reverse();

                return Ok(Summaries);
            }
            else
            {
                return BadRequest("Нет такого варианта сортировки!");
            }
        }
    }
}