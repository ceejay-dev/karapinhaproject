import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Alert, Button as BootstrapButton, Modal } from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";

type categoriaProps = {
  idCategoria: number;
  nomeCategoria: string;
};

export function AddCategorias() {
  const navigate = useNavigate();
  const [showAlert, setShowAlert] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");
  const [alertVariant, setAlertVariant] = useState("success");
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => {
    setFormData({
      idCategoria: 0,
      nomeCategoria: "",
    });
    setShow(true);
  };

  const [formData, setFormData] = useState({
    idCategoria: 0,
    nomeCategoria: "",
  });

  const handleChange = (event: any) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleConfirmedClick = async (event: any) => {
    event.preventDefault();

    // Valide os dados antes de enviar
    if (!formData.nomeCategoria) {
      setAlertMessage("Por favor, preencha o nome da categoria.");
      setAlertVariant("danger");
      setShowAlert(true);
      return;
    }

    const url = `https://localhost:7209/AddCategory`;
    const requestData = {
      nomeCategoria: formData.nomeCategoria,
    };

    try {
      const response = await fetch(url, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      });

      if (response.ok) {
        const categoryAdded = await response.json();
        setCategorias([...categorias, categoryAdded]);
        setAlertMessage("Categoria criada com sucesso!");
        setAlertVariant("success");
        setShowAlert(true);
        setTimeout(() => {
          setShow(false);
          navigate("/GestorHome");
        }, 3000);
      } else {
        const errorData = await response.json();
        console.error("Falha ao criar categoria", errorData);
        setAlertMessage("Falha ao criar a categoria.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } catch (error) {
      console.error("Error creating category:", error);
      setAlertMessage("Erro ao criar a categoria.");
      setAlertVariant("danger");
      setShowAlert(true);
    }
  };

  // Listando as categorias
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
        <h3 className="pt-3 text-center">Categorias</h3>
        <div className="p-4">
          <div className="mb-3">
            <Button
              route="#"
              imageSrc={plus}
              className="link-signup pb-5"
              onClick={handleShow}
            />
          </div>

          {categorias.map((categoria, index) => (
            <div
              key={categoria.idCategoria || index}
              className="bg-white border border-2 border-black"
            >
              <div>
                <h3>Categoria: {categoria.nomeCategoria}</h3>
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
          <Modal.Title>Registo de Categoria</Modal.Title>
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
                <div className="input-container">
                  <input
                    value={formData.nomeCategoria}
                    onChange={handleChange}
                    name="nomeCategoria"
                    type="text"
                    placeholder="Nome da categoria"
                    className="form-control m-1"
                  />
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
