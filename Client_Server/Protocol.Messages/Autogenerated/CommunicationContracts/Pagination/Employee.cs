// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Messages.CustomCommunicationContracts;

namespace Data.CommunicationContracts.Pagination
{
    public class EmployeePaginationRequest : PaginationRequest<Data.Entities.DataHolders.Employee>
    {
        public Data.CommunicationContracts.EmployeeFilter Filter { get; }
    }
}