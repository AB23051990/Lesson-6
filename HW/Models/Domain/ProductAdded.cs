using System.Collections.Concurrent;

namespace HW.Models
{
    public class ProductAdded : IDomainEvent
    {
        public Category Product { get; }

        public ProductAdded(Category product)
        {
            Product = product;
        }
    }
}
