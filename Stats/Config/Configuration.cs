using System;
using UnityEngine;

namespace Stats.Config
{
    public class Configuration
    {
        private readonly ConfigurationService<ConfigurationDto> _configurationService;
        private readonly ConfigurationItemData[] _configurationItemDatas;

        private ConfigurationDto _dto;

        public Configuration(ConfigurationService<ConfigurationDto> configurationService, ConfigurationDto dto)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _dto = dto ?? throw new ArgumentNullException(nameof(dto));
            _configurationItemDatas = new[] {
                new ConfigurationItemData(ItemData.AverageIllnessRate, dto.AverageIllnessRate, dto.AverageIllnessRateCriticalThreshold, dto.AverageIllnessRateSortOrder),
                new ConfigurationItemData(ItemData.AverageChildrenIllnessRate, dto.AverageChildrenIllnessRate, dto.AverageChildrenIllnessRateCriticalThreshold, dto.AverageChildrenIllnessRateSortOrder),
                new ConfigurationItemData(ItemData.AverageElderlyIllnessRate, dto.AverageElderlyIllnessRate, dto.AverageElderlyIllnessRateCriticalThreshold, dto.AverageElderlyIllnessRateSortOrder),
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
                new ConfigurationItemData(ItemData.Library, dto.Library, dto.LibraryCriticalThreshold, dto.LibrarySortOrder),
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
                new ConfigurationItemData(ItemData.UnhappinessCommercial, dto.UnhappinessCommercial, dto.UnhappinessCommercialCriticalThreshold, dto.UnhappinessCommercialSortOrder),
                new ConfigurationItemData(ItemData.UnhappinessIndustrial, dto.UnhappinessIndustrial, dto.UnhappinessIndustrialCriticalThreshold, dto.UnhappinessIndustrialSortOrder),
                new ConfigurationItemData(ItemData.UnhappinessOffice, dto.UnhappinessOffice, dto.UnhappinessOfficeCriticalThreshold, dto.UnhappinessOfficeSortOrder),
                new ConfigurationItemData(ItemData.UnhappinessResidential, dto.UnhappinessResidential, dto.UnhappinessResidentialCriticalThreshold, dto.UnhappinessResidentialSortOrder),
                new ConfigurationItemData(ItemData.University, dto.University, dto.UniversityCriticalThreshold, dto.UniversitySortOrder),
                new ConfigurationItemData(ItemData.Water, dto.Water, dto.WaterCriticalThreshold, dto.WaterSortOrder),
                new ConfigurationItemData(ItemData.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorageCriticalThreshold, dto.WaterPumpingServiceStorageSortOrder),
                new ConfigurationItemData(ItemData.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehiclesCriticalThreshold, dto.WaterPumpingServiceVehiclesSortOrder),
                new ConfigurationItemData(ItemData.WaterReserveTank, dto.WaterReserveTank, dto.WaterReserveTankCriticalThreshold, dto.WaterReserveTankSortOrder)
            };
            ValidateIndexes(_configurationItemDatas);
        }

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
            get => new Vector2(_dto.MainPanelPositionX, _dto.MainPanelPositionY);
            set
            {
                _dto.MainPanelPositionX = value.x;
                _dto.MainPanelPositionY = value.y;
            }
        }

        public int MainPanelUpdateEveryXSeconds
        {
            get => _dto.MainPanelUpdateEveryXSeconds;
            set
            {
                _dto.MainPanelUpdateEveryXSeconds = value;
            }
        }

        public bool MainPanelAutoHide
        {
            get => _dto.MainPanelAutoHide;
            set
            {
                _dto.MainPanelAutoHide = value;
            }
        }

        public bool MainPanelHideItemsBelowThreshold
        {
            get => _dto.MainPanelHideItemsBelowThreshold;
            set
            {
                _dto.MainPanelHideItemsBelowThreshold = value;
            }
        }

        public bool MainPanelHideItemsNotAvailable
        {
            get => _dto.MainPanelHideItemsNotAvailable;
            set
            {
                _dto.MainPanelHideItemsNotAvailable = value;
            }
        }

        public int MainPanelColumnCount
        {
            get => _dto.MainPanelColumnCount;
            set
            {
                _dto.MainPanelColumnCount = value;
            }
        }

        public Color32 MainPanelBackgroundColor
        {
            get => _dto.MainPanelBackgroundColor.GetColor32();
            set
            {
                _dto.MainPanelBackgroundColor = value.GetColorString();
            }
        }

        public Color32 MainPanelForegroundColor
        {
            get => _dto.MainPanelForegroundColor.GetColor32();
            set
            {
                _dto.MainPanelForegroundColor = value.GetColorString();
            }
        }

        public Color32 MainPanelAccentColor
        {
            get => _dto.MainPanelAccentColor.GetColor32();
            set
            {
                _dto.MainPanelAccentColor = value.GetColorString();
            }
        }

        public float ItemWidth
        {
            get => _dto.ItemWidth;
            set
            {
                _dto.ItemWidth = value;
            }
        }

        public float ItemHeight
        {
            get => _dto.ItemHeight;
            set
            {
                _dto.ItemHeight = value;
            }
        }

        public float ItemPadding
        {
            get => _dto.ItemPadding;
            set
            {
                _dto.ItemPadding = value;
            }
        }

        public float ItemTextScale
        {
            get => _dto.ItemTextScale;
            set
            {
                _dto.ItemTextScale = value;
            }
        }

        public ConfigurationItemData GetConfigurationItemData(ItemData itemData)
        {
            return _configurationItemDatas[itemData.Index];
        }

        public int GetEnabledItemsCount()
        {
            var result = 0;
            for (int i = 0; i < _configurationItemDatas.Length; i++)
            {
                if (_configurationItemDatas[i].Enabled)
                {
                    result += 1;
                }
            }
            return result;
        }

        public void Save()
        {
            UpdateDtoItems();
            _configurationService.Save(_dto);
        }

        public void Reset()
        {
            _dto = new ConfigurationDto();
            UpdateConfigurationItems();
            Save();
        }

        public void ResetPosition()
        {
            _dto.MainPanelPositionX = 0f;
            _dto.MainPanelPositionY = 0f;
            Save();
        }

        private void UpdateDtoItems()
        {
            foreach (var configurationItem in _configurationItemDatas)
            {
                if (configurationItem.ItemData == ItemData.AverageIllnessRate)
                {
                    _dto.AverageIllnessRate = configurationItem.Enabled;
                    _dto.AverageIllnessRateCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.AverageIllnessRateSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.AverageChildrenIllnessRate)
                {
                    _dto.AverageChildrenIllnessRate = configurationItem.Enabled;
                    _dto.AverageChildrenIllnessRateCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.AverageChildrenIllnessRateSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.AverageElderlyIllnessRate)
                {
                    _dto.AverageElderlyIllnessRate = configurationItem.Enabled;
                    _dto.AverageElderlyIllnessRateCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.AverageElderlyIllnessRateSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Cemetery)
                {
                    _dto.Cemetery = configurationItem.Enabled;
                    _dto.CemeteryCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.CemeterySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CemeteryVehicles)
                {
                    _dto.CemeteryVehicles = configurationItem.Enabled;
                    _dto.CemeteryVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.CemeteryVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CityUnattractiveness)
                {
                    _dto.CityUnattractiveness = configurationItem.Enabled;
                    _dto.CityUnattractivenessCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.CityUnattractivenessSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Crematorium)
                {
                    _dto.Crematorium = configurationItem.Enabled;
                    _dto.CrematoriumCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.CrematoriumSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CrematoriumVehicles)
                {
                    _dto.CrematoriumVehicles = configurationItem.Enabled;
                    _dto.CrematoriumVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.CrematoriumVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.CrimeRate)
                {
                    _dto.CrimeRate = configurationItem.Enabled;
                    _dto.CrimeRateCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.CrimeRateSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DisasterResponseHelicopters)
                {
                    _dto.DisasterResponseHelicopters = configurationItem.Enabled;
                    _dto.DisasterResponseHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.DisasterResponseHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DisasterResponseVehicles)
                {
                    _dto.DisasterResponseVehicles = configurationItem.Enabled;
                    _dto.DisasterResponseVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.DisasterResponseVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.DrinkingWaterPollution)
                {
                    _dto.DrinkingWaterPollution = configurationItem.Enabled;
                    _dto.DrinkingWaterPollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.DrinkingWaterPollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Electricity)
                {
                    _dto.Electricity = configurationItem.Enabled;
                    _dto.ElectricityCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.ElectricitySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.ElementarySchool)
                {
                    _dto.ElementarySchool = configurationItem.Enabled;
                    _dto.ElementarySchoolCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.ElementarySchoolSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireDepartmentVehicles)
                {
                    _dto.FireDepartmentVehicles = configurationItem.Enabled;
                    _dto.FireDepartmentVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.FireDepartmentVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireHazard)
                {
                    _dto.FireHazard = configurationItem.Enabled;
                    _dto.FireHazardCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.FireHazardSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.FireHelicopters)
                {
                    _dto.FireHelicopters = configurationItem.Enabled;
                    _dto.FireHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.FireHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GarbageProcessing)
                {
                    _dto.GarbageProcessing = configurationItem.Enabled;
                    _dto.GarbageProcessingCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.GarbageProcessingSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GarbageProcessingVehicles)
                {
                    _dto.GarbageProcessingVehicles = configurationItem.Enabled;
                    _dto.GarbageProcessingVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.GarbageProcessingVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.GroundPollution)
                {
                    _dto.GroundPollution = configurationItem.Enabled;
                    _dto.GroundPollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.GroundPollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Healthcare)
                {
                    _dto.Healthcare = configurationItem.Enabled;
                    _dto.HealthcareCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.HealthcareSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.HealthcareVehicles)
                {
                    _dto.HealthcareVehicles = configurationItem.Enabled;
                    _dto.HealthcareVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.HealthcareVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Heating)
                {
                    _dto.Heating = configurationItem.Enabled;
                    _dto.HeatingCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.HeatingSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.HighSchool)
                {
                    _dto.HighSchool = configurationItem.Enabled;
                    _dto.HighSchoolCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.HighSchoolSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Landfill)
                {
                    _dto.Landfill = configurationItem.Enabled;
                    _dto.LandfillCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.LandfillSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.LandfillVehicles)
                {
                    _dto.LandfillVehicles = configurationItem.Enabled;
                    _dto.LandfillVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.LandfillVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Library)
                {
                    _dto.Library = configurationItem.Enabled;
                    _dto.LibraryCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.LibrarySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.MedicalHelicopters)
                {
                    _dto.MedicalHelicopters = configurationItem.Enabled;
                    _dto.MedicalHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.MedicalHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.NoisePollution)
                {
                    _dto.NoisePollution = configurationItem.Enabled;
                    _dto.NoisePollutionCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.NoisePollutionSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.ParkMaintenanceVehicles)
                {
                    _dto.ParkMaintenanceVehicles = configurationItem.Enabled;
                    _dto.ParkMaintenanceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.ParkMaintenanceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceHelicopters)
                {
                    _dto.PoliceHelicopters = configurationItem.Enabled;
                    _dto.PoliceHelicoptersCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.PoliceHelicoptersSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceHoldingCells)
                {
                    _dto.PoliceHoldingCells = configurationItem.Enabled;
                    _dto.PoliceHoldingCellsCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.PoliceHoldingCellsSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PoliceVehicles)
                {
                    _dto.PoliceVehicles = configurationItem.Enabled;
                    _dto.PoliceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.PoliceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PostTrucks)
                {
                    _dto.PostTrucks = configurationItem.Enabled;
                    _dto.PostTrucksCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.PostTrucksSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PostVans)
                {
                    _dto.PostVans = configurationItem.Enabled;
                    _dto.PostVansCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.PostVansSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PrisonCells)
                {
                    _dto.PrisonCells = configurationItem.Enabled;
                    _dto.PrisonCellsCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.PrisonCellsSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.PrisonVehicles)
                {
                    _dto.PrisonVehicles = configurationItem.Enabled;
                    _dto.PrisonVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.PrisonVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.RoadMaintenanceVehicles)
                {
                    _dto.RoadMaintenanceVehicles = configurationItem.Enabled;
                    _dto.RoadMaintenanceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.RoadMaintenanceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SewageTreatment)
                {
                    _dto.SewageTreatment = configurationItem.Enabled;
                    _dto.SewageTreatmentCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.SewageTreatmentSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SnowDump)
                {
                    _dto.SnowDump = configurationItem.Enabled;
                    _dto.SnowDumpCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.SnowDumpSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.SnowDumpVehicles)
                {
                    _dto.SnowDumpVehicles = configurationItem.Enabled;
                    _dto.SnowDumpVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.SnowDumpVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Taxis)
                {
                    _dto.Taxis = configurationItem.Enabled;
                    _dto.TaxisCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.TaxisSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.TrafficJam)
                {
                    _dto.TrafficJam = configurationItem.Enabled;
                    _dto.TrafficJamCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.TrafficJamSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Unemployment)
                {
                    _dto.Unemployment = configurationItem.Enabled;
                    _dto.UnemploymentCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.UnemploymentSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.UnhappinessCommercial)
                {
                    _dto.UnhappinessCommercial = configurationItem.Enabled;
                    _dto.UnhappinessCommercialCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.UnhappinessCommercialSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.UnhappinessIndustrial)
                {
                    _dto.UnhappinessIndustrial = configurationItem.Enabled;
                    _dto.UnhappinessIndustrialCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.UnhappinessIndustrialSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.UnhappinessOffice)
                {
                    _dto.UnhappinessOffice = configurationItem.Enabled;
                    _dto.UnhappinessOfficeCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.UnhappinessOfficeSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.UnhappinessResidential)
                {
                    _dto.UnhappinessResidential = configurationItem.Enabled;
                    _dto.UnhappinessResidentialCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.UnhappinessResidentialSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.University)
                {
                    _dto.University = configurationItem.Enabled;
                    _dto.UniversityCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.UniversitySortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.Water)
                {
                    _dto.Water = configurationItem.Enabled;
                    _dto.WaterCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.WaterSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterPumpingServiceStorage)
                {
                    _dto.WaterPumpingServiceStorage = configurationItem.Enabled;
                    _dto.WaterPumpingServiceStorageCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.WaterPumpingServiceStorageSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterPumpingServiceVehicles)
                {
                    _dto.WaterPumpingServiceVehicles = configurationItem.Enabled;
                    _dto.WaterPumpingServiceVehiclesCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.WaterPumpingServiceVehiclesSortOrder = configurationItem.SortOrder;
                }
                else if (configurationItem.ItemData == ItemData.WaterReserveTank)
                {
                    _dto.WaterReserveTank = configurationItem.Enabled;
                    _dto.WaterReserveTankCriticalThreshold = configurationItem.CriticalThreshold;
                    _dto.WaterReserveTankSortOrder = configurationItem.SortOrder;
                }
                else
                {
                    throw new Exception("Unknown configItem.Item type");
                }
            }
        }

        private void UpdateConfigurationItems()
        {
            foreach (var configurationItemData in _configurationItemDatas)
            {
                if (configurationItemData.ItemData == ItemData.AverageIllnessRate)
                {
                    configurationItemData.Enabled = _dto.AverageIllnessRate;
                    configurationItemData.CriticalThreshold = _dto.AverageIllnessRateCriticalThreshold;
                    configurationItemData.SortOrder = _dto.AverageIllnessRateSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.AverageChildrenIllnessRate)
                {
                    configurationItemData.Enabled = _dto.AverageChildrenIllnessRate;
                    configurationItemData.CriticalThreshold = _dto.AverageChildrenIllnessRateCriticalThreshold;
                    configurationItemData.SortOrder = _dto.AverageChildrenIllnessRateSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.AverageElderlyIllnessRate)
                {
                    configurationItemData.Enabled = _dto.AverageElderlyIllnessRate;
                    configurationItemData.CriticalThreshold = _dto.AverageElderlyIllnessRateCriticalThreshold;
                    configurationItemData.SortOrder = _dto.AverageElderlyIllnessRateSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Cemetery)
                {
                    configurationItemData.Enabled = _dto.Cemetery;
                    configurationItemData.CriticalThreshold = _dto.CemeteryCriticalThreshold;
                    configurationItemData.SortOrder = _dto.CemeterySortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.CemeteryVehicles)
                {
                    configurationItemData.Enabled = _dto.CemeteryVehicles;
                    configurationItemData.CriticalThreshold = _dto.CemeteryVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.CemeteryVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.CityUnattractiveness)
                {
                    configurationItemData.Enabled = _dto.CityUnattractiveness;
                    configurationItemData.CriticalThreshold = _dto.CityUnattractivenessCriticalThreshold;
                    configurationItemData.SortOrder = _dto.CityUnattractivenessSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Crematorium)
                {
                    configurationItemData.Enabled = _dto.Crematorium;
                    configurationItemData.CriticalThreshold = _dto.CrematoriumCriticalThreshold;
                    configurationItemData.SortOrder = _dto.CrematoriumSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.CrematoriumVehicles)
                {
                    configurationItemData.Enabled = _dto.CrematoriumVehicles;
                    configurationItemData.CriticalThreshold = _dto.CrematoriumVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.CrematoriumVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.CrimeRate)
                {
                    configurationItemData.Enabled = _dto.CrimeRate;
                    configurationItemData.CriticalThreshold = _dto.CrimeRateCriticalThreshold;
                    configurationItemData.SortOrder = _dto.CrimeRateSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.DisasterResponseHelicopters)
                {
                    configurationItemData.Enabled = _dto.DisasterResponseHelicopters;
                    configurationItemData.CriticalThreshold = _dto.DisasterResponseHelicoptersCriticalThreshold;
                    configurationItemData.SortOrder = _dto.DisasterResponseHelicoptersSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.DisasterResponseVehicles)
                {
                    configurationItemData.Enabled = _dto.DisasterResponseVehicles;
                    configurationItemData.CriticalThreshold = _dto.DisasterResponseVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.DisasterResponseVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.DrinkingWaterPollution)
                {
                    configurationItemData.Enabled = _dto.DrinkingWaterPollution;
                    configurationItemData.CriticalThreshold = _dto.DrinkingWaterPollutionCriticalThreshold;
                    configurationItemData.SortOrder = _dto.DrinkingWaterPollutionSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Electricity)
                {
                    configurationItemData.Enabled = _dto.Electricity;
                    configurationItemData.CriticalThreshold = _dto.ElectricityCriticalThreshold;
                    configurationItemData.SortOrder = _dto.ElectricitySortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.ElementarySchool)
                {
                    configurationItemData.Enabled = _dto.ElementarySchool;
                    configurationItemData.CriticalThreshold = _dto.ElementarySchoolCriticalThreshold;
                    configurationItemData.SortOrder = _dto.ElementarySchoolSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.FireDepartmentVehicles)
                {
                    configurationItemData.Enabled = _dto.FireDepartmentVehicles;
                    configurationItemData.CriticalThreshold = _dto.FireDepartmentVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.FireDepartmentVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.FireHazard)
                {
                    configurationItemData.Enabled = _dto.FireHazard;
                    configurationItemData.CriticalThreshold = _dto.FireHazardCriticalThreshold;
                    configurationItemData.SortOrder = _dto.FireHazardSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.FireHelicopters)
                {
                    configurationItemData.Enabled = _dto.FireHelicopters;
                    configurationItemData.CriticalThreshold = _dto.FireHelicoptersCriticalThreshold;
                    configurationItemData.SortOrder = _dto.FireHelicoptersSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.GarbageProcessing)
                {
                    configurationItemData.Enabled = _dto.GarbageProcessing;
                    configurationItemData.CriticalThreshold = _dto.GarbageProcessingCriticalThreshold;
                    configurationItemData.SortOrder = _dto.GarbageProcessingSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.GarbageProcessingVehicles)
                {
                    configurationItemData.Enabled = _dto.GarbageProcessingVehicles;
                    configurationItemData.CriticalThreshold = _dto.GarbageProcessingVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.GarbageProcessingVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.GroundPollution)
                {
                    configurationItemData.Enabled = _dto.GroundPollution;
                    configurationItemData.CriticalThreshold = _dto.GroundPollutionCriticalThreshold;
                    configurationItemData.SortOrder = _dto.GroundPollutionSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Healthcare)
                {
                    configurationItemData.Enabled = _dto.Healthcare;
                    configurationItemData.CriticalThreshold = _dto.HealthcareCriticalThreshold;
                    configurationItemData.SortOrder = _dto.HealthcareSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.HealthcareVehicles)
                {
                    configurationItemData.Enabled = _dto.HealthcareVehicles;
                    configurationItemData.CriticalThreshold = _dto.HealthcareVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.HealthcareVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Heating)
                {
                    configurationItemData.Enabled = _dto.Heating;
                    configurationItemData.CriticalThreshold = _dto.HeatingCriticalThreshold;
                    configurationItemData.SortOrder = _dto.HeatingSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.HighSchool)
                {
                    configurationItemData.Enabled = _dto.HighSchool;
                    configurationItemData.CriticalThreshold = _dto.HighSchoolCriticalThreshold;
                    configurationItemData.SortOrder = _dto.HighSchoolSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Landfill)
                {
                    configurationItemData.Enabled = _dto.Landfill;
                    configurationItemData.CriticalThreshold = _dto.LandfillCriticalThreshold;
                    configurationItemData.SortOrder = _dto.LandfillSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.LandfillVehicles)
                {
                    configurationItemData.Enabled = _dto.LandfillVehicles;
                    configurationItemData.CriticalThreshold = _dto.LandfillVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.LandfillVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Library)
                {
                    configurationItemData.Enabled = _dto.Library;
                    configurationItemData.CriticalThreshold = _dto.LibraryCriticalThreshold;
                    configurationItemData.SortOrder = _dto.LibrarySortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.MedicalHelicopters)
                {
                    configurationItemData.Enabled = _dto.MedicalHelicopters;
                    configurationItemData.CriticalThreshold = _dto.MedicalHelicoptersCriticalThreshold;
                    configurationItemData.SortOrder = _dto.MedicalHelicoptersSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.NoisePollution)
                {
                    configurationItemData.Enabled = _dto.NoisePollution;
                    configurationItemData.CriticalThreshold = _dto.NoisePollutionCriticalThreshold;
                    configurationItemData.SortOrder = _dto.NoisePollutionSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.ParkMaintenanceVehicles)
                {
                    configurationItemData.Enabled = _dto.ParkMaintenanceVehicles;
                    configurationItemData.CriticalThreshold = _dto.ParkMaintenanceVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.ParkMaintenanceVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.PoliceHelicopters)
                {
                    configurationItemData.Enabled = _dto.PoliceHelicopters;
                    configurationItemData.CriticalThreshold = _dto.PoliceHelicoptersCriticalThreshold;
                    configurationItemData.SortOrder = _dto.PoliceHelicoptersSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.PoliceHoldingCells)
                {
                    configurationItemData.Enabled = _dto.PoliceHoldingCells;
                    configurationItemData.CriticalThreshold = _dto.PoliceHoldingCellsCriticalThreshold;
                    configurationItemData.SortOrder = _dto.PoliceHoldingCellsSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.PoliceVehicles)
                {
                    configurationItemData.Enabled = _dto.PoliceVehicles;
                    configurationItemData.CriticalThreshold = _dto.PoliceVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.PoliceVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.PostTrucks)
                {
                    configurationItemData.Enabled = _dto.PostTrucks;
                    configurationItemData.CriticalThreshold = _dto.PostTrucksCriticalThreshold;
                    configurationItemData.SortOrder = _dto.PostTrucksSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.PostVans)
                {
                    configurationItemData.Enabled = _dto.PostVans;
                    configurationItemData.CriticalThreshold = _dto.PostVansCriticalThreshold;
                    configurationItemData.SortOrder = _dto.PostVansSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.PrisonCells)
                {
                    configurationItemData.Enabled = _dto.PrisonCells;
                    configurationItemData.CriticalThreshold = _dto.PrisonCellsCriticalThreshold;
                    configurationItemData.SortOrder = _dto.PrisonCellsSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.PrisonVehicles)
                {
                    configurationItemData.Enabled = _dto.PrisonVehicles;
                    configurationItemData.CriticalThreshold = _dto.PrisonVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.PrisonVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.RoadMaintenanceVehicles)
                {
                    configurationItemData.Enabled = _dto.RoadMaintenanceVehicles;
                    configurationItemData.CriticalThreshold = _dto.RoadMaintenanceVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.RoadMaintenanceVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.SewageTreatment)
                {
                    configurationItemData.Enabled = _dto.SewageTreatment;
                    configurationItemData.CriticalThreshold = _dto.SewageTreatmentCriticalThreshold;
                    configurationItemData.SortOrder = _dto.SewageTreatmentSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.SnowDump)
                {
                    configurationItemData.Enabled = _dto.SnowDump;
                    configurationItemData.CriticalThreshold = _dto.SnowDumpCriticalThreshold;
                    configurationItemData.SortOrder = _dto.SnowDumpSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.SnowDumpVehicles)
                {
                    configurationItemData.Enabled = _dto.SnowDumpVehicles;
                    configurationItemData.CriticalThreshold = _dto.SnowDumpVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.SnowDumpVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Taxis)
                {
                    configurationItemData.Enabled = _dto.Taxis;
                    configurationItemData.CriticalThreshold = _dto.TaxisCriticalThreshold;
                    configurationItemData.SortOrder = _dto.TaxisSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.TrafficJam)
                {
                    configurationItemData.Enabled = _dto.TrafficJam;
                    configurationItemData.CriticalThreshold = _dto.TrafficJamCriticalThreshold;
                    configurationItemData.SortOrder = _dto.TrafficJamSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Unemployment)
                {
                    configurationItemData.Enabled = _dto.Unemployment;
                    configurationItemData.CriticalThreshold = _dto.UnemploymentCriticalThreshold;
                    configurationItemData.SortOrder = _dto.UnemploymentSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.UnhappinessCommercial)
                {
                    configurationItemData.Enabled = _dto.UnhappinessCommercial;
                    configurationItemData.CriticalThreshold = _dto.UnhappinessCommercialCriticalThreshold;
                    configurationItemData.SortOrder = _dto.UnhappinessCommercialSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.UnhappinessIndustrial)
                {
                    configurationItemData.Enabled = _dto.UnhappinessIndustrial;
                    configurationItemData.CriticalThreshold = _dto.UnhappinessIndustrialCriticalThreshold;
                    configurationItemData.SortOrder = _dto.UnhappinessIndustrialSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.UnhappinessOffice)
                {
                    configurationItemData.Enabled = _dto.UnhappinessOffice;
                    configurationItemData.CriticalThreshold = _dto.UnhappinessOfficeCriticalThreshold;
                    configurationItemData.SortOrder = _dto.UnhappinessOfficeSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.UnhappinessResidential)
                {
                    configurationItemData.Enabled = _dto.UnhappinessResidential;
                    configurationItemData.CriticalThreshold = _dto.UnhappinessResidentialCriticalThreshold;
                    configurationItemData.SortOrder = _dto.UnhappinessResidentialSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.University)
                {
                    configurationItemData.Enabled = _dto.University;
                    configurationItemData.CriticalThreshold = _dto.UniversityCriticalThreshold;
                    configurationItemData.SortOrder = _dto.UniversitySortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.Water)
                {
                    configurationItemData.Enabled = _dto.Water;
                    configurationItemData.CriticalThreshold = _dto.WaterCriticalThreshold;
                    configurationItemData.SortOrder = _dto.WaterSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.WaterPumpingServiceStorage)
                {
                    configurationItemData.Enabled = _dto.WaterPumpingServiceStorage;
                    configurationItemData.CriticalThreshold = _dto.WaterPumpingServiceStorageCriticalThreshold;
                    configurationItemData.SortOrder = _dto.WaterPumpingServiceStorageSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.WaterPumpingServiceVehicles)
                {
                    configurationItemData.Enabled = _dto.WaterPumpingServiceVehicles;
                    configurationItemData.CriticalThreshold = _dto.WaterPumpingServiceVehiclesCriticalThreshold;
                    configurationItemData.SortOrder = _dto.WaterPumpingServiceVehiclesSortOrder;
                }
                else if (configurationItemData.ItemData == ItemData.WaterReserveTank)
                {
                    configurationItemData.Enabled = _dto.WaterReserveTank;
                    configurationItemData.CriticalThreshold = _dto.WaterReserveTankCriticalThreshold;
                    configurationItemData.SortOrder = _dto.WaterReserveTankSortOrder;
                }
                else
                {
                    throw new Exception("Unknown configItem.Item type");
                }
            }
        }
    }
}
