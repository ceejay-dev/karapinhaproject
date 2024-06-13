import { Button } from "../components/Button";
import "../styles/adminHome.css"

export function GestorHome (){
    return (
        <div className="principal-container">
            <ul className="nav">
        <li>
          <Button route="/profissionais" text="Profissionais" />
        </li>

        <li>
          <Button route="/servicos" text="Serviços" />
        </li>

        <li>
          <Button route="/categorias" text="Categoria" />
        </li>

        <li>
          <Button route="/marcacoes" text="Marcações" />
        </li>

        <li>
          <Button route="/" text="Sair" />
        </li>
      </ul>
        </div>
    );
}