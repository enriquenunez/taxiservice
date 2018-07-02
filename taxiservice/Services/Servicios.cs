using System;
using System.Collections.Generic;
using taxiservice.Models;
using Npgsql;
using NpgsqlTypes;
using System.Data;
namespace taxiservice.Services
{
	public class Servicios
	{
		private Conexion m_conn;
		private NpgsqlDataReader m_data;
		private List<Servicio> m_lista;

		public Servicios()
		{
			m_conn = new Conexion();
			m_data = null;
			m_lista = new List<Servicio>();
		}
		/// <summary>
		/// Crea un Objeto Servicio
		/// </summary>
		/// <param name="lData">Es un objeto NpgsqlDataReader que contiene datos para crear el objeto</param>
		/// <returns>Devuelve un Objeto de tipo Servicio</returns>
		private Servicio crearObjeto(NpgsqlDataReader lData)
		{
			Servicio lObjeto = null;
			if (lData != null)
			{
				lObjeto = new Servicio();
				lObjeto.IDCliente = Convert.ToInt32(lData["idservicio"]);
				lObjeto.IDConductor = Convert.ToInt32(lData["idconductor"]);
				lObjeto.IDAutomovil = Convert.ToInt32(lData["idautomovil"]);
				lObjeto.IDCliente = Convert.ToInt32(lData["idcliente"]);
				lObjeto.Fecha = Convert.ToString(lData["fecha"]);
				lObjeto.FechaInicio = Convert.ToString(lData["fecha_ini"]);
				lObjeto.FechaFin = Convert.ToString(lData["fecha_fin"]);
				lObjeto.Origen = Convert.ToString(lData["origen"]);
				lObjeto.Destino = Convert.ToString(lData["destino"]);
				lObjeto.Distancia = Convert.ToDecimal(lData["distancia"]);
				lObjeto.Ctmr = Convert.ToDecimal(lData["ctmr"]);
				lObjeto.Cmer = Convert.ToDecimal(lData["cmer"]);
				lObjeto.Costo = Convert.ToDecimal(lData["costo"]);
			}
			return lObjeto;
		}
		/// <summary>
		/// Devuelve un Objeto Servicio
		/// </summary>
		/// <param name="pID">Es el id que identifica a un registro en la tabla Servicio</param>
		/// <returns>Devuelve un Objeto de tipo Servicio</returns>
		public Servicio getConductor(int pID)
		{
			Servicio lObjeto = null;
			if (buscar("*", "where idconductor=" + pID))
			{
				lObjeto = crearObjeto(m_data);
			}
			return lObjeto;
		}
		/// <summary>
		/// Crea una lista de Conductores
		/// </summary>
		/// <returns>Devuelve una lista de conductores</returns>
		public List<Servicio> getData()
		{
			return m_lista;
		}
		/// <summary>
		/// Busca en la tabla Servicio segun los parametros especificados
		/// </summary>
		/// <param name="pCampos">Indica los campos a devolver cuando se ejecuta la búsqueda</param>
		/// <param name="pFiltro">Es el filtro que se aplicara al realizar la búsqueda</param>
		/// <returns></returns>
		public bool buscar(string pCampos, string pFiltro)
		{
			bool lRsp = false;

			try
			{
				if (m_conn.activo() == false) m_conn.abrir("");
				using (NpgsqlCommand lCmd = new NpgsqlCommand())
				{
					lCmd.CommandType = CommandType.Text;
					lCmd.CommandText = "select " + pCampos + " from vw_reg_conductor " + pFiltro;
					lCmd.Connection = m_conn.getConexion();
					m_data = lCmd.ExecuteReader();
					while (m_data.Read())
					{
						m_lista.Add(crearObjeto(m_data));
					}
					lRsp = (m_data != null);
				}
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
				throw;
			}
			finally
			{
				if (m_conn.activo()) m_conn.cerrar();
			}
			return lRsp;
		}
		/// <summary>
		/// Guarda/Actualiza un registro de Servicio
		/// </summary>
		/// <param name="pObjeto">Objeto tipo Servicio</param>
		/// <param name="pOpc">Parametro que indica si se crear un nuevo registro (INS),
		/// o si se va a actualizar un registro (UPD)</param>
		/// <returns>Entero que es el Id en la tabla de conductores</returns>
		public int guardar(Servicio pObjeto, string pOpc)
		{
			int lNuevoId = 0;
			try
			{
				if (m_conn.activo() == false) m_conn.abrir("");
				using (NpgsqlCommand lCmd = new NpgsqlCommand())
				{
					lCmd.CommandType = CommandType.Text;
					lCmd.CommandText = "fn_conductor";
					lCmd.Parameters.Add(new NpgsqlParameter("idservicio", NpgsqlDbType.Integer));
					lCmd.Parameters.Add(new NpgsqlParameter("idconductor", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("idautomovil", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("idcliente", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("fecha", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("fecha_ini", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("fecha_fin", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("origen", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("destino", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("distancia", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("ctmr", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("cmer", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("costo", NpgsqlDbType.Varchar));
					lCmd.Parameters.Add(new NpgsqlParameter("opc", NpgsqlDbType.Varchar));
					lCmd.Parameters[0].Value = pObjeto.IDServicio;
					lCmd.Parameters[1].Value = pObjeto.IDConductor;
					lCmd.Parameters[2].Value = pObjeto.IDAutomovil;
					lCmd.Parameters[3].Value = pObjeto.IDCliente;
					lCmd.Parameters[4].Value = pObjeto.Fecha;
					lCmd.Parameters[5].Value = pObjeto.FechaInicio;
					lCmd.Parameters[6].Value = pObjeto.FechaFin;
					lCmd.Parameters[7].Value = pObjeto.Origen;
					lCmd.Parameters[8].Value = pObjeto.Destino;
					lCmd.Parameters[9].Value = pObjeto.Distancia;
					lCmd.Parameters[10].Value = pObjeto.Ctmr;
					lCmd.Parameters[11].Value = pObjeto.Cmer;
					lCmd.Parameters[12].Value = pObjeto.Costo;
					lCmd.Parameters[13].Value = pOpc;
					using (NpgsqlDataReader lDrd = lCmd.ExecuteReader())
					{
						if (lDrd != null)
						{
							lDrd.Read();
							lNuevoId = Convert.ToInt32(lDrd[0]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
				throw;
			}
			finally
			{
				if (m_conn.activo()) m_conn.cerrar();
			}
			return lNuevoId;
		}
	}
}