import React from "react";
import { Card, CardBody, CardHeader, Input, Select, SelectItem, Button, Divider, Spinner } from "@heroui/react";
import { Icon } from "@iconify/react";
import { motion } from "framer-motion";
import { getMaterials, getMaterialsForProductType, getFormulaByProductType, PRODUCT_TYPES, Material, Formula } from "../services/api";

interface PaperType {
  id: string;
  name: string;
  pricePerUnit: number;
}

interface PrintType {
  id: string;
  name: string;
  priceMultiplier: number;
}

interface Size {
  id: string;
  name: string;
  priceMultiplier: number;
}

export const Calculator: React.FC = () => {
  const [productType, setProductType] = React.useState("business-card");
  const [paperType, setPaperType] = React.useState("");
  const [printType, setPrintType] = React.useState("color-one-side");
  const [size, setSize] = React.useState("standard");
  const [quantity, setQuantity] = React.useState("100");
  const [totalPrice, setTotalPrice] = React.useState(0);
  const [materials, setMaterials] = React.useState<Material[]>([]);
  const [formula, setFormula] = React.useState<Formula | null>(null);
  const [isLoading, setIsLoading] = React.useState(true);
  const [error, setError] = React.useState<string | null>(null);

  // Загрузка материалов и формул при монтировании компонента
  React.useEffect(() => {
    const loadData = async () => {
      try {
        setIsLoading(true);
        setError(null);
        
        // Загружаем материалы (может не работать без API)
        let materialsData: Material[] = [];
        try {
          materialsData = await getMaterials();
        } catch (err) {
          console.warn('Не удалось загрузить материалы из API, используем пустой список');
        }
        
        // Загружаем формулу (всегда работает благодаря встроенным формулам)
        const formulaData = await getFormulaByProductType(productType);
        
        setMaterials(materialsData);
        setFormula(formulaData);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Ошибка загрузки данных');
        console.error('Ошибка загрузки данных:', err);
      } finally {
        setIsLoading(false);
      }
    };

    loadData();
  }, []);

  // Загрузка формулы при изменении типа продукции
  React.useEffect(() => {
    const loadFormula = async () => {
      try {
        const formulaData = await getFormulaByProductType(productType);
        setFormula(formulaData);
      } catch (err) {
        console.error('Ошибка загрузки формулы:', err);
      }
    };

    if (productType) {
      loadFormula();
    }
  }, [productType]);

  // Получение доступных материалов для выбранного типа продукции
  const getAvailableMaterials = (productTypeId: string): Material[] => {
    return getMaterialsForProductType(materials, productTypeId);
  };

  // Преобразование материалов в формат для выпадающего списка
  const getPaperTypesForProduct = (productTypeId: string): PaperType[] => {
    const availableMaterials = getAvailableMaterials(productTypeId);
    
    // Если материалов нет (API недоступен), используем fallback
    if (availableMaterials.length === 0) {
      const fallbackMaterials: Record<string, PaperType[]> = {
        "business-card": [
          { id: "standard", name: "Стандартная 300 г/м²", pricePerUnit: 0.8 },
          { id: "premium", name: "Премиум 350 г/м²", pricePerUnit: 1.2 },
          { id: "glossy", name: "Глянцевая 300 г/м²", pricePerUnit: 1.0 }
        ],
        "brochure": [
          { id: "standard", name: "Офсетная 80 г/м²", pricePerUnit: 0.025 },
          { id: "glossy", name: "Глянцевая 115 г/м²", pricePerUnit: 0.035 },
          { id: "premium", name: "Премиум 130 г/м²", pricePerUnit: 0.045 }
        ],
        "poster": [
          { id: "standard", name: "Постерная бумага 150 г/м²", pricePerUnit: 0.05 },
          { id: "photo", name: "Фотобумага 200 г/м²", pricePerUnit: 0.08 },
          { id: "canvas", name: "Холст", pricePerUnit: 0.15 }
        ],
        "banner": [
          { id: "standard", name: "Винил 440 г/м²", pricePerUnit: 0.1 },
          { id: "premium", name: "Винил 510 г/м²", pricePerUnit: 0.14 },
          { id: "mesh", name: "Сетка", pricePerUnit: 0.12 }
        ],
        "calendar": [
          { id: "standard", name: "Мелованная 170 г/м²", pricePerUnit: 0.04 },
          { id: "premium", name: "Премиум 200 г/м²", pricePerUnit: 0.05 }
        ],
        "flyer": [
          { id: "standard", name: "Офсетная 80 г/м²", pricePerUnit: 0.025 },
          { id: "glossy", name: "Глянцевая 115 г/м²", pricePerUnit: 0.03 },
          { id: "premium", name: "Премиум 170 г/м²", pricePerUnit: 0.045 }
        ]
      };
      return fallbackMaterials[productTypeId] || [];
    }
    
    return availableMaterials.map(material => ({
      id: material.code,
      name: material.name,
      pricePerUnit: Number(material.unitPrice)
    }));
  };

  const printTypes: Record<string, PrintType[]> = {
    "business-card": [
      { id: "color-one-side", name: "Цветная односторонняя", priceMultiplier: 1 },
      { id: "color-two-side", name: "Цветная двусторонняя", priceMultiplier: 1.8 }
    ],
    "brochure": [
      { id: "color", name: "Полноцветная", priceMultiplier: 1 },
      { id: "bw", name: "Черно-белая", priceMultiplier: 0.7 }
    ],
    "poster": [
      { id: "standard", name: "Стандартная", priceMultiplier: 1 },
      { id: "high-quality", name: "Высокое качество", priceMultiplier: 1.5 }
    ],
    "banner": [
      { id: "standard", name: "Стандартная", priceMultiplier: 1 },
      { id: "high-quality", name: "Высокое качество", priceMultiplier: 1.3 }
    ],
    "calendar": [
      { id: "standard", name: "Стандартная", priceMultiplier: 1 },
      { id: "premium", name: "Премиум", priceMultiplier: 1.4 }
    ],
    "flyer": [
      { id: "color-one-side", name: "Цветная односторонняя", priceMultiplier: 1 },
      { id: "color-two-side", name: "Цветная двусторонняя", priceMultiplier: 1.7 }
    ]
  };

  const sizes: Record<string, Size[]> = {
    "business-card": [
      { id: "standard", name: "Стандартная (90×50 мм)", priceMultiplier: 1 },
      { id: "euro", name: "Евро (85×55 мм)", priceMultiplier: 1 }
    ],
    "brochure": [
      { id: "a5", name: "A5", priceMultiplier: 1 },
      { id: "a4", name: "A4", priceMultiplier: 1.8 },
      { id: "a3", name: "A3", priceMultiplier: 3 }
    ],
    "poster": [
      { id: "a3", name: "A3", priceMultiplier: 1 },
      { id: "a2", name: "A2", priceMultiplier: 1.8 },
      { id: "a1", name: "A1", priceMultiplier: 3 },
      { id: "a0", name: "A0", priceMultiplier: 5 }
    ],
    "banner": [
      { id: "1x1", name: "1×1 м", priceMultiplier: 1 },
      { id: "2x1", name: "2×1 м", priceMultiplier: 2 },
      { id: "3x1", name: "3×1 м", priceMultiplier: 3 },
      { id: "custom", name: "Нестандартный", priceMultiplier: 1 }
    ],
    "calendar": [
      { id: "wall", name: "Настенный", priceMultiplier: 1 },
      { id: "desk", name: "Настольный", priceMultiplier: 0.8 },
      { id: "pocket", name: "Карманный", priceMultiplier: 0.5 }
    ],
    "flyer": [
      { id: "a6", name: "A6", priceMultiplier: 0.5 },
      { id: "a5", name: "A5", priceMultiplier: 1 },
      { id: "a4", name: "A4", priceMultiplier: 1.8 }
    ]
  };

  const calculatePrice = React.useCallback(() => {
    // Если есть формула из API, используем её
    if (formula) {
      const availableMaterials = getPaperTypesForProduct(productType);
      const selectedPaper = availableMaterials.find(p => p.id === paperType);
      const selectedPrintType = printTypes[productType]?.find(p => p.id === printType);
      const selectedSize = sizes[productType]?.find(s => s.id === size);
      const quantityValue = parseInt(quantity) || 0;
      
      if (!selectedPaper || !selectedPrintType || !selectedSize) {
        return 0;
      }

      // Используем формулу из API
      const basePrice = formula.basePrice;
      const paperMultiplier = selectedPaper.pricePerUnit;
      const printMultiplier = selectedPrintType.priceMultiplier;
      const sizeMultiplier = selectedSize.priceMultiplier;
      
      // Простая реализация скидки за объем (можно расширить для более сложных формул)
      let volumeDiscount = 1.0;
      if (quantityValue >= 1000) {
        volumeDiscount = 0.7;
      } else if (quantityValue >= 500) {
        volumeDiscount = 0.8;
      } else if (quantityValue >= 200) {
        volumeDiscount = 0.9;
      }
      
      const price = basePrice * paperMultiplier * printMultiplier * sizeMultiplier * volumeDiscount * quantityValue;
      return Math.round(price / 100) * 100; // Округление до сотен
    }
    
    // Fallback к старой логике, если формула не найдена
    const selectedProduct = PRODUCT_TYPES.find(p => p.id === productType);
    const availableMaterials = getPaperTypesForProduct(productType);
    const selectedPaper = availableMaterials.find(p => p.id === paperType);
    const selectedPrintType = printTypes[productType]?.find(p => p.id === printType);
    const selectedSize = sizes[productType]?.find(s => s.id === size);
    
    if (!selectedProduct || !selectedPaper || !selectedPrintType || !selectedSize) {
      return 0;
    }

    const basePrice = selectedProduct.basePrice;
    const paperMultiplier = selectedPaper.pricePerUnit;
    const printMultiplier = selectedPrintType.priceMultiplier;
    const sizeMultiplier = selectedSize.priceMultiplier;
    const quantityValue = parseInt(quantity) || 0;
    
    let price = basePrice * paperMultiplier * printMultiplier * sizeMultiplier;
    
    if (quantityValue >= 1000) {
      price = price * 0.7;
    } else if (quantityValue >= 500) {
      price = price * 0.8;
    } else if (quantityValue >= 200) {
      price = price * 0.9;
    }
    
    return Math.round(price * quantityValue / 100) * 100;
  }, [productType, paperType, printType, size, quantity, materials, formula]);

  React.useEffect(() => {
    setTotalPrice(calculatePrice());
  }, [productType, paperType, printType, size, quantity, calculatePrice]);

  React.useEffect(() => {
    // Reset selections when product type changes
    const availablePaperTypes = getPaperTypesForProduct(productType);
    const availablePrintTypes = printTypes[productType] || [];
    const availableSizes = sizes[productType] || [];
    
    setPaperType(availablePaperTypes.length > 0 ? availablePaperTypes[0].id : '');
    setPrintType(availablePrintTypes.length > 0 ? availablePrintTypes[0].id : '');
    setSize(availableSizes.length > 0 ? availableSizes[0].id : '');
  }, [productType, materials]);

  return (
    <section id="calculator" className="py-16 bg-background">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-12">
          <h2 className="text-3xl font-bold text-foreground mb-4">Калькулятор стоимости</h2>
          <p className="text-foreground-600 max-w-2xl mx-auto">
            Рассчитайте приблизительную стоимость вашего заказа, выбрав параметры ниже.
            Для получения точной цены и оформления заказа, пожалуйста, свяжитесь с нами.
          </p>
        </div>

        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
          viewport={{ once: true }}
          className="grid grid-cols-1 lg:grid-cols-3 gap-8"
        >
          <div className="lg:col-span-2">
            <Card className="shadow-xs">
              <CardHeader className="flex gap-3">
                <Icon icon="lucide:calculator" className="text-primary text-xl" />
                <div className="flex flex-col">
                  <p className="text-lg font-semibold">Параметры печати</p>
                  <p className="text-small text-default-500">Выберите нужные параметры для расчета</p>
                </div>
              </CardHeader>
              <Divider />
              <CardBody>
                {error && (
                  <div className="mb-4 p-3 bg-danger-50 border border-danger-200 rounded-lg">
                    <p className="text-danger text-sm">{error}</p>
                  </div>
                )}
                
                {isLoading ? (
                  <div className="flex justify-center items-center py-8">
                    <Spinner size="lg" />
                    <span className="ml-2">Загрузка материалов...</span>
                  </div>
                ) : (
                  <div className="space-y-4">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                      <div>
                        <Select
                          label="Тип продукции"
                          placeholder="Выберите тип продукции"
                          selectedKeys={[productType]}
                          onChange={(e) => setProductType(e.target.value)}
                        >
                          {PRODUCT_TYPES.map((product) => (
                            <SelectItem key={product.id}>
                              {product.name}
                            </SelectItem>
                          ))}
                        </Select>
                      </div>
                      <div>
                        <Select
                          label="Тип бумаги"
                          placeholder="Выберите тип бумаги"
                          selectedKeys={[paperType]}
                          onChange={(e) => setPaperType(e.target.value)}
                          isDisabled={isLoading}
                        >
                          {getPaperTypesForProduct(productType).map((paper) => (
                            <SelectItem key={paper.id}>
                              {paper.name}
                            </SelectItem>
                          ))}
                        </Select>
                      </div>
                    </div>
                    
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                      <div>
                        <Select
                          label="Тип печати"
                          placeholder="Выберите тип печати"
                          selectedKeys={[printType]}
                          onChange={(e) => setPrintType(e.target.value)}
                        >
                          {printTypes[productType]?.map((print) => (
                            <SelectItem key={print.id}>
                              {print.name}
                            </SelectItem>
                          ))}
                        </Select>
                      </div>
                      <div>
                        <Select
                          label="Размер"
                          placeholder="Выберите размер"
                          selectedKeys={[size]}
                          onChange={(e) => setSize(e.target.value)}
                        >
                          {sizes[productType]?.map((sizeOption) => (
                            <SelectItem key={sizeOption.id}>
                              {sizeOption.name}
                            </SelectItem>
                          ))}
                        </Select>
                      </div>
                    </div>
                    
                    <Input
                      type="number"
                      label="Количество"
                      placeholder="Введите количество"
                      value={quantity}
                      onValueChange={setQuantity}
                      min={1}
                    />
                  </div>
                )}
              </CardBody>
            </Card>
          </div>

          <div>
            <Card className="shadow-xs bg-primary-50">
              <CardHeader className="flex gap-3">
                <Icon icon="lucide:receipt" className="text-primary text-xl" />
                <div className="flex flex-col">
                  <p className="text-lg font-semibold">Результат расчета</p>
                  <p className="text-small text-default-500">Приблизительная стоимость</p>
                </div>
              </CardHeader>
              <Divider />
              <CardBody>
                <div className="flex flex-col gap-4">
                  <div>
                    <p className="text-foreground-600 mb-1">Тип продукции:</p>
                    <p className="font-medium">{PRODUCT_TYPES.find(p => p.id === productType)?.name || ''}</p>
                  </div>
                  <div>
                    <p className="text-foreground-600 mb-1">Тип бумаги:</p>
                    <p className="font-medium">{getPaperTypesForProduct(productType).find(p => p.id === paperType)?.name || ''}</p>
                  </div>
                  <div>
                    <p className="text-foreground-600 mb-1">Тип печати:</p>
                    <p className="font-medium">{printTypes[productType]?.find(p => p.id === printType)?.name || ''}</p>
                  </div>
                  <div>
                    <p className="text-foreground-600 mb-1">Размер:</p>
                    <p className="font-medium">{sizes[productType]?.find(s => s.id === size)?.name || ''}</p>
                  </div>
                  <div>
                    <p className="text-foreground-600 mb-1">Количество:</p>
                    <p className="font-medium">{quantity} шт.</p>
                  </div>

                  {formula && (
                    <div>
                      <p className="text-foreground-600 mb-1">Формула:</p>
                      <p className="font-medium text-sm">{formula.name}</p>
                    </div>
                  )}

                  <Divider />
                  
                  <div className="flex justify-between items-center">
                    <p className="text-lg font-semibold">Итоговая стоимость:</p>
                    <p className="text-xl font-bold text-primary">{totalPrice.toLocaleString()} ₽</p>
                  </div>

                  <Button 
                    color="primary" 
                    size="lg" 
                    className="w-full mt-2 transition-all duration-200 hover:scale-[1.02]"
                  >
                    <Icon icon="lucide:send" className="mr-2" />
                    Отправить запрос
                  </Button>
                  
                  <p className="text-xs text-foreground-500 text-center mt-2">
                    * Расчет является приблизительным. Для получения точной стоимости, пожалуйста, свяжитесь с нами.
                  </p>
                </div>
              </CardBody>
            </Card>
          </div>
        </motion.div>
      </div>
    </section>
  );
};
