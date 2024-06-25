import { useEffect, useState } from "react";
import {
  Button as BootstrapButton,
  Modal,
  Alert,
  Table,
} from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";
import { useNavigate } from "react-router-dom";

type administrativesProps = {
  nomeUtilizador: string;
  emailUtilizador: string;
  bilheteUtilizador: string;
  usernameUtilizador: string;
  estado: string;
};

export function AddAdministrativos() {
  const [show, setShow] = useState(false);
  const navigate = useNavigate();
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleCanceledClick = () => {
    navigate("/adminHome");
  };

  // variavel de estado para adicionar o administrativo
  const [formData, setFormData] = useState({
    idUtilizador: "0",
    NomeUtilizador: "",
    EmailUtilizador: "",
    TelemovelUtilizador: "",
    FotoUtilizador: null,
    BilheteUtilizador: "",
    UsernameUtilizador: "",
    PasswordUtilizador: "",
    ConfirmarPassword: "",
  });

  //variavel de estado para mensagens de alerta
  const [alert, setAlert] = useState({
    show: false,
    message: "",
    variant: "",
  });

  const handleChange = (event: any) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleFileChange = (event: any) => {
    setFormData((prevData) => ({
      ...prevData,
      FotoUtilizador: event.target.files[0],
    }));
  };

  const handleRegisterClick = async (event: any) => {
    event.preventDefault();

    if (formData.PasswordUtilizador !== formData.ConfirmarPassword) {
      setAlert({
        show: true,
        message: "Password e Confirmar Password não são iguais.",
        variant: "danger",
      });
      return;
    }

    const form = new FormData();
    form.append("TipoPerfil", "administrativo");
    if (formData.FotoUtilizador !== null) {
      form.append("foto", formData.FotoUtilizador);
    }
    form.append("Estado", "inactivo");
    form.append("TelemovelUtilizador", formData.TelemovelUtilizador);
    form.append("BilheteUtilizador", formData.BilheteUtilizador);
    form.append("UsernameUtilizador", formData.UsernameUtilizador);
    form.append("NomeUtilizador", formData.NomeUtilizador);
    form.append("IdUtilizador", "");
    form.append("PasswordUtilizador", formData.PasswordUtilizador);
    form.append("EmailUtilizador", formData.EmailUtilizador);
    form.append("ConfirmarPassword", formData.ConfirmarPassword);

    try {
      const response = await fetch("https://localhost:7209/CreateUser", {
        method: "POST",
        body: form,
      });

      if (response.ok) {
        setAlert({
          show: true,
          message: "Registo realizado com sucesso.",
          variant: "success",
        });
        setTimeout(() => {
          navigate("/AdminHome");
        }, 3000);
      } else {
        setAlert({
          show: true,
          message: "Falha ao registar o utilizador.",
          variant: "danger",
        });
      }
    } catch (error) {
      console.error("Error:", error);
      setAlert({
        show: true,
        message: "Erro ao registar o utilizador.",
        variant: "danger",
      });
    }
  };

  // variavel de estado para vector de administrativos
  const [administratives, setAdministratives] = useState<
    administrativesProps[]
  >([]);

  useEffect(() => {
    const fetchAdministratives = async () => {
      try {
        const response = await fetch(
          "https://localhost:7209/GetAdministratives"
        );
        if (response.ok) {
          const data = await response.json();
          setAdministratives(data);
          console.log(data);
        } else {
          console.error("Failed to fetch administratives");
        }
      } catch (error) {
        console.error("Error fetching administratives", error);
      }
    };

    fetchAdministratives();
  }, []);

  return (
    <main className="container-service">
      <div className="p-2 container-service-added">
        <h4 className="bg-white text-center pt-2 rounded-top-2 m-0">Administrativos registados</h4>
        <div className="bg-white">
          <Button
            route="#"
            imageSrc={plus}
            className="link-signup"
            onClick={handleShow}
          />
        </div>

        <Table striped bordered hover>
          <thead>
            <tr>
              <th>Nome</th>
              <th>Email</th>
              <th>BI</th>
              <th>Estado</th>
            </tr>
          </thead>
          <tbody>
            {administratives.map((administrative, index) => (
              <tr key={index}>
                <td>{administrative.nomeUtilizador}</td>
                <td>{administrative.emailUtilizador}</td>
                <td>{administrative.bilheteUtilizador}</td>
                <td>{administrative.estado}</td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>

      <Modal
        show={show}
        onHide={handleClose}
        dialogClassName="custom-modal-width"
      >
        <Modal.Header closeButton>
          <Modal.Title>Registo de Administrativos</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="modal-content-container">
            <div className="container-imagem"></div>
            <div className="container-form">
              <div className="d-flex justify-content-center">
                <img className="image-icon" src={logo} alt="Logo" />
              </div>
              <form
                className="d-flex justify-content-center formulario"
                onSubmit={handleRegisterClick}
              >
                <div className="d-flex flex-row">
                  <div className="input-container1">
                    <input
                      value={formData.NomeUtilizador}
                      onChange={handleChange}
                      name="NomeUtilizador"
                      type="text"
                      placeholder="Nome"
                      className="m-1"
                    />
                    <input
                      value={formData.EmailUtilizador}
                      onChange={handleChange}
                      name="EmailUtilizador"
                      type="text"
                      placeholder="Email"
                      className="m-1"
                    />
                    <input
                      value={formData.TelemovelUtilizador}
                      onChange={handleChange}
                      name="TelemovelUtilizador"
                      type="text"
                      placeholder="Telemóvel"
                      className="m-1"
                    />
                    <input
                      onChange={handleFileChange}
                      name="foto"
                      type="file"
                      className="m-1"
                    />
                  </div>

                  <div className="input-container2">
                    <input
                      value={formData.BilheteUtilizador}
                      onChange={handleChange}
                      name="BilheteUtilizador"
                      type="text"
                      placeholder="BI"
                      className="m-1"
                    />
                    <input
                      value={formData.UsernameUtilizador}
                      onChange={handleChange}
                      name="UsernameUtilizador"
                      type="text"
                      placeholder="Username"
                      className="m-1"
                    />
                    <input
                      value={formData.PasswordUtilizador}
                      onChange={handleChange}
                      name="PasswordUtilizador"
                      type="password"
                      placeholder="Password"
                      className="m-1"
                    />
                    <input
                      value={formData.ConfirmarPassword}
                      onChange={handleChange}
                      name="ConfirmarPassword"
                      type="password"
                      placeholder="Confirmar password"
                      className="m-1"
                    />
                  </div>
                </div>

                <div className="d-flex justify-content-center">
                  <div className="m-1">
                    <BootstrapButton
                      variant="danger"
                      onClick={handleCanceledClick}
                      className=""
                    >
                      Cancelar
                    </BootstrapButton>
                  </div>
                  <div className="m-1">
                    <BootstrapButton
                      variant="success"
                      type="submit"
                      className=""
                    >
                      Confirmar registo
                    </BootstrapButton>
                  </div>
                </div>

                <div className="d-flex justify-content-center w-100">
                  {alert.show && (
                    <Alert variant={alert.variant} className="mt-3 w-100">
                      {alert.message}
                    </Alert>
                  )}
                </div>
              </form>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </main>
  );
}
