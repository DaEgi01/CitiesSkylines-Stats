namespace Stats.Localization
{
    using System;
    using System.Collections.Generic;

    public class LoadLanguageResult
    {
        public LoadLanguageResult(string language, Dictionary<string, string> localizedStrings)
        {
            if (language is null)
                throw new ArgumentNullException(nameof(language));
            if (localizedStrings is null)
                throw new ArgumentNullException(nameof(localizedStrings));

            Language = language;
            LocalizedStrings = localizedStrings;
        }

        public string Language { get; }
        public Dictionary<string, string> LocalizedStrings { get; }
    }
}
