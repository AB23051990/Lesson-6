using System.Collections.Concurrent;

namespace HW.Models
{
    public class Catalog
    {        
        public record Category(Guid Id, string Name);
        public ConcurrentDictionary<Guid, Category> Product = new();
        public int Count => Product.Count;
        public void Add(Category product) => Product.TryAdd(product.Id, product);
        public void Remove(Category product) => Product.TryRemove(product.Id, out _);
        public IReadOnlyCollection<Category> GetAll() => Product.Values.ToArray();
    }
}
