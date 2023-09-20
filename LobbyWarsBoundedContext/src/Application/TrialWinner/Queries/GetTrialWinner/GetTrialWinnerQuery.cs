using MediatR;
using SignaturitAdmissionTest.Application.Common.Utils;

namespace SignaturitAdmissionTest.Application.TrialWinner.Queries.GetTrialWinner;

public record GetTrialWinnerQuery : IRequest<WinnerResultDto>
{
    public string PlaintiffSignatures { get; set; } = string.Empty;

    public string DefendantSignatures { get; set; } = string.Empty;
}

public class GetTrialWinnerQueryHandler : IRequestHandler<GetTrialWinnerQuery, WinnerResultDto>
{
    public GetTrialWinnerQueryHandler() { }

    public async Task<WinnerResultDto> Handle(GetTrialWinnerQuery request, CancellationToken cancellationToken)
    {
        var task = Task.Run(() =>
        {
            int plaintiffPoints = CommonUtilities.CountPartyPoints(request.PlaintiffSignatures);
            int defendantPoints = CommonUtilities.CountPartyPoints(request.DefendantSignatures);

            string result = string.Empty;

            if (plaintiffPoints > defendantPoints)
                result = @$"Plaintinff wins the trial. Plaintinff {plaintiffPoints} - {defendantPoints} Defendant";
            else if (defendantPoints > plaintiffPoints)
                result = @$"Defendant wins the trial. Defendant {defendantPoints} - {plaintiffPoints} Plaintiff";
            else
                result = @$"Trial is tie. Defendant {defendantPoints} - {plaintiffPoints} Plaintiff";

            return new WinnerResultDto()
            {
                Result = result
            };
        });

        return await task;
    }
}
