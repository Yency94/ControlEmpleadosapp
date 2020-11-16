using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ControlEmpleadoServicios
{
    class Program
    {
        public static void Main(string[] args)
        {

            String NombreDelServidor = "localhost";
            if (File.Exists("PuertoActual.inf") == false)
            {
                using (StreamWriter _rw = File.CreateText("PuertoActual.inf"))
                {
                    _rw.WriteLine("8030");
                }
            }
            String _myport = "";
            using (StreamReader _rd = File.OpenText("PuertoActual.inf"))
            {
                _myport = _rd.ReadLine();
                String _lcNombreDelServidor = _rd.ReadLine();
                if (_lcNombreDelServidor.Length > 0)
                    NombreDelServidor = _lcNombreDelServidor;
            }
            String _info = "http://" + NombreDelServidor + ":" + _myport + "/servicioconsultas/";
            WebServer ws = new WebServer(SendResponse, _info);
            ws.Run();
            Console.WriteLine("Servicio de Cobros  QUICKFACT Presione Q para Parar Servicio");
            Console.WriteLine(_info);
            Int32 _seguircorriendo = 1;
            while (_seguircorriendo == 1)
            {

                ConsoleKeyInfo _key = Console.ReadKey();
                if (_key.Key == ConsoleKey.Q)
                {
                    ws.Stop();
                    _seguircorriendo = 0;
                }
            }

        }

        public static String MensajeError(String pMensaje)
        {
            return string.Format("<HTML><BODY>Problemas para Procesar:<b>{0}</b></BODY></HTML>", pMensaje);
        }
        // public String GenerarXML(String pEncabezado, String  

        public static string SendResponse(HttpListenerRequest request)
        {

            //if (request.HttpMethod == "OPTIONS")
            //{
            //	response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
            //	response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            //	response.AddHeader("Access-Control-Max-Age", "1728000");
            //}
            //response.AppendHeader("Access-Control-Allow-Origin", "*");
            var query = request.QueryString;


            Int32 variableTotal = query.Count;

            if (variableTotal > 0)
            {
                try
                {
                    DateTime fechacontrol = DateTime.Now;

                    // ?verificador=SyscomeAuth$280257&empresa=1&clave254
                    String _verifier = query["verif"];
                    if (_verifier != "SyscomeAuth$280257")
                    {
                        return MensajeError("ACCESO DENEGADO");
                    }
                    String _q = query["q"];
                    //Console.WriteLine("Ingresando peticion " + fechacontrol + _q);
                    if (_q == "A")
                    {
                        //  return VerificarClaveMy(query["usr"], query["pwd"]);
                    }
                    return MensajeError("Acceso Denegado");
                    // _q son las opciones que puede utilizar
                    // A -> 1. Verificacion de Usuario
                    // B -> 2. Obtener Planificacion Actual
                    // C -> 3. Actualizar Resultado
                    // D -> 4. Obtener Datos de Asociado
                }
                catch (Exception lcex)
                {
                    return MensajeError(lcex.Message + " " + lcex.StackTrace);
                }
            }
            else
            {
                return MensajeError("ACCESO DENEGADO");
            }
        }
        public static SqlParameter Parametro(String pnombre, DbType pTipo, object pValor)
        {
            SqlParameter _param = new SqlParameter(pnombre, pValor);
            _param.DbType = pTipo;
            return _param;
        }
        public static SqlConnection Conexion()
        {
            String _path = System.AppDomain.CurrentDomain.BaseDirectory;
            StreamReader _re = File.OpenText(_path + "Datos.inf");
            String _conexi = _re.ReadLine();
            _re.Close();
            SqlConnection _conexion = new SqlConnection();
            //OdbcConnection _conexion = new OdbcConnection("DSN=ContpreRemoto;");
            _conexion.ConnectionString = _conexi;

            return _conexion;
        }
        public static SqlConnection ConexionQuickfact()
        {
            String _path = System.AppDomain.CurrentDomain.BaseDirectory;
            StreamReader _re = File.OpenText(_path + "quickfact.inf");
            String _conexi = _re.ReadLine();
            _re.Close();
            SqlConnection _conexion = new SqlConnection();
            //OdbcConnection _conexion = new OdbcConnection("DSN=ContpreRemoto;");
            _conexion.ConnectionString = _conexi;

            return _conexion;
        }
        public static IDbCommand Comando()
        {
            SqlCommand _commando = new SqlCommand();
            //OdbcCommand _commando = new OdbcCommand();
            return _commando;
        }

        public string InsertarAgendaDet(int IdAgenda, DateTime fecha, string nota, string rutanota, string Imagen, int programada, DateTime fechaprograma)
        {
            string _salida = "";

            SqlConnection _conn = ConexionQuickfact();
            IDbCommand _comm = Comando();
            _comm.Connection = _conn;
            _conn.Open();
            _comm.CommandText = "exec sp_AgregarAgendaDet @pIdAgenda, @pFecha, @pNota, @pRutaNota,@pRutaImagen, @pProgramar,@pFechaProgramacion";
            _comm.Parameters.Add(Parametro("@pIdAgenda", DbType.Int32, IdAgenda));
            _comm.Parameters.Add(Parametro("@pFecha", DbType.Date, fecha));
            _comm.Parameters.Add(Parametro("@pNota", DbType.String, nota));
            _comm.Parameters.Add(Parametro("@pRutaNota", DbType.String, rutanota));
            _comm.Parameters.Add(Parametro("@pRutaImagen", DbType.String, Imagen));
            _comm.Parameters.Add(Parametro("@pProgramar", DbType.String, programada));
            _comm.Parameters.Add(Parametro("@pFechaProgramacion", DbType.Date, fechaprograma));
            try { _salida = _comm.ExecuteScalar().ToString(); }
            catch { _salida = "0"; }
            //_comm.ExecuteNonQuery();
            _conn.Close();

            return _salida;
        }

        public string mostrarAgenda(DateTime Fecha, int Empleado)
        {
            string _salida = "";
            SqlConnection _conn = ConexionQuickfact();
            IDbCommand _comm = Comando();
            _comm.Connection = _conn;
            _conn.Open();
            _comm.CommandText = "exec sp_MostrarAgenda @Fecha, @Empleado";
            _comm.Parameters.Add(Parametro("@Fecha", DbType.Date, Fecha));
            _comm.Parameters.Add(Parametro("@Empleado", DbType.Int32, Empleado));
            IDataReader _reader = _comm.ExecuteReader();
            String _res = "";
            try
            {


                while (_reader.Read())
                {
                    _salida += _reader.GetString(0) + "|";
                    _salida += _reader.GetString(1) + "|";
                    _salida += _reader.GetDateTime(2).ToString("dd/MM/yyyy") + "|";
                    _salida += _reader.GetString(3) + "|";
                    _salida += _reader.GetInt32(4) + ";";

                }
            }
            catch { _salida = ""; }
            //_comm.ExecuteNonQuery();
            _conn.Close();



            return _salida;
        }
    }
}
