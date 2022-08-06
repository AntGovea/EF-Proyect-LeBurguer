using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {
        private int idRol;

        public PermisosRolAttribute(int _idrol)
        {
            idRol = _idrol;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                Usuario user = HttpContext.Current.Session["Usuario"] as Usuario;

                if (user.RolId != this.idRol)
                {
                    filterContext.Result = new RedirectResult("~/Home/SinPermiso");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Auth/Login");
            }
        }
    }
}