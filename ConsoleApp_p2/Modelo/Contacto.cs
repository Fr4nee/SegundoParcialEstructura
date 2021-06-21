using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    class Contacto
    {
        public string Nombre { get; set; }
        public string Info { get; set; }

        public Contacto(string nombre, string info)
        {
            if (nombre == null) throw new ArgumentException("nombre");
            if (info == null) throw new ArgumentException("Info");

            this.Nombre = nombre;
            this.Info = info;
        }


    }
}
