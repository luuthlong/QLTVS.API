using Microsoft.AspNetCore.Mvc;
using QLTVS.BUS;
using QLTVS.DTO;

namespace QLTVS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBUS authBUS;

        public AuthController(IAuthBUS bus)
        {
            authBUS = bus;
        }

        [HttpGet("Login")]
        public IActionResult Login([FromQuery] string tenDangNhap, [FromQuery] string matKhau)
        {
            var request = new LoginRequestDTO
            {
                TenDangNhap = tenDangNhap,
                MatKhau = matKhau
            };
            if (string.IsNullOrEmpty(request.TenDangNhap) || string.IsNullOrEmpty(request.MatKhau))
                return BadRequest(new LoginResponseDTO
                {
                    IsSuccess = false,
                    Message = "Vui lòng nhập đầy đủ."
                });

            var response = authBUS.Authenticate(request);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return Unauthorized(response);
        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO request)
        {
            if (string.IsNullOrEmpty(request.MatKhauMoi) ||
                string.IsNullOrEmpty(request.MatKhauCu) ||
                string.IsNullOrEmpty(request.TenDangNhap))
            {
                return BadRequest(new { message = "Vui lòng nhập đầy đủ thông tin (Tên đăng nhập, Mật khẩu cũ, Mật khẩu mới)." });
            }

            bool result = authBUS.ChangePassword(request);

            if (result)
            {
                return Ok(new { message = "Đổi mật khẩu thành công!" });
            }

            return BadRequest(new { message = "Tên đăng nhập hoặc mật khẩu cũ không đúng." });
        }

    }
}