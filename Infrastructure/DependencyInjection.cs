using Core.DTOs.RequestLoan;
using Core.DTOs.SimulateLoan;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Auth;
using Core.JWT;
using FluentValidation;
using Infrastructure.Auth;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Validation.Loan;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuth(configuration);
        services.AddDatabase(configuration);
        services.AddMapping();
        services.AddRepositories();
        services.AddServices();
        services.AddValidations();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        configuration.GetSection("JWT").Get<JwtOptions>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!))
            };
        });

        services.AddTransient<JwtProvider>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BankManagement");

        service.AddDbContext<AplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return service;
    }

    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<ISimulateLoanRepository, SimulateLoanRepository>();
        services.AddScoped<ILoanRequestRepository, LoanRequestRepository>();
        services.AddScoped<IApproveLoanRepository, ApproveLoanRepository>();
        services.AddScoped<ILoanDetailedRepository, LoanDetailedRepository>();
        services.AddScoped<ILoanPaymentRepository, LoanPaymentRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBankService, BankService>();
        services.AddScoped<ISimulateLoanService, SimulateLoanService>();

        return services;
    }

    private static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RequestLoanDTO>, RequestLoanValidation>();
        services.AddScoped<IValidator<LoanSimulate>, LoanSimulateValidation>();

        return services;
    }
}
