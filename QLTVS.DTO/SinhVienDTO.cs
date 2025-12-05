using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DTO
{
    public class SinhVienDTO
    {
        public string Masv { get; set; } = null!;

        public string Hoten { get; set; } = null!;

        public string? Lop { get; set; }

        public string? Email { get; set; }

        public string? Sdt { get; set; }

        public SinhVienDTO() { }

        public SinhVienDTO(string maSV, string hoTen, string lop, string email, string sdt)
        {
            Masv = maSV;
            Hoten = hoTen;
            Lop = lop;
            Email = email;
            Sdt = sdt;
        }
    }
}
