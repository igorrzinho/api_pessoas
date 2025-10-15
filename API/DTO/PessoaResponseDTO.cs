namespace Pessoas.API.DTO
{
    public class PessoaResponseDTO
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

      
        private static string FormatarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                return cpf; 
            }
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

 
        private static string FormatarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone) || !telefone.All(char.IsDigit))
            {
                return telefone; 
            }

            if (telefone.Length == 11)
            {
                return Convert.ToUInt64(telefone).ToString(@"\(00\) 00000\-0000");
            }

            if (telefone.Length == 10)
            {
                return Convert.ToUInt64(telefone).ToString(@"\(00\) 0000\-0000");
            }

            return telefone; 
        }

        
        public PessoaResponseDTO(Models.Pessoa pessoa)
        {
            Id = pessoa.Id;
            Nome = pessoa.Nome;
            Email = pessoa.Email;
            Telefone = FormatarTelefone(pessoa.Telefone);
            CPF = FormatarCPF(pessoa.CPF);
            Idade = pessoa.Idade;
            Logradouro = pessoa.Logradouro;
            Numero = pessoa.Numero;
            Complemento = pessoa.Complemento;
        }
    }
}