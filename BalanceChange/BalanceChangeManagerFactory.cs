namespace BankScraper.BalanceChange;

public class BalanceChangeManagerFactory
{
	public ABalanceChangeManager Get(string bankName)
	{
        if (bankName.ToLower().Contains("discount"))
            return new DiscountBalanceChangeManager();
        else
			throw new ArgumentException("invalid bankName");
	}
}