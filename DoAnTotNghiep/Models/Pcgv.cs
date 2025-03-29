using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Pcgv
{
    public string Mpc { get; set; } = null!;

    public string Mgv { get; set; } = null!;

    public string? Mlhp { get; set; }

    public string? Mdntt { get; set; }

    public string Phanloai { get; set; } = null!;

    public virtual Dntt? MdnttNavigation { get; set; }

    public virtual Giangvien MgvNavigation { get; set; } = null!;

    public virtual Lhp? MlhpNavigation { get; set; }
}
