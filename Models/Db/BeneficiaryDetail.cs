using System;
using System.Collections.Generic;

namespace insurance.Models.Db;

public partial class BeneficiaryDetail
{
    public int BeneficiaryId { get; set; }

    public string? UserName { get; set; }

    public string Name { get; set; } = null!;

    public string Relationship { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? ContactNumber { get; set; }

    public virtual User? UserNameNavigation { get; set; }
}
