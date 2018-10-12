using System;

namespace ConsoleApp.Models
{
    public class PessoaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public DateTime Criado { get; set; }
    }
}
