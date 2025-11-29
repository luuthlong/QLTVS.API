using QLTVS.DAO.Data;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DAO
{
    public interface ITheLoaiDAO
    {
        List<TheLoaiDTO> GetAllTheLoai();
    }

    public class TheLoaiDAO : ITheLoaiDAO
    {
        private readonly QltvContext db;
        public TheLoaiDAO(QltvContext context)

        {

            db = context;

        }

        // Hàm lấy tất cả thể loại
        public List<TheLoaiDTO> GetAllTheLoai()
        {
            return db.Theloais.Select(theLoai => new TheLoaiDTO
            {
                Matheloai = theLoai.Matheloai,
                Tentheloai = theLoai.Tentheloai
            }).ToList();
        }
    }
}
