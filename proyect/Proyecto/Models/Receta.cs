using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Receta
    {
        public int RecetaId { get; set; }//Id

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string MateriasPrimas{ get; set; }   //lista de  ingredientes de la hamburguesa

        public string Cantidades { get; set; }   //lista de  ingredientes de la hamburguesa

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public int ProductoId { get; set; }   //idProducto para saber costo, cantidad y la imagen

        



     }
}