import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button as BootstrapButton, Card, OverlayTrigger, Tooltip, Table } from "react-bootstrap";
import { FaArrowUp, FaArrowDown } from "react-icons/fa";
import { Button } from "../components/Button";

type ServiceProps = {
  nomeServico: string;
  descricao: string;
  quantidade: number;
  preco: number;
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
  servicos: {
    nomeServico?: string;
    nomeProfissional?: string;
    descricao?: string;
  }[];
};

export function GestorHome() {
  const navigate = useNavigate();
  const [mostRequestedServices, setMostRequestedServices] = useState<ServiceProps[]>([]);
  const [mostRequestedProfessionals, setMostRequestedProfessionals] = useState<ProfissionalProps[]>([]);
  const [monthlyBookings, setMonthlyBookings] = useState<Marcacao[]>([]);

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
          setMonthlyBookings(data);
        } else {
          console.error("Erro ao buscar as marcações mensais:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar as marcações mensais:", error);
      }
    }

    fetchMostRequestedServices();
    fetchMostRequestedProfessionals();
    fetchMonthlyBookings();
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
                <Card.Text key={service.nomeServico} className="d-flex justify-content-between align-items-center">
                  {service.nomeServico}
                  {index === 0 && (
                    <OverlayTrigger
                      placement="top"
                      overlay={<Tooltip>Serviço mais solicitado</Tooltip>}
                    >
                      <FaArrowUp style={{ color: 'green' }} />
                    </OverlayTrigger>
                  )}
                  {index === mostRequestedServices.length - 1 && (
                    <OverlayTrigger
                      placement="top"
                      overlay={<Tooltip>Serviço menos solicitado</Tooltip>}
                    >
                      <FaArrowDown style={{ color: 'red' }} />
                    </OverlayTrigger>
                  )}
                </Card.Text>
              ))}
            </Card.Body>
          </Card>

          <Card className="custom-card bg-dark text-white">
            <Card.Header className="custom-card-header">Profissionais Mais Solicitados</Card.Header>
            <Card.Body className="custom-card-body">
              {mostRequestedProfessionals.map((professional) => (
                <Card.Text key={professional.nomeProfissional}>
                  {professional.nomeProfissional} - Solicitações: {professional.contador}
                </Card.Text>
              ))}
            </Card.Body>
          </Card>
        </div>
      </div>
    </section>
  );
}
