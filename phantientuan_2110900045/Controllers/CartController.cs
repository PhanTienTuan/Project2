using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using phantientuan_2110900045.Models;

namespace phantientuan_2110900045.Controllers
{
    public class CartController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();

        public IActionResult CartIndex()
        {
            var Usename = HttpContext.Session.GetString("UserName");
            var userId = db.KhachHangs.Where(kh => kh.Username == Usename).Select(kh => kh.MaKh).FirstOrDefault();
           // Truy vấn dữ liệu từ cơ sở dữ liệu
            var shoppingCartItems = db.ChiTietHdbans.Where(ct => ct.MaHdNavigation.MaKh == userId).Include(ct => ct.MaSpNavigation)
               .Select(ct => new ShoppingCartViewModel
			   {
				    MaSp = ct.MaSpNavigation.MaSp,
                    AnhSp = ct.MaSpNavigation.AnhSp,
                    TenSp = ct.MaSpNavigation.TenSp,
                    GiaSp = ct.MaSpNavigation.GiaSp.HasValue ? ct.MaSpNavigation.GiaSp.Value : 0,
                    SoLuongBan = ct.SoLuongBan.HasValue ? ct.SoLuongBan.Value : 0, 
                TongTien = (ct.MaSpNavigation.GiaSp ?? 0) * (ct.SoLuongBan ?? 0) 
               }).ToList();
            return View(shoppingCartItems);
        }

		public IActionResult AddToCart(string maSp, int SoLuong)
		{
			var Usename = HttpContext.Session.GetString("UserName");
			var userId = db.KhachHangs.Where(kh => kh.Username == Usename).Select(kh => kh.MaKh).FirstOrDefault();

			Hdban existingInvoice = null;

			// Thử tìm hóa đơn chưa hoàn thành của người dùng
			existingInvoice = db.Hdbans.FirstOrDefault(hd => hd.MaKh == userId && hd.NgayBan == null);

			if (existingInvoice == null)
			{
				// Nếu không có hóa đơn chưa hoàn thành, tạo một mới và lưu vào DbContext
				var newInvoiceId = CreateNewInvoiceForUser(userId);
				existingInvoice = db.Hdbans.Find(newInvoiceId);
			}

			var existingCartItem = db.ChiTietHdbans.FirstOrDefault(ct => ct.MaHd == existingInvoice.MaHd && ct.MaSp == maSp);

			if (existingCartItem != null)
			{
				existingCartItem.SoLuongBan += SoLuong;
			}
			else
			{
				var newCartItem = new ChiTietHdban
				{
					MaHd = existingInvoice.MaHd,
					MaSp = maSp,
					SoLuongBan = SoLuong
				};
				db.ChiTietHdbans.Add(newCartItem);
			}

			db.SaveChanges();

			return RedirectToAction("CartIndex", "Cart");
		}



            private string CreateNewInvoiceForUser(string userId)
		{
			string newInvoiceId = GenerateUniqueInvoiceId();

			var newInvoice = new Hdban
			{
				MaHd = newInvoiceId,
				NgayBan = null,
				MaKh = userId,
				TongTien = 0
			};

			db.Hdbans.Add(newInvoice);
			db.SaveChanges();

			return newInvoiceId;
		}

		private string GenerateUniqueInvoiceId()
		{
			string currentDate = DateTime.Now.ToString("yyyyMMdd");
			string newInvoiceId = $"HD{currentDate}";

			while (true)
			{
				var existingInvoice = db.Hdbans.FirstOrDefault(h => h.MaHd == newInvoiceId);

				if (existingInvoice == null)
				{
					return newInvoiceId;
				}
				else
				{
					int lastInvoiceNumber;
					if (int.TryParse(newInvoiceId.Replace("HD", ""), out lastInvoiceNumber))
					{
						newInvoiceId = $"HD{lastInvoiceNumber + 1:D7}";
					}
				}
			}
		}


		public IActionResult IncreaseQuantity(string maSp)
		{
			var Usename = HttpContext.Session.GetString("UserName");
			var userId = db.KhachHangs.Where(kh => kh.Username == Usename).Select(kh => kh.MaKh).FirstOrDefault();

			var cartItem = db.ChiTietHdbans.FirstOrDefault(ct => ct.MaHdNavigation.MaKh == userId && ct.MaSp == maSp);

			if (cartItem != null)
			{
				cartItem.SoLuongBan++;
				db.SaveChanges();
			}

			return RedirectToAction("CartIndex", "Cart");
		}

		public IActionResult DecreaseQuantity(string maSp)
		{
			var Usename = HttpContext.Session.GetString("UserName");
			var userId = db.KhachHangs.Where(kh => kh.Username == Usename).Select(kh => kh.MaKh).FirstOrDefault();

			var cartItem = db.ChiTietHdbans.FirstOrDefault(ct => ct.MaHdNavigation.MaKh == userId && ct.MaSp == maSp);

			if (cartItem != null && cartItem.SoLuongBan > 1)
			{
				cartItem.SoLuongBan--;
				db.SaveChanges();
			}

			return RedirectToAction("CartIndex", "Cart");
		}

		public IActionResult RemoveFromCart(string maSp)
		{
			var Usename = HttpContext.Session.GetString("UserName");
			var userId = db.KhachHangs.Where(kh => kh.Username == Usename).Select(kh => kh.MaKh).FirstOrDefault();

			// Tìm sản phẩm trong giỏ hàng
			var cartItem = db.ChiTietHdbans.FirstOrDefault(ct => ct.MaHdNavigation.MaKh == userId && ct.MaSp == maSp);

			if (cartItem != null)
			{
				// Xóa sản phẩm khỏi giỏ hàng
				db.ChiTietHdbans.Remove(cartItem);
				db.SaveChanges();
			}

			return RedirectToAction("CartIndex", "Cart");
		}
        [HttpPost]
        [HttpPost]
        public IActionResult Checkout(string DiaChiNhanHang, int SoDienThoai, int TotalAmount)
        {
            var Usename = HttpContext.Session.GetString("UserName");
            var userId = db.KhachHangs.Where(kh => kh.Username == Usename).Select(kh => kh.MaKh).FirstOrDefault();

            var existingInvoice = db.Hdbans.FirstOrDefault(hd => hd.MaKh == userId && hd.NgayBan == null);

            if (existingInvoice != null)
            {
                var cartItems = db.ChiTietHdbans.Where(ct => ct.MaHd == existingInvoice.MaHd);

                if (cartItems.Any(ct => ct.SoLuongBan != null))
                {
                    existingInvoice.DiaChiNhanHang = DiaChiNhanHang;
                    existingInvoice.SdtnhanHang = SoDienThoai;
                    existingInvoice.NgayBan = DateTime.Now;
                    existingInvoice.TongTien = TotalAmount;

                    db.ChiTietHdbans.RemoveRange(cartItems);

                    db.SaveChanges();
                }
                else
                {
                    TempData["HoaDon"] = "Không thể thanh toán vì không có sản phẩm trong giỏ hàng";
                    return View("CartIndex");
                }
            }

            return RedirectToAction("CartIndex");
        }

    }
}
