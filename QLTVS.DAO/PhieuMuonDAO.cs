using Microsoft.EntityFrameworkCore; // Cần thiết cho các hoạt động của EF Core
using QLTVS.DAO.Data;
using QLTVS.DAO.Models;
using QLTVS.DTO;

namespace QLTVS_DAO
{
    public interface IPhieuMuonDAO
    {
        List<ChiTietMuonSachDTO> GetAllCTMS();
        List<PhieuMuonDTO> GetAllPhieuMuon();
        List<TaiLieuDTO> GetAllTaiLieu();
        List<SinhVienDTO> GetAllSinhVien();
        void AddPhieuMuon(PhieuMuonDTO phieuMuon);
        void UpdatePhieuMuon(PhieuMuonDTO phieuMuon);
        void DeletePhieuMuon(string maPhieu);
        void addCTMS(ChiTietMuonSachDTO chiTiet);
        void UpdateCTMS(ChiTietMuonSachDTO chiTiet);
        void DeleteCTMS(string maPhieu, string maTaiLieu);
        List<ChiTietMuonSachDTO> SearchCTMS(string maPhieu);
        void ReturnCTMS(string tinhTrangTra, DateTime? ngayTraTaiLieu, string maPhieu, string maTaiLieu);
        void UpdateTrangThai(string maPhieu);
    }

    public class PhieuMuonDAO : IPhieuMuonDAO
    {
        private readonly QltvContext db;

        public PhieuMuonDAO(QltvContext context)
        {

            db = context;

        }

        // Hàm lấy tất cả CTMS
        public List<ChiTietMuonSachDTO> GetAllCTMS()
        {
            return db.Chitietmuonsaches.Select(chiTiet => new ChiTietMuonSachDTO
            {
                Maphieu = chiTiet.Maphieu,
                Matailieu = chiTiet.Matailieu,
                Soluong = chiTiet.Soluong ?? 0,
                Tinhtrangmuon = chiTiet.Tinhtrangmuon ?? null,
                Tinhtrangtra = chiTiet.Tinhtrangtra ?? null,
                Ngaytratailieu = chiTiet.Ngaytratailieu ?? null
            }).ToList();
        }

