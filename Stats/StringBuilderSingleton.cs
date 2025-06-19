using System.Text;

namespace Stats;

public static class StringBuilderSingleton
{
    private static readonly StringBuilder _stringBuilder = new();

    public static StringBuilder Instance
    {
        get
        {
            _stringBuilder.Length = 0;
            return _stringBuilder;
        }
    }
}
