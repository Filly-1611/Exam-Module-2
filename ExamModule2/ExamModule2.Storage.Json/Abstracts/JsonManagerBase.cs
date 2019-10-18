using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExamModule2.Core.Entities.Interfaces;
using ExamModule2.Core.Managers.Interfaces;
using Newtonsoft.Json;

namespace ExamModule2.Storage.Json.Abstracts
{
    /// <summary>
    /// Classe base per tutti i manager che usano json
    /// </summary>
    public abstract class JsonManagerBase<TEntity> : IManager<TEntity>
        where TEntity : class, IEntity
    {
        public void Aggiorna(TEntity entityDaModificare)
        {
            //Validazione dell'input
            if (entityDaModificare == null)
                throw new ArgumentNullException(nameof(entityDaModificare));

            //Se non ho "Id" eccezione
            if (entityDaModificare.Id <= 0)
                throw new InvalidOperationException("Attenzione! L'oggetto " +
                    $"non ha il campo 'Id' valorizzato! Prima crearlo!");

            //Carico tutti in memoria
            IList<TEntity> tuttiIDati = Carica();

            //Scorro elenco generi esistenti
            foreach (var currentGenereInDatabase in tuttiIDati)
            {
                //Se l'id non corrisponde, continuo alla prossima iterazione
                if (currentGenereInDatabase.Id != entityDaModificare.Id)
                    continue;

                //Rimappo tutti i valori specifici sull'oggetto già 
                //presente sulla lista caricata dal database
                RemapNuoviValoriSuEntityInLista(entityDaModificare, currentGenereInDatabase);

                //Eseguo il cast "leggero"
                IMonitorableEntity boxedExistingEntity = currentGenereInDatabase as IMonitorableEntity;
                IMonitorableEntity boxedChangedEntity = entityDaModificare as IMonitorableEntity;

                //Se il cast è andato a buon fine
                if (boxedExistingEntity != null && boxedChangedEntity != null)
                {
                    //Cambio i valori dell'oggetto esistente sui campi comuni tra le entità                
                    boxedExistingEntity.Timestamp = boxedChangedEntity.Timestamp;
                    boxedExistingEntity.UtenteCreatore = boxedChangedEntity.UtenteCreatore;
                }
            }

            //Arrivato qui abbiamo la lista dati perfettamente aggiornata
            Salva(tuttiIDati);
        }

        protected abstract void RemapNuoviValoriSuEntityInLista(TEntity targetEntity, TEntity sourceEntity);

        public void Cancella(TEntity entityDaCancellare)
        {
            //Validazione dell'input
            if (entityDaCancellare == null)
                throw new ArgumentNullException(nameof(entityDaCancellare));

            //Se non ho "Id" eccezione
            if (entityDaCancellare.Id <= 0)
                throw new InvalidOperationException("Attenzione! L'oggetto " +
                    $"non ha il campo 'Id' valorizzato! Prima crearlo!");

            //Carico elementi da database
            var tutti = Carica();

            //Variabile per elemento da cancellare
            TEntity entityInListDaCancellare = null;

            //Scorro elementi esistenti
            foreach (var currentEntity in tutti)
            {
                //Se l'id non corrisponde, passa al prossimo
                if (currentEntity.Id != entityDaCancellare.Id)
                    continue;

                //Se arrivo qui, ho trovato l'elemento
                entityInListDaCancellare = currentEntity;
                break;
            }

            //Rimuovo da lista
            tutti.Remove(entityInListDaCancellare);

            //Riscrivo la lista sul database
            Salva(tutti);
        }

        public IList<TEntity> Carica()
        {
            //1) Percorso del file che contiene il json
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons");
            var path = Path.Combine(basePath, $"{typeof(TEntity).Name}.json");

            //Se il file non esiste, ritorno lista vuota
            if (!File.Exists(path))
                return new List<TEntity>();

            //2) Lettura di tutto il file e non delle singole righe
            string json = File.ReadAllText(path);

            //3) De-serializzazione della stringa in oggetti strutturati
            List<TEntity> dati = JsonConvert.DeserializeObject<List<TEntity>>(json);
            return dati;
        }

        public void Crea(TEntity entityDaCreare)
        {
            // Validazione dell'input
            if (entityDaCreare == null)
                throw new ArgumentNullException(nameof(entityDaCreare));

            //Se ho già un "Id", eccezione
            if (entityDaCreare.Id > 0)
                throw new InvalidOperationException("Attenzione! L'oggetto " +
                    $"ha già il campo 'Id' impostato al valore {entityDaCreare.Id}!");

            //Contiamo quanti record ci sono nel database esistente
            //(ci serve per sapere quale "Id" dare al nuovo elemento
            //=> Carico tutti gli elementi in archivio
            IList<TEntity> tutti = Carica();
            var count = tutti.Count;

            //Prossimo "Id" => count + 1
            var prossimoId = count + 1;

            //Assegnazione Id al nuovo elemento
            entityDaCreare.Id = prossimoId;

            //ATTENZIONE! Se questo oggetto implementa IMonitorableEntity
            //allora voglio che venga impostato il campo "TimeStamp"
            if (entityDaCreare is IMonitorableEntity boxedEntity)
                boxedEntity.Timestamp = DateTime.Now;

            //Aggiungo nuovo elemento a lista esistente
            tutti.Add(entityDaCreare);

            //Salva tutta la lista insieme
            Salva(tutti);
        }

        private void Salva(IList<TEntity> allData)
        {
            //Validazione input
            if (allData == null)
                throw new ArgumentNullException(nameof(allData));

            //1) Percorso del file che contiene il json
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons");
            var path = Path.Combine(basePath, $"{typeof(TEntity).Name}.json");

            //Serializzazione della lista in JSON
            string json = JsonConvert.SerializeObject(allData, Formatting.Indented);

            //Scrivo tutto il json nel file target
            File.WriteAllText(path, json);
        }
    }
}




