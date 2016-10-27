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
            ViewBag.Script = "<script type='text/javascript'>alert('Mensaje');</script>";
            return View(tabla.plantillasList(Convert.ToInt32(Session["idUsuario"])));
        }

        //
        // GET: /plantilla/Details/5
        public ActionResult Details(int id)
        {
            plantillas nodo = tabla.plantillasGetEdit(id);
            if (nodo == null)
            {
                return HttpNotFound();
            }
            correo email = new correo();
            email.idPlantilla = nodo.idPlantilla;
            email.mensaje = nodo.mensaje;
            email.html = nodo.html;

            return View(email);
        }

        [HttpPost]
        public ActionResult Details(correo email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario user = tabla.usuario(Convert.ToInt32(Session["idUsuario"]));
                    email.nombre = user.getNom();
                    email.correo2 = user.getCor();
                    email.contrasena = user.getPas();

                    bool estado = tabla.enviar(email);

                    ModelState.AddModelError("", "Los correos fueron enviados.");

                    ViewBag.Script = "<script type='text/javascript'>alert('Mensaje');</script>";

                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Los correos NO fueron enviados.");
                return View(email);
            }
            catch
            {
                ModelState.AddModelError("", "Los correos NO fueron enviados.");
                return View(email);
            }
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
                return View(nodo);
            }
        }

        //
        // GET: /plantilla/Edit/5
        public ActionResult Edit(int id)
        {
            plantillas nodo = tabla.plantillasGetEdit(id);
            if (nodo == null)
            {
                return HttpNotFound();
            }
            return View(nodo);
        }

        //
        // POST: /plantilla/Edit/5
        [HttpPost]
        public ActionResult Edit(plantillas nodo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool estado = tabla.plantillasSetEdit(nodo);
                    return RedirectToAction("Index");
                }
                return View(nodo);
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
            plantillas nodo = tabla.plantillasGetEdit(id);
            if (nodo == null)
            {
                return HttpNotFound();
            }
            return View(nodo);
        }

        //
        // POST: /plantilla/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                tabla.eliminarPlantilla(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
