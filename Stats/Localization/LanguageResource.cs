using ColossalFramework.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stats.Localization
{
    public class LanguageResource : IDisposable
    {
        private readonly LanguageResourceService<LanguageResourceDto> languageResourceService;
        private readonly LocaleManager localeManager;
        private readonly string fallbackLanguageTwoLetterCode;

        private Dictionary<string, string> localizedStrings;

        private LanguageResource(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            LocaleManager localeManager,
            Dictionary<string, string> localizedStrings,
            string fallbackLanguageTwoLetterCode)
        {
            this.languageResourceService = languageResourceService ?? throw new ArgumentNullException(nameof(languageResourceService));
            this.localeManager = localeManager ?? throw new ArgumentNullException(nameof(localeManager));
            this.fallbackLanguageTwoLetterCode = fallbackLanguageTwoLetterCode ?? throw new ArgumentNullException(nameof(fallbackLanguageTwoLetterCode));
            this.localizedStrings = localizedStrings ?? throw new ArgumentNullException(nameof(localizedStrings));
            LocaleManager.eventLocaleChanged += LocaleManager_eventLocaleChanged;
        }

        public static LanguageResource Create(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            LocaleManager localeManager,
            string initialLanguageTwoLetterCode,
            string fallbackLanguageTwoLetterCode)
        {
            var localizedStrings = LoadLanguageOrFallbackLanguage(
                languageResourceService,
                initialLanguageTwoLetterCode,
                fallbackLanguageTwoLetterCode);

            return new LanguageResource(languageResourceService,
                localeManager,
                localizedStrings,
                fallbackLanguageTwoLetterCode);
        }

        public void Dispose()
        {
            LocaleManager.eventLocaleChanged -= LocaleManager_eventLocaleChanged;
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

        private static Dictionary<string, string> LoadLanguageOrFallbackLanguage(
            LanguageResourceService<LanguageResourceDto> languageResourceService,
            string languageTwoLetterCode,
            string fallbackLanguageTwoLetterCode)
        {
            var languageResourceDto = languageResourceService.Load(languageTwoLetterCode)
                ?? languageResourceService.Load(fallbackLanguageTwoLetterCode);

            if (languageResourceDto == null)
            {
                throw new Exception($"Could not load the LanguageResourceDto for the {nameof(languageTwoLetterCode)} '{languageTwoLetterCode}', nor for the {nameof(fallbackLanguageTwoLetterCode)} '{fallbackLanguageTwoLetterCode}'.");
            }

            return languageResourceDto.LocalizedItems.ToDictionary(x => x.Key, x => x.Value);
        }

        private void LocaleManager_eventLocaleChanged()
        {
            this.localizedStrings = LoadLanguageOrFallbackLanguage(
                this.languageResourceService,
                this.localeManager.language,
                this.fallbackLanguageTwoLetterCode);
        }
    }
}
