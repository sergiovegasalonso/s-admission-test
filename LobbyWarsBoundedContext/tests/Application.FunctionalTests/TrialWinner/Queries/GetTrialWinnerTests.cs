using FluentAssertions;
using NUnit.Framework;
using SignaturitAdmissionTest.Application.Common.Exceptions;
using SignaturitAdmissionTest.Application.IntegrationTests;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetMinimumSignatureToWin;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetTrialWinner;

using static SignaturitAdmissionTest.Application.IntegrationTests.Testing;

namespace SignaturitAdmissionTest.Application.UnitTests.TrialWinner.Queries;

public class GetTrialWinnerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var query = new GetTrialWinnerQuery();

        await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireMinimunFielsNotEmpty()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = string.Empty,
            DefendantSignatures = string.Empty,
        };

        await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequirePlaintiffHasNotHashCharacter()
    {
        var query = new GetTrialWinnerQuery()
        {
            PlaintiffSignatures = "KNV#",
            DefendantSignatures = "KVV",
        };

        await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireDefendantHasNotHashCharacter()
    {
        var query = new GetTrialWinnerQuery()
        {
            PlaintiffSignatures = "KNV",
            DefendantSignatures = "K#VV",
        };

        await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldPlaintiffWinsTheTrial()
    {
        var query = new GetTrialWinnerQuery()
        {
            PlaintiffSignatures = "KNV",
            DefendantSignatures = "KVV",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Plaintinff wins the trial. Plaintinff 7 - 5 Defendant");
    }

    [Test]
    public async Task ShouldDefendantWinsTheTrial()
    {
        var query = new GetTrialWinnerQuery()
        {
            PlaintiffSignatures = "KK",
            DefendantSignatures = "VVVVVVVVVVV",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Defendant wins the trial. Defendant 11 - 10 Plaintiff");
    }

    [Test]
    public async Task ShouldTrialBeTie()
    {
        var query = new GetTrialWinnerQuery()
        {
            PlaintiffSignatures = "NNVVV",
            DefendantSignatures = "KNVV",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Trial is tie. Defendant 7 - 7 Plaintiff");
    }
}
