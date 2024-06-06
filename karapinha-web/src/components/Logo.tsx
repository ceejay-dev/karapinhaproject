import {logo} from '../components/Images'
import "../styles/login.css"

export function Logo(){
    return (
        <img
            className="logo-image"
            src={logo}
            alt="Logo"
        />
    );
}