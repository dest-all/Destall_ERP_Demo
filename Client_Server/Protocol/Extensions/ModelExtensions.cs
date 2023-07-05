using Protocol.Models;

namespace Protocol.Extensions;

public static class ModelExtensions
{
    public static bool Exists(this IReferrableModel referrableModel)
        => referrableModel.Reference.IsEmpty();
}
