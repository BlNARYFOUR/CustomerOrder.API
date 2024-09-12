using Microsoft.EntityFrameworkCore;
using CustomerOrder.API.Infrastructure.Data;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Infrastructure.Repositories;
using CustomerOrder.API.Application.Filters;
using CustomerOrder.API.Application.Mappers;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Services;
using CustomerOrder.API.Infrastructure.Services;
using FluentValidation;
using CustomerOrder.API.Application.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CustomerOrderContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerOrderContext") ?? throw new InvalidOperationException("Connection string 'CustomerOrderContext' not found.")));
builder.Services.AddControllers(options => {
    options.ReturnHttpNotAcceptable = true;
    options.Filters.Add<NotFoundExceptionFilter>();
    options.Filters.Add<ValidationExceptionFilter>();
});
builder.Services.AddProblemDetails();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = typeof(Program).Assembly;
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddTransient<ICustomerMapper, CustomerMapper>();
builder.Services.AddTransient<ICustomerListMapper, CustomerListMapper>();
builder.Services.AddTransient<IOrderMapper, OrderMapper>();
builder.Services.AddTransient<IOrderListMapper, OrderListMapper>();

builder.Services.AddTransient<IMailer, ConsoleMailer>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
