using Microsoft.EntityFrameworkCore;
using Npgsql;
using QLTVS.BUS;
using QLTVS.DAO;
using QLTVS.DAO.Data;
using QLTVS.DTO;
// (Không cần using System.Security.Cryptography.X509Certificates; nếu không dùng trực tiếp)

// =========================================================================
// PHẦN TOP-LEVEL STATEMENTS (Các câu lệnh cấp cao nhất, chạy trước tiên)
// =========================================================================

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext sử dụng phương thức Extension
builder.Services.ConfigureDbConnection(builder.Configuration);

// ITaiLieuDAO -> TaiLieuDAO
builder.Services.AddScoped<ITaiLieuDAO, TaiLieuDAO>();

// ITheLoaiDAO -> TheLoaiDAO
builder.Services.AddScoped<ITheLoaiDAO, TheLoaiDAO>();

// (Thêm các dịch vụ khác của bạn)

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// =========================================================================
// PHẦN KHAI BÁO KIỂU DỮ LIỆU/CLASS (Phải đặt sau các Top-level statements)
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
                // Lỗi CS0019 đã được sửa: uri.Port là int, không cần ??
                Port = uri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = uri.AbsolutePath.TrimStart('/'),
                // Cảnh báo CS0618: TrustServerCertificate bị xóa (optional)
                SslMode = SslMode.Prefer,
                // TrustServerCertificate = true // Bỏ dòng này để loại bỏ cảnh báo
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
