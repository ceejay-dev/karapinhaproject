import { useEffect, useState } from "react";
import {
  Button as BootstrapButton,
  Card,
  Col,
  Container,
  Modal,
  Row,
} from "react-bootstrap";
import { logo } from "../components/Images";
import "../styles/marcacao.css";
import { getAllData } from "../services/getData";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "react-time-picker/dist/TimePicker.css";

type servicosProps = {
  idServico: number;
  nomeServico: string;
  preco: number;
  nomeCategoria: string;
  fkCategoria: number;
};

type profissionaisProps = {
  idProfissional: number;
  nomeProfissional: string;
  fkCategoria: number;
  emailProfissional: string;
  fotoProfissional: string;
  bilheteProfissional: string;
  telemovelProfissional: string;
};

export function AddMarcacoes() {
  const [show, setShow] = useState(false);
  const [selectedServicos, setSelectedServicos] = useState<servicosProps[]>([]);
  
  const [servicos, setServicos] = useState<servicosProps[]>([]);
  const [profissionaisByServico, setProfissionaisByServico] = useState<{
    [key: number]: profissionaisProps[];
  }>({});
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [cartCount, setCartCount] = useState(0); // Estado para armazenar a contagem do carrinho

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleCanceledClick = () => {
    handleClose();
  };  

  const handleConfirmedClick = () => {
    // Adicione a lógica de registro aqui
  };

  const handleAddedClick = async (servico: servicosProps) => {
    if (!selectedServicos.some((s) => s.idServico === servico.idServico)) {
      setSelectedServicos([...selectedServicos, servico]);
      await fetchProfissionaisByServico(servico.fkCategoria, servico.idServico);
      setCartCount(cartCount + 1); // Atualiza a contagem do carrinho
    }
  };

  const handleRemoveClick = (idServico: number) => {
    const updatedServicos = selectedServicos.filter(
      (servico) => servico.idServico !== idServico
    );
    setSelectedServicos(updatedServicos);
    setCartCount(cartCount - 1); // Decrementa a contagem do carrinho
  };

  const fetchProfissionaisByServico = async (
    idCategoria: number,
    idServico: number
  ) => {
    var url = `https://localhost:7209/GetAllProfissinalsByIdCategoria?id=${idCategoria}`;
    const getProfissionais = await getAllData({ url });
    setProfissionaisByServico((prevState) => ({
      ...prevState,
      [idServico]: getProfissionais,
    }));
  };

  useEffect(() => {
    async function waitServicos() {
      var url = `https://localhost:7209/GetAllServicosByIdCategoria`;
      const getServicos = await getAllData({ url });
      setServicos(getServicos);
    }
    waitServicos();
  }, []);

  return (
    <main className="container-service">
      <div className="bg-white p-2 container-service-added">
        <div className="pb-2">
          <BootstrapButton
            type="button"
            className="pe-2 btn btn-dark position-relative"
            onClick={handleShow}
          >
            Carrinho
            <span className="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
              {cartCount}
              <span className="visually-hidden">unread messages</span>
            </span>
          </BootstrapButton>
        </div>

        <Container>
          <Row>
            {servicos.map((servico) => (
              <Col md={3} key={servico.idServico} className="mb-3">
                <Card style={{ width: "18rem" }}>
                  <Card.Body>
                    <Card.Title>{servico.nomeServico}</Card.Title>
                    <Card.Text>Preço: {servico.preco} kz</Card.Text>
                    <BootstrapButton
                      variant="info"
                      onClick={() => handleAddedClick(servico)}
                    >
                      Adicionar
                    </BootstrapButton>
                  </Card.Body>
                </Card>
              </Col>
            ))}
          </Row>
        </Container>
      </div>

      <Modal
        show={show}
        onHide={handleClose}
        dialogClassName="custom-modal-width"
      >
        <Modal.Header closeButton>
          <Modal.Title>Finalização da Marcação</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{ maxHeight: "70vh", overflowY: "auto" }}>
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
                {selectedServicos.map((servico) => (
                  <div key={servico.idServico} className="mb-3">
                    <Card>
                      <div className="d-flex justify-content-end m-3">
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="16"
                          height="16"
                          fill="currentColor"
                          className="bi bi-x-lg"
                          viewBox="0 0 16 16"
                          onClick={() => handleRemoveClick(servico.idServico)}
                        >
                          <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z" />
                        </svg>
                      </div>
                      <Card.Body>
                        <Card.Title>{servico.nomeServico}</Card.Title>
                        <Card.Text>Preço: {servico.preco} kz</Card.Text>
                        <select
                          className="form-select"
                          aria-label="Selecione o Profissional"
                        >
                          {profissionaisByServico[servico.idServico]?.map(
                            (profissional) => (
                              <option
                                key={profissional.idProfissional}
                                value={profissional.idProfissional}
                              >
                                {profissional.nomeProfissional}
                              </option>
                            )
                          )}
                        </select>
                      </Card.Body>
                    </Card>
                  </div>
                ))}
                <div className="d-flex flex-row">
                  <div className="input-container1">
                    <DatePicker
                      selected={selectedDate}
                      onChange={(date: Date | null) => setSelectedDate(date)}
                      dateFormat="dd/MM/yyyy"
                      className="form-control me-5"
                      placeholderText="Data"
                    />
                  </div>
                  <div className="input-container2"></div>
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
                      Finalizar
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
