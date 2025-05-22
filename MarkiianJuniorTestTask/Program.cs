using Application.Handlers;
using BLL.Services;
using BLL.ServicesInterfaces;
using DAL.DbContextFactory;
using DAL.Interfaces;
using DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MediatR//
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetOnlyTvCustomersHandler).Assembly);
});
//----//

//Repositories//
builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//----//

//Servises//
builder.Services.AddScoped<ICustomerService, CustomerService>();
//----//

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
