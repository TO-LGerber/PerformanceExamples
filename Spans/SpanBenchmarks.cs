using BenchmarkDotNet.Attributes;

[MemoryDiagnoser(false)]
public class SpanBenchmarks
{
    private static readonly string _dateString = "01 05 1991";

    [Benchmark]
    public DateTime With_String()
    {
        var day = _dateString.Substring(0, 2);
        var month = _dateString.Substring(3, 2);
        var year = _dateString.Substring(6);
        return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
    }

    [Benchmark]
    public DateTime With_Span()
    {
        ReadOnlySpan<char> dateSpan = _dateString;
        var day = dateSpan.Slice(0, 2);
        var month = dateSpan.Slice(3, 2);
        var year = dateSpan.Slice(6);
        return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
    }
}