using QLTVS.DAO.Models;
using QLTVS.DAO;
using QLTVS.DTO;
using System.Collections.Generic;

namespace QLTVS.BUS
{
    public interface IMemberBUS
    {
        List<MemberDTO> GetAllMembers();
        bool CreateStudent(SinhVienDTO sinhVien);
        bool CreateManager(QuanLyDTO quanLy);
        bool UpdateStudent(SinhVienDTO dto);
        bool UpdateManager(QuanLyDTO dto);
        bool DeleteMember(string ma, string loai);


        List<MemberDTO> SearchMembers(string keyword);
    }

    public class MemberBUS : IMemberBUS
    {
        private readonly IMemberDAO memberDAO;
        public MemberBUS(IMemberDAO dao)
        {
            memberDAO = dao;
        }

        public List<MemberDTO> SearchMembers(string keyword)
        {

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return memberDAO.GetAllMembers();
            }
            return memberDAO.SearchMembers(keyword);
        }

        public List<MemberDTO> GetAllMembers()
        {
            return memberDAO.GetAllMembers();
        }

        public bool CreateStudent(SinhVienDTO dto)
        {
            string cleanMaSV = dto.Masv.Trim();
            string cleanHoTen = dto.Hoten.Trim();
            Sinhvien sv = new Sinhvien
            {
                Masv = cleanMaSV,
                Hoten = cleanHoTen,
                Lop = dto.Lop?.Trim(),
                Email = dto.Email?.Trim(),
                Sdt = dto.Sdt?.Trim()
            };
            Taikhoan tk = new Taikhoan
            {
                Tendangnhap = cleanMaSV,
                Matkhau = "123456",
                Vaitro = "SinhVien",
                Masv = cleanMaSV
            };
            return memberDAO.InsertStudent(sv, tk);
        }

        public bool CreateManager(QuanLyDTO dto)
        {
            string cleanMaQL = dto.Maql.Trim();
            string cleanHoTen = dto.Hoten.Trim();
            Quanly ql = new Quanly
            {
                Maql = cleanMaQL,
                Hoten = cleanHoTen,
                Email = dto.Email?.Trim(),
                Sdt = dto.Sdt?.Trim()
            };
            Taikhoan tk = new Taikhoan
            {
                Tendangnhap = cleanMaQL,
                Matkhau = "admin123",
                Vaitro = "QuanLy",
                Maql = cleanMaQL
            };
            return memberDAO.InsertManager(ql, tk);
        }

        public bool UpdateStudent(SinhVienDTO dto)
        {
            var sv = new Sinhvien
            {
                Masv = dto.Masv.Trim(),
                Hoten = dto.Hoten.Trim(),
                Lop = dto.Lop?.Trim(),
                Email = dto.Email?.Trim(),
                Sdt = dto.Sdt?.Trim()
            };
            return memberDAO.UpdateStudent(sv);
        }

        public bool UpdateManager(QuanLyDTO dto)
        {
            var ql = new Quanly
            {
                Maql = dto.Maql.Trim(),
                Hoten = dto.Hoten.Trim(),
                Email = dto.Email?.Trim(),
                Sdt = dto.Sdt?.Trim()
            };
            return memberDAO.UpdateManager(ql);
        }

        public bool DeleteMember(string ma, string loai)
        {
            return memberDAO.DeleteMember(ma.Trim(), loai);
        }
    }
}
