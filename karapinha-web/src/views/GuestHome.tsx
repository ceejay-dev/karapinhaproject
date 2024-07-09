import "../styles/home.css";
import { useState } from "react";
import { Link } from "react-router-dom";
import Carousel from "react-bootstrap/Carousel";
import { carrosel1, carrosel2, carrosel3 } from "../components/Images";
import { Button } from "../components/Button";

export function GuestHome() {
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex: any) => {
    setIndex(selectedIndex);
  };

  return (
    <main>
      <div className="carousel-container">
        <ul className="nav">
          <li>
            <Button route="/" text="Home" />
          </li>

          <li>
            <Button route="/marcacoes" text="Serviços" />
          </li>

          <li>
            <Button route="/about" text="Sobre" />
          </li>

          <div className="login-container d-flex flex-row">
            <li className="botao-nav">
              <Button route="/signup" text="Signup" className="link-signup" />
            </li>
            <li className="botao-nav2">
              <Button route="/Login" text="Login" />
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
                <Link to="/marcacoes">Makeup</Link>
              </h3>
              <p>Realce sua beleza com nossa maquiagem profissional. </p>
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
                <Link to="/marcacoes">Cabelo</Link>
              </h3>
              <p>
                Transforme seu visual com nossos cortes e estilos exclusivos.
              </p>
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
                <Link to="/marcacoes">Estética</Link>
              </h3>
              <p>
                Cuide de sua pele com nossos tratamentos estéticos
                personalizados
              </p>
            </Carousel.Caption>
          </Carousel.Item>
        </Carousel>
      </div>
    </main>
  );
}
