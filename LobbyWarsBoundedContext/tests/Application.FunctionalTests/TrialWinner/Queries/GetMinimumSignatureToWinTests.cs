using FluentAssertions;
using NUnit.Framework;
using SignaturitAdmissionTest.Application.Common.Exceptions;
using SignaturitAdmissionTest.Application.IntegrationTests;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetMinimumSignatureToWin;

using static SignaturitAdmissionTest.Application.IntegrationTests.Testing;

namespace SignaturitAdmissionTest.Application.UnitTests.TrialWinner.Queries;

public class GetMinimumSignatureToWinTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var query = new GetMinimumSignatureToWinQuery();

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
    public async Task ShouldRequireAtLeastOnePartyHasHashCharacter()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "KNV",
            DefendantSignatures = "KVV",
        };

        await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireOnlyOnePartyHasHashCharacter()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "KNV#",
            DefendantSignatures = "KVV#",
        };

        await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldPlaintiffNeedsAValidatorsSignatureToWin()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "NN#",
            DefendantSignatures = "NN",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Plaintiff needs a Validator's signature to win the trial");
    }

    [Test]
    public async Task ShouldPlaintiffNeedsANotarysSignatureToWin()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "KN#",
            DefendantSignatures = "KN",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Plaintiff needs a Notary's signature to win the trial");
    }

    [Test]
    public async Task ShouldPlaintiffNeedsAKingsSignatureToWin()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "#",
            DefendantSignatures = "VVN",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Plaintiff needs a King's signature to win the trial");
    }

    [Test]
    public async Task ShouldPlaintiffCannotWinTheTrial()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "K#NV",
            DefendantSignatures = "KKKKK",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Plaintiff can't win the trial with only one more signature");
    }

    [Test]
    public async Task ShouldDefendantNeedsAValidatorsSignatureToWin()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "VV",
            DefendantSignatures = "VV#",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Defendant needs a Validator's signature to win the trial");
    }

    [Test]
    public async Task ShouldDefendantNeedsANotarysSignatureToWin()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "VVV",
            DefendantSignatures = "VV#",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Defendant needs a Notary's signature to win the trial");
    }

    [Test]
    public async Task ShouldDefendantNeedsAKingsSignatureToWin()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "KNV",
            DefendantSignatures = "K#VV",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Defendant needs a King's signature to win the trial");
    }

    [Test]
    public async Task ShouldDefendantCannotWinTheTrial()
    {
        var query = new GetMinimumSignatureToWinQuery()
        {
            PlaintiffSignatures = "KKKKK",
            DefendantSignatures = "K#NV",
        };

        var result = await SendAsync(query);

        result.Result?.Should().Be("Defendant can't win the trial with only one more signature");
    }
}
