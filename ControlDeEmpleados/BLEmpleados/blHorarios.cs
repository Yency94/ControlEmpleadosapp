using Syscome.ControlEmpleados.Datos;
using Syscome.ControlEmpleados.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syscome.ControlEmpleados.BL
{
 public class blHorarios
    {
        public List<Horarios> ObtenerHorarios(String pConn)
        {
            dbHorarios db = new dbHorarios(pConn);
            return db.ObtenerHorarios();
        }

        public void AgregarHorario(Horarios _i, String pConn)
        {
            dbHorarios db = new dbHorarios(pConn);
            db.AgregarHorarios(_i);
        }

        public void ActualizarHorario(Horarios _i,String pConn)
        {
            dbHorarios db = new dbHorarios(pConn);
            db.ActualizarHorarios(_i);
        }
    }
}
