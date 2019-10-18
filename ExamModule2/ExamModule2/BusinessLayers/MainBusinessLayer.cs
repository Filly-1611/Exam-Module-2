using ExamModule2.Core.Entities;
using ExamModule2.Core.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamModule2.Core.BusinessLayers
{

        /// <summary>
        /// Classe che contiene il flusso funzionale di Galaxy
        /// </summary>
        public class MainBusinessLayer
        {
            private IManager<Automobile> _AutoManager;
            

            public MainBusinessLayer(IManager<Automobile> AutoMan)
            {
              
                _AutoManager = AutoMan;
            }
        }

    
    public string[] CreaAutoSeNonEsiste(
            int Id, string marca, string modello, int numCavalli,
            bool isDiesel, int annoImmatricolazione)
    {
        //1) Validazione degli input
        if (string.IsNullOrEmpty(marca))
            throw new ArgumentNullException(nameof(marca));
        if (string.IsNullOrEmpty(modello))
            throw new ArgumentNullException(nameof(modello));
        if (annoImmatricolazione == 0)
            throw new ArgumentOutOfRangeException(nameof(annoImmatricolazione));
        if (Id == 0)
            throw new ArgumentOutOfRangeException(nameof(Id));
        if (numCavalli == 0)
            throw new ArgumentOutOfRangeException(nameof(numCavalli));
        if (isDiesel == false)
            throw new ArgumentNullException(nameof(isDiesel));

        //Predisposizione messaggi di uscita
        IList<string> messaggi = new List<string>();

        //2)  Verifico che l'anno sia tra 1000 e oggi
        if (annoImmatricolazione < 1000 || annoImmatricolazione > DateTime.Now.Year)
        {
            //Aggiungo il messaggio di errore, ed esco
            messaggi.Add($"L'anno deve essere compreso tra 1000 e {DateTime.Now.Year}");
            return messaggi.ToArray();
        }

        

        //3) Verifico che il codice non sia già usato
        //Automobile AutoConStessoCodice = GetLibroByCodice(Id);
        //if (AutoConStessoCodice != null)
        //{
            //Aggiungo il messaggio di errore, ed esco
           // messaggi.Add($"Esiste già un'automobile con il " +
                //$"codice '{codice}' (ha l'id {libroConStessoCodice.Id})");
            //return messaggi.ToArray();
        //}

        //5) Ricerco il genere in archivio
        //Genere existingGenere =
            //GetGenereByNome(nomeGenere)
            //?? CreateGenereWithName(nomeGenere);

        //7) Creo l'oggetto con tutti i dati
        Automobile nuovaAuto = new Automobile
        {
            Id = Id,
            Modello = modello,
            Marca = marca,
            NumCavalli = numCavalli,
            IsDiesel = isDiesel,
            AnnoImmatricolazione = annoImmatricolazione,
            
        };

        //Aggiungo l'automobile
        _AutomobileManager.Crea(nuovaAuto);

        //8) Ritorno in uscita le validazioni (vuote se non ho errori)
        return messaggi.ToArray();
    }
}
