using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace phantientuan_2110900045.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class NCCController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [Route("")]
        [Route("NCC")]
        [HttpGet]
        public IActionResult NCC()
        {
            return View();
        }
        [Route("DanhSachNCC")]
        public IActionResult DanhSachNCC(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstNcc = db.Nccs.AsNoTracking().OrderBy(x => x.MaNcc);
            PagedList<Ncc> lst = new PagedList<Ncc>(lstNcc, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemNCC")]
        [HttpGet]
        public IActionResult ThemNcc()
        {
            return View();
        }
        [Route("ThemNCC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNCC(Ncc ncc)
        {
            if (ModelState.IsValid)
            {
                if (db.Nccs.Any(x => x.MaNcc == ncc.MaNcc))
                {
                    TempData["Message"] = "Mã nhà cung cấp đã tồn tại";
                    return View(ncc);
                }
                else
                {
                    db.Nccs.Add(ncc);
                    db.SaveChanges();
                    TempData["Message"] = "Đã thêm nhà cung cấp";
                }
                return RedirectToAction("DanhSachNCC");
            }
            return View(ncc);
        }
        [Route("SuaNCC")]
        [HttpGet]
        public IActionResult SuaNCC(String MaNcc)
        {
            var ncc = db.Nccs.Find(MaNcc);
            return View(ncc);
        }
        [Route("SuaNCC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNCC(Ncc ncc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ncc).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Nhà cung cấp đã được sửa";
                return RedirectToAction("DanhSachNCC" );
            }
            else
            {
                TempData["Message"] = "Sửa không thành công";
                return View(ncc);
            }
        }
        [Route("XoaNCC")]
        [HttpGet]
        public IActionResult XoaNCC(String MaNcc)
        {
            try
            {
                var ncc = db.Nccs.FirstOrDefault(x => x.MaNcc == MaNcc);
                if (ncc == null)
                {
                    TempData["Message"] = "Nhà cung cấp không tồn tại";
                    return RedirectToAction("DanhSachNCC");
                }
                db.Nccs.Remove(ncc);
                db.SaveChanges();
                TempData["Message"] = "Nhà cung cấp đã được xóa";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Xóa không thành công: " + ex.Message;
            }
            return RedirectToAction("DanhSachNCC" );
        }
    }
}
