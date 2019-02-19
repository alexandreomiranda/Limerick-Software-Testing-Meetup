using Flunt.Notifications;
using Flunt.Validations;

namespace LSTM.AuthenticationContext.Domain.ValueObjects
{
    public class Email : Notifiable
    {

        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Address, "Email.Address", "Invalid E-mail")
            );
        }

        public string Address { get; private set; }

    }
}
