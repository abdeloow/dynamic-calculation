using CosmenticFormulaApp.Application.Common.Interfaces;
using CosmenticFormulaApp.Domain.Repositories;
using CosmenticFormulaApp.Infrastructure.Data.Configuration;
using CosmenticFormulaApp.Infrastructure.Data;
using CosmenticFormulaApp.Infrastructure.Repositories;
using CosmenticFormulaApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CosmenticFormulaApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        services.AddScoped<IFormulaRepository, FormulaRepository>();
        services.AddScoped<IRawMaterialRepository, RawMaterialRepository>();
        services.AddScoped<ISubstanceRepository, SubstanceRepository>();

        services.AddScoped<IJsonParsingService, JsonParsingService>();

        services.Configure<ConnectionStringsOptions>(
            configuration.GetSection(ConnectionStringsOptions.SectionName));

        services.Configure<ImportSettings>(
            configuration.GetSection(ImportSettings.SectionName));

        services.Configure<FileProcessingOptions>(
            configuration.GetSection(FileProcessingOptions.SectionName));

        services.AddHostedService<FolderWatcherService>();

        return services;
    }
}