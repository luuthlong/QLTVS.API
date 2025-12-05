using QLTVS.DAO.Data;
using QLTVS.DAO.Models;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLTVS.DAO
{
    public interface ITheLoaiDAO
    {
        List<TheLoaiDTO> GetAllTheLoai();
        void InsertTheLoai(TheLoaiDTO theLoai);
        void UpdateTheLoai(TheLoaiDTO theLoai);
        void DeleteTheLoai(string maTheLoai);
    }

    public class TheLoaiDAO : ITheLoaiDAO
    {
        private readonly QltvContext db;

        public TheLoaiDAO(QltvContext context)
        {
            db = context;
        }

        // Lấy tất cả thể loại
        public List<TheLoaiDTO> GetAllTheLoai()
        {
            return db.Theloais.Select(t => new TheLoaiDTO
            {
                Matheloai = t.Matheloai.Trim(),
                Tentheloai = t.Tentheloai.Trim()
            }).ToList();
        }

        // Thêm thể loại
        public void InsertTheLoai(TheLoaiDTO theLoai)
        {
            string ma = theLoai.Matheloai.Trim();
            string ten = theLoai.Tentheloai.Trim();

            if (db.Theloais.Any(t => t.Matheloai.Trim() == ma))
                throw new Exception($"Mã thể loại '{ma}' đã tồn tại!");

            db.Theloais.Add(new Theloai
            {
                Matheloai = ma,
                Tentheloai = ten
            });
            db.SaveChanges();
        }

        // Sửa thể loại
        public void UpdateTheLoai(TheLoaiDTO theLoai)
        {
            string ma = theLoai.Matheloai.Trim();
            string ten = theLoai.Tentheloai.Trim();

            var entity = db.Theloais.FirstOrDefault(t => t.Matheloai.Trim() == ma);
            if (entity == null)
                throw new Exception("Thể loại không tồn tại!");

            entity.Tentheloai = ten;
            db.SaveChanges();
        }

        // Xóa thể loại
        public void DeleteTheLoai(string maTheLoai)
        {
            string ma = maTheLoai.Trim();
            var entity = db.Theloais.FirstOrDefault(t => t.Matheloai.Trim() == ma);
            if (entity == null)
                throw new Exception("Thể loại không tồn tại!");

            if (db.Tailieus.Any(t => t.Matheloai.Trim() == ma))
                throw new Exception("Không thể xóa thể loại vì đang được sử dụng trong bảng Tài Liệu!");

            db.Theloais.Remove(entity);
            db.SaveChanges();
        }
    }
}
