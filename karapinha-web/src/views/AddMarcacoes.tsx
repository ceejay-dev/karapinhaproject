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
import "react-time-picker/dist/TimePicker.css";
import { useNavigate } from "react-router-dom";

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

export function AddMarcacoes() {
  const [show, setShow] = useState(false);
  const [selectedServicos, setSelectedServicos] = useState<servicosProps[]>([]);
  const [servicos, setServicos] = useState<servicosProps[]>([]);
  const [profissionaisByServico, setProfissionaisByServico] = useState<{
    [key: number]: profissionaisProps[];
  }>({});
  const [idUtilizador, setIdUtilizador] = useState<number | null>(null);
  const [selectedProfissionalId, setSelectedProfissionalId] = useState<
    number | null
  >(null);
  const [selectedHorarioId, setSelectedHorarioId] = useState<number | null>(
    null
  );
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
    handleClose();
  };

  const handleConfirmedClick = async () => {
    const usernameStorage = localStorage.getItem("usernameUtilizador");
    if (usernameStorage !== null) {
      try {
        // ID do usuário
        await fetchUserIdByUsername(usernameStorage);
  
        // Calcular o preço total da marcação
        const totalPreco = selectedServicos.reduce(
          (acc, servico) => acc + servico.preco,
          0
        );
  
        // Extrair os IDs necessários da marcação
        const idServico = selectedServicos.map((servico) => servico.idServico)[0]; // Ajuste conforme necessário
        const idHorario = selectedHorarioId; // Ajuste conforme necessário
        const idProfissional = selectedProfissionalId;
        const dataMarcacao = selectedDate?.toISOString(); // Ajuste conforme necessário
  
        // Verificar se o idUtilizador foi obtido com sucesso
        if (idUtilizador === null) {
          console.error("Não foi possível obter o ID do usuário.");
          return;
        }
  
        // Validação dos campos
        if (!idServico || !idHorario || !idProfissional || !dataMarcacao) {
          setAlertMessage("Por favor, preencha todos os campos obrigatórios.");
          setAlertVariant("warning");
          setShowAlert(true);
          return;
        }
  
        // Preparar os dados a serem enviados para o endpoint
        const formDataToSend = {
          totalPreco,
          idUsuario: idUtilizador,
          idHorario,
          idServico,
          idProfissional,
          dataMarcacao,
        };
  
        const url = `https://localhost:7209/CreateBooking`;
  
        const response = await fetch(url, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(formDataToSend),
        });
  
        if (response.ok) {
          // Marcação criada com sucesso
          setAlertMessage("Marcação criada com sucesso!");
          setAlertVariant("success");
          setShowAlert(true);
          setTimeout(() => {
            setShow(false);
            navigate("/logged");
          }, 3000);
        } else {
          const errorData = await response.json();
          console.error("Falha ao criar marcação", errorData);
          setAlertMessage("Falha ao criar a marcação.");
          setAlertVariant("danger");
          setShowAlert(true);
        }
      } catch (error) {
        console.error("Erro ao criar marcação:", error);
        setAlertMessage("Erro ao criar a marcação.");
        setAlertVariant("danger");
        setShowAlert(true);
      }
    } else {
      navigate("/login");
    }
  };
  
  

  const handleAddedClick = async (servico: servicosProps) => {
    if (!selectedServicos.some((s) => s.idServico === servico.idServico)) {
      setSelectedServicos([...selectedServicos, servico]);
      await fetchProfissionaisByServico(servico.fkCategoria, servico.idServico);
      setCartCount(cartCount + 1);
    }
  };

  const handleRemoveClick = (idServico: number) => {
    const updatedServicos = selectedServicos.filter(
      (servico) => servico.idServico !== idServico
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

  const handleProfissionalChange = async (profissionalId: number) => {
    setSelectedProfissionalId(profissionalId);
    if (profissionalId) {
      await fetchHorariosByProfissional(profissionalId);
    } else {
      setHorariosProfissional([]);
    }
  };

  const fetchHorariosByProfissional = async (profissionalId: number) => {
    var url = `https://localhost:7209/GetAllSchedulesByProfissionalId?profissionalId=${profissionalId}`;
    const horarios = await getAllData({ url });
    setHorariosProfissional(horarios);
  };

  const fetchUserIdByUsername = async (username: string) => {
    const url = `https://localhost:7209/GetIdByUsername?username=${encodeURIComponent(username)}`;
    
    try {
      const response = await fetch(url);
      if (response.ok) {
        const data = await response.json();
        setIdUtilizador(data.id); // Supondo que a resposta retorna um objeto com a propriedade 'id'
      } else {
        console.error('Falha ao obter o ID do usuário');
        // Lógica de tratamento de erro, se necessário
      }
    } catch (error) {
      console.error('Erro ao fazer a requisição:', error);
      // Lógica de tratamento de erro, se necessário
    }
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
            {showAlert && (
              <Alert variant={alertVariant} onClose={() => setShowAlert(false)} dismissible>
                {alertMessage}
              </Alert>
            )}
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
                          onChange={(e) =>
                            handleProfissionalChange(parseInt(e.target.value))
                          }
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
                            setSelectedHorarioId(parseInt(e.target.value))
                          }
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
                ))}
                <div className="d-flex flex-row">
                  <div className="input-container1">
                    <DatePicker
                      selected={selectedDate}
                      onChange={(date: Date | null) => setSelectedDate(date)}
                      dateFormat="dd/MM/yyyy"
                      className="form-control me-5"
                      placeholderText="Data da Marcação"
                    />
                  </div>
                </div>
                <div className="d-flex justify-content-center">
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
              </form>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </main>
  );
}
