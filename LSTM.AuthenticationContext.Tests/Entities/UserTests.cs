using LSTM.AuthenticationContext.Domain.Entities;
using LSTM.AuthenticationContext.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LSTM.AuthenticationContext.Tests.Entities
{
    [TestClass]
    public class UserTests
    {
        private readonly Email _email;
        private readonly Email _invalidEmail;
        
        public UserTests()
        {
            _email = new Email("alexandre@alexandreomiranda.com");
            _invalidEmail = new Email("alexandre@alexandreomiranda");
        }

        [TestMethod]
        [TestCategory("User - New User")]
        public void Should_Return_Error_When_Name_Is_Invalid()
        {
            var user = new User("", "alexandre", "123456", _email);
            Assert.IsTrue(user.Invalid);
        }

        [TestMethod]
        [TestCategory("User - New User")]
        public void Should_Return_Error_When_Username_Is_Invalid()
        {
            var user = new User("Alexandre Miranda", "", "123456", _email);
            Assert.IsTrue(user.Invalid);
        }

        [TestMethod]
        [TestCategory("User - New User")]
        public void Should_Return_Error_When_Password_Is_Invalid()
        {
            var user = new User("Alexandre Miranda", "alexandre", "", _email);
            Assert.IsTrue(user.Invalid);
        }

        [TestMethod]
        [TestCategory("User - New User")]
        public void Should_Return_Error_When_Email_Is_Invalid()
        {
            var user = new User("Alexandre", "alexandre", "123456", _invalidEmail);
            Assert.IsTrue(_invalidEmail.Invalid);
        }

        [TestMethod]
        [TestCategory("User - New User")]
        public void Should_Return_Success_When_User_Is_Valid()
        {
            var user = new User("Alexandre Miranda", "alexandre", "123456", _email);
            Assert.IsTrue(user.Valid);
        }


        
        [TestMethod]
        [TestCategory("User - Authenticating User")]
        public void Should_Return_Failure_When_Authenticate_A_Invalid_Password()
        {
            var user = new User("Alexandre Miranda", "alexandre", "123456", _email);
            var authenticationReturn = user.Authenticate("456456");
            Assert.IsTrue(authenticationReturn.Equals(Domain.Enums.ELoginStatus.Failure));
        }

        [TestMethod]
        [TestCategory("User - Authenticating User")]
        public void Should_Return_LockedOut_When_Authenticate_A_Locked_User()
        {
            var user = new User("Alexandre Miranda", "alexandre", "123456", _email);
            user.Lock();
            var authenticationReturn = user.Authenticate("123456");
            Assert.IsTrue(authenticationReturn.Equals(Domain.Enums.ELoginStatus.LockedOut));
        }

        [TestMethod]
        [TestCategory("User - Authenticating User")]
        public void Should_Return_Failure_When_Authenticate_A_Deactivated_User()
        {
            var user = new User("Alexandre Miranda", "alexandre", "123456", _email);
            user.Deactivate();
            var authenticationReturn = user.Authenticate("123456");
            Assert.IsTrue(authenticationReturn.Equals(Domain.Enums.ELoginStatus.Failure));
        }

        [TestMethod]
        [TestCategory("User - Authenticating User")]
        public void Should_Return_Success_When_Authenticate_A_Valid_User()
        {
            var user = new User("Alexandre Miranda", "alexandre", "123456", _email);
            var authenticationReturn = user.Authenticate("123456");
            Assert.IsTrue(authenticationReturn.Equals(Domain.Enums.ELoginStatus.Success));
        }


        

    }
}
