using System;

namespace ConsoleApp_p2.Modelo
{
    class Contacto
    {
        public string Nombre { get; set; }
        public string Info { get; set; }

        public Contacto(string nombre, string info)
        {
            this.Nombre = nombre;
            this.Info = info;

            if (nombre == null)
            {
                throw new ArgumentException("Nombre");
            }
            if (info == null)
            {
                throw new ArgumentException("Info");
            }
        }


    }
}
