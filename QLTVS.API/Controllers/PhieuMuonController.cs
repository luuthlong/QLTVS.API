using Microsoft.AspNetCore.Mvc;
using QLTVS_BUS;
using QLTVS.DTO;

namespace QLTVS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuMuonController : ControllerBase
    {
        private readonly IPhieuMuonBUS phieuMuonBUS;

        public PhieuMuonController(IPhieuMuonBUS bus)
        {
            phieuMuonBUS = bus;
        }

        // ======================== KIỂM TRA API ========================
        [HttpGet("ping")]
        public IActionResult Ping() => Ok("PhieuMuon API is running");

        // ======================== GET ========================
        [HttpGet("CTMS")]
        public IActionResult LayDanhSachCTMS()
        {
            try
            {
                var result = phieuMuonBUS.LayDanhSachCTMS();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy danh sách chi tiết mượn trả: {ex.Message}");
            }
        }

        [HttpGet("PhieuMuon")]
        public IActionResult LayDanhSachPhieuMuon()
        {
            try
            {
                var result = phieuMuonBUS.LayDanhSachPhieuMuon();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy danh sách phiếu mượn: {ex.Message}");
            }
        }

        [HttpGet("TaiLieu")]
        public IActionResult LayDanhSachTaiLieu()
        {
            try
            {
                var result = phieuMuonBUS.LayDanhSachTaiLieu();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy danh sách tài liệu: {ex.Message}");
            }
        }

        [HttpGet("SinhVien")]
        public IActionResult LayDanhSachSinhvien()
        {
            try
            {
                var result = phieuMuonBUS.LayDanhSachSinhvien();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy danh sách sinh viên: {ex.Message}");
            }
        }

        [HttpGet("CTMS/{maPhieu}")]
        public IActionResult TimKiemCTMS(string maPhieu)
        {
            try
            {
                var result = phieuMuonBUS.TimKiemCTMS(maPhieu);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tìm kiếm chi tiết mượn trả: {ex.Message}");
            }
        }

        // ======================== POST ========================
        [HttpPost("PhieuMuon")]
        public IActionResult ThemPhieuMuon([FromBody] PhieuMuonDTO phieuMuon)
        {
            try
            {
                if (phieuMuon == null) return BadRequest("Dữ liệu phiếu mượn không hợp lệ");

                phieuMuonBUS.ThemPhieuMuon(phieuMuon);
                return Ok("Thêm phiếu mượn thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm phiếu mượn: {ex.Message}");
            }
        }

        [HttpPost("CTMS")]
        public IActionResult ThemCTMS([FromBody] ChiTietMuonSachDTO chiTiet)
        {
            try
            {
                if (chiTiet == null) return BadRequest("Dữ liệu chi tiết mượn trả không hợp lệ");

                phieuMuonBUS.ThemCTMS(chiTiet);
                return Ok("Thêm chi tiết mượn trả thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm chi tiết mượn trả: {ex.Message}");
            }
        }

        // ======================== PUT ========================
        [HttpPut("PhieuMuon")]
        public IActionResult SuaPhieuMuon([FromBody] PhieuMuonDTO phieuMuon)
        {
            try
            {
                if (phieuMuon == null) return BadRequest("Dữ liệu phiếu mượn không hợp lệ");

                phieuMuonBUS.SuaPhieuMuon(phieuMuon);
                return Ok("Cập nhật phiếu mượn thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật phiếu mượn: {ex.Message}");
            }
        }

        [HttpPut("CTMS")]
        public IActionResult SuaCTMS([FromBody] ChiTietMuonSachDTO chiTiet)
        {
            try
            {
                if (chiTiet == null) return BadRequest("Dữ liệu chi tiết mượn trả không hợp lệ");

                phieuMuonBUS.SuaCTMS(chiTiet);
                return Ok("Cập nhật chi tiết mượn trả thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật chi tiết mượn trả: {ex.Message}");
            }
        }

        [HttpPut("TraCTMS")]
        public IActionResult TraCMTS([FromBody] ChiTietMuonSachDTO chiTiet)
        {
            try
            {
                if (chiTiet == null || string.IsNullOrEmpty(chiTiet.Maphieu) || string.IsNullOrEmpty(chiTiet.Matailieu))
                    return BadRequest("Mã phiếu và mã tài liệu không được để trống");

                phieuMuonBUS.TraCMTS(
                    chiTiet.Tinhtrangtra,
                    chiTiet.Ngaytratailieu,
                    chiTiet.Maphieu,
                    chiTiet.Matailieu
                );

                return Ok("Cập nhật trạng thái trả tài liệu thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi trả chi tiết mượn trả: {ex.Message}");
            }
        }

        [HttpPut("TrangThai/{maPhieu}")]
        public IActionResult CapNhatTrangThai(string maPhieu)
        {
            try
            {
                if (string.IsNullOrEmpty(maPhieu)) return BadRequest("Mã phiếu không hợp lệ");

                phieuMuonBUS.CapNhatTrangThai(maPhieu);
                return Ok("Cập nhật trạng thái phiếu mượn thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật trạng thái phiếu mượn: {ex.Message}");
            }
        }

        // ======================== DELETE ========================
        [HttpDelete("PhieuMuon/{maPhieu}")]
        public IActionResult XoaPhieuMuon(string maPhieu)
        {
            try
            {
                if (string.IsNullOrEmpty(maPhieu)) return BadRequest("Mã phiếu không hợp lệ");

                phieuMuonBUS.XoaPhieuMuon(maPhieu);
                return Ok("Xóa phiếu mượn thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa phiếu mượn: {ex.Message}");
            }
        }

        [HttpDelete("CTMS/{maPhieu}/{maTaiLieu}")]
        public IActionResult XoaCTMS(string maPhieu, string maTaiLieu)
        {
            try
            {
                if (string.IsNullOrEmpty(maPhieu) || string.IsNullOrEmpty(maTaiLieu))
                    return BadRequest("Mã phiếu hoặc mã tài liệu không hợp lệ");

                phieuMuonBUS.XoaCTMS(maPhieu, maTaiLieu);
                return Ok("Xóa chi tiết mượn trả thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa chi tiết mượn trả: {ex.Message}");
            }
        }
    }
}
