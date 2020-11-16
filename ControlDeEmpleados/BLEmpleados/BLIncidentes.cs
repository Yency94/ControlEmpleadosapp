using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syscome.ControlEmpleados.Entidades;
using Syscome.ControlEmpleados.Datos;
namespace Syscome.ControlEmpleados.BL
{
   public class BLIncidentes
    {
      
       public List<Incidentes> ObtenerIncidentes(int pCodigo, int pTipoProc, String pConn)
       {
           dbIncidentes db = new dbIncidentes(pConn);
           return db.ObtenerIncidentes(pCodigo, pTipoProc);
       }

       public void AgregarIncidente(Incidentes _i, String pConn)
       {
            dbIncidentes db = new dbIncidentes(pConn);
            db.AgregarIncidente(_i);
       }

       public void ActualizarIncidente(int id, string descripcion, DateTime fecha, String pConn)
       {
           dbIncidentes db = new dbIncidentes(pConn);
           db.ActualizarIncidente(id, descripcion, fecha);
       }
    }
}
