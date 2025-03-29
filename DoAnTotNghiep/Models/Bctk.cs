using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Bctk
{
    public string Mbctk { get; set; } = null!;

    public string Msv { get; set; } = null!;

    public DateTime Ngaynop { get; set; }

    public string Tieude { get; set; } = null!;

    public string Noidung { get; set; } = null!;

    public string? Linkfile { get; set; }

    public virtual Sinhvien MsvNavigation { get; set; } = null!;
}
