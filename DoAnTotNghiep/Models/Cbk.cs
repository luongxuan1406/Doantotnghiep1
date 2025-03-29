using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Cbk
{
    public string Mcbk { get; set; } = null!;

    public string Hoten { get; set; } = null!;

    public string Makhoa { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public virtual Khoa MakhoaNavigation { get; set; } = null!;

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
