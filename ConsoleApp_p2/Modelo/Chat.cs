using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp_p2.Modelo
{
    class Chat
    {
        public Contacto Contacto;
        public List<Mensaje> Mensaje;
        public Random Rng = new Random();

        public Chat(Contacto contacto)
        {
            this.Contacto = contacto;
        }

        public int ContarNoLeidos(List<Mensaje> mensaje)
        {
            int v = 0;

            for (int i = 0; i < Mensaje.Count(); i++)
            {
                if (mensaje[i].EsMio == false && mensaje[i].Visto == false)
                {
                    v += 1;
                }
            }
            return v;
        }

        public void Enviar(Mensaje mensaje)
        {
            if (string.IsNullOrEmpty(mensaje.Texto.Trim()))
            {
                throw new ArgumentException();
            }
            else
            {
                mensaje.EsMio = true;
                Mensaje.Add(mensaje);
            }
        }

        public bool ContieneTermino(string texto)
        {
            foreach (Mensaje msj in Mensaje)
            {
                if (msj.Texto.Contains(texto))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ActualizarVistos(List<Mensaje> mensajes)
        {
            foreach (Mensaje msj in mensajes)
            {
                if (msj.EsMio == false && msj.Visto == false)
                {
                    msj.Visto = true;
                }
            }
        }

        public int IndexDe(Mensaje mensaje)
        {
            if (this.Mensaje.Contains(mensaje))
            {
                return this.Mensaje.IndexOf(mensaje);
            }
            else
            {
                return -1;
            }
        }

        public bool Refrescar()
        {
            int val = Rng.Next(0, 100);

            if (val >= 25)
            {
                return false;
            }

            foreach (Mensaje msj in this.Mensaje)
            {
                if (msj.EsMio == true && msj.Visto == false)
                {
                    msj.Visto = true;
                }
            }

            if (this.Mensaje.Count() == 0)
            {
                this.Mensaje.Add(new Mensaje("hola, ¿como estas?", false, false));
            }
            else if (this.Mensaje.Last().EsMio)
            {
                this.Mensaje.Add(new Mensaje(Mensaje.Last().Texto.ToUpper(), false, false));
            }
            else if (Mensaje.Last().EsMio == false)
            {
                this.Mensaje.Add(new Mensaje("Respondeme pliz", false, false));
            }

            return true;
        }
    }
}
