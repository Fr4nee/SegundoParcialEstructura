using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    class Mensaje
    {
        public DateTime FechaHora;
        public string Texto;
        public bool EsMio;
        public bool Visto;
        public Mensaje MensajeRespondido = null;

        public Mensaje(string mensaje, bool esMio, bool visto)
        {
            this.FechaHora = DateTime.Now;
            this.Texto = mensaje;
            this.EsMio = esMio;
            this.Visto = visto;
        }
    }
}
