using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace phantientuan_2110900045.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class HoaDonController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [Route("")]
        [Route("HoaDon")]
        [HttpGet]
        public IActionResult HoaDon()
        {
            return View();
        }
        [Route("DanhSachHoaDon")]
        public IActionResult DanhSachHoaDon(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstHoaDon = db.Hdbans.AsNoTracking().OrderBy(x => x.MaHd);
            PagedList<Hdban> lst = new PagedList<Hdban>(lstHoaDon, pageNumber, pageSize);
            return View(lst);
        }
    }
}
