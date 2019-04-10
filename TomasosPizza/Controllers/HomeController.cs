using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizza.Models;
using TomasosPizza.ViewModels;
using Microsoft.AspNetCore.Identity;
using TomasosPizza.ModelsIdentity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizza.Controllers
{
    public class HomeController : Controller
    {
        private TomasosContext _context;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(TomasosContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ViewProducts()
        {
            FoodViewModel model = new FoodViewModel();

            model.Dishes = _context.Matratts.ToList();
            model.DishIngredients = _context.MatrattProdukts.ToList();
            model.Ingridients = _context.Produkts.ToList();
            model.TypeOfDish = _context.MatrattTyps.ToList();

            return View(model);
        }

        public IActionResult AddToCart(int id)
        {
            List<Matratt> model;

            var newProduct = _context.Matratt.SingleOrDefault(p => p.MatrattId == id);

            if (HttpContext.Session.GetString("cart") == null)
            {
                model = new List<Matratt>();
            }
            else
            {
                var temp = HttpContext.Session.GetString("cart");
                model = JsonConvert.DeserializeObject<List<Matratt>>(temp);
            }

            model.Add(newProduct);
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(model));

            return PartialView("_DetailCart", model);
        }

        public IActionResult NewOrder()
        {
            var order = HttpContext.Session.GetString("cart");

            var model = JsonConvert.DeserializeObject<List<Matratt>>(order);

            var id = _userManager.GetUserId(User);
            var customer = _context.Kunds.SingleOrDefault(k => k.AspNetUserId == id);
            

            Bestallning newOrder = new Bestallning();
            newOrder.KundId = customer.KundId;
            //newOrder.BestallningId = _context.Bestallning.OrderByDescending(bestallning => bestallning.BestallningId).First().BestallningId + 1;
            newOrder.BestallningDatum = DateTime.Now;
            newOrder.Totalbelopp = model.Sum(s => s.Pris);
            newOrder.Levererad = false;

            _context.Bestallning.Add(newOrder);
            _context.SaveChanges();
            var tempList = model.GroupBy(m => m.MatrattId).Select(g => g.First()); 

            foreach (var item in tempList)
            {
                BestallningMatratt dish = new BestallningMatratt();
                dish.BestallningId = newOrder.BestallningId;
                dish.MatrattId = item.MatrattId;
                dish.Antal = model.Where(m => m.MatrattId == item.MatrattId).Count();
                _context.BestallningMatratt.Add(dish);
            }

            _context.SaveChanges();

            return View(newOrder);
        }

        public IActionResult Update()
        {
            CustomerViewModel model = new CustomerViewModel();
            var id = _userManager.GetUserId(User);

            var customer = _context.Kunds.SingleOrDefault(k => k.AspNetUserId == id);
            model.CurrentCustomer = customer;
            model.UserName = _userManager.GetUserName(User);

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CustomerViewModel newPerson)
        {
            //var oldPersonValues = _context.Kunds.SingleOrDefault(k => k.KundId == newPerson.CurrentCustomer.KundId);
            var userId = _userManager.GetUserId(User);
            newPerson.CurrentCustomer.AspNetUserId = userId;

            var oldPersonValues = _context.Kunds.Single(k => k.AspNetUserId == newPerson.CurrentCustomer.AspNetUserId);

            _context.Entry(oldPersonValues).CurrentValues.SetValues(newPerson.CurrentCustomer);

            _context.SaveChanges();

            ModelState.Clear();

            return View("HomePage");
        }
    }
}
