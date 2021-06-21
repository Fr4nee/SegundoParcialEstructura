using ConsoleApp_p2.Controller;
using ConsoleApp_p2.Modelo;
using System;
using System.Collections.Generic;
using System.Threading;


namespace ConsoleApp_p2
{
    class Program
    {
        static void Main(string[] args)
        {
            MessengerController messengerController = new MessengerController();
            messengerController.Funcionar();

            Console.ReadLine();
        }
    }
}
