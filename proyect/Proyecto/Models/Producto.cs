using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double Precio { get; set; }
        public byte[] Imagen { get; set; }
    }
}