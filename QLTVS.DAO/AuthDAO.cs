using QLTVS.DAO.Data;
using QLTVS.DAO.Models;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DAO
{
    public interface IAuthDAO
    {
        Taikhoan? GetAccountByCredentials(LoginRequestDTO request);
        bool ChangePassword(string username, string newPassword);
    }

    public class AuthDAO : IAuthDAO
    {
        private readonly QltvContext db;

        public AuthDAO(QltvContext context)
        {

            db = context;

        }

        public Taikhoan? GetAccountByCredentials(LoginRequestDTO request)
        {
            return db.Taikhoans.FirstOrDefault(taiKhoan =>
                    taiKhoan.Tendangnhap == request.TenDangNhap &&
                    taiKhoan.Matkhau == request.MatKhau);
        }

        public bool ChangePassword(string username, string newPassword)
        {
            var account = db.Taikhoans.FirstOrDefault(x => x.Tendangnhap == username);
            if (account == null) return false;
            account.Matkhau = newPassword;
            db.SaveChanges();
            return true;
        }
    }
}