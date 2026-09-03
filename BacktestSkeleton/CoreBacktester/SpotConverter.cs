
namespace CoreBacktester;

public static class SpotConverter
{
    public static double[] ToArray(string[] ids, Dictionary<string, double> values)
    {
        double[] res = new double[ids.Length];
        for (int i = 0; i < ids.Length; i++)
            res[i] = values[ids[i]];
        return res;
    }

    public static Dictionary<string, double> ToDictionary(string[] ids, double[] values)
    {
        var d = new Dictionary<string, double>();
        for (int i = 0; i < ids.Length; i++)
            d[ids[i]] = values[i];
        return d;
    }

    public static double DotProduct(Dictionary<string, double> a, Dictionary<string, double> b)
    {
        double total = 0;
        foreach (var kv in a)
            total += kv.Value * b[kv.Key];
        return total;
    }
}