// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

namespace Data.CommunicationContracts
{
    public class IncomingPaymentFilter : Filter, DestallMaterials.CodeGeneration.ERP.Models.Data.Entity.IFilterFor<Data.Entities.Documents.Finances.IncomingPayment>
    {
        public NumberFilter Sum { get; }

        //
        public StringFilter Number { get; }

        //
        public NumberFilter Status { get; }

        //
        public ReferenceFilter Reference { get; }

        //
        public ReferenceFilter Order { get; }

        //
        public ReferenceFilter Payer { get; }

        //
        public ReferenceFilter Currency { get; }

        //
        public ReferenceFilter Accountable { get; }
    //
    }
}