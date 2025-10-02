import React from "react";
import { Modal, ModalContent, ModalHeader, ModalBody, ModalFooter, Button, Input, Textarea, Checkbox, Select, SelectItem, useDisclosure, addToast } from "@heroui/react";
import { Icon } from "@iconify/react";

interface OrderFormProps {
  isOpen: boolean;
  onClose: () => void;
  selectedProduct: string | null;
}

export const OrderForm: React.FC<OrderFormProps> = ({ isOpen, onClose, selectedProduct }) => {
  const { onOpenChange } = useDisclosure();
  
  const [formData, setFormData] = React.useState({
    name: "",
    email: "",
    phone: "",
    company: "",
    product: selectedProduct || "",
    quantity: "",
    comments: "",
    hasDesign: false,
    needDesign: false,
    deliveryMethod: "pickup"
  });
  
  const [errors, setErrors] = React.useState<Record<string, string>>({});
  const [isSubmitting, setIsSubmitting] = React.useState(false);

  React.useEffect(() => {
    if (selectedProduct) {
      setFormData(prev => ({ ...prev, product: selectedProduct }));
    }
  }, [selectedProduct]);

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
    
    if (!formData.product.trim()) {
      newErrors.product = "Пожалуйста, выберите продукт";
    }
    
    if (!formData.quantity.trim()) {
      newErrors.quantity = "Пожалуйста, укажите количество";
    } else if (isNaN(Number(formData.quantity)) || Number(formData.quantity) <= 0) {
      newErrors.quantity = "Пожалуйста, укажите корректное количество";
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

  const handleCheckboxChange = (name: string, checked: boolean) => {
    setFormData(prev => ({ ...prev, [name]: checked }));
  };

  const handleSelectChange = (name: string, value: string) => {
    setFormData(prev => ({ ...prev, [name]: value }));
    
    // Clear error when field is edited
    if (errors[name]) {
      setErrors(prev => ({ ...prev, [name]: "" }));
    }
  };

  const handleSubmit = async () => {
    if (!validate()) return;
    
    setIsSubmitting(true);
    
    // Simulate API call
    try {
      await new Promise(resolve => setTimeout(resolve, 1500));
      
      addToast({
        title: "Заказ отправлен!",
        description: "Мы свяжемся с вами в ближайшее время для уточнения деталей.",
        icon: <Icon icon="lucide:check-circle" className="text-success text-xl" />,
      });
      
      onClose();
      setFormData({
        name: "",
        email: "",
        phone: "",
        company: "",
        product: "",
        quantity: "",
        comments: "",
        hasDesign: false,
        needDesign: false,
        deliveryMethod: "pickup"
      });
    } catch (error) {
      addToast({
        title: "Ошибка!",
        description: "Произошла ошибка при отправке заказа. Пожалуйста, попробуйте еще раз.",
        icon: <Icon icon="lucide:alert-circle" className="text-danger text-xl" />,
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Modal 
      isOpen={isOpen} 
      onOpenChange={onOpenChange}
      onClose={onClose}
      size="2xl"
      scrollBehavior="inside"
    >
      <ModalContent>
        {(onClose) => (
          <>
            <ModalHeader className="flex flex-col gap-1">
              <div className="flex items-center gap-2">
                <Icon icon="lucide:file-text" className="text-primary text-xl" />
                <span>Оформление заказа</span>
              </div>
            </ModalHeader>
            <ModalBody>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Input
                  autoFocus
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
                <Input
                  label="Компания"
                  placeholder="Введите название компании"
                  name="company"
                  value={formData.company}
                  onChange={handleChange}
                />
                <Input
                  label="Продукт"
                  placeholder="Укажите тип продукции"
                  name="product"
                  value={formData.product}
                  onChange={handleChange}
                  isRequired
                  isInvalid={!!errors.product}
                  errorMessage={errors.product}
                />
                <Input
                  label="Количество"
                  placeholder="Укажите количество"
                  name="quantity"
                  type="number"
                  min="1"
                  value={formData.quantity}
                  onChange={handleChange}
                  isRequired
                  isInvalid={!!errors.quantity}
                  errorMessage={errors.quantity}
                />
                <div className="md:col-span-2">
                  <Select
                    label="Способ доставки"
                    placeholder="Выберите способ доставки"
                    selectedKeys={[formData.deliveryMethod]}
                    onChange={(e) => handleSelectChange("deliveryMethod", e.target.value)}
                    className="mb-4"
                  >
                    <SelectItem key="pickup" value="pickup">
                      Самовывоз
                    </SelectItem>
                    <SelectItem key="courier" value="courier">
                      Курьерская доставка
                    </SelectItem>
                    <SelectItem key="post" value="post">
                      Почта России
                    </SelectItem>
                  </Select>
                </div>
                <div className="md:col-span-2">
                  <Textarea
                    label="Комментарии к заказу"
                    placeholder="Укажите дополнительные требования или пожелания"
                    name="comments"
                    value={formData.comments}
                    onChange={handleChange}
                  />
                </div>
                <div className="md:col-span-2 flex flex-col gap-2">
                  <Checkbox
                    isSelected={formData.hasDesign}
                    onValueChange={(checked) => handleCheckboxChange("hasDesign", checked)}
                  >
                    У меня есть готовый дизайн-макет
                  </Checkbox>
                  <Checkbox
                    isSelected={formData.needDesign}
                    onValueChange={(checked) => handleCheckboxChange("needDesign", checked)}
                  >
                    Мне нужна разработка дизайна
                  </Checkbox>
                </div>
              </div>
            </ModalBody>
            <ModalFooter>
              <Button color="danger" variant="light" onPress={onClose}>
                Отмена
              </Button>
              <Button 
                color="primary" 
                onPress={handleSubmit}
                isLoading={isSubmitting}
                className="transition-all duration-200 hover:scale-[1.02]"
              >
                {!isSubmitting && <Icon icon="lucide:send" className="mr-2" />}
                Отправить заказ
              </Button>
            </ModalFooter>
          </>
        )}
      </ModalContent>
    </Modal>
  );
};