using phantientuan_2110900045.Models;
namespace phantientuan_2110900045.Repository
{
	public interface ILoaiSpRepository
	{
		LoaiSp add(LoaiSp loaiSP);

		LoaiSp Update(LoaiSp loaiSP);

		LoaiSp Detele(String maLoai);

		LoaiSp GetLoaiSp(String maLoai);

		IEnumerable<LoaiSp> GetAllLoaiSp();

	}
}
