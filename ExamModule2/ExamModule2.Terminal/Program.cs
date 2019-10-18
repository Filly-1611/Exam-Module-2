using System;
using ExamModule2.Core.Utils;
using ExamModule2.Terminal;
using ExamModule2.Terminal.Procedures;

namespace ExamModule2.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            //Visualizzo menu e richiedo selezione
            Console.WriteLine("*******************************");
            Console.WriteLine("            * MENU *");
            Console.WriteLine("*******************************");
            Console.WriteLine("Selezionare il catalogo al quale vuoi accedere (A per catalogo biciclette, B per catalogo automobili:")
            string[] selezione = ConsoleUtils.LeggiLetteraDaConsole("A", "B");

            //Selezione dell'opzione
            switch (selezione)
            {
                case "A":
                    LaunchBusinessLayerMenuAuto.Summary();
                    break;
                case "B":
                    LaunchBusinessLayerMenuAuto.Summary();
                    break;
                default:
                    Console.WriteLine("Opzione non valida!");
                    break;
            }

            //Richiedo conferma di uscita
            ConsoleUtils.ConfermaUscita();
        }
    }
}
        
