import { Button } from "../components/Button";
import "../styles/adminHome.css"

export function GestorHome (){
    return (
        <div className="principal-container">
            <ul className="nav">
        <li>
          <Button route="/" text="Profissionais" />
        </li>

        <li>
          <Button route="/" text="Serviços" />
        </li>

        <li>
          <Button route="/" text="Marcações" />
        </li>

        <li>
          <Button route="/" text="Sair" />
        </li>
      </ul>
        </div>
    );
}