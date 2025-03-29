using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Taikhoan
{
    public string Mtk { get; set; } = null!;

    public string? Mcbk { get; set; }

    public string? Mgv { get; set; }

    public string? Mdn { get; set; }

    public string? Msv { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Vaitro { get; set; }

    public string? Quyen { get; set; }

    public virtual Cbk? McbkNavigation { get; set; }

    public virtual Dntt? MdnNavigation { get; set; }

    public virtual Giangvien? MgvNavigation { get; set; }

    public virtual Sinhvien? MsvNavigation { get; set; }

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
}
