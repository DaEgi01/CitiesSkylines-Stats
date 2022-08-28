namespace Stats.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            if (languageResourceService is null)
                throw new ArgumentNullException(nameof(languageResourceService));
            if (currentLanguage is null)
                throw new ArgumentNullException(nameof(currentLanguage));
            if (localizedStrings is null)
                throw new ArgumentNullException(nameof(localizedStrings));
            if (fallbackLanguageTwoLetterCode is null)
                throw new ArgumentNullException(nameof(fallbackLanguageTwoLetterCode));

            _currentLanguage = currentLanguage;
            _languageResourceService = languageResourceService;
            _localizedStrings = localizedStrings;
            _fallbackLanguageTwoLetterCode = fallbackLanguageTwoLetterCode;
        }

        public string CurrentLanguage => _currentLanguage;

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

        public static LanguageResource Create(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            string initialLanguageTwoLetterCode,
            string fallbackLanguageTwoLetterCode)
        {
            var result = LoadLanguageOrFallbackLanguage(
                languageResourceService,
                initialLanguageTwoLetterCode,
                fallbackLanguageTwoLetterCode);

            return new LanguageResource(
                languageResourceService,
                result.Language,
                result.LocalizedStrings,
                fallbackLanguageTwoLetterCode);
        }

        public string GetLocalizedItemString(ItemData item)
        {
            return _localizedStrings[item.Name];
        }

        public void LoadLanguage(string languageTwoLetterCode)
        {
            var result = LoadLanguageOrFallbackLanguage(
                _languageResourceService,
                languageTwoLetterCode,
                _fallbackLanguageTwoLetterCode);

            _currentLanguage = result.Language;
            _localizedStrings = result.LocalizedStrings;
        }

        private static LoadLanguageResult LoadLanguageOrFallbackLanguage(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            string languageTwoLetterCode,
            string fallbackLanguageTwoLetterCode)
        {
            var desiredLanguageResourceDto = languageResourceService.Load(languageTwoLetterCode);
            if (desiredLanguageResourceDto is not null)
            {
                return ValidateAndCreateResult(desiredLanguageResourceDto, languageTwoLetterCode);
            }

            if (languageTwoLetterCode != fallbackLanguageTwoLetterCode)
            {
                var fallbackLanguageResourceDto = languageResourceService.Load(fallbackLanguageTwoLetterCode);
                if (fallbackLanguageResourceDto is not null)
                {
                    return ValidateAndCreateResult(fallbackLanguageResourceDto, fallbackLanguageTwoLetterCode);
                }
            }

            throw new Exception($"Could not load the LanguageResourceDto for the {nameof(languageTwoLetterCode)} '{languageTwoLetterCode}', nor for the {nameof(fallbackLanguageTwoLetterCode)} '{fallbackLanguageTwoLetterCode}'.");
        }

        private static LoadLanguageResult ValidateAndCreateResult(
            LanguageResourceDto languageResourceDto,
            string languageTwoLetterCode)
        {
            ValidateLanguageItems(languageResourceDto, languageTwoLetterCode);

            return new LoadLanguageResult(
                languageTwoLetterCode,
                languageResourceDto.LocalizedItems.ToDictionary(x => x.Key!, x => x.Value!));
        }

        private static void ValidateLanguageItems(LanguageResourceDto? languageResourceDto, string languageTwoLetterCode)
        {
            if (languageResourceDto is null)
                throw new Exception($"Could not load {nameof(LanguageResourceDto)} for languageTwoLetterCode: '{languageTwoLetterCode}'. Check if file exists.");

            if (languageResourceDto.LanguageTwoLetterCode is null)
                throw new IsNullException(nameof(languageResourceDto.LanguageTwoLetterCode), $"Localization file for '{languageTwoLetterCode}' broken.");

            if (languageResourceDto.Version is null)
                throw new IsNullException(nameof(languageResourceDto.Version), $"Localization file for '{languageTwoLetterCode}' broken.");

            if (languageResourceDto.LocalizedItems is null)
                throw new IsNullException(nameof(languageResourceDto.LocalizedItems), $"Localization file for '{languageTwoLetterCode}' broken.");

            foreach (var item in languageResourceDto.LocalizedItems)
            {
                if (item.Key is null || item.Value is null)
                    throw new Exception($"Localization file for '{languageTwoLetterCode}' broken. At least one LocalizedItem inside localization file for '{languageTwoLetterCode}' contains null values.");
            }
        }
    }
}
