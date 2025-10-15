namespace Pessoas.API.Models
{
    public class Pessoa
    {
        public int Id { get; set; } 
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public string Telefone { get; set; } 
        public string CPF { get; set; }
        public int Idade { get; set; }
        public string Logradouro { get; set; } 
        public int Numero { get; set; } 
        public string Complemento { get; set; } 

    }
}