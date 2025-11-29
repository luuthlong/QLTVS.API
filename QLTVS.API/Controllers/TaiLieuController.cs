using Microsoft.AspNetCore.Mvc;
using QLTVS.BUS;
using QLTVS.DTO;

namespace QLTVS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiLieuController : ControllerBase
    {
        private readonly ITaiLieuBUS taiLieuBUS;

        public TaiLieuController(ITaiLieuBUS bus)
        {
            taiLieuBUS = bus;
        }

        // =============================
        // 1. GET all 
        // =============================
        // GET: api/TaiLieu
        [HttpGet]
        public ActionResult<List<TaiLieuDTO>> GetAll()
        {
            return Ok(taiLieuBUS.LayDanhSachTaiLieu());
        }

        // =============================
        // 2. Tìm kiếm tài liệu
        // =============================
        // GET: api/TaiLieu/search?keyword=abc
        [HttpGet("search")]
        public ActionResult<List<TaiLieuDTO>> Search(string keyword)
        {
            var result = taiLieuBUS.TimKiemTaiLieu(keyword);
            return Ok(result);
        }

        // =============================
        // 3. Thêm tài liệu
        // =============================
        // POST: api/TaiLieu
        [HttpPost]
        public ActionResult Add([FromBody] TaiLieuDTO taiLieu)
        {
            try
            {
                taiLieuBUS.ThemHangHoa(taiLieu);
                return Ok(new { message = "Thêm tài liệu thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // =============================
        // 4. Sửa tài liệu
        // =============================
        // PUT: api/TaiLieu
        [HttpPut]
        public ActionResult Update([FromBody] TaiLieuDTO taiLieu)
        {
            try
            {
                taiLieuBUS.SuaTaiLieu(taiLieu);
                return Ok(new { message = "Cập nhật tài liệu thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // =============================
        // 5. Xóa tài liệu
        // =============================
        // DELETE: api/TaiLieu/{ma}
        [HttpDelete("{maTaiLieu}")]
        public ActionResult Delete(string maTaiLieu)
        {
            try
            {
                taiLieuBUS.XoaTaiLieu(maTaiLieu);
                return Ok(new { message = "Xóa tài liệu thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
