using System;

namespace LSTM.AuthenticationContext.Domain.Entities
{
    public class UserPermissions
    {
        public UserPermissions(Guid userId, string module, string level)
        {
            UserId = userId;
            Module = module;
            Level = level;
        }

        public Guid UserId { get; private set; }
        public string Module { get; private set; }
        public string Level { get; private set; }
    }
}
