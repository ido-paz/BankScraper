using Microsoft.Playwright;

namespace BankScraper.BalanceChange
{
    public abstract class ABalanceChangeManager
    {
        public async Task<List<BalanceChangeRow>> Get(DateTime fromDate, DateTime toDate, string tzId, string tzPassword, string aidnum)
        {
            var locators = await GetLocators(fromDate, toDate, tzId, tzPassword, aidnum);
            return await Get(locators);
        }

        public async Task<List<BalanceChangeRow>> Get(ReportArguments ra)
        {
            var locators = await GetLocators(ra);
            return await Get(locators);
        }

        protected abstract Task<BalanceChangeRow?> Get(ILocator row);

        protected async Task<List<BalanceChangeRow>> Get(IReadOnlyList<ILocator>? locatorList)
        {
            if (locatorList == null)
                throw new ArgumentNullException(nameof(locatorList));
            List<BalanceChangeRow> rows = new List<BalanceChangeRow>();
            foreach (var item in locatorList)
                rows.Add(await Get(item));
            return rows;
        }

        protected abstract Task<IReadOnlyList<ILocator>> GetLocators(DateTime fromDate, DateTime toDate, string tzId, string tzPassword, string aidnum);

        protected async Task<IReadOnlyList<ILocator>> GetLocators(ReportArguments ra)
        {
            return await GetLocators(ra.FromDate, ra.ToDate, ra.User.TzId, ra.User.TzPassword, ra.User.Aidnum);
        }

        protected async Task<string?> GetTrimmedValue(ILocator locator)
        {
            if (locator != null)
            {
                string? temp = await locator.TextContentAsync();
                if (temp != null)
                    return temp.Trim();
            }
            return null;
        }

    }
}
