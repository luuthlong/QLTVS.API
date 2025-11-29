using QLTVS.DAO;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.BUS
{
    public interface ITaiLieuBUS
    {
        List<TaiLieuDTO> LayDanhSachTaiLieu();
        void ThemHangHoa(TaiLieuDTO taiLieu);
        void SuaTaiLieu(TaiLieuDTO taiLieu);
        void XoaTaiLieu(string maTaiLieu);
        List<TaiLieuDTO> TimKiemTaiLieu(string tuKhoa);
    }

    public class TaiLieuBUS : ITaiLieuBUS
    {
        private readonly ITaiLieuDAO taiLieuDAO;

        public TaiLieuBUS(ITaiLieuDAO dao)
        {
            taiLieuDAO = dao;
        }

        public List<TaiLieuDTO> LayDanhSachTaiLieu()
        {
            return taiLieuDAO.GetAllTaiLieu();
        }

        // Hàm thêm
        public void ThemHangHoa(TaiLieuDTO taiLieu)
        {
            if (string.IsNullOrWhiteSpace(taiLieu.Matailieu))
                throw new Exception("Mã tài liệu không được để trống!");

            if (string.IsNullOrWhiteSpace(taiLieu.Tentailieu))
                throw new Exception("Tên tài liệu không được để trống!");

            if (string.IsNullOrWhiteSpace(taiLieu.Tacgia))
                throw new Exception("Tác giả không được để trống!");

            if (taiLieu.Namxuatban < 0)
                throw new Exception("Năm xuất bản phải lớn hơn hoặc bằng 0!");

            if (string.IsNullOrWhiteSpace(taiLieu.Nhaxuatban))
                throw new Exception("Nhà xuất bản không được để trống!");

            if (taiLieu.Soluong < 0)
                throw new Exception("Số lượng nhập phải lớn hơn hoặc bằng 0!");

            if (string.IsNullOrWhiteSpace(taiLieu.Linktailieu))
                throw new Exception("Link tài liệu không được để trống!");

            taiLieuDAO.InsertTaiLieu(taiLieu);
        }

        // Hàm sửa
        public void SuaTaiLieu(TaiLieuDTO taiLieu)
        {
            if (string.IsNullOrWhiteSpace(taiLieu.Matailieu))
                throw new Exception("Mã tài liệu không được để trống!");

            if (string.IsNullOrWhiteSpace(taiLieu.Tentailieu))
                throw new Exception("Tên tài liệu không được để trống!");

            if (string.IsNullOrWhiteSpace(taiLieu.Tacgia))
                throw new Exception("Tác giả không được để trống!");

            if (taiLieu.Namxuatban < 0)
                throw new Exception("Năm xuất bản phải lớn hơn hoặc bằng 0!");

            if (string.IsNullOrWhiteSpace(taiLieu.Nhaxuatban))
                throw new Exception("Nhà xuất bản không được để trống!");

            if (taiLieu.Soluong < 0)
                throw new Exception("Số lượng nhập phải lớn hơn hoặc bằng 0!");

            if (string.IsNullOrWhiteSpace(taiLieu.Linktailieu))
                throw new Exception("Link tài liệu không được để trống!");

            taiLieuDAO.UpdateTaiLieu(taiLieu);
        }

        // Hàm xóa
        public void XoaTaiLieu(string maTaiLieu)
        {
            if (string.IsNullOrWhiteSpace(maTaiLieu))
                throw new Exception("Mã tài liệu không được để trống!");

            taiLieuDAO.DeleteTaiLieu(maTaiLieu);
        }

        // Hàm tìm kiếm
        public List<TaiLieuDTO> TimKiemTaiLieu(string tuKhoa)
        {
            List<TaiLieuDTO> ketQua = taiLieuDAO.SearchTaiLieu(tuKhoa);
            int size = ketQua.Count;
            if (size > 0)
            {
                return ketQua;
            }
            return taiLieuDAO.GetAllTaiLieu();
        }
    }
}
