using System;
using System.Collections.Generic;

namespace DoAnTotNghiep.Models;

public partial class Khoa
{
    public string Makhoa { get; set; } = null!;

    public string Tenkhoa { get; set; } = null!;

    public virtual ICollection<Cbk> Cbks { get; set; } = new List<Cbk>();

    public virtual ICollection<Giangvien> Giangviens { get; set; } = new List<Giangvien>();

    public virtual ICollection<Lhp> Lhps { get; set; } = new List<Lhp>();

    public virtual ICollection<Sinhvien> Sinhviens { get; set; } = new List<Sinhvien>();
}
