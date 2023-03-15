using System;
using System.Text.RegularExpressions;

namespace StudentsManager
{
    public class Passport
    {
        public string Value { get; set; }

        protected Passport() { }
        public Passport(string passport)
        {
            if (passport is null)
            {
                throw new ArgumentNullException(nameof(passport));
            }
            if (!IsCorrectPassport(passport))
            {
                throw new ArgumentException("Некорректные паспортные данные!", nameof(passport));
            }
            Value = passport;
        }
        public bool IsCorrectPassport(string passport)
        {
            string pattern = "^\\d{10,10}(?:\\.\\d{0,9})?$";
            Regex regexPassport = new Regex(pattern);
            return regexPassport.IsMatch(passport);
        }
        public override string ToString()
        {
            return Value;
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        protected bool Equals(Passport secondPassport)
        {
            return Value == secondPassport.Value;
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Passport)obj);
        }
        public static bool operator ==(Passport? left, Passport? right)
        {
            return left.Value == right.Value;
        }
        public static bool operator !=(Passport? left, Passport? right)
        {
            return left.Value != right.Value;
        }
    }
}
