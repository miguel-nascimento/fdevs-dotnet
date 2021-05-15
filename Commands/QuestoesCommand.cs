using System.Collections.Generic;

namespace fdevs_aula02.Commands
{
    public class QuestoesCommand
    {
        public string Pergunta { get; set; }
        public List<AlternativasCommand> Alternativas { get; set; }
    }
}
