using System.ComponentModel.DataAnnotations;

namespace Pessoas.API.DTO
{
    public class PessoaCreateDTO
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O Idade é obrigatório.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O Número é obrigatório.")]
        public int Numero { get; set; }

        public string? Complemento { get; set; }
    }
}