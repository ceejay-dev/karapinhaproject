import { useState, useEffect } from "react";
import { Button as BootstrapButton, Table } from "react-bootstrap";
import "../styles/marcacao.css";

type clientesProps = {
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

export function ClientsList() {
  const [clientes, setClientes] = useState<clientesProps[]>([]);

  useEffect(() => {
    const fetchClientes = async () => {
      try {
        const response = await fetch("https://localhost:7209/GetClients");
        if (response.ok) {
          const data = await response.json();
          setClientes(data);
          console.log("Clientes:", data); 
        } else {
          console.error("Failed to fetch clientes");
        }
      } catch (error) {
        console.error("Error fetching clientes:", error);
      }
    };

    fetchClientes();
  }, []);

  const handleActivation = async (
    cliente: clientesProps,
    activate: boolean
  ) => {
    try {
      console.log("Activando cliente:", cliente); 
      console.log("Ativar:", activate); 

      const response = await fetch(
        `https://localhost:7209/ActivateOrDesactivate?id=${cliente.idUtilizador}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ activate }),
        }
      );

      if (response.ok) {
        const updatedClientes = clientes.map((c) =>
          c.idUtilizador === cliente.idUtilizador
            ? { ...c, estado: activate ? "activo" : "inactivo" }
            : c
        );
        setClientes(updatedClientes);
        console.log("Clientes atualizados:", updatedClientes); // Verifica se o estado dos clientes é atualizado corretamente
      } else {
        console.error("Failed to activate or deactivate cliente");
      }
    } catch (error) {
      console.error("Error activating or deactivating cliente:", error);
    }
  };

  return (
    <main className="container-service">
      <h4 className="text-center bg-white m-0 p-3 rounded-top-2">Clientes registados</h4>
        <Table striped bordered hover className="rounded-bottom-2">
          <thead>
            <tr>
              <th>Nome</th>
              <th>Email</th>
              <th>BI</th>
              <th>Telemóvel</th>
              <th>Username</th>
              <th>Estado</th>
              <th>Acções</th>
            </tr>
          </thead>
          <tbody>
            {clientes.map((cliente, index) => (
              <tr key={`${cliente.idUtilizador}-${index}`}>
                <td>{cliente.nomeUtilizador}</td>
                <td>{cliente.emailUtilizador}</td>
                <td>{cliente.bilheteUtilizador}</td>
                <td>{cliente.telemovelUtilizador}</td>
                <td>{cliente.usernameUtilizador}</td>
                <td>{cliente.estado}</td>
                <td>
                  <BootstrapButton
                    variant="success"
                    onClick={() => handleActivation(cliente, true)}
                    className="me-2"
                    disabled={cliente.estado === "activo"}
                  >
                    Activar
                  </BootstrapButton>
                  <BootstrapButton
                    variant="danger"
                    onClick={() => handleActivation(cliente, false)}
                    disabled={cliente.estado === "inactivo"}
                  >
                    Desactivar
                  </BootstrapButton>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
    </main>
  );
}
