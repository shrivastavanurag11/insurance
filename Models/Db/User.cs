using System;
using System.Collections.Generic;

namespace insurance.Models.Db;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string Email { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public byte[]? CustomerImage { get; set; }

    public virtual ICollection<BeneficiaryDetail> BeneficiaryDetails { get; set; } = new List<BeneficiaryDetail>();

    public virtual ICollection<PolicySold> PolicySolds { get; set; } = new List<PolicySold>();
}
