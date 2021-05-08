using System;
using System.Collections.Generic;

namespace fdevs_aula02.Models
{
    public class Quiz
    {
        public Guid UsuarioId { get; set; }
        public Guid CursoId { get; set; }
        public List<Questoes> Questoes { get; set; }
    }
}
