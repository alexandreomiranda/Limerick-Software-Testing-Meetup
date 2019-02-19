using Flunt.Notifications;
using LSTM.AuthenticationContext.Domain.Commands;
using LSTM.AuthenticationContext.Domain.Repositories;

namespace LSTM.AuthenticationContext.Domain.Handlers
{
    public class AuthenticationHandler : Notifiable, IHandler<AuthenticateUserCommand>
    {
        private readonly IUserRepository _repository;

        public AuthenticationHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AuthenticateUserCommand command)
        {
            // Check if username exists
            var user = _repository.CheckUsername(command.Username);
            if (user == null)
                AddNotification("Username", "Invalid username");

            user.Authenticate(command.Password);

            // Grouping domain notifications
            AddNotifications(user);

            // Checking notifications
            if (Invalid)
                return new CommandResult(false, "User not authenticated", Notifications);

            return new CommandResult(true, "User authenticated succesfully", user.Name);
        }
        
    }
}