        // Hàm lấy tất cả phiếu mượn
        public List<PhieuMuonDTO> GetAllPhieuMuon()
        {
            return db.Phieumuons.Select(phieuMuon => new PhieuMuonDTO
            {
                Maphieu = phieuMuon.Maphieu,
                Masv = phieuMuon.Masv,
                Ngaymuon = phieuMuon.Ngaymuon,
                Hantra = phieuMuon.Hantra,
                Trangthai = "Đang Mượn"
            }).ToList();
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

        // Hàm lấy tất cả sinh viên
        public List<SinhVienDTO> GetAllSinhVien()
        {
            return db.Sinhviens.Select(sinhVien => new SinhVienDTO
            {
                Masv = sinhVien.Masv,
                Hoten = sinhVien.Hoten,
                Lop = sinhVien.Lop,
                Email = sinhVien.Email,
                Sdt = sinhVien.Sdt
            }).ToList();
        }

        // Thêm phiếu mượn mới
        public void AddPhieuMuon(PhieuMuonDTO phieuMuon)
        {
            Phieumuon phieumuon = new Phieumuon
            {
                Maphieu = phieuMuon.Maphieu,
                Masv = phieuMuon.Masv,
                Ngaymuon = phieuMuon.Ngaymuon,
                Hantra = phieuMuon.Hantra,
                Trangthai = "Đang Mượn"
            };
            db.Phieumuons.Add(phieumuon);
            db.SaveChanges();
        }

        // Cập nhật phiếu mượn
        public void UpdatePhieuMuon(PhieuMuonDTO phieuMuon)
        {
            var phieumuon = db.Phieumuons.SingleOrDefault(p => p.Maphieu == phieuMuon.Maphieu);
            if (phieumuon != null)
            {
                phieumuon.Maphieu = phieuMuon.Maphieu;
                phieumuon.Masv = phieuMuon.Masv;
                phieumuon.Ngaymuon = phieuMuon.Ngaymuon;
                phieumuon.Hantra = phieuMuon.Hantra;
                phieumuon.Trangthai = phieuMuon.Trangthai;
                db.SaveChanges();
            }
        }

        // Xóa chi tiết phiếu mượn TRƯỚC khi xóa phiếu mượn
        public void BeforeDeletePhieuMuon(string maPhieu)
        {
            var chitiet = db.Chitietmuonsaches.Where(c => c.Maphieu == maPhieu);
            foreach (var c in chitiet)
            {
                db.Chitietmuonsaches.Remove(c);
            }
            db.SaveChanges();
        }

        // Xóa phiếu mượn
        public void DeletePhieuMuon(string maPhieu)
        {
            BeforeDeletePhieuMuon(maPhieu);
            var phieumuon = db.Phieumuons.SingleOrDefault(p => p.Maphieu == maPhieu);
            if (phieumuon != null)
            {
                db.Phieumuons.Remove(phieumuon);
                db.SaveChanges();
            }
        }


        // Thêm CTMS
        public void addCTMS(ChiTietMuonSachDTO chiTiet)
        {
            Chitietmuonsach chitiet = new Chitietmuonsach
            {
                Maphieu = chiTiet.Maphieu,
                Matailieu = chiTiet.Matailieu,
                Soluong = chiTiet.Soluong,
                Tinhtrangmuon = chiTiet.Tinhtrangmuon,
                Tinhtrangtra = chiTiet.Tinhtrangtra,
                Ngaytratailieu = chiTiet.Ngaytratailieu
            };
            db.Chitietmuonsaches.Add(chitiet);
            db.SaveChanges();
        }

        // Hàm sửa CTMS
        public void UpdateCTMS(ChiTietMuonSachDTO chiTiet)
        {
            var chitiet = db.Chitietmuonsaches.SingleOrDefault(c => c.Maphieu == chiTiet.Maphieu && c.Matailieu == chiTiet.Matailieu);
            if (chitiet != null)
            {
                chitiet.Maphieu = chiTiet.Maphieu;
                chitiet.Matailieu = chiTiet.Matailieu;
                chitiet.Soluong = chiTiet.Soluong;
                chitiet.Tinhtrangmuon = chiTiet.Tinhtrangmuon;
                chitiet.Tinhtrangtra = chiTiet.Tinhtrangtra;
                if (chiTiet.Ngaytratailieu.HasValue)
                {
                    chitiet.Ngaytratailieu = chiTiet.Ngaytratailieu.Value;
                }
                db.SaveChanges();
            }
        }

        // Hàm xóa CTMS
        public void DeleteCTMS(string maPhieu, string maTaiLieu)
        {
            var chitiet = db.Chitietmuonsaches.SingleOrDefault(c => c.Maphieu == maPhieu && c.Matailieu == maTaiLieu);
            if (chitiet != null)
            {
                db.Chitietmuonsaches.Remove(chitiet);
                db.SaveChanges();
            }
        }

        // Hàm tìm kiếm CTMS
        public List<ChiTietMuonSachDTO> SearchCTMS(string maPhieu)
        {
            return db.Chitietmuonsaches.Where(c => c.Maphieu == maPhieu).Select(ct => new ChiTietMuonSachDTO
            {
                Maphieu = ct.Maphieu,
                Matailieu = ct.Matailieu,
                Soluong = ct.Soluong ?? 0,
                Tinhtrangmuon = ct.Tinhtrangmuon ?? null,
                Tinhtrangtra = ct.Tinhtrangtra ?? null,
                Ngaytratailieu = ct.Ngaytratailieu ?? null,
            }).ToList();
        }

        // Hàm trả CTMS
        public void ReturnCTMS(string tinhTrangTra, DateTime? ngayTraTaiLieu, string maPhieu, string maTaiLieu)
        {
            var chitiet = db.Chitietmuonsaches.SingleOrDefault(c => c.Maphieu == maPhieu && c.Matailieu == maTaiLieu);
            if (chitiet != null)
            {
                chitiet.Tinhtrangtra = tinhTrangTra;
                chitiet.Ngaytratailieu = ngayTraTaiLieu;
                db.SaveChanges();
            }
        }

        // Hàm cập nhật trạng thái phiếu mượn
        public void UpdateTrangThai(string maPhieu)
        {
            var phieumuon = db.Phieumuons.SingleOrDefault(p => p.Maphieu == maPhieu);
            if (phieumuon != null)
            {
                phieumuon.Trangthai = "đã trả";
            }
            db.SaveChanges();
        }

    }
}