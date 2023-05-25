
using JsonWebToken.Core.Entity;
using JsonWebToken.Core.IRepository;
using JsonWebToken.Core.IService;
using JsonWebToken.Core.IUnitOfWorks;
using JsonWebToken.Repository.Context;
using JsonWebToken.Repository.Repository;
using JsonWebToken.Service;
using JsonWebToken.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.Configure<Client>(builder.Configuration.GetSection("Clients"));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();


var tokenOption=builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();

builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer("Data Source=OLGUNPC\\SQLEXPRESS; Initial Catalog=JwtWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddIdentity<UserApp, UserRole>(x => { x.Password.RequireNonAlphanumeric = false; }).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AuthenticationExtension(tokenOption!);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
