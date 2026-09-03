using PricingLibrary.MarketDataFeed;
using PricingLibrary.DataClasses;
using PricingLibrary.Computations;

namespace CoreBacktester;

public class PricingService
{
    private readonly Pricer _pricer;
    private readonly string[] _ids;

    public PricingService(BasketTestParameters parameters)
    {
        _pricer = new Pricer(parameters);
        _ids = _pricer.UnderlyingShareIds;
    }

    public PricingResults Price(DataFeed feed)
    {
        return _pricer.Price(feed.Date, SpotConverter.ToArray(_ids, feed.PriceList));
    }

    public Dictionary<string, double> ToDeltas(double[] deltas)
    {
        return SpotConverter.ToDictionary(_ids, deltas);
    }
}