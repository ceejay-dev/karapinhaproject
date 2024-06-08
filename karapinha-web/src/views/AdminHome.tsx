import { Button } from "../components/Button";
import "../styles/adminHome.css";

export function AdminHome() {
  return (
    <div className="principal-container">
      <ul className="nav">
        <li>
          <Button route="/clientes" text="Clientes" />
        </li>

        <li>
          <Button route="/administrativos" text="Administrativos" />
        </li>

        <li>
          <Button route="/guest" text="Sair" />
        </li>
      </ul>
    </div>
  );
}
