using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    class MSNMessenger
    {
        public List<Contacto> Contacto = new List<Contacto>();
        public List<Chat> Chat = new List<Chat>();

        public bool AgregarContacto(Contacto nuevoContacto)
        {
            bool agregaContacto = true;
            for(int i = 0; i < Contacto.Count; i++)
            {
                if (nuevoContacto.Nombre == Contacto[i].Nombre && nuevoContacto.Info == Contacto[i].Info)
                {
                    agregaContacto = false;
                    break;
                }
            }
            if(agregaContacto == true)
            {
                Contacto.Add(nuevoContacto);
            }
            return agregaContacto;
        }

        public Chat AgregarChat(Contacto contacto)
        {
            foreach(Chat cha in Chat)
            {
                if (cha.Contacto == contacto)
                {
                    return cha;
                }       
            }
            Chat.Add(new Chat(contacto));
            return Chat.Last();
        }

        public void Refrescar()
        {
            foreach (Chat cha in Chat)
            {
                cha.Refrescar();
            }  
        }

        public List<Chat> BuscarChats(string terminoABuscar)
        {
            List<Chat> auxList = new List<Chat>();
            foreach(Chat cha in Chat)
            {
                foreach(Mensaje men in cha.Mensaje)
                {
                    if (men.Texto.Contains(terminoABuscar))
                    {
                        auxList.Add(cha);
                        break;
                    }
                }
            }
            return auxList;
        }
    }
}
