import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Login } from "../views/Login";
import { Signup } from "../views/Signup";
import { AdminHome } from "../views/AdminHome";
import { GestorHome } from "../views/GestorHome";
import { Home } from "../views/Home";
import { UpdateUser } from "../views/UpdateUser";
import { GuestHome } from "../views/GuestHome";
import { AddMarcacoes } from "../views/AddMarcacoes";
import { AddProfissionais } from "../views/AddProfissionais";
import { AddAdministrativos } from "../views/AddAdministrativos";
import { ClientsList } from "../views/ClientsList";
import { AddServicos } from "../views/AddServicos";
import { ScheduleList } from "../views/ScheduleList";
import { AddCategorias } from "../views/AddCategorias";
import { MarcacoesList } from "../views/MarcacoesList";
import { ConfirmMarcacoes } from "../views/ConfirmMarcacoes";
import About from "../views/About";


export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<GuestHome />} />
        <Route path="/logged" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<Signup />} />
        <Route path="/editUser" element={<UpdateUser />} />
        <Route path="/marcacoes" element={<AddMarcacoes />} />
        <Route path="/mymarcacoes" element={<MarcacoesList/>}/>
        <Route path="/confirm-marcacoes" element={<ConfirmMarcacoes/>}/>
        <Route path="/adminHome" element={<AdminHome />} />
        <Route path="/gestorHome" element={<GestorHome />} />
        <Route path="/servicos" element={<AddServicos />} />
        <Route path="/profissionais" element={<AddProfissionais />} />
        <Route path="/administrativos" element={<AddAdministrativos />} />
        <Route path="/about" element={<About />} />
        <Route path="/categorias" element={<AddCategorias />} />
        <Route path="/clientes" element={<ClientsList />} />
        <Route path="/consulta-marcacoes" element={<ScheduleList />} />
      </Routes>
    </BrowserRouter>
  );
}
