// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Messages.CustomCommunicationContracts;

namespace Data.CommunicationContracts.Pagination
{
    public class CashEntryPaginationRequest : PaginationRequest<Data.Entities.Documents.Finances.CashEntry>
    {
        public Data.CommunicationContracts.CashEntryFilter Filter { get; }
    }
}