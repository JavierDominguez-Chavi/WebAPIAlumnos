using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Alumno
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        public bool EstaAprobado { get; set; }

        public string Secreto { get; set; }
    }
}
