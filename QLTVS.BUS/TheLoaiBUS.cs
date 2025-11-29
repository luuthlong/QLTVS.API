using QLTVS.DAO;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.BUS
{
    public interface ITheLoaiBUS
    {
        List<TheLoaiDTO> LayDanhSachTheLoai();
    }

    public class TheLoaiBUS : ITheLoaiBUS
    {
        private readonly ITheLoaiDAO theLoaiDAO;

        public TheLoaiBUS(ITheLoaiDAO dao) // Nhận Interface DAO qua DI
        {
            theLoaiDAO = dao;
        }

        public List<TheLoaiDTO> LayDanhSachTheLoai()
        {
            return theLoaiDAO.GetAllTheLoai();
        }
    }
}
