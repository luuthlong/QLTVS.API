using QLTVS.DAO.Data;
using QLTVS.DAO.Models;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DAO
{
    public interface ITaiLieuDAO
    {
        List<TaiLieuDTO> GetAllTaiLieu();

        void InsertTaiLieu(TaiLieuDTO taiLieu);

        void UpdateTaiLieu(TaiLieuDTO taiLieu);

        void DeleteTaiLieu(string maTaiLieu);

        List<TaiLieuDTO> SearchTaiLieu(string tuKhoa);
    }

    public class TaiLieuDAO : ITaiLieuDAO
    {
        private readonly QltvContext db;

        public TaiLieuDAO(QltvContext context)
        {

            db = context;

        }

        // Hàm lấy tất cả tài liệu
        public List<TaiLieuDTO> GetAllTaiLieu()
        {
            return db.Tailieus.Select(taiLieu => new TaiLieuDTO
            {
                Matailieu = taiLieu.Matailieu,
                Tentailieu = taiLieu.Tentailieu,
                Matheloai = taiLieu.Matheloai,
                Tacgia = taiLieu.Tacgia,
                Namxuatban = taiLieu.Namxuatban ?? 0,
                Nhaxuatban = taiLieu.Nhaxuatban,
                Soluong = taiLieu.Soluong ?? 0,
                Linktailieu = taiLieu.Linktailieu
            }).ToList();
        }

        // Hàm thêm tài liệu
        public void InsertTaiLieu(TaiLieuDTO taiLieu)
        {
            Tailieu entity = new Tailieu
            {
                Matailieu = taiLieu.Matailieu,
                Tentailieu = taiLieu.Tentailieu,
                Tacgia = taiLieu.Tacgia,
                Namxuatban = taiLieu.Namxuatban,
                Nhaxuatban = taiLieu.Nhaxuatban,
                Soluong = taiLieu.Soluong,
                Linktailieu = taiLieu.Linktailieu
            };
            db.Tailieus.Add(entity);
            db.SaveChanges();
        }

        // Hàm sửa tài liệu
        public void UpdateTaiLieu(TaiLieuDTO taiLieu)
        {
            var entity = db.Tailieus.SingleOrDefault(x => x.Matailieu == taiLieu.Matailieu);

            if (entity != null)
            {
                entity.Matailieu = taiLieu.Matailieu;
                entity.Tentailieu = taiLieu.Tentailieu;
                entity.Matheloai = taiLieu.Matheloai;
                entity.Tacgia = taiLieu.Tacgia;
                entity.Namxuatban = taiLieu.Namxuatban;
                entity.Nhaxuatban = taiLieu.Nhaxuatban;
                entity.Soluong = taiLieu.Soluong;
                entity.Linktailieu = taiLieu.Linktailieu;

                db.SaveChanges();
            }
        }

        // Hàm xóa tài liệu
        public void DeleteTaiLieu(string maTaiLieu)
        {
            var entity = db.Tailieus.SingleOrDefault(x => x.Matailieu == maTaiLieu);

            if (entity != null)
            {
                db.Tailieus.Remove(entity);
                db.SaveChanges();
            }
        }

        // Hàm tìm kếm tài liệu
        public List<TaiLieuDTO> SearchTaiLieu(string tuKhoa)
        {
            var query = db.Tailieus.AsQueryable();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(taiLieu => taiLieu.Matailieu.ToLower().Contains(tuKhoa) || taiLieu.Tentailieu.ToLower().Contains(tuKhoa) || taiLieu.Tacgia.ToLower().Contains(tuKhoa));
            }
            return query.Select(taiLieu => new TaiLieuDTO
            {
                Matailieu = taiLieu.Matailieu,
                Tentailieu = taiLieu.Tentailieu,
                Tacgia = taiLieu.Tacgia
            }).ToList();
        }
    }
}