using System;
using System.Linq;
using UnityEngine;

namespace Stats.Config;

public sealed class Configuration
{
    private readonly ConfigurationService<ConfigurationDto> _configurationService;
    private readonly ConfigurationItemData[] _configurationItemDatas;

    public Configuration(ConfigurationService<ConfigurationDto> configurationService, ConfigurationDto dto)
    {
        if (configurationService is null)
            throw new ArgumentNullException(nameof(configurationService));
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        _configurationService = configurationService;

        _configurationItemDatas = ItemData
            .AllItems
            .Select(i => new ConfigurationItemData(i, false, 0, 0))
            .OrderBy(i => i.ItemData.Index)
            .ToArray();

        ApplyDto(dto);

        ValidateIndexes(_configurationItemDatas);
    }

    public Vector2 MainPanelPosition { get; set; }
    public int MainPanelUpdateEveryXSeconds { get; set; }
    public bool MainPanelAutoHide { get; set; }
    public bool MainPanelHideItemsBelowThreshold { get; set; }
    public bool MainPanelHideItemsNotAvailable { get; set; }
    public int MainPanelColumnCount { get; set; }
    public Color32 MainPanelBackgroundColor { get; set; }
    public Color32 MainPanelForegroundColor { get; set; }
    public Color32 MainPanelAccentColor { get; set; }
    public float ItemPadding { get; set; }
    public float ItemIconSize { get; set; }
    public float ItemTextScale { get; set; }
    public ItemTextPosition ItemTextPosition { get; set; } = ItemTextPosition.None;

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

    public void Reset()
    {
        var dto = new ConfigurationDto();
        ApplyDto(dto);
        Save();
    }

    public void ResetPosition()
    {
        MainPanelPosition = Vector2.zero;
        Save();
    }

    public void Save()
    {
        var dto = GetConfigurationDto();
        _configurationService.Save(dto);
    }

