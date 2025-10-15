using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pessoas.API.Data;
using Pessoas.API.DTO;
using Pessoas.API.Models;

namespace Pessoas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly PessoaContext _context;

        public PessoasController(PessoaContext context)
        {
            _context = context;
        }

        // GET: api/Pessoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaResponseDTO>>> GetPessoas()
        {
            var pessoas = await _context.Pessoas.ToListAsync();
            var pessoasDTO = pessoas.Select(p => new PessoaResponseDTO(p));
            return Ok(pessoasDTO);
        }

        // GET: api/Pessoas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaResponseDTO>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return new PessoaResponseDTO(pessoa);
        }

        // PUT: api/Pessoas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, PessoaUpdateDTO dadosAtualizados)
        {
            var pessoaDB = await _context.Pessoas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (pessoaDB == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dadosAtualizados.Email) && dadosAtualizados.Email != pessoaDB.Email)
            {
                if (await _context.Pessoas.AnyAsync(p => p.Email == dadosAtualizados.Email))
                {
                    return Conflict("O e-mail informado já está em uso por outro usuário.");
                }
            }

            var telefoneLimpo = RemoverFormatacao(dadosAtualizados.Telefone);
            if (!string.IsNullOrEmpty(telefoneLimpo) && telefoneLimpo != pessoaDB.Telefone)
            {
                if (await _context.Pessoas.AnyAsync(p => p.Telefone == telefoneLimpo))
                {
                    return Conflict("O telefone informado já está em uso por outro usuário.");
                }
            }

            pessoaDB.Nome = dadosAtualizados.Nome ?? pessoaDB.Nome;
            pessoaDB.Email = dadosAtualizados.Email ?? pessoaDB.Email;
            pessoaDB.Telefone = telefoneLimpo ?? pessoaDB.Telefone;
            pessoaDB.Idade = dadosAtualizados.Idade ?? pessoaDB.Idade;
            pessoaDB.Logradouro = dadosAtualizados.Logradouro ?? pessoaDB.Logradouro;
            pessoaDB.Numero = dadosAtualizados.Numero ?? pessoaDB.Numero;
            pessoaDB.Complemento = dadosAtualizados.Complemento ?? pessoaDB.Complemento;

            _context.Pessoas.Update(pessoaDB);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new PessoaResponseDTO(pessoaDB));
        }

        // POST: api/Pessoas
        [HttpPost]
        public async Task<ActionResult<PessoaResponseDTO>> PostPessoa(PessoaCreateDTO pessoaDTO)
        {
            var cpfLimpo = RemoverFormatacao(pessoaDTO.CPF);
            var telefoneLimpo = RemoverFormatacao(pessoaDTO.Telefone);

            if (await _context.Pessoas.AnyAsync(p => p.CPF == cpfLimpo))
            {
                return Conflict("CPF já cadastrado.");
            }
            if (await _context.Pessoas.AnyAsync(p => p.Telefone == telefoneLimpo))
            {
                return Conflict("Telefone já cadastrado.");
            }
            if (await _context.Pessoas.AnyAsync(p => p.Email == pessoaDTO.Email))
            {
                return Conflict("E-mail já cadastrado.");
            }

            var pessoa = new Pessoa
            {
                Nome = pessoaDTO.Nome,
                Email = pessoaDTO.Email,
                Telefone = telefoneLimpo, 
                CPF = cpfLimpo,           
                Idade = pessoaDTO.Idade,
                Logradouro = pessoaDTO.Logradouro,
                Numero = pessoaDTO.Numero,
                Complemento = pessoaDTO.Complemento
            };

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.Id }, new PessoaResponseDTO(pessoa));
        }

        // DELETE: api/Pessoas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }

    
        private static string RemoverFormatacao(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return valor;
            return System.Text.RegularExpressions.Regex.Replace(valor, @"[^\d]", "");
        }
    }
}
