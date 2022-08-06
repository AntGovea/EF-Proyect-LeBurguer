using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Proyecto.Permisos;

namespace Proyecto.Controllers
{
    public class UsuariosController : Controller
    {
        private ConexionDBContext db = new ConexionDBContext();

        //[PermisosRolAttribute(1)]
        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Rol);
            return View(usuarios.ToList());
        }

        //[PermisosRolAttribute(1)]
        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }


        // GET: Usuarios/Registrar
        public ActionResult Registrar()
        {
            return View();
        }

        //[PermisosRolAttribute(1)]
        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.RolId = new SelectList(db.Roles, "RolId", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,RolId,Nombre,ApellidoPaterno,ApellidoMaterno,Teléfono,Email,Password,Estatus")] Usuario usuario, int tipo)
        {

            if (tipo == 1) //ADMINISTRADOR
            {
                if (VerificarCorreo(usuario))
                {
                    ViewBag.Message = "El correo electronico ya ha sido registrado, intente con otro.";
                    ViewBag.RolId = new SelectList(db.Roles, "RolId", "Nombre", usuario.RolId);
                    return View(usuario);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        usuario.Password = Encrypt(usuario);
                        db.Usuarios.Add(usuario);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.RolId = new SelectList(db.Roles, "RolId", "Nombre", usuario.RolId);
                }

            }
            else if (tipo == 2) //CLIENTE
            {
                if (VerificarCorreo(usuario))
                {
                    ViewBag.Message = "El correo electronico ya ha sido registrado, intente con otro.";
                    ViewBag.RolId = new SelectList(db.Roles, "RolId", "Nombre", usuario.RolId);
                    return View("Registrar", usuario);
                }
                else
                {
                    usuario.RolId = 3; //ID DE CLIENTE ES 3
                    usuario.Estatus = true; //Estatus activo
                    usuario.Password = Encrypt(usuario);

                    if (ModelState.IsValid)
                    {
                        db.Usuarios.Add(usuario);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }

                    ViewBag.RolId = new SelectList(db.Roles, "RolId", "Nombre", usuario.RolId);

                }
            }

            return View(usuario);
        }

        public bool VerificarCorreo(Usuario user)
        {
            return db.Usuarios.Where(u => u.Email == user.Email).FirstOrDefault() != null;
        }

        //[PermisosRolAttribute(1)]
        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            usuario.Password = Decrypt(usuario);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            ViewBag.RolId = new SelectList(db.Roles, "RolId", "Nombre", usuario.RolId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[PermisosRolAttribute(1)]
        public ActionResult Edit([Bind(Include = "UsuarioId,RolId,Nombre,ApellidoPaterno,ApellidoMaterno,Teléfono,Email,Password,Estatus")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Password = Encrypt(usuario);
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.RolId = new SelectList(db.Roles, "RolId", "Nombre", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [PermisosRolAttribute(1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermisosRolAttribute(1)]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string Encrypt(Usuario str)
        {
            string hash = "la burguer";
            byte[] data = UTF8Encoding.UTF8.GetBytes(str.Password.ToString());

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] sb = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(sb);
        }

        public string Decrypt(Usuario value)
        {
            string hash = "la burguer";
            byte[] data = Convert.FromBase64String(value.Password.ToString());

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] sb = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(sb);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
