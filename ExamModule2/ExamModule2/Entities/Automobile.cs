using System;
using System.Collections.Generic;
using System.Text;
using ExamModule2.Core.Entities.Abstracts;


namespace ExamModule2.Core.Entities
{
    /// <summary>
    /// Entità che definisce il veicolo automobile
    /// </summary>
    public class Automobile:MonitorableEntityBase
    {
        /// <summary>
        /// Id primario
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numero cavalli 
        /// </summary>
        public int NumCavalli { get; set; }

        /// <summary>
        /// Tipo motore 
        /// </summary>
        public bool IsDiesel { get; set; }

        /// <summary>
        /// Anno di immatricolazione
        /// </summary>

        public int AnnoImmatricolazione { get; set; }
    }
}
