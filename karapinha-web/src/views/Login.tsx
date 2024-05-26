import "../styles/login.css";
export function Login() {
  return (
    <div className="main-container">
      <div className="imagem-container">

      </div>
      
      <div className="form-container">
        <form action="">
          <input type="text" placeholder="Username" />
          <input type="password" placeholder="Password" />
          <button type="submit">Login</button>
        </form>
      </div>
    </div>
  );
}
