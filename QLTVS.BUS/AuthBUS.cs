using QLTVS.DAO;
using QLTVS.DAO.Models;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.BUS
{
    public interface IAuthBUS
    {
        LoginResponseDTO Authenticate(LoginRequestDTO request);
        bool ChangePassword(ChangePasswordDTO request);
    }

    public class AuthBUS : IAuthBUS
    {

        private readonly IAuthDAO authDAO;

        public AuthBUS(IAuthDAO dao)
        {
            authDAO = dao;
        }

        public LoginResponseDTO Authenticate(LoginRequestDTO request)
        {
            var account = authDAO.GetAccountByCredentials(request);

            if (account == null)
            {
                return new LoginResponseDTO { IsSuccess = false, Message = "Tên đăng nhập hoặc mật khẩu không đúng." };
            }

            string userId = account.Masv ?? account.Maql;

            return new LoginResponseDTO
            {
                IsSuccess = true,
                Role = account.Vaitro,
                UserId = userId,
                Message = "Đăng nhập thành công!"
            };
        }
        public bool ChangePassword(ChangePasswordDTO request)
        {
            var checkLogin = new LoginRequestDTO
            {
                TenDangNhap = request.TenDangNhap,
                MatKhau = request.MatKhauCu
            };
            var account = authDAO.GetAccountByCredentials(checkLogin);
            if (account == null) return false;
            return authDAO.ChangePassword(request.TenDangNhap, request.MatKhauMoi);
        }
    }
}
