using System.ComponentModel.DataAnnotations;

namespace MVCpractica9.Models
{
    public class estados_equipo
    {
        [Key]
        [Display(Name = "ID")]
        public int id_estados_equipo { get; set; }
        [Display(Name = "Descripcion")]
        public string? descripcion { get; set; }
        [Display(Name = "Estado del equipo")]
        public string? estado { get; set; }
    }
}
