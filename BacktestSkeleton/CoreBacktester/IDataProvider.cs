namespace CoreBacktester;
using PricingLibrary.MarketDataFeed;
public interface IDataProvider
{
    public DataFeed[] extractor();


}