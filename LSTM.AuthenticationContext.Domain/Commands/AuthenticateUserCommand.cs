using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSTM.AuthenticationContext.Domain.Commands
{
    public class AuthenticateUserCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
