using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DTO
{
    public class TaiLieuDTO
    {
        public string Matailieu { get; set; } = null!;

        public string Tentailieu { get; set; } = null!;

        public string? Tacgia { get; set; }

        public int? Namxuatban { get; set; }

        public string? Nhaxuatban { get; set; }

        public string? Matheloai { get; set; }

        public int? Soluong { get; set; }

        public string? Linktailieu { get; set; }

        public TaiLieuDTO() { }

        public TaiLieuDTO(string maTaiLieu, string tenTaiLieu, string maTheLoai, string tacGia, int namXuatBan, string nhaXuatBan, int soLuong, string linkTaiLieu)
        {
            Matailieu = maTaiLieu;
            Tentailieu = tenTaiLieu;
            Matheloai = maTheLoai;
            Tacgia = tacGia;
            Namxuatban = namXuatBan;
            Nhaxuatban = nhaXuatBan;
            Soluong = soLuong;
            Linktailieu = linkTaiLieu;
        }
    }
}
