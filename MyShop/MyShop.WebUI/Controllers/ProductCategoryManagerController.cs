using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository Context;

        public ProductCategoryManagerController()
        {
            Context = new ProductCategoryRepository();
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = Context.Collection().ToList();

            return View(productCategories);
        }


        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);

        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                Context.Insert(productCategory);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(String Id)
        {
            ProductCategory productCategory = Context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {

                return View(productCategory);
            }

        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, String Id)
        {
            ProductCategory productCategoryToEdit = Context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                productCategoryToEdit.Category = productCategory.Category;
                

                Context.Commit();
                return RedirectToAction("Index");

            }
        }

        public ActionResult Delete(String Id)
        {
            ProductCategory productToDelete = Context.Find(Id);
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
            ProductCategory productCategoryToDelete = Context.Find(Id);

            if (productCategoryToDelete == null)
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