// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Messages.CustomCommunicationContracts;

namespace Data.CommunicationContracts.Pagination
{
    public class AccountEntryPaginationRequest : PaginationRequest<Data.Entities.Registers.AccountEntry>
    {
        public Data.CommunicationContracts.AccountEntryFilter Filter { get; }
    }
}