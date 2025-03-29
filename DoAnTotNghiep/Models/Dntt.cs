using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Dntt
{
    public string Mdntt { get; set; } = null!;

    public string? Mdnnt { get; set; }

    public string? Mdnsv { get; set; }

    public string Mgv { get; set; } = null!;

    public virtual Dnnt? MdnntNavigation { get; set; }

    public virtual Dnsv? MdnsvNavigation { get; set; }

    public virtual Giangvien MgvNavigation { get; set; } = null!;

    public virtual ICollection<Pcgv> Pcgvs { get; set; } = new List<Pcgv>();

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
