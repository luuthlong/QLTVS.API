using Microsoft.EntityFrameworkCore;
using QLTVS.DAO.Data;
using QLTVS.DAO.Models;
using QLTVS.DTO;
using System.Collections.Generic;
using System.Linq;

namespace QLTVS.DAO
{
    public interface IMemberDAO
    {
        List<MemberDTO> GetAllMembers();
        bool InsertStudent(Sinhvien sv, Taikhoan tk);
        bool InsertManager(Quanly ql, Taikhoan tk);
        bool UpdateStudent(Sinhvien sv);
        bool UpdateManager(Quanly ql);
        bool DeleteMember(string ma, string loai);


        List<MemberDTO> SearchMembers(string keyword);
    }

    public class MemberDAO : IMemberDAO
    {
        private readonly QltvContext db;
        public MemberDAO(QltvContext context)
        {
            db = context;
        }


        public List<MemberDTO> SearchMembers(string keyword)
        {

            string key = keyword.Trim().ToLower();

            var query = from tk in db.Taikhoans

                        join sv in db.Sinhviens on tk.Masv equals sv.Masv into svGroup
                        from subSv in svGroup.DefaultIfEmpty()

                        join ql in db.Quanlies on tk.Maql equals ql.Maql into qlGroup
                        from subQl in qlGroup.DefaultIfEmpty()


                        where (tk.Masv != null && tk.Masv.ToLower().Contains(key)) ||
                              (tk.Maql != null && tk.Maql.ToLower().Contains(key)) ||
                              (subSv != null && subSv.Hoten.ToLower().Contains(key)) ||
                              (subQl != null && subQl.Hoten.ToLower().Contains(key))

                        select new MemberDTO
                        {
                            MaSV = tk.Masv != null ? tk.Masv.Trim() : null,
                            MaQL = tk.Maql != null ? tk.Maql.Trim() : null,
                            HoTen = subSv != null ? subSv.Hoten : (subQl != null ? subQl.Hoten : "N/A"),
                            Lop = subSv != null ? subSv.Lop : "",
                            Email = subSv != null ? subSv.Email : (subQl != null ? subQl.Email : ""),
                            Sdt = subSv != null ? subSv.Sdt : (subQl != null ? subQl.Sdt : ""),
                            Role = tk.Vaitro
                        };

            return query.ToList();
        }

        public List<MemberDTO> GetAllMembers()
        {
            var query = from tk in db.Taikhoans
                        join sv in db.Sinhviens on tk.Masv equals sv.Masv into svGroup
                        from subSv in svGroup.DefaultIfEmpty()
                        join ql in db.Quanlies on tk.Maql equals ql.Maql into qlGroup
                        from subQl in qlGroup.DefaultIfEmpty()
                        select new MemberDTO
                        {
                            MaSV = tk.Masv != null ? tk.Masv.Trim() : null,
                            MaQL = tk.Maql != null ? tk.Maql.Trim() : null,
                            HoTen = subSv != null ? subSv.Hoten : (subQl != null ? subQl.Hoten : "N/A"),
                            Lop = subSv != null ? subSv.Lop : "",
                            Email = subSv != null ? subSv.Email : (subQl != null ? subQl.Email : ""),
                            Sdt = subSv != null ? subSv.Sdt : (subQl != null ? subQl.Sdt : ""),
                            Role = tk.Vaitro
                        };
            return query.ToList();
        }

        public bool InsertStudent(Sinhvien sv, Taikhoan tk)
        {
            sv.Masv = sv.Masv.Trim();
            tk.Tendangnhap = tk.Tendangnhap.Trim();
            tk.Masv = tk.Masv.Trim();

            using var transaction = db.Database.BeginTransaction();
            try
            {
                db.Sinhviens.Add(sv); db.SaveChanges();
                db.Taikhoans.Add(tk); db.SaveChanges();
                transaction.Commit(); return true;
            }
            catch { transaction.Rollback(); return false; }
        }

        public bool InsertManager(Quanly ql, Taikhoan tk)
        {
            ql.Maql = ql.Maql.Trim();
            tk.Tendangnhap = tk.Tendangnhap.Trim();
            tk.Maql = tk.Maql.Trim();

            using var transaction = db.Database.BeginTransaction();
            try
            {
                db.Quanlies.Add(ql); db.SaveChanges();
                db.Taikhoans.Add(tk); db.SaveChanges();
                transaction.Commit(); return true;
            }
            catch { transaction.Rollback(); return false; }
        }

        public bool UpdateStudent(Sinhvien sv)
        {
            try
            {
                var existingSV = db.Sinhviens.FirstOrDefault(x => x.Masv == sv.Masv);
                if (existingSV == null) return false;
                existingSV.Hoten = sv.Hoten;
                existingSV.Lop = sv.Lop;
                existingSV.Email = sv.Email;
                existingSV.Sdt = sv.Sdt;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool UpdateManager(Quanly ql)
        {
            try
            {
                var existingQL = db.Quanlies.FirstOrDefault(x => x.Maql == ql.Maql);
                if (existingQL == null) return false;
                existingQL.Hoten = ql.Hoten;
                existingQL.Email = ql.Email;
                existingQL.Sdt = ql.Sdt;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteMember(string ma, string loai)
        {
            string cleanMa = ma.Trim();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                var account = loai == "SV" ? db.Taikhoans.FirstOrDefault(t => t.Masv == cleanMa) : db.Taikhoans.FirstOrDefault(t => t.Maql == cleanMa);
                if (account != null) db.Taikhoans.Remove(account);
                if (loai == "SV") { var sv = db.Sinhviens.FirstOrDefault(s => s.Masv == cleanMa); if (sv != null) db.Sinhviens.Remove(sv); }
                else { var ql = db.Quanlies.FirstOrDefault(q => q.Maql == cleanMa); if (ql != null) db.Quanlies.Remove(ql); }
                db.SaveChanges(); transaction.Commit(); return true;
            }
            catch { transaction.Rollback(); return false; }
        }
    }
}
