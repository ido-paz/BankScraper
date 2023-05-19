namespace BankScraper
{
    public class ReportArguments
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string JsonFileName { get;set; }
        public User User { get; set; }
        //
        public ReportArguments() { }
        //
        public ReportArguments(string[] args)
        {
            DateTime tempDateTime;
            if (args.Length < 6)
                throw new ArgumentException("Usage: BankScraper.exe <BankName> <tzId> <tzPassword> <aidnum> <fromDate> <toDate> [<JsonFileName>]");            
            //
            User = new User(args);
            //
            if (DateTime.TryParse(args[4],out tempDateTime))
                FromDate = DateTime.Parse(args[4]);
            if (DateTime.TryParse(args[5], out tempDateTime))
                ToDate = DateTime.Parse(args[5]);
            if (args.Length == 7 && !string.IsNullOrEmpty(args[6]))
                JsonFileName = args[6];
        }

        public override string ToString()
        {
            return $"{User.BankName} {User.TzId} {User.TzPassword} {User.Aidnum} {FromDate.ToShortDateString()} {ToDate.ToShortDateString()} {JsonFileName}";            
        }
    }
}
