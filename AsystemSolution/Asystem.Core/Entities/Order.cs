using Asystem.Core.Entities.Enums;

namespace Asystem.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public decimal Price { get; set; } = 0m;
        public decimal CalculatedCost { get; set; } = 0m;
        public int Quantity { get; set; } = 0;
        public OrderStage Stage { get; set; } = OrderStage.New;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<OrderTask> Tasks { get; set; } = new();
    }
}