    public ConfigurationDto GetConfigurationDto()
    {
        return new ConfigurationDto()
        {
            MainPanelPositionX = MainPanelPosition.x,
            MainPanelPositionY = MainPanelPosition.y,
            MainPanelUpdateEveryXSeconds = MainPanelUpdateEveryXSeconds,
            MainPanelAutoHide = MainPanelAutoHide,
            MainPanelHideItemsBelowThreshold = MainPanelHideItemsBelowThreshold,
            MainPanelHideItemsNotAvailable = MainPanelHideItemsNotAvailable,
            MainPanelColumnCount = MainPanelColumnCount,
            MainPanelBackgroundColor = MainPanelBackgroundColor.GetColorString(),
            MainPanelForegroundColor = MainPanelForegroundColor.GetColorString(),
            MainPanelAccentColor = MainPanelAccentColor.GetColorString(),
            ItemIconSize = ItemIconSize,
            ItemPadding = ItemPadding,
            ItemTextScale = ItemTextScale,
            ItemTextPosition = ItemTextPosition.Name,
            AverageIllnessRate = GetConfigurationItemData(ItemData.AverageIllnessRate).Enabled,
            AverageIllnessRateCriticalThreshold = GetConfigurationItemData(ItemData.AverageIllnessRate).CriticalThreshold,
            AverageIllnessRateSortOrder = GetConfigurationItemData(ItemData.AverageIllnessRate).SortOrder,
            AverageChildrenIllnessRate = GetConfigurationItemData(ItemData.AverageChildrenIllnessRate).Enabled,
            AverageChildrenIllnessRateCriticalThreshold = GetConfigurationItemData(ItemData.AverageChildrenIllnessRate).CriticalThreshold,
            AverageChildrenIllnessRateSortOrder = GetConfigurationItemData(ItemData.AverageChildrenIllnessRate).SortOrder,
            AverageElderlyIllnessRate = GetConfigurationItemData(ItemData.AverageElderlyIllnessRate).Enabled,
            AverageElderlyIllnessRateCriticalThreshold = GetConfigurationItemData(ItemData.AverageElderlyIllnessRate).CriticalThreshold,
            AverageElderlyIllnessRateSortOrder = GetConfigurationItemData(ItemData.AverageElderlyIllnessRate).SortOrder,
            Cemetery = GetConfigurationItemData(ItemData.Cemetery).Enabled,
            CemeteryCriticalThreshold = GetConfigurationItemData(ItemData.Cemetery).CriticalThreshold,
            CemeterySortOrder = GetConfigurationItemData(ItemData.Cemetery).SortOrder,
            CemeteryVehicles = GetConfigurationItemData(ItemData.CemeteryVehicles).Enabled,
            CemeteryVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.CemeteryVehicles).CriticalThreshold,
            CemeteryVehiclesSortOrder = GetConfigurationItemData(ItemData.CemeteryVehicles).SortOrder,
            CityUnattractiveness = GetConfigurationItemData(ItemData.CityUnattractiveness).Enabled,
            CityUnattractivenessCriticalThreshold = GetConfigurationItemData(ItemData.CityUnattractiveness).CriticalThreshold,
            CityUnattractivenessSortOrder = GetConfigurationItemData(ItemData.CityUnattractiveness).SortOrder,
            Crematorium = GetConfigurationItemData(ItemData.Crematorium).Enabled,
            CrematoriumCriticalThreshold = GetConfigurationItemData(ItemData.Crematorium).CriticalThreshold,
            CrematoriumSortOrder = GetConfigurationItemData(ItemData.Crematorium).SortOrder,
            CrematoriumVehicles = GetConfigurationItemData(ItemData.CrematoriumVehicles).Enabled,
            CrematoriumVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.CrematoriumVehicles).CriticalThreshold,
            CrematoriumVehiclesSortOrder = GetConfigurationItemData(ItemData.CrematoriumVehicles).SortOrder,
            CrimeRate = GetConfigurationItemData(ItemData.CrimeRate).Enabled,
            CrimeRateCriticalThreshold = GetConfigurationItemData(ItemData.CrimeRate).CriticalThreshold,
            CrimeRateSortOrder = GetConfigurationItemData(ItemData.CrimeRate).SortOrder,
            DisasterResponseHelicopters = GetConfigurationItemData(ItemData.DisasterResponseHelicopters).Enabled,
            DisasterResponseHelicoptersCriticalThreshold = GetConfigurationItemData(ItemData.DisasterResponseHelicopters).CriticalThreshold,
            DisasterResponseHelicoptersSortOrder = GetConfigurationItemData(ItemData.DisasterResponseHelicopters).SortOrder,
            DisasterResponseVehicles = GetConfigurationItemData(ItemData.DisasterResponseVehicles).Enabled,
            DisasterResponseVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.DisasterResponseVehicles).CriticalThreshold,
            DisasterResponseVehiclesSortOrder = GetConfigurationItemData(ItemData.DisasterResponseVehicles).SortOrder,
            DrinkingWaterPollution = GetConfigurationItemData(ItemData.DrinkingWaterPollution).Enabled,
            DrinkingWaterPollutionCriticalThreshold = GetConfigurationItemData(ItemData.DrinkingWaterPollution).CriticalThreshold,
            DrinkingWaterPollutionSortOrder = GetConfigurationItemData(ItemData.DrinkingWaterPollution).SortOrder,
            Electricity = GetConfigurationItemData(ItemData.Electricity).Enabled,
            ElectricityCriticalThreshold = GetConfigurationItemData(ItemData.Electricity).CriticalThreshold,
            ElectricitySortOrder = GetConfigurationItemData(ItemData.Electricity).SortOrder,
            ElementarySchool = GetConfigurationItemData(ItemData.ElementarySchool).Enabled,
            ElementarySchoolCriticalThreshold = GetConfigurationItemData(ItemData.ElementarySchool).CriticalThreshold,
            ElementarySchoolSortOrder = GetConfigurationItemData(ItemData.ElementarySchool).SortOrder,
            FireDepartmentVehicles = GetConfigurationItemData(ItemData.FireDepartmentVehicles).Enabled,
            FireDepartmentVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.FireDepartmentVehicles).CriticalThreshold,
            FireDepartmentVehiclesSortOrder = GetConfigurationItemData(ItemData.FireDepartmentVehicles).SortOrder,
            FireHazard = GetConfigurationItemData(ItemData.FireHazard).Enabled,
            FireHazardCriticalThreshold = GetConfigurationItemData(ItemData.FireHazard).CriticalThreshold,
            FireHazardSortOrder = GetConfigurationItemData(ItemData.FireHazard).SortOrder,
            FireHelicopters = GetConfigurationItemData(ItemData.FireHelicopters).Enabled,
            FireHelicoptersCriticalThreshold = GetConfigurationItemData(ItemData.FireHelicopters).CriticalThreshold,
            FireHelicoptersSortOrder = GetConfigurationItemData(ItemData.FireHelicopters).SortOrder,
            GarbageProcessing = GetConfigurationItemData(ItemData.GarbageProcessing).Enabled,
            GarbageProcessingCriticalThreshold = GetConfigurationItemData(ItemData.GarbageProcessing).CriticalThreshold,
            GarbageProcessingSortOrder = GetConfigurationItemData(ItemData.GarbageProcessing).SortOrder,
            GarbageProcessingVehicles = GetConfigurationItemData(ItemData.GarbageProcessingVehicles).Enabled,
            GarbageProcessingVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.GarbageProcessingVehicles).CriticalThreshold,
            GarbageProcessingVehiclesSortOrder = GetConfigurationItemData(ItemData.GarbageProcessingVehicles).SortOrder,
            GroundPollution = GetConfigurationItemData(ItemData.GroundPollution).Enabled,
            GroundPollutionCriticalThreshold = GetConfigurationItemData(ItemData.GroundPollution).CriticalThreshold,
            GroundPollutionSortOrder = GetConfigurationItemData(ItemData.GroundPollution).SortOrder,
            Healthcare = GetConfigurationItemData(ItemData.Healthcare).Enabled,
            HealthcareCriticalThreshold = GetConfigurationItemData(ItemData.Healthcare).CriticalThreshold,
            HealthcareSortOrder = GetConfigurationItemData(ItemData.Healthcare).SortOrder,
            HealthcareVehicles = GetConfigurationItemData(ItemData.HealthcareVehicles).Enabled,
            HealthcareVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.HealthcareVehicles).CriticalThreshold,
            HealthcareVehiclesSortOrder = GetConfigurationItemData(ItemData.HealthcareVehicles).SortOrder,
            Heating = GetConfigurationItemData(ItemData.Heating).Enabled,
            HeatingCriticalThreshold = GetConfigurationItemData(ItemData.Heating).CriticalThreshold,
            HeatingSortOrder = GetConfigurationItemData(ItemData.Heating).SortOrder,
            HighSchool = GetConfigurationItemData(ItemData.HighSchool).Enabled,
            HighSchoolCriticalThreshold = GetConfigurationItemData(ItemData.HighSchool).CriticalThreshold,
            HighSchoolSortOrder = GetConfigurationItemData(ItemData.HighSchool).SortOrder,
            Landfill = GetConfigurationItemData(ItemData.Landfill).Enabled,
            LandfillCriticalThreshold = GetConfigurationItemData(ItemData.Landfill).CriticalThreshold,
            LandfillSortOrder = GetConfigurationItemData(ItemData.Landfill).SortOrder,
            LandfillVehicles = GetConfigurationItemData(ItemData.LandfillVehicles).Enabled,
            LandfillVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.LandfillVehicles).CriticalThreshold,
            LandfillVehiclesSortOrder = GetConfigurationItemData(ItemData.LandfillVehicles).SortOrder,
            Library = GetConfigurationItemData(ItemData.Library).Enabled,
            LibraryCriticalThreshold = GetConfigurationItemData(ItemData.Library).CriticalThreshold,
            LibrarySortOrder = GetConfigurationItemData(ItemData.Library).SortOrder,
            MedicalHelicopters = GetConfigurationItemData(ItemData.MedicalHelicopters).Enabled,
            MedicalHelicoptersCriticalThreshold = GetConfigurationItemData(ItemData.MedicalHelicopters).CriticalThreshold,
            MedicalHelicoptersSortOrder = GetConfigurationItemData(ItemData.MedicalHelicopters).SortOrder,
            NoisePollution = GetConfigurationItemData(ItemData.NoisePollution).Enabled,
            NoisePollutionCriticalThreshold = GetConfigurationItemData(ItemData.NoisePollution).CriticalThreshold,
            NoisePollutionSortOrder = GetConfigurationItemData(ItemData.NoisePollution).SortOrder,
            ParkMaintenanceVehicles = GetConfigurationItemData(ItemData.ParkMaintenanceVehicles).Enabled,
            ParkMaintenanceVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.ParkMaintenanceVehicles).CriticalThreshold,
            ParkMaintenanceVehiclesSortOrder = GetConfigurationItemData(ItemData.ParkMaintenanceVehicles).SortOrder,
            PoliceHelicopters = GetConfigurationItemData(ItemData.PoliceHelicopters).Enabled,
            PoliceHelicoptersCriticalThreshold = GetConfigurationItemData(ItemData.PoliceHelicopters).CriticalThreshold,
            PoliceHelicoptersSortOrder = GetConfigurationItemData(ItemData.PoliceHelicopters).SortOrder,
            PoliceHoldingCells = GetConfigurationItemData(ItemData.PoliceHoldingCells).Enabled,
            PoliceHoldingCellsCriticalThreshold = GetConfigurationItemData(ItemData.PoliceHoldingCells).CriticalThreshold,
            PoliceHoldingCellsSortOrder = GetConfigurationItemData(ItemData.PoliceHoldingCells).SortOrder,
            PoliceVehicles = GetConfigurationItemData(ItemData.PoliceVehicles).Enabled,
            PoliceVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.PoliceVehicles).CriticalThreshold,
            PoliceVehiclesSortOrder = GetConfigurationItemData(ItemData.PoliceVehicles).SortOrder,
            PostTrucks = GetConfigurationItemData(ItemData.PostTrucks).Enabled,
            PostTrucksCriticalThreshold = GetConfigurationItemData(ItemData.PostTrucks).CriticalThreshold,
            PostTrucksSortOrder = GetConfigurationItemData(ItemData.PostTrucks).SortOrder,
            PostVans = GetConfigurationItemData(ItemData.PostVans).Enabled,
            PostVansCriticalThreshold = GetConfigurationItemData(ItemData.PostVans).CriticalThreshold,
            PostVansSortOrder = GetConfigurationItemData(ItemData.PostVans).SortOrder,
            PrisonCells = GetConfigurationItemData(ItemData.PrisonCells).Enabled,
            PrisonCellsCriticalThreshold = GetConfigurationItemData(ItemData.PrisonCells).CriticalThreshold,
            PrisonCellsSortOrder = GetConfigurationItemData(ItemData.PrisonCells).SortOrder,
            PrisonVehicles = GetConfigurationItemData(ItemData.PrisonVehicles).Enabled,
            PrisonVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.PrisonVehicles).CriticalThreshold,
            PrisonVehiclesSortOrder = GetConfigurationItemData(ItemData.PrisonVehicles).SortOrder,
            RoadMaintenanceVehicles = GetConfigurationItemData(ItemData.RoadMaintenanceVehicles).Enabled,
            RoadMaintenanceVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.RoadMaintenanceVehicles).CriticalThreshold,
            RoadMaintenanceVehiclesSortOrder = GetConfigurationItemData(ItemData.RoadMaintenanceVehicles).SortOrder,
            SewageTreatment = GetConfigurationItemData(ItemData.SewageTreatment).Enabled,
            SewageTreatmentCriticalThreshold = GetConfigurationItemData(ItemData.SewageTreatment).CriticalThreshold,
            SewageTreatmentSortOrder = GetConfigurationItemData(ItemData.SewageTreatment).SortOrder,
            SnowDump = GetConfigurationItemData(ItemData.SnowDump).Enabled,
            SnowDumpCriticalThreshold = GetConfigurationItemData(ItemData.SnowDump).CriticalThreshold,
            SnowDumpSortOrder = GetConfigurationItemData(ItemData.SnowDump).SortOrder,
            SnowDumpVehicles = GetConfigurationItemData(ItemData.SnowDumpVehicles).Enabled,
            SnowDumpVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.SnowDumpVehicles).CriticalThreshold,
            SnowDumpVehiclesSortOrder = GetConfigurationItemData(ItemData.SnowDumpVehicles).SortOrder,
            Taxis = GetConfigurationItemData(ItemData.Taxis).Enabled,
            TaxisCriticalThreshold = GetConfigurationItemData(ItemData.Taxis).CriticalThreshold,
            TaxisSortOrder = GetConfigurationItemData(ItemData.Taxis).SortOrder,
            TrafficJam = GetConfigurationItemData(ItemData.TrafficJam).Enabled,
            TrafficJamCriticalThreshold = GetConfigurationItemData(ItemData.TrafficJam).CriticalThreshold,
            TrafficJamSortOrder = GetConfigurationItemData(ItemData.TrafficJam).SortOrder,
            Unemployment = GetConfigurationItemData(ItemData.Unemployment).Enabled,
            UnemploymentCriticalThreshold = GetConfigurationItemData(ItemData.Unemployment).CriticalThreshold,
            UnemploymentSortOrder = GetConfigurationItemData(ItemData.Unemployment).SortOrder,
            UnhappinessCommercial = GetConfigurationItemData(ItemData.UnhappinessCommercial).Enabled,
            UnhappinessCommercialCriticalThreshold = GetConfigurationItemData(ItemData.UnhappinessCommercial).CriticalThreshold,
            UnhappinessCommercialSortOrder = GetConfigurationItemData(ItemData.UnhappinessCommercial).SortOrder,
            UnhappinessIndustrial = GetConfigurationItemData(ItemData.UnhappinessIndustrial).Enabled,
            UnhappinessIndustrialCriticalThreshold = GetConfigurationItemData(ItemData.UnhappinessIndustrial).CriticalThreshold,
            UnhappinessIndustrialSortOrder = GetConfigurationItemData(ItemData.UnhappinessIndustrial).SortOrder,
            UnhappinessOffice = GetConfigurationItemData(ItemData.UnhappinessOffice).Enabled,
            UnhappinessOfficeCriticalThreshold = GetConfigurationItemData(ItemData.UnhappinessOffice).CriticalThreshold,
            UnhappinessOfficeSortOrder = GetConfigurationItemData(ItemData.UnhappinessOffice).SortOrder,
            UnhappinessResidential = GetConfigurationItemData(ItemData.UnhappinessResidential).Enabled,
            UnhappinessResidentialCriticalThreshold = GetConfigurationItemData(ItemData.UnhappinessResidential).CriticalThreshold,
            UnhappinessResidentialSortOrder = GetConfigurationItemData(ItemData.UnhappinessResidential).SortOrder,
            University = GetConfigurationItemData(ItemData.University).Enabled,
            UniversityCriticalThreshold = GetConfigurationItemData(ItemData.University).CriticalThreshold,
            UniversitySortOrder = GetConfigurationItemData(ItemData.University).SortOrder,
            Water = GetConfigurationItemData(ItemData.Water).Enabled,
            WaterCriticalThreshold = GetConfigurationItemData(ItemData.Water).CriticalThreshold,
            WaterSortOrder = GetConfigurationItemData(ItemData.Water).SortOrder,
            WaterPumpingServiceStorage = GetConfigurationItemData(ItemData.WaterPumpingServiceStorage).Enabled,
            WaterPumpingServiceStorageCriticalThreshold = GetConfigurationItemData(ItemData.WaterPumpingServiceStorage).CriticalThreshold,
            WaterPumpingServiceStorageSortOrder = GetConfigurationItemData(ItemData.WaterPumpingServiceStorage).SortOrder,
            WaterPumpingServiceVehicles = GetConfigurationItemData(ItemData.WaterPumpingServiceVehicles).Enabled,
            WaterPumpingServiceVehiclesCriticalThreshold = GetConfigurationItemData(ItemData.WaterPumpingServiceVehicles).CriticalThreshold,
            WaterPumpingServiceVehiclesSortOrder = GetConfigurationItemData(ItemData.WaterPumpingServiceVehicles).SortOrder,
            WaterReserveTank = GetConfigurationItemData(ItemData.WaterReserveTank).Enabled,
            WaterReserveTankCriticalThreshold = GetConfigurationItemData(ItemData.WaterReserveTank).CriticalThreshold,
            WaterReserveTankSortOrder = GetConfigurationItemData(ItemData.WaterReserveTank).SortOrder,
        };
    }

    private void ApplyDto(ConfigurationDto dto)
    {
        MainPanelPosition = new Vector2(dto.MainPanelPositionX, dto.MainPanelPositionY);
        MainPanelUpdateEveryXSeconds = dto.MainPanelUpdateEveryXSeconds;
        MainPanelAutoHide = dto.MainPanelAutoHide;
        MainPanelHideItemsBelowThreshold = dto.MainPanelHideItemsBelowThreshold;
        MainPanelHideItemsNotAvailable = dto.MainPanelHideItemsNotAvailable;
        MainPanelColumnCount = dto.MainPanelColumnCount;
        MainPanelBackgroundColor = dto.MainPanelBackgroundColor.GetColor32();
        MainPanelForegroundColor = dto.MainPanelForegroundColor.GetColor32();
        MainPanelAccentColor = dto.MainPanelAccentColor.GetColor32();
        ItemIconSize = dto.ItemIconSize;
        ItemTextScale = dto.ItemTextScale;
        ItemPadding = dto.ItemPadding;
        ItemTextPosition = ItemTextPosition.Parse(dto.ItemTextPosition);

        ApplyItem(ItemData.AverageIllnessRate, dto.AverageIllnessRate, dto.AverageIllnessRateCriticalThreshold, dto.AverageIllnessRateSortOrder);
        ApplyItem(ItemData.AverageChildrenIllnessRate, dto.AverageChildrenIllnessRate, dto.AverageChildrenIllnessRateCriticalThreshold, dto.AverageChildrenIllnessRateSortOrder);
        ApplyItem(ItemData.AverageElderlyIllnessRate, dto.AverageElderlyIllnessRate, dto.AverageElderlyIllnessRateCriticalThreshold, dto.AverageElderlyIllnessRateSortOrder);
        ApplyItem(ItemData.Cemetery, dto.Cemetery, dto.CemeteryCriticalThreshold, dto.CemeterySortOrder);
        ApplyItem(ItemData.CemeteryVehicles, dto.CemeteryVehicles, dto.CemeteryVehiclesCriticalThreshold, dto.CemeteryVehiclesSortOrder);
        ApplyItem(ItemData.CityUnattractiveness, dto.CityUnattractiveness, dto.CityUnattractivenessCriticalThreshold, dto.CityUnattractivenessSortOrder);
        ApplyItem(ItemData.Crematorium, dto.Crematorium, dto.CrematoriumCriticalThreshold, dto.CrematoriumSortOrder);
        ApplyItem(ItemData.CrematoriumVehicles, dto.CrematoriumVehicles, dto.CrematoriumVehiclesCriticalThreshold, dto.CrematoriumVehiclesSortOrder);
        ApplyItem(ItemData.CrimeRate, dto.CrimeRate, dto.CrimeRateCriticalThreshold, dto.CrimeRateSortOrder);
        ApplyItem(ItemData.DisasterResponseHelicopters, dto.DisasterResponseHelicopters, dto.DisasterResponseHelicoptersCriticalThreshold, dto.DisasterResponseHelicoptersSortOrder);
        ApplyItem(ItemData.DisasterResponseVehicles, dto.DisasterResponseVehicles, dto.DisasterResponseVehiclesCriticalThreshold, dto.DisasterResponseVehiclesSortOrder);
        ApplyItem(ItemData.DrinkingWaterPollution, dto.DrinkingWaterPollution, dto.DrinkingWaterPollutionCriticalThreshold, dto.DrinkingWaterPollutionSortOrder);
        ApplyItem(ItemData.Electricity, dto.Electricity, dto.ElectricityCriticalThreshold, dto.ElectricitySortOrder);
        ApplyItem(ItemData.ElementarySchool, dto.ElementarySchool, dto.ElementarySchoolCriticalThreshold, dto.ElementarySchoolSortOrder);
        ApplyItem(ItemData.FireDepartmentVehicles, dto.FireDepartmentVehicles, dto.FireDepartmentVehiclesCriticalThreshold, dto.FireDepartmentVehiclesSortOrder);
        ApplyItem(ItemData.FireHazard, dto.FireHazard, dto.FireHazardCriticalThreshold, dto.FireHazardSortOrder);
        ApplyItem(ItemData.FireHelicopters, dto.FireHelicopters, dto.FireHelicoptersCriticalThreshold, dto.FireHelicoptersSortOrder);
        ApplyItem(ItemData.GarbageProcessing, dto.GarbageProcessing, dto.GarbageProcessingCriticalThreshold, dto.GarbageProcessingSortOrder);
        ApplyItem(ItemData.GarbageProcessingVehicles, dto.GarbageProcessingVehicles, dto.GarbageProcessingVehiclesCriticalThreshold, dto.GarbageProcessingVehiclesSortOrder);
        ApplyItem(ItemData.GroundPollution, dto.GroundPollution, dto.GroundPollutionCriticalThreshold, dto.GroundPollutionSortOrder);
        ApplyItem(ItemData.Healthcare, dto.Healthcare, dto.HealthcareCriticalThreshold, dto.HealthcareSortOrder);
        ApplyItem(ItemData.HealthcareVehicles, dto.HealthcareVehicles, dto.HealthcareVehiclesCriticalThreshold, dto.HealthcareVehiclesSortOrder);
        ApplyItem(ItemData.Heating, dto.Heating, dto.HeatingCriticalThreshold, dto.HeatingSortOrder);
        ApplyItem(ItemData.HighSchool, dto.HighSchool, dto.HighSchoolCriticalThreshold, dto.HighSchoolSortOrder);
        ApplyItem(ItemData.Landfill, dto.Landfill, dto.LandfillCriticalThreshold, dto.LandfillSortOrder);
        ApplyItem(ItemData.LandfillVehicles, dto.LandfillVehicles, dto.LandfillVehiclesCriticalThreshold, dto.LandfillVehiclesSortOrder);
        ApplyItem(ItemData.Library, dto.Library, dto.LibraryCriticalThreshold, dto.LibrarySortOrder);
        ApplyItem(ItemData.MedicalHelicopters, dto.MedicalHelicopters, dto.MedicalHelicoptersCriticalThreshold, dto.MedicalHelicoptersSortOrder);
        ApplyItem(ItemData.NoisePollution, dto.NoisePollution, dto.NoisePollutionCriticalThreshold, dto.NoisePollutionSortOrder);
        ApplyItem(ItemData.ParkMaintenanceVehicles, dto.ParkMaintenanceVehicles, dto.ParkMaintenanceVehiclesCriticalThreshold, dto.ParkMaintenanceVehiclesSortOrder);
        ApplyItem(ItemData.PoliceHelicopters, dto.PoliceHelicopters, dto.PoliceHelicoptersCriticalThreshold, dto.PoliceHelicoptersSortOrder);
        ApplyItem(ItemData.PoliceHoldingCells, dto.PoliceHoldingCells, dto.PoliceHoldingCellsCriticalThreshold, dto.PoliceHoldingCellsSortOrder);
        ApplyItem(ItemData.PoliceVehicles, dto.PoliceVehicles, dto.PoliceVehiclesCriticalThreshold, dto.PoliceVehiclesSortOrder);
        ApplyItem(ItemData.PostTrucks, dto.PostTrucks, dto.PostTrucksCriticalThreshold, dto.PostTrucksSortOrder);
        ApplyItem(ItemData.PostVans, dto.PostVans, dto.PostVansCriticalThreshold, dto.PostVansSortOrder);
        ApplyItem(ItemData.PrisonCells, dto.PrisonCells, dto.PrisonCellsCriticalThreshold, dto.PrisonCellsSortOrder);
        ApplyItem(ItemData.PrisonVehicles, dto.PrisonVehicles, dto.PrisonVehiclesCriticalThreshold, dto.PrisonVehiclesSortOrder);
        ApplyItem(ItemData.RoadMaintenanceVehicles, dto.RoadMaintenanceVehicles, dto.RoadMaintenanceVehiclesCriticalThreshold, dto.RoadMaintenanceVehiclesSortOrder);
        ApplyItem(ItemData.SewageTreatment, dto.SewageTreatment, dto.SewageTreatmentCriticalThreshold, dto.SewageTreatmentSortOrder);
        ApplyItem(ItemData.SnowDump, dto.SnowDump, dto.SnowDumpCriticalThreshold, dto.SnowDumpSortOrder);
        ApplyItem(ItemData.SnowDumpVehicles, dto.SnowDumpVehicles, dto.SnowDumpVehiclesCriticalThreshold, dto.SnowDumpVehiclesSortOrder);
        ApplyItem(ItemData.Taxis, dto.Taxis, dto.TaxisCriticalThreshold, dto.TaxisSortOrder);
        ApplyItem(ItemData.TrafficJam, dto.TrafficJam, dto.TrafficJamCriticalThreshold, dto.TrafficJamSortOrder);
        ApplyItem(ItemData.Unemployment, dto.Unemployment, dto.UnemploymentCriticalThreshold, dto.UnemploymentSortOrder);
        ApplyItem(ItemData.UnhappinessCommercial, dto.UnhappinessCommercial, dto.UnhappinessCommercialCriticalThreshold, dto.UnhappinessCommercialSortOrder);
        ApplyItem(ItemData.UnhappinessIndustrial, dto.UnhappinessIndustrial, dto.UnhappinessIndustrialCriticalThreshold, dto.UnhappinessIndustrialSortOrder);
        ApplyItem(ItemData.UnhappinessOffice, dto.UnhappinessOffice, dto.UnhappinessOfficeCriticalThreshold, dto.UnhappinessOfficeSortOrder);
        ApplyItem(ItemData.UnhappinessResidential, dto.UnhappinessResidential, dto.UnhappinessResidentialCriticalThreshold, dto.UnhappinessResidentialSortOrder);
        ApplyItem(ItemData.University, dto.University, dto.UniversityCriticalThreshold, dto.UniversitySortOrder);
        ApplyItem(ItemData.Water, dto.Water, dto.WaterCriticalThreshold, dto.WaterSortOrder);
        ApplyItem(ItemData.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorage, dto.WaterPumpingServiceStorageCriticalThreshold, dto.WaterPumpingServiceStorageSortOrder);
        ApplyItem(ItemData.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehicles, dto.WaterPumpingServiceVehiclesCriticalThreshold, dto.WaterPumpingServiceVehiclesSortOrder);
        ApplyItem(ItemData.WaterReserveTank, dto.WaterReserveTank, dto.WaterReserveTankCriticalThreshold, dto.WaterReserveTankSortOrder);
    }

    private void ApplyItem(ItemData itemData, bool enabled, int criticalThreshold, int sortOrder)
    {
        var configurationItemData = GetConfigurationItemData(itemData);
        configurationItemData.Enabled = enabled;
        configurationItemData.CriticalThreshold = criticalThreshold;
        configurationItemData.SortOrder = sortOrder;
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
}
