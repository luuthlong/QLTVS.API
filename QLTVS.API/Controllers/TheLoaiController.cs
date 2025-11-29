using Microsoft.AspNetCore.Mvc;
using QLTVS.BUS;
using QLTVS.DTO;

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
            var ds = theLoaiBUS.LayDanhSachTheLoai();
            return Ok(ds);
        }
    }
}
