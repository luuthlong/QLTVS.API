using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DTO
{
    public class TheLoaiDTO
    {
        public string Matheloai { get; set; } = null!;
        public string Tentheloai { get; set; } = null!;

        public TheLoaiDTO() { }

        public TheLoaiDTO(string maTheLoai, string tenTheLoai)
        {
            Matheloai = maTheLoai;
            Tentheloai = tenTheLoai;
        }
    }
}
