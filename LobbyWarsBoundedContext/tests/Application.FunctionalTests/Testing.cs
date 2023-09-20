using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace SignaturitAdmissionTest.Application.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    [OneTimeSetUp]
    public void RunBeforeAnyTests() { }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests() { }
}
