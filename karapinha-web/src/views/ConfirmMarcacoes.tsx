import { useEffect, useState } from "react";
import { Container, Table, Button, Modal } from "react-bootstrap";
import "../styles/marcacao.css";

type Servico = {
  fkHorario: number;
  fkServico: number;
  fkProfissional: number;
};

type Utilizador = {
  idUtilizador: number;
  nomeUtilizador: string;
  emailUtilizador: string;
  bilheteUtilizador: string;
  telemovelUtilizador: string;
  fotoUtilizador: string;
  usernameUtilizador: string;
  passwordUtilizador: string;
  estado: string;
  tipoPerfil: string;
};

type Marcacao = {
  idMarcacao: number;
  precoMarcacao: number;
  estado: string;
  dataMarcacao: string;
  utilizador: Utilizador;
  servicos: Servico[];
};

export function ConfirmMarcacoes() {
  const [marcacoes, setMarcacoes] = useState<Marcacao[]>([]);
  const [confirming, setConfirming] = useState<{ [key: number]: boolean }>({});
  const [showModal, setShowModal] = useState(false);
  const [modalMessage, setModalMessage] = useState("");
  const [modalVariant, setModalVariant] = useState<"success" | "danger">("success");

  useEffect(() => {
    async function fetchMarcacoes() {
      try {
        const response = await fetch("https://localhost:7209/GetAllBookings");
        if (response.ok) {
          const data = await response.json();
          setMarcacoes(data);
        } else {
          console.error("Erro ao buscar marcações:", response.statusText);
        }
      } catch (error) {
        console.error("Erro ao buscar marcações:", error);
      }
    }

    fetchMarcacoes();
  }, []);

  const handleConfirm = async (idMarcacao: number) => {
    setConfirming((prev) => ({ ...prev, [idMarcacao]: true }));
    const url = `https://localhost:7209/ConfirmBooking?id=${idMarcacao}`;
    try {
      const response = await fetch(url, { method: "PUT" });
      if (response.ok) {
        setModalMessage("Marcação confirmada com sucesso!");
        setModalVariant("success");
      } else {
        setModalMessage("Erro ao confirmar marcação.");
        setModalVariant("danger");
      }
    } catch (error) {
      console.error("Erro ao confirmar marcação:", error);
      setModalMessage("Erro ao confirmar marcação.");
      setModalVariant("danger");
    } finally {
      setConfirming((prev) => ({ ...prev, [idMarcacao]: false }));
      setShowModal(true);
      setTimeout(() => {
        setShowModal(false);
        window.location.reload();
      }, 2500); 
    }
  };

  return (
    <main className="container-service">
      <div className="bg-white m-2 p-3">
        <Container>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Preço Total</th>
                <th>Data</th>
                <th>Estado</th>
                <th>Cliente</th>
                <th>Serviços</th>
                <th>Profissional</th>
                <th>Horário</th>
                <th>Ação</th>
              </tr>
            </thead>
            <tbody>
              {marcacoes.map((marcacao) => (
                <tr key={marcacao.idMarcacao}>
                  <td>{marcacao.precoMarcacao} kz</td>
                  <td>{new Date(marcacao.dataMarcacao).toLocaleDateString()}</td>
                  <td>{marcacao.estado}</td>
                  <td>{marcacao.utilizador.nomeUtilizador}</td>
                  <td>
                    {marcacao.servicos.map((servico, index) => (
                      <div key={index}>
                        Serviço {index + 1}: {servico.fkServico}
                      </div>
                    ))}
                  </td>
                  <td>
                    {marcacao.servicos.map((servico, index) => (
                      <div key={index}>
                        Profissional {index + 1}: {servico.fkProfissional}
                      </div>
                    ))}
                  </td>
                  <td>
                    {marcacao.servicos.map((servico, index) => (
                      <div key={index}>
                        Horário {index + 1}: {servico.fkHorario}
                      </div>
                    ))}
                  </td>
                  <td>
                    <Button
                      variant="success"
                      onClick={() => handleConfirm(marcacao.idMarcacao)}
                      disabled={confirming[marcacao.idMarcacao] || marcacao.estado === "validado"}
                    >
                      Confirmar
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Container>
      </div>

      {/* Modal para exibir mensagem de confirmação */}
      <Modal show={showModal} onHide={() => setShowModal(false)} centered>
        <Modal.Body className={`text-${modalVariant}`}>
          <p>{modalMessage}</p>
        </Modal.Body>
      </Modal>
    </main>
  );
}