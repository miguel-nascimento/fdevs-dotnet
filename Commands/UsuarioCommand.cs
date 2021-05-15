using System.Collections.Generic;

namespace fdevs_aula02.Commands
{
    public class UsuarioCommand : BaseCommand
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Pontuacao { get; set; }
        public string Imagem { get; set; }
    }
}