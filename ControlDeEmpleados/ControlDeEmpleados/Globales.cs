using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlDeEmpleados
{
    public class Globales
    {
        public static string Empresa { get; set; }
        public static string SucursalNombre { get; set; }
        public static string idSucursal { get; set; }
       
        public static string LogoPath { get; set; }
        public static bool? ReporteNombre { get; set; }
        public static string RolWebMaster { get; set; }
        public static Double TimeOut { get; set; }
        public static decimal? Factor { get; set; }
        public static bool BlockSession { get; set; }
        public static string NumerarioCajero { get; set; }
        public static string LogoPathFirmaRecibos { get; set; }
        public static string NombreCorto { get; set; }
       
        public static string NombreUsuario { get; set; }
    }
}