using BenchmarkDotNet.Attributes;
using FastEnumUtility;

[MemoryDiagnoser(false)]
public class EnumBenchmarks
{
    private static readonly ContextTypes contextType = ContextTypes.Withings;

    [Benchmark]
    public string EnumToString_Default()
    {
        return contextType.ToString();
    }

    [Benchmark]
    public string EnumToString_With_Sourcegenerator()
    {
        return contextType.ToStringFast();
    }


    [Benchmark]
    public string EnumToString_With_Cache()
    {
        return contextType.FastToString();
    }
}