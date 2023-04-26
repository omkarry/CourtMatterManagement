using CourtMatterManagement.Data.DbContexts;
using CourtMatterManagement.Service.Interfaces;
using CourtMatterManagement.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(o => o.AddPolicy("ReactPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

          // Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CourtMatterDbContext>(options =>
    options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IClientRepository, ClientService>();
builder.Services.AddScoped<IAttorneyRepository, AttorneyService>();
builder.Services.AddScoped<IMatterRepository, MatterService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceService>();
builder.Services.AddScoped<IJurisdictionRepository, JurisdictionService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
