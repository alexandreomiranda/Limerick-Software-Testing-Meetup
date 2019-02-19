using LSTM.AuthenticationContext.Domain.Commands;

namespace LSTM.AuthenticationContext.Domain.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
