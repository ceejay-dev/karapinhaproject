import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { logo } from "../components/Images";
import "../styles/signup.css";
import { Button, Alert } from "react-bootstrap";

export function Signup() {
  const navigate = useNavigate();
  const [showAlert, setShowAlert] = useState(false);
  
  const [formData, setFormData] = useState({
    idUtilizador: '0',
    NomeUtilizador: '',
    EmailUtilizador: '',
    TelemovelUtilizador: '',
    FotoUtilizador: null,
    BilheteUtilizador: '',
    UsernameUtilizador: '',
    PasswordUtilizador: '',
  });

  const handleChange = (event:any) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleFileChange = (event:any) => {
    setFormData((prevData) => ({
      ...prevData,
      FotoUtilizador: event.target.files[0],
    }));
  };

  const handleLoginClick = (event:any) => {
    event.preventDefault();
    navigate("/login");
  };

  const handleRegisterClick = async (event:any) => {
    event.preventDefault();
    
    const form = new FormData();
    form.append('TipoPerfil', 'cliente');
    if (formData.FotoUtilizador !== null) {
      form.append('foto', formData.FotoUtilizador);
    }    
    form.append('Estado', 'inactivo');
    form.append('TelemovelUtilizador', formData.TelemovelUtilizador);
    form.append('BilheteUtilizador', formData.BilheteUtilizador);
    form.append('UsernameUtilizador', formData.UsernameUtilizador);
    form.append('NomeUtilizador', formData.NomeUtilizador);
    form.append('IdUtilizador', '');
    form.append('PasswordUtilizador', formData.PasswordUtilizador);
    form.append('EmailUtilizador', formData.EmailUtilizador);

    try {
      const response = await fetch('https://localhost:7209/CreateUser', {
        method: 'POST',
        body: form,
      });
      
      if (response.ok) {
        setShowAlert(true);
        setTimeout(() => {
          navigate("/");
        }, 3000);
      } else {
        console.error("Failed to register user.");
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  return (
    <div className="container-main">
      <div className="container-image"></div>

      <div className="container-form">
        <div className=" d-flex justify-content-center">
          <img className="image-icon" src={logo} alt="Logo" />
        </div>
        <form
          className="d-flex justify-content-center formulario"
          onSubmit={handleRegisterClick}
        >
          <div className="d-flex flex-row">
            <div className="input-container1">
              <input
                value={formData.NomeUtilizador}
                onChange={handleChange}
                name="NomeUtilizador"
                type="text"
                placeholder="Nome"
                className="m-1"
              />
              <input
                value={formData.EmailUtilizador}
                onChange={handleChange}
                name="EmailUtilizador"
                type="text"
                placeholder="Email"
                className="m-1"
              />
              <input
                value={formData.TelemovelUtilizador}
                onChange={handleChange}
                name="TelemovelUtilizador"
                type="text"
                placeholder="Telemóvel"
                className="m-1"
              />
              <input
                onChange={handleFileChange}
                name="FotoUtilizador"
                type="file"
                className="m-1"
              />
            </div>

            <div className="input-container2">
              <input
                value={formData.BilheteUtilizador}
                onChange={handleChange}
                name="BilheteUtilizador"
                type="text"
                placeholder="BI"
                className="m-1"
              />
              <input
                value={formData.UsernameUtilizador}
                onChange={handleChange}
                name="UsernameUtilizador"
                type="text"
                placeholder="Username"
                className="m-1"
              />
              <input
                value={formData.PasswordUtilizador}
                onChange={handleChange}
                name="PasswordUtilizador"
                type="text"
                placeholder="Password"
                className="m-1"
              />
            </div>
          </div>

          <div className="d-flex justify-content-center">
            <div className="m-1">
              <Button variant="outline-dark" onClick={handleLoginClick}>
                Login
              </Button>
            </div>

            <div className="m-1">
              <Button variant="dark" type="submit">
                Registar
              </Button>
            </div>
          </div>
        </form>
        {showAlert && (
          <Alert variant="info" className="mt-3">
            Administrador precisa ativar a sua conta para fazer login.
          </Alert>
        )}
      </div>
    </div>
  );
}
