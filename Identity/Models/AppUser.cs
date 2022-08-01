using System;
using Application.Contracts.Identity;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class AppUser : IdentityUser<Guid>, IAppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}