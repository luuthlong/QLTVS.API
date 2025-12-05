using QLTVS.DAO.Data;
using QLTVS.DAO.Models;
using QLTVS.DTO;

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
                // Cắt khoảng trắng thừa khi trả về DTO
                Matailieu = taiLieu.Matailieu.Trim(),
                Tentailieu = taiLieu.Tentailieu,
                Matheloai = taiLieu.Matheloai.Trim(), // Nên Trim cả mã thể loại
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
            // Cắt khoảng trắng ở mã tài liệu DTO trước khi kiểm tra và lưu
            string trimmedMaTaiLieu = taiLieu.Matailieu.Trim();

            // Kiểm tra tồn tại bằng cách Trim() cả mã trong DB và DTO
            var exists = db.Tailieus.Any(t => t.Matailieu.Trim() == trimmedMaTaiLieu);
            if (exists)
                throw new Exception($"Mã tài liệu '{trimmedMaTaiLieu}' đã tồn tại!");

            Tailieu entity = new Tailieu
            {
                // CHÚ Ý: Nếu cột DB là CHAR, nên giữ nguyên mã DTO hoặc đảm bảo nó có độ dài đúng
                // Tuy nhiên, việc lưu trữ mã đã được TRIM có thể gây lỗi nếu cột là CHAR.
                // Để tránh lỗi ghi, ta sẽ lưu nguyên mã DTO (hy vọng lớp BUS đã Trim)
                Matailieu = taiLieu.Matailieu,
                Tentailieu = taiLieu.Tentailieu,
                Tacgia = taiLieu.Tacgia,
                Namxuatban = taiLieu.Namxuatban,
                Nhaxuatban = taiLieu.Nhaxuatban,
                Matheloai = taiLieu.Matheloai,
                Soluong = taiLieu.Soluong,
                Linktailieu = taiLieu.Linktailieu
            };
            db.Tailieus.Add(entity);
            db.SaveChanges();
        }

        // Hàm sửa tài liệu
        public void UpdateTaiLieu(TaiLieuDTO taiLieu)
        {
            // Cắt khoảng trắng ở mã tài liệu DTO khi tìm kiếm
            string trimmedMaTaiLieu = taiLieu.Matailieu.Trim();

            // Tìm entity bằng cách Trim() mã trong DB và so sánh với mã đã Trim của DTO
            var entity = db.Tailieus.SingleOrDefault(x => x.Matailieu.Trim() == trimmedMaTaiLieu);

            if (entity != null)
            {
                // Cập nhật các trường dữ liệu
                // Giữ nguyên entity.Matailieu để tránh lỗi CHAR padding nếu có
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
            // Cắt khoảng trắng ở mã tài liệu nhận từ API
            string trimmedMaTaiLieu = maTaiLieu.Trim();

            // Tìm entity bằng cách Trim() mã trong DB và so sánh với mã đã Trim
            var entity = db.Tailieus.SingleOrDefault(x => x.Matailieu.Trim() == trimmedMaTaiLieu);

            if (entity == null)
                throw new Exception("Không tìm thấy tài liệu!");

            // Kiểm tra FK trong bảng ChiTietMuonSach
            // Cần Trim cả mã trong DB và mã nhận vào để so sánh chính xác
            bool isUsed = db.Chitietmuonsaches.Any(x => x.Matailieu.Trim() == trimmedMaTaiLieu);
            if (isUsed)
                throw new Exception("Không thể xóa tài liệu vì đã có dữ liệu mượn trả liên quan!");

            db.Tailieus.Remove(entity);
            db.SaveChanges();
        }


        // Hàm tìm kiếm tài liệu
        public List<TaiLieuDTO> SearchTaiLieu(string tuKhoa)
        {
            var query = db.Tailieus.AsQueryable();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                string lowerTuKhoa = tuKhoa.ToLower();
                // Thay thế phép so sánh bằng cách gọi .Trim().ToLower() để đảm bảo tìm kiếm chính xác
                query = query.Where(taiLieu => taiLieu.Matailieu.Trim().ToLower().Contains(lowerTuKhoa)
                                            || taiLieu.Tentailieu.ToLower().Contains(lowerTuKhoa)
                                            || taiLieu.Tacgia.ToLower().Contains(lowerTuKhoa));
            }
            return query.Select(taiLieu => new TaiLieuDTO
            {
                Matailieu = taiLieu.Matailieu.Trim(), // Cắt khoảng trắng khi trả về
                Tentailieu = taiLieu.Tentailieu,
                Tacgia = taiLieu.Tacgia,
                Matheloai = taiLieu.Matheloai,
                Namxuatban = taiLieu.Namxuatban ?? 0,
                Nhaxuatban = taiLieu.Nhaxuatban,
                Soluong = taiLieu.Soluong ?? 0,
                Linktailieu = taiLieu.Linktailieu

            }).ToList();
        }
    }
}