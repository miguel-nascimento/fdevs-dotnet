using Microsoft.AspNetCore.Mvc;
using fdevs_aula02.Models;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using fdevs_aula02.Commands;

namespace fdevs_aula02.Controllers
{
    [ApiController]
    [Route("[quizzes]")]
    public class QuizController : ControllerBase
    {
        private ICollection<T> LoadData<T>()
        {
            using var openStream = System.IO.File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "quizzes.json"));
            return JsonSerializer.DeserializeAsync<ICollection<T>>(openStream, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }).Result;
        }
        private ICollection<T> WriteData<T>(ICollection<T> data)
        {
            using var createStream = System.IO.File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "quizzes.json"));
            var writedUser = JsonSerializer.SerializeAsync<ICollection<T>>(createStream, data, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return data;
        }

        [HttpGet]
        public IActionResult GetQuizzes()
        {
            var quizzes = LoadData<Quiz>();

            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuiz([FromRoute] Guid id)
        {
            var quizzes = LoadData<Quiz>();
            var quiz = quizzes.Where(quiz => quiz.Id == id);

            return Ok(quiz);
        }

        [HttpPost]
        public IActionResult PostQuiz([FromBody] QuizCommand command)
        {
            if (command == null)
                return BadRequest("mo burrão errou no json kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");

            var quizzes = LoadData<Quiz>();
            var quizz = new Quiz()
            {
                Id = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid(),
                CursoId = Guid.NewGuid(),
                Codigo = command.Codigo,
            };

            quizz.Id = Guid.NewGuid();
            quizzes.Add(quizz);
            WriteData(quizzes);

            return Created("quizzes/{id}", quizz);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, QuizCommand command)
        {
            var quizzes = LoadData<Quiz>();
            var quizz = quizzes.Where(quizz => quizz.Id == id).FirstOrDefault();
            
            // KKKKKKKKKKKKKKKKKKKKK MELHOR UPDATE
            quizzes.Remove(quizz);

            quizz.Id = command.Id;
            quizz.UsuarioId = command.UsuarioId;
            quizz.CursoId = command.CursoId;
            quizz.Codigo = command.Codigo;

            quizzes.Add(quizz);

            WriteData(quizzes);

            return NoContent();
        }

        [HttpDelete("/{id}")]
        public IActionResult DeleteQuiz([FromRoute] Guid id)
        {
            var quizzes = LoadData<Quiz>();
            quizzes = quizzes.Where(quizz => quizz.Id != id).ToList();

            WriteData(quizzes);

            return NoContent();
        }
    }
}
