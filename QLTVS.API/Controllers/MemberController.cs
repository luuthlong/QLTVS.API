using Microsoft.AspNetCore.Mvc;
using QLTVS.BUS;
using QLTVS.DTO;

namespace QLTVS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberBUS memberBUS;

        public MemberController(IMemberBUS bus)
        {
            memberBUS = bus;
        }

        // ============================================
        // GET: api/Member/Member (Lấy tất cả)
        // ============================================
        [HttpGet("Member")]
        public IActionResult GetAllMembers()
        {
            var list = memberBUS.GetAllMembers();
            return Ok(list);
        }

        // ============================================
        // [MỚI] GET: api/Member/Search?keyword=...
        // ============================================
        [HttpGet("Search")]
        public IActionResult SearchMembers([FromQuery] string keyword)
        {
            var list = memberBUS.SearchMembers(keyword);

            if (list == null || list.Count == 0)
            {
                return NotFound(new { message = $"Không tìm thấy thành viên nào khớp với '{keyword}'." });
            }

            return Ok(list);
        }

        // ============================================
        // POST: api/Member/Student
        // ============================================
        [HttpPost("Student")]
        public IActionResult CreateStudent([FromBody] SinhVienDTO sinhVien)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool result = memberBUS.CreateStudent(sinhVien);
            if (!result) return StatusCode(500, "Không thể tạo sinh viên.");
            return Ok(new { message = "Tạo sinh viên thành công!" });
        }

        // ============================================
        // POST: api/Member/Manager
        // ============================================
        [HttpPost("Manager")]
        public IActionResult CreateManager([FromBody] QuanLyDTO quanLy)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool result = memberBUS.CreateManager(quanLy);
            if (!result) return StatusCode(500, "Không thể tạo quản lý.");
            return Ok(new { message = "Tạo quản lý thành công!" });
        }

        // ============================================
        // PUT: api/Member/Student (Sửa)
        // ============================================
        [HttpPut("Student")]
        public IActionResult UpdateStudent([FromBody] SinhVienDTO sinhVien)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool result = memberBUS.UpdateStudent(sinhVien);
            if (!result) return NotFound(new { message = "Không tìm thấy sinh viên hoặc lỗi cập nhật." });
            return Ok(new { message = "Cập nhật sinh viên thành công!" });
        }

        // ============================================
        // PUT: api/Member/Manager (Sửa)
        // ============================================
        [HttpPut("Manager")]
        public IActionResult UpdateManager([FromBody] QuanLyDTO quanLy)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool result = memberBUS.UpdateManager(quanLy);
            if (!result) return NotFound(new { message = "Không tìm thấy quản lý hoặc lỗi cập nhật." });
            return Ok(new { message = "Cập nhật quản lý thành công!" });
        }

        // ============================================
        // DELETE: api/Member/{ma}?loai=SV or QL
        // ============================================
        [HttpDelete("Member/{ma}")]
        public IActionResult DeleteMember(string ma, [FromQuery] string loai)
        {
            if (loai != "SV" && loai != "QL") return BadRequest("loai phải là 'SV' hoặc 'QL'.");
            bool result = memberBUS.DeleteMember(ma, loai);
            if (!result) return StatusCode(500, "Xóa thành viên thất bại.");
            return Ok(new { message = "Xóa thành viên thành công!" });
        }
    }
}
