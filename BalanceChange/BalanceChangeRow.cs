namespace BankScraper.BalanceChange
{
    public class BalanceChangeRow
    {
        public string? OperationDate { get; set; }
        public string? ValueDate { get; set; }
        public string? Description { get; set; }
        public string? ID { get; set; }
        public string? Amount { get; set; }
        public string? Balance { get; set; }

        public override string ToString()
        {
            return $"{OperationDate},{ValueDate},{Description},{ID},{Amount},{Balance}";
        }
    }
}
