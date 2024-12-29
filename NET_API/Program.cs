using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NET_API.DbConnect;
using NET_API.Mappings;
using NET_API.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbConnectApp>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectString")));

builder.Services.AddScoped<IRegionRepository, IMPRegionRepository>();
builder.Services.AddScoped<IWalkRepository, IMPWalkRepository>();


builder.Services.AddAutoMapper(typeof(AutoRegionMapperFrofiles));


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
