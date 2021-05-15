using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fdevs_aula02.Models;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text.Json;
using fdevs_aula02.Commands;
using System.Linq;

namespace fdevs_aula02.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private ICollection<T> LoadData<T>()
        {
            using var openStream = System.IO.File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json"));
            return JsonSerializer.DeserializeAsync<ICollection<T>>(openStream, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }).Result;
        }
        private ICollection<T> WriteData<T>(ICollection<T> data)
        {
            using var createStream = System.IO.File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json"));
            var writedUser = JsonSerializer.SerializeAsync<ICollection<T>>(createStream,data, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return data;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = LoadData<Usuario>();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult getUser([FromRoute] Guid id)
        {
            var users = LoadData<Usuario>();
            var user = users.Where(user => user.Id == id);

            return Ok(user);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] UsuarioCommand command)
        {
            if (command == null)
                return BadRequest("kkkkkkkkkkk burrão n sabe fazer json lek");

            if (string.IsNullOrEmpty(command.Nome))
                throw new Exception("Nome do usuário obrigatório");


            var users = LoadData<Usuario>();
            var user = new Usuario()
            {
                Nome = command.Nome,
                Email = command.Email,
                Pontuacao = 0,
                Imagem = command.Imagem
            };

            user.Id = Guid.NewGuid();
            users.Add(user);
            WriteData<Usuario>(users);

            return Created("usuarios/{id}", user);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, UsuarioCommand command)
        {
            var users = LoadData<Usuario>();
            var user = users.Where(user => user.Id == id).FirstOrDefault();

            users.Remove(user);

            user.Nome = command.Nome;
            user.Email = command.Email;
            user.Imagem = command.Imagem;

            users.Add(user);

            WriteData(users);

            return NoContent();
        }

        [HttpDelete("/{id}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            var users = LoadData<Usuario>();
            users = users.Where(user => user.Id != id).ToList();

            WriteData(users);

            return NoContent();
        }
    }
}
