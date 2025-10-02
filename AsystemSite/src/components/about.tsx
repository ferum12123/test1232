import React from "react";
import { Card, CardBody } from "@heroui/react";
import { Icon } from "@iconify/react";
import { motion } from "framer-motion";

export const About: React.FC = () => {
  const features = [
    {
      icon: "lucide:printer",
      title: "Современное оборудование",
      description: "Мы используем только современное оборудование, которое позволяет нам обеспечивать высокое качество печати и быстрые сроки выполнения заказов."
    },
    {
      icon: "lucide:award",
      title: "Качественные материалы",
      description: "Мы работаем только с проверенными поставщиками материалов, что гарантирует высокое качество конечной продукции."
    },
    {
      icon: "lucide:users",
      title: "Опытные специалисты",
      description: "Наша команда состоит из опытных специалистов, которые помогут вам с выбором материалов и технологии печати."
    },
    {
      icon: "lucide:clock",
      title: "Соблюдение сроков",
      description: "Мы ценим ваше время и всегда выполняем заказы в оговоренные сроки."
    },
    {
      icon: "lucide:palette",
      title: "Дизайн-услуги",
      description: "Наши дизайнеры помогут вам создать уникальный макет для вашей печатной продукции."
    },
    {
      icon: "lucide:truck",
      title: "Доставка",
      description: "Мы предлагаем различные варианты доставки, включая самовывоз, курьерскую доставку и отправку почтой."
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
    <section id="about" className="py-16 bg-content2">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-12">
          <h2 className="text-3xl font-bold text-foreground mb-4">О нашей типографии</h2>
          <p className="text-foreground-600 max-w-2xl mx-auto">
            Мы предоставляем полный спектр полиграфических услуг для бизнеса и частных лиц.
            Наша типография работает на рынке более 10 лет и за это время мы накопили огромный опыт
            в производстве различной печатной продукции.
          </p>
        </div>

        <motion.div 
          variants={container}
          initial="hidden"
          whileInView="show"
          viewport={{ once: true, amount: 0.2 }}
          className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6"
        >
          {features.map((feature, index) => (
            <motion.div key={index} variants={item}>
              <Card className="h-full hover:shadow-md transition-all duration-300">
                <CardBody className="flex flex-col items-center text-center p-6">
                  <div className="bg-primary-100 p-3 rounded-full mb-4">
                    <Icon icon={feature.icon} className="text-primary text-2xl" />
                  </div>
                  <h3 className="text-xl font-semibold mb-2">{feature.title}</h3>
                  <p className="text-foreground-600">{feature.description}</p>
                </CardBody>
              </Card>
            </motion.div>
          ))}
        </motion.div>

        <div className="mt-16 bg-content1 rounded-lg p-8 shadow-xs">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-8 items-center">
            <motion.div
              initial={{ opacity: 0, x: -20 }}
              whileInView={{ opacity: 1, x: 0 }}
              transition={{ duration: 0.5 }}
              viewport={{ once: true }}
            >
              <h3 className="text-2xl font-bold mb-4">Наша история</h3>
              <p className="text-foreground-600 mb-4">
                Наша типография была основана в 2012 году с небольшого офиса и одного печатного станка.
                За прошедшие годы мы значительно расширили наши производственные мощности и спектр предоставляемых услуг.
              </p>
              <p className="text-foreground-600 mb-4">
                Сегодня мы являемся одной из ведущих типографий в регионе, обслуживающей как малый бизнес,
                так и крупные компании. Наша миссия — предоставлять качественные полиграфические услуги
                по доступным ценам с индивидуальным подходом к каждому клиенту.
              </p>
              <p className="text-foreground-600">
                Мы постоянно следим за новыми технологиями и тенденциями в области полиграфии,
                чтобы предлагать нашим клиентам самые современные решения.
              </p>
            </motion.div>
            <motion.div
              initial={{ opacity: 0, x: 20 }}
              whileInView={{ opacity: 1, x: 0 }}
              transition={{ duration: 0.5 }}
              viewport={{ once: true }}
              className="relative h-64 md:h-80 overflow-hidden rounded-lg"
            >
              <img 
                src="https://img.heroui.chat/image/ai?w=800&h=600&u=printing-company-office" 
                alt="Наша типография" 
                className="w-full h-full object-cover"
              />
            </motion.div>
          </div>
        </div>
      </div>
    </section>
  );
};