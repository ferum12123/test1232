namespace Asystem.Core.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal OnHand { get; set; }
        public decimal Reserved { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
