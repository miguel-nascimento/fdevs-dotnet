using System;
using System.Collections.Generic;

namespace fdevs_aula02.Commands
{
    public class QuizCommand : BaseCommand
    {
        public Guid UsuarioId { get; set; }
        public Guid CursoId { get; set; }
        public List<QuestoesCommand> Questoes { get; set; }
    }
}
