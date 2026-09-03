namespace CoreBacktester;
using PricingLibrary.MarketDataFeed;

public interface IRebalancingOracle
{
    public bool Rebalancing(DataFeed feed);
}