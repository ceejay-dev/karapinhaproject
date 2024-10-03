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

  const handleLoginClick = (event: any) => {
    event.preventDefault();
    if (!usernameUtilizador || !passwordUtilizador) {
      setErrorMessage("Deve preencher todos os campos.");
      return;
    }
    verifyLogin();
  };

  const handleRegisterClick = (event: any) => {
    event.preventDefault();
    navigate("/signup");
  };

  const handlePasswordChange = (event: any) => {
    event.preventDefault();
    changePassword();
  };

  function verifyLogin() {
    var serverCode = 0;
    fetch("https://localhost:7209/Login", {
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      method: "POST",
      body: JSON.stringify({ usernameUtilizador, passwordUtilizador }),
    })
      .then((res) => {
        if (res.status === 200) {
          return res.json();
        } else if (res.status === 401) {
          serverCode=res.status;
          setErrorMessage("A sua conta se encontra inactiva.");
          return;
        }
      })
      .then((data) => {
        const { idUtilizador, tipoPerfil, estado } = data;
        if (data) {
          localStorage.setItem("usernameUtilizador", usernameUtilizador);
          localStorage.setItem("idUtilizador", idUtilizador);

          if (tipoPerfil === "cliente") {
            if (estado == "activo") {
              navigate("/logged");
            } else {
              setErrorMessage(
                "A sua conta está inactiva.  Deverá contactar o administrador para activar a sua conta."
              );
            }
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
            "Login feito com sucesso, será redirecionado para a página apropriada. "
          );
        } else {
          if (serverCode===401){
            setErrorMessage("A sua conta se encontra inactiva.");
          } else {
            setErrorMessage("Credenciais incorretas. Tente novamente.");
          }
        }
      })
      .catch(() => {
        setErrorMessage("Credenciais incorretas. Tente novamente.");
      });
  }

  function changePassword() {
    if (!usernameUtilizador || !newPassword) {
      setErrorMessage("Por favor, preencha todos os campos.");
      return;
    }

    fetch(
      `https://localhost:7209/ActivateAndChangePassword?username=${usernameUtilizador}&password=${newPassword}`,
      {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        method: "PUT",
      }
    )
      .then((res) => {
        if (res.ok) {
          setShowModal(false);
          setSuccessMessage(
            "Senha alterada com sucesso. Você será redirecionado."
          );
          setTimeout(() => {
            navigate("/gestorHome");
          }, 3000);
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
              required
            />
            <input
              type="password"
              placeholder="Password"
              value={passwordUtilizador}
              onChange={(e) => setPassword(e.target.value)}
              required
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
          <Modal.Title>Trocar palavra-senha</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formNewPassword">
              <Form.Label>Nova palavra-passe</Form.Label>
              <Form.Control
                type="password"
                placeholder="Digite a nova palavra-passe"
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
              />
            </Form.Group>
            <Button variant="primary" onClick={handlePasswordChange}>
              Trocar palavra-passe
            </Button>
          </Form>
        </Modal.Body>
      </Modal>
    </div>
  );
}

export default Login;
