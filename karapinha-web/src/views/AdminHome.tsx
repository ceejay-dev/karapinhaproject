import { Button } from "../components/Button";
import "../styles/adminHome.css";

export function AdminHome() {
  return (
    <div className="principal-container">
      <ul className="nav">
        <li>
          <Button route="/" text="Clientes" />
        </li>

        <li>
          <Button route="/" text="Administrativos" />
        </li>

        <li>
          <Button route="/" text="Sair" />
        </li>
      </ul>
    </div>
  );
}
