using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Nombre { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Direccion { get; set; }
        public int CodigoPostal { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Ciudad { get; set; }
        [StringLength(13)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string RFC { get; set; }
        [StringLength(15)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Telefono { get; set; }
    }
}