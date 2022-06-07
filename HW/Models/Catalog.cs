namespace HW.Models
{
    public class Catalog
    {
        public List<Category> Products { get; set; } = new();          
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }       
    }
}