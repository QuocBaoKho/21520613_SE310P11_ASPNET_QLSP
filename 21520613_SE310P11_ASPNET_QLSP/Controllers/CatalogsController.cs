using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using _21520613_SE310P11_ASPNET_QLSP.Models;
namespace _21520613_SE310P11_ASPNET_QLSP.Controllers
{
    public class CatalogsController : Controller
    {
        // GET: Catalogs
        readonly string connectionString = "Server=LAPTOP-N267PPCR;Database=QuanLySanPham;Trusted_Connection=True;TrustServerCertificate=True;";

        public ActionResult Index()
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<Catalog> catalogs = context.Catalogs.ToList();
            return View(catalogs);
        }
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                string code = Request.Form["CatalogCode"];
                string name = Request.Form["CatalogName"];
                QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
                Catalog c = new Catalog();
                c.CatalogCode = code;
                c.CatalogName = name;
                context.Catalogs.InsertOnSubmit(c);
                context.SubmitChanges();
                return RedirectToAction("Index");
            } 

            return View();
        }
        public ActionResult Edit(int id)
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            Catalog c = context.Catalogs.Where(ct  => ct.Id == id).FirstOrDefault();
            if(Request.Form.Count == 0)
            {
                return View(c);
            }
            c.CatalogCode = Request.Form["CatalogCode"];
            c.CatalogName = Request.Form["CatalogName"];
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            Catalog c = context.Catalogs.Where(ct => ct.Id == id).FirstOrDefault();
            if(c != null)
            {
                context.Catalogs.DeleteOnSubmit(c);
                context.SubmitChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Details(int id) {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            Catalog catalog = context.Catalogs.FirstOrDefault(c => c.Id == id);
            return View(catalog);
        
        }
        public ActionResult Products(int catalogId)
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<Product> products = context.Products.Where(p => p.CatalogId == catalogId).ToList();
            return View(products);
        }
    
       
    }
}