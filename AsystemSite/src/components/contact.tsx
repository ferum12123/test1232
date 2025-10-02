import React from "react";
import { Card, CardBody, Input, Textarea, Button, addToast } from "@heroui/react";
import { Icon } from "@iconify/react";
import { motion } from "framer-motion";

export const Contact: React.FC = () => {
  const [formData, setFormData] = React.useState({
    name: "",
    email: "",
    phone: "",
    message: ""
  });
  
  const [errors, setErrors] = React.useState<Record<string, string>>({});
  const [isSubmitting, setIsSubmitting] = React.useState(false);

  const validate = () => {
    const newErrors: Record<string, string> = {};
    
    if (!formData.name.trim()) {
      newErrors.name = "Пожалуйста, укажите ваше имя";
    }
    
    if (!formData.email.trim()) {
      newErrors.email = "Пожалуйста, укажите ваш email";
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = "Пожалуйста, укажите корректный email";
    }
    
    if (!formData.phone.trim()) {
      newErrors.phone = "Пожалуйста, укажите ваш телефон";
    }
    
    if (!formData.message.trim()) {
      newErrors.message = "Пожалуйста, введите сообщение";
    }
    
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
    
    // Clear error when field is edited
    if (errors[name]) {
      setErrors(prev => ({ ...prev, [name]: "" }));
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validate()) return;
    
    setIsSubmitting(true);
    
    // Simulate API call
    try {
      await new Promise(resolve => setTimeout(resolve, 1500));
      
      addToast({
        title: "Сообщение отправлено!",
        description: "Мы свяжемся с вами в ближайшее время.",
        icon: <Icon icon="lucide:check-circle" className="text-success text-xl" />,
      });
      
      setFormData({
        name: "",
        email: "",
        phone: "",
        message: ""
      });
    } catch (error) {
      addToast({
        title: "Ошибка!",
        description: "Произошла ошибка при отправке сообщения. Пожалуйста, попробуйте еще раз.",
        icon: <Icon icon="lucide:alert-circle" className="text-danger text-xl" />,
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  const contactInfo = [
    {
      icon: "lucide:map-pin",
      title: "Адрес",
      details: ["ул. Печатников, д. 42", "г. Москва, 123456"]
    },
    {
      icon: "lucide:phone",
      title: "Телефон",
      details: ["+7 (495) 123-45-67", "+7 (495) 765-43-21"]
    },
    {
      icon: "lucide:mail",
      title: "Email",
      details: ["info@printmaster.ru", "orders@printmaster.ru"]
    },
    {
      icon: "lucide:clock",
      title: "Режим работы",
      details: ["Пн-Пт: 9:00 - 18:00", "Сб: 10:00 - 15:00", "Вс: выходной"]
    }
  ];

  return (
    <section id="contact" className="py-16 bg-content1">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-12">
          <h2 className="text-3xl font-bold text-foreground mb-4">Свяжитесь с нами</h2>
          <p className="text-foreground-600 max-w-2xl mx-auto">
            Если у вас есть вопросы или вы хотите оформить заказ, заполните форму ниже или воспользуйтесь
            контактной информацией. Мы свяжемся с вами в ближайшее время.
          </p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5 }}
            viewport={{ once: true }}
            className="lg:col-span-2"
          >
            <Card className="shadow-xs">
              <CardBody className="p-6">
                <h3 className="text-xl font-semibold mb-4">Отправить сообщение</h3>
                <form onSubmit={handleSubmit} className="space-y-4">
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <Input
                      label="Ваше имя"
                      placeholder="Введите ваше имя"
                      name="name"
                      value={formData.name}
                      onChange={handleChange}
                      isRequired
                      isInvalid={!!errors.name}
                      errorMessage={errors.name}
                    />
                    <Input
                      label="Email"
                      placeholder="Введите ваш email"
                      name="email"
                      value={formData.email}
                      onChange={handleChange}
                      isRequired
                      isInvalid={!!errors.email}
                      errorMessage={errors.email}
                    />
                  </div>
                  <Input
                    label="Телефон"
                    placeholder="Введите ваш телефон"
                    name="phone"
                    value={formData.phone}
                    onChange={handleChange}
                    isRequired
                    isInvalid={!!errors.phone}
                    errorMessage={errors.phone}
                  />
                  <Textarea
                    label="Сообщение"
                    placeholder="Введите ваше сообщение"
                    name="message"
                    value={formData.message}
                    onChange={handleChange}
                    isRequired
                    isInvalid={!!errors.message}
                    errorMessage={errors.message}
                  />
                  <Button 
                    type="submit" 
                    color="primary" 
                    className="w-full transition-all duration-200 hover:scale-[1.02]"
                    isLoading={isSubmitting}
                  >
                    {!isSubmitting && <Icon icon="lucide:send" className="mr-2" />}
                    Отправить сообщение
                  </Button>
                </form>
              </CardBody>
            </Card>
          </motion.div>

          <motion.div
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.2 }}
            viewport={{ once: true }}
          >
            <Card className="shadow-xs h-full">
              <CardBody className="p-6">
                <h3 className="text-xl font-semibold mb-6">Контактная информация</h3>
                <div className="space-y-6">
                  {contactInfo.map((item, index) => (
                    <div key={index} className="flex items-start">
                      <div className="bg-primary-100 p-2 rounded-full mr-4">
                        <Icon icon={item.icon} className="text-primary text-xl" />
                      </div>
                      <div>
                        <h4 className="font-medium text-foreground">{item.title}</h4>
                        {item.details.map((detail, i) => (
                          <p key={i} className="text-foreground-600">{detail}</p>
                        ))}
                      </div>
                    </div>
                  ))}
                </div>

                <div className="mt-8">
                  <h4 className="font-medium text-foreground mb-3">Мы в социальных сетях</h4>
                  <div className="flex gap-3">
                    <Button isIconOnly variant="flat" size="sm" className="text-foreground-600 hover:text-primary">
                      <Icon icon="lucide:facebook" className="text-xl" />
                    </Button>
                    <Button isIconOnly variant="flat" size="sm" className="text-foreground-600 hover:text-primary">
                      <Icon icon="lucide:instagram" className="text-xl" />
                    </Button>
                    <Button isIconOnly variant="flat" size="sm" className="text-foreground-600 hover:text-primary">
                      <Icon icon="lucide:twitter" className="text-xl" />
                    </Button>
                    <Button isIconOnly variant="flat" size="sm" className="text-foreground-600 hover:text-primary">
                      <Icon icon="lucide:youtube" className="text-xl" />
                    </Button>
                  </div>
                </div>
              </CardBody>
            </Card>
          </motion.div>
        </div>

        <div className="mt-12">
          <Card className="shadow-xs overflow-hidden">
            <CardBody className="p-0 h-[400px]">
              <iframe 
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2245.5887635376636!2d37.61744231594884!3d55.75197998055754!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46b54a50b315e573%3A0xa886bf5a3d9b2e68!2sThe%20Moscow%20Kremlin!5e0!3m2!1sen!2sru!4v1629884750172!5m2!1sen!2sru" 
                width="100%" 
                height="100%" 
                style={{ border: 0 }} 
                allowFullScreen 
                loading="lazy" 
                title="Карта"
              ></iframe>
            </CardBody>
          </Card>
        </div>
      </div>
    </section>
  );
};