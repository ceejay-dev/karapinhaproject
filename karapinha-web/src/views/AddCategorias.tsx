import { useEffect, useState } from "react";
import {
  Alert,
  Button as BootstrapButton,
  Modal,
  Table,
} from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";
import { useNavigate } from "react-router-dom";

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

    // Criando uma categoria
    const url = `https://localhost:7209/CreateCategory?NomeCategoria=${formData.nomeCategoria}`;
    try {
      const response = await fetch(url, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ nomeCategoria: formData.nomeCategoria }),
      });

      if (response.ok) {
        const categoryAdded = await response.json();
        setCategorias([...categorias, categoryAdded]);
        setAlertMessage("Categoria criada com sucesso!");
        setAlertVariant("success");
        setShowAlert(true);
        setTimeout(() => {
          setShow(false);
          navigate("/categorias");
        }, 3000);
      } else {
        const errorData = await response.json();
        console.error("Falha ao criar categoria", errorData);
        setAlertMessage("Falha ao criar a categoria.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } catch (error) {
      console.error("Erro ao criar categoria:", error);
      setAlertMessage("Erro ao criar a categoria.");
      setAlertVariant("danger");
      setShowAlert(true);
    }
  };

  //Apagando uma categoria
  const handleDelete = async (idCategoria: number) => {
    const url = `https://localhost:7209/DeleteCategory?id=${idCategoria}`;
    try {
      const response = await fetch(url, {
        method: "DELETE",
      });

      if (response.ok) {
        setCategorias(categorias.filter(categoria => categoria.idCategoria !== idCategoria));
        alert("Categoria apagada com sucesso!");
        console.log("Categoria apagada com sucesso!");
        // setAlertMessage("Categoria apagada com sucesso!");
        // setAlertVariant("success");
        // setShowAlert(true);
      } else {
        const errorData = await response.json();
        console.error("Falha ao apagar categoria", errorData);
        // setAlertMessage("Falha ao apagar a categoria.");
        // setAlertVariant("danger");
        // setShowAlert(true);
      }
    } catch (error) {
      console.error("Erro ao apagar categoria:", error);
      // setAlertMessage("Erro ao apagar a categoria.");
      // setAlertVariant("danger");
      // setShowAlert(true);
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
    <main className="container-service category-container">
      <h4 className=" text-center bg-white m-0 rounded-top-2">
        Categorias registadas
      </h4>
      <div className="bg-white m-0">
        <div className="mb-3">
          <Button
            route="#"
            imageSrc={plus}
            className="link-signup pb-5"
            onClick={handleShow}
          />
        </div>

        <Table striped bordered hover>
          <thead>
            <tr>
              <th>Nome da categoria</th>
              <th>Acções</th>
            </tr>
          </thead>
          <tbody>
            {categorias.map((categoria, index) => (
              <tr key={`${categoria.idCategoria}-${index}`}>
                <td>{categoria.nomeCategoria}</td>
                <td>
                  <BootstrapButton 
                    variant="danger"
                    onClick={() => handleDelete(categoria.idCategoria)}
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
