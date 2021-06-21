using System;

namespace ConsoleApp_p2.Modelo
{
    class Mensaje
    {
        public DateTime FechaHora { get; set; }
        public string Texto { get; set; }
        public bool EsMio { get; set; }
        public bool Visto { get; set; }
        public Mensaje MensajeRespondido { get; set; }

        public Mensaje(string mensaje, bool esMio, bool visto)
        {
            this.FechaHora = DateTime.Now;
            this.Texto = mensaje;
            this.EsMio = esMio;
            this.Visto = visto;
        }
    }
}
