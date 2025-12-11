using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA_SIFCOS.Entities.Auditoria;


namespace DA_SIFCOS.Entities.CDDResponse
{
    public class CDDResponseAuditoriaGeneral : CDDResponse
    {
        /// <summary>
        /// Listado de auditoria general sobre un documento digitalizado.
        /// </summary>
        public List<AuditoriaGeneral> Lista_Auditoria_General { get; set; }

        public CDDResponseAuditoriaGeneral()
        {
            Lista_Auditoria_General = new List<AuditoriaGeneral>();
        }
    }
}