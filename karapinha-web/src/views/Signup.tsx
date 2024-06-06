import { useNavigate } from "react-router-dom";
import { logo } from "../components/Images";
import "../styles/signup.css";
import { Button } from "react-bootstrap";

export function Signup() {
  const navigate = useNavigate();

  const handleLoginClick = (event: { preventDefault: () => void }) => {
    event.preventDefault();
    navigate("/Login");
  };

  const handleRegisterClick = (event: { preventDefault: () => void }) => {
    event.preventDefault();
    navigate("/Signup");
  };
  return (
    <div className="container-main">
      <div className="container-image"></div>

      <div className="container-form">
        <div className=" d-flex justify-content-center">
          <img className="image-icon" src={logo} alt="Logo" />
        </div>
        <form action="" className="d-flex justify-content-center formulario">
          <div className="d-flex flex-row">
            <div className="input-container1">
              <input type="text" placeholder="Nome" className="m-1"/>
              <input type="text" placeholder="Email" className="m-1"/>
              <input type="text" placeholder="Telemóvel" className="m-1"/>
              <input type="text" placeholder="Foto" className="m-1"/>
            </div>

            <div className="input-container2">
              <input type="text" placeholder="BI" className="m-1"/>
              <input type="text" placeholder="Username" className="m-1"/>
              <input type="text" placeholder="Password" className="m-1"/>
              <input type="text" placeholder="Confirmar password" className="m-1"/>
            </div>
          </div>

          <div className="d-flex justify-content-center">
            <div className="botao">
              <Button variant="outline-dark" onClick={handleLoginClick}>
                Login
              </Button>
            </div>

            <div className="botao">
              <Button variant="dark" onClick={handleRegisterClick}>
                Registar
              </Button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}
