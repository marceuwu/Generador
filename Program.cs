//DIAZ GUERRERO MARCELA
using System;
using System.IO;

namespace Generador
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            using (Lenguaje a = new Lenguaje("c2.gram"))
            {
                try
                {
                    a.Gramatica();
                    /*while(!a.FinArchivo())
                    {
                        a.NextToken();
                    }*/
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.ReadLine();
        }
    }
}