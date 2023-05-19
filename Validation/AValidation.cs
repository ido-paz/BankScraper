using BankScraper.Validation;

namespace BankScraper.Validation
{
    public abstract class AValidation
    {
        public string Value { get; set; }

        public AValidation(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} has value of : {Value}";
        }

        public abstract bool IsValid();
    }

    public class EmailValidation : AValidation
    {
        public EmailValidation(string value):base(value)
        {}

        public override bool IsValid()
        {
            if (Value.Length > 0 && Value.Contains("@"))
                return true;
            return false;
        }
    }
}


