import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Login } from "../views/Login";
import { Home } from "../views/Home";
import { Signup } from "../views/Signup";
import { AdminHome } from "../views/AdminHome";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<Signup />}></Route>
        <Route path="/adminhome" element={<AdminHome />}></Route>
      </Routes>
    </BrowserRouter>
  );
}
