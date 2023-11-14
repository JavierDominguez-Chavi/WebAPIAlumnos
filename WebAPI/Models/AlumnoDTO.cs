using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class AlumnoDTO
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        public bool EstaAprobado { get; set; }

    }
}
