using ConsoleApp_p2.Modelo;
using ConsoleApp_p2.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Controller
{
    class MessengerController
    {
        private MSNMessenger Modelo = new MSNMessenger();
        private MSNMessengerView Vista = new MSNMessengerView();

        public void Funcionar()
        {
            bool fin = false;
            do
            {
                switch (Vista.MostarMenuPrincipal())
                {
                    case MenuPrincipalOpt.NuevoContacto:
                        Modelo.Refrescar();
                        NuevoContacto();
                        break;
                    case MenuPrincipalOpt.NuevoChat:
                        Modelo.Refrescar();
                        NuevoChat();
                        break;
                    case MenuPrincipalOpt.VerChats:
                        Modelo.Refrescar();
                        VerChats();
                        break;
                    case MenuPrincipalOpt.BuscarChats:
                        Modelo.Refrescar();
                        BuscarChats();
                        break;
                }
            } while (fin == false);
        }

        public void NuevoContacto()
        {
            ContactoViewModel contacto = Vista.MostrarPantallaCrearContacto();

            if (contacto != null)
            {
                Contacto aux = new Contacto(contacto.Nombre, contacto.Info);

                if (Modelo.AgregarContacto(aux) == false)
                {
                    Console.WriteLine("Error, el contacto ya existe.");
                    Console.ReadKey();
                }
                else
                {
                    Modelo.AgregarContacto(aux);
                }
            }
        }

        public void NuevoChat()
        {
            int aux;

            List<ContactoViewModel> contactos = new List<ContactoViewModel>();

            foreach (Contacto con in Modelo.Contacto)
            {
                contactos.Add(new ContactoViewModel() { Nombre = con.Nombre, Info = con.Info });
            }

            aux = Vista.MostrarPantallaSeleccionDeContacto(contactos);

            if (aux != -1)
            {
                new Modelo.Chat(Modelo.Contacto[aux]);
                Chatear(Modelo.Chat[aux]);               
            }
        }

        public void BuscarChats()
        {
            string term = Vista.MostrarPantallaDeBusqueda();
      
            if (term == null)
            {
                return;
            }
            else
            {
                VerChats(Modelo.BuscarChats(term));
            }
        }

        public void Chatear(Chat chat)
        {
            Chat.ActualizarVistos(chat.Mensaje);
            MensajeViewModel nuevoMensajeVM = new MensajeViewModel();

            ChatViewModel cmv = new ChatViewModel()
            {
                Nombre = chat.Contacto.Nombre,
                Info = chat.Contacto.Info,
                Mensajes = new List<MensajeViewModel>()
            };

                foreach (Mensaje msj in chat.Mensaje)
                {
                    cmv.Mensajes.Add(new MensajeViewModel()
                    {
                        EsMio = msj.EsMio,
                        FechaHora = msj.FechaHora,
                        Texto = msj.Texto,
                        Visto = msj.Visto,
                        MensajeCitadoIndex = chat.IndexDe(msj.MensajeRespondido)
                    }); 
                }
            nuevoMensajeVM = Vista.MostrarPantallaDeChat(cmv);

            while (nuevoMensajeVM != null)
            {
                Mensaje mensaje = new Mensaje(nuevoMensajeVM.Texto, nuevoMensajeVM.EsMio, nuevoMensajeVM.Visto);

                chat.Enviar(mensaje);
                chat.ActualizarVistos(chat.mensaje);
                chat.Refrescar();

                

            }
        }

        public void VerChats(List<Chat> chats)
        {
            List<ChatViewModel> listaDeChats = new List<ChatViewModel>();

            for (int i = 0; i < chats.Count(); i++)
            {
                listaDeChats.Add(new ChatViewModel()
                {
                    Nombre = chats[i].Contacto.Nombre, 
                    Info = chats[i].Contacto.Info, 
                    Mensajes = new List<MensajeViewModel>()
                });
            }
        }

    }
}
