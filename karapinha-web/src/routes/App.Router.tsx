import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Login } from "../views/Login";
import { Signup } from "../views/Signup";
import { AdminHome } from "../views/AdminHome";
import { GestorHome } from "../views/GestorHome";
import { Home } from "../views/Home";
import { UpdateUser } from "../views/UpdateUser";
import { GuestHome } from "../views/GuestHome";
import { AddServicos } from "../views/AddServicos";
import { AddProfissionais } from "../views/AddProfissionais";
import { AddAdministrativos } from "../views/AddAdministrativos";
import { ClientsList } from "../views/ClientsList";
import { CreateServicos } from "../views/CreateServicos";
import { ScheduleList } from "../views/ScheduleList";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        
      <Route path="/" element={<GuestHome />} />
        <Route path="/logged" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<Signup />} />
        <Route path="/adminHome" element={<AdminHome />} />
        <Route path="/gestorHome" element={<GestorHome />} />
        <Route path="/editUser" element={<UpdateUser />} />
        <Route path="/servicos" element={<AddServicos />} />
        <Route path="/profissionais" element={<AddProfissionais />} />
        <Route path="/administrativos" element={<AddAdministrativos />} />
        <Route path="/clientes" element={<ClientsList />} />
        <Route path="/createServicos" element={<CreateServicos />} />
        <Route path="/marcacoes" element={<ScheduleList />} />

      </Routes>
    </BrowserRouter>
  );
}
