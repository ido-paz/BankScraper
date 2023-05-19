using BankScraper.BalanceChange;

namespace BankScraper
{
    public enum ProgramResult { Success = 0, Error = -1 };

    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                Output.ToConsoleLine($"Started");
                //
                ReportArguments ra = new ReportArguments(args);
                BalanceChangeManagerFactory bcmf = new BalanceChangeManagerFactory();
                ABalanceChangeManager dbcm = bcmf.Get(ra.User.BankName);
                //
                Output.ToConsoleLine($"Trying to create report with parameters : {ra}");
                //
                var rows = await dbcm.Get(ra);
                if (rows == null)
                    Output.ToConsoleLine($"No rows were found");
                else
                {
                    if (!string.IsNullOrEmpty(ra.JsonFileName))
                    {
                        Output.ToConsoleLine($"Creating {ra.JsonFileName} file");
                        Output.ToJSONFile<List<BalanceChangeRow>>(rows, ra.JsonFileName);
                    }
                    else
                    {
                        Output.ToConsoleLine($"Printing rows");
                        Output.ToConsoleLines("OperationDate | Description | Amount | Balance", 
                                                  rows.Select(row => $"{row.OperationDate} {row.Description} {row.Amount} {row.Balance}"));
                    }
                    Output.ToConsoleLine($"Found {rows.Count()} rows");
                }
                //            
                Output.ToConsoleLine($"Ended successfuly");
                return (int)ProgramResult.Success;
            }
            catch (Exception e)
            {
                Output.ToConsoleLine($"Ended with exception message:{e.Message}");
                return (int)ProgramResult.Error; ;
            }
        }

    }
}