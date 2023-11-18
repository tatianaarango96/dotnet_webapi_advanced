using System;
using System.Collections.Generic;

namespace IngenieriaInversa.Models;

public partial class VaccineCategory
{
    public int VaccineCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Vaccine> VaccinesVaccines { get; } = new List<Vaccine>();
}
