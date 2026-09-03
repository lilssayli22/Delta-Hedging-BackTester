namespace CoreBacktester;
using PricingLibrary.MarketDataFeed;

public interface IPortfolio
{

    
    double GetValue(DataFeed feed);
    void ReFormePortfolio(DataFeed feed, Dictionary<string, double> deltas);
}