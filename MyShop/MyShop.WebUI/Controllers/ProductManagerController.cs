using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository Context;
        ProductCategoryRepository productCategories;

        public ProductManagerController()
        {
            productCategories = new ProductCategoryRepository();
            Context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();

            return View(products);
        }


        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product= new Product();
            viewModel.ProductCategories =productCategories.Collection();

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                Context.Insert(product);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }
           
        public ActionResult Edit(String Id)
        {
            Product product = Context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();


                    
                return View(viewModel);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Product product, String Id)
        {
            Product productToEdit = Context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Catergory = product.Catergory;
                productToEdit.Dscription = product.Dscription;
                productToEdit.Name = product.Name;
                productToEdit.Image = product.Image;
                productToEdit.Price = product.Price;

                Context.Commit();
                return RedirectToAction("Index");

            }
        }

        public ActionResult Delete(String Id)
        {
            Product productToDelete = Context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {

                return View(productToDelete);

            }
        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult ConfirmDelete(String Id)
        {
            Product productToDelete = Context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Context.Delete(Id);
                Context.Commit();
                return RedirectToAction("Index");

            }
        }

    }
}