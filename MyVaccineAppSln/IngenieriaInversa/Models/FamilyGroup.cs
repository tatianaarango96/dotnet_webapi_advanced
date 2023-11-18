using System;
using System.Collections.Generic;

namespace IngenieriaInversa.Models;

public partial class FamilyGroup
{
    public int FamilyGroupId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<User> UsersUsers { get; } = new List<User>();
}
