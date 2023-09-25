using DevagramCSharp.Models;
using isragram_csharp;
using isragram_csharp.Repository;
using isragram_csharp.Repository.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//config for DB entity
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IsragramContext>(option => option.UseSqlServer(connectionString));
// escopo para inicialização dos repositories
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IFollowerRepository, FollowerRepositoryImpl>();
builder.Services.AddScoped<IPostRepository, PostRepositoryImpl>();
builder.Services.AddScoped<ICommentRepository, CommentRepositoryImpl>();

//config for JWT bearer token 
var cryptographyKey = Encoding.ASCII.GetBytes(JWTKey.SecretKey);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(autenticacao =>
{
    autenticacao.RequireHttpsMetadata = false;
    autenticacao.SaveToken = true;
    autenticacao.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(cryptographyKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
