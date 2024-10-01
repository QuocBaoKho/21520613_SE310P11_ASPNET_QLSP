using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _21520613_SE310P11_ASPNET_QLSP.Models;
namespace _21520613_SE310P11_ASPNET_QLSP.Controllers
{
    public class CatalogsController : Controller
    {
        // GET: Catalogs
        public ActionResult Index()
        {
            string connectionString = "Server=LAPTOP-N267PPCR;Database=QuanLySanPham;Trusted_Connection=True;TrustServerCertificate=True;";
            QuanLySanPHamDataContext context = new QuanLySanPHamDataContext(connectionString);
            List<Catalog> catalogs = context.Catalogs.ToList();
            return View(catalogs);
        }
    }
}