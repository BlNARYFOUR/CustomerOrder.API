using Microsoft.EntityFrameworkCore;
using CustomerOrder.API.Infrastructure.Data;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CustomerOrderContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerOrderContext") ?? throw new InvalidOperationException("Connection string 'CustomerOrderContext' not found.")));
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true);
builder.Services.AddProblemDetails(options => options.CustomizeProblemDetails = ctx => {
    ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

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
