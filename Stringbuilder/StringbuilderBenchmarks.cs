using BenchmarkDotNet.Attributes;
using System.Text;

namespace Stringbuilder
{
    [MemoryDiagnoser(false)]
    public class StringbuilderBenchmarks
    {
        private string[] _strings;

        [Params(2, 10, 100)]
        public int AmountOfLines { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _strings = Enumerable.Range(0, AmountOfLines).Select(i => 50.RandomString() + "\n").ToArray();
        }

        [Benchmark]
        public string StringConcat()
        {
            var s = "";
            for (int i = 0; i < AmountOfLines; i++)
            {
                s += _strings[i];
            }

            return s;
        }

        [Benchmark]
        public string StringbuilderAppend()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < AmountOfLines; i++)
            {
                sb.AppendLine(_strings[i]);
            }

            return sb.ToString();
        }
    }

    public static class Extensions
    {
        private static string possibleChars = "abcdefghijklmnopqrstuvwxyz";

        private static char[] possibleCharsArray = possibleChars.ToCharArray();

        private static int possibleCharsAvailable = possibleChars.Length;
        private static Random random = new Random();

        public static string RandomString(this int length)
        {
            var rBytes = new byte[length];
            random.NextBytes(rBytes);
            var rName = new char[length]; 
            while (length-- > 0) rName[length] = possibleCharsArray[rBytes[length] % possibleCharsAvailable];
            return new string(rName);
        }
    }
}
