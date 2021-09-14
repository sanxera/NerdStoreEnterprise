using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects
{
    public class Email
    {
        public const int EmailMaxLength = 254;
        public const int EmailMinLength = 5;
        public string AddressEmail { get; private set; }

        //Construtor do EntityFramework
        protected Email() { }

        public Email(string addressEmail)
        {
            if (!Validate(addressEmail)) throw new DomainException("E-mail inválido");
            AddressEmail = addressEmail;
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}