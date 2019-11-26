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
                new ConfigurationItemModel(Item.AverageIllnessRate, dto.AverageIllnessRate, dto.AverageIllnessRateCriticalThreshold, dto.AverageIllnessRateSortOrder),
                new ConfigurationItemModel(Item.Cemetery, dto.Cemetery, dto.CemeteryCriticalThreshold, dto.CemeterySortOrder),
                new ConfigurationItemModel(Item.CemeteryVehicles, dto.CemeteryVehicles, dto.CemeteryVehiclesCriticalThreshold, dto.CemeteryVehiclesSortOrder),
                new ConfigurationItemModel(Item.CityUnattractiveness, dto.CityUnattractiveness, dto.CityUnattractivenessCriticalThreshold, dto.CityUnattractivenessSortOrder),
                new ConfigurationItemModel(Item.Crematorium, dto.Crematorium, dto.CrematoriumCriticalThreshold, dto.CrematoriumSortOrder),
                new ConfigurationItemModel(Item.CrematoriumVehicles, dto.CrematoriumVehicles, dto.CrematoriumVehiclesCriticalThreshold, dto.CrematoriumVehiclesSortOrder),
                new ConfigurationItemModel(Item.CrimeRate, dto.CrimeRate, dto.CrimeRateCriticalThreshold, dto.CrimeRateSortOrder),
                new ConfigurationItemModel(Item.DisasterResponseHelicopters, dto.DisasterResponseHelicopters, dto.DisasterResponseHelicoptersCriticalThreshold, dto.DisasterResponseHelicoptersSortOrder),
                new ConfigurationItemModel(Item.DisasterResponseVehicles, dto.DisasterResponseVehicles, dto.DisasterResponseVehiclesCriticalThreshold, dto.DisasterResponseVehiclesSortOrder),
                new ConfigurationItemModel(Item.DrinkingWaterPollution, dto.DrinkingWaterPollution, dto.DrinkingWaterPollutionCriticalThreshold, dto.DrinkingWaterPollutionSortOrder),
                new ConfigurationItemModel(Item.Electricity, dto.Electricity, dto.ElectricityCriticalThreshold, dto.ElectricitySortOrder),
                new ConfigurationItemModel(Item.ElementarySchool, dto.ElementarySchool, dto.ElementarySchoolCriticalThreshold, dto.ElementarySchoolSortOrder),
                new ConfigurationItemModel(Item.FireDepartmentVehicles, dto.FireDepartmentVehicles, dto.FireDepartmentVehiclesCriticalThreshold, dto.FireDepartmentVehiclesSortOrder),
                new ConfigurationItemModel(Item.FireHazard, dto.FireHazard, dto.FireHazardCriticalThreshold, dto.FireHazardSortOrder),
                new ConfigurationItemModel(Item.FireHelicopters, dto.FireHelicopters, dto.FireHelicoptersCriticalThreshold, dto.FireHelicoptersSortOrder),
                new ConfigurationItemModel(Item.GarbageProcessing, dto.GarbageProcessing, dto.GarbageProcessingCriticalThreshold, dto.GarbageProcessingSortOrder),
                new ConfigurationItemModel(Item.GarbageProcessingVehicles, dto.GarbageProcessingVehicles, dto.GarbageProcessingVehiclesCriticalThreshold, dto.GarbageProcessingVehiclesSortOrder),
                new ConfigurationItemModel(Item.GroundPollution, dto.GroundPollution, dto.GroundPollutionCriticalThreshold, dto.GroundPollutionSortOrder),
                new ConfigurationItemModel(Item.Healthcare, dto.Healthcare, dto.HealthcareCriticalThreshold, dto.HealthcareSortOrder),
                new ConfigurationItemModel(Item.HealthcareVehicles, dto.HealthcareVehicles, dto.HealthcareVehiclesCriticalThreshold, dto.HealthcareVehiclesSortOrder),
                new ConfigurationItemModel(Item.Heating, dto.Heating, dto.HeatingCriticalThreshold, dto.HeatingSortOrder),
                new ConfigurationItemModel(Item.HighSchool, dto.HighSchool, dto.HighSchoolCriticalThreshold, dto.HighSchoolSortOrder),
                new ConfigurationItemModel(Item.Landfill, dto.Landfill, dto.LandfillCriticalThreshold, dto.LandfillSortOrder),
                new ConfigurationItemModel(Item.LandfillVehicles, dto.LandfillVehicles, dto.LandfillVehiclesCriticalThreshold, dto.LandfillVehiclesSortOrder),
                new ConfigurationItemModel(Item.MedicalHelicopters, dto.MedicalHelicopters, dto.MedicalHelicoptersCriticalThreshold, dto.MedicalHelicoptersSortOrder),
                new ConfigurationItemModel(Item.NoisePollution, dto.NoisePollution, dto.NoisePollutionCriticalThreshold, dto.NoisePollutionSortOrder),
                new ConfigurationItemModel(Item.ParkMaintenanceVehicles, dto.ParkMaintenanceVehicles, dto.ParkMaintenanceVehiclesCriticalThreshold, dto.ParkMaintenanceVehiclesSortOrder),
                new ConfigurationItemModel(Item.PoliceHelicopters, dto.PoliceHelicopters, dto.PoliceHelicoptersCriticalThreshold, dto.PoliceHelicoptersSortOrder),
                new ConfigurationItemModel(Item.PoliceHoldingCells, dto.PoliceHoldingCells, dto.PoliceHoldingCellsCriticalThreshold, dto.PoliceHoldingCellsSortOrder),
                new ConfigurationItemModel(Item.PoliceVehicles, dto.PoliceVehicles, dto.PoliceVehiclesCriticalThreshold, dto.PoliceVehiclesSortOrder),
                new ConfigurationItemModel(Item.PostTrucks, dto.PostTrucks, dto.PostTrucksCriticalThreshold, dto.PostTrucksSortOrder),
                new ConfigurationItemModel(Item.PostVans, dto.PostVans, dto.PostVansCriticalThreshold, dto.PostVansSortOrder),
                new ConfigurationItemModel(Item.PrisonCells, dto.PrisonCells, dto.PrisonCellsCriticalThreshold, dto.PrisonCellsSortOrder),
                new ConfigurationItemModel(Item.PrisonVehicles, dto.PrisonVehicles, dto.PrisonVehiclesCriticalThreshold, dto.PrisonVehiclesSortOrder),
                new ConfigurationItemModel(Item.RoadMaintenanceVehicles, dto.RoadMaintenanceVehicles, dto.RoadMaintenanceVehiclesCriticalThreshold, dto.RoadMaintenanceVehiclesSortOrder),
                new ConfigurationItemModel(Item.SewageTreatment, dto.SewageTreatment, dto.SewageTreatmentCriticalThreshold, dto.SewageTreatmentSortOrder),
                new ConfigurationItemModel(Item.SnowDump, dto.SnowDump, dto.SnowDumpCriticalThreshold, dto.SnowDumpSortOrder),
                new ConfigurationItemModel(Item.SnowDumpVehicles, dto.SnowDumpVehicles, dto.SnowDumpVehiclesCriticalThreshold, dto.SnowDumpVehiclesSortOrder),
                new ConfigurationItemModel(Item.Taxis, dto.Taxis, dto.TaxisCriticalThreshold, dto.TaxisSortOrder),
                new ConfigurationItemModel(Item.TrafficJam, dto.TrafficJam, dto.TrafficJamCriticalThreshold, dto.TrafficJamSortOrder),
                new ConfigurationItemModel(Item.Unemployment, dto.Unemployment, dto.UnemploymentCriticalThreshold, dto.UnemploymentSortOrder),
                new ConfigurationItemModel(Item.University, dto.University, dto.UniversityCriticalThreshold, dto.UniversitySortOrder),
                new ConfigurationItemModel(Item.Water, dto.Water, dto.WaterCriticalThreshold, dto.WaterSortOrder),
                new ConfigurationItemModel(Item.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorageCriticalThreshold, dto.WaterPumpingServiceStorageSortOrder),
                new ConfigurationItemModel(Item.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehiclesCriticalThreshold, dto.WaterPumpingServiceVehiclesSortOrder),
                new ConfigurationItemModel(Item.WaterReserveTank, dto.WaterReserveTank, dto.WaterReserveTankCriticalThreshold, dto.WaterReserveTankSortOrder)
            };
            ValidateIndexes(this.configurationItems);
            AddItemEvents(this.configurationItems);
        }

        private void ValidateIndexes(ConfigurationItemModel[] configurationItems)
        {
            for (int i = 0; i < configurationItems.Length; i++)
            {
                if (i != configurationItems[i].Item.Index)
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

        public ConfigurationItemModel GetConfigurationItem(Item item)
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
                if (configurationItem.Item == Item.AverageIllnessRate)
                {
                    dto.AverageIllnessRate = configurationItem.Enabled;
                    dto.AverageIllnessRateCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.AverageIllnessRateSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Cemetery)
                {
                    dto.Cemetery = configurationItem.Enabled;
                    dto.CemeteryCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CemeterySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.CemeteryVehicles)
                {
                    dto.CemeteryVehicles = configurationItem.Enabled;
                    dto.CemeteryVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CemeteryVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.CityUnattractiveness)
                {
                    dto.CityUnattractiveness = configurationItem.Enabled;
                    dto.CityUnattractivenessCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CityUnattractivenessSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Crematorium)
                {
                    dto.Crematorium = configurationItem.Enabled;
                    dto.CrematoriumCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CrematoriumSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.CrematoriumVehicles)
                {
                    dto.CrematoriumVehicles = configurationItem.Enabled;
                    dto.CrematoriumVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CrematoriumVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.CrimeRate)
                {
                    dto.CrimeRate = configurationItem.Enabled;
                    dto.CrimeRateCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.CrimeRateSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.DisasterResponseHelicopters)
                {
                    dto.DisasterResponseHelicopters = configurationItem.Enabled;
                    dto.DisasterResponseHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.DisasterResponseHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.DisasterResponseVehicles)
                {
                    dto.DisasterResponseVehicles = configurationItem.Enabled;
                    dto.DisasterResponseVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.DisasterResponseVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.DrinkingWaterPollution)
                {
                    dto.DrinkingWaterPollution = configurationItem.Enabled;
                    dto.DrinkingWaterPollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.DrinkingWaterPollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Electricity)
                {
                    dto.Electricity = configurationItem.Enabled;
                    dto.ElectricityCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.ElectricitySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.ElementarySchool)
                {
                    dto.ElementarySchool = configurationItem.Enabled;
                    dto.ElementarySchoolCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.ElementarySchoolSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.FireDepartmentVehicles)
                {
                    dto.FireDepartmentVehicles = configurationItem.Enabled;
                    dto.FireDepartmentVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.FireDepartmentVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.FireHazard)
                {
                    dto.FireHazard = configurationItem.Enabled;
                    dto.FireHazardCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.FireHazardSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.FireHelicopters)
                {
                    dto.FireHelicopters = configurationItem.Enabled;
                    dto.FireHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.FireHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.GarbageProcessing)
                {
                    dto.GarbageProcessing = configurationItem.Enabled;
                    dto.GarbageProcessingCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.GarbageProcessingSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.GarbageProcessingVehicles)
                {
                    dto.GarbageProcessingVehicles = configurationItem.Enabled;
                    dto.GarbageProcessingVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.GarbageProcessingVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.GroundPollution)
                {
                    dto.GroundPollution = configurationItem.Enabled;
                    dto.GroundPollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.GroundPollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Healthcare)
                {
                    dto.Healthcare = configurationItem.Enabled;
                    dto.HealthcareCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.HealthcareSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.HealthcareVehicles)
                {
                    dto.HealthcareVehicles = configurationItem.Enabled;
                    dto.HealthcareVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.HealthcareVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Heating)
                {
                    dto.Heating = configurationItem.Enabled;
                    dto.HeatingCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.HeatingSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.HighSchool)
                {
                    dto.HighSchool = configurationItem.Enabled;
                    dto.HighSchoolCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.HighSchoolSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Landfill)
                {
                    dto.Landfill = configurationItem.Enabled;
                    dto.LandfillCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.LandfillSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.LandfillVehicles)
                {
                    dto.LandfillVehicles = configurationItem.Enabled;
                    dto.LandfillVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.LandfillVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.MedicalHelicopters)
                {
                    dto.MedicalHelicopters = configurationItem.Enabled;
                    dto.MedicalHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.MedicalHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.NoisePollution)
                {
                    dto.NoisePollution = configurationItem.Enabled;
                    dto.NoisePollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.NoisePollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.ParkMaintenanceVehicles)
                {
                    dto.ParkMaintenanceVehicles = configurationItem.Enabled;
                    dto.ParkMaintenanceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.ParkMaintenanceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.PoliceHelicopters)
                {
                    dto.PoliceHelicopters = configurationItem.Enabled;
                    dto.PoliceHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PoliceHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.PoliceHoldingCells)
                {
                    dto.PoliceHoldingCells = configurationItem.Enabled;
                    dto.PoliceHoldingCellsCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PoliceHoldingCellsSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.PoliceVehicles)
                {
                    dto.PoliceVehicles = configurationItem.Enabled;
                    dto.PoliceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PoliceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.PostTrucks)
                {
                    dto.PostTrucks = configurationItem.Enabled;
                    dto.PostTrucksCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PostTrucksSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.PostVans)
                {
                    dto.PostVans = configurationItem.Enabled;
                    dto.PostVansCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PostVansSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.PrisonCells)
                {
                    dto.PrisonCells = configurationItem.Enabled;
                    dto.PrisonCellsCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PrisonCellsSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.PrisonVehicles)
                {
                    dto.PrisonVehicles = configurationItem.Enabled;
                    dto.PrisonVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.PrisonVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.RoadMaintenanceVehicles)
                {
                    dto.RoadMaintenanceVehicles = configurationItem.Enabled;
                    dto.RoadMaintenanceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.RoadMaintenanceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.SewageTreatment)
                {
                    dto.SewageTreatment = configurationItem.Enabled;
                    dto.SewageTreatmentCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.SewageTreatmentSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.SnowDump)
                {
                    dto.SnowDump = configurationItem.Enabled;
                    dto.SnowDumpCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.SnowDumpSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.SnowDumpVehicles)
                {
                    dto.SnowDumpVehicles = configurationItem.Enabled;
                    dto.SnowDumpVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.SnowDumpVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Taxis)
                {
                    dto.Taxis = configurationItem.Enabled;
                    dto.TaxisCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.TaxisSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.TrafficJam)
                {
                    dto.TrafficJam = configurationItem.Enabled;
                    dto.TrafficJamCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.TrafficJamSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Unemployment)
                {
                    dto.Unemployment = configurationItem.Enabled;
                    dto.UnemploymentCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.UnemploymentSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.University)
                {
                    dto.University = configurationItem.Enabled;
                    dto.UniversityCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.UniversitySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.Water)
                {
                    dto.Water = configurationItem.Enabled;
                    dto.WaterCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.WaterSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.WaterPumpingServiceStorage)
                {
                    dto.WaterPumpingServiceStorage = configurationItem.Enabled;
                    dto.WaterPumpingServiceStorageCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.WaterPumpingServiceStorageSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.WaterPumpingServiceVehicles)
                {
                    dto.WaterPumpingServiceVehicles = configurationItem.Enabled;
                    dto.WaterPumpingServiceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    dto.WaterPumpingServiceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.Item == Item.WaterReserveTank)
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
                if (configurationItem.Item == Item.AverageIllnessRate)
                {
                    configurationItem.Enabled = dto.AverageIllnessRate;
                    configurationItem.CriticalThreshold = dto.AverageIllnessRateCriticalThreshold;
                    configurationItem.SortOrder = dto.AverageIllnessRateSortOrder;
                }
                else if (configurationItem.Item == Item.Cemetery)
                {
                    configurationItem.Enabled = dto.Cemetery;
                    configurationItem.CriticalThreshold = dto.CemeteryCriticalThreshold;
                    configurationItem.SortOrder = dto.CemeterySortOrder;
                }
                else if (configurationItem.Item == Item.CemeteryVehicles)
                {
                    configurationItem.Enabled = dto.CemeteryVehicles;
                    configurationItem.CriticalThreshold = dto.CemeteryVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.CemeteryVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.CityUnattractiveness)
                {
                    configurationItem.Enabled = dto.CityUnattractiveness;
                    configurationItem.CriticalThreshold = dto.CityUnattractivenessCriticalThreshold;
                    configurationItem.SortOrder = dto.CityUnattractivenessSortOrder;
                }
                else if (configurationItem.Item == Item.Crematorium)
                {
                    configurationItem.Enabled = dto.Crematorium;
                    configurationItem.CriticalThreshold = dto.CrematoriumCriticalThreshold;
                    configurationItem.SortOrder = dto.CrematoriumSortOrder;
                }
                else if (configurationItem.Item == Item.CrematoriumVehicles)
                {
                    configurationItem.Enabled = dto.CrematoriumVehicles;
                    configurationItem.CriticalThreshold = dto.CrematoriumVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.CrematoriumVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.CrimeRate)
                {
                    configurationItem.Enabled = dto.CrimeRate;
                    configurationItem.CriticalThreshold = dto.CrimeRateCriticalThreshold;
                    configurationItem.SortOrder = dto.CrimeRateSortOrder;
                }
                else if (configurationItem.Item == Item.DisasterResponseHelicopters)
                {
                    configurationItem.Enabled = dto.DisasterResponseHelicopters;
                    configurationItem.CriticalThreshold = dto.DisasterResponseHelicoptersCriticalThreshold;
                    configurationItem.SortOrder = dto.DisasterResponseHelicoptersSortOrder;
                }
                else if (configurationItem.Item == Item.DisasterResponseVehicles)
                {
                    configurationItem.Enabled = dto.DisasterResponseVehicles;
                    configurationItem.CriticalThreshold = dto.DisasterResponseVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.DisasterResponseVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.DrinkingWaterPollution)
                {
                    configurationItem.Enabled = dto.DrinkingWaterPollution;
                    configurationItem.CriticalThreshold = dto.DrinkingWaterPollutionCriticalThreshold;
                    configurationItem.SortOrder = dto.DrinkingWaterPollutionSortOrder;
                }
                else if (configurationItem.Item == Item.Electricity)
                {
                    configurationItem.Enabled = dto.Electricity;
                    configurationItem.CriticalThreshold = dto.ElectricityCriticalThreshold;
                    configurationItem.SortOrder = dto.ElectricitySortOrder;
                }
                else if (configurationItem.Item == Item.ElementarySchool)
                {
                    configurationItem.Enabled = dto.ElementarySchool;
                    configurationItem.CriticalThreshold = dto.ElementarySchoolCriticalThreshold;
                    configurationItem.SortOrder = dto.ElementarySchoolSortOrder;
                }
                else if (configurationItem.Item == Item.FireDepartmentVehicles)
                {
                    configurationItem.Enabled = dto.FireDepartmentVehicles;
                    configurationItem.CriticalThreshold = dto.FireDepartmentVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.FireDepartmentVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.FireHazard)
                {
                    configurationItem.Enabled = dto.FireHazard;
                    configurationItem.CriticalThreshold = dto.FireHazardCriticalThreshold;
                    configurationItem.SortOrder = dto.FireHazardSortOrder;
                }
                else if (configurationItem.Item == Item.FireHelicopters)
                {
                    configurationItem.Enabled = dto.FireHelicopters;
                    configurationItem.CriticalThreshold = dto.FireHelicoptersCriticalThreshold;
                    configurationItem.SortOrder = dto.FireHelicoptersSortOrder;
                }
                else if (configurationItem.Item == Item.GarbageProcessing)
                {
                    configurationItem.Enabled = dto.GarbageProcessing;
                    configurationItem.CriticalThreshold = dto.GarbageProcessingCriticalThreshold;
                    configurationItem.SortOrder = dto.GarbageProcessingSortOrder;
                }
                else if (configurationItem.Item == Item.GarbageProcessingVehicles)
                {
                    configurationItem.Enabled = dto.GarbageProcessingVehicles;
                    configurationItem.CriticalThreshold = dto.GarbageProcessingVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.GarbageProcessingVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.GroundPollution)
                {
                    configurationItem.Enabled = dto.GroundPollution;
                    configurationItem.CriticalThreshold = dto.GroundPollutionCriticalThreshold;
                    configurationItem.SortOrder = dto.GroundPollutionSortOrder;
                }
                else if (configurationItem.Item == Item.Healthcare)
                {
                    configurationItem.Enabled = dto.Healthcare;
                    configurationItem.CriticalThreshold = dto.HealthcareCriticalThreshold;
                    configurationItem.SortOrder = dto.HealthcareSortOrder;
                }
                else if (configurationItem.Item == Item.HealthcareVehicles)
                {
                    configurationItem.Enabled = dto.HealthcareVehicles;
                    configurationItem.CriticalThreshold = dto.HealthcareVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.HealthcareVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.Heating)
                {
                    configurationItem.Enabled = dto.Heating;
                    configurationItem.CriticalThreshold = dto.HeatingCriticalThreshold;
                    configurationItem.SortOrder = dto.HeatingSortOrder;
                }
                else if (configurationItem.Item == Item.HighSchool)
                {
                    configurationItem.Enabled = dto.HighSchool;
                    configurationItem.CriticalThreshold = dto.HighSchoolCriticalThreshold;
                    configurationItem.SortOrder = dto.HighSchoolSortOrder;
                }
                else if (configurationItem.Item == Item.Landfill)
                {
                    configurationItem.Enabled = dto.Landfill;
                    configurationItem.CriticalThreshold = dto.LandfillCriticalThreshold;
                    configurationItem.SortOrder = dto.LandfillSortOrder;
                }
                else if (configurationItem.Item == Item.LandfillVehicles)
                {
                    configurationItem.Enabled = dto.LandfillVehicles;
                    configurationItem.CriticalThreshold = dto.LandfillVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.LandfillVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.MedicalHelicopters)
                {
                    configurationItem.Enabled = dto.MedicalHelicopters;
                    configurationItem.CriticalThreshold = dto.MedicalHelicoptersCriticalThreshold;
                    configurationItem.SortOrder = dto.MedicalHelicoptersSortOrder;
                }
                else if (configurationItem.Item == Item.NoisePollution)
                {
                    configurationItem.Enabled = dto.NoisePollution;
                    configurationItem.CriticalThreshold = dto.NoisePollutionCriticalThreshold;
                    configurationItem.SortOrder = dto.NoisePollutionSortOrder;
                }
                else if (configurationItem.Item == Item.ParkMaintenanceVehicles)
                {
                    configurationItem.Enabled = dto.ParkMaintenanceVehicles;
                    configurationItem.CriticalThreshold = dto.ParkMaintenanceVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.ParkMaintenanceVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.PoliceHelicopters)
                {
                    configurationItem.Enabled = dto.PoliceHelicopters;
                    configurationItem.CriticalThreshold = dto.PoliceHelicoptersCriticalThreshold;
                    configurationItem.SortOrder = dto.PoliceHelicoptersSortOrder;
                }
                else if (configurationItem.Item == Item.PoliceHoldingCells)
                {
                    configurationItem.Enabled = dto.PoliceHoldingCells;
                    configurationItem.CriticalThreshold = dto.PoliceHoldingCellsCriticalThreshold;
                    configurationItem.SortOrder = dto.PoliceHoldingCellsSortOrder;
                }
                else if (configurationItem.Item == Item.PoliceVehicles)
                {
                    configurationItem.Enabled = dto.PoliceVehicles;
                    configurationItem.CriticalThreshold = dto.PoliceVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.PoliceVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.PostTrucks)
                {
                    configurationItem.Enabled = dto.PostTrucks;
                    configurationItem.CriticalThreshold = dto.PostTrucksCriticalThreshold;
                    configurationItem.SortOrder = dto.PostTrucksSortOrder;
                }
                else if (configurationItem.Item == Item.PostVans)
                {
                    configurationItem.Enabled = dto.PostVans;
                    configurationItem.CriticalThreshold = dto.PostVansCriticalThreshold;
                    configurationItem.SortOrder = dto.PostVansSortOrder;
                }
                else if (configurationItem.Item == Item.PrisonCells)
                {
                    configurationItem.Enabled = dto.PrisonCells;
                    configurationItem.CriticalThreshold = dto.PrisonCellsCriticalThreshold;
                    configurationItem.SortOrder = dto.PrisonCellsSortOrder;
                }
                else if (configurationItem.Item == Item.PrisonVehicles)
                {
                    configurationItem.Enabled = dto.PrisonVehicles;
                    configurationItem.CriticalThreshold = dto.PrisonVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.PrisonVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.RoadMaintenanceVehicles)
                {
                    configurationItem.Enabled = dto.RoadMaintenanceVehicles;
                    configurationItem.CriticalThreshold = dto.RoadMaintenanceVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.RoadMaintenanceVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.SewageTreatment)
                {
                    configurationItem.Enabled = dto.SewageTreatment;
                    configurationItem.CriticalThreshold = dto.SewageTreatmentCriticalThreshold;
                    configurationItem.SortOrder = dto.SewageTreatmentSortOrder;
                }
                else if (configurationItem.Item == Item.SnowDump)
                {
                    configurationItem.Enabled = dto.SnowDump;
                    configurationItem.CriticalThreshold = dto.SnowDumpCriticalThreshold;
                    configurationItem.SortOrder = dto.SnowDumpSortOrder;
                }
                else if (configurationItem.Item == Item.SnowDumpVehicles)
                {
                    configurationItem.Enabled = dto.SnowDumpVehicles;
                    configurationItem.CriticalThreshold = dto.SnowDumpVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.SnowDumpVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.Taxis)
                {
                    configurationItem.Enabled = dto.Taxis;
                    configurationItem.CriticalThreshold = dto.TaxisCriticalThreshold;
                    configurationItem.SortOrder = dto.TaxisSortOrder;
                }
                else if (configurationItem.Item == Item.TrafficJam)
                {
                    configurationItem.Enabled = dto.TrafficJam;
                    configurationItem.CriticalThreshold = dto.TrafficJamCriticalThreshold;
                    configurationItem.SortOrder = dto.TrafficJamSortOrder;
                }
                else if (configurationItem.Item == Item.Unemployment)
                {
                    configurationItem.Enabled = dto.Unemployment;
                    configurationItem.CriticalThreshold = dto.UnemploymentCriticalThreshold;
                    configurationItem.SortOrder = dto.UnemploymentSortOrder;
                }
                else if (configurationItem.Item == Item.University)
                {
                    configurationItem.Enabled = dto.University;
                    configurationItem.CriticalThreshold = dto.UniversityCriticalThreshold;
                    configurationItem.SortOrder = dto.UniversitySortOrder;
                }
                else if (configurationItem.Item == Item.Water)
                {
                    configurationItem.Enabled = dto.Water;
                    configurationItem.CriticalThreshold = dto.WaterCriticalThreshold;
                    configurationItem.SortOrder = dto.WaterSortOrder;
                }
                else if (configurationItem.Item == Item.WaterPumpingServiceStorage)
                {
                    configurationItem.Enabled = dto.WaterPumpingServiceStorage;
                    configurationItem.CriticalThreshold = dto.WaterPumpingServiceStorageCriticalThreshold;
                    configurationItem.SortOrder = dto.WaterPumpingServiceStorageSortOrder;
                }
                else if (configurationItem.Item == Item.WaterPumpingServiceVehicles)
                {
                    configurationItem.Enabled = dto.WaterPumpingServiceVehicles;
                    configurationItem.CriticalThreshold = dto.WaterPumpingServiceVehiclesCriticalThreshold;
                    configurationItem.SortOrder = dto.WaterPumpingServiceVehiclesSortOrder;
                }
                else if (configurationItem.Item == Item.WaterReserveTank)
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
