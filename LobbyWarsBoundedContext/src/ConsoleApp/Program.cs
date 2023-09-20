using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetTrialWinner;

var services = new ServiceCollection();
services.AddConsoleAppServices();

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();

var query = new GetTrialWinnerQuery()
{
    PlaintiffSignatures = "KNV",
    DefendantSignatures = "KVV",
};

var response = await mediator.Send(query);
Console.WriteLine(response.Result);
Console.ReadLine();