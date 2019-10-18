using System;
using ExamModule2.Core.BusinessLayers;
using ExamModule2.Core.Entities;
using ExamModule2.Core.Managers.Enum;
using ExamModule2.Core.Managers.Interfaces;
using ExamModule2.Core.Utils;
using ExamModule2.Storage.Json;

namespace ExamModule2.Terminal.Procedures
{
    public static class LaunchBusinessLayerMenuAuto
    {    
        public static void Summary()
{
    //Menu
    Console.WriteLine("***********************");
    Console.WriteLine("* Business Layer Menu *");
    Console.WriteLine("***********************");
    Console.WriteLine("*Carica dati automobile");

    //Recupero della selezione
    var selezione = ConsoleUtils.LeggiNumeroInteroDaConsole(1, 1);

    //Avvio della procedura
    switch (selezione)
    {
        //********************************************************
        case 1:
            CreaAutomobile();
            break;

        //********************************************************
        default:
            Console.WriteLine("Selezione non valida");
            break;
    }
}

private static void CreaAutomobile()
{
    //Richiedo all'utente il tipo di provider dati
    ConsoleUtils.WriteColor(ConsoleColor.Yellow, "Provider storage(Json):");
    string storageTypeAsString = ConsoleUtils.ReadLine<string>(e => e == "Json");
    StorageType storageType = Enum.Parse<StorageType>(storageTypeAsString);

    //Richiediamo i dati da console
    ConsoleUtils.WriteColor(ConsoleColor.Yellow, "Id:");
    int Id = ConsoleUtils.ReadLine<int>(a => a > 0);
    ConsoleUtils.WriteColor(ConsoleColor.Yellow, "Modello:");
    string Modello = ConsoleUtils.ReadLine<string>(t => !string.IsNullOrEmpty(t));
    ConsoleUtils.WriteColor(ConsoleColor.Yellow, "Marca:");
    string Marca = ConsoleUtils.ReadLine<string>(t => !string.IsNullOrEmpty(t));
    ConsoleUtils.WriteColor(ConsoleColor.Yellow, "NumeroTelaio:");
    int NumeroTelaio = ConsoleUtils.ReadLine<int>(a => a > 0);
    ConsoleUtils.WriteColor(ConsoleColor.Yellow, "E' elettrica:");
    //int Id = ConsoleUtils.ReadLine<int>(a => a > 0);


    IManager<Automobile> AutomobileManager;


    //Switch sul tipo di storage
    switch (storageType)
    {
        case StorageType.Json:
            AutomobileManager = new JsonAutomobileManager();

            break;
        default:
            throw new NotSupportedException($"Il provider {storageType} non è supportato");
    }

    //Istanzio il business layer (che il cervello della 
    //nostra applicazione)
    MainBusinessLayer layer = new MainBusinessLayer(AutomobileManager);

    //Avvio la funzione di creazione
    string[] messaggiDiErrore = layer.CreaAutoSeNonEsiste(
        Id, Modello, Marca, NumCavalli, IsDiesel, Annoimmatricolazione);

    //Se non ho messaggi di errore, confermo
    if (messaggiDiErrore.Length == 0)
        ConsoleUtils.WriteColorLine(ConsoleColor.Green, "ok!");
    else
    {
        //Messaggio di errore generale
        ConsoleUtils.WriteColorLine(ConsoleColor.Yellow,
            "Attenzione! Ci sono errori nella creazione!");

        //Scorriamo gli errori e li mostriamo all'utente
        foreach (var currentMessage in messaggiDiErrore)
            ConsoleUtils.WriteColorLine(ConsoleColor.Yellow, currentMessage);
    }

}
    }
}