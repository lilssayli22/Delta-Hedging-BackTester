namespace CoreBacktester;
using PricingLibrary.MarketDataFeed;

public class RegularOracle : IRebalancingOracle
{
    public int _period{get;set;}
    public int conteur{get ;private set;}

    public RegularOracle(int period)
    {
        _period = period;
        conteur =1;
    }

    public bool Rebalancing(DataFeed feed)
    {
        if (conteur%_period == 0)
        {conteur+=1;
            return true;
        }
        else
        {conteur+=1;
            return false;
        }
    }
}