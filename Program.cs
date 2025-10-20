using Pessoas.API.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PessoaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PessoaContext") ?? throw new InvalidOperationException("Connection string 'PessoaContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    var scope = app.Services.CreateScope();
    var dbcontext = scope.ServiceProvider.GetRequiredService<PessoaContext>();
    dbcontext.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
