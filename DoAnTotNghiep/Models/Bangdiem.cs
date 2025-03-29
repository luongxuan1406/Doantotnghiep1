using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Bangdiem
{
    public string Msv { get; set; } = null!;

    public decimal? Diemh1l1 { get; set; }

    public decimal? Diemh1l2 { get; set; }

    public decimal? Diemh1l3 { get; set; }

    public decimal? Diemh2l1 { get; set; }

    public decimal? Diemh2l2 { get; set; }

    public decimal? Diemh2l3 { get; set; }

    public decimal? Diemck { get; set; }

    public decimal? Diemtb { get; set; }

    public string? Dat { get; set; }

    public virtual Sinhvien MsvNavigation { get; set; } = null!;
}
