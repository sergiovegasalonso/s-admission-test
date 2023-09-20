using FluentValidation;
using SignaturitAdmissionTest.Domain.Enums;

namespace SignaturitAdmissionTest.Application.TrialWinner.Queries.GetMinimumSignatureToWin;

public class GetMinimumSignatureToWinQueryValidator : AbstractValidator<GetMinimumSignatureToWinQuery>
{
    public GetMinimumSignatureToWinQueryValidator()
    {
        RuleFor(x => x.PlaintiffSignatures)
            .NotNull()
            .NotEmpty()
            .WithMessage("PlaintiffSignatures cannot be empty.");

        RuleForEach(x => x.PlaintiffSignatures)
            .Must(x => Enum.GetValues(typeof(Role)).Cast<Role>().Select(r => (char)r).ToArray().Contains(x) || x == (char)Character.Hash)
            .WithMessage("PlaintiffSignatures has one or more invalid value for Role enumeration.");

        RuleFor(x => x.DefendantSignatures)
            .NotNull()
            .NotEmpty()
            .WithMessage("DefendantSignatures cannot be empty.");

        RuleForEach(x => x.DefendantSignatures)
            .Must(x => Enum.GetValues(typeof(Role)).Cast<Role>().Select(r => (char)r).ToArray().Contains(x) || x == (char)Character.Hash)
            .WithMessage("DefendantSignatures has one or more invalid value for Role enumeration.");

        RuleFor(x => x)
            .Must(x =>
                (x.PlaintiffSignatures.Count(c => c == (char)Character.Hash) == 1 && x.DefendantSignatures.Count(c => c == (char)Character.Hash) == 0) ||
                (x.DefendantSignatures.Count(c => c == (char)Character.Hash) == 1 && x.PlaintiffSignatures.Count(c => c == (char)Character.Hash) == 0)
            )
            .WithMessage("DefendantSignatures should have one '#' character when PlaintiffSignatures have not.");
    }
}
