using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Sinhvien
{
    public string Msv { get; set; } = null!;

    public string Hoten { get; set; } = null!;

    public string Lop { get; set; } = null!;

    public string Makhoa { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string Tinhtrang { get; set; } = null!;

    public virtual Bangdiem? Bangdiem { get; set; }

    public virtual ICollection<Bctk> Bctks { get; set; } = new List<Bctk>();

    public virtual ICollection<Bct> Bcts { get; set; } = new List<Bct>();

    public virtual Danhgium? Danhgium { get; set; }

    public virtual ICollection<Dktt> Dktts { get; set; } = new List<Dktt>();

    public virtual Khoa MakhoaNavigation { get; set; } = null!;

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
