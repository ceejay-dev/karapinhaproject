import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "../components/Button";
import { Button as BootstrapButton, Card } from "react-bootstrap";
import "../styles/adminHome.css";

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

export function GestorHome() {
  const navigate = useNavigate();
  const [mostRequestedServices, setMostRequestedServices] = useState<
    ServiceProps[]
  >([]);
  const [mostRequestedProfessionals, setMostRequestedProfessionals] = useState<
    ProfissionalProps[]
  >([]);

  useEffect(() => {
    async function fetchMostRequestedServices() {
      try {
        const response = await fetch(
          "https://localhost:7209/GetMostResquestedTreatments"
        );
        if (response.ok) {
          const data = await response.json();
          setMostRequestedServices(data);
        } else {
          console.error(
            "Erro ao buscar os serviços mais solicitados:",
            response.statusText
          );
        }
      } catch (error) {
        console.error("Erro ao buscar os serviços mais solicitados:", error);
      }
    }

    async function fetchMostRequestedProfessionals() {
      try {
        const response = await fetch(
          "https://localhost:7209/GetMostResquestedProfissional"
        );
        if (response.ok) {
          const data = await response.json();
          setMostRequestedProfessionals(data);
        } else {
          console.error(
            "Erro ao buscar os profissionais mais solicitados:",
            response.statusText
          );
        }
      } catch (error) {
        console.error(
          "Erro ao buscar os profissionais mais solicitados:",
          error
        );
      }
    }

    fetchMostRequestedServices();
    fetchMostRequestedProfessionals();
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
          <BootstrapButton
            className="link-button"
            onClick={() => {
              handleDeleteStorage();
            }}
          >
            Sair
          </BootstrapButton>
        </li>
      </nav>

      <div className="dashboard">
        <Card className="small-card bg-dark text-white cartao allcartoes">
          <Card.Header>Serviços Mais Solicitados</Card.Header>
          <Card.Body>
            {mostRequestedServices.map((service) => (
              <Card.Text key={service.nomeServico}>
                {service.nomeServico}
              </Card.Text>
            ))}
          </Card.Body>
        </Card>

        <Card className="small-card mt-3 bg-dark text-white allcartoes">
          <Card.Header>Profissionais Mais Solicitados</Card.Header>
          <Card.Body>
            {mostRequestedProfessionals.map((professional) => (
              <Card.Text key={professional.nomeProfissional}>
                {professional.nomeProfissional} - Solicitações:{" "}
                {professional.contador}
              </Card.Text>
            ))}
          </Card.Body>
        </Card>
      </div>
    </section>
  );
}
