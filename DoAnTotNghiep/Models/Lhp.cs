using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Lhp
{
    public string Mlhp { get; set; } = null!;

    public string Makhoa { get; set; } = null!;

    public virtual ICollection<Dktt> Dktts { get; set; } = new List<Dktt>();

    public virtual Khoa MakhoaNavigation { get; set; } = null!;

    public virtual ICollection<Pcgv> Pcgvs { get; set; } = new List<Pcgv>();
}
