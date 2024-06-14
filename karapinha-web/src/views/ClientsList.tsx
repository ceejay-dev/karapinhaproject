import { useState, useEffect } from "react";
import { Button as BootstrapButton } from "react-bootstrap";
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
          console.log("Clientes:", data); // Verifica se os clientes foram carregados corretamente
        } else {
          console.error("Failed to fetch clientes");
        }
      } catch (error) {
        console.error("Error fetching clientes:", error);
      }
    };

    fetchClientes();
  }, []);

  const handleActivation = async (cliente: clientesProps, activate: boolean) => {
    try {
      console.log("Activando cliente:", cliente); // Verifique se o cliente tem idUtilizador
      console.log("Ativar:", activate); // Verifique se a função de ativação é chamada corretamente

      const response = await fetch(`https://localhost:7209/ActivateOrDesactivate?id=${cliente.idUtilizador}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ activate }), // Corrigido para enviar um objeto JSON válido
      });

      if (response.ok) {
        const updatedClientes = clientes.map((c) =>
          c.idUtilizador === cliente.idUtilizador ? { ...c, estado: activate ? "activo" : "inactivo" } : c
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
      <h4 className="text-center text-info">Clientes</h4>
      {clientes.map((cliente, index) => (
        <div key={`${cliente.idUtilizador}-${index}`} className="bg-white border border-2 border-black">
          <div>
            <h3>Nome: {cliente.nomeUtilizador}</h3>
            <h5>Email: {cliente.emailUtilizador} </h5>
            <h5>BI: {cliente.bilheteUtilizador}</h5>
            <h5>Telemóvel: {cliente.telemovelUtilizador}</h5>
            <h5>Username: {cliente.usernameUtilizador}</h5>
            <h5>Estado: {cliente.estado}</h5>
          </div>
  
          <div className="m-1">
            <BootstrapButton
              variant="info"
              onClick={() => handleActivation(cliente, true)}
              className="me-2"
              disabled={cliente.estado === "activo"}
            >
              Activar
            </BootstrapButton>
            <BootstrapButton
              variant="info"
              onClick={() => handleActivation(cliente, false)}
              disabled={cliente.estado === "inactivo"}
            >
              Desactivar
            </BootstrapButton>
          </div>
        </div>
      ))}
    </main>
  );  
}
