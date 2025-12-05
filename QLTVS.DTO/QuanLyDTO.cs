using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DTO
{
    public class QuanLyDTO
    {
        public string Maql { get; set; } = null!;

        public string Hoten { get; set; } = null!;

        public string? Email { get; set; }

        public string? Sdt { get; set; }
    }
}
