using System;
using System.Collections.Generic;
using System.Text;
using ExamModule2.Core.Entities.Interfaces;

namespace ExamModule2.Core.Managers.Interfaces
{
    /// <summary>
    /// Interfaccia per tutti gli oggetti che si occupano
    /// di fare CRUD su uno specifico sistema di storage
    /// /(es. filesystem, database SQL, file xml, ecc)
    /// </summary>
    /// <typeparam name="TEntity">Tipo di entità trattata</typeparam>
    public interface IManager<TEntity>
        where TEntity : class, IEntity
    {
        
        /// <summary>
        /// Crea l'entità passata sullo storage
        /// </summary>
        /// <param name="entityDaCreare">Entità da creare</param>
        void Crea(TEntity entityDaCreare);

        /// <summary>
        /// Aggiorna l'entità passata nello storage
        /// </summary>
        /// <param name="entityDaModificare">Entità da aggiornare</param>
        void Aggiorna(TEntity entityDaModificare);

        /// <summary>
        /// Cancella l'entità specificata dallo storage
        /// </summary>
        /// <param name="entityDaCancellare">Entità da cancellare</param>
        void Cancella(TEntity entityDaCancellare);

        /// <summary>
        /// Carica tutte le entità nello storage
        /// </summary>
        /// <returns>Ritorna la lista di entità presenti</returns>
        IList<TEntity> Carica();
    }

}







    
    
