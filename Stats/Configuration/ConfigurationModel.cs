using System;
using UnityEngine;

namespace Stats.Configuration
{
    public class ConfigurationModel : IDisposable
    {
        private readonly ConfigurationService<ConfigurationDto> configurationService;
        private ConfigurationDto dto;
        private ConfigurationItemModel[] configurationItems;

        public ConfigurationModel(ConfigurationService<ConfigurationDto> configurationService, ConfigurationDto dto)
        {
            this.configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            this.dto = dto ?? throw new ArgumentNullException(nameof(dto));
            this.configurationItems = new[] {
                new ConfigurationItemModel(ItemData.AverageIllnessRate, dto.AverageIllnessRate, dto.AverageIllnessRateCriticalThreshold, dto.AverageIllnessRateSortOrder),
                new ConfigurationItemModel(ItemData.Cemetery, dto.Cemetery, dto.CemeteryCriticalThreshold, dto.CemeterySortOrder),
                new ConfigurationItemModel(ItemData.CemeteryVehicles, dto.CemeteryVehicles, dto.CemeteryVehiclesCriticalThreshold, dto.CemeteryVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.CityUnattractiveness, dto.CityUnattractiveness, dto.CityUnattractivenessCriticalThreshold, dto.CityUnattractivenessSortOrder),
                new ConfigurationItemModel(ItemData.Crematorium, dto.Crematorium, dto.CrematoriumCriticalThreshold, dto.CrematoriumSortOrder),
                new ConfigurationItemModel(ItemData.CrematoriumVehicles, dto.CrematoriumVehicles, dto.CrematoriumVehiclesCriticalThreshold, dto.CrematoriumVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.CrimeRate, dto.CrimeRate, dto.CrimeRateCriticalThreshold, dto.CrimeRateSortOrder),
                new ConfigurationItemModel(ItemData.DisasterResponseHelicopters, dto.DisasterResponseHelicopters, dto.DisasterResponseHelicoptersCriticalThreshold, dto.DisasterResponseHelicoptersSortOrder),
                new ConfigurationItemModel(ItemData.DisasterResponseVehicles, dto.DisasterResponseVehicles, dto.DisasterResponseVehiclesCriticalThreshold, dto.DisasterResponseVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.DrinkingWaterPollution, dto.DrinkingWaterPollution, dto.DrinkingWaterPollutionCriticalThreshold, dto.DrinkingWaterPollutionSortOrder),
                new ConfigurationItemModel(ItemData.Electricity, dto.Electricity, dto.ElectricityCriticalThreshold, dto.ElectricitySortOrder),
                new ConfigurationItemModel(ItemData.ElementarySchool, dto.ElementarySchool, dto.ElementarySchoolCriticalThreshold, dto.ElementarySchoolSortOrder),
                new ConfigurationItemModel(ItemData.FireDepartmentVehicles, dto.FireDepartmentVehicles, dto.FireDepartmentVehiclesCriticalThreshold, dto.FireDepartmentVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.FireHazard, dto.FireHazard, dto.FireHazardCriticalThreshold, dto.FireHazardSortOrder),
                new ConfigurationItemModel(ItemData.FireHelicopters, dto.FireHelicopters, dto.FireHelicoptersCriticalThreshold, dto.FireHelicoptersSortOrder),
                new ConfigurationItemModel(ItemData.GarbageProcessing, dto.GarbageProcessing, dto.GarbageProcessingCriticalThreshold, dto.GarbageProcessingSortOrder),
                new ConfigurationItemModel(ItemData.GarbageProcessingVehicles, dto.GarbageProcessingVehicles, dto.GarbageProcessingVehiclesCriticalThreshold, dto.GarbageProcessingVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.GroundPollution, dto.GroundPollution, dto.GroundPollutionCriticalThreshold, dto.GroundPollutionSortOrder),
                new ConfigurationItemModel(ItemData.Healthcare, dto.Healthcare, dto.HealthcareCriticalThreshold, dto.HealthcareSortOrder),
                new ConfigurationItemModel(ItemData.HealthcareVehicles, dto.HealthcareVehicles, dto.HealthcareVehiclesCriticalThreshold, dto.HealthcareVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.Heating, dto.Heating, dto.HeatingCriticalThreshold, dto.HeatingSortOrder),
                new ConfigurationItemModel(ItemData.HighSchool, dto.HighSchool, dto.HighSchoolCriticalThreshold, dto.HighSchoolSortOrder),
                new ConfigurationItemModel(ItemData.Landfill, dto.Landfill, dto.LandfillCriticalThreshold, dto.LandfillSortOrder),
                new ConfigurationItemModel(ItemData.LandfillVehicles, dto.LandfillVehicles, dto.LandfillVehiclesCriticalThreshold, dto.LandfillVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.MedicalHelicopters, dto.MedicalHelicopters, dto.MedicalHelicoptersCriticalThreshold, dto.MedicalHelicoptersSortOrder),
                new ConfigurationItemModel(ItemData.NoisePollution, dto.NoisePollution, dto.NoisePollutionCriticalThreshold, dto.NoisePollutionSortOrder),
                new ConfigurationItemModel(ItemData.ParkMaintenanceVehicles, dto.ParkMaintenanceVehicles, dto.ParkMaintenanceVehiclesCriticalThreshold, dto.ParkMaintenanceVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.PoliceHelicopters, dto.PoliceHelicopters, dto.PoliceHelicoptersCriticalThreshold, dto.PoliceHelicoptersSortOrder),
                new ConfigurationItemModel(ItemData.PoliceHoldingCells, dto.PoliceHoldingCells, dto.PoliceHoldingCellsCriticalThreshold, dto.PoliceHoldingCellsSortOrder),
                new ConfigurationItemModel(ItemData.PoliceVehicles, dto.PoliceVehicles, dto.PoliceVehiclesCriticalThreshold, dto.PoliceVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.PostTrucks, dto.PostTrucks, dto.PostTrucksCriticalThreshold, dto.PostTrucksSortOrder),
                new ConfigurationItemModel(ItemData.PostVans, dto.PostVans, dto.PostVansCriticalThreshold, dto.PostVansSortOrder),
                new ConfigurationItemModel(ItemData.PrisonCells, dto.PrisonCells, dto.PrisonCellsCriticalThreshold, dto.PrisonCellsSortOrder),
                new ConfigurationItemModel(ItemData.PrisonVehicles, dto.PrisonVehicles, dto.PrisonVehiclesCriticalThreshold, dto.PrisonVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.RoadMaintenanceVehicles, dto.RoadMaintenanceVehicles, dto.RoadMaintenanceVehiclesCriticalThreshold, dto.RoadMaintenanceVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.SewageTreatment, dto.SewageTreatment, dto.SewageTreatmentCriticalThreshold, dto.SewageTreatmentSortOrder),
                new ConfigurationItemModel(ItemData.SnowDump, dto.SnowDump, dto.SnowDumpCriticalThreshold, dto.SnowDumpSortOrder),
                new ConfigurationItemModel(ItemData.SnowDumpVehicles, dto.SnowDumpVehicles, dto.SnowDumpVehiclesCriticalThreshold, dto.SnowDumpVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.Taxis, dto.Taxis, dto.TaxisCriticalThreshold, dto.TaxisSortOrder),
                new ConfigurationItemModel(ItemData.TrafficJam, dto.TrafficJam, dto.TrafficJamCriticalThreshold, dto.TrafficJamSortOrder),
                new ConfigurationItemModel(ItemData.Unemployment, dto.Unemployment, dto.UnemploymentCriticalThreshold, dto.UnemploymentSortOrder),
                new ConfigurationItemModel(ItemData.University, dto.University, dto.UniversityCriticalThreshold, dto.UniversitySortOrder),
                new ConfigurationItemModel(ItemData.Water, dto.Water, dto.WaterCriticalThreshold, dto.WaterSortOrder),
                new ConfigurationItemModel(ItemData.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorageCriticalThreshold, dto.WaterPumpingServiceStorageSortOrder),
                new ConfigurationItemModel(ItemData.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehiclesCriticalThreshold, dto.WaterPumpingServiceVehiclesSortOrder),
                new ConfigurationItemModel(ItemData.WaterReserveTank, dto.WaterReserveTank, dto.WaterReserveTankCriticalThreshold, dto.WaterReserveTankSortOrder)
            };
            ValidateIndexes(this.configurationItems);
            AddItemEvents(this.configurationItems);
        }

