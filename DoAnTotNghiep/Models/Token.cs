using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Token
{
    public string Id { get; set; } = null!;

    public string Mtk { get; set; } = null!;

    public string Rk { get; set; } = null!;

    public DateTime? Thoihan { get; set; }

    public virtual Taikhoan MtkNavigation { get; set; } = null!;
}
