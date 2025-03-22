using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace phantientuan_2110900045.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
	public class KhachHangController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [Route("")]
        [Route("KhachHang")]
        [HttpGet]
        public IActionResult KhachHang()
        {
            return View();
        }
        [Route("DanhSachKhachHang")]
        public IActionResult DanhSachKhachHang(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstKhachHang = db.KhachHangs.AsNoTracking().OrderBy(x => x.MaKh);
            PagedList<KhachHang> lst = new PagedList<KhachHang>(lstKhachHang, pageNumber, pageSize);
            return View(lst);
        }
    }
}