        private void ValidateIndexes(ConfigurationItemModel[] configurationItems)
        {
            for (int i = 0; i < configurationItems.Length; i++)
            {
                if (i != configurationItems[i].ItemData.Index)
                {
                    throw new IndexesMessedUpException(i);
                }
            }
        }

        private void AddItemEvents(ConfigurationItemModel[] configurationItems)
        {
            foreach (var configurationItem in configurationItems)
            {
                configurationItem.PropertyChanged += OnLayoutPropertyChanged;
            }
        }

        private void RemoveItemEvents()
        {
            foreach (var configurationItem in configurationItems)
            {
                configurationItem.PropertyChanged -= OnLayoutPropertyChanged;
            }
        }

        public void Dispose()
        {
            RemoveItemEvents();
        }

        public Vector2 MainPanelPosition
        {
            get => new Vector2(dto.MainPanelPositionX, dto.MainPanelPositionY);
            set
            {
                dto.MainPanelPositionX = value.x;
                dto.MainPanelPositionY = value.y;
                this.OnPositionChanged();
            }
        }

        public bool MainPanelAutoHide
        {
            get => dto.MainPanelAutoHide;
            set
            {
                dto.MainPanelAutoHide = value;
            }
        }

        public bool MainPanelHideItemsBelowThreshold
        {
            get => dto.MainPanelHideItemsBelowThreshold;
            set
            {
                dto.MainPanelHideItemsBelowThreshold = value;
                this.OnLayoutPropertyChanged();
            }
        }

