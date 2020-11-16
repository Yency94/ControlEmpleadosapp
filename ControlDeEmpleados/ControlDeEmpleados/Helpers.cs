using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlDeEmpleados
{
	public class Helpers
	{
		public static List<string> horas_consulta()
		{
			List<string> horas = new List<string>();

			horas.Add("7:30 am");
			for (int i = 8 ; i < 12 ; i++)
			{
				horas.Add(i + ":" + "00" + " am");
				horas.Add(i + ":" + "30" + " am");
			}
			horas.Add("12:00 pm");
			horas.Add("12:30 pm");

			for (int i = 1 ; i <= 5 ; i++)
			{
				horas.Add(i + ":" + "00" + " pm");
				horas.Add(i + ":" + "30" + " pm");
			}

			return horas;

		}

		//este metodo toma la hora en valor string (2:30 pm) y devuel un decimal (14.3)
		public static decimal Decimal(string hora)
		{
			switch (hora)//casos especiales
			{
				case "12:00 pm":
					return 12.00m;
				case "12:30 pm":
					return 12.30m;
			}
			bool morning = (hora.Contains("am")); //determinamos si es hora de dia o tarde
			decimal hora_dec;
			char[] quitar = { 'a', 'm', 'p' };//caracteres que quitaremos de la string

			hora = hora.Trim();//quitemoas espacios en blancos y caracteres
			hora = hora.Trim(quitar);

			hora = hora.Replace(':', '.');//cambiamos los dos puntos por el punto decimal

			hora_dec = decimal.Parse(hora);//hacemos la conversion

			if (!morning) //si es de tarde sumaremos 12 horas
			{
				hora_dec += 12;
			}

			return hora_dec;
		}

		//este metodo toma la hora en decimal (14.3) y devuelve la hora en formato string (2:30 pm)
		public static string Hora(decimal hora)
		{
			string tiempo = "am";

			if (hora >= 12 && hora < 13)
			{
				tiempo = "pm";
			}
			else if (hora >= 13)
			{
				tiempo = "pm";
				hora -= 12;
			}
			string hora_formateada = hora.ToString();

			hora_formateada = hora_formateada.Replace('.', ':');
			hora_formateada = hora_formateada + " " + tiempo;

			return hora_formateada;

		}

		public static string asociadoNombre(string codigo)
		{
			string asociado = "";
			using (var db = new ControlDeEmpleados.Models.QuickfatControlEmplEntities1())
			{
				//var codigoasoc = codigo.Substring(0, 7);
				//var modeltmp = db.maeasoc.FirstOrDefault(par => par.Codigo == codigoasoc);
				//obtenemos el nombre de la garantia para el modelo de la vista                
				// asociado = modeltmp.Nombre + " " + modeltmp.Apellido;
			}
			return asociado;
		}

		public static string numReferencia(string codigopre)
		{
			string numRef = "";
            using (var db = new ControlDeEmpleados.Models.QuickfatControlEmplEntities1())
			{
				//var modeltmp = db.JuridicoProcesoHead.FirstOrDefault(p => p.codigopre == codigopre);
				//numRef = modeltmp.NumReferencia;
				//obtenemos el nombre de la garantia para el modelo de la vista                                
			}
			return numRef;
		}
		public static int getAge(String dateString)
		{
			var today = DateTime.Now;
			var birthDate = DateTime.Parse(dateString);
			var age = today.Year - birthDate.Year;
			var m = today.Month - birthDate.Month;
			if (m < 0 || (m == 0 && today < birthDate))
			{
				age--;
			}
			return age;
		}

		public static String SetNombrePaso(int? PasoValor)
		{
			string NombrePasoF = " ";
			switch (PasoValor)
			{
				case 1: { NombrePasoF = "Presentacion de Demanda"; }
					break;
				case 2: { NombrePasoF = "Admision de Demanda"; }
					break;
				case 3: { NombrePasoF = "Emplazamiento"; }
					break;
				case 4: { NombrePasoF = "Embargo"; }
					break;
				case 5: { NombrePasoF = "Sentencia de Demanda"; }
					break;
				case 6: { NombrePasoF = "Ejecucion de Sentencia"; }
					break;
				case 7: { NombrePasoF = "Liquidacion"; }
					break;
				case 8: { NombrePasoF = "Finalizar el Proceso"; }
					break;
				default: { NombrePasoF = ""; }
					break;
			}
			return NombrePasoF;
		}//Fin Funcion


		//funcion para convertir 2 valores enteros a decimal
		//usada en Asociados->Models-mv_Asociado para convertir años y meses trabajados a valor decimal   
		public static decimal joinDecimal(int parteEntera, int parteDecimal)
		{
			decimal res = 0;
			var decimalString = parteDecimal.ToString();
			if (decimalString.Length == 1)
			{
				decimalString = "0" + decimalString;
			}
			string numero = parteEntera.ToString() + "." + decimalString;
			try
			{
				res = decimal.Parse(numero);
				return res;
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		//funcion reciprocra de joinDecimal
		public static void splitDecimal(ref int parteEntera, ref int parteDecimal, decimal? source)
		{
			if (source == null)
			{
				parteEntera = 0;
				parteDecimal = 0;
				return;
			}
			string numero = source.ToString();
			var partes = numero.Split(new char[] { ',', '.' });
			parteEntera = (int)source;
			parteDecimal = int.Parse(partes[1]);

		}
		public static List<ControlDeEmpleados.Models.RegistroOpciones> GetOpcMenu(string name)
		{
            List<ControlDeEmpleados.Models.RegistroOpciones> ListaOpc = new List<ControlDeEmpleados.Models.RegistroOpciones>();
            using (ControlDeEmpleados.Models.QuickfatControlEmplEntities1 context = new ControlDeEmpleados.Models.QuickfatControlEmplEntities1())
			{
				//Extraemos la lista de opciones que tiene acceso el usuario por rol asignado
				var O = from u in context.Users
						//join r in context.Roles on u.Id equals r.Users.FirstOrDefault(par => par.UserName == name).Id
						join r in context.Roles on u.Id equals r.Users.FirstOrDefault(par => par.UserName == name).Id
						select new { r.RegistroOpciones }.RegistroOpciones.ToList();
				//Se iterea y se coloca todo en una sola lista

				foreach (var list in O)
				{
					ListaOpc.AddRange(list);
				}
				ListaOpc = ListaOpc.Distinct().
					Where(par => par.Activado != false)
					.OrderBy(b => b.Jerarquia)
					.ThenBy(b => b.Nombre)
					.ToList();
			}
			return ListaOpc;
		}
	}
}
#region
public class Meses
{
	public int codigo { get; set; }
	public string nombre { get; set; }
}
#endregion