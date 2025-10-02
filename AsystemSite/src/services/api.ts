// API сервис для работы с backend
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '';

export interface Material {
  id: number;
  code: string;
  name: string;
  onHand: number;
  reserved: number;
  unitPrice: number;
}

export interface ProductType {
  id: string;
  name: string;
  basePrice: number;
  materials: string[]; // коды материалов, подходящих для этого типа продукции
}

export interface Formula {
  id: number;
  productType: string;
  name: string;
  basePrice: number;
  paperMultiplierFormula: string;
  printMultiplierFormula: string;
  sizeMultiplierFormula: string;
  volumeDiscountFormula: string;
  finalFormula: string;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

// Получение всех материалов
export async function getMaterials(): Promise<Material[]> {
  const response = await fetch(`${API_BASE_URL}/api/Materials`);
  if (!response.ok) {
    throw new Error(`Ошибка загрузки материалов: ${response.status}`);
  }
  return response.json();
}

// Получение всех заказов
export async function getOrders() {
  const response = await fetch(`${API_BASE_URL}/api/Orders`);
  if (!response.ok) {
    throw new Error(`Ошибка загрузки заказов: ${response.status}`);
  }
  return response.json();
}

// Создание заказа
export async function createOrder(orderData: any) {
  const response = await fetch(`${API_BASE_URL}/api/Orders`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(orderData),
  });
  
  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Ошибка создания заказа: ${response.status} ${errorText}`);
  }
  
  return response.json();
}

// Типы продукции с привязкой к материалам из базы
export const PRODUCT_TYPES: ProductType[] = [
  {
    id: "business-card",
    name: "Визитки",
    basePrice: 500,
    materials: ["CARDSTOCK_300", "LAMINATE_GLOSS", "LAMINATE_MATT"]
  },
  {
    id: "brochure", 
    name: "Брошюры",
    basePrice: 1500,
    materials: ["PAPER_A4_80", "PAPER_A4_120", "BINDING_SPIRAL", "BINDING_WIRE"]
  },
  {
    id: "poster",
    name: "Плакаты", 
    basePrice: 800,
    materials: ["PAPER_A3_80", "PAPER_A4_80"]
  },
  {
    id: "banner",
    name: "Баннеры",
    basePrice: 2000,
    materials: ["PAPER_A3_80", "PAPER_A4_80"]
  },
  {
    id: "calendar",
    name: "Календари",
    basePrice: 1200,
    materials: ["PAPER_A4_120", "PAPER_A3_80"]
  },
  {
    id: "flyer",
    name: "Листовки",
    basePrice: 600,
    materials: ["PAPER_A4_80", "PAPER_A5_80"]
  }
];

// Встроенные формулы для каждого типа продукции
export const BUILT_IN_FORMULAS: Record<string, Formula> = {
  "business-card": {
    id: 1,
    productType: "business-card",
    name: "Стандартная формула визиток",
    basePrice: 500,
    paperMultiplierFormula: "material.unitPrice",
    printMultiplierFormula: "printType === 'color-two-side' ? 1.8 : 1.0",
    sizeMultiplierFormula: "size === 'euro' ? 1.0 : 1.0",
    volumeDiscountFormula: "quantity >= 1000 ? 0.7 : quantity >= 500 ? 0.8 : quantity >= 200 ? 0.9 : 1.0",
    finalFormula: "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
    isActive: true,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString()
  },
  "brochure": {
    id: 2,
    productType: "brochure",
    name: "Формула брошюр с переплетом",
    basePrice: 1500,
    paperMultiplierFormula: "material.unitPrice * 1.2",
    printMultiplierFormula: "printType === 'bw' ? 0.7 : 1.0",
    sizeMultiplierFormula: "size === 'a4' ? 1.8 : size === 'a3' ? 3.0 : 1.0",
    volumeDiscountFormula: "quantity >= 2000 ? 0.6 : quantity >= 1000 ? 0.7 : quantity >= 500 ? 0.8 : 1.0",
    finalFormula: "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
    isActive: true,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString()
  },
  "poster": {
    id: 3,
    productType: "poster",
    name: "Формула плакатов с наценкой за размер",
    basePrice: 800,
    paperMultiplierFormula: "material.unitPrice * 1.5",
    printMultiplierFormula: "printType === 'high-quality' ? 1.8 : 1.0",
    sizeMultiplierFormula: "size === 'a2' ? 2.0 : size === 'a1' ? 4.0 : size === 'a0' ? 8.0 : 1.0",
    volumeDiscountFormula: "quantity >= 100 ? 0.8 : 1.0",
    finalFormula: "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
    isActive: true,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString()
  },
  "banner": {
    id: 4,
    productType: "banner",
    name: "Формула баннеров с учетом площади",
    basePrice: 2000,
    paperMultiplierFormula: "material.unitPrice * 2.0",
    printMultiplierFormula: "printType === 'high-quality' ? 1.5 : 1.0",
    sizeMultiplierFormula: "size === '2x1' ? 2.0 : size === '3x1' ? 3.0 : 1.0",
    volumeDiscountFormula: "quantity >= 50 ? 0.7 : quantity >= 20 ? 0.8 : 1.0",
    finalFormula: "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
    isActive: true,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString()
  },
  "calendar": {
    id: 5,
    productType: "calendar",
    name: "Формула календарей с учетом типа",
    basePrice: 1200,
    paperMultiplierFormula: "material.unitPrice * 1.3",
    printMultiplierFormula: "printType === 'premium' ? 1.4 : 1.0",
    sizeMultiplierFormula: "size === 'desk' ? 0.8 : size === 'pocket' ? 0.5 : 1.0",
    volumeDiscountFormula: "quantity >= 1000 ? 0.6 : quantity >= 500 ? 0.7 : quantity >= 200 ? 0.8 : 1.0",
    finalFormula: "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
    isActive: true,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString()
  },
  "flyer": {
    id: 6,
    productType: "flyer",
    name: "Формула листовок с учетом размера",
    basePrice: 600,
    paperMultiplierFormula: "material.unitPrice",
    printMultiplierFormula: "printType === 'color-two-side' ? 1.7 : 1.0",
    sizeMultiplierFormula: "size === 'a6' ? 0.5 : size === 'a4' ? 1.8 : 1.0",
    volumeDiscountFormula: "quantity >= 5000 ? 0.5 : quantity >= 2000 ? 0.6 : quantity >= 1000 ? 0.7 : quantity >= 500 ? 0.8 : 1.0",
    finalFormula: "basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantity",
    isActive: true,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString()
  }
};

// Фильтрация материалов по типу продукции
export function getMaterialsForProductType(materials: Material[], productTypeId: string): Material[] {
  const productType = PRODUCT_TYPES.find(p => p.id === productTypeId);
  if (!productType) return [];
  
  return materials.filter(material => 
    productType.materials.includes(material.code)
  );
}

// Получение формул расчета
export async function getFormulas(): Promise<Formula[]> {
  const response = await fetch(`${API_BASE_URL}/api/Formulas`);
  if (!response.ok) {
    throw new Error(`Ошибка загрузки формул: ${response.status}`);
  }
  return response.json();
}

// Получение формулы по типу продукции (сначала из API, потом встроенная)
export async function getFormulaByProductType(productType: string): Promise<Formula | null> {
  try {
    // Сначала пытаемся получить формулу из API
    const response = await fetch(`${API_BASE_URL}/api/Formulas/by-product/${productType}`);
    if (response.ok) {
      return response.json();
    }
  } catch (error) {
    console.warn('API недоступен, используем встроенную формулу:', error);
  }
  
  // Если API недоступен или формула не найдена, используем встроенную
  return BUILT_IN_FORMULAS[productType] || null;
}

// Группировка материалов по категориям для калькулятора
export function groupMaterialsForCalculator(materials: Material[]): {
  paper: Material[];
  ink: Material[];
  finishing: Material[];
} {
  return {
    paper: materials.filter(m => 
      m.code.includes('PAPER') || m.code.includes('CARDSTOCK')
    ),
    ink: materials.filter(m => m.code.includes('INK')),
    finishing: materials.filter(m => 
      m.code.includes('LAMINATE') || m.code.includes('BINDING') || m.code.includes('ENVELOPE')
    )
  };
}
