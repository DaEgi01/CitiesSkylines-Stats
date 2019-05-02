using ColossalFramework.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stats.Localization
{
    public class LanguageResourceModel : IDisposable
    {
        private readonly LanguageResourceService<LanguageResourceDto> languageResourceService;

        private Dictionary<string, string> itemsDictionary;

        public LanguageResourceModel(LanguageResourceService<LanguageResourceDto> languageResourceService, LocaleManager localeManager)
        {
            this.languageResourceService = languageResourceService ?? throw new ArgumentNullException(nameof(languageResourceService));

            var languageResourceDto = languageResourceService.Load(localeManager.language);
            this.itemsDictionary = languageResourceDto.LocalizedItems.ToDictionary(x => x.Key, x => x.Value);

            LocaleManager.eventLocaleChanged += LocaleManager_eventLocaleChanged;
        }

        public string Reset => itemsDictionary["Reset"];
        public string ResetPosition => itemsDictionary["ResetPosition"];
        public string UpdateEveryXSeconds => itemsDictionary["UpdateEveryXSeconds"];
        public string AutoHide => itemsDictionary["AutoHide"];
        public string HideItemsBelowThreshold => itemsDictionary["HideItemsBelowThreshold"];
        public string HideItemsNotAvailable => itemsDictionary["HideItemsNotAvailable"];
        public string BackgroundColor => itemsDictionary["BackgroundColor"];
        public string ForegroundColor => itemsDictionary["ForegroundColor"];
        public string AccentColor => itemsDictionary["AccentColor"];
        public string MainWindow => itemsDictionary["MainWindow"];
        public string ColumnCount => itemsDictionary["ColumnCount"];
        public string ItemWidth => itemsDictionary["ItemWidth"];
        public string ItemHeight => itemsDictionary["ItemHeight"];
        public string ItemPadding => itemsDictionary["ItemPadding"];
        public string ItemTextScale => itemsDictionary["ItemTextScale"];
        public string Items => itemsDictionary["Items"];
        public string Enabled => itemsDictionary["Enabled"];
        public string CriticalThreshold => itemsDictionary["CriticalThreshold"];

        public string GetItemLocalizedString(ItemData itemData)
        {
            return itemsDictionary[itemData.SystemName];
        }

        public void Dispose()
        {
            LocaleManager.eventLocaleChanged -= LocaleManager_eventLocaleChanged;
        }

        private void LocaleManager_eventLocaleChanged()
        {
            var languageResourceDto = this.languageResourceService.Load(LocaleManager.instance.language);
            this.UpdateFromDto(languageResourceDto);
        }

        private void UpdateFromDto(LanguageResourceDto dto)
        {
            this.itemsDictionary = dto.LocalizedItems.ToDictionary(x => x.Key, x => x.Value);
            this.OnLanguageChanged();
        }

        public event Action LanguageChanged;

        private void OnLanguageChanged()
        {
            this.LanguageChanged?.Invoke();
        }
    }
}
