using ExamModule2.Core.Entities;
using ExamModule2.Storage.Json.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamModule2.Storage.Json
{

    public class JsonAutomobileManager : JsonManagerBase<Automobile>
    {
        protected override void RemapNuoviValoriSuEntityInLista(
            Automobile entitySorgente, Automobile entityDestinazione)
        {
            entityDestinazione.Id = entitySorgente.Id;
            entityDestinazione.Modello = entitySorgente.Modello;
            entityDestinazione.Marca = entitySorgente.Marca;
            entityDestinazione.NumCavalli = entitySorgente.NumCavalli;
            entityDestinazione.IsDiesel = entitySorgente.IsDiesel;
            entityDestinazione.AnnoImmatricolazione = entitySorgente.AnnoImmatricolazione;
            
        }
    }
}
