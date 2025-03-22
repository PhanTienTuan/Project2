using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using phantientuan_2110900045.Models.Authentication;
using phantientuan_2110900045.Models;

namespace phantientuan_2110900045.Controllers
{
	public class HomeController : Controller
	{
		QuanLyWebBanHangContext db = new QuanLyWebBanHangContext();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		[Authentication]
		public IActionResult Index(int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstSanPham = db.SanPhams.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<SanPham> lst = new PagedList<SanPham>(lstSanPham, pageNumber, pageSize);
			return View(lst);
		}

		public IActionResult SanPhamTheoLoai(String MaLoai, int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstSanPham = db.SanPhams.AsNoTracking().Where(x => x.MaLoai == MaLoai).OrderBy(x => x.TenSp);
			PagedList<SanPham> lst = new PagedList<SanPham>(lstSanPham, pageNumber, pageSize);
			ViewBag.maloai = MaLoai;
			return View(lst);
		}

		public IActionResult ChiTietSanPham(String maSp)
		{
			var sanPham = db.SanPhams.SingleOrDefault(x => x.MaSp == maSp);
			var anhSanPham = db.SanPhams.Where(x => x.MaSp == maSp).ToList();
			ViewBag.anhSanPham = anhSanPham;
			return View(sanPham);
		}
		public IActionResult Search(string keyword, int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			var lstSanPham = db.SanPhams.AsNoTracking()
				.Where(x => x.TenSp.Contains(keyword))
				.OrderBy(x => x.TenSp);

			PagedList<SanPham> lst = new PagedList<SanPham>(lstSanPham, pageNumber, pageSize);

			ViewBag.Keyword = keyword;

			return View(lst);
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}