using Microsoft.AspNetCore.Mvc;
using QLTVS.BUS;
using QLTVS.DTO;
using System;
using System.Collections.Generic;

namespace QLTVS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheLoaiController : ControllerBase
    {
        private readonly ITheLoaiBUS theLoaiBUS;

        public TheLoaiController(ITheLoaiBUS bus)
        {
            theLoaiBUS = bus;
        }

        // GET: api/TheLoai
        [HttpGet]
        public ActionResult<List<TheLoaiDTO>> GetAll()
        {
            try
            {
                var list = theLoaiBUS.LayDanhSachTheLoai();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/TheLoai
        [HttpPost]
        public ActionResult Create([FromBody] TheLoaiDTO theLoai)
        {
            try
            {
                theLoaiBUS.ThemTheLoai(theLoai);
                return Ok(new { message = "Thêm thể loại thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/TheLoai
        [HttpPut]
        public ActionResult Update([FromBody] TheLoaiDTO theLoai)
        {
            try
            {
                theLoaiBUS.SuaTheLoai(theLoai);
                return Ok(new { message = "Cập nhật thể loại thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/TheLoai/{maTheLoai}
        [HttpDelete("{maTheLoai}")]
        public ActionResult Delete(string maTheLoai)
        {
            try
            {
                theLoaiBUS.XoaTheLoai(maTheLoai);
                return Ok(new { message = "Xóa thể loại thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
