using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace BankScraper
{
    public class Output
    {
        //
        public static void ToConsoleLine(string message)
        {
            Console.Out.WriteLine($"{DateTime.Now.ToLongTimeString()}:{message}");
        }
        //
        public static void ToConsoleLines(string titleRow, IEnumerable<string> rows)
        {
            if (!string.IsNullOrEmpty(titleRow))
                Console.Out.WriteLine(titleRow);
            foreach (var row in rows)
                Console.Out.WriteLine(row);
        }
        //
        public static void ToJSONFile<T>(T data, string fileName)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            var jso = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize<T>(data, jso);
            File.WriteAllText(fileName, json, Encoding.UTF8);
        }
    }
}
