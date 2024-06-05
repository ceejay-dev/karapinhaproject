import "../styles/login.css";
import Button from 'react-bootstrap/Button';

export function Login() {
  return (
    <div className="main-container">
      <div className="imagem-container">
      </div>

      <div className="form-container">
        <form action="">
          <input type="text" placeholder="Username" />
          <input type="password" placeholder="Password" />

          <div className="button-container">
            <div className="botao">
              <Button variant="outline-dark" type="submit">Login</Button>
            </div>

            <div className="botao">
              <Button variant="dark" type="submit">Registar</Button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}
