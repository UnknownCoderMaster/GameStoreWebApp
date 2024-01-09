using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Data.Repositories;
using GameStoreWebApp.Domain.Entities.Addresses;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.Interfaces.Games;
using GameStoreWebApp.Service.Interfaces.Rates;
using GameStoreWebApp.Service.Interfaces.Users;
using GameStoreWebApp.Service.Services.Games;
using GameStoreWebApp.Service.Services.Rates;
using GameStoreWebApp.Service.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Text;

namespace GameStoreWebApp.API.Extensions;

public static class ServiceExtensions
{
	public static void AddCustomServices(this IServiceCollection services)
	{
		// repositories
		services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
		services.AddScoped<IGenericRepository<Verification>, GenericRepository<Verification>>();
		services.AddScoped<IGenericRepository<Region>, GenericRepository<Region>>();
		services.AddScoped<IGenericRepository<Country>, GenericRepository<Country>>();
		services.AddScoped<IGenericRepository<Game>, GenericRepository<Game>>();
		services.AddScoped<IGenericRepository<Feedback>, GenericRepository<Feedback>>();
		services.AddScoped<IGenericRepository<Rate>, GenericRepository<Rate>>();

		// services
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IRateService, RateService>();
		services.AddScoped<IGameService, GameService>();
	}

	public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = false,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = configuration["JWT:ValidIssuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

			};
		});
	}

	public static void AddSwaggerService(this IServiceCollection services)
	{
		services.AddSwaggerGen(p =>
		{
			p.ResolveConflictingActions(ad => ad.First());
			p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
			});

			p.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme()
						{
							Reference = new OpenApiReference()
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
					}
				});
		});
	}
}
