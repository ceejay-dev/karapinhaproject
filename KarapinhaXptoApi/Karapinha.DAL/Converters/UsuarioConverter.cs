using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Converters
{
    public abstract class UtilizadorConverter
    {
        public static Utilizador ToUtilizador(UtilizadorCreateDTO dto)
        {
            return new Utilizador
            {
                NomeUtilizador = dto.NomeUtilizador,
                EmailUtilizador = dto.EmailUtilizador,
                TelemovelUtilizador = dto.TelemovelUtilizador,
                FotoUtilizador = dto.FotoUtilizador,
                UsernameUtilizador = dto.UsernameUtilizador,
                PasswordUtilizador = dto.PasswordUtilizador,
                Estado = dto.Estado,
                TipoConta = dto.TipoConta
            };
        }

        public static UtilizadorCreateDTO ToUtilizadorDTO(Utilizador utilizador)
        {
            return new UtilizadorCreateDTO
            {
                NomeUtilizador = utilizador.NomeUtilizador,
                EmailUtilizador = utilizador.EmailUtilizador,
                TelemovelUtilizador = utilizador.TelemovelUtilizador,
                FotoUtilizador = utilizador.FotoUtilizador,
                UsernameUtilizador = utilizador.UsernameUtilizador,
                PasswordUtilizador = utilizador.PasswordUtilizador,
                Estado = utilizador.Estado,
                TipoConta = utilizador.TipoConta
            };

        }

        public static Utilizador UpdateUtilizador(UtilizadorUpdateDTO utilizador,Utilizador user)
        {
            user.NomeUtilizador = utilizador.NomeUtilizador;
            user.EmailUtilizador = utilizador.EmailUtilizador;
            user.TelemovelUtilizador = utilizador.TelemovelUtilizador;
            user.FotoUtilizador = utilizador.FotoUtilizador;
            user.UsernameUtilizador = utilizador.UsernameUtilizador;
            user.PasswordUtilizador = utilizador.PasswordUtilizador;
            return user;
        }

        /*public static Utilizador ToUtilizador_(UtilizadorUpdateDTO utilizador)
        {
            return new Utilizador
            {
                IdUtilizador = utilizador.IdUtilizador,
                NomeUtilizador = utilizador.NomeUtilizador,
                EmailUtilizador = utilizador.EmailUtilizador,
                TelemovelUtilizador = utilizador.TelemovelUtilizador,
                FotoUtilizador = utilizador.FotoUtilizador,
                UsernameUtilizador = utilizador.UsernameUtilizador,
                PasswordUtilizador = utilizador.PasswordUtilizador,
            };

        }*/
    }
}
