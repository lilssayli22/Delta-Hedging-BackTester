using NUnit.Framework;
using CoreBacktester;
using PricingLibrary.MarketDataFeed;

namespace SkeletonTests;

[TestFixture]
public class PortfolioTests
{
    [Test]
    public void Constructor_SetsCashQuantity_AsPriceMinusPositionsValue()
    {
        var feed = new DataFeed(DateTime.Now,
            new Dictionary<string, double> { { "s1", 100 }, { "s2", 200 } });
        var deltas = new Dictionary<string, double> { { "s1", 1 }, { "s2", 2 } };

        var p = new Portfolio(1000, deltas, feed);

        Assert.That(p.CashValue, Is.EqualTo(500).Within(1E-10));
    }
}