namespace BankScraper
{
    public class User
    {
        public string BankName { get; set; }
        public string TzId { get; set; }
        public string TzPassword { get; set; }
        public string Aidnum { get; set; }

        public User() { }

        public User(string bankName, string tzId, string tzPassword, string aidnum)
        {
            BankName = bankName;
            TzId = tzId;
            TzPassword = tzPassword;
            Aidnum = aidnum;
        }

        public User(string[] args):this(args[0] , args[1], args[2], args[3]){}
    }
}
