using System;
using System.Collections.Generic;

namespace insurance.Models.Db;

public partial class Policy
{
    public int PolicyId { get; set; }

    public string PolicyType { get; set; } = null!;

    public string PolicyName { get; set; } = null!;

    public decimal InsuranceAmount { get; set; }

    public int PolicyValidity { get; set; }

    public string PolicyDescription { get; set; } = null!;

    public string? Available { get; set; }

    public virtual ICollection<PolicySold> PolicySolds { get; set; } = new List<PolicySold>();
}
