using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pessoas.API.Models;

namespace Pessoas.API.Data
{
    public class PessoaContext : DbContext
    {
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options) { }
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}