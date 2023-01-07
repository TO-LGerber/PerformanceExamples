using BenchmarkDotNet.Attributes;
using FastEnumUtility;

[MemoryDiagnoser(false)]
public class EnumBenchmarks
{
    private static readonly ContextTypes contextType = ContextTypes.Withings;
    private static readonly string stringifiedContextType = contextType.ToString();

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

    [Benchmark]
    public string Create_With_Plus()
    {
        return "ContextType is: " + contextType;
    }

    [Benchmark]
    public string Create_With_Interprelation()
    {
        return $"ContextType is: {contextType}";
    }

    [Benchmark]
    public string Create_With_nameof()
    {
        return $"ContextType is: {nameof(ContextTypes.Accorhotels)}";
    }

    [Benchmark]
    public string Create_With_StringifiedEnum()
    {
        return $"ContextType is: {stringifiedContextType}";
    }
}