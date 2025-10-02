import React from "react";
import { Link, Divider } from "@heroui/react";
import { Icon } from "@iconify/react";

export const Footer: React.FC = () => {
  return (
    <footer className="bg-content3 text-foreground py-12">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          <div>
            <div className="flex items-center mb-4">
              <Icon icon="lucide:printer" className="text-primary text-2xl mr-2" />
              <span className="font-bold text-xl">ПринтМастер</span>
            </div>
            <p className="text-foreground-600 mb-4">
              Качественная печать для вашего бизнеса. Мы предлагаем широкий спектр услуг печати с высоким качеством и доступными ценами.
            </p>
            <div className="flex gap-3">
              <Link href="#" className="text-foreground-600 hover:text-primary">
                <Icon icon="lucide:facebook" className="text-xl" />
              </Link>
              <Link href="#" className="text-foreground-600 hover:text-primary">
                <Icon icon="lucide:instagram" className="text-xl" />
              </Link>
              <Link href="#" className="text-foreground-600 hover:text-primary">
                <Icon icon="lucide:twitter" className="text-xl" />
              </Link>
              <Link href="#" className="text-foreground-600 hover:text-primary">
                <Icon icon="lucide:youtube" className="text-xl" />
              </Link>
            </div>
          </div>

          <div>
            <h3 className="font-semibold text-lg mb-4">Услуги</h3>
            <ul className="space-y-2">
              <li>
                <Link href="#products" className="text-foreground-600 hover:text-primary">Визитки</Link>
              </li>
              <li>
                <Link href="#products" className="text-foreground-600 hover:text-primary">Брошюры и каталоги</Link>
              </li>
              <li>
                <Link href="#products" className="text-foreground-600 hover:text-primary">Плакаты и афиши</Link>
              </li>
              <li>
                <Link href="#products" className="text-foreground-600 hover:text-primary">Баннеры</Link>
              </li>
              <li>
                <Link href="#products" className="text-foreground-600 hover:text-primary">Календари</Link>
              </li>
              <li>
                <Link href="#products" className="text-foreground-600 hover:text-primary">Сувенирная продукция</Link>
              </li>
            </ul>
          </div>

          <div>
            <h3 className="font-semibold text-lg mb-4">Компания</h3>
            <ul className="space-y-2">
              <li>
                <Link href="#about" className="text-foreground-600 hover:text-primary">О нас</Link>
              </li>
              <li>
                <Link href="#" className="text-foreground-600 hover:text-primary">Наши клиенты</Link>
              </li>
              <li>
                <Link href="#" className="text-foreground-600 hover:text-primary">Портфолио</Link>
              </li>
              <li>
                <Link href="#" className="text-foreground-600 hover:text-primary">Блог</Link>
              </li>
              <li>
                <Link href="#" className="text-foreground-600 hover:text-primary">Вакансии</Link>
              </li>
              <li>
                <Link href="#contact" className="text-foreground-600 hover:text-primary">Контакты</Link>
              </li>
            </ul>
          </div>

          <div>
            <h3 className="font-semibold text-lg mb-4">Контакты</h3>
            <ul className="space-y-3">
              <li className="flex items-start">
                <Icon icon="lucide:map-pin" className="text-primary mr-2 mt-1" />
                <span className="text-foreground-600">ул. Печатников, д. 42, г. Москва, 123456</span>
              </li>
              <li className="flex items-center">
                <Icon icon="lucide:phone" className="text-primary mr-2" />
                <span className="text-foreground-600">+7 (495) 123-45-67</span>
              </li>
              <li className="flex items-center">
                <Icon icon="lucide:mail" className="text-primary mr-2" />
                <span className="text-foreground-600">info@printmaster.ru</span>
              </li>
              <li className="flex items-start">
                <Icon icon="lucide:clock" className="text-primary mr-2 mt-1" />
                <div className="text-foreground-600">
                  <div>Пн-Пт: 9:00 - 18:00</div>
                  <div>Сб: 10:00 - 15:00</div>
                  <div>Вс: выходной</div>
                </div>
              </li>
            </ul>
          </div>
        </div>

        <Divider className="my-8" />

        <div className="flex flex-col md:flex-row justify-between items-center">
          <p className="text-foreground-600 text-sm mb-4 md:mb-0">
            © {new Date().getFullYear()} ПринтМастер. Все права защищены.
          </p>
          <div className="flex gap-4">
            <Link href="#" className="text-foreground-600 hover:text-primary text-sm">Политика конфиденциальности</Link>
            <Link href="#" className="text-foreground-600 hover:text-primary text-sm">Условия использования</Link>
            <Link href="#" className="text-foreground-600 hover:text-primary text-sm">Карта сайта</Link>
          </div>
        </div>
      </div>
    </footer>
  );
};