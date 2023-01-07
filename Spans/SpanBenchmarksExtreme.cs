using BenchmarkDotNet.Attributes;

[MemoryDiagnoser(false)]
public class SpanBenchmarksExtreme
{
    [Params(100, 200)]
    public int N;

    private string countries;

    private int index, numberOfCharactersToExtract;

    [GlobalSetup]
    public void GlobalSetup()
    {
        countries = "India, USA, UK, Australia, Netherlands, Belgium";
        index = countries.LastIndexOf(",", StringComparison.Ordinal);
        numberOfCharactersToExtract = countries.Length - index;
    }

    [Benchmark]
    public void Substring()
    {
        for (int i = 0; i < N; i++)
        {
            var data = countries.Substring(index + 1, numberOfCharactersToExtract - 1);
        }
    }

    [Benchmark]
    public void Span()
    {
        for (int i = 0; i < N; i++)
        {
            var data = countries.AsSpan().Slice(index + 1, numberOfCharactersToExtract - 1);
        }
    }
}
