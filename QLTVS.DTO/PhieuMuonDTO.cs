using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DTO
{
    public class PhieuMuonDTO
    {
        public string Maphieu { get; set; } = null!;

        public string Masv { get; set; } = null!;

        public DateTime Ngaymuon { get; set; }

        public DateTime Hantra { get; set; }

        public string? Trangthai { get; set; }

        public PhieuMuonDTO() { }

        public PhieuMuonDTO(string maPhieu, string maSV, DateTime ngayMuon, DateTime hanTra, string trangThai)
        {
            Maphieu = maPhieu;
            Masv = maSV;
            Ngaymuon = ngayMuon;
            Hantra = hanTra;
            Trangthai = trangThai;
        }
    }
}
