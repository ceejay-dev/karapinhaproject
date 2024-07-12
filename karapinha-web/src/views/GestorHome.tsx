import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button as BootstrapButton, Card, OverlayTrigger, Tooltip, Table } from "react-bootstrap";
import { FaArrowUp, FaArrowDown } from "react-icons/fa";
import { Button } from "../components/Button";
import "../styles/adminHome.css";

type ServiceProps = {
  fkHorario: number;
  fkServico: number;
  fkProfissional: number;
  nomeServico?: string;
  nomeProfissional?: string;
  descricao?: string;
};

type ProfissionalProps = {
  nomeProfissional: string;
  contador: number;
};

type Marcacao = {
  idMarcacao: number;
  precoMarcacao: number;
  estado: string;
  dataMarcacao: string;
  utilizador: {
    nomeUtilizador: string;
  };
  servicos: ServiceProps[]; // Alterado para ServiceProps
};

export function GestorHome() {
  const navigate = useNavigate();
  const [mostRequestedServices, setMostRequestedServices] = useState<ServiceProps[]>([]);
  const [mostRequestedProfessionals, setMostRequestedProfessionals] = useState<ProfissionalProps[]>([]);
  const [monthlyBookings, setMonthlyBookings] = useState<Marcacao[]>([]);
  const [totalAmountToday, setTotalAmountToday] = useState<number | null>(null);
  const [totalAmountYesterday, setTotalAmountYesterday] = useState<number | null>(null);
  const [totalAmountCurrentMonth, setTotalAmountCurrentMonth] = useState<number | null>(null);
  const [totalAmountPastMonth, setTotalAmountPastMonth] = useState<number | null>(null);

  useEffect(() => {
    async function fetchMostRequestedServices() {
      try {
        const response = await fetch("https://localhost:7209/GetMostResquestedTreatments");
        if (response.ok) {
          const data = await response.json();
          setMostRequestedServices(data);
        } else {
          console.error("Erro ao buscar os serviços mais solicitados:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar os serviços mais solicitados:", error);
      }
    }

    async function fetchMostRequestedProfessionals() {
      try {
        const response = await fetch("https://localhost:7209/GetMostResquestedProfissional");
        if (response.ok) {
          const data = await response.json();
          setMostRequestedProfessionals(data);
        } else {
          console.error("Erro ao buscar os profissionais mais solicitados:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar os profissionais mais solicitados:", error);
      }
    }

    async function fetchMonthlyBookings() {
      try {
        const response = await fetch("https://localhost:7209/GetBookingsByMonth");
        if (response.ok) {
          const data = await response.json();

          // Transformando as marcações para incluir nome do serviço, nome do profissional e descrição do horário
          const updatedBookings = await Promise.all(data.map(async (marcacao: Marcacao) => {
            const updatedServicos = await Promise.all(marcacao.servicos.map(async (servico) => {
              try {
                const serviceResponse = await fetch(`https://localhost:7209/GetTreatmentById?id=${servico.fkServico}`);
                if (serviceResponse.ok) {
                  const serviceData = await serviceResponse.json();
                  servico.nomeServico = serviceData.nomeServico;
                } else {
                  console.error("Erro ao buscar o serviço:", serviceResponse.statusText);
                }

                const professionalResponse = await fetch(`https://localhost:7209/GetProfissionalById?id=${servico.fkProfissional}`);
                if (professionalResponse.ok) {
                  const professionalData = await professionalResponse.json();
                  servico.nomeProfissional = professionalData.nomeProfissional;
                } else {
                  console.error("Erro ao buscar o profissional:", professionalResponse.statusText);
                }

                const scheduleResponse = await fetch(`https://localhost:7209/GetSchedule?id=${servico.fkHorario}`);
                if (scheduleResponse.ok) {
                  const scheduleData = await scheduleResponse.json();
                  servico.descricao = scheduleData.descricao;
                } else {
                  console.error("Erro ao buscar o horário:", scheduleResponse.statusText);
                }
              } catch (error) {
                console.error("Erro ao processar a requisição:", error);
              }

              return servico;
            }));

            marcacao.servicos = updatedServicos;
            return marcacao;
          }));

          setMonthlyBookings(updatedBookings);
        } else {
          console.error("Erro ao buscar as marcações mensais:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar as marcações mensais:", error);
      }
    }

    async function fetchTotalAmountToday() {
      try {
        const response = await fetch("https://localhost:7209/GetTotalAmountToday");
        if (response.ok) {
          const data = await response.json();
          setTotalAmountToday(data);
        } else {
          console.error("Erro ao buscar o total faturado hoje:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar o total faturado hoje:", error);
      }
    }

    async function fetchTotalAmountYesterday() {
      try {
        const response = await fetch("https://localhost:7209/GetTotalAmountYesterday");
        if (response.ok) {
          const data = await response.json();
          setTotalAmountYesterday(data);
        } else {
          console.error("Erro ao buscar o total faturado ontem:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar o total faturado ontem:", error);
      }
    }

    async function fetchTotalAmountCurrentMonth() {
      try {
        const response = await fetch("https://localhost:7209/GetTotalAmountCurrentMonth");
        if (response.ok) {
          const data = await response.json();
          setTotalAmountCurrentMonth(data);
        } else {
          console.error("Erro ao buscar o total faturado no mês atual:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar o total faturado no mês atual:", error);
      }
    }

    async function fetchTotalAmountPastMonth() {
      try {
        const response = await fetch("https://localhost:7209/GetTotalAmountPastMonth");
        if (response.ok) {
          const data = await response.json();
          setTotalAmountPastMonth(data);
        } else {
          console.error("Erro ao buscar o total faturado no mês passado:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar o total faturado no mês passado:", error);
      }
    }

    fetchMostRequestedServices();
    fetchMostRequestedProfessionals();
    fetchMonthlyBookings();
    fetchTotalAmountToday();
    fetchTotalAmountYesterday();
    fetchTotalAmountCurrentMonth();
    fetchTotalAmountPastMonth();
  }, []);

  const handleDeleteStorage = () => {
    localStorage.clear();
    navigate("/");
  };

  return (
    <section className="principal-container">
      <nav className="nav">
        <li>
          <Button route="/profissionais" text="Profissionais" />
        </li>
        <li>
          <Button route="/categorias" text="Categorias" />
        </li>
        <li>
          <Button route="/servicos" text="Serviços" />
        </li>
        <li>
          <Button route="/confirm-marcacoes" text="Marcações" />
        </li>
        <li>
          <BootstrapButton className="link-button" onClick={handleDeleteStorage}>
            Sair
          </BootstrapButton>
        </li>
      </nav>

      <div className="dashboard">
        <h4 className="text-white text-center m-0 bg-dark desc">Agenda Mensal</h4>
        <Table striped bordered hover variant="dark" className="agenda-table">
          <thead>
            <tr>
              <th>Preço Total</th>
              <th>Data</th>
              <th>Estado</th>
              <th>Cliente</th>
              <th>Serviços</th>
              <th>Profissional</th>
              <th>Horário</th>
            </tr>
          </thead>
          <tbody>
            {monthlyBookings.map((marcacao) => (
              <tr key={marcacao.idMarcacao}>
                <td>{marcacao.precoMarcacao} kz</td>
                <td>{new Date(marcacao.dataMarcacao).toLocaleDateString()}</td>
                <td>{marcacao.estado}</td>
                <td>{marcacao.utilizador.nomeUtilizador}</td>
                <td>
                  {marcacao.servicos.map((servico, index) => (
                    <div key={index}>
                      {servico.nomeServico || "Carregando..."}
                    </div>
                  ))}
                </td>
                <td>
                  {marcacao.servicos.map((servico, index) => (
                    <div key={index}>
                      {servico.nomeProfissional || "Carregando..."}
                    </div>
                  ))}
                </td>
                <td>
                  {marcacao.servicos.map((servico, index) => (
                    <div key={index}>
                      {servico.descricao || "Carregando..."}
                    </div>
                  ))}
                </td>
              </tr>
            ))}
          </tbody>
        </Table>

        <div className="custom-card-container">
          <Card className="custom-card bg-dark text-white">
            <Card.Header className="custom-card-header">Serviços Mais Solicitados</Card.Header>
            <Card.Body className="custom-card-body">
              {mostRequestedServices.map((service, index) => (
                <Card.Text key={index} className="d-flex justify-content-between align-items-center">
                  {service.nomeServico}
                  {index === 0 && (
                    <OverlayTrigger placement="top" overlay={<Tooltip>Serviço mais solicitado</Tooltip>}>
                      <FaArrowUp style={{ color: "green" }} />
                    </OverlayTrigger>
                  )}
                  {index === mostRequestedServices.length - 1 && (
                    <OverlayTrigger placement="top" overlay={<Tooltip>Serviço menos solicitado</Tooltip>}>
                      <FaArrowDown style={{ color: "red" }} />
                    </OverlayTrigger>
                  )}
                </Card.Text>
              ))}
            </Card.Body>
          </Card>

          <Card className="custom-card bg-dark text-white">
            <Card.Header className="custom-card-header">Profissionais Mais Solicitados</Card.Header>
            <Card.Body className="custom-card-body">
              {mostRequestedProfessionals.map((professional, index) => (
                <Card.Text key={index}>
                  {professional.nomeProfissional} - Solicitações: {professional.contador}
                </Card.Text>
              ))}
            </Card.Body>
          </Card>

          <Card className="custom-card bg-dark text-white">
            <Card.Header className="custom-card-header">Tabela de Facturação</Card.Header>
            <Card.Body className="custom-card-body">
              <Card.Text>Hoje: {totalAmountToday !== null ? `${totalAmountToday} kz` : "Carregando..."}</Card.Text>
              <Card.Text>Ontem: {totalAmountYesterday !== null ? `${totalAmountYesterday} kz` : "Carregando..."}</Card.Text>
              <Card.Text>Mês actual: {totalAmountCurrentMonth !== null ? `${totalAmountCurrentMonth} kz` : "Carregando..."}</Card.Text>
              <Card.Text>Mês Passado: {totalAmountPastMonth !== null ? `${totalAmountPastMonth} kz` : "Carregando..."}</Card.Text>
            </Card.Body>
          </Card>
        </div>
      </div>
    </section>
  );
}
