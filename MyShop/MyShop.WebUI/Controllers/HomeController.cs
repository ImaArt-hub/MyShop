using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> ProductContext, IRepository<ProductCategory> productCategoriesContext)
        {
            productCategories = productCategoriesContext;
            Context = ProductContext;
        }
        public ActionResult Index(string Category=null)
        {

            List<Product> products ;
            List<ProductCategory> categories = productCategories.Collection().ToList();

            if (Category == null)
            {
                products = Context.Collection().ToList();
            }
            else
            {
                products = Context.Collection().Where(p => p.Catergory == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            return View(model); 
        }
        public ActionResult Details(string Id)
        {
            Product product = Context.Find(Id);
            if (product == null){
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}