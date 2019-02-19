using LSTM.AuthenticationContext.Domain.Commands;
using LSTM.AuthenticationContext.Domain.Handlers;
using LSTM.AuthenticationContext.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LSTM.AuthenticationContext.Tests.Handlers
{
    [TestClass]
    public class AuthenticationHandlerTests
    {
        [TestMethod]
        [TestCategory("Handler - Authentication")]
        public void Should_Authenticate_When_Password_Matches()
        {

            var command = new AuthenticateUserCommand();
            command.Username = "alexandre";
            command.Password = "123456";


            var handler = new AuthenticationHandler(new FakeUserRepository());
            handler.Handle(command);
            
            Assert.AreEqual(true, handler.Valid);
        }

        [TestMethod]
        [TestCategory("Handler - Authentication")]
        public void Should_Not_Authenticate_When_Password_Does_Not_Match()
        {

            var command = new AuthenticateUserCommand();
            command.Username = "alexandre";
            command.Password = "123";


            var handler = new AuthenticationHandler(new FakeUserRepository());
            handler.Handle(command);
            
            Assert.AreEqual(false, handler.Valid);
        }
    }
}
