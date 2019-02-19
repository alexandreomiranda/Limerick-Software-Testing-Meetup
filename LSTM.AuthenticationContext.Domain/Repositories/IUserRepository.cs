using LSTM.AuthenticationContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSTM.AuthenticationContext.Domain.Repositories
{
    public interface IUserRepository
    {
        User CheckUsername(string username);
    }
}
