using System;
using System.Collections.Generic;

namespace QLTVS.DAO.Models;

public partial class Phieumuon
{
    public string Maphieu { get; set; } = null!;

    public string Masv { get; set; } = null!;

    public DateTime Ngaymuon { get; set; }

    public DateTime Hantra { get; set; }

    public string? Trangthai { get; set; }

    public virtual ICollection<Chitietmuonsach> Chitietmuonsaches { get; set; } = new List<Chitietmuonsach>();

    public virtual Sinhvien MasvNavigation { get; set; } = null!;
}
