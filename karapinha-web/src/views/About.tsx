import "../styles/login.css";
import { logo } from "../components/Images";

export function About() {
  return (
    <div>
      <div className="main-container">
        <div className="imagem-container"></div>

        <div className="form-container">
          <img className="logo-image" src={logo} alt="Logo" />
          <p className="text-center ms-3 me-3 fst-italic">
            O salão Karapinha XPTO é um dos mais renomados em Angola, conhecido
            por sua excelência em serviços de beleza e bem-estar. Oferecendo uma
            ampla gama de tratamentos, desde cortes de cabelo sofisticados até
            cuidados de pele de alta qualidade, o salão se destaca pela sua
            equipe de profissionais altamente qualificados. Com um ambiente
            acolhedor e sofisticado, o Karapinha XPTO garante uma experiência
            única e personalizada para cada cliente. Sempre à frente das
            tendências, o salão é sinônimo de inovação e qualidade no setor de
            beleza!
          </p>

          <span className="text-center fst-italic fw-bold">
            Made by Cândido Ucuahamba &copy;{" "}
          </span>
        </div>
      </div>
    </div>
  );
}

export default About;
