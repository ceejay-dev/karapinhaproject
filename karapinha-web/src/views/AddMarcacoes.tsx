import { useEffect, useState } from "react";
import {
  Button as BootstrapButton,
  Card,
  Col,
  Container,
  Modal,
  Row,
  Alert,
} from "react-bootstrap";
import { logo } from "../components/Images";
import "../styles/marcacao.css";
import { getAllData } from "../services/getData";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useNavigate } from "react-router-dom";
import jsPDF from "jspdf";

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
  horarios: { idHorario: number; descricao: string; estado: string }[];
};

type selectedServicoProps = {
  servico: servicosProps;
  profissionalId: number | null;
  horarioId: number | null;
};

export function AddMarcacoes() {
  const [show, setShow] = useState(false);
  const [selectedServicos, setSelectedServicos] = useState<
    selectedServicoProps[]
  >([]);
  const [servicos, setServicos] = useState<servicosProps[]>([]);
  const [profissionaisByServico, setProfissionaisByServico] = useState<{
    [key: number]: profissionaisProps[];
  }>({});
  const [horariosProfissional, setHorariosProfissional] = useState<
    { idHorario: number; descricao: string; estado: string }[]
  >([]);
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [cartCount, setCartCount] = useState(0);
  const [alertMessage, setAlertMessage] = useState<string | null>(null);
  const [alertVariant, setAlertVariant] = useState<string>("success");
  const [showAlert, setShowAlert] = useState<boolean>(false);
  const navigate = useNavigate();

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleCanceledClick = () => {
    setSelectedServicos([]);
    setCartCount(0);
    handleClose();
  };

  const generatePDF = (marcacao: any) => {
    const doc = new jsPDF();

    doc.setFontSize(12);
    let yPos = 15; // posição inicial vertical

    doc.text("FATURA DE COMPRA", 15, yPos);
    yPos += 15; // incrementa a posição vertical para a próxima linha

    // Informações da marcação
    doc.text(
      `Cliente: ${localStorage.getItem("usernameUtilizador")}`,
      15,
      yPos
    );
    yPos += 15;

    doc.text(
      `Data da Marcação: ${new Date(marcacao.dataMarcacao).toLocaleString()}`,
      15,
      yPos
    );
    yPos += 15;

    doc.text(`Preço Total: ${marcacao.precoMarcacao} kz`, 15, yPos);

    // Salvar ou exibir o PDF
    doc.save("Marcacao.pdf");
  };

  const handleConfirmedClick = async () => {
    const usernameStorage = localStorage.getItem("usernameUtilizador");
    if (usernameStorage !== null) {
      try {
        const idStorage = localStorage.getItem("idUtilizador");
        console.log(idStorage);

        const precoMarcacao = selectedServicos.reduce(
          (acc, { servico }) => acc + servico.preco,
          0
        );

        const servicosToSend = selectedServicos.map(
          ({ servico, profissionalId, horarioId }) => ({
            fkServico: servico.idServico,
            fkProfissional: profissionalId,
            fkHorario: horarioId,
          })
        );

        console.log("Serviços", servicosToSend);
        const dataMarcacao = selectedDate?.toISOString().split("T")[0];

        if (idStorage === null) {
          console.error("Não foi possível obter o ID do usuário.");
          return;
        }

        if (
          !dataMarcacao ||
          servicosToSend.some((s) => !s.fkProfissional || !s.fkHorario)
        ) {
          setAlertMessage("Por favor, preencha todos os campos obrigatórios.");
          setAlertVariant("warning");
          setShowAlert(true);
          return;
        }

        const formDataToSend = {
          precoMarcacao,
          fkUtilizador: parseInt(idStorage),
          dataMarcacao,
          servicos: servicosToSend,
        };

        console.log(formDataToSend);

        const url = `https://localhost:7209/CreateBooking`;

        const response = await fetch(url, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(formDataToSend),
        });

        if (response.ok) {
          const data = await response.json();
          const { precoMarcacao, servicos, profissional, horario } = data;
          const marcacao = {
            precoMarcacao,
            servicos,
            profissional,
            horario,
            dataMarcacao,
          };

          setAlertMessage("Marcação criada com sucesso!");
          setAlertVariant("success");
          setShowAlert(true);
          setTimeout(() => {
            generatePDF(marcacao);
            setShow(false);
            navigate("/mymarcacoes");
          }, 3000);
        } else {
          const errorText = await response.text();
          console.log("Falha ao criar marcação", errorText);
          setAlertMessage("Falha ao criar a marcação.");
          setAlertVariant("danger");
          setShowAlert(true);
        }
      } catch (error) {
        console.log("Erro ao criar marcação:", error);
        setAlertMessage("Erro ao criar a marcação.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } else {
      navigate("/login");
    }
  };

  const handleAddedClick = async (servico: servicosProps) => {
    if (
      !selectedServicos.some((s) => s.servico.idServico === servico.idServico)
    ) {
      setSelectedServicos([
        ...selectedServicos,
        { servico, profissionalId: null, horarioId: null },
      ]);
      await fetchProfissionaisByServico(servico.fkCategoria, servico.idServico);
      setCartCount(cartCount + 1);
    }
  };

  const handleRemoveClick = (idServico: number) => {
    const updatedServicos = selectedServicos.filter(
      ({ servico }) => servico.idServico !== idServico
    );
    setSelectedServicos(updatedServicos);
    setCartCount(cartCount - 1);
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

  const handleProfissionalChange = async (
    servicoId: number,
    profissionalId: number
  ) => {
    const updatedServicos = selectedServicos.map((item) =>
      item.servico.idServico === servicoId ? { ...item, profissionalId } : item
    );
    setSelectedServicos(updatedServicos);

    if (profissionalId) {
      await fetchHorariosByProfissional(profissionalId);
    } else {
      setHorariosProfissional([]);
    }
  };

  const handleHorarioChange = (servicoId: number, horarioId: number) => {
    const updatedServicos = selectedServicos.map((item) =>
      item.servico.idServico === servicoId ? { ...item, horarioId } : item
    );
    setSelectedServicos(updatedServicos);
  };

  const fetchHorariosByProfissional = async (profissionalId: number) => {
    var url = `https://localhost:7209/GetAllSchedulesByProfissionalId?profissionalId=${profissionalId}`;
    const horarios = await getAllData({ url });
    setHorariosProfissional(horarios);
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
              <span className="visually-hidden">Numero de servicos</span>
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
                      Adicionar ao carrinho
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
                {selectedServicos.map(
                  ({ servico, profissionalId, horarioId }) => (
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
                            onChange={(e) =>
                              handleProfissionalChange(
                                servico.idServico,
                                parseInt(e.target.value)
                              )
                            }
                            value={profissionalId || ""}
                          >
                            <option value="">Selecione o Profissional</option>
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

                          <select
                            className="form-select mt-3"
                            aria-label="Selecione o Horário"
                            onChange={(e) =>
                              handleHorarioChange(
                                servico.idServico,
                                parseInt(e.target.value)
                              )
                            }
                            value={horarioId || ""}
                          >
                            <option value="">Selecione o Horário</option>
                            {horariosProfissional.map((horario) => (
                              <option
                                key={horario.idHorario}
                                value={horario.idHorario}
                              >
                                {horario.descricao}
                              </option>
                            ))}
                          </select>
                        </Card.Body>
                      </Card>
                    </div>
                  )
                )}
                <div className="d-flex flex-row">
                  <div className="input-container1">
                    <DatePicker
                      selected={selectedDate}
                      onChange={(date: Date | null) => setSelectedDate(date)}
                      dateFormat="dd/MM/yyyy"
                      minDate={new Date()}
                      className="form-control me-5"
                      placeholderText="Data da Marcação"
                    />
                  </div>
                </div>
                <div className="d-flex justify-content-center mt-3">
                  <div className="m-1">
                    <BootstrapButton
                      variant="success"
                      onClick={handleConfirmedClick}
                      className=""
                    >
                      Finalizar
                    </BootstrapButton>
                  </div>
                  <div className="m-1">
                    <BootstrapButton
                      variant="danger"
                      onClick={handleCanceledClick}
                      className=""
                    >
                      Cancelar
                    </BootstrapButton>
                  </div>
                </div>
                {showAlert && (
                  <Alert
                    variant={alertVariant}
                    onClose={() => setShowAlert(false)}
                    dismissible
                    className="mt-3"
                  >
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
