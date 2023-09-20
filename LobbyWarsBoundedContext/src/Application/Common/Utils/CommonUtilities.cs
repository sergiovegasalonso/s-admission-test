using SignaturitAdmissionTest.Domain.Enums;

namespace SignaturitAdmissionTest.Application.Common.Utils;
public static class CommonUtilities
{
    public static Dictionary<char, int> GetRoleToPointsMap()
    {
        return new Dictionary<char, int>()
        {
            { (char)Role.King, 5 },
            { (char)Role.Notary, 2 },
            { (char)Role.Validator, 1 }
        };
    }

    public static int GetPointsByRole(char role)
    {
        GetRoleToPointsMap().TryGetValue(role, out var value);

        return value;
    }

    public static int CountPartyPoints(string partySignatures)
    {
        bool signaturesContainsAKingsSignature = partySignatures.Contains((char)Role.King);
        return partySignatures.Aggregate(0, (total, role) =>
                                    total += signaturesContainsAKingsSignature && role == (char)Role.Validator ? 0 : GetPointsByRole(role));
    }
}
