using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _21520613_SE310P11_ASPNET_QLSP.Models;
using Antlr.Runtime.Tree;
namespace _21520613_SE310P11_ASPNET_QLSP.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        string connectionString = "Server=LAPTOP-N267PPCR;Database=QuanLySanPham;Trusted_Connection=True;TrustServerCertificate=True;";
        public ActionResult Index()
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<Product> products = null;
            if (Request.QueryString.Count == 0)
            {
                products = context.Products.ToList();
            }
            else
            {
                double min = Double.Parse(Request.QueryString["minPriceInput"]);
                double max = Double.Parse(Request.QueryString["maxPriceInput"]);
                products = context.Products.Where(p=>p.UnitPrice >= min && p.UnitPrice <= max).ToList();
            }

            return View(products);
        }
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
                Product p = new Product();
                p.CatalogId = int.Parse(Request.Form["CatalogId"]);
                p.ProductCode = Request.Form["ProductCode"];
                p.ProductName = Request.Form["ProductName"];
                p.UnitPrice = Double.Parse(Request.Form["UnitPrice"]);
                HttpPostedFileBase file = Request.Files["Picture"];
                if (file != null)
                {
                    string serverPath = HttpContext.Server.MapPath("~/Images");
                    string filepath = serverPath + "/" + file.FileName;
                    file.SaveAs(filepath);
                    p.Picture = file.FileName;
                }
                context.Products.InsertOnSubmit(p);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id) {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            Product p = context.Products.SingleOrDefault(pr => pr.Id == id);
            if(Request.Form.Count == 0)
            {
                return View(p);
            }
            p.CatalogId = int.Parse(Request.Form["CatalogId"]);
            p.ProductCode = Request.Form["ProductCode"];
            p.ProductName = Request.Form["ProductName"];
            p.UnitPrice = Double.Parse(Request.Form["UnitPrice"]);
            HttpPostedFileBase file = Request.Files["Picture"];
            if (file != null)
            {
                string serverPath = HttpContext.Server.MapPath("~/Images");
                string filepath = serverPath + "/" + file.FileName;
                file.SaveAs(filepath);
                p.Picture = file.FileName;
            }
            context.SubmitChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            Product product = context.Products.SingleOrDefault(p => p.Id == id);
            if(product != null)
            {
                context.Products.DeleteOnSubmit(product);
                context.SubmitChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id) {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            Product product = context.Products.SingleOrDefault(p => p.Id == id);
            return View(product);
        }
        public ActionResult CapNhatGiaSP(int id, int unitPrice) {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            context.CapNhatGia(id, unitPrice);
            return RedirectToAction("Index");
        }
        public ActionResult ChiTietSanPham_sp(int id)
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            Product p = context.Products.SingleOrDefault(pr=> pr.Id == id);
            return View(p);
        }
        public ActionResult ThemSanPham_sp()
        {
            if (Request.Form.Count > 0)
            {
                QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
                Product p = new Product();
                string fileName = "";
                HttpPostedFileBase file = Request.Files["Picture"];
                if (file != null)
                {
                    string serverPath = HttpContext.Server.MapPath("~/Images");
                    string filepath = serverPath + "/" + file.FileName;
                    file.SaveAs(filepath);
                    fileName = file.FileName;
                }
                context.ThemSanPham(int.Parse(Request.Form["CatalogId"]), Request.Form["ProductCode"],
                    Request.Form["ProductName"], fileName, double.Parse(Request.Form["UnitPrice"]));
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult ToanBoSanPham_sp()
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<ToanBoSanPhamResult> results = context.ToanBoSanPham().ToList();
            return View(results);
        }
        public ActionResult XoaSanPham_sp(int id)
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            context.XoaSanPham(id);
            return RedirectToAction("Index");
        }
        public ActionResult BiggerThanK(double k)
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<Product> products = context.Products.Where(p=>p.UnitPrice > k).ToList();
            return View(products);
        }
        public ActionResult ArrangeViaPrice()
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<Product> products = context.Products.OrderBy(p=>p.UnitPrice).ToList();
            return View(products);
        }
        public ActionResult ArrangeViaPriceViaDescendingName()
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<Product> products = context.Products.OrderBy(p => p.UnitPrice).OrderByDescending(p=>p.ProductName).ToList();
            return View(products);
        }
        public ActionResult DisplayProduct()
        {
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<ProductForDisplay> displayed = context.Products
                .Select(p => new ProductForDisplay() { ProductCode = p.ProductCode, ProductName = p.ProductName, }).ToList();
            return View(displayed);
        }

    }
}