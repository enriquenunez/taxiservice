using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taxiservice.Models
{
	public class Servicio
	{
		public int IDServicio { get; set; }
		public int IDCliente { get; set; }
		public int IDConductor { get; set; }
		public int IDAutomovil { get; set; }
		public string Fecha { get; set; }
		public string FechaInicio { get; set; }
		public string FechaFin { get; set; }
		public string Origen { get; set; }
		public string Destino { get; set; }
		public decimal Distancia { get; set; }
		public decimal Ctmr { get; set; }
		public decimal Cmer { get; set; }
		public decimal Costo { get; set; }
	}
}