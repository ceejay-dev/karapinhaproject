import "../styles/login.css"; 
import { logo } from '../components/Images';
import Button from 'react-bootstrap/Button';
import { useNavigate } from 'react-router-dom';

export function Login() {
  const navigate = useNavigate();

  const handleLoginClick = (event: { preventDefault: () => void; }) => {
    event.preventDefault(); 
    navigate('/Login'); 
  };

  const handleRegisterClick = (event: { preventDefault: () => void; }) => {
    event.preventDefault();
    navigate('/Signup'); 
  };

  return (
    <div className="main-container">
      <div className="imagem-container">
      </div>

      <div className="form-container">
        <img className="logo-image" src={logo} alt="Logo" />
        <form>
          <input type="text" placeholder="Username" />
          <input type="password" placeholder="Password" />

          <div className="button-container">
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

export default Login;
