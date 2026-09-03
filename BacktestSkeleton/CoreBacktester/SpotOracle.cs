using PricingLibrary.MarketDataFeed;

namespace CoreBacktester;

public class SpotOracle : IRebalancingOracle
{
    public double seuil { get; private set; }
    private Dictionary<string, double> spots_prev;

    public SpotOracle(double seuil)
    {
        this.seuil = seuil;
        spots_prev = null;
    }

    public bool Rebalancing(DataFeed feed)
    {
        if (spots_prev == null)
        {
            spots_prev = new Dictionary<string, double>(feed.PriceList);
            return true;
        }

        foreach (var kv in feed.PriceList)
        {
            if (Math.Abs(kv.Value - spots_prev[kv.Key]) > seuil)
            {
                spots_prev = new Dictionary<string, double>(feed.PriceList);
                return true;
            }
        }
        return false;
    }
}