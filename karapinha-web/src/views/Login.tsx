import "../styles/login.css";
import { logo } from "../components/Images";
import Button from "react-bootstrap/Button";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { Alert, Modal, Form } from "react-bootstrap";

export function Login() {
  const [usernameUtilizador, setUsername] = useState("");
  const [passwordUtilizador, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState(""); // Estado para mensagem de erro
  const [successMessage, setSuccessMessage] = useState(""); // Estado para mensagem de sucesso
  const [showModal, setShowModal] = useState(false); // Estado para mostrar o modal
  const [newPassword, setNewPassword] = useState(""); // Estado para nova senha

  const navigate = useNavigate();

  const handleLoginClick = (event:any) => {
    event.preventDefault();
    verifyLogin();
  };

  const handleRegisterClick = (event:any) => {
    event.preventDefault();
    navigate("/signup");
  };

  const handlePasswordChange = (event:any) => {
    event.preventDefault();
    // Implementar a lógica para trocar a senha
    changePassword();
  };

  function verifyLogin() {
    fetch("https://localhost:7209/Login", {
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      method: "POST",
      body: JSON.stringify({ usernameUtilizador, passwordUtilizador }),
    })
      .then((res) => res.json())
      .then((data) => {
        if (data) {
          const { tipoPerfil, estado } = data;
          localStorage.setItem('usernameUtilizador', usernameUtilizador);
          if (tipoPerfil === "cliente") {
            navigate("/logged");
          } else if (tipoPerfil === "administrador") {
            navigate("/adminHome");
          } else if (tipoPerfil === "administrativo") {
            if (estado === "activo") {
              navigate("/gestorHome");
            } else if (estado === "inactivo") {
              setShowModal(true);
            }
          }
          setSuccessMessage(
            "Login feito com sucesso, será redirecionado para a página apropriada."
          );
        } else {
          setErrorMessage("Credenciais incorretas. Tente novamente.");
        }
      })
      .catch(() => {
        setErrorMessage("Ocorreu um erro. Tente novamente.");
      });
  }

  function changePassword() {
    fetch("https://localhost:7209/ChangePassword", {
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      method: "POST",
      body: JSON.stringify({ usernameUtilizador, newPassword }),
    })
      .then((res) => {
        if (res.ok) {
          setShowModal(false);
          setSuccessMessage("Senha alterada com sucesso. Você será redirecionado.");
          navigate("/gestorHome");
        } else {
          setErrorMessage("Erro ao alterar a senha. Tente novamente.");
        }
      })
      .catch(() => {
        setErrorMessage("Ocorreu um erro ao alterar a senha. Tente novamente.");
      });
  }

  return (
    <div>
      <div className="main-container">
        <div className="imagem-container"></div>

        <div className="form-container">
          <img className="logo-image" src={logo} alt="Logo" />
          <form>
            <input
              type="text"
              placeholder="Username"
              value={usernameUtilizador}
              onChange={(e) => setUsername(e.target.value)}
            />
            <input
              type="password"
              placeholder="Password"
              value={passwordUtilizador}
              onChange={(e) => setPassword(e.target.value)}
            />

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
          {/* mensagem de erro no login */}
          {errorMessage && (
            <Alert variant="danger" className="m-0">
              {errorMessage}
            </Alert>
          )}
        </div>
      </div>

      {/* Modal para trocar senha */}
      <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Trocar Senha</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formNewPassword">
              <Form.Label>Nova Senha</Form.Label>
              <Form.Control
                type="password"
                placeholder="Digite a nova senha"
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
              />
            </Form.Group>
            <Button variant="primary" onClick={handlePasswordChange}>
              Trocar Senha
            </Button>
          </Form>
        </Modal.Body>
      </Modal>
    </div>
  );
}

export default Login;
