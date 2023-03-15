using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsManager
{
    public class Email
    {
        public string Value { get; private set; }

        protected Email() { }
        public Email(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (!IsCorrectEmail(email))
            {
                throw new ArgumentException("Некорректный адрес почты!", nameof(email));
            }
            Value = email;
        }

        private static bool IsCorrectEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return obj is Email email && Value == email.Value;
        }
        public static bool operator ==(Email? left, Email? right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(Email? left, Email? right)
        {
            return left.Value != right.Value;
        }
    }
}
