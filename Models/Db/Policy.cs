using System;
using System.Collections.Generic;

namespace insurance.Models.Db;

public partial class Policy
{
    public int PolicyId { get; set; }

    public string PolicyName { get; set; } = null!;

    public string PolicyDescription { get; set; } = null!;

    public virtual ICollection<PolicySold> PolicySolds { get; set; } = new List<PolicySold>();
}
