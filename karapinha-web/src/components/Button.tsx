import { Link } from "react-router-dom";
import '../styles/home.css';

interface Props {
  text?: string;
  route: string;
  className?: string;
  imageSrc?: string;
}

export function Button({ text, route, className, imageSrc }: Props) {
  return (
    <Link to={route} className={className}>
      {imageSrc ? <img src={imageSrc} alt={text} className="button-image" /> : text}
    </Link>
  );
}
