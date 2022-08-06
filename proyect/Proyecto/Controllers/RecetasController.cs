using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class RecetasController : Controller
    {
        private ConexionDBContext db = new ConexionDBContext();
        private string[] aux;

        // GET: Recetas
        public ActionResult Index()
        {
            return View(db.Recetas.ToList());
        }

        // GET: Recetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // GET: Recetas/Create
        public ActionResult Create()
        {
            ViewBag.Productos = db.Productos.ToList().ToArray();
            ViewBag.MateriasPrimas = db.Materias.ToList().ToArray();

            ViewBag.MaxIngredientes = 12;
            return View();
        }

        // POST: Recetas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( string idProducto)
        {
            int  maximoIngredientes = 12;//dinamico

            Receta receta = new Receta();

            string ingredientesIds = "";
            string cantidades = "";

            for (int i = 0; i < maximoIngredientes; i++)
            {
                if (ingredientesIds.Length>0 )//para veer si es el primero o nel
                {
                    var auxIngrediente = Request.Form["Ingrediente" + i].ToString();
                    var auxUnidad = Request.Form["Unidad" + i].ToString();

                    if (auxIngrediente.Length>0 && auxUnidad.Length > 0)
                    {
                        ingredientesIds = ingredientesIds + "," + Request.Form["Ingrediente" + i].ToString();
                        cantidades = cantidades + "," + Request.Form["Unidad" + i].ToString();
                    }
                    else
                    {
                        Console.WriteLine("no paso");
                    }
                   
                    }
                //en caso de ser el primer ingrediente
                else
                {
                    ingredientesIds =Request.Form["Ingrediente" + i].ToString();
                    cantidades = Request.Form["Unidad" + i].ToString();
                }
              
            }

            receta.MateriasPrimas = ingredientesIds;
            receta.Cantidades = cantidades;
            receta.ProductoId = Convert.ToInt16(idProducto);
            


            if (ModelState.IsValid)
            {
                db.Recetas.Add(receta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(receta);
        }

        // GET: Recetas/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            //obtener la informacion de los ingredientes que tenemos en la receta 



            //validaciones , no mas de 2 panes

            //listado para el select de ingredientes
            
            var listaIngredientes=ViewBag.MateriasPrimas = db.Materias.ToList().ToArray();
            
            //maximo de ingredinetes, pendiente que sea dinamico y no estatico
            int ingredientesMax;
            ViewBag.MaxIngredientes =  ingredientesMax= 12;
            
            Producto p = new Producto();
            var ids = receta.MateriasPrimas.Split(',');
            MateriaPrima[] materias = new MateriaPrima[ingredientesMax];

            //crear arreglo que contenga los ids y la cantidades para desplegarlos en el front y poder editar las cantidades
            for (int i = 0; i < listaIngredientes.Length; i++)
            {
                if (i<ids.Length) {
                    int idTemp = Convert.ToInt32(ids[i]);
                    materias[i] = db.Materias.Find(idTemp);
                }
                else
                {
                   materias[i] = listaIngredientes[i];
                }
            }
           // ViewBag.MateriasPrimas=materias;//objeto de materias primas para desplegar con campos nullos
             var aux=receta.MateriasPrimas.Split(',');//ids de las materias primas seleccionadas en array
            int[] a = new int[aux.Length];
            for (int i = 0; i < aux.Length; i++)
            {
                a[i] = Convert.ToInt16(aux[i]);
            }
            ViewBag.MateriasPrimasSeleccionadas = a;
            ViewBag.IngredientesTotales = a;



            if (receta == null)
            {

                return HttpNotFound();


            }
            return View(receta);
        }



        // POST: Recetas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecetaId")] Receta receta)
        {
           

            if (ModelState.IsValid)
            {
               
            }

            {
              //  db.Entry(receta).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(receta);

           }

        // GET: Recetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Recetas.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // POST: Recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receta receta = db.Recetas.Find(id);
            db.Recetas.Remove(receta);
            db.SaveChanges();
            return RedirectToAction("Index");
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
