using Microsoft.AspNetCore.Mvc;
using QLTVS.BUS;
using QLTVS.DTO;
using System;
using System.Collections.Generic;

namespace QLTVS.Controllers
{
    [Route("api/[controller]")] // Base route: api/TaiLieu
    [ApiController]
    public class TaiLieuController : ControllerBase
    {
        private readonly ITaiLieuBUS taiLieuBUS;

        public TaiLieuController(ITaiLieuBUS bus)
        {
            taiLieuBUS = bus;
        }

        // 1. GET: Lấy tất cả tài liệu
        // GET /api/TaiLieu
        [HttpGet] // Sửa: Xóa "Tài liệu"
        public IActionResult GetAll()
        {
            try
            {
                List<TaiLieuDTO> danhSach = taiLieuBUS.LayDanhSachTaiLieu();
                return Ok(danhSach);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server khi lấy danh sách: {ex.Message}");
            }
        }

        // 2. POST: Thêm tài liệu mới
        // POST /api/TaiLieu
        [HttpPost] // Sửa: Xóa "Tài liệu"
        public IActionResult Create([FromBody] TaiLieuDTO taiLieu)
        {
            try
            {
                taiLieuBUS.ThemTaiLieu(taiLieu);
                // Dùng nameof(GetAll) sẽ trỏ về [HttpGet] (tức là /api/TaiLieu)
                return CreatedAtAction(nameof(GetAll), taiLieu);
            }
            catch (Exception ex)
            {
                return BadRequest($"Thêm tài liệu thất bại: {ex.Message}");
            }
        }

        // 3. PUT: Cập nhật tài liệu
        // PUT /api/TaiLieu
        [HttpPut] // Sửa: Xóa "Tài liệu"
        public IActionResult Update([FromBody] TaiLieuDTO taiLieu)
        {
            try
            {
                taiLieuBUS.SuaTaiLieu(taiLieu);
                return Ok("Cập nhật tài liệu thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Sửa tài liệu thất bại: {ex.Message}");
            }
        }

        // 4. DELETE: Xóa tài liệu theo mã
        // DELETE /api/TaiLieu/{maTaiLieu}
        [HttpDelete("{maTaiLieu}")] // Sửa: Xóa "Tài liệu/"
        public IActionResult Delete(string maTaiLieu)
        {
            try
            {
                taiLieuBUS.XoaTaiLieu(maTaiLieu);
                return Ok($"Xóa tài liệu '{maTaiLieu}' thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Xóa tài liệu thất bại: {ex.Message}");
            }
        }

        // 5. GET: Tìm kiếm tài liệu
        // GET /api/TaiLieu/search?tukhoa=abc
        [HttpGet("search")] // Sửa: Chỉ giữ lại "search"
        public IActionResult Search(string tuKhoa)
        {
            try
            {
                List<TaiLieuDTO> ketQua = taiLieuBUS.TimKiemTaiLieu(tuKhoa);
                return Ok(ketQua);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server khi tìm kiếm: {ex.Message}");
            }
        }
    }
}