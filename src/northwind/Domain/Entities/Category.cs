using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Category:Entity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
