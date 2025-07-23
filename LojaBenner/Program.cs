using LojaBenner.Contexts;
using LojaBenner.Infrastructure.Repositories;
using LojaBenner.Infrastructure.Repositories.Interfaces;
using LojaBenner.Infrastructure.UnitOfWork;
using LojaBenner.Infrastructure.UnitOfWork.Interfaces;
using LojaBenner.Services;
using LojaBenner.Services.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOpenApi();

builder.Services.AddDbContext<BennerContext>(opt =>
    opt.UseSqlite("Data Source=loja.db"));

builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 2.1 Descoberta de endpoints
builder.Services.AddEndpointsApiExplorer();   // 🔍 gera o /swagger/v1/swagger.json
builder.Services.AddControllers();
// 2.2 Geração + customização do Swagger
builder.Services.AddSwaggerGen(opt =>
{
    // Título, descrição e versão
    opt.SwaggerDoc("v1", new()
    {
        Title = "Loja API",
        Description = "API de demonstração com Pessoa, Produto e Pedido",
        Version = "v1"
    });

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "LojaBenner.xml");
    if (File.Exists(xmlPath))
        opt.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllAccess",
        policy =>
        {
            policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


var app = builder.Build();

app.UseCors("AllAccess");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();




app.Run();

