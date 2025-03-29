﻿using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Dnsv
{
    public string Mdn { get; set; } = null!;

    public string Tdn { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Ttnlh { get; set; } = null!;

    public string? Thl { get; set; }

    public string? Lydo { get; set; }

    public virtual ICollection<Dktt> Dktts { get; set; } = new List<Dktt>();

    public virtual ICollection<Dntt> Dntts { get; set; } = new List<Dntt>();
}
