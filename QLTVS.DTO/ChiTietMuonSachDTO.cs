using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DTO
{
    public class ChiTietMuonSachDTO
    {
        public string Maphieu { get; set; } = null!;

        public string Matailieu { get; set; } = null!;

        public int? Soluong { get; set; } = 1;

        public string? Tinhtrangmuon { get; set; }

        public string? Tinhtrangtra { get; set; }

        public DateTime? Ngaytratailieu { get; set; }

        public ChiTietMuonSachDTO() { }

        public ChiTietMuonSachDTO(string maPhieu, string maTaiLieu, int soLuong, string tinhTrangMuon, string tinhTrangTra, DateTime? ngayTraTaiLieu)
        {
            Maphieu = maPhieu;
            Matailieu = maTaiLieu;
            Soluong = soLuong;
            Tinhtrangmuon = tinhTrangMuon;
            Tinhtrangtra = tinhTrangTra;
            Ngaytratailieu = ngayTraTaiLieu;
        }
    }
}
