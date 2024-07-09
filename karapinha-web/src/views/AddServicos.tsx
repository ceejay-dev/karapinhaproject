import { useEffect, useState } from "react";
import {
  Alert,
  Button as BootstrapButton,
  Form,
  Modal,
  Table,
} from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";

type servicosProps = {
  idServico: number;
  nomeServico: string;
  preco: number;
  nomeCategoria: string;
  fkCategoria: number;
};

type categoriaProps = {
  idCategoria: number;
  nomeCategoria: string;
};

export function AddServicos() {
  // const navigate = useNavigate();
  const [showAlert, setShowAlert] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");
  const [alertVariant, setAlertVariant] = useState("success");
  const [show, setShow] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = (servico?: servicosProps) => {
    if (servico) {
      setFormData({
        idServico: servico.idServico,
        fkCategoria: servico.fkCategoria.toString(),
        nomeServico: servico.nomeServico,
        preco: servico.preco.toString(),
        nomeCategoria: servico.nomeCategoria,
      });
      setIsEditMode(true);
    } else {
      setFormData({
        idServico: 0,
        fkCategoria: "0",
        nomeServico: "",
        preco: "",
        nomeCategoria: "",
      });
      setIsEditMode(false);
    }
    setShow(true);
  };

  const [formData, setFormData] = useState({
    idServico: 0,
    fkCategoria: "0",
    nomeServico: "",
    preco: "",
    nomeCategoria: "",
  });

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = event.target;

    // Validar o campo de preço para não aceitar números negativos
    if (name === "preco" && parseFloat(value) < 0) {
      setTimeout(() => {
        setAlertMessage("O preço do serviço deve ser positivo.");
        setAlertVariant("danger");
        setShowAlert(true);
      }, 500);
      // setFormData(prevData => ({
      //   ...prevData,
      //   [name]: '',
      // }));
    } else {
      setFormData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const handleDelete = async (id: number) => {
    const url = `https://localhost:7209/DeleteTreatment?id=${id}`;
    try {
      const response = await fetch(url, {
        method: "DELETE",
      });
      if (response.ok) {
        setAlertMessage("Serviço apagado com sucesso!");
        setAlertVariant("success");
        setShowAlert(true);
        // Remover o serviço da lista
        setServicos((prevServicos) =>
          prevServicos.filter((service) => service.idServico !== id)
        );
      } else {
        const errorData = await response.json();
        console.error("Falha ao apagar serviço", errorData);
        setAlertMessage("Falha ao apagar o serviço.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } catch (error) {
      console.error("Erro ao apagar serviço:", error);
      setAlertMessage("Erro ao apagar o serviço.");
      setAlertVariant("danger");
      setShowAlert(true);
    }
  };

  const handleConfirmedClick = async (event: any) => {
    event.preventDefault();

    // Valide os dados antes de enviar
    if (
      !formData.nomeServico ||
      !formData.preco ||
      (formData.fkCategoria === "0" && !isEditMode)
    ) {
      setAlertMessage("Por favor, preencha todos os campos.");
      setAlertVariant("danger");
      setShowAlert(true);
      return;
    }

    const url = isEditMode
      ? `https://localhost:7209/UpdateTreatment?IdServico=${formData.idServico}&NomeServico=${formData.nomeServico}&Preco=${formData.preco}`
      : `https://localhost:7209/CreateTreatment?NomeServico=${formData.nomeServico}&Preco=${formData.preco}&FkCategoria=${formData.fkCategoria}`;

    try {
      const response = await fetch(url, {
        method: isEditMode ? "PUT" : "POST",
      });

      if (response.ok) {
        setAlertMessage(
          isEditMode
            ? "Serviço atualizado com sucesso!"
            : "Serviço criado com sucesso!"
        );
        setAlertVariant("success");
        setShowAlert(true);
        setTimeout(() => {
          setShow(false);
          window.location.reload();
        }, 2500);
      } else {
        const errorData = await response.json();
        console.error(
          isEditMode ? "Falha ao atualizar serviço" : "Falha ao criar serviço",
          errorData
        );
        setAlertMessage(
          isEditMode
            ? "Falha ao atualizar o serviço."
            : "Falha ao criar o serviço."
        );
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } catch (error) {
      console.error(
        isEditMode ? "Error updating service:" : "Error creating service:",
        error
      );
      setAlertMessage(
        isEditMode ? "Erro ao atualizar o serviço." : "Erro ao criar o serviço."
      );
      setAlertVariant("danger");
      setShowAlert(true);
    }
  };

  const handleEdit = async (id: number) => {
    try {
      const response = await fetch(
        `https://localhost:7209/GetTreatmentById?id=${id}`
      );
      if (response.ok) {
        const servico: servicosProps = await response.json();
        handleShow(servico);
      } else {
        console.error("Failed to fetch serviço");
      }
    } catch (error) {
      console.error("Error fetching serviço:", error);
    }
  };

  // Listando os serviços
  const [servicos, setServicos] = useState<servicosProps[]>([]);

  useEffect(() => {
    const fetchServicos = async () => {
      try {
        const response = await fetch(
          "https://localhost:7209/GetAllServicosByIdCategoria"
        );
        if (response.ok) {
          const data = await response.json();
          setServicos(data);
          console.log(data);
        } else {
          console.error("Failed to fetch serviços");
        }
      } catch (error) {
        console.error("Error fetching serviços:", error);
      }
    };
    fetchServicos();
  }, []);

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
          console.error("Failed to fetch categorias");
        }
      } catch (error) {
        console.error("Error fetcing categorias:", error);
      }
    };

    fetchCategorias();
  }, []);

  return (
    <main className="container-service">
      <div className="p-2 container-service-added">
        <h3 className="pt-2 text-center bg-dark m-0 rounded-top-2 text-white">
          Serviços registados
        </h3>
        <div className="bg-dark">
          <Button
            route="#"
            imageSrc={plus}
            className="link-signup pb-5"
            onClick={() => handleShow()}
          />
        </div>
        <Table striped bordered hover className="m-0" variant="dark">
          <thead>
            <tr>
              <th>Descrição </th>
              <th>Preço (kz)</th>
              <th>Categoria</th>
              <th>Acções</th>
            </tr>
          </thead>
          <tbody>
            {servicos.map((servico, index) => (
              <tr key={servico.idServico || index}>
                <td>{servico.nomeServico}</td>
                <td>{servico.preco} kz</td>
                <td>{servico.nomeCategoria}</td>
                <td>
                  <BootstrapButton
                    variant="danger"
                    className="m-2"
                    onClick={() => handleDelete(servico.idServico)}
                  >
                    Apagar
                  </BootstrapButton>
                  <BootstrapButton
                    variant="info"
                    onClick={() => handleEdit(servico.idServico)}
                  >
                    Alterar
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
          <Modal.Title>
            {isEditMode ? "Editar Serviço" : "Registo de Serviços"}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="modal-content-container">
            <div className="container-imagem"></div>
            <div className="container-form">
              <div className="d-flex justify-content-center">
                <img className="image-icon" src={logo} alt="Logo" />
              </div>
              <form
                onSubmit={handleConfirmedClick}
                className="d-flex justify-content-center formulario"
              >
                <div className="d-flex flex-row">
                  <div className="input-container1">
                    {isEditMode ? (
                      <Form.Control
                        value={formData.nomeCategoria}
                        disabled
                        className="form-control m-1"
                      />
                    ) : (
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
                    )}
                    <input
                      value={formData.preco}
                      onChange={handleChange}
                      name="preco"
                      type="number"
                      placeholder="Preço"
                      className="form-control m-1"
                    />
                  </div>

                  <div className="input-container2">
                    <input
                      value={formData.nomeServico}
                      onChange={handleChange}
                      name="nomeServico"
                      type="text"
                      placeholder="Descrição do serviço"
                      className="form-control m-1"
                    />
                  </div>
                </div>

                <div className="d-flex justify-content-center">
                  <div className="m-1">
                    <BootstrapButton
                      variant="danger"
                      onClick={handleClose}
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
