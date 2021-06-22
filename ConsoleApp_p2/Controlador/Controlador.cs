using System;
using System.Collections.Generic;
using System.Linq;
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
                        VerChats(Modelo.Chat);
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
                    Console.WriteLine("¡ERROR! El contacto que quiere ingresar ya existe.");
                    Console.ReadKey();
                }
                else
                {
                    Modelo.AgregarContacto(aux);
                }
            }
        }

        public void Chatear(Chat chat)
        {
            Chat.ActualizarVistos(chat.Mensaje);

            MensajeViewModel msjvm = new MensajeViewModel();

            ChatViewModel chatviewmodel = new ChatViewModel()
            {
                Nombre = chat.Contacto.Nombre,
                Info = chat.Contacto.Info,
                Mensajes = new List<MensajeViewModel>()
            };

                foreach (Mensaje msj in chat.Mensaje)
                {
                    chatviewmodel.Mensajes.Add(new MensajeViewModel()
                    {
                        EsMio = msj.EsMio,
                        FechaHora = msj.FechaHora,
                        Texto = msj.Texto,
                        Visto = msj.Visto,
                        MensajeCitadoIndex = chat.IndexDe(msj.MensajeRespondido)
                    }); 
                }

            msjvm = Vista.MostrarPantallaDeChat(chatviewmodel);

            while (msjvm != null)
            {
                Mensaje mensaje = new Mensaje(msjvm.Texto, msjvm.EsMio, msjvm.Visto);

                chat.Enviar(mensaje);
                Chat.ActualizarVistos(chat.Mensaje);
                chat.Refrescar();

                chatviewmodel.Nombre = chat.Contacto.Nombre;
                chatviewmodel.Info = chat.Contacto.Info;

                chatviewmodel.Mensajes.Clear();

                for (int i = 0; i < chat.Mensaje.Count(); i++)
                {
                    chatviewmodel.Mensajes.Add(new MensajeViewModel()
                    {
                        Texto = chat.Mensaje[i].Texto,
                        EsMio = chat.Mensaje[i].EsMio,
                        FechaHora = chat.Mensaje[i].FechaHora,
                        Visto = chat.Mensaje[i].Visto,
                    });
                }
                msjvm = Vista.MostrarPantallaDeChat(chatviewmodel);
            }
        }






        public void VerChats(List<Chat> chats)
        {
            int aux;

            List<ChatViewModel> listaChats = new List<ChatViewModel>();

            List<ChatItemViewModel> itemListaChats = new List<ChatItemViewModel>();

            for (int i = 0; i < chats.Count; i++)
            {
                listaChats.Add(new ChatViewModel()
                {
                    Nombre = chats[i].Contacto.Nombre, 
                    Info = chats[i].Contacto.Info, 
                    Mensajes = new List<MensajeViewModel>()
                });

                aux = Vista.MostrarPantallaSeleccionDeChat(itemListaChats);

                //if (aux != -1)
                //{
                //    Chatear(chats);
                //}
            }

        }









        public void NuevoChat()
        {
            int aux;

            List<ContactoViewModel> contactos = new List<ContactoViewModel>();

            foreach (Contacto contacto in Modelo.Contacto)
            {
                contactos.Add(new ContactoViewModel() { Nombre = contacto.Nombre, Info = contacto.Info });
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
            List<ChatItemViewModel> itemListaChats = new List<ChatItemViewModel>();

            string term = Vista.MostrarPantallaDeBusqueda();

            if (term == null)
            {
                return;
            }
            else
            {
                Modelo.BuscarChats(term);
                //VerChats(itemListaChats(term));
            }
        }

    }
}
