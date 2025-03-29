using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Dktt
{
    public string Mdk { get; set; } = null!;

    public string Msv { get; set; } = null!;

    public string? Mlhp { get; set; }

    public string? Mdnnt { get; set; }

    public string? Mdnsv { get; set; }

    public string Htdk { get; set; } = null!;

    public DateTime Tgbd { get; set; }

    public DateTime Tgkt { get; set; }

    public virtual Dnnt? MdnntNavigation { get; set; }

    public virtual Dnsv? MdnsvNavigation { get; set; }

    public virtual Lhp? MlhpNavigation { get; set; }

    public virtual Sinhvien MsvNavigation { get; set; } = null!;
}
