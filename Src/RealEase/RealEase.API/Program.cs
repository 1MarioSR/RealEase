using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using RealEase.Persistence.Context;
using RealEase.Infrastructure.Repositories;
using RealEase.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RealEaseDbContext>(p =>
    p.UseSqlServer(builder.Configuration.GetConnectionString("RealEaseStrConnection"))); 

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "AllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7146")
            .AllowAnyMethod()
            .AllowAnyHeader();
            //.SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPropertieRepository, PropertieRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

