using FluentValidation;
using SignaturitAdmissionTest.Domain.Enums;

namespace SignaturitAdmissionTest.Application.TrialWinner.Queries.GetTrialWinner;

public class GetMinimumSignatureToWinQueryValidator : AbstractValidator<GetTrialWinnerQuery>
{
    public GetMinimumSignatureToWinQueryValidator()
    {
        RuleFor(x => x.PlaintiffSignatures)
            .NotNull()
            .NotEmpty()
            .WithMessage("PlaintiffSignatures cannot be empty.");

        RuleForEach(x => x.PlaintiffSignatures)
            .Must(x => Enum.GetValues(typeof(Role)).Cast<Role>().Select(r => (char)r).ToArray().Contains(x))
            .WithMessage("PlaintiffSignatures has one or more invalid value for Role enumeration.");

        RuleFor(x => x.DefendantSignatures)
            .NotNull()
            .NotEmpty()
            .WithMessage("DefendantSignatures cannot be empty.");

        RuleForEach(x => x.DefendantSignatures)
            .Must(x => Enum.GetValues(typeof(Role)).Cast<Role>().Select(r => (char)r).ToArray().Contains(x))
            .WithMessage("DefendantSignatures has one or more invalid value for Role enumeration.");
    }
}
