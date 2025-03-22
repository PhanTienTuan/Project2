using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace phantientuan_2110900045.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class SanPhamController : Controller
    {
        QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
        [Route("")]
        [Route("SanPham")]
        [HttpGet]
        public IActionResult SanPham()
        {
            return View();
        }
        [Route("DanhMucSanPham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.SanPhams.AsNoTracking().OrderBy(x => x.MaSp);
            PagedList<SanPham> lst = new PagedList<SanPham>(lstSanPham, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList(), "MaLoai", "LoaiSp1");
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(), "MaChatLieu", "ChatLieu1");
            ViewBag.MaNcc = new SelectList(db.Nccs.ToList(), "MaNcc", "TenNcc");
            ViewBag.MaNsx = new SelectList(db.Nsxes.ToList(), "MaNsx", "TenNsx");
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                if (db.SanPhams.Any(x => x.MaSp == sanPham.MaSp))
                {
                    TempData["Message"] = "Mã sản phẩm đã tồn tại";
                    return View(sanPham);
                }
                else
                {
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    TempData["Message"] = "Đã thêm mới sản phẩm";
                }
                return RedirectToAction("danhmucsanpham");
            }
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(String MaSp)
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList(), "MaLoai", "LoaiSp1");
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(), "MaChatLieu", "ChatLieu1");
            ViewBag.MaNcc = new SelectList(db.Nccs.ToList(), "MaNcc", "TenNcc");
            ViewBag.MaNsx = new SelectList(db.Nsxes.ToList(), "MaNsx", "TenNsx");
            var sanPham = db.SanPhams.Find(MaSp);
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Sản phẩm đã được sửa";
                return RedirectToAction("DanhMucSanPham");
            }
            else
            {
                TempData["Message"] = "Sửa không thành công";
                return View(sanPham);
            }

        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string MaSp)
        {
            try
            {
                var ChiTietHdbans = db.ChiTietHdbans.Where(x => x.MaSp == MaSp).ToList();
                if (ChiTietHdbans.Count > 0)
                {
                    TempData["Message"] = "Không xóa được sản phẩm này vì có chi tiết hóa đơn bán liên quan";
                }
                else
                {
                    var Sp = db.SanPhams.FirstOrDefault(x => x.MaSp == MaSp);
                    if (Sp != null)
                    {
                        db.SanPhams.Remove(Sp);
                        db.SaveChanges();
                        TempData["Message"] = "Sản phẩm đã được xóa";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Đã xảy ra lỗi khi xóa sản phẩm: " + ex.Message;
            }

            return RedirectToAction("DanhMucSanPham");
        }
    }
}
