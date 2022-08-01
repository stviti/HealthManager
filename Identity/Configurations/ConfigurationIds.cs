using System;
namespace Identity.Configurations
{
    public static class ConfigurationIds
    {
        public static Guid AdminId { get; } = Guid.NewGuid();
        public static Guid UserId { get; } = Guid.NewGuid();

        public static Guid AdminRoleId { get; } = Guid.NewGuid();
        public static Guid UserRoleId { get; } = Guid.NewGuid();

    }
}
