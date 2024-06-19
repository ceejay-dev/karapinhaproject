import { useEffect, useState } from "react";
import { Button as BootstrapButton, Modal, Alert } from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";

type categoriaProps = {
  IdCategoria: number;
  NomeCategoria: string;
};

export function AddCategorias() {
  const [show, setShow] = useState(false);
  const [categoriaDescricao, setCategoriaDescricao] = useState("");
  const [categorias, setCategorias] = useState<categoriaProps[]>([]);
  const [alertMessage, setAlertMessage] = useState("");
  const [alertVariant, setAlertVariant] = useState<"success" | "danger" | "">("");

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleCanceledClick = () => {
    setCategoriaDescricao("");
    handleClose();
  };

  const handleConfirmedClick = async () => {
    try {
      const response = await fetch("https://localhost:7209/AddCategory", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ NomeCategoria: categoriaDescricao }),
      });

      if (response.ok) {
        // Sucesso ao registrar categoria
        const newCategoria = await response.json();
        setCategorias([...categorias, newCategoria]);
        setAlertMessage("Categoria registrada com sucesso!");
        setAlertVariant("success");
        setCategoriaDescricao("");
        handleClose();
      } else {
        setAlertMessage("Falha ao registrar categoria.");
        setAlertVariant("danger");
      }
    } catch (error) {
      setAlertMessage("Erro ao registrar categoria.");
      setAlertVariant("danger");
    }
  };

  useEffect(() => {
    const fetchServicos = async () => {
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

    fetchServicos();
  }, []);

  return (
    <main className="container-service">
      <div className="bg-white p-2 container-service-added">
        <h3 className="text-center mt-1">Categorias</h3>
        <div className="m-2">
          <Button
            route="#"
            imageSrc={plus}
            className="link-signup"
            onClick={handleShow}
          />
        </div>

        {categorias.map((categoria) => (
          <div
            key={categoria.IdCategoria}
            className="pt-4 bg-white border border-3 border-black"
          >
            <div>
              <h2>Descrição: {categoria.NomeCategoria}</h2>
            </div>
          </div>
        ))}
      </div>

      <Modal
        show={show}
        onHide={handleClose}
        dialogClassName="custom-modal-width"
      >
        <Modal.Header closeButton>
          <Modal.Title>Registo de categorias</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="modal-content-container">
            <div className="container-imagem"></div>
            <div className="container-form">
              <div className="d-flex justify-content-center">
                <img className="image-icon" src={logo} alt="Logo" />
              </div>
              <form
                onSubmit={(e) => {
                  e.preventDefault();
                  handleConfirmedClick();
                }}
                className="d-flex justify-content-center formulario"
              >
                <div className="d-flex flex-row d-flex justify-content-center align-content-center">
                  <div className="">
                    <input
                      type="text"
                      placeholder="Descrição da categoria"
                      className="m-1"
                      value={categoriaDescricao}
                      onChange={(e) => setCategoriaDescricao(e.target.value)}
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
                      Registar
                    </BootstrapButton>
                  </div>
                </div>
              </form>
              {alertMessage && (
                <Alert
                  variant={alertVariant}
                  onClose={() => setAlertMessage("")}
                  dismissible
                  className="mt-3"
                >
                  {alertMessage}
                </Alert>
              )}
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </main>
  );
}
