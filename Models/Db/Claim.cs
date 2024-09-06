using System;
using System.Collections.Generic;

namespace insurance.Models.Db;

public partial class Claim
{
    public int ClaimId { get; set; }

    public int? PurchaseId { get; set; }

    public decimal? ClaimAmount { get; set; }

    public decimal? RemainingAmount { get; set; }

    public DateTime? ClaimDate { get; set; }

    public virtual PolicySold? Purchase { get; set; }
}
