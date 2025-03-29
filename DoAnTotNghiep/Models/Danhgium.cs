using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Danhgium
{
    public string Msv { get; set; } = null!;

    public string Nguoidg { get; set; } = null!;

    public DateTime Ngaydg { get; set; }

    public string Tieude { get; set; } = null!;

    public string Noidung { get; set; } = null!;

    public string? Linkfile { get; set; }

    public virtual Sinhvien MsvNavigation { get; set; } = null!;
}
