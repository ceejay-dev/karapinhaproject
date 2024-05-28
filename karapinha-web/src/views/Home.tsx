import "../styles/home.css";
import { useState } from "react";
import { Link } from "react-router-dom";
import Carousel from "react-bootstrap/Carousel";
import { carrosel1, carrosel2, carrosel3 } from "../assets";

export function Home() {
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex: any) => {
    setIndex(selectedIndex);
  };

  return (
    <main>
      <div className="carousel-container">
        <ul className="nav">
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/login">Serviços</Link>
          </li>
          <li>
            <Link to="/">Sobre</Link>
          </li>

          <div className="login-container d-flex flex-row">
          <li className="botao-nav">
            <Link to="/" className="link-signup">Signup</Link>
          </li>
          <li className="botao-nav2">
            <Link to="/login">Login</Link>
          </li>
          </div>
        </ul>
        <Carousel activeIndex={index} onSelect={handleSelect}>
          <Carousel.Item>
            <img
              className="carousel-container img"
              src={carrosel1}
              alt="Primeiro slide"
            />
            <Carousel.Caption>
              <h3>
                <Link to="/login">Makeup</Link>
              </h3>
              <p>Realce sua beleza com nossa maquiagem profissional.</p>
            </Carousel.Caption>
          </Carousel.Item>
          <Carousel.Item>
            <img
              className="carousel-container img"
              src={carrosel2}
              alt="Segundo slide"
            />
            <Carousel.Caption>
              <h3>
                <Link to="/login">Cabelo</Link>
              </h3>
              <p>Transforme seu visual com nossos cortes e estilos exclusivos.</p>
            </Carousel.Caption>
          </Carousel.Item>
          <Carousel.Item>
            <img
              className="carousel-container img"
              src={carrosel3}
              alt="Terceiro slide"
            />
            <Carousel.Caption>
              <h3>
                <Link to="/login">Estética</Link>
              </h3>
              <p>Cuide de sua pele com nossos tratamentos estéticos personalizados</p>
            </Carousel.Caption>
          </Carousel.Item>
        </Carousel>
      </div>
    </main>
  );
}
