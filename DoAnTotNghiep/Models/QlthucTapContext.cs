using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoAnTotNghiep.Models;

public partial class QlthucTapContext : DbContext
{
    public QlthucTapContext()
    {
    }

    public QlthucTapContext(DbContextOptions<QlthucTapContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bangdiem> Bangdiems { get; set; }

    public virtual DbSet<Bct> Bcts { get; set; }

    public virtual DbSet<Bctk> Bctks { get; set; }

    public virtual DbSet<Cbk> Cbks { get; set; }

    public virtual DbSet<Danhgium> Danhgia { get; set; }

    public virtual DbSet<Dktt> Dktts { get; set; }

    public virtual DbSet<Dnnt> Dnnts { get; set; }

    public virtual DbSet<Dnsv> Dnsvs { get; set; }

    public virtual DbSet<Dntt> Dntts { get; set; }

    public virtual DbSet<Giangvien> Giangviens { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<Lhp> Lhps { get; set; }

    public virtual DbSet<Pcgv> Pcgvs { get; set; }

    public virtual DbSet<Sinhvien> Sinhviens { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QLThucTap;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bangdiem>(entity =>
        {
            entity.HasKey(e => e.Msv).HasName("PK__BANGDIEM__C790E5ACCB8B02B9");

            entity.ToTable("BANGDIEM");

            entity.Property(e => e.Msv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MSV");
            entity.Property(e => e.Dat)
                .HasMaxLength(50)
                .HasColumnName("DAT");
            entity.Property(e => e.Diemck)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DIEMCK");
            entity.Property(e => e.Diemh1l1)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DIEMH1L1");
            entity.Property(e => e.Diemh1l2)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DIEMH1L2");
            entity.Property(e => e.Diemh1l3)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DIEMH1L3");
            entity.Property(e => e.Diemh2l1)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DIEMH2L1");
            entity.Property(e => e.Diemh2l2)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DIEMH2L2");
            entity.Property(e => e.Diemh2l3)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DIEMH2L3");
            entity.Property(e => e.Diemtb)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DIEMTB");

            entity.HasOne(d => d.MsvNavigation).WithOne(p => p.Bangdiem)
                .HasForeignKey<Bangdiem>(d => d.Msv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BANGDIEM__MSV__619B8048");
        });

        modelBuilder.Entity<Bct>(entity =>
        {
            entity.HasKey(e => e.Mbct).HasName("PK__BCT__6061F1B18F866E85");

            entity.ToTable("BCT");

            entity.Property(e => e.Mbct)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MBCT");
            entity.Property(e => e.Linkfile)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LINKFILE");
            entity.Property(e => e.Msv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MSV");
            entity.Property(e => e.Ngaynop)
                .HasColumnType("datetime")
                .HasColumnName("NGAYNOP");
            entity.Property(e => e.Noidung)
                .HasMaxLength(1000)
                .HasColumnName("NOIDUNG");
            entity.Property(e => e.Tieude)
                .HasMaxLength(255)
                .HasColumnName("TIEUDE");

            entity.HasOne(d => d.MsvNavigation).WithMany(p => p.Bcts)
                .HasForeignKey(d => d.Msv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BCT__MSV__628FA481");
        });

        modelBuilder.Entity<Bctk>(entity =>
        {
            entity.HasKey(e => e.Mbctk).HasName("PK__BCTK__5385C30672FAED34");

            entity.ToTable("BCTK");

            entity.Property(e => e.Mbctk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MBCTK");
            entity.Property(e => e.Linkfile)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LINKFILE");
            entity.Property(e => e.Msv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MSV");
            entity.Property(e => e.Ngaynop)
                .HasColumnType("datetime")
                .HasColumnName("NGAYNOP");
            entity.Property(e => e.Noidung)
                .HasMaxLength(1000)
                .HasColumnName("NOIDUNG");
            entity.Property(e => e.Tieude)
                .HasMaxLength(255)
                .HasColumnName("TIEUDE");

            entity.HasOne(d => d.MsvNavigation).WithMany(p => p.Bctks)
                .HasForeignKey(d => d.Msv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BCTK__MSV__6383C8BA");
        });

        modelBuilder.Entity<Cbk>(entity =>
        {
            entity.HasKey(e => e.Mcbk).HasName("PK__CBK__60B32D327FD673C5");

            entity.ToTable("CBK");

            entity.HasIndex(e => e.Email, "UQ__CBK__161CF724B93A4CC2").IsUnique();

            entity.HasIndex(e => e.Sdt, "UQ__CBK__CA1930A5B34D5745").IsUnique();

            entity.Property(e => e.Mcbk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MCBK");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Makhoa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAKHOA");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SDT");

            entity.HasOne(d => d.MakhoaNavigation).WithMany(p => p.Cbks)
                .HasForeignKey(d => d.Makhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CBK__MAKHOA__6477ECF3");
        });

        modelBuilder.Entity<Danhgium>(entity =>
        {
            entity.HasKey(e => e.Msv).HasName("PK__DANHGIA__C790E5AC7291BC6F");

            entity.ToTable("DANHGIA");

            entity.Property(e => e.Msv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MSV");
            entity.Property(e => e.Linkfile)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LINKFILE");
            entity.Property(e => e.Ngaydg)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDG");
            entity.Property(e => e.Nguoidg)
                .HasMaxLength(255)
                .HasColumnName("NGUOIDG");
            entity.Property(e => e.Noidung)
                .HasMaxLength(1000)
                .HasColumnName("NOIDUNG");
            entity.Property(e => e.Tieude)
                .HasMaxLength(255)
                .HasColumnName("TIEUDE");

            entity.HasOne(d => d.MsvNavigation).WithOne(p => p.Danhgium)
                .HasForeignKey<Danhgium>(d => d.Msv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DANHGIA__MSV__656C112C");
        });

        modelBuilder.Entity<Dktt>(entity =>
        {
            entity.HasKey(e => e.Mdk).HasName("PK__DKTT__C797137195D61DE5");

            entity.ToTable("DKTT");

            entity.Property(e => e.Mdk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDK");
            entity.Property(e => e.Htdk)
                .HasMaxLength(255)
                .HasColumnName("HTDK");
            entity.Property(e => e.Mdnnt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDNNT");
            entity.Property(e => e.Mdnsv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDNSV");
            entity.Property(e => e.Mlhp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MLHP");
            entity.Property(e => e.Msv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MSV");
            entity.Property(e => e.Tgbd)
                .HasColumnType("datetime")
                .HasColumnName("TGBD");
            entity.Property(e => e.Tgkt)
                .HasColumnType("datetime")
                .HasColumnName("TGKT");

            entity.HasOne(d => d.MdnntNavigation).WithMany(p => p.Dktts)
                .HasForeignKey(d => d.Mdnnt)
                .HasConstraintName("FK__DKTT__MDNNT__66603565");

            entity.HasOne(d => d.MdnsvNavigation).WithMany(p => p.Dktts)
                .HasForeignKey(d => d.Mdnsv)
                .HasConstraintName("FK__DKTT__MDNSV__6754599E");

            entity.HasOne(d => d.MlhpNavigation).WithMany(p => p.Dktts)
                .HasForeignKey(d => d.Mlhp)
                .HasConstraintName("FK__DKTT__MLHP__68487DD7");

            entity.HasOne(d => d.MsvNavigation).WithMany(p => p.Dktts)
                .HasForeignKey(d => d.Msv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DKTT__MSV__693CA210");
        });

        modelBuilder.Entity<Dnnt>(entity =>
        {
            entity.HasKey(e => e.Mdn).HasName("PK__DNNT__C7971374B8E536D7");

            entity.ToTable("DNNT");

            entity.HasIndex(e => e.Email, "UQ__DNNT__161CF724BCE40B76").IsUnique();

            entity.HasIndex(e => e.Sdt, "UQ__DNNT__CA1930A57F977AE9").IsUnique();

            entity.Property(e => e.Mdn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDN");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Tdn)
                .HasMaxLength(255)
                .HasColumnName("TDN");
            entity.Property(e => e.Ttnlh)
                .HasMaxLength(255)
                .HasColumnName("TTNLH");
        });

        modelBuilder.Entity<Dnsv>(entity =>
        {
            entity.HasKey(e => e.Mdn).HasName("PK__DNSV__C7971374517D42E1");

            entity.ToTable("DNSV");

            entity.HasIndex(e => e.Email, "UQ__DNSV__161CF72499463AAC").IsUnique();

            entity.HasIndex(e => e.Sdt, "UQ__DNSV__CA1930A500A7C660").IsUnique();

            entity.Property(e => e.Mdn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDN");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Lydo)
                .HasMaxLength(255)
                .HasColumnName("LYDO");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Tdn)
                .HasMaxLength(255)
                .HasColumnName("TDN");
            entity.Property(e => e.Thl)
                .HasMaxLength(50)
                .HasColumnName("THL");
            entity.Property(e => e.Ttnlh)
                .HasMaxLength(255)
                .HasColumnName("TTNLH");
        });

        modelBuilder.Entity<Dntt>(entity =>
        {
            entity.HasKey(e => e.Mdntt).HasName("PK__DNTT__116FAE589311EF1C");

            entity.ToTable("DNTT");

            entity.Property(e => e.Mdntt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDNTT");
            entity.Property(e => e.Mdnnt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDNNT");
            entity.Property(e => e.Mdnsv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDNSV");
            entity.Property(e => e.Mgv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MGV");

            entity.HasOne(d => d.MdnntNavigation).WithMany(p => p.Dntts)
                .HasForeignKey(d => d.Mdnnt)
                .HasConstraintName("FK__DNTT__MDNNT__6A30C649");

            entity.HasOne(d => d.MdnsvNavigation).WithMany(p => p.Dntts)
                .HasForeignKey(d => d.Mdnsv)
                .HasConstraintName("FK__DNTT__MDNSV__6B24EA82");

            entity.HasOne(d => d.MgvNavigation).WithMany(p => p.Dntts)
                .HasForeignKey(d => d.Mgv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DNTT__MGV__6C190EBB");
        });

        modelBuilder.Entity<Giangvien>(entity =>
        {
            entity.HasKey(e => e.Mgv).HasName("PK__GIANGVIE__C7970BDF10D2424F");

            entity.ToTable("GIANGVIEN");

            entity.HasIndex(e => e.Email, "UQ__GIANGVIE__161CF72448ACD3BC").IsUnique();

            entity.HasIndex(e => e.Sdt, "UQ__GIANGVIE__CA1930A5B740A8DD").IsUnique();

            entity.Property(e => e.Mgv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MGV");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Makhoa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAKHOA");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SDT");

            entity.HasOne(d => d.MakhoaNavigation).WithMany(p => p.Giangviens)
                .HasForeignKey(d => d.Makhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GIANGVIEN__MAKHO__6D0D32F4");
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.Makhoa).HasName("PK__KHOA__22F41770E3BCB837");

            entity.ToTable("KHOA");

            entity.Property(e => e.Makhoa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAKHOA");
            entity.Property(e => e.Tenkhoa)
                .HasMaxLength(255)
                .HasColumnName("TENKHOA");
        });

        modelBuilder.Entity<Lhp>(entity =>
        {
            entity.HasKey(e => e.Mlhp).HasName("PK__LHP__6CF57EF6E710F382");

            entity.ToTable("LHP");

            entity.Property(e => e.Mlhp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MLHP");
            entity.Property(e => e.Makhoa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAKHOA");

            entity.HasOne(d => d.MakhoaNavigation).WithMany(p => p.Lhps)
                .HasForeignKey(d => d.Makhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LHP__MAKHOA__6E01572D");
        });

        modelBuilder.Entity<Pcgv>(entity =>
        {
            entity.HasKey(e => e.Mpc).HasName("PK__PCGV__C7908DE0DEF131DE");

            entity.ToTable("PCGV");

            entity.Property(e => e.Mpc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MPC");
            entity.Property(e => e.Mdntt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDNTT");
            entity.Property(e => e.Mgv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MGV");
            entity.Property(e => e.Mlhp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MLHP");
            entity.Property(e => e.Phanloai)
                .HasMaxLength(255)
                .HasColumnName("PHANLOAI");

            entity.HasOne(d => d.MdnttNavigation).WithMany(p => p.Pcgvs)
                .HasForeignKey(d => d.Mdntt)
                .HasConstraintName("FK__PCGV__MDNTT__6EF57B66");

            entity.HasOne(d => d.MgvNavigation).WithMany(p => p.Pcgvs)
                .HasForeignKey(d => d.Mgv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PCGV__MGV__6FE99F9F");

            entity.HasOne(d => d.MlhpNavigation).WithMany(p => p.Pcgvs)
                .HasForeignKey(d => d.Mlhp)
                .HasConstraintName("FK__PCGV__MLHP__70DDC3D8");
        });

        modelBuilder.Entity<Sinhvien>(entity =>
        {
            entity.HasKey(e => e.Msv).HasName("PK__SINHVIEN__C790E5AC43ADAAD1");

            entity.ToTable("SINHVIEN");

            entity.HasIndex(e => e.Email, "UQ__SINHVIEN__161CF724DDB3AD4C").IsUnique();

            entity.HasIndex(e => e.Sdt, "UQ__SINHVIEN__CA1930A58652407B").IsUnique();

            entity.Property(e => e.Msv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MSV");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Lop)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOP");
            entity.Property(e => e.Makhoa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAKHOA");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Tinhtrang)
                .HasMaxLength(255)
                .HasColumnName("TINHTRANG");

            entity.HasOne(d => d.MakhoaNavigation).WithMany(p => p.Sinhviens)
                .HasForeignKey(d => d.Makhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SINHVIEN__MAKHOA__71D1E811");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Mtk).HasName("PK__TAIKHOAN__C797915D55F5428C");

            entity.ToTable("TAIKHOAN");

            entity.HasIndex(e => e.Username, "UQ__TAIKHOAN__B15BE12E1B505D81").IsUnique();

            entity.Property(e => e.Mtk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MTK");
            entity.Property(e => e.Mcbk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MCBK");
            entity.Property(e => e.Mdn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MDN");
            entity.Property(e => e.Mgv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MGV");
            entity.Property(e => e.Msv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MSV");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Quyen)
                .HasMaxLength(255)
                .HasColumnName("QUYEN");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Vaitro)
                .HasMaxLength(255)
                .HasDefaultValue("User")
                .HasColumnName("VAITRO");

            entity.HasOne(d => d.McbkNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.Mcbk)
                .HasConstraintName("FK__TAIKHOAN__MCBK__72C60C4A");

            entity.HasOne(d => d.MdnNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.Mdn)
                .HasConstraintName("FK__TAIKHOAN__MDN__73BA3083");

            entity.HasOne(d => d.MgvNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.Mgv)
                .HasConstraintName("FK__TAIKHOAN__MGV__74AE54BC");

            entity.HasOne(d => d.MsvNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.Msv)
                .HasConstraintName("FK__TAIKHOAN__MSV__75A278F5");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TOKEN__3214EC273F218C62");

            entity.ToTable("TOKEN");

            entity.HasIndex(e => e.Rk, "UQ__TOKEN__321537C40D0E4CCE").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Mtk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MTK");
            entity.Property(e => e.Rk)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RK");
            entity.Property(e => e.Thoihan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("THOIHAN");

            entity.HasOne(d => d.MtkNavigation).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.Mtk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TOKEN__MTK__76969D2E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
