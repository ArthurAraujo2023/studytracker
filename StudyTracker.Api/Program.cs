using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StudyTracker.Core.Interfaces;
using StudyTracker.Core.Services;
using StudyTracker.Infrastructure.Data;
using StudyTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<StudyTrackerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISessaoEstudoRepository, SessaoEstudoRepository>();
builder.Services.AddScoped<SessaoEstudoService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();