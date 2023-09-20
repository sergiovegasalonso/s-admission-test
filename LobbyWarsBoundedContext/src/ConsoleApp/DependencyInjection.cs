using System.Reflection;
using FluentValidation;
using MediatR;
using SignaturitAdmissionTest.Application.Common.Behaviours;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetMinimumSignatureToWin;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetTrialWinner;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddConsoleAppServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ValidationBehaviour<,>)));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IRequestHandler<GetTrialWinnerQuery, WinnerResultDto>, GetTrialWinnerQueryHandler>();
            services.AddTransient<IRequestHandler<GetMinimumSignatureToWinQuery, MinimumSignatureToWinResultDto>, GetMinimumSignatureToWinQueryHandler>();
        });

        return services;
    }
}
