using System.ComponentModel.DataAnnotations.Schema;

namespace Asystem.Core.Entities
{
    public class OrderTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsCompleted { get; set; } = false;

        // foreign key
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}

