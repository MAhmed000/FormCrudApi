using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using AutoMapper;
//using EmployeeM.data.Models;
using EmployeeM.data.MapperProfile;
using EmployeeBLL.BLL.Interfaces;
using EmployeeBLL.BLL.Repositories;
using EmployeeM.data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(opt=>opt.SerializerSettings.ContractResolver=new DefaultContractResolver());
builder.Services.AddDbContext<Employee_DbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConStr"));
});

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddScoped<IEmployee, EmployeeService>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin", option =>
    {
        option.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors(opt=>opt.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

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
