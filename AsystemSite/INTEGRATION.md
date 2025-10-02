# Интеграция калькулятора с базой данных

## Что было сделано

### 1. Создан API сервис (`src/services/api.ts`)
- **getMaterials()** - получение материалов из API `/api/Materials`
- **getOrders()** - получение заказов из API `/api/Orders` 
- **createOrder()** - создание заказа через API `/api/Orders`
- **PRODUCT_TYPES** - константы типов продукции с привязкой к материалам
- **getMaterialsForProductType()** - фильтрация материалов по типу продукции
- **groupMaterialsForCalculator()** - группировка материалов для калькулятора

### 2. Обновлен калькулятор (`src/components/calculator.tsx`)
- Загрузка материалов из API при монтировании компонента
- Динамическое формирование списка "Тип бумаги" на основе материалов из БД
- Использование реальных цен материалов для расчета стоимости
- Обработка состояний загрузки и ошибок
- Спиннер и сообщения об ошибках

### 3. Настроен прокси в Vite (`vite.config.ts`)
- Прокси `/api` → `http://localhost:5000` для обхода CORS в dev-режиме

## Сопоставление данных

### Материалы из БД → Калькулятор
```typescript
// Материалы из API
Material {
  id: number;
  code: string;        // "PAPER_A4_80", "CARDSTOCK_300", etc.
  name: string;        // "Бумага A4 80г/м²", "Картон 300г/м²", etc.
  unitPrice: decimal;  // 0.025, 0.80, etc.
}

// Преобразование в формат калькулятора
PaperType {
  id: string;          // = material.code
  name: string;        // = material.name  
  pricePerUnit: number; // = material.unitPrice
}
```

### Типы продукции → Материалы
```typescript
PRODUCT_TYPES = [
  {
    id: "business-card",
    name: "Визитки", 
    materials: ["CARDSTOCK_300", "LAMINATE_GLOSS", "LAMINATE_MATT"]
  },
  // ...
]
```

## Как это работает

1. **При загрузке страницы**: калькулятор запрашивает материалы через API
2. **При выборе типа продукции**: фильтруются доступные материалы
3. **При расчете стоимости**: используются реальные цены из БД
4. **При изменении типа продукции**: сбрасываются выбранные материалы

## Запуск

1. Запустите API: `cd AsystemSolution/Asystem.Api && dotnet run`
2. Запустите фронт: `cd AsystemSite && npm run dev`
3. API будет доступен на `http://localhost:5000`
4. Фронт будет доступен на `http://localhost:5173` с прокси к API

## Структура данных

### Материалы в БД (из DbSeeder.cs)
- **Бумага**: PAPER_A4_80, PAPER_A4_120, PAPER_A3_80, PAPER_A5_80
- **Картон**: CARDSTOCK_300
- **Краски**: INK_CYAN, INK_MAGENTA, INK_YELLOW, INK_BLACK
- **Ламинация**: LAMINATE_GLOSS, LAMINATE_MATT
- **Брошюровка**: BINDING_SPIRAL, BINDING_WIRE
- **Конверты**: ENVELOPE_C5, ENVELOPE_A4

### Типы продукции
- **Визитки**: картон + ламинация
- **Брошюры**: бумага + брошюровка  
- **Плакаты**: бумага A3/A4
- **Баннеры**: бумага A3/A4
- **Календари**: бумага A4/A3
- **Листовки**: бумага A4/A5
