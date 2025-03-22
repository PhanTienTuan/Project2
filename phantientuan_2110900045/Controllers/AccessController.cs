using Microsoft.AspNetCore.Mvc;
using phantientuan_2110900045.Models;

namespace phantientuan_2110900045.Controllers
{
    public class AccessController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(NguoiDung User, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                var usernameToCheck = User.Usename;
                var check = db.NguoiDungs.FirstOrDefault(x => x.Usename.Equals(usernameToCheck));

                if (check == null)
                {
                    if (User.Password == confirmPassword)
                    {
                        var khachHang = new KhachHang { MaKh = User.Usename, Username = User.Usename };
                        db.KhachHangs.Add(khachHang);
                        db.NguoiDungs.Add(User);
                        db.SaveChanges();
                        return RedirectToAction("Login", "Access");
                    }
                    else
                    {
                        TempData["Message"] = "Mật khẩu và mật khẩu xác nhận không khớp.";
                        return View(User);
                    }
                }
                else
                {
                    TempData["Message"] = "Tên đăng nhập đã có người sử dụng";
                    return View(User);
                }
            }
            TempData["Message"] = "Thông tin không hợp lệ";
            return View(User);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(NguoiDung user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.NguoiDungs.SingleOrDefault(x => x.Usename.Equals(user.Usename) && x.Password.Equals(user.Password)); if (u != null)
                {
                    byte loaiUser = u.LoaiUser.GetValueOrDefault();

                    if (loaiUser == 1)
                    {
                        HttpContext.Session.SetString("UserName", u.Usename.ToString());
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        HttpContext.Session.SetString("UserName", u.Usename.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["Message"] = "Đăng nhập thất bại hãy kiểm tra lại tài khoản và mật khẩu";
                }
            }
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
