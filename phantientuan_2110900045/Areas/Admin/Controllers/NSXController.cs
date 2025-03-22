using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace phantientuan_2110900045.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class NSXController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [Route("")]
        [Route("NSX")]
        [HttpGet]
        public IActionResult NSX()
        {
            return View();
        }
        [Route("DanhSachNSX")]
        public IActionResult DanhSachNSX(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstNcc = db.Nsxes.AsNoTracking().OrderBy(x => x.MaNsx);
            PagedList<Nsx> lst = new PagedList<Nsx>(lstNcc, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemNSX")]
        [HttpGet]
        public IActionResult ThemNSX()
        {
            return View();
        }
        [Route("ThemNSX")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNSX(Nsx nsx)
        {
            if (ModelState.IsValid)
            {
                if (db.Nsxes.Any(x => x.MaNsx == nsx.MaNsx))
                {
                    TempData["Message"] = "Mã nhà sản xuất đã tồn tại";
                    return View(nsx);
                }
                else
                {
                    db.Nsxes.Add(nsx);
                    db.SaveChanges();
                    TempData["Message"] = "Đã thêm nhà sản xuất";
                }
                return RedirectToAction("DanhSachNSX");
            }
            return View(nsx);
        }
        [Route("SuaNSX")]
        [HttpGet]
        public IActionResult SuaNSX(String MaNsx)
        {
            var nsx = db.Nsxes.Find(MaNsx);
            return View(nsx);
        }
        [Route("SuaNSX")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNSX(Nsx nsx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nsx).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Nhà sản xuất đã được sửa";
                return RedirectToAction("DanhSachNSX" );
            }
            else
            {
                TempData["Message"] = "Sửa không thành công";
                return View(nsx);
            }
        }
        [Route("XoaNSX")]
        [HttpGet]
        public IActionResult XoaNSX(String MaNsx)
        {
            try
            {
                var nsx = db.Nsxes.FirstOrDefault(x => x.MaNsx == MaNsx);
                if (nsx == null)
                {
                    TempData["Message"] = "Nhà sản xuất không tồn tại";
                    return RedirectToAction("DanhSachNSX" );
                }
                db.Nsxes.Remove(nsx);
                db.SaveChanges();
                TempData["Message"] = "Nhà sản xuất đã được xóa";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Xóa không thành công: " + ex.Message;
            }
            return RedirectToAction("DanhSachNSX" );
        }
    }
}
