using ColossalFramework.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stats.Localization
{
    public class LanguageResourceModel : IDisposable
    {
        private readonly LanguageResourceService<LanguageResourceDto> languageResourceService;

        private Dictionary<string, string> localizedStrings;

        public LanguageResourceModel(LanguageResourceService<LanguageResourceDto> languageResourceService, LocaleManager localeManager)
        {
            this.languageResourceService = languageResourceService ?? throw new ArgumentNullException(nameof(languageResourceService));

            var languageResourceDto = languageResourceService.Load(localeManager.language);
            this.localizedStrings = languageResourceDto.LocalizedItems.ToDictionary(x => x.Key, x => x.Value);

            LocaleManager.eventLocaleChanged += LocaleManager_eventLocaleChanged;
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

        public string GetItemLocalizedItemString(ItemData item)
        {
            return localizedStrings[item.Name];
        }

        private void LocaleManager_eventLocaleChanged()
        {
            var languageResourceDto = this.languageResourceService.Load(LocaleManager.instance.language);
            this.UpdateFromDto(languageResourceDto);
        }

        private void UpdateFromDto(LanguageResourceDto dto)
        {
            this.localizedStrings = dto.LocalizedItems.ToDictionary(x => x.Key, x => x.Value);
            this.OnLanguageChanged();
        }

        public event Action LanguageChanged;

        private void OnLanguageChanged()
        {
            this.LanguageChanged?.Invoke();
        }
    }
}
