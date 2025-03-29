using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Giangvien
{
    public string Mgv { get; set; } = null!;

    public string Hoten { get; set; } = null!;

    public string Makhoa { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public virtual ICollection<Dntt> Dntts { get; set; } = new List<Dntt>();

    public virtual Khoa MakhoaNavigation { get; set; } = null!;

    public virtual ICollection<Pcgv> Pcgvs { get; set; } = new List<Pcgv>();

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
