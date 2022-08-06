using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Permisos;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [PermisosRolAttribute(2)] //solo puede ver empleado
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [PermisosRolAttribute(1)] //solo puede ver administrador
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SinPermiso()
        {
            ViewBag.Message = "Usted no cuenta con permisos";

            return View();
        }
    }
}