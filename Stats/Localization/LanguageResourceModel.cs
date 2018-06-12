using ColossalFramework.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stats.Localization
{
    public class LanguageResourceModel
    {
        private readonly LocaleManager localeManager;
        private readonly LanguageResourceService languageResourceService;
        private readonly LanguageResourceDto languageResourceDto;

        private Dictionary<string, string> itemsDictionary;

        public LanguageResourceModel(LocaleManager localeManager, LanguageResourceService languageResourceService, LanguageResourceDto languageResourceDto)
        {
            this.localeManager = localeManager ?? throw new ArgumentNullException(nameof(localeManager));
            this.languageResourceService = languageResourceService ?? throw new ArgumentNullException(nameof(languageResourceService));
            this.languageResourceDto = languageResourceDto ?? throw new ArgumentNullException(nameof(languageResourceDto));

            LocaleManager.eventLocaleChanged += LocaleManager_eventLocaleChanged;
            this.UpdateFromDto(languageResourceDto);

            foreach (var item in itemsDictionary)
            {
                if (string.IsNullOrEmpty(item.Value))
                {
                    throw new Exception($"A translation value is missing for the language '{languageResourceDto.LanguageTwoLetterCode}' and the item '{item.Key}'.");
                }
            }
        }

        public string Reset => this.itemsDictionary["Reset"];
        public string UpdateEveryXSeconds => this.itemsDictionary["UpdateEveryXSeconds"];
        public string AutoHide => itemsDictionary["AutoHide"];
        public string HideItemsBelowTreshold => itemsDictionary["HideItemsBelowTreshold"];
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
        public string CriticalTreshold => itemsDictionary["CriticalTreshold"];
        public string Electricity => itemsDictionary[ItemData.Electricity];
        public string Heating => itemsDictionary[ItemData.Heating];
        public string Water => itemsDictionary[ItemData.Water];
        public string SewageTreatment => itemsDictionary[ItemData.SewageTreatment];
        public string WaterReserveTank => itemsDictionary[ItemData.WaterReserveTank];
        public string WaterPumpingServiceStorage => itemsDictionary[ItemData.WaterPumpingServiceStorage];
        public string WaterPumpingServiceVehicles => itemsDictionary[ItemData.WaterPumpingServiceVehicles];
        public string Landfill => itemsDictionary[ItemData.Landfill];
        public string LandfillVehicles => itemsDictionary[ItemData.LandfillVehicles];
        public string GarbageProcessing => itemsDictionary[ItemData.GarbageProcessing];
        public string GarbageProcessingVehicles => itemsDictionary[ItemData.GarbageProcessingVehicles];
        public string ElementarySchool => itemsDictionary[ItemData.ElementarySchool];
        public string HighSchool => itemsDictionary[ItemData.HighSchool];
        public string University => itemsDictionary[ItemData.University];
        public string Healthcare => itemsDictionary[ItemData.Healthcare];
        public string HealthcareVehicles => itemsDictionary[ItemData.HealthcareVehicles];
        public string AverageIllnessRate => itemsDictionary[ItemData.AverageIllnessRate];
        public string Cemetery => itemsDictionary[ItemData.Cemetery];
        public string CemeteryVehicles => itemsDictionary[ItemData.CemeteryVehicles];
        public string Crematorium => itemsDictionary[ItemData.Crematorium];
        public string CrematoriumVehicles => itemsDictionary[ItemData.CrematoriumVehicles];
        public string TrafficJam => itemsDictionary[ItemData.TrafficJam];
        public string GroundPollution => itemsDictionary[ItemData.GroundPollution];
        public string DrinkingWaterPollution => itemsDictionary[ItemData.DrinkingWaterPollution];
        public string NoisePollution => itemsDictionary[ItemData.NoisePollution];
        public string FireHazard => itemsDictionary[ItemData.FireHazard];
        public string FireDepartmentVehicles => itemsDictionary[ItemData.FireDepartmentVehicles];
        public string CrimeRate => itemsDictionary[ItemData.CrimeRate];
        public string PoliceHoldingCells => itemsDictionary[ItemData.PoliceHoldingCells];
        public string PoliceVehicles => itemsDictionary[ItemData.PoliceVehicles];
        public string PrisonCells => itemsDictionary[ItemData.PrisonCells];
        public string PrisonVehicles => itemsDictionary[ItemData.PrisonVehicles];
        public string Unemployment => itemsDictionary[ItemData.Unemployment];
        public string RoadMaintenanceVehicles => itemsDictionary[ItemData.RoadMaintenanceVehicles];
        public string ParkMaintenanceVehicles => itemsDictionary[ItemData.ParkMaintenanceVehicles];
        public string SnowDump => itemsDictionary[ItemData.SnowDump];
        public string SnowDumpVehicles => itemsDictionary[ItemData.SnowDumpVehicles];
        public string CityUnattractiveness => itemsDictionary[ItemData.CityUnattractiveness];

        private void UpdateFromDto(LanguageResourceDto dto)
        {
            this.itemsDictionary = dto.LocalizedItems.ToDictionary(x => x.Key, x => x.Value);
        }

        public void UpdateFromLanguage(string languageTwoLetterCode)
        {
            var dto = this.languageResourceService.Load(languageTwoLetterCode);
            this.UpdateFromDto(dto);
        }

        public string GetLocalizedString(ItemData item)
        {
            return this.itemsDictionary[item.SystemName];
        }

        public event Action LanguageChanged;

        private void OnLanguageChanged()
        {
            this.LanguageChanged?.Invoke();
        }

        private void LocaleManager_eventLocaleChanged()
        {
            this.UpdateFromLanguage(this.localeManager.language);
            this.OnLanguageChanged();
        }
    }
}
