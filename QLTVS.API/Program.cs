using Microsoft.EntityFrameworkCore;
using Npgsql;
using QLTVS.BUS;
using QLTVS.DAO;
using QLTVS.DAO.Data;
using QLTVS.DTO;
using QLTVS_BUS;
using QLTVS_DAO;

// =========================================================================
// PHẦN TOP-LEVEL STATEMENTS (Các câu lệnh cấp cao nhất, chạy trước tiên)
// =========================================================================

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext sử dụng phương thức Extension
builder.Services.ConfigureDbConnection(builder.Configuration);

builder.Services.AddScoped<ITaiLieuDAO, TaiLieuDAO>();
builder.Services.AddScoped<ITaiLieuBUS, TaiLieuBUS>();

builder.Services.AddScoped<ITheLoaiDAO, TheLoaiDAO>();
builder.Services.AddScoped<ITheLoaiBUS, TheLoaiBUS>();

builder.Services.AddScoped<IMemberDAO, MemberDAO>(); // Đăng ký DAO
builder.Services.AddScoped<IMemberBUS, MemberBUS>(); // Đăng ký BUS

builder.Services.AddScoped<IAuthDAO, AuthDAO>();   // Đăng ký cho logic xác thực Database
builder.Services.AddScoped<IAuthBUS, AuthBUS>();   // Đăng ký cho logic nghiệp vụ Đăng nhập
 
builder.Services.AddScoped<IPhieuMuonDAO, PhieuMuonDAO>();
builder.Services.AddScoped<IPhieuMuonBUS, PhieuMuonBUS>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// **Đã loại bỏ 'if (app.Environment.IsDevelopment())' ở đây**
app.UseSwagger();
app.UseSwaggerUI();
// KHẮC PHỤC CẢNH BÁO: Bỏ HttpsRedirection vì Railway đã xử lý HTTPS ở tầng ngoài.
// app.UseHttpsRedirection(); 
app.UseAuthorization();
app.MapControllers();

app.Run();

// =========================================================================
// PHẦN KHAI BÁO KIỂU DỮ LIỆU/CLASS
// =========================================================================

public static class ServiceExtensions
{
    public static void ConfigureDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("QLTVSConnection");
        }

        if (connectionString != null && connectionString.StartsWith("postgresql://"))
        {
            var uri = new Uri(connectionString);
            var userInfo = uri.UserInfo.Split(':');

            connectionString = new NpgsqlConnectionStringBuilder
            {
                Host = uri.Host,
                Port = uri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = uri.AbsolutePath.TrimStart('/'),
                // SslMode.Prefer là cần thiết cho các môi trường hosting như Railway
                SslMode = SslMode.Prefer,
            }.ToString();
        }

        if (connectionString != null)
        {
            services.AddDbContext<QltvContext>(options =>
               options.UseNpgsql(connectionString)
           );
        }
        else
        {
            Console.WriteLine("ERROR: Không tìm thấy chuỗi kết nối cơ sở dữ liệu.");
        }
    }
}