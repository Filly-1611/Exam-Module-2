using System;
using System.Collections.Generic;
using System.Text;
using ExamModule2.Core.Entities.Abstracts;

namespace ExamModule2.Core.Entities
{
    /// <summary>
    /// Entità che definisce il veicolo bicicletta
    /// </summary>
    public class Bicicletta:MonitorableEntityBase 
    {
        /// <summary>
        /// Id primario
        /// </summary>
        /// 
        public int Id { get; set; }
        /// <summary>
        /// Nome telaio
        /// </summary>
        /// 
        public int NumeroTelaio { get; set; }

        /// <summary>
        /// E' elettrica?
        /// </summary>

        public bool IsElettrica { get; set; }
    }
}
