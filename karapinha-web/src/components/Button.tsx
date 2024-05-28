import { Link } from "react-router-dom";

interface Props{
    text: string;
    route: string;
    className?: string;
}

export function Button({text, route, className}:Props) {
  return <Link to={route} className={className}>{text} </Link>;
}
