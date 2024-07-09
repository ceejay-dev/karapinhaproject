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
  const userId = localStorage.getItem("idUtilizador");

  useEffect(() => {
    async function fetchMarcacoes() {
      if (userId) {
        const url = `https://localhost:7209/GetAllBookingByUserId?id=${userId}`;
        try {
          const response = await fetch(url);
          if (response.ok) {
            const data = await response.json();
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
  }, [userId]);

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
                          Descrição do serviço: {servico.fkServico}
                        </Card.Text>
                        <Card.Text>
                          Profissional: {servico.fkProfissional}
                        </Card.Text>
                        <Card.Text>Horário: {servico.fkHorario}</Card.Text>
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
