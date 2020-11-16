using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syscome.ControlEmpleados.Entidades;
using Syscome.ControlEmpleados.Datos;

namespace Syscome.ControlEmpleados.BL
{
    public class BLmaempl
    {
        public List<maEmpl> obtenerEmpleados(String pConn)
        {
            dbmaempl db = new dbmaempl(pConn);
            return db.obtenerEmpleados();
        }

        public void guardarEmpleado(maEmpl _m, String pConn)
        {
            dbmaempl db = new dbmaempl(pConn);
            db.guardarEmpleado(_m);
        }

        public void actualizarEmpleado(maEmpl _m, String pConn)
        {
            dbmaempl db = new dbmaempl(pConn);
            db.editarEmpleado(_m);
        }
    }
}
