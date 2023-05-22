using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ParcialJuanJoseHerreraQuinchia.DAL.Entities
{
    public class Tickets
    {
        //Attributes
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Fecha_Uso")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "Usada")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public Boolean IsUsed { get; set; }

        [Display(Name = "Localidad")]
        public String? EntranceGate { get; set; }

    }
}