        public bool MainPanelHideItemsNotAvailable
        {
            get => dto.MainPanelHideItemsNotAvailable;
            set
            {
                dto.MainPanelHideItemsNotAvailable = value;
                this.OnLayoutPropertyChanged();
            }
        }

        public int MainPanelColumnCount
        {
            get => dto.MainPanelColumnCount;
            set
            {
                dto.MainPanelColumnCount = value;
                this.OnLayoutPropertyChanged();
            }
        }

        public Color32 MainPanelBackgroundColor
        {
            get => dto.MainPanelBackgroundColor.GetColor32();
            set
            {
                dto.MainPanelBackgroundColor = value.GetColorString();
            }
        }

        public Color32 MainPanelForegroundColor
        {
            get => dto.MainPanelForegroundColor.GetColor32();
            set
            {
                dto.MainPanelForegroundColor = value.GetColorString();
            }
        }

        public Color32 MainPanelAccentColor
        {
            get => dto.MainPanelAccentColor.GetColor32();
            set
            {
                dto.MainPanelAccentColor = value.GetColorString();
            }
        }

        public float ItemWidth
        {
            get => dto.ItemWidth;
            set
            {
                dto.ItemWidth = value;
                this.OnLayoutPropertyChanged();
            }
        }

        public float ItemHeight
        {
            get => dto.ItemHeight;
            set
            {
                dto.ItemHeight = value;
                this.OnLayoutPropertyChanged();
            }
        }

        public float ItemPadding
        {
            get => dto.ItemPadding;
            set
            {
                dto.ItemPadding = value;
                this.OnLayoutPropertyChanged();
            }
        }

