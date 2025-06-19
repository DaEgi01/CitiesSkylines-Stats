using System;

namespace Stats;

public sealed class IndexesMessedUpException : Exception
{
    public IndexesMessedUpException(int index)
        : base($"Indexes are invalid. Error found at index '{index.ToString()}'")
    {
    }
}
