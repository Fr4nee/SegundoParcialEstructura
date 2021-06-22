using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_p2.Modelo;
using ConsoleApp_p2.Vista;

namespace ConsoleApp_p2.Controller
{
    class Controlador
    {
        private MSN_Messenger Modelo = new MSN_Messenger();
        private MSNMessengerView Vista = new MSNMessengerView();

        public void Funcionar()
        {
            bool x = false;
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
                        VerChats(Modelo.Chat);
                        break;
                    case MenuPrincipalOpt.BuscarChats:
                        Modelo.Refrescar();
                        BuscarChats();
                        break;
                }
            } while (x == false);
        }

        public void VerChats(List<Chat> chats)
        {
            List<ChatItemViewModel> listaChats = new List<ChatItemViewModel>();
            for(int i = 0; i < chats.Count(); i++)
            {
                listaChats.Add(new ChatItemViewModel(){ 
                    Nombre = chats[i].Contacto.Nombre, 
                    Info = chats[i].Contacto.Info, 
                    CantMsjsNuevos = chats[i].ContarNoLeidos(chats[i].Mensaje), 
                    UltimoMsj = chats.Last().ToString() 
                });
            }
            int ind = Vista.MostrarPantallaSeleccionDeChat(listaChats);
            if (ind != -1)
            {
                Chatear(chats[ind]);
            }
        }

        public void NuevoContacto()
        {
            ContactoViewModel contacto = Vista.MostrarPantallaCrearContacto();
            if (contacto != null)
            {
                Contacto aux = new Contacto(contacto.Nombre, contacto.Info);
                if (Modelo.AgregarContacto(aux) == false)
                {
                    Console.WriteLine("¡ERROR! El contacto que quiere ingresar ya existe.");
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
            List<ContactoViewModel> contactos = new List<ContactoViewModel>();

            foreach(Contacto con in Modelo.Contacto)
            {
                contactos.Add(new ContactoViewModel() { 
                    Nombre = con.Nombre, 
                    Info = con.Info 
                });
            }

            int aux = Vista.MostrarPantallaSeleccionDeContacto(contactos);
            if(aux != -1)
            {
                Modelo.AgregarChat(Modelo.Contacto[aux]);
                Chatear(Modelo.Chat[aux]);
            }
        }

        public void Chatear(Chat chat)
        {
            Chat.ActualizarVistos(chat.Mensaje);
            MensajeViewModel msjvm = new MensajeViewModel();

            ChatViewModel chtvm = new ChatViewModel()
            {
                Nombre = chat.Contacto.Nombre,
                Info = chat.Contacto.Info,
                Mensajes = new List<MensajeViewModel>()
            };

            foreach(Mensaje mensaje in chat.Mensaje)
            {
                chtvm.Mensajes.Add(new MensajeViewModel()
                {
                    FechaHora = mensaje.FechaHora,
                    Visto = mensaje.Visto,
                    EsMio = mensaje.EsMio,
                    Texto = mensaje.Texto,
                    MensajeCitadoIndex = chat.IndexDe(mensaje.MensajeRespondido)
                });
            };

            msjvm = Vista.MostrarPantallaDeChat(chtvm);

            while(msjvm != null)
            {
                Mensaje mensaje = new Mensaje (
                    msjvm.Texto,
                    msjvm.EsMio,
                    msjvm.Visto
                );

                chat.Enviar(mensaje);
                Chat.ActualizarVistos(chat.Mensaje);
                chat.Refrescar();

                chtvm.Nombre = chat.Contacto.Nombre;
                chtvm.Info = chat.Contacto.Info;
                chtvm.Mensajes.Clear();

                for(int i = 0; i < chat.Mensaje.Count; i++)
                {
                    chtvm.Mensajes.Add(new MensajeViewModel()
                    {
                        EsMio = chat.Mensaje[i].EsMio,
                        FechaHora = chat.Mensaje[i].FechaHora,
                        Texto = chat.Mensaje[i].Texto,
                        Visto = chat.Mensaje[i].Visto,
                        MensajeCitadoIndex = 0
                    });
                }
                msjvm = Vista.MostrarPantallaDeChat(chtvm);
            }
        }
        public void BuscarChats()
        {
            string term = Vista.MostrarPantallaDeBusqueda();
            if (term == null)
                return;
            else
            {
                VerChats(Modelo.BuscarChats(term));
            }
        }

    }
}
