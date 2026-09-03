using System.Globalization;
using PricingLibrary.MarketDataFeed;

namespace CoreBacktester;

public class CsvDataProvider : IDataProvider
{
    public String FilePath { get; private set; }

    public CsvDataProvider(String filePath)
    {
        FilePath = filePath;
    }

    public DataFeed[] extractor()
    {
        return File.ReadAllLines(FilePath)
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(','))
            .Select(values => new ShareValue
            {
                Id = values[0],
                DateOfPrice = DateTime.ParseExact(
                    values[1], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Value = double.Parse(values[2], CultureInfo.InvariantCulture)
            })
            .GroupBy(sv => sv.DateOfPrice)
            .OrderBy(g => g.Key)
            .Select(g => new DataFeed(
                g.Key,
                g.ToDictionary(sv => sv.Id, sv => sv.Value)))
            .ToArray();
    }
}


