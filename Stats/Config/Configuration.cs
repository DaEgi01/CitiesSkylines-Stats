using System;
using UnityEngine;

namespace Stats.Config
{
    public class Configuration
    {
        private readonly ConfigurationService<ConfigurationDto> configurationService;
        private readonly ConfigurationItemData[] configurationItemDatas;
        
        private ConfigurationDto dto;

        public Configuration(ConfigurationService<ConfigurationDto> configurationService, ConfigurationDto dto)
        {
            this.configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            this.dto = dto ?? throw new ArgumentNullException(nameof(dto));
            this.configurationItemDatas = new[] {
                new ConfigurationItemData(ItemData.AverageIllnessRate, dto.AverageIllnessRate, dto.AverageIllnessRateCriticalThreshold, dto.AverageIllnessRateSortOrder),
                new ConfigurationItemData(ItemData.Cemetery, dto.Cemetery, dto.CemeteryCriticalThreshold, dto.CemeterySortOrder),
                new ConfigurationItemData(ItemData.CemeteryVehicles, dto.CemeteryVehicles, dto.CemeteryVehiclesCriticalThreshold, dto.CemeteryVehiclesSortOrder),
                new ConfigurationItemData(ItemData.CityUnattractiveness, dto.CityUnattractiveness, dto.CityUnattractivenessCriticalThreshold, dto.CityUnattractivenessSortOrder),
                new ConfigurationItemData(ItemData.Crematorium, dto.Crematorium, dto.CrematoriumCriticalThreshold, dto.CrematoriumSortOrder),
                new ConfigurationItemData(ItemData.CrematoriumVehicles, dto.CrematoriumVehicles, dto.CrematoriumVehiclesCriticalThreshold, dto.CrematoriumVehiclesSortOrder),
                new ConfigurationItemData(ItemData.CrimeRate, dto.CrimeRate, dto.CrimeRateCriticalThreshold, dto.CrimeRateSortOrder),
                new ConfigurationItemData(ItemData.DisasterResponseHelicopters, dto.DisasterResponseHelicopters, dto.DisasterResponseHelicoptersCriticalThreshold, dto.DisasterResponseHelicoptersSortOrder),
                new ConfigurationItemData(ItemData.DisasterResponseVehicles, dto.DisasterResponseVehicles, dto.DisasterResponseVehiclesCriticalThreshold, dto.DisasterResponseVehiclesSortOrder),
                new ConfigurationItemData(ItemData.DrinkingWaterPollution, dto.DrinkingWaterPollution, dto.DrinkingWaterPollutionCriticalThreshold, dto.DrinkingWaterPollutionSortOrder),
                new ConfigurationItemData(ItemData.Electricity, dto.Electricity, dto.ElectricityCriticalThreshold, dto.ElectricitySortOrder),
                new ConfigurationItemData(ItemData.ElementarySchool, dto.ElementarySchool, dto.ElementarySchoolCriticalThreshold, dto.ElementarySchoolSortOrder),
                new ConfigurationItemData(ItemData.FireDepartmentVehicles, dto.FireDepartmentVehicles, dto.FireDepartmentVehiclesCriticalThreshold, dto.FireDepartmentVehiclesSortOrder),
                new ConfigurationItemData(ItemData.FireHazard, dto.FireHazard, dto.FireHazardCriticalThreshold, dto.FireHazardSortOrder),
                new ConfigurationItemData(ItemData.FireHelicopters, dto.FireHelicopters, dto.FireHelicoptersCriticalThreshold, dto.FireHelicoptersSortOrder),
                new ConfigurationItemData(ItemData.GarbageProcessing, dto.GarbageProcessing, dto.GarbageProcessingCriticalThreshold, dto.GarbageProcessingSortOrder),
                new ConfigurationItemData(ItemData.GarbageProcessingVehicles, dto.GarbageProcessingVehicles, dto.GarbageProcessingVehiclesCriticalThreshold, dto.GarbageProcessingVehiclesSortOrder),
                new ConfigurationItemData(ItemData.GroundPollution, dto.GroundPollution, dto.GroundPollutionCriticalThreshold, dto.GroundPollutionSortOrder),
                new ConfigurationItemData(ItemData.Healthcare, dto.Healthcare, dto.HealthcareCriticalThreshold, dto.HealthcareSortOrder),
                new ConfigurationItemData(ItemData.HealthcareVehicles, dto.HealthcareVehicles, dto.HealthcareVehiclesCriticalThreshold, dto.HealthcareVehiclesSortOrder),
                new ConfigurationItemData(ItemData.Heating, dto.Heating, dto.HeatingCriticalThreshold, dto.HeatingSortOrder),
                new ConfigurationItemData(ItemData.HighSchool, dto.HighSchool, dto.HighSchoolCriticalThreshold, dto.HighSchoolSortOrder),
                new ConfigurationItemData(ItemData.Landfill, dto.Landfill, dto.LandfillCriticalThreshold, dto.LandfillSortOrder),
                new ConfigurationItemData(ItemData.LandfillVehicles, dto.LandfillVehicles, dto.LandfillVehiclesCriticalThreshold, dto.LandfillVehiclesSortOrder),
                new ConfigurationItemData(ItemData.MedicalHelicopters, dto.MedicalHelicopters, dto.MedicalHelicoptersCriticalThreshold, dto.MedicalHelicoptersSortOrder),
                new ConfigurationItemData(ItemData.NoisePollution, dto.NoisePollution, dto.NoisePollutionCriticalThreshold, dto.NoisePollutionSortOrder),
                new ConfigurationItemData(ItemData.ParkMaintenanceVehicles, dto.ParkMaintenanceVehicles, dto.ParkMaintenanceVehiclesCriticalThreshold, dto.ParkMaintenanceVehiclesSortOrder),
                new ConfigurationItemData(ItemData.PoliceHelicopters, dto.PoliceHelicopters, dto.PoliceHelicoptersCriticalThreshold, dto.PoliceHelicoptersSortOrder),
                new ConfigurationItemData(ItemData.PoliceHoldingCells, dto.PoliceHoldingCells, dto.PoliceHoldingCellsCriticalThreshold, dto.PoliceHoldingCellsSortOrder),
                new ConfigurationItemData(ItemData.PoliceVehicles, dto.PoliceVehicles, dto.PoliceVehiclesCriticalThreshold, dto.PoliceVehiclesSortOrder),
                new ConfigurationItemData(ItemData.PostTrucks, dto.PostTrucks, dto.PostTrucksCriticalThreshold, dto.PostTrucksSortOrder),
                new ConfigurationItemData(ItemData.PostVans, dto.PostVans, dto.PostVansCriticalThreshold, dto.PostVansSortOrder),
                new ConfigurationItemData(ItemData.PrisonCells, dto.PrisonCells, dto.PrisonCellsCriticalThreshold, dto.PrisonCellsSortOrder),
                new ConfigurationItemData(ItemData.PrisonVehicles, dto.PrisonVehicles, dto.PrisonVehiclesCriticalThreshold, dto.PrisonVehiclesSortOrder),
                new ConfigurationItemData(ItemData.RoadMaintenanceVehicles, dto.RoadMaintenanceVehicles, dto.RoadMaintenanceVehiclesCriticalThreshold, dto.RoadMaintenanceVehiclesSortOrder),
                new ConfigurationItemData(ItemData.SewageTreatment, dto.SewageTreatment, dto.SewageTreatmentCriticalThreshold, dto.SewageTreatmentSortOrder),
                new ConfigurationItemData(ItemData.SnowDump, dto.SnowDump, dto.SnowDumpCriticalThreshold, dto.SnowDumpSortOrder),
                new ConfigurationItemData(ItemData.SnowDumpVehicles, dto.SnowDumpVehicles, dto.SnowDumpVehiclesCriticalThreshold, dto.SnowDumpVehiclesSortOrder),
                new ConfigurationItemData(ItemData.Taxis, dto.Taxis, dto.TaxisCriticalThreshold, dto.TaxisSortOrder),
                new ConfigurationItemData(ItemData.TrafficJam, dto.TrafficJam, dto.TrafficJamCriticalThreshold, dto.TrafficJamSortOrder),
                new ConfigurationItemData(ItemData.Unemployment, dto.Unemployment, dto.UnemploymentCriticalThreshold, dto.UnemploymentSortOrder),
                new ConfigurationItemData(ItemData.University, dto.University, dto.UniversityCriticalThreshold, dto.UniversitySortOrder),
                new ConfigurationItemData(ItemData.Water, dto.Water, dto.WaterCriticalThreshold, dto.WaterSortOrder),
                new ConfigurationItemData(ItemData.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorageCriticalThreshold, dto.WaterPumpingServiceStorageSortOrder),
                new ConfigurationItemData(ItemData.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehiclesCriticalThreshold, dto.WaterPumpingServiceVehiclesSortOrder),
                new ConfigurationItemData(ItemData.WaterReserveTank, dto.WaterReserveTank, dto.WaterReserveTankCriticalThreshold, dto.WaterReserveTankSortOrder)
            };
            ValidateIndexes(this.configurationItemDatas);
        }

