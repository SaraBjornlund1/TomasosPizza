using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizza.Models;

namespace TomasosPizza.ViewModels
{
    public class OrderViewModel
    {
        public Bestallning Bestallning { get; set; }
        public BestallningMatratt GetBestallningMatratt { get; set; }

        public Kund CustomerOrder { get; set; }

        public List<Matratt> CartList { get; set; }

        //Listor

        public OrderViewModel()
        {
            CartList = new List<Matratt>();
        }
    }
}
