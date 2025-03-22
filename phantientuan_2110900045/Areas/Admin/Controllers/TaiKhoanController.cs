using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace phantientuan_2110900045.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	public class TaiKhoanController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [Route("")]
        [Route("TaiKhoan")]
        [HttpGet]
        public IActionResult TaiKhoan()
        {
            return View();
        }
        [Route("TaiKhoanNguoiDung")]
        public IActionResult TaiKhoanNguoiDung(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstNguoiDung = db.NguoiDungs.AsNoTracking().OrderByDescending(x => x.LoaiUser);
            PagedList<NguoiDung> lst = new PagedList<NguoiDung>(lstNguoiDung, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemTaiKhoan")]
        [HttpGet]
        public IActionResult ThemTaiKhoan()
        {
            return View();
        }
        [Route("ThemTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoan(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                if (db.NguoiDungs.Any(x => x.Usename == nguoiDung.Usename))
                {
                    TempData["Message"] = "Tên tài khoản đã tồn tại";
                    return View(nguoiDung);
                }
                else
                {
                    db.NguoiDungs.Add(nguoiDung);
                    db.SaveChanges();
                    TempData["Message"] = "Đã thêm tài khoản mới";
                    return RedirectToAction("TaiKhoanNguoiDung");
                }
            }
            return View(nguoiDung);
        }
        [Route("SuaTaiKhoan")]
        [HttpGet]
        public IActionResult SuaTaiKhoan(String Usename)
        {
            var nguoiDung = db.NguoiDungs.Find(Usename);
            return View(nguoiDung);
        }
        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Tài khoản đã được sửa";
                return RedirectToAction("TaiKhoanNguoiDung");
            }

            TempData["Message"] = "Sửa không thành công";
            return View(nguoiDung);
        }
        [Route("XoaTaiKhoan")]
        [HttpGet]
        public IActionResult XoaTaiKhoan(string Usename)
        {
            try
            {
                var khachHangToDelete = db.KhachHangs.Where(kh => kh.Username == Usename).ToList();
                var nhanvienToDelete = db.NhanViens.Where(nv => nv.Username == Usename).ToList();
                var nguoiDungToDelete = db.NguoiDungs.FirstOrDefault(ng => ng.Usename == Usename);
                if (khachHangToDelete.Count > 0 || nhanvienToDelete.Count > 0)
                {
                    TempData["Message"] = "Không xóa được vì tài khoản này là của nhân viên hoặc khách hàng đã cập nhập thông tin";
                }


                {
                    if (nguoiDungToDelete != null)
                    {
                        db.NguoiDungs.Remove(nguoiDungToDelete);
                        db.SaveChanges();
                        TempData["Message"] = "Xóa tài khoản thành công";
                    }
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Xóa tài khoản thất bại";
            }

            return RedirectToAction("TaiKhoanNguoiDung" );
        }
    }
}
