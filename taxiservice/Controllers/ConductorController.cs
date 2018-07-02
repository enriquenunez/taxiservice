using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using taxiservice.Services;
using taxiservice.Models;

namespace taxiservice.Controllers
{
	public class ConductorController: Controller
	{
		[HttpGet]
		public ActionResult Agregar()
		{
			Conductor lConductor = new Conductor();
			return View(lConductor);
		}
		[HttpPost]
		public ActionResult Agregar(Conductor pData)
		{
			Conductores conductores = new Conductores();
			conductores.guardar(pData, "INS");
			return View(conductores);
		}
		[HttpGet]
		public ActionResult Editar(int id)
		{
			Conductores conductores = new Conductores();
			Conductor obj = conductores.getConductor(id);
			var modek = obj;
			return View(obj);
		}
		[HttpPost]
		public ActionResult Editar(Conductor pData)
		{
			Conductores conductores = new Conductores();
			conductores.guardar(pData, "INS");
			return View(conductores);
		}
		public ActionResult Listar()
		{
			ViewBag.Message = "Listado de Conductores";
			Conductores ldata = new Conductores();
			ldata.buscar("*", "");
			var model = ldata.getData();
			return View(model);
		}
		public ActionResult VerDatos(int id)
		{
			Conductores conductores = new Conductores();
			Conductor obj = conductores.getConductor(id);
			var model = obj;
			return View(obj);
		}
	}
}