using Microsoft.EntityFrameworkCore;
using musicSchool.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Connection string - update antes de ejecutar
builder.Configuration["ConnectionStrings:DefaultConnection"] = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection") ?? "Server=.;Database=MusicSchoolDb;Trusted_Connection=True;MultipleActiveResultSets=true";

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MusicSchoolDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
