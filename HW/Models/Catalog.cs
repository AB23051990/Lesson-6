using System.Collections.Concurrent;

namespace HW.Models
{
    public class Catalog
    {
        public List<Category> Products { get; set; } = new();        
        private readonly object _syncObj1 = new();
        public void AddProduct(Category product)
        {
            lock (_syncObj1)
            {
                Products.Add(product);
            }
        }
        public void RemoveProduct(Category product)
        {
            lock (_syncObj1)
            {
                Products.Remove(product);
            }
        }

        public record Prices(Guid Price);
        public ConcurrentDictionary<Guid, Prices> PricesDict = new();
        public int Count => PricesDict.Count;
        public void Add(Prices prices) => PricesDict.TryAdd(prices.Price, prices);
        public void Remove(Prices prices) => PricesDict.TryRemove(prices.Price, out _);
        public IReadOnlyCollection<Prices> GetAll() => PricesDict.Values.ToArray();
    }
}
