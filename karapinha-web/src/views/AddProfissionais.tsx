import { useEffect, useState } from "react";
import { Button as BootstrapButton, Modal, Form, Alert } from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";
import { useNavigate } from "react-router-dom";

// preparando as listas que virão de cada requisição GET
type profissionaisProps = {
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
    horarios: [] as number[],
  });

  const handleChange = (event: any) => {
    const { name, value, type, files } = event.target;
    if (type === "file") {
      setFormData((prevData) => ({
        ...prevData,
        [name]: files[0],
      }));
    } else {
      setFormData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const handleConfirmedClick = async (event: any) => {
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

    const url = `https://localhost:7209/CreateProfissional`;

    try {
      const formDataToSend = new FormData();
      formDataToSend.append("NomeProfissional", formData.nomeProfissional);
      formDataToSend.append("FkCategoria", formData.fkCategoria);
      formDataToSend.append("EmailProfissional", formData.emailProfissional);
      formDataToSend.append("FotoProfissional", formData.fotoProfissional as File);
      formDataToSend.append("BilheteProfissional", formData.bilheteProfissional);
      formDataToSend.append("TelemovelProfissional", formData.telemovelProfissional);
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
        const response = await fetch("https://localhost:7209/GetProfissinalsByIdCategoria");
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

  return (
    <main className="container-service">
      <div className="bg-white p-2 container-service-added">
        <div>
          <Button
            route="#"
            imageSrc={plus}
            className="link-signup"
            onClick={handleShow}
          />
        </div>

        {profissionais.map((profissional, index) => (
          <div key={index} className="bg-white border border-2 border-black">
            <div>
              <h3>Nome do profissional: {profissional.nomeProfissional}</h3>
              <h5>Email: {profissional.emailProfissional}</h5>
              <h5>Categoria: {profissional.nomeCategoria}</h5>
              <h5>Telemóvel: {profissional.telemovelProfissional}</h5>
            </div>

            <div className="m-1">
              <BootstrapButton variant="danger" className="me-2">
                Apagar
              </BootstrapButton>
            </div>
          </div>
        ))}
      </div>

      <Modal show={show} onHide={handleClose} dialogClassName="custom-modal-width">
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
              <form action="" className="d-flex justify-content-center formulario">
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
                        <option key={categoria.idCategoria} value={categoria.idCategoria}>
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
                      value={formData.horarios.map(String)}
                      onChange={(event) => {
                        const value = Array.from(
                          event.target.selectedOptions,
                          (option) => parseInt(option.value)
                        );
                        setFormData((prevData) => ({
                          ...prevData,
                          horarios: value,
                        }));
                      }}
                     // multiple
                      className="select"
                    >
                      <option value="0">Selecione o horario:</option>
                      {horarios.map((horario) => (
                        <option key={horario.idHorario} value={horario.idHorario}>
                          {horario.descricao}
                        </option>
                      ))}
                    </Form.Select>
                  </div>
                </div>
                <div className="d-flex justify-content-center">
                  <div className="m-1">
                    <BootstrapButton variant="danger" onClick={handleClose} className="">
                      Cancelar
                    </BootstrapButton>
                  </div>
                  <div className="m-1">
                    <BootstrapButton variant="success" onClick={handleConfirmedClick} className="">
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
