import React from "react";
import { Navbar } from "./components/navbar";
import { Hero } from "./components/hero";
import { Products } from "./components/products";
import { Calculator } from "./components/calculator";
import { Footer } from "./components/footer";
import { About } from "./components/about";

export default function App() {

  return (
    <div className="min-h-screen bg-background">
      <Navbar />
      <main>
        <Hero />
        <Products />
        <Calculator />
        <About />
      </main>
      <Footer />
    </div>
  );
}