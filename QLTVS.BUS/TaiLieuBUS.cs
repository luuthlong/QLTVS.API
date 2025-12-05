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
        void ThemTaiLieu(TaiLieuDTO taiLieu);
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


        // Hàm lấy danh sách tài liệu
        public List<TaiLieuDTO> LayDanhSachTaiLieu()
        {
            return taiLieuDAO.GetAllTaiLieu();
        }

        // Hàm thêm tài liệu
        public void ThemTaiLieu(TaiLieuDTO taiLieu)
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
            
            // Kiểm tra trùng mã tài liệu
            var exists = taiLieuDAO.GetAllTaiLieu().Any(t => t.Matailieu == taiLieu.Matailieu);
            if (exists)
                throw new Exception($"Mã tài liệu '{taiLieu.Matailieu}' đã tồn tại!");

            taiLieuDAO.InsertTaiLieu(taiLieu);
        }

        // Hàm sửa tài liệu
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

        // Hàm xóa tài liệu
        public void XoaTaiLieu(string maTaiLieu)
        {
            maTaiLieu = maTaiLieu.Trim();

            if (string.IsNullOrWhiteSpace(maTaiLieu))
                throw new Exception("Mã tài liệu không được để trống!");

            try
            {
                taiLieuDAO.DeleteTaiLieu(maTaiLieu);
            }
            catch (Exception ex)
            {
                // Bắt lỗi từ DAO (như lỗi FK, không tìm thấy...)
                throw new Exception("Xóa tài liệu thất bại: " + ex.Message);
            }
        }


        // Hàm tìm kiếm tài liệu
        public List<TaiLieuDTO> TimKiemTaiLieu(string tuKhoa)
        {
            // 1. Luôn Trim() tuKhoa ở tầng BUS để đảm bảo mã tài liệu hoặc từ khóa sạch
            if (tuKhoa != null)
            {
                tuKhoa = tuKhoa.Trim();
            }

            // 2. Nếu từ khóa rỗng (hoặc chỉ toàn khoảng trắng sau khi Trim), trả về tất cả.
            if (string.IsNullOrEmpty(tuKhoa))
            {
                return taiLieuDAO.GetAllTaiLieu();
            }

            // 3. Nếu từ khóa KHÔNG rỗng, luôn trả về kết quả tìm kiếm, dù nó rỗng hay có dữ liệu.
            // Logic này giúp hàm không còn tự động trả về GetAllTaiLieu() khi tìm kiếm thất bại.
            return taiLieuDAO.SearchTaiLieu(tuKhoa);
        }
    }
}
