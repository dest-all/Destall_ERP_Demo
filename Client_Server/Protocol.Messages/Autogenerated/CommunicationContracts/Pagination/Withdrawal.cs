// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Messages.CustomCommunicationContracts;

namespace Data.CommunicationContracts.Pagination
{
    public class WithdrawalPaginationRequest : PaginationRequest<Data.Entities.Documents.Finances.Withdrawal>
    {
        public Data.CommunicationContracts.WithdrawalFilter Filter { get; }
    }
}