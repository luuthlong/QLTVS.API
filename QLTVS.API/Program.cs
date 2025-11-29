using Microsoft.EntityFrameworkCore;
using QLTVS.BUS;
using QLTVS.DAO;
using QLTVS.DAO.Data;
using QLTVS.DTO;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("QLTVSConnection");

builder.Services.AddDbContext<QltvContext>(options => options.UseNpgsql(connectionString));

// ITaiLieuDAO -> TaiLieuDAO
builder.Services.AddScoped<ITaiLieuDAO, TaiLieuDAO>();

// ITaiLieuBUS -> TaiLieuBUS
builder.Services.AddScoped<ITaiLieuBUS, TaiLieuBUS>();

// ITheLoaiDAO -> TheLoaiDAO
builder.Services.AddScoped<ITheLoaiDAO, TheLoaiDAO>();

// ITheLoaiBUS -> TheLoaiBUS
builder.Services.AddScoped<ITheLoaiBUS, TheLoaiBUS>();

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