        public ConfigurationItemData[] ConfigurationItems => configurationItemDatas;

        private void ValidateIndexes(ConfigurationItemData[] configurationItems)
        {
            for (int i = 0; i < configurationItems.Length; i++)
            {
                if (i != configurationItems[i].ItemData.Index)
                {
                    throw new IndexesMessedUpException(i);
                }
            }
        }

        public Vector2 MainPanelPosition
        {
            get => new Vector2(dto.MainPanelPositionX, dto.MainPanelPositionY);
            set
            {
                dto.MainPanelPositionX = value.x;
                dto.MainPanelPositionY = value.y;
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
            }
        }

        public bool MainPanelHideItemsNotAvailable
        {
            get => dto.MainPanelHideItemsNotAvailable;
            set
            {
                dto.MainPanelHideItemsNotAvailable = value;
            }
        }

        public int MainPanelColumnCount
        {
            get => dto.MainPanelColumnCount;
            set
            {
                dto.MainPanelColumnCount = value;
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
            }
        }

        public float ItemHeight
        {
            get => dto.ItemHeight;
            set
            {
                dto.ItemHeight = value;
            }
        }

        public float ItemPadding
        {
            get => dto.ItemPadding;
            set
            {
                dto.ItemPadding = value;
            }
        }

        public float ItemTextScale
        {
            get => dto.ItemTextScale;
            set
            {
                dto.ItemTextScale = value;
            }
        }

