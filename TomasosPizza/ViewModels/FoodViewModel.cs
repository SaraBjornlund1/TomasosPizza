using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizza.Models;

namespace TomasosPizza.ViewModels
{
    public class FoodViewModel
    {
        //Listor
        public List<Produkt> Ingridients { get; set; }
        public List<Matratt> Dishes { get; set; }
        public List<MatrattTyp> TypeOfDish { get; set; }
        public List<MatrattProdukt> DishIngredients { get; set; }

        public FoodViewModel()
        {
            Ingridients = new List<Produkt>();
            Dishes = new List<Matratt>();
            TypeOfDish = new List<MatrattTyp>();
            DishIngredients = new List<MatrattProdukt>();
        }
    }
}
