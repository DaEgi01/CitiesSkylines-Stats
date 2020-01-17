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

        private void ValidateIndexes(ConfigurationItemData[] configurationItems)
        {
            for (int i = 0; i < configurationItems.Length; i++)
            {
                if (i != configurationItems[i].itemData.Index)
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
                if (configurationItem.itemData == ItemData.AverageIllnessRate)
                {
                    dto.AverageIllnessRate = configurationItem.enabled;
                    dto.AverageIllnessRateCriticalThreshold = configurationItem.criticalThreshold;
                    dto.AverageIllnessRateSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Cemetery)
                {
                    dto.Cemetery = configurationItem.enabled;
                    dto.CemeteryCriticalThreshold = configurationItem.criticalThreshold;
                    dto.CemeterySortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.CemeteryVehicles)
                {
                    dto.CemeteryVehicles = configurationItem.enabled;
                    dto.CemeteryVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.CemeteryVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.CityUnattractiveness)
                {
                    dto.CityUnattractiveness = configurationItem.enabled;
                    dto.CityUnattractivenessCriticalThreshold = configurationItem.criticalThreshold;
                    dto.CityUnattractivenessSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Crematorium)
                {
                    dto.Crematorium = configurationItem.enabled;
                    dto.CrematoriumCriticalThreshold = configurationItem.criticalThreshold;
                    dto.CrematoriumSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.CrematoriumVehicles)
                {
                    dto.CrematoriumVehicles = configurationItem.enabled;
                    dto.CrematoriumVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.CrematoriumVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.CrimeRate)
                {
                    dto.CrimeRate = configurationItem.enabled;
                    dto.CrimeRateCriticalThreshold = configurationItem.criticalThreshold;
                    dto.CrimeRateSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.DisasterResponseHelicopters)
                {
                    dto.DisasterResponseHelicopters = configurationItem.enabled;
                    dto.DisasterResponseHelicoptersCriticalThreshold = configurationItem.criticalThreshold;
                    dto.DisasterResponseHelicoptersSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.DisasterResponseVehicles)
                {
                    dto.DisasterResponseVehicles = configurationItem.enabled;
                    dto.DisasterResponseVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.DisasterResponseVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.DrinkingWaterPollution)
                {
                    dto.DrinkingWaterPollution = configurationItem.enabled;
                    dto.DrinkingWaterPollutionCriticalThreshold = configurationItem.criticalThreshold;
                    dto.DrinkingWaterPollutionSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Electricity)
                {
                    dto.Electricity = configurationItem.enabled;
                    dto.ElectricityCriticalThreshold = configurationItem.criticalThreshold;
                    dto.ElectricitySortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.ElementarySchool)
                {
                    dto.ElementarySchool = configurationItem.enabled;
                    dto.ElementarySchoolCriticalThreshold = configurationItem.criticalThreshold;
                    dto.ElementarySchoolSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.FireDepartmentVehicles)
                {
                    dto.FireDepartmentVehicles = configurationItem.enabled;
                    dto.FireDepartmentVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.FireDepartmentVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.FireHazard)
                {
                    dto.FireHazard = configurationItem.enabled;
                    dto.FireHazardCriticalThreshold = configurationItem.criticalThreshold;
                    dto.FireHazardSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.FireHelicopters)
                {
                    dto.FireHelicopters = configurationItem.enabled;
                    dto.FireHelicoptersCriticalThreshold = configurationItem.criticalThreshold;
                    dto.FireHelicoptersSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.GarbageProcessing)
                {
                    dto.GarbageProcessing = configurationItem.enabled;
                    dto.GarbageProcessingCriticalThreshold = configurationItem.criticalThreshold;
                    dto.GarbageProcessingSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.GarbageProcessingVehicles)
                {
                    dto.GarbageProcessingVehicles = configurationItem.enabled;
                    dto.GarbageProcessingVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.GarbageProcessingVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.GroundPollution)
                {
                    dto.GroundPollution = configurationItem.enabled;
                    dto.GroundPollutionCriticalThreshold = configurationItem.criticalThreshold;
                    dto.GroundPollutionSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Healthcare)
                {
                    dto.Healthcare = configurationItem.enabled;
                    dto.HealthcareCriticalThreshold = configurationItem.criticalThreshold;
                    dto.HealthcareSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.HealthcareVehicles)
                {
                    dto.HealthcareVehicles = configurationItem.enabled;
                    dto.HealthcareVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.HealthcareVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Heating)
                {
                    dto.Heating = configurationItem.enabled;
                    dto.HeatingCriticalThreshold = configurationItem.criticalThreshold;
                    dto.HeatingSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.HighSchool)
                {
                    dto.HighSchool = configurationItem.enabled;
                    dto.HighSchoolCriticalThreshold = configurationItem.criticalThreshold;
                    dto.HighSchoolSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Landfill)
                {
                    dto.Landfill = configurationItem.enabled;
                    dto.LandfillCriticalThreshold = configurationItem.criticalThreshold;
                    dto.LandfillSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.LandfillVehicles)
                {
                    dto.LandfillVehicles = configurationItem.enabled;
                    dto.LandfillVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.LandfillVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.MedicalHelicopters)
                {
                    dto.MedicalHelicopters = configurationItem.enabled;
                    dto.MedicalHelicoptersCriticalThreshold = configurationItem.criticalThreshold;
                    dto.MedicalHelicoptersSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.NoisePollution)
                {
                    dto.NoisePollution = configurationItem.enabled;
                    dto.NoisePollutionCriticalThreshold = configurationItem.criticalThreshold;
                    dto.NoisePollutionSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.ParkMaintenanceVehicles)
                {
                    dto.ParkMaintenanceVehicles = configurationItem.enabled;
                    dto.ParkMaintenanceVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.ParkMaintenanceVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.PoliceHelicopters)
                {
                    dto.PoliceHelicopters = configurationItem.enabled;
                    dto.PoliceHelicoptersCriticalThreshold = configurationItem.criticalThreshold;
                    dto.PoliceHelicoptersSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.PoliceHoldingCells)
                {
                    dto.PoliceHoldingCells = configurationItem.enabled;
                    dto.PoliceHoldingCellsCriticalThreshold = configurationItem.criticalThreshold;
                    dto.PoliceHoldingCellsSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.PoliceVehicles)
                {
                    dto.PoliceVehicles = configurationItem.enabled;
                    dto.PoliceVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.PoliceVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.PostTrucks)
                {
                    dto.PostTrucks = configurationItem.enabled;
                    dto.PostTrucksCriticalThreshold = configurationItem.criticalThreshold;
                    dto.PostTrucksSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.PostVans)
                {
                    dto.PostVans = configurationItem.enabled;
                    dto.PostVansCriticalThreshold = configurationItem.criticalThreshold;
                    dto.PostVansSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.PrisonCells)
                {
                    dto.PrisonCells = configurationItem.enabled;
                    dto.PrisonCellsCriticalThreshold = configurationItem.criticalThreshold;
                    dto.PrisonCellsSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.PrisonVehicles)
                {
                    dto.PrisonVehicles = configurationItem.enabled;
                    dto.PrisonVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.PrisonVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.RoadMaintenanceVehicles)
                {
                    dto.RoadMaintenanceVehicles = configurationItem.enabled;
                    dto.RoadMaintenanceVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.RoadMaintenanceVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.SewageTreatment)
                {
                    dto.SewageTreatment = configurationItem.enabled;
                    dto.SewageTreatmentCriticalThreshold = configurationItem.criticalThreshold;
                    dto.SewageTreatmentSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.SnowDump)
                {
                    dto.SnowDump = configurationItem.enabled;
                    dto.SnowDumpCriticalThreshold = configurationItem.criticalThreshold;
                    dto.SnowDumpSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.SnowDumpVehicles)
                {
                    dto.SnowDumpVehicles = configurationItem.enabled;
                    dto.SnowDumpVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.SnowDumpVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Taxis)
                {
                    dto.Taxis = configurationItem.enabled;
                    dto.TaxisCriticalThreshold = configurationItem.criticalThreshold;
                    dto.TaxisSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.TrafficJam)
                {
                    dto.TrafficJam = configurationItem.enabled;
                    dto.TrafficJamCriticalThreshold = configurationItem.criticalThreshold;
                    dto.TrafficJamSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Unemployment)
                {
                    dto.Unemployment = configurationItem.enabled;
                    dto.UnemploymentCriticalThreshold = configurationItem.criticalThreshold;
                    dto.UnemploymentSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.University)
                {
                    dto.University = configurationItem.enabled;
                    dto.UniversityCriticalThreshold = configurationItem.criticalThreshold;
                    dto.UniversitySortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.Water)
                {
                    dto.Water = configurationItem.enabled;
                    dto.WaterCriticalThreshold = configurationItem.criticalThreshold;
                    dto.WaterSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.WaterPumpingServiceStorage)
                {
                    dto.WaterPumpingServiceStorage = configurationItem.enabled;
                    dto.WaterPumpingServiceStorageCriticalThreshold = configurationItem.criticalThreshold;
                    dto.WaterPumpingServiceStorageSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.WaterPumpingServiceVehicles)
                {
                    dto.WaterPumpingServiceVehicles = configurationItem.enabled;
                    dto.WaterPumpingServiceVehiclesCriticalThreshold = configurationItem.criticalThreshold;
                    dto.WaterPumpingServiceVehiclesSortOrder = configurationItem.sortOrder;
                }
                else if (configurationItem.itemData == ItemData.WaterReserveTank)
                {
                    dto.WaterReserveTank = configurationItem.enabled;
                    dto.WaterReserveTankCriticalThreshold = configurationItem.criticalThreshold;
                    dto.WaterReserveTankSortOrder = configurationItem.sortOrder;
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
                
                if (configurationItemData.itemData == ItemData.AverageIllnessRate)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.AverageIllnessRate,
                        dto.AverageIllnessRateCriticalThreshold,
                        dto.AverageIllnessRateSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Cemetery)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Cemetery,
                        dto.CemeteryCriticalThreshold,
                        dto.CemeterySortOrder);
                }
                else if (configurationItemData.itemData == ItemData.CemeteryVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.CemeteryVehicles,
                        dto.CemeteryVehiclesCriticalThreshold,
                        dto.CemeteryVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.CityUnattractiveness)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.CityUnattractiveness,
                        dto.CityUnattractivenessCriticalThreshold,
                        dto.CityUnattractivenessSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Crematorium)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Crematorium,
                        dto.CrematoriumCriticalThreshold,
                        dto.CrematoriumSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.CrematoriumVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.CrematoriumVehicles,
                        dto.CrematoriumVehiclesCriticalThreshold,
                        dto.CrematoriumVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.CrimeRate)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.CrimeRate,
                        dto.CrimeRateCriticalThreshold,
                        dto.CrimeRateSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.DisasterResponseHelicopters)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.DisasterResponseHelicopters,
                        dto.DisasterResponseHelicoptersCriticalThreshold,
                        dto.DisasterResponseHelicoptersSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.DisasterResponseVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.DisasterResponseVehicles,
                        dto.DisasterResponseVehiclesCriticalThreshold,
                        dto.DisasterResponseVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.DrinkingWaterPollution)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.DrinkingWaterPollution,
                        dto.DrinkingWaterPollutionCriticalThreshold,
                        dto.DrinkingWaterPollutionSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Electricity)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Electricity,
                        dto.ElectricityCriticalThreshold,
                        dto.ElectricitySortOrder);
                }
                else if (configurationItemData.itemData == ItemData.ElementarySchool)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.ElementarySchool,
                        dto.ElementarySchoolCriticalThreshold,
                        dto.ElementarySchoolSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.FireDepartmentVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.FireDepartmentVehicles,
                        dto.FireDepartmentVehiclesCriticalThreshold,
                        dto.FireDepartmentVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.FireHazard)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.FireHazard,
                        dto.FireHazardCriticalThreshold,
                        dto.FireHazardSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.FireHelicopters)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.FireHelicopters,
                        dto.FireHelicoptersCriticalThreshold,
                        dto.FireHelicoptersSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.GarbageProcessing)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.GarbageProcessing,
                        dto.GarbageProcessingCriticalThreshold,
                        dto.GarbageProcessingSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.GarbageProcessingVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.GarbageProcessingVehicles,
                        dto.GarbageProcessingVehiclesCriticalThreshold,
                        dto.GarbageProcessingVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.GroundPollution)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.GroundPollution,
                        dto.GroundPollutionCriticalThreshold,
                        dto.GroundPollutionSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Healthcare)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Healthcare,
                        dto.HealthcareCriticalThreshold,
                        dto.HealthcareSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.HealthcareVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.HealthcareVehicles,
                        dto.HealthcareVehiclesCriticalThreshold,
                        dto.HealthcareVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Heating)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Heating,
                        dto.HeatingCriticalThreshold,
                        dto.HeatingSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.HighSchool)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.HighSchool,
                        dto.HighSchoolCriticalThreshold,
                        dto.HighSchoolSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Landfill)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Landfill,
                        dto.LandfillCriticalThreshold,
                        dto.LandfillSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.LandfillVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.LandfillVehicles,
                        dto.LandfillVehiclesCriticalThreshold,
                        dto.LandfillVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.MedicalHelicopters)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.MedicalHelicopters,
                        dto.MedicalHelicoptersCriticalThreshold,
                        dto.MedicalHelicoptersSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.NoisePollution)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.NoisePollution,
                        dto.NoisePollutionCriticalThreshold,
                        dto.NoisePollutionSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.ParkMaintenanceVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.ParkMaintenanceVehicles,
                        dto.ParkMaintenanceVehiclesCriticalThreshold,
                        dto.ParkMaintenanceVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.PoliceHelicopters)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.PoliceHelicopters,
                        dto.PoliceHelicoptersCriticalThreshold,
                        dto.PoliceHelicoptersSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.PoliceHoldingCells)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.PoliceHoldingCells,
                        dto.PoliceHoldingCellsCriticalThreshold,
                        dto.PoliceHoldingCellsSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.PoliceVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.PoliceVehicles,
                        dto.PoliceVehiclesCriticalThreshold,
                        dto.PoliceVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.PostTrucks)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.PostTrucks,
                        dto.PostTrucksCriticalThreshold,
                        dto.PostTrucksSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.PostVans)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.PostVans,
                        dto.PostVansCriticalThreshold,
                        dto.PostVansSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.PrisonCells)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.PrisonCells,
                        dto.PrisonCellsCriticalThreshold,
                        dto.PrisonCellsSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.PrisonVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.PrisonVehicles,
                        dto.PrisonVehiclesCriticalThreshold,
                        dto.PrisonVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.RoadMaintenanceVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.RoadMaintenanceVehicles,
                        dto.RoadMaintenanceVehiclesCriticalThreshold,
                        dto.RoadMaintenanceVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.SewageTreatment)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.SewageTreatment,
                        dto.SewageTreatmentCriticalThreshold,
                        dto.SewageTreatmentSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.SnowDump)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.SnowDump,
                        dto.SnowDumpCriticalThreshold,
                        dto.SnowDumpSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.SnowDumpVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.SnowDumpVehicles,
                        dto.SnowDumpVehiclesCriticalThreshold,
                        dto.SnowDumpVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Taxis)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Taxis,
                        dto.TaxisCriticalThreshold,
                        dto.TaxisSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.TrafficJam)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.TrafficJam,
                        dto.TrafficJamCriticalThreshold,
                        dto.TrafficJamSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Unemployment)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Unemployment,
                        dto.UnemploymentCriticalThreshold,
                        dto.UnemploymentSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.University)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.University,
                        dto.UniversityCriticalThreshold,
                        dto.UniversitySortOrder);
                }
                else if (configurationItemData.itemData == ItemData.Water)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.Water,
                        dto.WaterCriticalThreshold,
                        dto.WaterSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.WaterPumpingServiceStorage)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.WaterPumpingServiceStorage,
                        dto.WaterPumpingServiceStorageCriticalThreshold,
                        dto.WaterPumpingServiceStorageSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.WaterPumpingServiceVehicles)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
                        dto.WaterPumpingServiceVehicles,
                        dto.WaterPumpingServiceVehiclesCriticalThreshold,
                        dto.WaterPumpingServiceVehiclesSortOrder);
                }
                else if (configurationItemData.itemData == ItemData.WaterReserveTank)
                {
                    configItemRef = new ConfigurationItemData(
                        configItemRef.itemData,
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
