import { Button as BootstrapButton } from "react-bootstrap";
import '../styles/home.css';

interface Props {
  text?: string;
  className?: string;
  imageSrc?: string;
  onClick?: () => void;
}

export function ButtonNav({ text, className, imageSrc, onClick }: Props) {
  return (
    <BootstrapButton className={className}>
      {imageSrc ? <img src={imageSrc} alt={text} className="button-image" onClick={onClick}/> : text}
    </BootstrapButton>
  );
}
