namespace Asystem.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string ProductType { get; set; } = ""; // business-card, brochure, etc.
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
