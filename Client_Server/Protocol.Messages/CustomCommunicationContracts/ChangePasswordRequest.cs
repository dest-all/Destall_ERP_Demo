using Protocol.Messages;

namespace Protocol.Messages.CustomCommunicationContracts;

public class ChangePasswordRequest : Transportable
{
    public string OldPassword { get; }
    public string NewPassword { get; }
}