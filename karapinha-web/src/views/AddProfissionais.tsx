import { useEffect, useState } from "react";
import {
  Button as BootstrapButton,
  Modal,
  Form,
  Alert,
  Table,
} from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";
import { useNavigate } from "react-router-dom";

// preparando as listas que virão de cada requisição GET
type profissionaisProps = {
  idProfissional: number;
  nomeProfissional: string;
  emailProfissional: string;
  nomeCategoria: string;
  telemovelProfissional: string;
};

type categoriaProps = {
  idCategoria: number;
  nomeCategoria: string;
};

type horariosProps = {
  idHorario: number;
  descricao: string;
};

export function AddProfissionais() {
  const navigate = useNavigate();
  const [showAlert, setShowAlert] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");
  const [alertVariant, setAlertVariant] = useState("success");
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [formData, setFormData] = useState({
    idProfissional: "0",
    nomeProfissional: "",
    fkCategoria: "0",
    emailProfissional: "",
    fotoProfissional: null as File | null,
    bilheteProfissional: "",
    telemovelProfissional: "",
    horarios: "0",
  });

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = event.target;

    if (type === "file") {
      if ((event.target as HTMLInputElement).files) {
        setFormData((prevData) => ({
          ...prevData,
          [name]: (event.target as HTMLInputElement).files![0],
        }));
      }
    } else {
      setFormData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const handleConfirmedClick = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    // Validando os dados antes de enviar
    if (
      formData.fkCategoria === "0" ||
      !formData.bilheteProfissional ||
      !formData.emailProfissional ||
      !formData.fotoProfissional ||
      !formData.nomeProfissional ||
      !formData.telemovelProfissional ||
      formData.horarios === "0"
    ) {
      setAlertMessage("Por favor, preencha todos os campos.");
      setAlertVariant("danger");
      setShowAlert(true);
      return;
    }
    console.log(formData);
    const url = `https://localhost:7209/CreateProfissional`;

    try {
      const formDataToSend = new FormData();
      formDataToSend.append("NomeProfissional", formData.nomeProfissional);
      formDataToSend.append("FkCategoria", formData.fkCategoria);
      formDataToSend.append("EmailProfissional", formData.emailProfissional);
      formDataToSend.append(
        "FotoProfissional",
        formData.fotoProfissional as File
      );
      formDataToSend.append(
        "BilheteProfissional",
        formData.bilheteProfissional
      );
      formDataToSend.append(
        "TelemovelProfissional",
        formData.telemovelProfissional
      );
      formDataToSend.append("Horarios", JSON.stringify(formData.horarios));

      const response = await fetch(url, {
        method: "POST",
        body: formDataToSend,
      });

      if (response.ok) {
        setAlertMessage("Profissional criado com sucesso!");
        setAlertVariant("success");
        setShowAlert(true);
        setTimeout(() => {
          setShow(false);
          navigate("/GestorHome");
        }, 3000);
      } else {
        const errorData = await response.json();
        console.error("Falha ao criar profissional", errorData);
        setAlertMessage("Falha ao criar o profissional.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } catch (error) {
      console.error("Erro ao criar profissional:", error);
      setAlertMessage("Erro ao criar o profissional.");
      setAlertVariant("danger");
      setShowAlert(true);
    }
  };

  const [categorias, setCategorias] = useState<categoriaProps[]>([]);

  useEffect(() => {
    const fetchCategorias = async () => {
      try {
        const response = await fetch("https://localhost:7209/GetAllCategories");
        if (response.ok) {
          const data = await response.json();
          setCategorias(data);
          console.log(data);
        } else {
          console.error("Falha ao buscar categorias");
        }
      } catch (error) {
        console.error("Erro ao buscar categorias:", error);
      }
    };

    fetchCategorias();
  }, []);

  const [profissionais, setProfissionais] = useState<profissionaisProps[]>([]);

  useEffect(() => {
    const fetchProfissionais = async () => {
      try {
        const response = await fetch(
          "https://localhost:7209/GetProfissinalsByIdCategoria"
        );
        if (response.ok) {
          const data = await response.json();
          setProfissionais(data);
          console.log(data);
        } else {
          console.error("Falha ao buscar profissionais");
        }
      } catch (error) {
        console.error("Erro ao buscar profissionais:", error);
      }
    };

    fetchProfissionais();
  }, []);

  const [horarios, setHorarios] = useState<horariosProps[]>([]);

  useEffect(() => {
    const fetchHorarios = async () => {
      try {
        const response = await fetch("https://localhost:7209/GetAllSchedules");
        if (response.ok) {
          const data = await response.json();
          setHorarios(data);
          console.log(data);
        } else {
          console.error("Falha ao buscar horários");
        }
      } catch (error) {
        console.error("Erro ao buscar horários:", error);
      }
    };

    fetchHorarios();
  }, []);

  const handleDelete = async (id: number) => {
    const url = `https://localhost:7209/DeleteProfissional?id=${id}`;
    try {
      const response = await fetch(url, {
        method: "DELETE",
      });
      if (response.ok) {
        //setAlertMessage("Profissional apagado com sucesso!");
        //setAlertVariant("success");
        //setShowAlert(true);
        // Remover o profissional da lista
        setProfissionais((prevProfissionais) =>
          prevProfissionais.filter((prof) => prof.idProfissional !== id)
        );
      } else {
        const errorData = await response.json();
        console.error("Falha ao apagar profissional", errorData);
        setAlertMessage("Falha ao apagar o profissional.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } catch (error) {
      console.error("Erro ao apagar profissional:", error);
      setAlertMessage("Erro ao apagar o profissional.");
      setAlertVariant("danger");
      setShowAlert(true);
    }
  };

  return (
    <main className="container-service">
      <div className="p-2 container-service-added">
        <h4 className="bg-white text-center m-0 pt-2 rounded-top-2">Profissionais registados</h4>
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
              <th>Nome do Profissional</th>
              <th>Email</th>
              <th>Categoria</th>
              <th>Telemóvel</th>
              <th>Acções</th>
            </tr>
          </thead>
          <tbody>
            {profissionais.map((profissional, index) => (
              <tr key={index}>
                <td>{profissional.nomeProfissional}</td>
                <td>{profissional.emailProfissional}</td>
                <td>{profissional.nomeCategoria}</td>
                <td>{profissional.telemovelProfissional}</td>
                <td>
                  <BootstrapButton
                    variant="danger"
                    className="me-2"
                    onClick={() => handleDelete(profissional.idProfissional)}
                  >
                    Apagar
                  </BootstrapButton>
                </td>
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
          <Modal.Title>Registo de Profissionais</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="modal-content-container">
            <div className="container-imagem"></div>
            <div className="container-form">
              <div className="d-flex justify-content-center">
                <img className="image-icon" src={logo} alt="Logo" />
              </div>
              <form
                action=""
                className="d-flex justify-content-center formulario"
                onSubmit={handleConfirmedClick}
              >
                <div className="d-flex flex-row">
                  <div className="input-container1">
                    <input
                      name="nomeProfissional"
                      value={formData.nomeProfissional}
                      onChange={handleChange}
                      type="text"
                      placeholder="Nome do profissional"
                      className="m-1"
                    />

                    <input
                      name="emailProfissional"
                      value={formData.emailProfissional}
                      onChange={handleChange}
                      type="text"
                      placeholder="Email"
                      className="m-1"
                    />

                    <input
                      name="bilheteProfissional"
                      value={formData.bilheteProfissional}
                      onChange={handleChange}
                      type="text"
                      placeholder="BI"
                      className="m-1"
                    />

                    <input
                      name="fotoProfissional"
                      onChange={handleChange}
                      type="file"
                      className="m-1"
                      required
                    />
                  </div>

                  <div className="input-container2">
                    <Form.Select
                      name="fkCategoria"
                      value={formData.fkCategoria}
                      onChange={handleChange}
                      className="select"
                    >
                      <option value="0">Selecione a categoria:</option>
                      {categorias.map((categoria) => (
                        <option
                          key={categoria.idCategoria}
                          value={categoria.idCategoria}
                        >
                          {categoria.nomeCategoria}
                        </option>
                      ))}
                    </Form.Select>
                    <input
                      name="telemovelProfissional"
                      value={formData.telemovelProfissional}
                      onChange={handleChange}
                      type="text"
                      placeholder="Telemóvel"
                      className="m-1"
                    />
                    <Form.Select
                      name="horarios"
                      value={formData.horarios}
                      onChange={handleChange}
                      className="select"
                    >
                      
                      <option value="0">Selecione o horário:</option>
                      {horarios.map((horario) => (
                        <option
                          key={horario.idHorario}
                          value={horario.idHorario}
                        >
                          {horario.descricao}
                        </option>
                      ))}
                    </Form.Select>
                  </div>
                </div>

                <div className="d-flex justify-content-center">
                  <div className="m-1">
                    <BootstrapButton variant="danger" onClick={handleClose}>
                      Cancelar
                    </BootstrapButton>
                  </div>
                  <div className="m-1">
                    <BootstrapButton variant="success" type="submit">
                      Confirmar
                    </BootstrapButton>
                  </div>
                </div>
                {showAlert && (
                  <Alert variant={alertVariant} className="mt-3">
                    {alertMessage}
                  </Alert>
                )}
              </form>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </main>
  );
}
