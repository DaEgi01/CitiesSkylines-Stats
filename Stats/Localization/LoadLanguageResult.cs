using System;
using System.Collections.Generic;

namespace Stats.Localization
{
    public class LoadLanguageResult
    {
        public LoadLanguageResult(string language, Dictionary<string, string> localizedStrings)
        {
            Language = language ?? throw new ArgumentNullException(nameof(language));
            LocalizedStrings = localizedStrings ?? throw new ArgumentNullException(nameof(localizedStrings));
        }

        public string Language { get; }
        public Dictionary<string, string> LocalizedStrings { get; }
    }
}
