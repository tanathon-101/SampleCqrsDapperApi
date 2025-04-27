using Microsoft.OpenApi.Models;
using MediatR;
using SampleCqrsDapperApi.Domain.Interfaces;
using SampleCqrsDapperApi.Infrastructure.Repositories;
using SampleCqrsDapperApi.Application.Commands;
using FluentValidation.AspNetCore;
using SampleCqrsDapperApi.Application.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection
builder.Services.AddMediatR(typeof(CreateCustomerCommand).Assembly);
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddControllers();;
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>();
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SampleCqrsDapperApi",
        Version = "v1",
        Description = "CQRS + Dapper Minimal API Sample"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleCqrsDapperApi v1");
        c.RoutePrefix = string.Empty; // เปิดหน้า Swagger ที่ root เลย
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
