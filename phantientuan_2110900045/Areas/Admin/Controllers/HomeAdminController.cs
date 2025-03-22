using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using phantientuan_2110900045.Models;
using Azure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace phantientuan_2110900045.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [Route("")]
        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}
