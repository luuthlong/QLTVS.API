using QLTVS.DAO;
using QLTVS.DTO;
using QLTVS_DAO;

namespace QLTVS_BUS
{
    public interface IPhieuMuonBUS
    {
        List<ChiTietMuonSachDTO> LayDanhSachCTMS();
        List<PhieuMuonDTO> LayDanhSachPhieuMuon();
        List<TaiLieuDTO> LayDanhSachTaiLieu();
        List<SinhVienDTO> LayDanhSachSinhvien();
        void ThemPhieuMuon(PhieuMuonDTO phieuMuon);
        void SuaPhieuMuon(PhieuMuonDTO phieuMuon);
        void XoaPhieuMuon(string maPhieu);
        void ThemCTMS(ChiTietMuonSachDTO chiTiet);
        void SuaCTMS(ChiTietMuonSachDTO chiTiet);
        void XoaCTMS(string maPhieu, string maTaiLieu);
        List<ChiTietMuonSachDTO> TimKiemCTMS(string maPhieu);
        void TraCMTS(string tinhTrangTra, DateTime? ngayTraTaiLieu, string maPhieu, string maTaiLieu);
        void CapNhatTrangThai(string maPhieu);
    }

    public class PhieuMuonBUS : IPhieuMuonBUS
    {

        private readonly IPhieuMuonDAO phieuMuonDAO;

        public PhieuMuonBUS(IPhieuMuonDAO dao)
        {
            phieuMuonDAO = dao;
        }

        // Hàm lấy danh sách CTMS
        public List<ChiTietMuonSachDTO> LayDanhSachCTMS()
        {
            return phieuMuonDAO.GetAllCTMS();
        }

        // Hàm lấy danh sách phiếu mượn
        public List<PhieuMuonDTO> LayDanhSachPhieuMuon()
        {
            return phieuMuonDAO.GetAllPhieuMuon();
        }

        // Hàm lấy danh sách tài liệu
        public List<TaiLieuDTO> LayDanhSachTaiLieu()
        {
            return phieuMuonDAO.GetAllTaiLieu();
        }

        // Hàm lấy danh sách sinh viên
        public List<SinhVienDTO> LayDanhSachSinhvien()
        {
            return phieuMuonDAO.GetAllSinhVien();
        }

        // Hàm thêm phiếu mượn
        public void ThemPhieuMuon(PhieuMuonDTO phieuMuon)
        {
            var kt = phieuMuonDAO.GetAllPhieuMuon().SingleOrDefault(phieu => phieu.Maphieu == phieuMuon.Maphieu);
            if (kt != null)
            {
                throw new Exception("Mã Phiếu không được trùng!!!");
            }
            if (string.IsNullOrWhiteSpace(phieuMuon.Maphieu))
            {
                throw new Exception("Mã Phiếu không được trống!!!");
            }
            phieuMuonDAO.AddPhieuMuon(phieuMuon);
        }


        // Hàm sửa phiếu mượn
        public void SuaPhieuMuon(PhieuMuonDTO phieuMuon)
        {
            phieuMuonDAO.UpdatePhieuMuon(phieuMuon);
        }

        // Hàm xóa phiếu mượn
        public void XoaPhieuMuon(string maPhieu)
        {
            phieuMuonDAO.DeletePhieuMuon(maPhieu);
        }

        // Hàm thêm CTMS
        public void ThemCTMS(ChiTietMuonSachDTO chiTiet)
        {
            var check = phieuMuonDAO.GetAllCTMS().SingleOrDefault(ctt => ctt.Maphieu == chiTiet.Maphieu && ctt.Matailieu == chiTiet.Matailieu);
            if (check != null)
            {
                throw new Exception("Mã Phiếu và Mã Tài Liệu không được trùng");
            }
            phieuMuonDAO.addCTMS(chiTiet);
        }

        // Hàm sửa CTMS
        public void SuaCTMS(ChiTietMuonSachDTO chiTiet)
        {
            phieuMuonDAO.UpdateCTMS(chiTiet);
        }

        // Hàm xóa CTMS
        public void XoaCTMS(string maPhieu, string maTaiLieu)
        {
            phieuMuonDAO.DeleteCTMS(maPhieu, maTaiLieu);
        }

        // Hàm tìm kiếm CTMS
        public List<ChiTietMuonSachDTO> TimKiemCTMS(string maPhieu)
        {
            return phieuMuonDAO.SearchCTMS(maPhieu);
        }

        // Hàm trả CTMS
        public void TraCMTS(string tinhTrangTra, DateTime? ngayTraTaiLieu, string maPhieu, string maTaiLieu)
        {
            phieuMuonDAO.ReturnCTMS(tinhTrangTra, ngayTraTaiLieu, maPhieu, maTaiLieu);
        }

        // Hàm cập nhật trạng thái phiếu mượn
        public void CapNhatTrangThai(string maPhieu)
        {
            phieuMuonDAO.UpdateTrangThai(maPhieu);
        }
    }
}