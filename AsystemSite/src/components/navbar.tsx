import React from "react";
import { Navbar as HeroNavbar, NavbarBrand, NavbarContent, NavbarItem, Link, Button } from "@heroui/react";
import { Icon } from "@iconify/react";

export const Navbar: React.FC = () => {
  return (
    <HeroNavbar maxWidth="xl" className="bg-background border-b border-divider">
      <NavbarBrand>
        <Icon icon="lucide:printer" className="text-primary text-2xl mr-2" />
        <p className="font-bold text-inherit">ПринтМастер</p>
      </NavbarBrand>
      <NavbarContent className="hidden sm:flex gap-4" justify="center">
        <NavbarItem>
          <Link color="foreground" href="#products">
            Продукция
          </Link>
        </NavbarItem>
        <NavbarItem>
          <Link color="foreground" href="#calculator">
            Калькулятор
          </Link>
        </NavbarItem>
        <NavbarItem>
          <Link color="foreground" href="#about">
            О нас
          </Link>
        </NavbarItem>
        <NavbarItem>
          <Link color="foreground" href="#contact">
            Контакты
          </Link>
        </NavbarItem>
      </NavbarContent>
      <NavbarContent justify="end">
        <NavbarItem>
          <Button as={Link} color="primary" href="#contact" variant="flat">
            <Icon icon="lucide:phone" className="mr-1" />
            Связаться с нами
          </Button>
        </NavbarItem>
      </NavbarContent>
    </HeroNavbar>
  );
};