using Microsoft.EntityFrameworkCore;
using Tardigrade.Api.Hubs;
using Tardigrade.Api.Services;
using Tardigrade.Data.Entities;
using Tardigrade.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// Database
builder.Services.AddDbContext<TardigradeDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("TardigradeDB")));
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddScoped<IWorldAnvilService, WorldAnvilService>();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200");
}));

builder.WebHost
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseIISIntegration()
    .UseStartup("Tardigrade.Api")
    .UseIISIntegration();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");
app.MapControllers();
app.MapHub<ChatHub>("/ChatHub");

app.Run();
