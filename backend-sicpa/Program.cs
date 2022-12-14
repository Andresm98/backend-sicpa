using backend_sicpa.DbRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScottDbContext>(_ =>
{
    _.UseMySql("server=sql396.main-hosting.eu;port=3306;database=u304374569_sicpa_project;uid=u304374569_sicpa_project;password=Sicpa-project12$", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.12-mariadb"));
});

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
