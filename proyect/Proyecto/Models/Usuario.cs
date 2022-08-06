using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        public int RolId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string ApellidoPaterno { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string ApellidoMaterno { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Teléfono { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public string Password { get; set; }

        public bool Estatus { get; set; }

        public Rol Rol { get; set; }

    }
}