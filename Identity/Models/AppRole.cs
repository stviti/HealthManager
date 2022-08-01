using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() { }
        public AppRole(string name) { Name = name; }
    }
}