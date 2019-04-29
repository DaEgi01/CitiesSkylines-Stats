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

        public string Reset => this.itemsDictionary["Reset"];
        public string ResetPosition => this.itemsDictionary["ResetPosition"];
        public string UpdateEveryXSeconds => this.itemsDictionary["UpdateEveryXSeconds"];
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
        public string MedicalHelicopters => itemsDictionary[ItemData.MedicalHelicopters];
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
        public string FireHelicopters => itemsDictionary[ItemData.FireHelicopters];
        public string CrimeRate => itemsDictionary[ItemData.CrimeRate];
        public string PoliceHoldingCells => itemsDictionary[ItemData.PoliceHoldingCells];
        public string PoliceVehicles => itemsDictionary[ItemData.PoliceVehicles];
        public string PoliceHelicopters => itemsDictionary[ItemData.PoliceHelicopters];
        public string PrisonCells => itemsDictionary[ItemData.PrisonCells];
        public string PrisonVehicles => itemsDictionary[ItemData.PrisonVehicles];
        public string Unemployment => itemsDictionary[ItemData.Unemployment];
        public string RoadMaintenanceVehicles => itemsDictionary[ItemData.RoadMaintenanceVehicles];
        public string ParkMaintenanceVehicles => itemsDictionary[ItemData.ParkMaintenanceVehicles];
        public string SnowDump => itemsDictionary[ItemData.SnowDump];
        public string SnowDumpVehicles => itemsDictionary[ItemData.SnowDumpVehicles];
        public string CityUnattractiveness => itemsDictionary[ItemData.CityUnattractiveness];
        public string Taxis => itemsDictionary[ItemData.Taxis];

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
