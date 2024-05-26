import "../styles/home.css";
import { useState } from "react";
import { Link } from "react-router-dom";
import Carousel from "react-bootstrap/Carousel";

export function Home() {
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex: any) => {
    setIndex(selectedIndex);
  };

  return (
    <main>
      <div className="main-container card">
        <header>
          <nav>
            <ul>
              <li>
              <Link to='/'>Home</Link>
              </li>
              <li>
              <Link to='/login'>Serviços</Link>
              </li>
              <li>
              <Link to='/'>Sobre</Link>
              </li>

            </ul>
          </nav>
        </header>
      </div>

      <div className="carousel-container">
          <Carousel activeIndex={index} onSelect={handleSelect}>
            <Carousel.Item>
              <img
                className="carousel-container"
                src="../assets/carrosel1.jpeg"
                alt="Primeiro slide"
              />
              <Carousel.Caption>
                <h3>First slide label</h3>
                <p>
                  Nulla vitae elit libero, a pharetra augue mollis interdum.
                </p>
              </Carousel.Caption>
            </Carousel.Item>
            <Carousel.Item>
              <img
                className="carousel-container"
                src="../assets/carrosel2.jpeg"
                alt="Segundo slide"
              />
              <Carousel.Caption>
                <h3>Second slide label</h3>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
              </Carousel.Caption>
            </Carousel.Item>
            <Carousel.Item>
              <img
                className="carousel-container"
                src="../assets/carrosel3.jpeg"
                alt="Terceiro slide"
              />
              <Carousel.Caption>
                <h3>Third slide label</h3>
                <p>
                  Praesent commodo cursus magna, vel scelerisque nisl
                  consectetur.
                </p>
              </Carousel.Caption>
            </Carousel.Item>
          </Carousel>
      </div>
    </main>
  );
}
