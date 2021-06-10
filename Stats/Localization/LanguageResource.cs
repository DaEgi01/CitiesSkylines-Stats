using System;
using System.Collections.Generic;
using System.Linq;

namespace Stats.Localization
{
    public class LanguageResource
    {
        private readonly LanguageResourceService<LanguageResourceDto> _languageResourceService;
        private readonly string _fallbackLanguageTwoLetterCode;

        private string _currentLanguage;
        private Dictionary<string, string> _localizedStrings;

        private LanguageResource(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            string currentLanguage,
            Dictionary<string, string> localizedStrings,
            string fallbackLanguageTwoLetterCode)
        {
            _currentLanguage = currentLanguage ?? throw new ArgumentNullException(nameof(currentLanguage));
            _languageResourceService = languageResourceService ?? throw new ArgumentNullException(nameof(languageResourceService));
            _localizedStrings = localizedStrings ?? throw new ArgumentNullException(nameof(localizedStrings));
            _fallbackLanguageTwoLetterCode = fallbackLanguageTwoLetterCode ?? throw new ArgumentNullException(nameof(fallbackLanguageTwoLetterCode));
        }

        public string CurrentLanguage => _currentLanguage;

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

        public string Reset => _localizedStrings["Reset"];
        public string ResetPosition => _localizedStrings["ResetPosition"];
        public string UpdateEveryXSeconds => _localizedStrings["UpdateEveryXSeconds"];
        public string AutoHide => _localizedStrings["AutoHide"];
        public string HideItemsBelowThreshold => _localizedStrings["HideItemsBelowThreshold"];
        public string HideItemsNotAvailable => _localizedStrings["HideItemsNotAvailable"];
        public string BackgroundColor => _localizedStrings["BackgroundColor"];
        public string ForegroundColor => _localizedStrings["ForegroundColor"];
        public string AccentColor => _localizedStrings["AccentColor"];
        public string MainWindow => _localizedStrings["MainWindow"];
        public string ColumnCount => _localizedStrings["ColumnCount"];
        public string ItemPadding => _localizedStrings["ItemPadding"];
        public string ItemIconSize => _localizedStrings["ItemIconSize"];
        public string ItemTextScale => _localizedStrings["ItemTextScale"];
        public string ItemTextPosition => _localizedStrings["ItemTextPosition"];
        public string Items => _localizedStrings["Items"];
        public string Enabled => _localizedStrings["Enabled"];
        public string CriticalThreshold => _localizedStrings["CriticalThreshold"];
        public string SortOrder => _localizedStrings["SortOrder"];

        public string GetLocalizedItemString(ItemData item)
        {
            return _localizedStrings[item.Name];
        }

        public void LoadLanguage(string languageTwoLetterCode)
        {
            var result = LoadLanguageOrFallbackLanguage(
                _languageResourceService,
                languageTwoLetterCode,
                _fallbackLanguageTwoLetterCode
            );

            _currentLanguage = result.Language;
            _localizedStrings = result.LocalizedStrings;
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
