import { useNavigate } from "react-router-dom";
import { logo } from "../components/Images";
import "../styles/signup.css";
import { Button } from "react-bootstrap";

export function UpdateUser() {
  const navigate = useNavigate();

  const handleHomeClick = (event: { preventDefault: () => void }) => {
    event.preventDefault();
    navigate("/");
  };

  const handleEditedClick = (event: { preventDefault: () => void }) => {
    event.preventDefault();
    navigate("/");
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
            <div className="m-1">
              <Button variant="danger" onClick={handleHomeClick} className="botao">
                Sair
              </Button>
            </div>

            <div className="m-1">
              <Button variant="success" onClick={handleEditedClick} className="botao">
                Confirmar
              </Button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}
