using CoreBacktester;
using PricingLibrary.DataClasses;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using PricingLibrary.RebalancingOracleDescriptions;
using System.Text.Json;


public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.Error.WriteLine("false try again write ./Program params.json data.csv output.json");
            return;
        }
        
        
        var jsonOptions = new JsonSerializerOptions{PropertyNameCaseInsensitive = true,PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        jsonOptions.Converters.Add(new RebalancingOracleDescriptionConverter());
        var parametersJson = File.ReadAllText(args[0]);
        BasketTestParameters parameters = JsonSerializer.Deserialize<BasketTestParameters>(parametersJson, jsonOptions);

        var dataProvider = new CsvDataProvider(args[1]);

        var backtester = new Backtester();
        IRebalancingOracle oracle;
        if (parameters.RebalancingOracleDescription is RegularOracleDescription regular)
             oracle = new RegularOracle(regular.Period);
        else if(parameters.RebalancingOracleDescription is SpotOracleDescription spot)
             oracle = new SpotOracle(spot.Threshold);
        else
             throw new NotSupportedException("Oracle non supporte");
        OutputData[] results = backtester.Run(parameters,dataProvider.extractor(),oracle);


        var resultsJson = JsonSerializer.Serialize(results,jsonOptions);

        File.WriteAllText(args[2],resultsJson);





    }
}