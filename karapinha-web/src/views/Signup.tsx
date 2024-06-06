import { logo } from "../components/Images";
import "../styles/signup.css";


export function Signup() {
  return (
    <div className="container-main">
      <div className="container-image"></div>

      <div className="container-form">
      <img className="image-icon" src={logo} alt="Logo" />
        <form action="">
          <div className="input-container1">
            <input type="text" />
            <input type="text" />
            <input type="text" />
            <input type="text" />
          </div>

          <div className="input-container2">
            <input type="text" />
            <input type="text" />
            <input type="text" />
            <input type="text" />
          </div>
        </form>
      </div>
    </div>
  );
}
