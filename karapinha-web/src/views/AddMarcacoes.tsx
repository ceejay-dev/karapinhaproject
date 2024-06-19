import { useEffect, useState } from "react";
import { Button as BootstrapButton, Modal } from "react-bootstrap";
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

export function AddMarcacoes() {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleCanceledClick = () => {
    handleClose;
  };

  const handleConfirmedClick = () => {
    // Adicione a lógica de registro aqui
  };

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

        {servicos.map((servico) => (
          <div className="pt-4 bg-white border border-3 border-black">
            <div>
              <div>
                <h3>Descrição: {servico.nomeServico}</h3>
                <h5>Preço: {servico.preco} kz</h5>
                <h5>Categoria: {servico.nomeCategoria}</h5>
              </div>
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
          <Modal.Title>Solicitação de serviços</Modal.Title>
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
              >
                <div className="d-flex flex-row">
                  <div className="input-container1">
                    <input
                      type="text"
                      placeholder="Categoria"
                      className="m-1"
                    />
                    <input
                      type="text"
                      placeholder="Profissional"
                      className="m-1"
                    />
                  </div>
                  <div className="input-container2">
                    <input type="text" placeholder="Serviços" className="m-1" />
                    <input type="text" placeholder="Hora" className="m-1" />
                    <input type="text" placeholder="Data" className="m-1" />
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
                      onClick={handleConfirmedClick}
                      className=""
                    >
                      Confirmar
                    </BootstrapButton>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </main>
  );
}
