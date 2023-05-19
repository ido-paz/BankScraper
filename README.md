# BankScraper

* Usefull to create a report from a bank account balance changes in dates range.

* When executing add parameters in the following order: [bankName] [tzID] [password] [id] [fromDate] [toDate] [output file name

-------------------------
* Logic

1. The report is created after passing n pages.
2. The report contains a variable that contains a collection of pages on the way to the report.
3. The page are excuted in theh order they were added to the collection.
4. Each page contains a URL, and a collection of input fields containing a selector and a value.
5. The page contains a button that is clicked after filling in the input fields.
6. The report is created after the last page is executed.
7. The report is saved in the output file.