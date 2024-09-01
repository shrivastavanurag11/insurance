﻿using Microsoft.AspNetCore.Authorization;

namespace insurance.Models
{
    public class SecurityPolicy
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static AuthorizationPolicy AdminPolicy()
        {
            var builder = new AuthorizationPolicyBuilder();
            AuthorizationPolicy policy = builder.RequireAuthenticatedUser().RequireRole(Admin).Build();
            return policy;
        }

        public static AuthorizationPolicy UserPolicy()
        {
            var builder = new AuthorizationPolicyBuilder();
            AuthorizationPolicy policy = builder.RequireAuthenticatedUser().RequireRole(User).Build();
            return policy;
        }
    }
}
