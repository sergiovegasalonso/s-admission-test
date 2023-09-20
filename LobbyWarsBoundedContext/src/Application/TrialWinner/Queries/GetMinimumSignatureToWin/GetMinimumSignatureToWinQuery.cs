using MediatR;
using SignaturitAdmissionTest.Application.Common.Utils;
using SignaturitAdmissionTest.Domain.Enums;

namespace SignaturitAdmissionTest.Application.TrialWinner.Queries.GetMinimumSignatureToWin;

public record GetMinimumSignatureToWinQuery : IRequest<MinimumSignatureToWinResultDto>
{
    public string PlaintiffSignatures { get; set; } = string.Empty;

    public string DefendantSignatures { get; set; } = string.Empty;
}

public class GetMinimumSignatureToWinQueryHandler : IRequestHandler<GetMinimumSignatureToWinQuery, MinimumSignatureToWinResultDto>
{
    public GetMinimumSignatureToWinQueryHandler() { }

    public async Task<MinimumSignatureToWinResultDto> Handle(GetMinimumSignatureToWinQuery request, CancellationToken cancellationToken)
    {
        var task = Task.Run(() =>
        {
            Party partyWithUnknownSignature = request.PlaintiffSignatures.Count(x => x == (char)Character.Hash) == 1 ? Party.Plaintiff : Party.Defendant;

            int plaintiffPoints = CommonUtilities.CountPartyPoints(request.PlaintiffSignatures);
            int defendantPoints = CommonUtilities.CountPartyPoints(request.DefendantSignatures);

            string result = string.Empty;

            if (partyWithUnknownSignature == Party.Plaintiff)
                result = GetResult(plaintiffPoints - defendantPoints, partyWithUnknownSignature, request.PlaintiffSignatures.Contains((char)Role.King));
            else if (partyWithUnknownSignature == Party.Defendant)
                result = GetResult(defendantPoints - plaintiffPoints, partyWithUnknownSignature, request.DefendantSignatures.Contains((char)Role.King));

            return new MinimumSignatureToWinResultDto()
            {
                Result = result,
            };
        });

        return await task;
    }

    private string GetResult(int differenceWithOtherParty, Party partyWithUnknownSignature, bool signaturesContainsAKingsSignature)
    {
        string result = string.Empty;

        if (differenceWithOtherParty > 0)
            result = $@"{partyWithUnknownSignature} does not need more signatures to win the trial";
        else
        {
            int candidatesPoints = int.MaxValue;
            Role? candidatesRole = null;

            foreach (var item in CommonUtilities.GetRoleToPointsMap())
            {
                if (signaturesContainsAKingsSignature && item.Key == (char)Role.Validator) continue;
                int amount = item.Value + differenceWithOtherParty;
                if (amount > 0 && item.Value < candidatesPoints)
                {
                    candidatesPoints = item.Value;
                    candidatesRole = (Role)item.Key;
                }
            }

            if (candidatesRole != null)
                result = $@"{partyWithUnknownSignature} needs a {candidatesRole}'s signature to win the trial";
            else
                result = $@"{partyWithUnknownSignature} can't win the trial with only one more signature";
        }
        return result;
    }
}
