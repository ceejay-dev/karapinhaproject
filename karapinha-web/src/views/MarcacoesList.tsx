import { useEffect, useState } from "react";
import { Container, Row, Col, Card } from "react-bootstrap";
import "../styles/marcacao.css";

type Servico = {
  fkHorario: number;
  fkServico: number;
  fkProfissional: number;
};

type Marcacao = {
  idMarcacao: number;
  precoMarcacao: number;
  dataMarcacao: string;
  estado: string;
  servicos: Servico[];
};

export function MarcacoesList() {
  const [marcacoes, setMarcacoes] = useState<Marcacao[]>([]);
  const [serviceNames, setServiceNames] = useState<{ [key: number]: string }>({});
  const [professionalNames, setProfessionalNames] = useState<{ [key: number]: string }>({});
  const [scheduleDescriptions, setScheduleDescriptions] = useState<{ [key: number]: string }>({});
  const userId = localStorage.getItem("idUtilizador");

  useEffect(() => {
    async function fetchServiceName(id: number) {
      const url = `https://localhost:7209/GetTreatmentById?id=${id}`;
      try {
        const response = await fetch(url);
        if (response.ok) {
          const data = await response.json();
          return data.nomeServico;
        } else {
          console.error("Erro ao buscar nome do serviço");
        }
      } catch (error) {
        console.error("Erro ao buscar nome do serviço:", error);
      }
      return null;
    }

    async function fetchProfessionalName(id: number) {
      const url = `https://localhost:7209/GetProfissionalById?id=${id}`;
      try {
        const response = await fetch(url);
        if (response.ok) {
          const data = await response.json();
          return data.nomeProfissional;
        } else {
          console.error("Erro ao buscar nome do profissional");
        }
      } catch (error) {
        console.error("Erro ao buscar nome do profissional:", error);
      }
      return null;
    }

    async function fetchScheduleDescription(id: number) {
      const url = `https://localhost:7209/GetSchedule?id=${id}`;
      try {
        const response = await fetch(url);
        if (response.ok) {
          const data = await response.json();
          return data.descricao;
        } else {
          console.error("Erro ao buscar descrição do horário");
        }
      } catch (error) {
        console.error("Erro ao buscar descrição do horário:", error);
      }
      return null;
    }

    async function fetchMarcacoes() {
      if (userId) {
        const url = `https://localhost:7209/GetAllBookingByUserId?id=${userId}`;
        try {
          const response = await fetch(url);
          if (response.ok) {
            const data = await response.json();

            // Fetch nome do serviço, nome do professional, e descrição hora
            const namePromises = data.flatMap((marcacao: Marcacao) =>
              marcacao.servicos.map(async (servico: Servico) => {
                if (!serviceNames[servico.fkServico]) {
                  const nomeServico = await fetchServiceName(servico.fkServico);
                  if (nomeServico) {
                    setServiceNames(prevNames => ({
                      ...prevNames,
                      [servico.fkServico]: nomeServico
                    }));
                  }
                }
                if (!professionalNames[servico.fkProfissional]) {
                  const nomeProfissional = await fetchProfessionalName(servico.fkProfissional);
                  if (nomeProfissional) {
                    setProfessionalNames(prevNames => ({
                      ...prevNames,
                      [servico.fkProfissional]: nomeProfissional
                    }));
                  }
                }
                if (!scheduleDescriptions[servico.fkHorario]) {
                  const descricaoHorario = await fetchScheduleDescription(servico.fkHorario);
                  if (descricaoHorario) {
                    setScheduleDescriptions(prevDescriptions => ({
                      ...prevDescriptions,
                      [servico.fkHorario]: descricaoHorario
                    }));
                  }
                }
              })
            );

            await Promise.all(namePromises);
            setMarcacoes(data);
          } else {
            console.error("Erro ao buscar marcações");
          }
        } catch (error) {
          console.error("Erro ao buscar marcações:", error);
        }
      }
    }

    fetchMarcacoes();
  }, [userId, serviceNames, professionalNames, scheduleDescriptions]);

  return (
    <main className="container-service">
      <div className="bg-white m-2 p-3">
        <Container>
          <Row>
            {marcacoes.map((marcacao) => (
              <Col key={marcacao.idMarcacao} className="col-md-3 mb-3">
                <Card className="w-100">
                  <Card.Body>
                    <Card.Title>Marcação</Card.Title>
                    <Card.Text>
                      Preço Total: {marcacao.precoMarcacao} kz
                    </Card.Text>
                    <Card.Text>Estado: {marcacao.estado} </Card.Text>
                    <Card.Text>
                      Data da Marcação:{" "}
                      {new Date(marcacao.dataMarcacao).toLocaleString()}
                    </Card.Text>
                  </Card.Body>
                  <Card.Body>
                    <span className="h6">Serviços: </span>
                    {marcacao.servicos.map((servico, index) => (
                      <div key={index}>
                        <Card.Text>
                          Descrição do serviço: {serviceNames[servico.fkServico] || servico.fkServico}
                        </Card.Text>
                        <Card.Text>
                          Profissional: {professionalNames[servico.fkProfissional] || servico.fkProfissional}
                        </Card.Text>
                        <Card.Text>Horário: {scheduleDescriptions[servico.fkHorario] || servico.fkHorario}</Card.Text>
                        <span>------------------------------</span>
                      </div>
                    ))}
                  </Card.Body>
                </Card>
              </Col>
            ))}
          </Row>
        </Container>
      </div>
    </main>
  );
}
