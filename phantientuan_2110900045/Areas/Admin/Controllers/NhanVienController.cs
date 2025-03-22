using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace phantientuan_2110900045.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class NhanVienController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [Route("")]
        [Route("NhanVien")]
        [HttpGet]
        public IActionResult NhanVien()
        {
            return View();
        }
        [Route("DanhSachNhanVien")]
        public IActionResult DanhSachNhanVien(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstNhanVien = db.NhanViens.AsNoTracking().OrderBy(x => x.MaNv);
            PagedList<NhanVien> lst = new PagedList<NhanVien>(lstNhanVien, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemNhanVien")]
        [HttpGet]
        public IActionResult ThemNhanVien()
        {
            ViewBag.Username = new SelectList(db.NguoiDungs.ToList(), "Usename", "Usename");
            return View();
        }
        [Route("ThemNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVien(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                if (db.NhanViens.Any(x => x.MaNv == nhanVien.MaNv))
                {
                    TempData["Message"] = "Mã nhân viên đã tồn tại";
                    return View(nhanVien);
                }
                else
                {
                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                    TempData["Message"] = "Đã thêm nhân viên mới";
                }
                return RedirectToAction("DanhSachNhanVien");
            }
            return View(nhanVien);
        }
        [Route("SuaNhanVien")]
        [HttpGet]
        public IActionResult SuaNhanVien(String MaNv)
        {
            ViewBag.Username = new SelectList(db.NguoiDungs.ToList(), "Usename", "Usename");
            var nhanVien = db.NhanViens.Find(MaNv);
            return View(nhanVien);
        }
        [Route("SuaNhanvien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhanVien(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Sản phẩm đã được sửa";
                return RedirectToAction("DanhSachNhanVien");
            }
            else
            {
                TempData["Message"] = "Sửa không thành công";
                return View(nhanVien);
            }
        }
        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(string MaNv)
        {
            try
            {
                var nhanVien = db.NhanViens.FirstOrDefault(x => x.MaNv == MaNv);
                if (nhanVien == null)
                {
                    TempData["Message"] = "Nhân viên không tồn tại";
                    return RedirectToAction("DanhSachNhanVien" );
                }
                db.NhanViens.Remove(nhanVien);
                db.SaveChanges();
                TempData["Message"] = "Nhân viên đã được xóa";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Xóa không thành công: " + ex.Message;
            }
            return RedirectToAction("DanhSachNhanVien" );
        }
    }
}
