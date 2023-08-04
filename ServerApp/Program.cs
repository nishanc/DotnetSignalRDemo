using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
var corsPolicy = "AllowAngularOrigins";
// Add services to the container.

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        b =>
        {
            b.WithOrigins(
                    "http://localhost:4200"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add Db Context
builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var value = builder.Configuration.GetSection("AppSettings:Token").Value;
if (value != null)
{
    var key = Encoding.ASCII.GetBytes(value);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
