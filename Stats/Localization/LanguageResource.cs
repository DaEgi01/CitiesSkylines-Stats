using ColossalFramework.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stats.Localization
{
    public class LanguageResource
    {
        private readonly LanguageResourceService<LanguageResourceDto> languageResourceService;
        private readonly string fallbackLanguageTwoLetterCode;

        private string currentLanguage;
        private Dictionary<string, string> localizedStrings;

        private LanguageResource(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            string currentLanguage,
            Dictionary<string, string> localizedStrings,
            string fallbackLanguageTwoLetterCode)
        {
            this.currentLanguage = currentLanguage ?? throw new ArgumentNullException(nameof(currentLanguage));
            this.languageResourceService = languageResourceService ?? throw new ArgumentNullException(nameof(languageResourceService));
            this.localizedStrings = localizedStrings ?? throw new ArgumentNullException(nameof(localizedStrings));
            this.fallbackLanguageTwoLetterCode = fallbackLanguageTwoLetterCode ?? throw new ArgumentNullException(nameof(fallbackLanguageTwoLetterCode));
        }

        public string CurrentLanguage => currentLanguage;

        public static LanguageResource Create(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            string initialLanguageTwoLetterCode,
            string fallbackLanguageTwoLetterCode)
        {
            var result = LoadLanguageOrFallbackLanguage(
                languageResourceService,
                initialLanguageTwoLetterCode,
                fallbackLanguageTwoLetterCode
            );

            return new LanguageResource(
                languageResourceService,
                result.Language,
                result.LocalizedStrings,
                fallbackLanguageTwoLetterCode
            );
        }

        public string Reset => localizedStrings["Reset"];
        public string ResetPosition => localizedStrings["ResetPosition"];
        public string UpdateEveryXSeconds => localizedStrings["UpdateEveryXSeconds"];
        public string AutoHide => localizedStrings["AutoHide"];
        public string HideItemsBelowThreshold => localizedStrings["HideItemsBelowThreshold"];
        public string HideItemsNotAvailable => localizedStrings["HideItemsNotAvailable"];
        public string BackgroundColor => localizedStrings["BackgroundColor"];
        public string ForegroundColor => localizedStrings["ForegroundColor"];
        public string AccentColor => localizedStrings["AccentColor"];
        public string MainWindow => localizedStrings["MainWindow"];
        public string ColumnCount => localizedStrings["ColumnCount"];
        public string ItemWidth => localizedStrings["ItemWidth"];
        public string ItemHeight => localizedStrings["ItemHeight"];
        public string ItemPadding => localizedStrings["ItemPadding"];
        public string ItemTextScale => localizedStrings["ItemTextScale"];
        public string Items => localizedStrings["Items"];
        public string Enabled => localizedStrings["Enabled"];
        public string CriticalThreshold => localizedStrings["CriticalThreshold"];
        public string SortOrder => localizedStrings["SortOrder"];

        public string GetLocalizedItemString(ItemData item)
        {
            return localizedStrings[item.Name];
        }

        public void LoadLanguage(string languageTwoLetterCode)
        {
            var result = LoadLanguageOrFallbackLanguage(
                this.languageResourceService,
                languageTwoLetterCode,
                this.fallbackLanguageTwoLetterCode
            );

            this.currentLanguage = result.Language;
            this.localizedStrings = result.LocalizedStrings;
        }

        private static LoadLanguageResult LoadLanguageOrFallbackLanguage(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            string languageTwoLetterCode,
            string fallbackLanguageTwoLetterCode)
        {
            var desiredLanguageResourceDto = languageResourceService.Load(languageTwoLetterCode);
            if (desiredLanguageResourceDto != null)
            {
                return new LoadLanguageResult(
                    languageTwoLetterCode,
                    desiredLanguageResourceDto.LocalizedItems
                        .ToDictionary(x => x.Key, x => x.Value)
                );
            }

            var fallbackLanguageResourceDto = languageResourceService.Load(fallbackLanguageTwoLetterCode);
            if (fallbackLanguageResourceDto != null)
            {
                return new LoadLanguageResult(
                    fallbackLanguageTwoLetterCode,
                    fallbackLanguageResourceDto.LocalizedItems
                        .ToDictionary(x => x.Key, x => x.Value)
                );
            }

            throw new Exception($"Could not load the LanguageResourceDto for the {nameof(languageTwoLetterCode)} '{languageTwoLetterCode}', nor for the {nameof(fallbackLanguageTwoLetterCode)} '{fallbackLanguageTwoLetterCode}'.");
        }
    }
}
