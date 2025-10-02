namespace Asystem.Core.Entities
{
    public class Formula
    {
        public int Id { get; set; }
        public string ProductType { get; set; } = ""; // business-card, brochure, etc.
        public string Name { get; set; } = ""; // Название формулы
        public decimal BasePrice { get; set; } // Базовая цена
        public string PaperMultiplierFormula { get; set; } = ""; // Формула для расчета множителя бумаги
        public string PrintMultiplierFormula { get; set; } = ""; // Формула для расчета множителя печати
        public string SizeMultiplierFormula { get; set; } = ""; // Формула для расчета множителя размера
        public string VolumeDiscountFormula { get; set; } = ""; // Формула скидки за объем
        public string FinalFormula { get; set; } = ""; // Итоговая формула расчета
        public bool IsActive { get; set; } = true; // Активна ли формула
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
