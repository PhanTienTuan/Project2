using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace phantientuan_2110900045.Models;

public partial class QuanLyWebBanHangContext : DbContext
{
    public QuanLyWebBanHangContext()
    {
    }

    public QuanLyWebBanHangContext(DbContextOptions<QuanLyWebBanHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatLieu> ChatLieus { get; set; }

    public virtual DbSet<ChiTietHdban> ChiTietHdbans { get; set; }

    public virtual DbSet<Hdban> Hdbans { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LienHe> LienHes { get; set; }

    public virtual DbSet<LoaiSp> LoaiSps { get; set; }

    public virtual DbSet<Ncc> Nccs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Nsx> Nsxes { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-2M6L6UE;Initial Catalog=QuanLyWebBanHang;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatLieu>(entity =>
        {
            entity.HasKey(e => e.MaChatLieu);

            entity.ToTable("ChatLieu");

            entity.Property(e => e.MaChatLieu)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ChatLieu1)
                .HasMaxLength(100)
                .HasColumnName("ChatLieu");
        });

        modelBuilder.Entity<ChiTietHdban>(entity =>
        {
            entity.HasKey(e => new { e.MaHd, e.MaSp });

            entity.ToTable("ChiTietHDBan");

            entity.Property(e => e.MaHd)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHD");
            entity.Property(e => e.MaSp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHdbans)
                .HasForeignKey(d => d.MaHd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDBan_HDBan");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietHdbans)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDBan_SanPham");
        });

        modelBuilder.Entity<Hdban>(entity =>
        {
            entity.HasKey(e => e.MaHd);

            entity.ToTable("HDBan");

            entity.Property(e => e.MaHd)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHD");
            entity.Property(e => e.MaKh)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.NgayBan).HasColumnType("date");
            entity.Property(e => e.SdtnhanHang).HasColumnName("SDTNhanHang");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Hdbans)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_HDBan_KhachHang");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Gmail).HasMaxLength(100);
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_KhachHang_NguoiDung");
        });

        modelBuilder.Entity<LienHe>(entity =>
        {
            entity.HasKey(e => e.IdLienHe);

            entity.ToTable("LienHe");

            entity.Property(e => e.IdLienHe)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<LoaiSp>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LoaiSP");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LoaiSp1)
                .HasMaxLength(50)
                .HasColumnName("LoaiSP");
        });

        modelBuilder.Entity<Ncc>(entity =>
        {
            entity.HasKey(e => e.MaNcc);

            entity.ToTable("NCC");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNCC");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(50)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Usename).HasName("PK_User");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.Usename)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.ChucVu).HasMaxLength(100);
            entity.Property(e => e.DiaChi).HasMaxLength(150);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SoDienThoai2)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNhanVien).HasMaxLength(100);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_NhanVien_NguoiDung");
        });

        modelBuilder.Entity<Nsx>(entity =>
        {
            entity.HasKey(e => e.MaNsx);

            entity.ToTable("NSX");

            entity.Property(e => e.MaNsx)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNSX");
            entity.Property(e => e.TenNsx)
                .HasMaxLength(50)
                .HasColumnName("TenNSX");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp);

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");
            entity.Property(e => e.AnhSp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AnhSP");
            entity.Property(e => e.GiaSp).HasColumnName("GiaSP");
            entity.Property(e => e.MaChatLieu)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaLoai)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaNcc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNsx)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNSX");
            entity.Property(e => e.SoLuongSp).HasColumnName("SoLuongSP");
            entity.Property(e => e.TenSp)
                .HasMaxLength(100)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.MaChatLieuNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaChatLieu)
                .HasConstraintName("FK_SanPham_ChatLieu");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_SanPham_LoaiSP");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_SanPham_NCC");

            entity.HasOne(d => d.MaNsxNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNsx)
                .HasConstraintName("FK_SanPham_NSX");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
