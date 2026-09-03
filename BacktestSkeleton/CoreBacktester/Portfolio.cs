using PricingLibrary.MarketDataFeed;
namespace CoreBacktester;

public class Portfolio : IPortfolio
{
    private double _cash;
    private Dictionary<string, double> _positions;
    private DateTime _lastUpdate;

    public double CashValue => _cash;

    public Portfolio(double prime, Dictionary<string, double> deltas, DataFeed feed)
{
    _positions = new Dictionary<string, double>(deltas);
    _cash = prime - SpotConverter.DotProduct(_positions, feed.PriceList);
    _lastUpdate = feed.Date;
}

public double GetValue(DataFeed feed)
{
    return _cash * RiskFreeRateProvider.GetRiskFreeRateAccruedValue(_lastUpdate, feed.Date)
         + SpotConverter.DotProduct(_positions, feed.PriceList);
}

public void ReFormePortfolio(DataFeed feed, Dictionary<string, double> deltas)
{
    _cash = GetValue(feed);
    _lastUpdate = feed.Date;
    _positions = new Dictionary<string, double>(deltas);
    _cash -= SpotConverter.DotProduct(_positions, feed.PriceList);
}
}