using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fdevs_aula02.Models;
using Newtonsoft.Json;

namespace fdevs_aula02.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Quiz[] Get()
        {
            var json = System.IO.File.ReadAllText(@"./Controllers/quizzes.json");
            Quiz[] quiz = JsonConvert.DeserializeObject<Quiz[]>(json);

            return quiz;
        }
    }
}
