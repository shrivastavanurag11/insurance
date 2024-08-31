using System;
using System.Collections.Generic;

namespace insurance.Models.Db;

public partial class PolicySold
{
    public int PurchaseId { get; set; }

    public int? UserId { get; set; }

    public int? PolicyId { get; set; }

    public DateTime SoldDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Policy? Policy { get; set; }

    public virtual User? User { get; set; }
}
