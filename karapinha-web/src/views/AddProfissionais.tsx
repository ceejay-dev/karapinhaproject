import { useState } from "react";
import { Button as BootstrapButton, Modal } from "react-bootstrap";
import { Button } from "../components/Button";
import { logo, plus } from "../components/Images";
import "../styles/marcacao.css";

export function AddProfissionais() {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleCanceledClick = () => {
    // Adicione a lógica de cancelar aqui
  };

  const handleConfirmedClick = () => {
    // Adicione a lógica de confirmar aqui
  };

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
              >
                <div className="d-flex flex-row">
                  <div className="input-container1">
                    <input
                      type="text"
                      placeholder="Nome do profissional"
                      className="m-1"
                    />
                    <input type="text" placeholder="Email" className="m-1" />
                    <input type="text" placeholder="Foto" className="m-1" />
                  </div>

                  <div className="input-container2">
                    <input
                      type="text"
                      placeholder="Categoria"
                      className="m-1"
                    />
                    <input type="text" placeholder="Username" className="m-1" />
                    <input
                      type="text"
                      placeholder="Telemóvel"
                      className="m-1"
                    />
                    <input type="text" placeholder="Horários" className="m-1" />
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
