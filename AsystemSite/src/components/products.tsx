import React from "react";
import { Card, CardBody, Image } from "@heroui/react";
import { Icon } from "@iconify/react";
import { motion } from "framer-motion";

interface ProductsProps {}

interface Product {
  id: number;
  name: string;
  description: string;
  image: string;
  icon: string;
}

export const Products: React.FC<ProductsProps> = () => {
  const products: Product[] = [
    {
      id: 1,
      name: "Визитки",
      description: "Стандартные и премиум визитки на различных типах бумаги с разными видами отделки.",
      image: "https://img.heroui.chat/image/ai?w=600&h=400&u=business-cards-printing",
      icon: "lucide:credit-card"
    },
    {
      id: 2,
      name: "Брошюры и каталоги",
      description: "Многостраничные издания разных форматов с различными вариантами скрепления.",
      image: "https://img.heroui.chat/image/ai?w=600&h=400&u=brochures-catalogs-printing",
      icon: "lucide:book-open"
    },
    {
      id: 3,
      name: "Плакаты и афиши",
      description: "Крупноформатная печать для рекламных и информационных целей.",
      image: "https://img.heroui.chat/image/ai?w=600&h=400&u=posters-printing",
      icon: "lucide:image"
    },
    {
      id: 4,
      name: "Баннеры",
      description: "Широкоформатная печать на виниле и других материалах для наружной рекламы.",
      image: "https://img.heroui.chat/image/ai?w=600&h=400&u=banners-printing",
      icon: "lucide:flag"
    },
    {
      id: 5,
      name: "Календари",
      description: "Настенные, настольные и карманные календари с индивидуальным дизайном.",
      image: "https://img.heroui.chat/image/ai?w=600&h=400&u=calendars-printing",
      icon: "lucide:calendar"
    },
    {
      id: 6,
      name: "Сувенирная продукция",
      description: "Печать на кружках, футболках, ручках и других сувенирах.",
      image: "https://img.heroui.chat/image/ai?w=600&h=400&u=promotional-items-printing",
      icon: "lucide:gift"
    }
  ];

  const container = {
    hidden: { opacity: 0 },
    show: {
      opacity: 1,
      transition: {
        staggerChildren: 0.1
      }
    }
  };

  const item = {
    hidden: { opacity: 0, y: 20 },
    show: { opacity: 1, y: 0, transition: { duration: 0.5 } }
  };

  return (
    <section id="products" className="py-16 bg-content1">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-12">
          <h2 className="text-3xl font-bold text-foreground mb-4">Наша продукция</h2>
          <p className="text-foreground-600 max-w-2xl mx-auto">
            Мы предлагаем широкий спектр печатной продукции для любых потребностей вашего бизнеса.
            Выберите интересующую вас категорию или свяжитесь с нами для индивидуального заказа.
          </p>
        </div>

        <motion.div 
          variants={container}
          initial="hidden"
          whileInView="show"
          viewport={{ once: true, amount: 0.2 }}
          className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6"
        >
          {products.map((product) => (
            <motion.div key={product.id} variants={item}>
              <Card className="h-full overflow-visible hover:shadow-md transition-all duration-300">
                <CardBody className="p-0 overflow-hidden">
                  <Image
                    removeWrapper
                    alt={product.name}
                    className="w-full h-48 object-cover"
                    src={product.image}
                  />
                  <div className="p-4">
                    <div className="flex items-center gap-2 mb-2">
                      <Icon icon={product.icon} className="text-primary text-xl" />
                      <h3 className="text-xl font-semibold">{product.name}</h3>
                    </div>
                    <p className="text-foreground-600">{product.description}</p>
                  </div>
                </CardBody>
              </Card>
            </motion.div>
          ))}
        </motion.div>
      </div>
    </section>
  );
};