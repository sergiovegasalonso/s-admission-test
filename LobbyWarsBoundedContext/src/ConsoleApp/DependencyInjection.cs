using System.Reflection;
using MediatR;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetMinimumSignatureToWin;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetTrialWinner;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddConsoleAppServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IRequestHandler<GetTrialWinnerQuery, WinnerResultDto>, GetTrialWinnerQueryHandler>();
            services.AddTransient<IRequestHandler<GetMinimumSignatureToWinQuery, MinimumSignatureToWinResultDto>, GetMinimumSignatureToWinQueryHandler>();
        });

        return services;
    }
}