        public float ItemTextScale
        {
            get => dto.ItemTextScale;
            set
            {
                dto.ItemTextScale = value;
                this.OnLayoutPropertyChanged();
            }
        }

        public ConfigurationItemModel GetConfigurationItem(ItemData item)
        {
            return this.configurationItems[item.Index];
        }

        public void Save()
        {
            this.UpdateDtoItems();
            configurationService.Save(dto);
        }

        public void Reset()
        {
            dto = new ConfigurationDto();
            UpdateConfigurationItems();
            this.OnPositionChanged();
            this.OnLayoutPropertyChanged();
            this.Save();
        }

        public void ResetPosition()
        {
            dto.MainPanelPositionX = 0f;
            dto.MainPanelPositionY = 0f;
            this.OnPositionChanged();
            this.Save();
        }

        private void UpdateDtoItems()
        {
            foreach (var configurationItem in this.configurationItems)
            {
                if (configurationItem.ItemData == ItemData.AverageIllnessRate)
                {
                    dto.AverageIllnessRate = configurationItem.Enabled;
                    dto.AverageIllnessRateCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.AverageIllnessRateSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Cemetery)
                {
                    dto.Cemetery = configurationItem.Enabled;
                    dto.CemeteryCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CemeterySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CemeteryVehicles)
                {
                    dto.CemeteryVehicles = configurationItem.Enabled;
                    dto.CemeteryVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CemeteryVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CityUnattractiveness)
                {
                    dto.CityUnattractiveness = configurationItem.Enabled;
                    dto.CityUnattractivenessCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CityUnattractivenessSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Crematorium)
                {
                    dto.Crematorium = configurationItem.Enabled;
                    dto.CrematoriumCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CrematoriumSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CrematoriumVehicles)
                {
                    dto.CrematoriumVehicles = configurationItem.Enabled;
                    dto.CrematoriumVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CrematoriumVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CrimeRate)
                {
                    dto.CrimeRate = configurationItem.Enabled;
                    dto.CrimeRateCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CrimeRateSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DisasterResponseHelicopters)
                {
                    dto.DisasterResponseHelicopters = configurationItem.Enabled;
                    dto.DisasterResponseHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.DisasterResponseHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DisasterResponseVehicles)
                {
                    dto.DisasterResponseVehicles = configurationItem.Enabled;
                    dto.DisasterResponseVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.DisasterResponseVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DrinkingWaterPollution)
                {
                    dto.DrinkingWaterPollution = configurationItem.Enabled;
                    dto.DrinkingWaterPollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.DrinkingWaterPollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Electricity)
                {
                    dto.Electricity = configurationItem.Enabled;
                    dto.ElectricityCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.ElectricitySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.ElementarySchool)
                {
                    dto.ElementarySchool = configurationItem.Enabled;
                    dto.ElementarySchoolCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.ElementarySchoolSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireDepartmentVehicles)
                {
                    dto.FireDepartmentVehicles = configurationItem.Enabled;
                    dto.FireDepartmentVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.FireDepartmentVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireHazard)
                {
                    dto.FireHazard = configurationItem.Enabled;
                    dto.FireHazardCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.FireHazardSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireHelicopters)
                {
                    dto.FireHelicopters = configurationItem.Enabled;
                    dto.FireHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.FireHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GarbageProcessing)
                {
                    dto.GarbageProcessing = configurationItem.Enabled;
                    dto.GarbageProcessingCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.GarbageProcessingSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GarbageProcessingVehicles)
                {
                    dto.GarbageProcessingVehicles = configurationItem.Enabled;
                    dto.GarbageProcessingVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.GarbageProcessingVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GroundPollution)
                {
                    dto.GroundPollution = configurationItem.Enabled;
                    dto.GroundPollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.GroundPollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Healthcare)
                {
                    dto.Healthcare = configurationItem.Enabled;
                    dto.HealthcareCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.HealthcareSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.HealthcareVehicles)
                {
                    dto.HealthcareVehicles = configurationItem.Enabled;
                    dto.HealthcareVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.HealthcareVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Heating)
                {
                    dto.Heating = configurationItem.Enabled;
                    dto.HeatingCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.HeatingSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.HighSchool)
                {
                    dto.HighSchool = configurationItem.Enabled;
                    dto.HighSchoolCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.HighSchoolSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Landfill)
                {
                    dto.Landfill = configurationItem.Enabled;
                    dto.LandfillCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.LandfillSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.LandfillVehicles)
                {
                    dto.LandfillVehicles = configurationItem.Enabled;
                    dto.LandfillVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.LandfillVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.MedicalHelicopters)
                {
                    dto.MedicalHelicopters = configurationItem.Enabled;
                    dto.MedicalHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.MedicalHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.NoisePollution)
                {
                    dto.NoisePollution = configurationItem.Enabled;
                    dto.NoisePollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.NoisePollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.ParkMaintenanceVehicles)
                {
                    dto.ParkMaintenanceVehicles = configurationItem.Enabled;
                    dto.ParkMaintenanceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.ParkMaintenanceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceHelicopters)
                {
                    dto.PoliceHelicopters = configurationItem.Enabled;
                    dto.PoliceHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PoliceHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceHoldingCells)
                {
                    dto.PoliceHoldingCells = configurationItem.Enabled;
                    dto.PoliceHoldingCellsCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PoliceHoldingCellsSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceVehicles)
                {
                    dto.PoliceVehicles = configurationItem.Enabled;
                    dto.PoliceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PoliceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PostTrucks)
                {
                    dto.PostTrucks = configurationItem.Enabled;
                    dto.PostTrucksCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PostTrucksSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PostVans)
                {
                    dto.PostVans = configurationItem.Enabled;
                    dto.PostVansCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PostVansSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PrisonCells)
                {
                    dto.PrisonCells = configurationItem.Enabled;
                    dto.PrisonCellsCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PrisonCellsSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PrisonVehicles)
                {
                    dto.PrisonVehicles = configurationItem.Enabled;
                    dto.PrisonVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PrisonVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.RoadMaintenanceVehicles)
                {
                    dto.RoadMaintenanceVehicles = configurationItem.Enabled;
                    dto.RoadMaintenanceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.RoadMaintenanceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SewageTreatment)
                {
                    dto.SewageTreatment = configurationItem.Enabled;
                    dto.SewageTreatmentCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.SewageTreatmentSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SnowDump)
                {
                    dto.SnowDump = configurationItem.Enabled;
                    dto.SnowDumpCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.SnowDumpSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SnowDumpVehicles)
                {
                    dto.SnowDumpVehicles = configurationItem.Enabled;
                    dto.SnowDumpVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.SnowDumpVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Taxis)
                {
                    dto.Taxis = configurationItem.Enabled;
                    dto.TaxisCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.TaxisSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.TrafficJam)
                {
                    dto.TrafficJam = configurationItem.Enabled;
                    dto.TrafficJamCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.TrafficJamSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Unemployment)
                {
                    dto.Unemployment = configurationItem.Enabled;
                    dto.UnemploymentCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.UnemploymentSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.University)
                {
                    dto.University = configurationItem.Enabled;
                    dto.UniversityCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.UniversitySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Water)
                {
                    dto.Water = configurationItem.Enabled;
                    dto.WaterCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.WaterSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterPumpingServiceStorage)
                {
                    dto.WaterPumpingServiceStorage = configurationItem.Enabled;
                    dto.WaterPumpingServiceStorageCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.WaterPumpingServiceStorageSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterPumpingServiceVehicles)
                {
                    dto.WaterPumpingServiceVehicles = configurationItem.Enabled;
                    dto.WaterPumpingServiceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.WaterPumpingServiceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterReserveTank)
                {
                    dto.WaterReserveTank = configurationItem.Enabled;
                    dto.WaterReserveTankCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.WaterReserveTankSortOrder = configurationItem.SortOrder;
                }
                else
                {
                    throw new Exception("Unknown configItem.Item type");
                }
            }
        }