        public ConfigurationItemData GetConfigurationItemData(ItemData itemData)
        {
            return this.configurationItemDatas[itemData.Index];
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
            this.Save();
        }

        public void ResetPosition()
        {
            dto.MainPanelPositionX = 0f;
            dto.MainPanelPositionY = 0f;
            this.Save();
        }

        private void UpdateDtoItems()
        {
            foreach (var configurationItem in this.configurationItemDatas)
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
            foreach (var configurationItemData in this.configurationItemDatas)
            {
                ref var configItemRef = ref configurationItemDatas[ItemData.AverageIllnessRate.Index];
                
                if (configurationItemData.ItemData == ItemData.AverageIllnessRate)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.AverageIllnessRate,
                        dto.AverageIllnessRateCriticalThreshold,
                        dto.AverageIllnessRateSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Cemetery)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Cemetery,
                        dto.CemeteryCriticalThreshold,
                        dto.CemeterySortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.CemeteryVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.CemeteryVehicles,
                        dto.CemeteryVehiclesCriticalThreshold,
                        dto.CemeteryVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.CityUnattractiveness)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.CityUnattractiveness,
                        dto.CityUnattractivenessCriticalThreshold,
                        dto.CityUnattractivenessSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Crematorium)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Crematorium,
                        dto.CrematoriumCriticalThreshold,
                        dto.CrematoriumSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.CrematoriumVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.CrematoriumVehicles,
                        dto.CrematoriumVehiclesCriticalThreshold,
                        dto.CrematoriumVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.CrimeRate)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.CrimeRate,
                        dto.CrimeRateCriticalThreshold,
                        dto.CrimeRateSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.DisasterResponseHelicopters)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.DisasterResponseHelicopters,
                        dto.DisasterResponseHelicoptersCriticalThreshold,
                        dto.DisasterResponseHelicoptersSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.DisasterResponseVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.DisasterResponseVehicles,
                        dto.DisasterResponseVehiclesCriticalThreshold,
                        dto.DisasterResponseVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.DrinkingWaterPollution)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.DrinkingWaterPollution,
                        dto.DrinkingWaterPollutionCriticalThreshold,
                        dto.DrinkingWaterPollutionSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Electricity)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Electricity,
                        dto.ElectricityCriticalThreshold,
                        dto.ElectricitySortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.ElementarySchool)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.ElementarySchool,
                        dto.ElementarySchoolCriticalThreshold,
                        dto.ElementarySchoolSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.FireDepartmentVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.FireDepartmentVehicles,
                        dto.FireDepartmentVehiclesCriticalThreshold,
                        dto.FireDepartmentVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.FireHazard)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.FireHazard,
                        dto.FireHazardCriticalThreshold,
                        dto.FireHazardSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.FireHelicopters)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.FireHelicopters,
                        dto.FireHelicoptersCriticalThreshold,
                        dto.FireHelicoptersSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.GarbageProcessing)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.GarbageProcessing,
                        dto.GarbageProcessingCriticalThreshold,
                        dto.GarbageProcessingSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.GarbageProcessingVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.GarbageProcessingVehicles,
                        dto.GarbageProcessingVehiclesCriticalThreshold,
                        dto.GarbageProcessingVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.GroundPollution)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.GroundPollution,
                        dto.GroundPollutionCriticalThreshold,
                        dto.GroundPollutionSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Healthcare)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Healthcare,
                        dto.HealthcareCriticalThreshold,
                        dto.HealthcareSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.HealthcareVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.HealthcareVehicles,
                        dto.HealthcareVehiclesCriticalThreshold,
                        dto.HealthcareVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Heating)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Heating,
                        dto.HeatingCriticalThreshold,
                        dto.HeatingSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.HighSchool)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.HighSchool,
                        dto.HighSchoolCriticalThreshold,
                        dto.HighSchoolSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Landfill)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Landfill,
                        dto.LandfillCriticalThreshold,
                        dto.LandfillSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.LandfillVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.LandfillVehicles,
                        dto.LandfillVehiclesCriticalThreshold,
                        dto.LandfillVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.MedicalHelicopters)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.MedicalHelicopters,
                        dto.MedicalHelicoptersCriticalThreshold,
                        dto.MedicalHelicoptersSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.NoisePollution)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.NoisePollution,
                        dto.NoisePollutionCriticalThreshold,
                        dto.NoisePollutionSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.ParkMaintenanceVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.ParkMaintenanceVehicles,
                        dto.ParkMaintenanceVehiclesCriticalThreshold,
                        dto.ParkMaintenanceVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.PoliceHelicopters)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.PoliceHelicopters,
                        dto.PoliceHelicoptersCriticalThreshold,
                        dto.PoliceHelicoptersSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.PoliceHoldingCells)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.PoliceHoldingCells,
                        dto.PoliceHoldingCellsCriticalThreshold,
                        dto.PoliceHoldingCellsSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.PoliceVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.PoliceVehicles,
                        dto.PoliceVehiclesCriticalThreshold,
                        dto.PoliceVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.PostTrucks)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.PostTrucks,
                        dto.PostTrucksCriticalThreshold,
                        dto.PostTrucksSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.PostVans)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.PostVans,
                        dto.PostVansCriticalThreshold,
                        dto.PostVansSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.PrisonCells)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.PrisonCells,
                        dto.PrisonCellsCriticalThreshold,
                        dto.PrisonCellsSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.PrisonVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.PrisonVehicles,
                        dto.PrisonVehiclesCriticalThreshold,
                        dto.PrisonVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.RoadMaintenanceVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.RoadMaintenanceVehicles,
                        dto.RoadMaintenanceVehiclesCriticalThreshold,
                        dto.RoadMaintenanceVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.SewageTreatment)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.SewageTreatment,
                        dto.SewageTreatmentCriticalThreshold,
                        dto.SewageTreatmentSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.SnowDump)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.SnowDump,
                        dto.SnowDumpCriticalThreshold,
                        dto.SnowDumpSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.SnowDumpVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.SnowDumpVehicles,
                        dto.SnowDumpVehiclesCriticalThreshold,
                        dto.SnowDumpVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Taxis)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Taxis,
                        dto.TaxisCriticalThreshold,
                        dto.TaxisSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.TrafficJam)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.TrafficJam,
                        dto.TrafficJamCriticalThreshold,
                        dto.TrafficJamSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Unemployment)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Unemployment,
                        dto.UnemploymentCriticalThreshold,
                        dto.UnemploymentSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.University)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.University,
                        dto.UniversityCriticalThreshold,
                        dto.UniversitySortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.Water)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.Water,
                        dto.WaterCriticalThreshold,
                        dto.WaterSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.WaterPumpingServiceStorage)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.WaterPumpingServiceStorage,
                        dto.WaterPumpingServiceStorageCriticalThreshold,
                        dto.WaterPumpingServiceStorageSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.WaterPumpingServiceVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.WaterPumpingServiceVehicles,
                        dto.WaterPumpingServiceVehiclesCriticalThreshold,
                        dto.WaterPumpingServiceVehiclesSortOrder);
                }
                else if (configurationItemData.ItemData == ItemData.WaterReserveTank)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.ItemData,
                        dto.WaterReserveTank,
                        dto.WaterReserveTankCriticalThreshold,
                        dto.WaterReserveTankSortOrder);
                }
                else
                {
                    throw new Exception("Unknown configItem.Item type");
                }
            }
        }
    }
}
