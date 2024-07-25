using GameStoreWebApp.API.Extensions;
using GameStoreWebApp.API.Helpers;
using GameStoreWebApp.API.Middlewares;
using GameStoreWebApp.Data.Contexts;
using GameStoreWebApp.Service.Helpers;
using GameStoreWebApp.Service.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddControllers(options =>
{
	options.Conventions.Add(new RouteTokenTransformerConvention(
								 new ConfigureApiUrlName()));
});

builder.Services.AddControllers().AddJsonOptions(x =>
{
	// serialize enums as strings in api responses (e.g. Role)
	x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

	// ignore omitted parameters on models to enable optional params (e.g. User update)
	x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument();

// builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<GameStoreDbContext>(option =>
	option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();

// Add custom services
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddCustomServices();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseOpenApi();
	app.UseReDoc(options =>
    {
        options.Path = "/redoc";
    });
}

app.UseStaticFiles();


//Set helpers
EnvironmentHelper.WebRootPath = app.Services.GetRequiredService<IWebHostEnvironment>()?.WebRootPath;

if (app.Services.GetService<IHttpContextAccessor>() != null)
	HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseMiddleware<GameStoreExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();