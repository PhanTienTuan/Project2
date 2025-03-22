using phantientuan_2110900045.Models;

namespace phantientuan_2110900045.Repository
{
	public class LoaiSpRepository : ILoaiSpRepository
	{ 
		private readonly QuanLyWebBanHangContext _context;
		public  LoaiSpRepository(QuanLyWebBanHangContext context)
		{
			_context = context;
		}
		public LoaiSp add(LoaiSp loaiSP)
		{
			_context.LoaiSps.Add(loaiSP);
			_context.SaveChanges();
			return loaiSP;
		}

		public LoaiSp Detele(String maLoai)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<LoaiSp> GetAllLoaiSp()
		{
			return _context.LoaiSps;
		}

		public LoaiSp GetLoaiSp(String maLoai)
		{
			return _context.LoaiSps.Find(maLoai);
		}

		public LoaiSp Update(LoaiSp loaiSP)
		{
			_context.Update(loaiSP);
			_context.SaveChanges();
			return loaiSP;
		}
	}
}
