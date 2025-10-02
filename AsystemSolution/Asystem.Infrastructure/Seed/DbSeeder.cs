using Asystem.Core.Entities;
using Asystem.Core.Entities.Enums;
using Asystem.Infrastructure.Data;

namespace Asystem.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Materials.Any()) return;

            // Материалы для типографии
            db.Materials.AddRange(
                new Material { Code = "PAPER_A4_80", Name = "Бумага A4 80г/м²", OnHand = 15000, Reserved = 2000, UnitPrice = 0.025m },
                new Material { Code = "PAPER_A4_120", Name = "Бумага A4 120г/м²", OnHand = 8000, Reserved = 500, UnitPrice = 0.035m },
                new Material { Code = "PAPER_A3_80", Name = "Бумага A3 80г/м²", OnHand = 5000, Reserved = 800, UnitPrice = 0.045m },
                new Material { Code = "PAPER_A5_80", Name = "Бумага A5 80г/м²", OnHand = 12000, Reserved = 1000, UnitPrice = 0.015m },
                new Material { Code = "INK_CYAN", Name = "Краска Cyan 1L", OnHand = 45, Reserved = 5, UnitPrice = 15.50m },
                new Material { Code = "INK_MAGENTA", Name = "Краска Magenta 1L", OnHand = 42, Reserved = 8, UnitPrice = 15.50m },
                new Material { Code = "INK_YELLOW", Name = "Краска Yellow 1L", OnHand = 48, Reserved = 2, UnitPrice = 15.50m },
                new Material { Code = "INK_BLACK", Name = "Краска Black 1L", OnHand = 55, Reserved = 10, UnitPrice = 14.00m },
                new Material { Code = "LAMINATE_GLOSS", Name = "Ламинация глянцевая A4", OnHand = 2000, Reserved = 200, UnitPrice = 2.50m },
                new Material { Code = "LAMINATE_MATT", Name = "Ламинация матовая A4", OnHand = 1800, Reserved = 150, UnitPrice = 2.50m },
                new Material { Code = "BINDING_SPIRAL", Name = "Пружина для брошюровки", OnHand = 500, Reserved = 50, UnitPrice = 0.80m },
                new Material { Code = "BINDING_WIRE", Name = "Проволока для брошюровки", OnHand = 300, Reserved = 30, UnitPrice = 0.60m },
                new Material { Code = "CARDSTOCK_300", Name = "Картон 300г/м²", OnHand = 3000, Reserved = 400, UnitPrice = 0.80m },
                new Material { Code = "ENVELOPE_C5", Name = "Конверты C5", OnHand = 1000, Reserved = 100, UnitPrice = 0.15m },
                new Material { Code = "ENVELOPE_A4", Name = "Конверты A4", OnHand = 800, Reserved = 80, UnitPrice = 0.25m }
            );

            // Добавляем формулы расчета
            if (!db.Formulas.Any())
            {
                db.Formulas.AddRange(
                    new Formula
                    {
                        ProductType = "business-card",
                        Name = "Стандартная формула визиток",
                        BasePrice = 500,
                        PaperMultiplierFormula = "material.unitPrice",
                        PrintMultiplierFormula = "printType == 'color-two-side' ? 1.8 : 1.0",
                        SizeMultiplierFormula = "size == 'euro' ? 1.0 : 1.0",
                        VolumeDiscountFormula = "quantity >= 1000 ? 0.7 : quantity >= 500 ? 0.8 : quantity >= 200 ? 0.9 : 1.0",
                        FinalFormula = "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
                        IsActive = true
                    },
                    new Formula
                    {
                        ProductType = "brochure",
                        Name = "Стандартная формула брошюр",
                        BasePrice = 1500,
                        PaperMultiplierFormula = "material.unitPrice",
                        PrintMultiplierFormula = "printType == 'bw' ? 0.7 : 1.0",
                        SizeMultiplierFormula = "size == 'a4' ? 1.8 : size == 'a3' ? 3.0 : 1.0",
                        VolumeDiscountFormula = "quantity >= 1000 ? 0.7 : quantity >= 500 ? 0.8 : quantity >= 200 ? 0.9 : 1.0",
                        FinalFormula = "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
                        IsActive = true
                    },
                    new Formula
                    {
                        ProductType = "poster",
                        Name = "Стандартная формула плакатов",
                        BasePrice = 800,
                        PaperMultiplierFormula = "material.unitPrice",
                        PrintMultiplierFormula = "printType == 'high-quality' ? 1.5 : 1.0",
                        SizeMultiplierFormula = "size == 'a2' ? 1.8 : size == 'a1' ? 3.0 : size == 'a0' ? 5.0 : 1.0",
                        VolumeDiscountFormula = "quantity >= 1000 ? 0.7 : quantity >= 500 ? 0.8 : quantity >= 200 ? 0.9 : 1.0",
                        FinalFormula = "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
                        IsActive = true
                    },
                    new Formula
                    {
                        ProductType = "banner",
                        Name = "Формула баннеров с учетом площади",
                        BasePrice = 2000,
                        PaperMultiplierFormula = "material.unitPrice * 2.0",
                        PrintMultiplierFormula = "printType == 'high-quality' ? 1.5 : 1.0",
                        SizeMultiplierFormula = "size == '2x1' ? 2.0 : size == '3x1' ? 3.0 : 1.0",
                        VolumeDiscountFormula = "quantity >= 50 ? 0.7 : quantity >= 20 ? 0.8 : 1.0",
                        FinalFormula = "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
                        IsActive = true
                    },
                    new Formula
                    {
                        ProductType = "calendar",
                        Name = "Формула календарей с учетом типа",
                        BasePrice = 1200,
                        PaperMultiplierFormula = "material.unitPrice * 1.3",
                        PrintMultiplierFormula = "printType == 'premium' ? 1.4 : 1.0",
                        SizeMultiplierFormula = "size == 'desk' ? 0.8 : size == 'pocket' ? 0.5 : 1.0",
                        VolumeDiscountFormula = "quantity >= 1000 ? 0.6 : quantity >= 500 ? 0.7 : quantity >= 200 ? 0.8 : 1.0",
                        FinalFormula = "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
                        IsActive = true
                    },
                    new Formula
                    {
                        ProductType = "flyer",
                        Name = "Формула листовок с учетом размера",
                        BasePrice = 600,
                        PaperMultiplierFormula = "material.unitPrice",
                        PrintMultiplierFormula = "printType == 'color-two-side' ? 1.7 : 1.0",
                        SizeMultiplierFormula = "size == 'a6' ? 0.5 : size == 'a4' ? 1.8 : 1.0",
                        VolumeDiscountFormula = "quantity >= 5000 ? 0.5 : quantity >= 2000 ? 0.6 : quantity >= 1000 ? 0.7 : quantity >= 500 ? 0.8 : 1.0",
                        FinalFormula = "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
                        IsActive = true
                    }
                );
            }

            // Добавляем/обновляем тестовые продукты — берем полный набор типов продукции с сайта
            var seedProducts = new List<Product>
            {
                new Product { Code = "PRD_BUSINESS_CARD", Name = "Визитка стандарт", ProductType = "business-card", Price = 500m, IsActive = true, Description = "Стандартная визитка 90x50 мм" },
                new Product { Code = "PRD_BROCHURE_A4", Name = "Брошюра A4", ProductType = "brochure", Price = 1500m, IsActive = true, Description = "Брошюра 16 страниц A4" },
                new Product { Code = "PRD_POSTER_A2", Name = "Плакат A2", ProductType = "poster", Price = 800m, IsActive = true, Description = "Плакат A2 ламинированный" },
                new Product { Code = "PRD_BANNER_STD", Name = "Баннер стандарт", ProductType = "banner", Price = 2000m, IsActive = true, Description = "Баннер стандартного размера" },
                new Product { Code = "PRD_CALENDAR_STD", Name = "Календарь настенный", ProductType = "calendar", Price = 1200m, IsActive = true, Description = "Настенный календарь формата A3" },
                new Product { Code = "PRD_FLYER_A5", Name = "Листовка A5", ProductType = "flyer", Price = 600m, IsActive = true, Description = "Листовка A5, двусторонняя" }
            };

            foreach (var sp in seedProducts)
            {
                var exists = db.Set<Product>().Any(p => p.Code == sp.Code || p.ProductType == sp.ProductType);
                if (!exists)
                {
                    db.Set<Product>().Add(sp);
                }
            }

            if (!db.Orders.Any())
            {
                var orders = new List<Order>
                {
                    new Order
                    {
                        Number = "ORD-001",
                        CustomerName = "ООО Ромашка",
                        Quantity = 500,
                        Price = 12000m,
                        CalculatedCost = 8500m,
                        Stage = OrderStage.Prepress,
                        CreatedAt = DateTime.UtcNow.AddDays(-5),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = true },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = false },
                            new OrderTask { Name = "Вывод форм (CTP)", IsCompleted = false }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-002",
                        CustomerName = "ИП Иванов",
                        Quantity = 1000,
                        Price = 8500m,
                        CalculatedCost = 6200m,
                        Stage = OrderStage.Print,
                        CreatedAt = DateTime.UtcNow.AddDays(-3),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = true },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = true },
                            new OrderTask { Name = "Вывод форм (CTP)", IsCompleted = true },
                            new OrderTask { Name = "Настройка печатной машины", IsCompleted = true },
                            new OrderTask { Name = "Печать тиража", IsCompleted = false }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-003",
                        CustomerName = "ЗАО Агротех",
                        Quantity = 2000,
                        Price = 25000m,
                        CalculatedCost = 18000m,
                        Stage = OrderStage.Postpress,
                        CreatedAt = DateTime.UtcNow.AddDays(-2),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = true },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = true },
                            new OrderTask { Name = "Вывод форм (CTP)", IsCompleted = true },
                            new OrderTask { Name = "Настройка печатной машины", IsCompleted = true },
                            new OrderTask { Name = "Печать тиража", IsCompleted = true },
                            new OrderTask { Name = "Ламинация", IsCompleted = false },
                            new OrderTask { Name = "Брошюровка", IsCompleted = false }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-004",
                        CustomerName = "ООО МегаМаркет",
                        Quantity = 5000,
                        Price = 45000m,
                        CalculatedCost = 32000m,
                        Stage = OrderStage.Ready,
                        CreatedAt = DateTime.UtcNow.AddDays(-1),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = true },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = true },
                            new OrderTask { Name = "Вывод форм (CTP)", IsCompleted = true },
                            new OrderTask { Name = "Настройка печатной машины", IsCompleted = true },
                            new OrderTask { Name = "Печать тиража", IsCompleted = true },
                            new OrderTask { Name = "Ламинация", IsCompleted = true },
                            new OrderTask { Name = "Брошюровка", IsCompleted = true },
                            new OrderTask { Name = "Контроль качества", IsCompleted = true },
                            new OrderTask { Name = "Упаковка", IsCompleted = true }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-005",
                        CustomerName = "ИП Петрова",
                        Quantity = 300,
                        Price = 4500m,
                        CalculatedCost = 3200m,
                        Stage = OrderStage.Delivered,
                        CreatedAt = DateTime.UtcNow.AddDays(-7),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = true },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = true },
                            new OrderTask { Name = "Вывод форм (CTP)", IsCompleted = true },
                            new OrderTask { Name = "Настройка печатной машины", IsCompleted = true },
                            new OrderTask { Name = "Печать тиража", IsCompleted = true },
                            new OrderTask { Name = "Ламинация", IsCompleted = true },
                            new OrderTask { Name = "Контроль качества", IsCompleted = true },
                            new OrderTask { Name = "Упаковка", IsCompleted = true },
                            new OrderTask { Name = "Доставка", IsCompleted = true }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-006",
                        CustomerName = "ООО ТехноПринт",
                        Quantity = 1500,
                        Price = 18500m,
                        CalculatedCost = 13200m,
                        Stage = OrderStage.New,
                        CreatedAt = DateTime.UtcNow.AddHours(-2),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = false },
                            new OrderTask { Name = "Проверка макета", IsCompleted = false }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-007",
                        CustomerName = "ИП Сидоров",
                        Quantity = 800,
                        Price = 9200m,
                        CalculatedCost = 6800m,
                        Stage = OrderStage.Prepress,
                        CreatedAt = DateTime.UtcNow.AddDays(-4),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = false },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = false }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-008",
                        CustomerName = "ООО ПромСнаб",
                        Quantity = 2500,
                        Price = 32000m,
                        CalculatedCost = 22500m,
                        Stage = OrderStage.Print,
                        CreatedAt = DateTime.UtcNow.AddDays(-6),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = true },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = true },
                            new OrderTask { Name = "Вывод форм (CTP)", IsCompleted = true },
                            new OrderTask { Name = "Настройка печатной машины", IsCompleted = false },
                            new OrderTask { Name = "Печать тиража", IsCompleted = false }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-009",
                        CustomerName = "ИП Козлова",
                        Quantity = 1200,
                        Price = 14800m,
                        CalculatedCost = 10500m,
                        Stage = OrderStage.Postpress,
                        CreatedAt = DateTime.UtcNow.AddDays(-8),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = true },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = true },
                            new OrderTask { Name = "Вывод форм (CTP)", IsCompleted = true },
                            new OrderTask { Name = "Настройка печатной машины", IsCompleted = true },
                            new OrderTask { Name = "Печать тиража", IsCompleted = true },
                            new OrderTask { Name = "Ламинация", IsCompleted = true },
                            new OrderTask { Name = "Брошюровка", IsCompleted = false },
                            new OrderTask { Name = "Контроль качества", IsCompleted = false }
                        }
                    },
                    new Order
                    {
                        Number = "ORD-010",
                        CustomerName = "ООО БизнесДок",
                        Quantity = 3500,
                        Price = 42000m,
                        CalculatedCost = 29500m,
                        Stage = OrderStage.Ready,
                        CreatedAt = DateTime.UtcNow.AddDays(-10),
                        Tasks = new List<OrderTask>
                        {
                            new OrderTask { Name = "Файлы получены", IsCompleted = true },
                            new OrderTask { Name = "Проверка макета", IsCompleted = true },
                            new OrderTask { Name = "Цветопроба согласована", IsCompleted = true },
                            new OrderTask { Name = "Вывод форм (CTP)", IsCompleted = true },
                            new OrderTask { Name = "Настройка печатной машины", IsCompleted = true },
                            new OrderTask { Name = "Печать тиража", IsCompleted = true },
                            new OrderTask { Name = "Ламинация", IsCompleted = true },
                            new OrderTask { Name = "Брошюровка", IsCompleted = true },
                            new OrderTask { Name = "Контроль качества", IsCompleted = true },
                            new OrderTask { Name = "Упаковка", IsCompleted = true }
                        }
                    }
                };

                db.Orders.AddRange(orders);
            }

            db.SaveChanges();
        }
    }
}

