import "../styles/login.css"; 
import { logo } from '../components/Images';
import Button from 'react-bootstrap/Button';
import { useNavigate } from 'react-router-dom';
import { useState } from "react";
import { Alert } from "react-bootstrap";

export function Login() {
  const [usernameUtilizador, setUsername] = useState("");
  const [passwordUtilizador, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState(""); // Estado para mensagem de erro
  const [successMessage, setSucessMessage] = useState(""); // Estado para mensagem de sucesso

  const navigate = useNavigate();

  const handleLoginClick = (event:any) => {
    event.preventDefault(); 
    verifyLogin();
  };

  const handleRegisterClick = (event:any) => {
    event.preventDefault();
    navigate('/signup'); 
  };

  function verifyLogin() {
   // console.log(usernameUtilizador, passwordUtilizador);
    fetch("https://localhost:7209/Login",
      {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          method: "POST",
          body: JSON.stringify({ usernameUtilizador, passwordUtilizador })
      })
      .then(res => {
        if (res.ok) {
          setSucessMessage("Login feito com sucesso, será redirecionado para a página principal.")
          navigate('/logged');
        } else {
          setErrorMessage("Credenciais incorretas. Tente novamente.");
        }
      })
      .catch(() => {
        setErrorMessage("Ocorreu um erro. Tente novamente.");
      });
  }

  return (
    <div>
      <div className="main-container">
        <div className="imagem-container">
        </div>

        <div className="form-container">
          <img className="logo-image" src={logo} alt="Logo" />
          <form>
            <input type="text" placeholder="Username" value={usernameUtilizador} onChange={(e) => setUsername(e.target.value)} />
            <input type="password" placeholder="Password" value={passwordUtilizador} onChange={(e) => setPassword(e.target.value)} />

            <div className="button-container">
              <div className="m-1">
                <Button variant="dark" onClick={handleLoginClick}>
                  Login
                </Button>
              </div>

              <div className="m-1">
                <Button variant="outline-dark" onClick={handleRegisterClick}>
                  Registar
                </Button>
              </div>

            </div>
          </form>
          {/* mensagem de sucesso no login */}
          {successMessage && (
          <Alert variant="success" className="m-0">
           {successMessage}
          </Alert>
        )}
          {/* mensagem de erro no login*/}
          {errorMessage && (
          <Alert variant="danger" className="m-0">
           {errorMessage}
          </Alert>
        )}
          
        </div>
      </div>
    </div>
  );
}

export default Login;
