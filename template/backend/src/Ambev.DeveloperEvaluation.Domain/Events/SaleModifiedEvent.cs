using System;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent
    {
        public Guid SaleId { get; }
        public DateTime ModifiedAt { get; }

        public SaleModifiedEvent(Guid saleId)
        {
            SaleId = saleId;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}