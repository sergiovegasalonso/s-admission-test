using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetMinimumSignatureToWin;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetTrialWinner;

var services = new ServiceCollection();
services.AddConsoleAppServices();

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<ISender>();

var getTrialWinnerQuery = new GetTrialWinnerQuery()
{
    PlaintiffSignatures = "KNV",
    DefendantSignatures = "KVV",
};

try
{
    var firstResponse = await mediator.Send(getTrialWinnerQuery);
    Console.WriteLine(firstResponse.Result);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

var getMinimumSignatureToWinQuery = new GetMinimumSignatureToWinQuery()
{
    PlaintiffSignatures = "KNV#",
    DefendantSignatures = "KVV",
};

try
{
    var secondResponse = await mediator.Send(getMinimumSignatureToWinQuery);
    Console.WriteLine(secondResponse.Result);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.ReadLine();