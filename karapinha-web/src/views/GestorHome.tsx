import { useNavigate } from "react-router-dom";
import { Button } from "../components/Button";
import {
  Button as BootstrapButton,
} from "react-bootstrap";
import "../styles/adminHome.css";

export function GestorHome() {
  const navigate = useNavigate();
  const handleDeleteStorage = () => {
    localStorage.clear();
    navigate("/");
  };

  return (
    <div className="principal-container">
      <ul className="nav">
        <li>
          <Button route="/profissionais" text="Profissionais" />
        </li>

        <li>
          <Button route="/categorias" text="Categorias" />
        </li>

        <li>
          <Button route="/servicos" text="Serviços" />
        </li>

        <li>
          <Button route="/marcacoes" text="Marcações" />
        </li>

        <li>
          <BootstrapButton 
            className="link-button"
            onClick={() => {
              handleDeleteStorage();
            } }
           >
            Sair
          </BootstrapButton>
        </li>
      </ul>
    </div>
  );
}
