using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int contador = 0;
            for(int i = 0; i < Mensaje.Count; i++)
            {
                if (mensaje[i].EsMio == false && mensaje[i].Visto == false)
                {
                    contador += 1;
                }
            }
            return contador;
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
            foreach(Mensaje mens in Mensaje)
            {
                if (mens.Texto.Contains(texto))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ActualizarVistos(List<Mensaje> mensajes)
        {
            foreach(Mensaje men in mensajes)
            {
                if (men.EsMio == false && men.Visto == false)
                {
                    men.Visto = true;
                }
            }
        }

        public int IndexDe(Mensaje mensaje)
        {
            if (this.Mensaje.Contains(mensaje))
                return this.Mensaje.IndexOf(mensaje);
            else
            return -1;
        }

        public bool Refrescar()
        {
            int valor = Rng.Next(0, 101);

            if(valor >= 25)
            {
                return false;
            }

            foreach(Mensaje men in this.Mensaje)
            {
                if (men.EsMio == true && men.Visto == false)
                {
                    men.Visto = true;
                }    
            }

            if(this.Mensaje.Count() == 0)
            {
                this.Mensaje.Add(new Mensaje("Hola como estas", false, false));
            }

            else if (this.Mensaje.Last().EsMio)
            {
                this.Mensaje.Add(new Mensaje(Mensaje.Last().Texto.ToUpper(), false, false));
            }

            else if(Mensaje.Last().EsMio == false)
            {
                this.Mensaje.Add(new Mensaje("Respondeme plizz", false, false));
            }

            return true;
        }
    }
}
