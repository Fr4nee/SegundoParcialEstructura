using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp_p2.Controller;

namespace ConsoleApp_p2.Modelo
{
	class MSN_Messenger
	{
		public List<Contacto> Contacto = new List<Contacto>();
		public List<Chat> Chat = new List<Chat>();

		public bool AgregarContacto(Contacto newCont)
		{
			bool addCont = true;

			for (int i = 0; i < Contacto.Count(); i++)
			{
				if (newCont.Nombre == Contacto[i].Nombre && newCont.Info == Contacto[i].Info)
				{
					addCont = false;
					break;
				}
			}
			if (addCont == true)
			{
				Contacto.Add(newCont);
			}
			return addCont;
		}

		public Chat AgregarChat(Contacto cont)
		{
			foreach (Chat chat in Chat)
			{
				if (chat.Contacto == cont)
				{
					return chat;
				}       
			}
			Chat.Add(new Chat(cont));
			return Chat.Last();
		}

		public void Refrescar()
		{
			foreach (Chat item in Chat)
			{
				item.Refrescar();
			}  
		}

		public List<Chat> BuscarChats(string search)
		{
			List<Chat> aux = new List<Chat>();

			foreach (Chat chat in Chat)
			{
				foreach (Mensaje msj in chat.Mensaje)
				{
					if (msj.Texto.Contains(search))
					{
						aux.Add(chat);
						break;
					}
				}
			}
			return aux;
		}
	}
}
