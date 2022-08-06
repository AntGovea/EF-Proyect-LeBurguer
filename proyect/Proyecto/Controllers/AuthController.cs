using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyecto.Controllers
{
    public class AuthController : Controller
    {
        public ConexionDBContext db = new ConexionDBContext();
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario user, string Return)
        {
            if (IsValid(user))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                Session["Usuario"] = extraerUsuario(user);
                if (Return != null)
                {

                    return Redirect(Return);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private bool IsValid(Usuario user)
        {

            return Autenticar(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Home");
        }

        public Usuario extraerUsuario(Usuario user)
        {
            return db.Usuarios.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();

        }

        public bool Autenticar(Usuario user)
        {
            user.Password = Encrypt(user);
            return db.Usuarios.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault() != null;
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
    }
}