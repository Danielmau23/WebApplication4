using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class PlantillaController : Controller
    {
        BaseDatos tabla = new BaseDatos();

        //
        // GET: /plantilla/
        public ActionResult Index()
        {
            return View(tabla.plantillasList(Convert.ToInt32(Session["idUsuario"])));
        }

        //
        // GET: /plantilla/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /plantilla/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /plantilla/Create
        [HttpPost]
        public ActionResult Create(plantillas nodo)
        {
            try
            {
                tabla.nuevaPlantilla(Convert.ToString(nodo.mensaje), Convert.ToInt32(Session["idUsuario"]));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /plantilla/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /plantilla/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /plantilla/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /plantilla/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
