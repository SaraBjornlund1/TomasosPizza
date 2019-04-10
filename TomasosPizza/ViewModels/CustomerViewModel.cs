using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using TomasosPizza.Models;
using System.ComponentModel.DataAnnotations;

namespace TomasosPizza.ViewModels
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage ="Måste ange ett användarnamn!")]
        [DisplayName("Ange användarnamn*")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Måste ange ett lösenord!")]
        [DisplayName("Ange lösenord*")]
        public string Password { get; set; }

        public string RoleName { get; set; }

        public Kund CurrentCustomer { get; set; }

        //Listor

    }
}
