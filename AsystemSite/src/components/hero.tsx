import React from "react";
import { Button } from "@heroui/react";
import { Icon } from "@iconify/react";
import { motion } from "framer-motion";

export const Hero: React.FC = () => {
  return (
    <div className="relative bg-gradient-to-b from-primary-50 to-background py-16 md:py-24">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-12 items-center">
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5 }}
            className="text-center md:text-left"
          >
            <h1 className="text-4xl md:text-5xl font-bold text-foreground mb-4">
              Качественная печать для вашего бизнеса
            </h1>
            <p className="text-lg text-foreground-600 mb-8 max-w-lg">
              Мы предлагаем широкий спектр услуг печати — от визиток и брошюр до баннеров и рекламных материалов. Высокое качество, доступные цены и быстрые сроки.
            </p>
            <div className="flex flex-col sm:flex-row gap-4 justify-center md:justify-start">
              <Button 
                color="primary" 
                size="lg" 
                as="a" 
                href="#products"
                className="transition-all duration-200 hover:scale-[1.02]"
              >
                <Icon icon="lucide:shopping-bag" className="mr-2" />
                Наша продукция
              </Button>
              <Button 
                variant="flat" 
                color="primary" 
                size="lg" 
                as="a" 
                href="#calculator"
                className="transition-all duration-200 hover:scale-[1.02]"
              >
                <Icon icon="lucide:calculator" className="mr-2" />
                Рассчитать стоимость
              </Button>
            </div>
          </motion.div>
          <motion.div
            initial={{ opacity: 0, scale: 0.95 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ duration: 0.5, delay: 0.2 }}
            className="relative"
          >
            <div className="relative h-64 md:h-96 overflow-hidden rounded-lg shadow-lg">
              <img 
                src="https://img.heroui.chat/image/ai?w=800&h=600&u=printing-company-hero" 
                alt="Печатная продукция" 
                className="w-full h-full object-cover"
              />
              <div className="absolute inset-0 bg-gradient-to-t from-black/50 to-transparent"></div>
              <div className="absolute bottom-4 left-4 right-4 text-white">
                <p className="font-medium">Современное оборудование и материалы высшего качества</p>
              </div>
            </div>
          </motion.div>
        </div>
      </div>
    </div>
  );
};