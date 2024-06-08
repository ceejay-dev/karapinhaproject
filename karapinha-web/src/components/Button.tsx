import { Link } from "react-router-dom";
import '../styles/home.css';

interface Props {
  text?: string;
  route: string;
  className?: string;
  imageSrc?: string;
  onClick?: () => void;
}

export function Button({ text, route, className, imageSrc, onClick }: Props) {
  return (
    <Link to={route} className={className}>
      {imageSrc ? <img src={imageSrc} alt={text} className="button-image"onClick={onClick}/> : text}
    </Link>
  );
}
