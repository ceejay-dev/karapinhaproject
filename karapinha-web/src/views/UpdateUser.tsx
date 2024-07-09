import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import "../styles/signup.css";
import { Button, Alert, Form } from "react-bootstrap";

type UserProps = {
  idUtilizador: number;
  nomeUtilizador: string;
  emailUtilizador: string;
  telemovelUtilizador: string;
  usernameUtilizador: string;
  bilheteUtilizador: string;
  fotoUtilizador: string;
  passwordUtilizador: string;
};

export function UpdateUser() {
  const navigate = useNavigate();
  const [alertMessage, setAlertMessage] = useState<string | null>(null);
  const [alertVariant, setAlertVariant] = useState<"success" | "danger">(
    "success"
  );

  const handleHomeClick = (event: { preventDefault: () => void }) => {
    event.preventDefault();
    navigate("/logged");
  };

  const handleEditedClick = async (event: { preventDefault: () => void }) => {
    event.preventDefault();

    try {
      const response = await fetch("https://localhost:7209/UpdateUser", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(user),
      });
      if (response.ok) {
        setAlertMessage(
          "Edição realizada com sucesso! Será redirecionado para a página principal."
        );
        setAlertVariant("success");
        setTimeout(() => {
          navigate("/logged");
        }, 2500);
      } else {
        setAlertMessage("Falha na edição. Por favor, tente novamente.");
        setAlertVariant("danger");
      }
    } catch (error) {
      setAlertMessage("Erro ao tentar editar. Por favor, tente novamente.");
      setAlertVariant("danger");
    }
  };

  const [user, setUser] = useState<UserProps | null>(null);

  useEffect(() => {
    const fetchUser = async () => {
      const usernameStorage = localStorage.getItem("usernameUtilizador");
      if (!usernameStorage) {
        console.error("Username não foi encontrado.");
        return;
      }

      try {
        const response = await fetch(
          `https://localhost:7209/GetUserByUsername?username=${usernameStorage}`
        );
        if (response.ok) {
          const data: UserProps = await response.json();
          setUser(data);
        } else {
          console.error("Failed to fetch user");
        }
      } catch (error) {
        console.error("Error fetching user:", error);
      }
    };

    fetchUser();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    // const numero = typeof value === 'string' ? parseInt(value, 10) : value;

    setUser((prevUser) => (prevUser ? { ...prevUser, [name]: value } : null));
  };

  return (
    <div className="container-main">
      <div className="container-image"></div>

      <div className="container-form">
        <div className="d-flex justify-content-center">
          <img
            className="image-icon rounded-circle"
            src={`https://localhost:7209/${user?.fotoUtilizador}`}
            alt="UserFoto"
          />
        </div>
        <form action="" className="d-flex justify-content-center formulario">
          <div className="d-flex flex-row">
            <div className="input-container1">
              <input
                type="text"
                name="nomeUtilizador"
                placeholder=""
                className="m-1"
                value={user?.nomeUtilizador || ""}
                onChange={handleChange}
              />
              <input
                type="text"
                name="emailUtilizador"
                placeholder="Email"
                className="m-1"
                value={user?.emailUtilizador || ""}
                onChange={handleChange}
              />
              <input
                type="number"
                name="telemovelUtilizador"
                placeholder="Telemóvel"
                className="m-1"
                value={user?.telemovelUtilizador || ""}
                onChange={handleChange}
              />
              <input
                type="text"
                name="bilheteUtilizador"
                placeholder="Telemóvel"
                className="m-1"
                value={user?.bilheteUtilizador || ""}
                onChange={handleChange}
              />
            </div>

            <div className="input-container2">
              <input
                type="text"
                name="usernameUtilizador"
                placeholder="Username"
                className="m-1"
                value={user?.usernameUtilizador || ""}
                onChange={handleChange}
              />

              <Form.Control type="file" className="input" />

              <input
                type="password"
                name="passwordUtilizador"
                placeholder="Password"
                className="m-1"
                value={user?.passwordUtilizador || ""}
                onChange={handleChange}
              />
            </div>
          </div>

          <div className="d-flex justify-content-center">
            <div className="m-1">
              <Button
                variant="danger"
                onClick={handleHomeClick}
                className="botao"
              >
                Voltar
              </Button>
            </div>

            <div className="m-1">
              <Button
                variant="success"
                onClick={handleEditedClick}
                className="botao"
              >
                Confirmar
              </Button>
            </div>
          </div>
        </form>
        {alertMessage && (
          <Alert variant={alertVariant} className="mt-3">
            {alertMessage}
          </Alert>
        )}
      </div>
    </div>
  );
}
