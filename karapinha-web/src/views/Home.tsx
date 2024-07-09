import "../styles/home.css";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import Carousel from "react-bootstrap/Carousel";
import {
  carrosel4,
  carrosel2,
  carrosel3,
  sair,
  userLogo,
} from "../components/Images";
import { Button } from "../components/Button";
import { ButtonNav } from "../components/ButtonNav";

export function Home() {
  const navigate = useNavigate();
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex: any) => {
    setIndex(selectedIndex);
  };

  const handleDeleteStorage = () => {
    localStorage.clear();
    navigate("/");
  };

  return (
    <main>
      <div className="carousel-container">
        <ul className="nav">
          <li>
            <Button route="/logged" text="Home" />
          </li>

          <li>
            <Button route="/mymarcacoes" text="Minhas Marcações" />
          </li>

          <li>
            <Button route="/marcacoes" text="Serviços" />
          </li>
          

          <li>
            <Button
              route="/logged"
              onClick={() => handleDeleteStorage}
              text="Sobre"
            />
          </li>

          <div className="navegacao d-flex flex-row">
            <li className="nav1">
              <Button
                route="/editUser"
                imageSrc={userLogo}
                className="link-signup"
              />
            </li>
            <li className="nav2">
              <ButtonNav imageSrc={sair} onClick={handleDeleteStorage} className="link-button"/>
            </li>
          </div>
        </ul>
        <Carousel activeIndex={index} onSelect={handleSelect}>
          <Carousel.Item>
            <img
              className="carousel-container img"
              src={carrosel4}
              alt="Primeiro slide"
            />
            <Carousel.Caption>
              <h3>
                <Link to="/marcacoes">Makeup</Link>
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
