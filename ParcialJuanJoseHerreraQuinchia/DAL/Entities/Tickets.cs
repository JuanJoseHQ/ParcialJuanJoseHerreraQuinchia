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
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Boolean IsUsed { get; set; }

        [Display(Name = "Localidad")]
        public String? EntranceGate { get; set; }

    }
}
