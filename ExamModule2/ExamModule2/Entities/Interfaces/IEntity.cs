using System;
using System.Collections.Generic;
using System.Text;

namespace ExamModule2.Core.Entities.Interfaces

{
    /// <summary>
    /// Interfaccia per entità con 
    /// id primario intero progressivo
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Id primario
        /// </summary>
        int Id { get; set; }
    }
}



