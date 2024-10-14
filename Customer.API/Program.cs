using Customer.API.Data;
using Customer.API.Mappings;
using Customer.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext for Customer database
builder.Services.AddDbContext<CustomerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerConnectionString"));
});

// Inject the Repositories to perform actions in dbContext
builder.Services.AddScoped<ICustomerRepository, SqlCustomerRepository>();

// Add Automapper third party to create mapping betweeen DTO and Domain model
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
