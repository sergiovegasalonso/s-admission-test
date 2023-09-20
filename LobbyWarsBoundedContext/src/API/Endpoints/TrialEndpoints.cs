using MediatR;
using Microsoft.AspNetCore.Mvc;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetMinimumSignatureToWin;
using SignaturitAdmissionTest.Application.TrialWinner.Queries.GetTrialWinner;

namespace SignaturitAdmissionTest.API.Endpoints;

public static class TrialEndpoints
{
    public static void MapTrialEndpoints(this WebApplication app)
    {
        app.MapGet("api/Trial/GetTrialWinner",
            async (ISender sender, [FromBody] GetTrialWinnerQuery query) => await sender.Send(query)
        ).RequireAuthorization("admin_policy");

        app.MapGet("api/Trial/GetMinimumSignatureToWin",
            async (ISender sender, [FromBody] GetMinimumSignatureToWinQuery query) => await sender.Send(query)
        ).RequireAuthorization("admin_policy");
    }
}
