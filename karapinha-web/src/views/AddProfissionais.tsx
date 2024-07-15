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

// preparando as listas que virão de cada requisição GET
type profissionaisProps = {
  idProfissional: number;
  nomeProfissional: string;
  emailProfissional: string;
  nomeCategoria: string;
  fotoProfissional: string;
  telemovelProfissional: string;
  horarios: horariosProps[];
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
    horarios: [] as number[],
  });

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = event.target;

    if (event.target instanceof HTMLSelectElement && name === "horarios") {
      const selectedOptions = event.target.selectedOptions;
      const selectedValues = Array.from(selectedOptions, (option) =>
        Number(option.value)
      );
      setFormData((prevData) => ({
        ...prevData,
        horarios: selectedValues,
      }));
    } else if (name === "telemovelProfissional" && value.length > 9) {
      setAlertMessage("O telemóvel deve possuir 9 dígitos.");
      setAlertVariant("danger");
      setShowAlert(true);
      setTimeout(() => {
        setShowAlert(false);
      }, 1500);
    } else if (name === "bilheteProfissional" && value.length > 14) {
      setAlertMessage("O bilhete de identidade possui apenas 14 caracterés.");
      setAlertVariant("danger");
      setShowAlert(true);
      setTimeout(() => {
        setShowAlert(false);
      }, 1500);
    } else {
      setFormData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const handleFileChange = (event: any) => {
    setFormData((prevData) => ({
      ...prevData,
      fotoProfissional: event.target.files[0],
    }));
    console.log(formData.fotoProfissional);
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
      formData.horarios.length === 0
    ) {
      setAlertMessage("Por favor, preencha todos os campos.");
      setAlertVariant("danger");
      setShowAlert(true);
      return;
    }
    console.log(formData.fotoProfissional);
    const url = `https://localhost:7209/CreateProfissional`;

    try {
      const formDataToSend = new FormData();
      formDataToSend.append("NomeProfissional", formData.nomeProfissional);
      formDataToSend.append("FkCategoria", formData.fkCategoria);
      formDataToSend.append("EmailProfissional", formData.emailProfissional);
      formDataToSend.append(
        "BilheteProfissional",
        formData.bilheteProfissional
      );
      formDataToSend.append(
        "TelemovelProfissional",
        formData.telemovelProfissional
      );
      if (formData.fotoProfissional !== null) {
        formDataToSend.append("foto", formData.fotoProfissional);
      }
      formData.horarios.forEach((horario) => {
        formDataToSend.append("Horarios", horario.toString());
      });
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
          // Recarregar a página após a criação bem-sucedida
          window.location.reload();
        }, 2500);
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

  const fetchHorariosByProfissional = async (profissionalId: number) => {
    const url = `https://localhost:7209/GetAllSchedulesByProfissionalId?profissionalId=${profissionalId}`;
    const response = await fetch(url);
    if (response.ok) {
      return await response.json();
    } else {
      console.error("Falha ao buscar horários do profissional");
      return [];
    }
  };

  useEffect(() => {
    const fetchProfissionais = async () => {
      try {
        const response = await fetch(
          "https://localhost:7209/GetAllProfissionaisWithCategories"
        );
        if (response.ok) {
          const data = await response.json();

          // Para cada profissional, buscar os horários detalhados
          const profissionaisWithHorarios = await Promise.all(
            data.map(async (profissional: profissionaisProps) => {
              const horarios = await fetchHorariosByProfissional(
                profissional.idProfissional
              );
              return { ...profissional, horarios };
            })
          );

          setProfissionais(profissionaisWithHorarios);
          console.log(profissionaisWithHorarios);
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
        method: "PUT",
      });
      if (response.ok) {
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
      <div className="p-2 container-service-added bg-dark rounded-2">
        <h4 className="bg-dark text-center m-0 pt-2 rounded-top-2 text-white">
          Profissionais registados
        </h4>
        <div className="bg-dark">
          <Button
            route="#"
            imageSrc={plus}
            className="link-signup"
            onClick={handleShow}
          />
        </div>

        <Table striped bordered hover variant="dark">
          <thead>
            <tr>
              <th>Nome do Profissional</th>
              <th>Email</th>
              <th>Categoria</th>
              <th>Telemóvel</th>
              <th>Horários</th>
              <th>Acções</th>
            </tr>
          </thead>
          <tbody>
            {profissionais.map((profissional, index) => (
              <tr key={index}>
                <td>
                  <img
                    src={`https://localhost:7209/${profissional?.fotoProfissional}`}
                    alt="Foto do Profissional"
                    className="img-fluid rounded-circle"
                    style={{ width: "50px", height: "50px" }}
                  />
                   {" "}
                  {profissional.nomeProfissional}
                </td>
                <td>{profissional.emailProfissional}</td>
                <td>{profissional.nomeCategoria}</td>
                <td>{profissional.telemovelProfissional}</td>
                <td>
                  {profissional.horarios
                    .map((horario) => horario.descricao)
                    .join(", ")}
                </td>
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
                      name="foto"
                      onChange={handleFileChange}
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
                      value={formData.horarios.map((horario) =>
                        horario.toString()
                      )}
                      onChange={handleChange}
                      className="select"
                      multiple
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