        private void UpdateConfigurationItems()
        {
            foreach (var configurationItem in this.configurationItems)
            {
                if (configurationItem.ItemData == ItemData.AverageIllnessRate)
                {
                    configurationItem.Enabled = dto.AverageIllnessRate;
                    configurationItem.CriticalThreshold = dto.AverageIllnessRateCriticalThreshold;
                    configurationItem.SortOrder = dto.AverageIllnessRateSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Cemetery)
                {
                    configurationItem.Enabled = dto.Cemetery;
                    configurationItem.CriticalThreshold = dto.CemeteryCriticalThreshold;
                    configurationItem.SortOrder = dto.CemeterySortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CemeteryVehicles)
                {
                    configurationItem.Enabled = dto.CemeteryVehicles;
                    configurationItem.CriticalThreshold = dto.CemeteryVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.CemeteryVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CityUnattractiveness)
                {
                    configurationItem.Enabled = dto.CityUnattractiveness;
                    configurationItem.CriticalThreshold = dto.CityUnattractivenessCriticalThreshold;
                    configurationItem.SortOrder = dto.CityUnattractivenessSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Crematorium)
                {
                    configurationItem.Enabled = dto.Crematorium;
                    configurationItem.CriticalThreshold = dto.CrematoriumCriticalThreshold;
                    configurationItem.SortOrder = dto.CrematoriumSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CrematoriumVehicles)
                {
                    configurationItem.Enabled = dto.CrematoriumVehicles;
                    configurationItem.CriticalThreshold = dto.CrematoriumVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.CrematoriumVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CrimeRate)
                {
                    configurationItem.Enabled = dto.CrimeRate;
                    configurationItem.CriticalThreshold = dto.CrimeRateCriticalThreshold;
                    configurationItem.SortOrder = dto.CrimeRateSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DisasterResponseHelicopters)
                {
                    configurationItem.Enabled = dto.DisasterResponseHelicopters;
                    configurationItem.CriticalThreshold = dto.DisasterResponseHelicoptersCriticalThreshold;
                    configurationItem.SortOrder = dto.DisasterResponseHelicoptersSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DisasterResponseVehicles)
                {
                    configurationItem.Enabled = dto.DisasterResponseVehicles;
                    configurationItem.CriticalThreshold = dto.DisasterResponseVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.DisasterResponseVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DrinkingWaterPollution)
                {
                    configurationItem.Enabled = dto.DrinkingWaterPollution;
                    configurationItem.CriticalThreshold = dto.DrinkingWaterPollutionCriticalThreshold;
                    configurationItem.SortOrder = dto.DrinkingWaterPollutionSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Electricity)
                {
                    configurationItem.Enabled = dto.Electricity;
                    configurationItem.CriticalThreshold = dto.ElectricityCriticalThreshold;
                    configurationItem.SortOrder = dto.ElectricitySortOrder;
                }
                else if (configurationItem.ItemData == ItemData.ElementarySchool)
                {
                    configurationItem.Enabled = dto.ElementarySchool;
                    configurationItem.CriticalThreshold = dto.ElementarySchoolCriticalThreshold;
                    configurationItem.SortOrder = dto.ElementarySchoolSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireDepartmentVehicles)
                {
                    configurationItem.Enabled = dto.FireDepartmentVehicles;
                    configurationItem.CriticalThreshold = dto.FireDepartmentVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.FireDepartmentVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireHazard)
                {
                    configurationItem.Enabled = dto.FireHazard;
                    configurationItem.CriticalThreshold = dto.FireHazardCriticalThreshold;
                    configurationItem.SortOrder = dto.FireHazardSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireHelicopters)
                {
                    configurationItem.Enabled = dto.FireHelicopters;
                    configurationItem.CriticalThreshold = dto.FireHelicoptersCriticalThreshold;
                    configurationItem.SortOrder = dto.FireHelicoptersSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GarbageProcessing)
                {
                    configurationItem.Enabled = dto.GarbageProcessing;
                    configurationItem.CriticalThreshold = dto.GarbageProcessingCriticalThreshold;
                    configurationItem.SortOrder = dto.GarbageProcessingSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GarbageProcessingVehicles)
                {
                    configurationItem.Enabled = dto.GarbageProcessingVehicles;
                    configurationItem.CriticalThreshold = dto.GarbageProcessingVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.GarbageProcessingVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GroundPollution)
                {
                    configurationItem.Enabled = dto.GroundPollution;
                    configurationItem.CriticalThreshold = dto.GroundPollutionCriticalThreshold;
                    configurationItem.SortOrder = dto.GroundPollutionSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Healthcare)
                {
                    configurationItem.Enabled = dto.Healthcare;
                    configurationItem.CriticalThreshold = dto.HealthcareCriticalThreshold;
                    configurationItem.SortOrder = dto.HealthcareSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.HealthcareVehicles)
                {
                    configurationItem.Enabled = dto.HealthcareVehicles;
                    configurationItem.CriticalThreshold = dto.HealthcareVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.HealthcareVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Heating)
                {
                    configurationItem.Enabled = dto.Heating;
                    configurationItem.CriticalThreshold = dto.HeatingCriticalThreshold;
                    configurationItem.SortOrder = dto.HeatingSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.HighSchool)
                {
                    configurationItem.Enabled = dto.HighSchool;
                    configurationItem.CriticalThreshold = dto.HighSchoolCriticalThreshold;
                    configurationItem.SortOrder = dto.HighSchoolSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Landfill)
                {
                    configurationItem.Enabled = dto.Landfill;
                    configurationItem.CriticalThreshold = dto.LandfillCriticalThreshold;
                    configurationItem.SortOrder = dto.LandfillSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.LandfillVehicles)
                {
                    configurationItem.Enabled = dto.LandfillVehicles;
                    configurationItem.CriticalThreshold = dto.LandfillVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.LandfillVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.MedicalHelicopters)
                {
                    configurationItem.Enabled = dto.MedicalHelicopters;
                    configurationItem.CriticalThreshold = dto.MedicalHelicoptersCriticalThreshold;
                    configurationItem.SortOrder = dto.MedicalHelicoptersSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.NoisePollution)
                {
                    configurationItem.Enabled = dto.NoisePollution;
                    configurationItem.CriticalThreshold = dto.NoisePollutionCriticalThreshold;
                    configurationItem.SortOrder = dto.NoisePollutionSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.ParkMaintenanceVehicles)
                {
                    configurationItem.Enabled = dto.ParkMaintenanceVehicles;
                    configurationItem.CriticalThreshold = dto.ParkMaintenanceVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.ParkMaintenanceVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceHelicopters)
                {
                    configurationItem.Enabled = dto.PoliceHelicopters;
                    configurationItem.CriticalThreshold = dto.PoliceHelicoptersCriticalThreshold;
                    configurationItem.SortOrder = dto.PoliceHelicoptersSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceHoldingCells)
                {
                    configurationItem.Enabled = dto.PoliceHoldingCells;
                    configurationItem.CriticalThreshold = dto.PoliceHoldingCellsCriticalThreshold;
                    configurationItem.SortOrder = dto.PoliceHoldingCellsSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceVehicles)
                {
                    configurationItem.Enabled = dto.PoliceVehicles;
                    configurationItem.CriticalThreshold = dto.PoliceVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.PoliceVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PostTrucks)
                {
                    configurationItem.Enabled = dto.PostTrucks;
                    configurationItem.CriticalThreshold = dto.PostTrucksCriticalThreshold;
                    configurationItem.SortOrder = dto.PostTrucksSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PostVans)
                {
                    configurationItem.Enabled = dto.PostVans;
                    configurationItem.CriticalThreshold = dto.PostVansCriticalThreshold;
                    configurationItem.SortOrder = dto.PostVansSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PrisonCells)
                {
                    configurationItem.Enabled = dto.PrisonCells;
                    configurationItem.CriticalThreshold = dto.PrisonCellsCriticalThreshold;
                    configurationItem.SortOrder = dto.PrisonCellsSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PrisonVehicles)
                {
                    configurationItem.Enabled = dto.PrisonVehicles;
                    configurationItem.CriticalThreshold = dto.PrisonVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.PrisonVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.RoadMaintenanceVehicles)
                {
                    configurationItem.Enabled = dto.RoadMaintenanceVehicles;
                    configurationItem.CriticalThreshold = dto.RoadMaintenanceVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.RoadMaintenanceVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SewageTreatment)
                {
                    configurationItem.Enabled = dto.SewageTreatment;
                    configurationItem.CriticalThreshold = dto.SewageTreatmentCriticalThreshold;
                    configurationItem.SortOrder = dto.SewageTreatmentSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SnowDump)
                {
                    configurationItem.Enabled = dto.SnowDump;
                    configurationItem.CriticalThreshold = dto.SnowDumpCriticalThreshold;
                    configurationItem.SortOrder = dto.SnowDumpSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SnowDumpVehicles)
                {
                    configurationItem.Enabled = dto.SnowDumpVehicles;
                    configurationItem.CriticalThreshold = dto.SnowDumpVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.SnowDumpVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Taxis)
                {
                    configurationItem.Enabled = dto.Taxis;
                    configurationItem.CriticalThreshold = dto.TaxisCriticalThreshold;
                    configurationItem.SortOrder = dto.TaxisSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.TrafficJam)
                {
                    configurationItem.Enabled = dto.TrafficJam;
                    configurationItem.CriticalThreshold = dto.TrafficJamCriticalThreshold;
                    configurationItem.SortOrder = dto.TrafficJamSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Unemployment)
                {
                    configurationItem.Enabled = dto.Unemployment;
                    configurationItem.CriticalThreshold = dto.UnemploymentCriticalThreshold;
                    configurationItem.SortOrder = dto.UnemploymentSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.University)
                {
                    configurationItem.Enabled = dto.University;
                    configurationItem.CriticalThreshold = dto.UniversityCriticalThreshold;
                    configurationItem.SortOrder = dto.UniversitySortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Water)
                {
                    configurationItem.Enabled = dto.Water;
                    configurationItem.CriticalThreshold = dto.WaterCriticalThreshold;
                    configurationItem.SortOrder = dto.WaterSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterPumpingServiceStorage)
                {
                    configurationItem.Enabled = dto.WaterPumpingServiceStorage;
                    configurationItem.CriticalThreshold = dto.WaterPumpingServiceStorageCriticalThreshold;
                    configurationItem.SortOrder = dto.WaterPumpingServiceStorageSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterPumpingServiceVehicles)
                {
                    configurationItem.Enabled = dto.WaterPumpingServiceVehicles;
                    configurationItem.CriticalThreshold = dto.WaterPumpingServiceVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.WaterPumpingServiceVehiclesSortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterReserveTank)
                {
                    configurationItem.Enabled = dto.WaterReserveTank;
                    configurationItem.CriticalThreshold = dto.WaterReserveTankCriticalThreshold;
                    configurationItem.SortOrder = dto.WaterReserveTankSortOrder;
                }
                else
                {
                    throw new Exception("Unknown configItem.Item type");
                }
            }
        }

        public event Action LayoutPropertyChanged;

        private void OnLayoutPropertyChanged()
        {
            LayoutPropertyChanged?.Invoke();
        }

        public event Action PositionChanged;

        private void OnPositionChanged()
        {
            PositionChanged?.Invoke();
        }
    }
}
