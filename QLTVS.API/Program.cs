using Microsoft.EntityFrameworkCore;
using Npgsql;
using QLTVS.BUS;
using QLTVS.DAO;
using QLTVS.DAO.Data;
using QLTVS.DTO;

// =========================================================================
// PHẦN TOP-LEVEL STATEMENTS (Các câu lệnh cấp cao nhất, chạy trước tiên)
// =========================================================================

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext sử dụng phương thức Extension
builder.Services.ConfigureDbConnection(builder.Configuration);

// === ĐĂNG KÝ CÁC LỚP DATA ACCESS (DAO) ===
builder.Services.AddScoped<ITaiLieuDAO, TaiLieuDAO>();
builder.Services.AddScoped<ITheLoaiDAO, TheLoaiDAO>();
// Thêm các DAO khác nếu có:
// builder.Services.AddScoped<IPhieuMuonDAO, PhieuMuonDAO>(); 

// === ĐĂNG KÝ CÁC LỚP BUSINESS LOGIC (BUS) ===
// Dòng này là dòng BẮT BUỘC để sửa lỗi "Unable to resolve service for type 'QLTVS.BUS.ITaiLieuBUS'"
builder.Services.AddScoped<ITaiLieuBUS, TaiLieuBUS>();
// Thêm các BUS khác nếu có:
builder.Services.AddScoped<ITheLoaiBUS, ITheLoaiBUS>();
// builder.Services.AddScoped<IPhieuMuonBUS, PhieuMuonBUS>();

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
