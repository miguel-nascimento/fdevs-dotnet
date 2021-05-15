using System;
using System.Collections.Generic;

namespace fdevs_aula02.Models
{
    public class Questoes : Base
    {
        public string Pergunta { get; set; }
        public List<Alternativas> Alternativas { get; set; }
    }
}
