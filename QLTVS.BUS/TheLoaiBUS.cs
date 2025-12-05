using QLTVS.DAO;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLTVS.BUS
{
    public interface ITheLoaiBUS
    {
        List<TheLoaiDTO> LayDanhSachTheLoai();
        void ThemTheLoai(TheLoaiDTO theLoai);
        void SuaTheLoai(TheLoaiDTO theLoai);
        void XoaTheLoai(string maTheLoai);
    }

    public class TheLoaiBUS : ITheLoaiBUS
    {
        private readonly ITheLoaiDAO theLoaiDAO;

        public TheLoaiBUS(ITheLoaiDAO dao)
        {
            theLoaiDAO = dao;
        }

        // Lấy danh sách thể loại
        public List<TheLoaiDTO> LayDanhSachTheLoai()
        {
            return theLoaiDAO.GetAllTheLoai();
        }

        // Thêm thể loại
        public void ThemTheLoai(TheLoaiDTO theLoai)
        {
            theLoai.Matheloai = theLoai.Matheloai?.Trim();
            theLoai.Tentheloai = theLoai.Tentheloai?.Trim();

            if (string.IsNullOrWhiteSpace(theLoai.Matheloai))
                throw new Exception("Mã thể loại không được để trống!");

            if (string.IsNullOrWhiteSpace(theLoai.Tentheloai))
                throw new Exception("Tên thể loại không được để trống!");

            var exists = theLoaiDAO.GetAllTheLoai().Any(t => t.Matheloai.Trim() == theLoai.Matheloai);
            if (exists)
                throw new Exception($"Mã thể loại '{theLoai.Matheloai}' đã tồn tại!");

            theLoaiDAO.InsertTheLoai(theLoai);
        }

        // Sửa thể loại
        public void SuaTheLoai(TheLoaiDTO theLoai)
        {
            if (string.IsNullOrWhiteSpace(theLoai.Matheloai))
                throw new Exception("Mã thể loại không được để trống!");

            if (string.IsNullOrWhiteSpace(theLoai.Tentheloai))
                throw new Exception("Tên thể loại không được để trống!");

            theLoaiDAO.UpdateTheLoai(theLoai);
        }

        // Xóa thể loại
        public void XoaTheLoai(string maTheLoai)
        {
            maTheLoai = maTheLoai?.Trim();
            if (string.IsNullOrWhiteSpace(maTheLoai))
                throw new Exception("Mã thể loại không được để trống!");

            try
            {
                theLoaiDAO.DeleteTheLoai(maTheLoai);
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa thể loại thất bại: " + ex.Message);
            }
        }
    }
}
