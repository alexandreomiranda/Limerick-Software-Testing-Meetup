using Flunt.Notifications;
using Flunt.Validations;
using LSTM.AuthenticationContext.Domain.Enums;
using LSTM.AuthenticationContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSTM.AuthenticationContext.Domain.Entities
{
    public class User : Notifiable
    {
        #region Ctor
        public User(string name, string username, string password, Email email)
        {
            UserId = Guid.NewGuid();
            Name = name;
            Username = username;
            Password = EncryptPassword(password);
            Email = email;
            Active = true;
            Locked = false;
            Role = ERole.User;
            DateRegistered = DateTime.Now;
            DateModified = null;
            LastLoginDate = null;
            ExpirationDatePassword = DateTime.Now.AddDays(90);

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Name, 3, "Name", "The name must be at least three characters long.")
                .HasMinLen(Username, 3, "Username", "The username must be at least three characters long.")
                .HasMinLen(Password, 6, "Password", "The password must be at least six characters long.")
            );
        }
        #endregion

        #region Properties
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public Email Email { get; private set; }
        public bool Active { get; private set; }
        public bool Locked { get; private set; }
        public ERole Role { get; private set; }
        public DateTime DateRegistered { get; private set; }
        public DateTime? DateModified { get; private set; }
        public DateTime? LastLoginDate { get; private set; }
        public DateTime ExpirationDatePassword { get; private set; }

        #endregion

        #region Methods
        public void Activate() => Active = true;
        public void Deactivate() => Active = false;
        public void Lock() => Locked = true;
        public void Unlock()
        {
            Locked = false;
            ExpirationDatePassword = DateTime.Now.AddDays(90);
        }
        
        public void GrantAdmin() => Role = ERole.Admin;
        public void RevokeAdmin() => Role = ERole.User;

        public string ResetPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 6);
        }

        public void ChangePassword(string password)
        {
            Unlock();
            Password = password;
        }
        public ELoginStatus Authenticate(string password)
        {
            if (!Active)
            {
                AddNotification("User", "Deactivated user");
                return ELoginStatus.Failure;
            }
            if (Locked)
            {
                AddNotification("User", "Locked user");
                return ELoginStatus.LockedOut;
            }
            if (ExpirationDatePassword < DateTime.Now)
            {
                Lock();
                AddNotification("User", "Locked user");
                return ELoginStatus.LockedOut;
            }

            if (Password == EncryptPassword(password))
            {
                LastLoginDate = DateTime.Now;
                return ELoginStatus.Success;
            }
            else
            {
                AddNotification("User", "Login failed");
                return ELoginStatus.Failure;
            }

        }

        private string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var password = (pass += "|8767a67c-24cf-42cd-ae2b-21bb2e739b39");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
        #endregion
    }
}
