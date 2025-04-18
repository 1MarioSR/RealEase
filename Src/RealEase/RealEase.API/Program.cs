using Microsoft.EntityFrameworkCore;
using RealEase.Persistence.Context;
using RealEase.Infrastructure.Repositories;
using RealEase.Infrastructure.Interfaces;
using RealEase.Application.Services;
using RealEase.Infrastructure.Core;
using Umbraco.Core.Services;

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

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PropertieRepository>();
builder.Services.AddScoped<ContractRepository>();
builder.Services.AddScoped<PaymentRepository>();
builder.Services.AddScoped<VisitRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PropertieService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<VisitService>();
builder.Services.AddScoped<ContractService>();

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

