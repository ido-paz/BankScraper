using Microsoft.Playwright;

namespace BankScraper.BalanceChange
{
    public class DiscountBalanceChangeManager : ABalanceChangeManager
    {
        const string LOGIN_PAGE_URL = "https://start.telebank.co.il/login/#/LOGIN_PAGE";
        const string OSH_PAGE_URL = "https://start.telebank.co.il/apollo/retail/#/OSH_LENTRIES_ALTAMIRA";

        #region Selectors
        const string USER_ID_SELECTOR = "#tzId";
        const string USER_PASSWORD_SELECTOR = "#tzPassword";
        const string USER_ADDITIONAL_ID_SELECTOR = "#aidnum";

        const string OSH_FROMDATE_SELECTOR = "input#fromDate";
        const string OSH_TODATE_SELECTOR = "input#oshTransfersAdvancedSearchDateTO";

        const string ADVANCED_SEARCH_BUTTON_SELECTOR = "button.advanced-search-window-btn";
        const string LAST_TRANSACTION_TABLE_SELECTOR = "#lastTransactionTable [role=row]";
        const string COLUMN_SELECTOR = "[role=gridcell]";

        const string SUBMIT_SELECTOR = "button[type='submit']";

        #endregion

        const string ENTER = "Enter";
        const int COLUMN_COUNT = 7;
        const string DATEFORMAT = "dd/MM/yyyy";

        protected override async Task<BalanceChangeRow?> Get(ILocator row)
        {
            BalanceChangeRow ar = new BalanceChangeRow();
            var locator = await row.Locator(COLUMN_SELECTOR).AllAsync();
            if (locator != null && locator.Count == COLUMN_COUNT)
            {
                ar.OperationDate = await GetTrimmedValue(locator[0]);
                ar.ValueDate = await GetTrimmedValue(locator[1]);
                ar.Description = await GetTrimmedValue(locator[2]);
                ar.ID = await GetTrimmedValue(locator[3]);
                ar.Amount = await GetTrimmedValue(locator[4]);
                ar.Balance = await GetTrimmedValue(locator[5]);
                return ar;
            }
            else
                throw new ArgumentException(nameof(row));
        }

        protected override async Task<IReadOnlyList<ILocator>> GetLocators(DateTime fromDate, DateTime toDate, string tzId, string tzPassword, string aidnum)
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();
            // Start tracing before creating / navigating a page.
            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
            var page = await context.NewPageAsync();
            //
            var fromDateStr = fromDate.ToString(DATEFORMAT);
            var toDateStr = toDate.ToString(DATEFORMAT);
            //
            try
            {
                await page.GotoAsync(LOGIN_PAGE_URL);
                await page.FillAsync(USER_ID_SELECTOR, tzId);
                await page.FillAsync(USER_PASSWORD_SELECTOR, tzPassword);
                await page.FillAsync(USER_ADDITIONAL_ID_SELECTOR, aidnum);
                await page.ClickAsync(SUBMIT_SELECTOR);
                //
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                //
                await page.GotoAsync(OSH_PAGE_URL);
                //
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                //
                await page.ClickAsync(ADVANCED_SEARCH_BUTTON_SELECTOR);
                //
                await page.FillAsync(OSH_FROMDATE_SELECTOR, fromDateStr);
                await page.Locator(OSH_FROMDATE_SELECTOR).PressAsync(ENTER);
                //
                await page.FillAsync(OSH_TODATE_SELECTOR, toDateStr);
                await page.Locator(OSH_TODATE_SELECTOR).PressAsync(ENTER);
                await page.ClickAsync(SUBMIT_SELECTOR);
            }
            catch (Exception pageException)
            {//for enabling stoping the trace
                throw pageException;
            }
            finally
            {
                await context.Tracing.StopAsync(new()
                {
                    Path = "trace.zip"
                });
            }
            //
            return await page.Locator(LAST_TRANSACTION_TABLE_SELECTOR).AllAsync();
        }
    }
}
