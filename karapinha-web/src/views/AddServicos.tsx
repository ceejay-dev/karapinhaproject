import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Alert, Button as BootstrapButton, Form, Modal } from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";

type servicosProps = {
  idServico: number; 
  nomeServico: string;
  preco: number;
  nomeCategoria: string;
};

type categoriaProps = {
  idCategoria: number;
  nomeCategoria: string;
};

export function AddServicos() {
  const navigate = useNavigate();
  const [showAlert, setShowAlert] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");
  const [alertVariant, setAlertVariant] = useState("success");
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [formData, setFormData] = useState({
    fkCategoria: "0",
    nomeServico: "",
    preco: "",
  });

  const handleChange = (event: any) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleDelete = async (id: number) => {
    const url = `https://localhost:7209/DeleteTreatment?id=${id}`;
    try {
      const response = await fetch(url, {
        method: "DELETE",
      });
      if (response.ok) {
        setAlertMessage("Servico apagado com sucesso!");
        setAlertVariant("success");
        setShowAlert(true);
        // Remover o servico da lista
        setServicos((prevProfissionais) =>
          prevProfissionais.filter((service) => service.idServico !== id)
        );
      } else {
        const errorData = await response.json();
        console.error("Falha ao apagar servico", errorData);
        setAlertMessage("Falha ao apagar o servico.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } catch (error) {
      console.error("Erro ao apagar servico:", error);
      setAlertMessage("Erro ao apagar o servico.");
      setAlertVariant("danger");
      setShowAlert(true);
    }
  };

  const handleConfirmedClick = async (event: any) => {
    event.preventDefault();

    // Valide os dados antes de enviar
    if (
      formData.fkCategoria === "0" ||
      !formData.nomeServico ||
      !formData.preco
    ) {
      setAlertMessage("Por favor, preencha todos os campos.");
      setAlertVariant("danger");
      setShowAlert(true);
      return;
    }

    const url = `https://localhost:7209/CreateTreatment?NomeServico=${formData.nomeServico}&Preco=${formData.preco}&FkCategoria=${formData.fkCategoria}`;

    try {
      const response = await fetch(url, {
        method: "POST",
      });

      if (response.ok) {
        setAlertMessage("Serviço criado com sucesso!");
        setAlertVariant("success");
        setShowAlert(true);
        setTimeout(() => {
          setShow(false);
          navigate("/GestorHome");
        }, 3000);
      } else {
        const errorData = await response.json();
        console.error("Falha ao criar serviço", errorData);
        setAlertMessage("Falha ao criar o serviço.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } catch (error) {
      console.error("Error creating service:", error);
      setAlertMessage("Erro ao criar o serviço.");
      setAlertVariant("danger");
      setShowAlert(true);
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
          console.error("Failed to fetch servicos");
        }
      } catch (error) {
        console.error("Error fetching servicos:", error);
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
        console.error("Error fetching categorias:", error);
      }
    };

    fetchCategorias();
  }, []);

  return (
    <main className="container-service">
      <div className="bg-white p-2 container-service-added">
        <h3 className="pt-3 text-center">Serviços</h3>
        <div className="p-4">
          <div className="mb-3">
            <Button
              route="#"
              imageSrc={plus}
              className="link-signup pb-5"
              onClick={handleShow}
            />
          </div>

          {servicos.map((servico, index) => (
            <div
              key={servico.idServico || index}
              className="bg-white border border-2 border-black"
            >
              <div>
                <h3>Id: {servico.idServico}</h3>
                <h3>Descrição: {servico.nomeServico}</h3>
                <h5>Preço: {servico.preco} kz</h5>
                <h5>Categoria: {servico.nomeCategoria}</h5>
              </div>

              <div className="m-1">
                <BootstrapButton
                  variant="danger"
                  className="m-2"
                  onClick={() => handleDelete(servico.idServico)}
                >
                  Apagar
                </BootstrapButton>
                <BootstrapButton variant="info">Alterar</BootstrapButton>
              </div>
            </div>
          ))}
        </div>
      </div>

      <Modal
        show={show}
        onHide={handleClose}
        dialogClassName="custom-modal-width"
      >
        <Modal.Header closeButton>
          <Modal.Title>Registo de Serviços</Modal.Title>
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
                      value={formData.preco}
                      onChange={handleChange}
                      name="preco"
                      type="number"
                      placeholder="Preço"
                      className="m-1"
                    />
                  </div>

                  <div className="input-container2">
                    <input
                      value={formData.nomeServico}
                      onChange={handleChange}
                      name="nomeServico"
                      type="text"
                      placeholder="Descrição do serviço"
                      className="m-1"
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
