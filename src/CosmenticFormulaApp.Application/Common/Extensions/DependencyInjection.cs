﻿using CosmenticFormulaApp.Application.Common.Behavior;
using CosmenticFormulaApp.Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CosmenticFormulaApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<ICostCalculationService, CostCalculationService>();
        services.AddScoped<ISubstanceAnalysisService, SubstanceAnalysisService>();
        services.AddScoped<IFormulaValidationService, FormulaValidationService>();

        return services;
    }
}