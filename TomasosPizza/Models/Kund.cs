using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TomasosPizza.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }
        [Required(ErrorMessage ="Obligatoriskt med namn!")]
        public string Namn { get; set; }
        [Required(ErrorMessage = "Obligatoriskt med Gatuadress!")]
        public string Gatuadress { get; set; }
        [Required(ErrorMessage = "Obligatoriskt med Postnummer!")]
        public string Postnr { get; set; }
        [Required(ErrorMessage = "Obligatoriskt med Post ort!")]
        public string Postort { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        //public string AnvandarNamn { get; set; }
        //public string Losenord { get; set; }
        public string AspNetUserId { get; set; }

        public virtual ICollection<Bestallning> Bestallning { get; set; }
    }
}
