using BenchmarkDotNet.Attributes;

[MemoryDiagnoser(false)]
public class SpanBenchmarks
{
    const string input = "AXTqyFYy0t6VtY9";
    readonly List<char[]> _encryptionMarkers = new List<char[]>()
    {
        new char[] { 'j', 'H', '4' },
        new char[] { 'p', 'B', '8' },
        new char[] { 'b', 'S', '3' }
    };

    [Benchmark]
    public string String_Substring()
    {
        var marker = _encryptionMarkers[input.Length % 3];
        return string.Concat
        (
            input.Substring(0, 1),
            marker[0],
            input.Substring(1, 3),
            marker[1],
            input.Substring(4),
            marker[2]
        );
    }

    [Benchmark]
    public string Span_Slice()
    {
        var marker = _encryptionMarkers[input.Length % 3];
        var readonlySpan = (ReadOnlySpan<char>)input;

        return string.Concat
        (
            readonlySpan.Slice(0, 1).ToString(),
            marker[0],
            readonlySpan.Slice(1, 3).ToString(),
            marker[1],
            readonlySpan.Slice(4).ToString(),
            marker[2]
        );
    }
}
