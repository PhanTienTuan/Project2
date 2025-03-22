using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace phantientuan_2110900045.Controllers
{
    public class AccountController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        public IActionResult ThongTinKhachHang()
        {
            string Usename = HttpContext.Session.GetString("UserName");

            if (Usename != null)
            {
                var ThongTinKH = db.KhachHangs.FirstOrDefault(kh => kh.Username == Usename);
                if (ThongTinKH != null)
                {
                    return View(ThongTinKH);
                }
            }
            return RedirectToAction("ThemKhachHang");
        }


        [HttpGet]
        public ActionResult SuaThongTinKhachHang(String MaKh)
        {
            var khachHang = db.KhachHangs.Find(MaKh);
            return View(khachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaThongTinKhachHang(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                string Usename = HttpContext.Session.GetString("UserName");

                var existingThongTinKH = db.KhachHangs.FirstOrDefault(kh => kh.Username == Usename);

                if (existingThongTinKH != null)
                {
                    // Cập nhật thông tin trực tiếp trên đối tượng existingThongTinKH
                    existingThongTinKH.TenKh = khachHang.TenKh;
                    existingThongTinKH.DiaChi = khachHang.DiaChi;
                    existingThongTinKH.DienThoai = khachHang.DienThoai;
                    existingThongTinKH.Gmail = khachHang.Gmail;

                    db.SaveChanges();
                    return RedirectToAction("ThongTinKhachHang", "Account");
                }
                else
                {
                    // Không tìm thấy thông tin khách hàng, bạn có thể thêm mới tại đây nếu cần
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("NhapThongTinKhachHang", "Account", khachHang);
        }
    }
}
