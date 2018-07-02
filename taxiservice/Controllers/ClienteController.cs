using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using taxiservice.Services;
using taxiservice.Models;

namespace taxiservice.Controllers
{
    public class ClienteController : Controller
    {
		[HttpGet]
		public ActionResult Agregar()
		{
			Cliente lCliente = new Cliente();
			return View(lCliente);
		}
		[HttpPost]
		public ActionResult Agregar(Cliente pData)
		{
			Clientes clientes = new Clientes();
			clientes.guardar(pData, "INS");
			return View();
		}
		[HttpGet]
		public ActionResult Editar(int id)
		{
			Clientes clientes = new Clientes();
			Cliente obj = clientes.getCliente(id);
			var model = obj;
			return View(obj);
		}
		[HttpPost]
		public ActionResult Editar(Cliente pData)
		{
			Clientes clientes = new Clientes();
			clientes.guardar(pData, "INS");
			return View();
		}
		public ActionResult Listar()
		{
			ViewBag.Message = "Listado de Clientes";
			Clientes ldata = new Clientes();
			ldata.buscar("*", "");
			var model = ldata.getData();
			return View(model);
		}
		public ActionResult Detalles(int id)
		{
			Clientes clientes = new Clientes();
			Cliente obj = clientes.getCliente(id);
			var model = obj;
			return View(obj);
		}
	}
}