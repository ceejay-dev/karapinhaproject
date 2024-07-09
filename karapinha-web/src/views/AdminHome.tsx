import { Button as BootstrapButton } from "react-bootstrap";
import "../styles/administradorHome.css";
import { Button } from "../components/Button";
import { useNavigate } from "react-router-dom";

export function AdminHome() {
  const navigate = useNavigate();
  const handleDeleteStorage = () => {
    localStorage.clear();
    navigate("/");
  };

  return (
    <main className="back-image">
      <ul className="nav">
        <li>
          <Button route="/clientes" text="Clientes " />
        </li>

        <li>
          <Button route="/administrativos" text="Administrativos " />
        </li>

        <li>
          <BootstrapButton
            className="link-button"
            onClick={() => {
              handleDeleteStorage();
            }}
          >
            Sair
          </BootstrapButton>
        </li>
      </ul>
    </main>
  );
}
