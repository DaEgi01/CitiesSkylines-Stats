namespace Stats
{
    using System;

    public class IndexesMessedUpException : Exception
    {
        public IndexesMessedUpException(int index)
            : base($"Indexes are invalid. Error found at index '{index.ToString()}'")
        {
        }
    }
}
