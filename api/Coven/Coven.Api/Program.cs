using Microsoft.EntityFrameworkCore;
using Coven.Api.Services;
using Coven.Data.Repository;
using Coven.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// Database
builder.Services.AddDbContext<CovenContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("CovenDB")));

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IWorldAnvilService, WorldAnvilService>();
builder.Services.AddScoped<IPineconeService, PineconeService>();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin();
}));

builder.WebHost
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseIISIntegration()
    .UseStartup("Coven.Api")
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

app.Run();
