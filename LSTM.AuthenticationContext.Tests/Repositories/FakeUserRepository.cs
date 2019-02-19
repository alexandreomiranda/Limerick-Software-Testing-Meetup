using LSTM.AuthenticationContext.Domain.Entities;
using LSTM.AuthenticationContext.Domain.Repositories;
using LSTM.AuthenticationContext.Domain.ValueObjects;

namespace LSTM.AuthenticationContext.Tests.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        public User CheckUsername(string username)
        {
            return new User("Alexandre Miranda", "alexandre", "123456", new Email("alex@alexandreomiranda.com"));
        }
    }
}
