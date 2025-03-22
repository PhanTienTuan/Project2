using phantientuan_2110900045.Models;
using phantientuan_2110900045.Repository;
using Microsoft.AspNetCore.Mvc;

namespace phantientuan_2110900045.ViewComponents
{
	public class LoaiSpMenuViewComponent : ViewComponent
	{
		private readonly ILoaiSpRepository _LoaiSpRepository;

		public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpRepository)
		{
			_LoaiSpRepository = loaiSpRepository;
		}
		public IViewComponentResult Invoke()
		{
			var loaisp = _LoaiSpRepository.GetAllLoaiSp().OrderBy(x=>x.LoaiSp1);
			return View(loaisp);
		}
	}
}
