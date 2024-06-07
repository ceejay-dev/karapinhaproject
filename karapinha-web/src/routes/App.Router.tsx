import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Login } from "../views/Login";
import { Signup } from "../views/Signup";
import { AdminHome } from "../views/AdminHome";
import { GestorHome } from "../views/GestorHome";
import { Home } from "../views/Home";
import { UpdateUser } from "../views/UpdateUser";
import { GuestHome } from "../views/GuestHome";
import { Services } from "../views/Services";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        
        <Route path="/" element={<Home />} />
        <Route path="/guest" element={<GuestHome />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<Signup />} />
        <Route path="/adminHome" element={<AdminHome />} />
        <Route path="/gestorHome" element={<GestorHome />} />
        <Route path="/editUser" element={<UpdateUser />} />
        <Route path="/services" element={<Services />} />
        
      </Routes>
    </BrowserRouter>
  );
}
