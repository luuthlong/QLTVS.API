using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DTO
{
    // LoginRequestDTO (Dữ liệu WinForms gửi lên API)
    public class LoginRequestDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
    }

    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Role { get; set; }    // "QuanLy" hoặc "SinhVien"
        public string UserId { get; set; }  // Mã SV hoặc Mã QL
        public string Message { get; set; }
    }

    public class ChangePasswordDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhauCu { get; set; }
        public string MatKhauMoi { get; set; }
    }
}
