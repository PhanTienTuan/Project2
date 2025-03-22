using phantientuan_2110900045.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace phantientuan_2110900045.Controllers
{
	public class PageSanPhamController : Controller
	{
		QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();

		public IActionResult SanPhamIndex(int? page)
		{
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstSanPham = db.SanPhams.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<SanPham> lst = new PagedList<SanPham>(lstSanPham, pageNumber, pageSize);
			return View(lst);
		}
		public IActionResult SanPhamTheoLoai(String MaLoai, int? Page)
		{
			int pageSize = 6;
			int pageNumber = Page == null || Page < 0 ? 1 : Page.Value;
			var lstSanPham = db.SanPhams.AsNoTracking().Where(x => x.MaLoai == MaLoai).OrderBy(x => x.TenSp);
			PagedList<SanPham> lst = new PagedList<SanPham>(lstSanPham, pageNumber, pageSize);
			ViewBag.maloai = MaLoai;
			return View(lst);
		}
		public IActionResult Search(string keyword, int? page)
		{
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			var lstSanPham = db.SanPhams.AsNoTracking()
				.Where(x => x.TenSp.Contains(keyword))
				.OrderBy(x => x.TenSp);

			PagedList<SanPham> lst = new PagedList<SanPham>(lstSanPham, pageNumber, pageSize);

			ViewBag.Keyword = keyword;

			return View(lst);
		}
	}
}
