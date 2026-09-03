using PricingLibrary.MarketDataFeed;
using PricingLibrary.DataClasses;
using PricingLibrary.Computations;

namespace CoreBacktester;

public class Backtester
{
    public OutputData[] Run(BasketTestParameters p, DataFeed[] data, IRebalancingOracle oracle)
{
    var pricing = new PricingService(p);
    var res = pricing.Price(data[0]);
    var portfolio = new Portfolio(res.Price, pricing.ToDeltas(res.Deltas), data[0]);

    OutputData[] outputData = new OutputData[data.Length];
    outputData[0] = new OutputData
    {
        Date = data[0].Date,
        Deltas = res.Deltas,
        DeltasStdDev = res.DeltaStdDev,
        Price = res.Price,
        PriceStdDev = res.PriceStdDev,
        Value = res.Price,
        TransactionCosts = 0.0
    };

    for (int i = 1; i < data.Length; i++)
    {
        double valeur = portfolio.GetValue(data[i]);
        res = pricing.Price(data[i]);

        if (oracle.Rebalancing(data[i]))
            portfolio.ReFormePortfolio(data[i], pricing.ToDeltas(res.Deltas));

        outputData[i] = new OutputData
        {
            Date = data[i].Date,
            Deltas = res.Deltas,
            DeltasStdDev = res.DeltaStdDev,
            Price = res.Price,
            PriceStdDev = res.PriceStdDev,
            Value = valeur,
            TransactionCosts = 0.0
        };
    }

    return outputData;
}
}