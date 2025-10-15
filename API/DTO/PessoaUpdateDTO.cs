using System.ComponentModel.DataAnnotations;

namespace Pessoas.API.DTO
{
    public class PessoaUpdateDTO
    {   
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        public string? Email { get; set; }

        public string? Telefone { get; set; }

        [Range(1, 130, ErrorMessage = "A idade deve ser um valor entre 0 e 130.")]
        public int? Idade { get; set; }

        public string? Logradouro { get; set; }
        public int? Numero { get; set; }
        public string? Complemento { get; set; }
    }
}