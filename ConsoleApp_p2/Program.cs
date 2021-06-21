using System;
using ConsoleApp_p2.Controller;

namespace ConsoleApp_p2
{
    class Program
    {
        static void Main(string[] args)
        {
            Controlador MSNapp = new Controlador();
            MSNapp.Funcionar();

            Console.ReadLine();
        }
    }
}
