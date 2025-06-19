using System;
using UnityEngine;

namespace Stats;

public sealed class GameEngineService
{
    private readonly DistrictManager _districtManager;
    private readonly BuildingManager _buildingManager;
    private readonly EconomyManager _economyManager;
    private readonly ImmaterialResourceManager _immaterialResourceManager;
    private readonly CitizenManager _citizenManager;
    private readonly VehicleManager _vehicleManager;

    public GameEngineService(
        DistrictManager districtManager,
        BuildingManager buildingManager,
        EconomyManager economyManager,
        ImmaterialResourceManager immaterialResourceManager,
        CitizenManager citizenManager,
        VehicleManager vehicleManager)
    {
        if (districtManager is null)
            throw new ArgumentNullException(nameof(districtManager));
        if (buildingManager is null)
            throw new ArgumentNullException(nameof(buildingManager));
        if (economyManager is null)
            throw new ArgumentNullException(nameof(economyManager));
        if (immaterialResourceManager is null)
            throw new ArgumentNullException(nameof(immaterialResourceManager));
        if (citizenManager is null)
            throw new ArgumentNullException(nameof(citizenManager));
        if (vehicleManager is null)
            throw new ArgumentNullException(nameof(vehicleManager));

        _districtManager = districtManager;
        _buildingManager = buildingManager;
        _economyManager = economyManager;
        _immaterialResourceManager = immaterialResourceManager;
        _citizenManager = citizenManager;
        _vehicleManager = vehicleManager;
    }

    public static bool HasMapSnowDumps()
    {
        var loadedCount = PrefabCollection<BuildingInfo>.LoadedCount();
        for (uint i = 0; i < loadedCount; i++)
        {
            var bi = PrefabCollection<BuildingInfo>.GetLoaded(i);
            if (bi is null)
                continue;

            if (bi.m_buildingAI is SnowDumpAI)
                return true;
        }

        return false;
    }

    public int? GetAverageIllnessRate()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return globalDistrict.GetSickCount() == 0
            ? null
            : (int)(100 - (float)globalDistrict.m_residentialData.m_finalHealth);
    }

    public int? GetAverageChildrenIllnessRate()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        var childrenAndTeensCount = globalDistrict.m_childData.m_finalCount
                                    + globalDistrict.m_teenData.m_finalCount;
        var sickChildrenAndTeensCount = globalDistrict.m_childHealthData.m_finalCount;

        return childrenAndTeensCount == 0
            ? null
            : (int)(100 - ((float)sickChildrenAndTeensCount / childrenAndTeensCount));
    }

    public int? GetAverageElderlyIllnessRate()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        var elderlyCount = globalDistrict.m_seniorData.m_finalCount;
        var sickElderlyCount = globalDistrict.m_seniorHealthData.m_finalCount;

        return elderlyCount == 0
            ? null
            : (int)(100 - ((float)sickElderlyCount / elderlyCount));
    }

    public int? GetCemeteryPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetDeadCapacity(), globalDistrict.GetDeadAmount());
    }

    public int? GetHealthCareVehiclesPercent()
    {
        var healthcareVehiclesTotal = 0;
        var healthcareVehiclesInUse = 0;

        var healthcareBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.HealthCare);

        for (int i = 0; i < healthcareBuildingIds.m_size; i++)
        {
            var buildingId = healthcareBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var hospitalAI = building.Info.GetAI() as HospitalAI;
            if (hospitalAI is null)
                continue;

            int budget = _economyManager.GetBudget(hospitalAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int healthcareVehicles = ((productionRate * hospitalAI.AmbulanceCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Sick, ref count, ref cargo, ref capacity, ref outside);

            healthcareVehiclesTotal += healthcareVehicles;
            healthcareVehiclesInUse += count;
        }

        return GetUsagePercent(healthcareVehiclesTotal, healthcareVehiclesInUse);
    }

    public int? GetMedicalHelicoptersPercent()
    {
        var medicalHelicoptersTotal = 0;
        var medicalHelicoptersInUse = 0;

        var healthcareBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.HealthCare);

        for (int i = 0; i < healthcareBuildingIds.m_size; i++)
        {
            var buildingId = healthcareBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var helicopterDepotAI = building.Info.GetAI() as HelicopterDepotAI;
            if (helicopterDepotAI is null)
                continue;

            int budget = _economyManager.GetBudget(helicopterDepotAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int medicalHelicopters = ((productionRate * helicopterDepotAI.m_helicopterCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Sick2, ref count, ref cargo, ref capacity, ref outside);

            medicalHelicoptersTotal += medicalHelicopters;
            medicalHelicoptersInUse += count;
        }

        return GetUsagePercent(medicalHelicoptersTotal, medicalHelicoptersInUse);
    }

    public int? GetCemeteryVehiclesPercent()
    {
        var cemeteryVehiclesTotal = 0;
        var cemeteryVehiclesInUse = 0;

        var healthcareBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.HealthCare);

        for (int i = 0; i < healthcareBuildingIds.m_size; i++)
        {
            var buildingId = healthcareBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var cemeteryAI = building.Info.GetAI() as CemeteryAI;
            if (cemeteryAI is null || cemeteryAI.m_graveCount == 0) // m_graveCount == 0 -> Crematorium
                continue;

            int budget = _economyManager.GetBudget(cemeteryAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);

            int cemeteryVehicles = ((productionRate * cemeteryAI.m_hearseCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Dead, ref count, ref cargo, ref capacity, ref outside);
            }
            else
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.DeadMove, ref count, ref cargo, ref capacity, ref outside);
            }

            cemeteryVehiclesTotal += cemeteryVehicles;
            cemeteryVehiclesInUse += count;
        }

        return GetUsagePercent(cemeteryVehiclesTotal, cemeteryVehiclesInUse);
    }

    public int? GetCrematoriumVehiclesPercent()
    {
        var crematoriumVehiclesTotal = 0;
        var crematoriumVehiclesInUse = 0;

        var healthcareBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.HealthCare);

        for (int i = 0; i < healthcareBuildingIds.m_size; i++)
        {
            var buildingId = healthcareBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var cemeteryAI = building.Info.GetAI() as CemeteryAI;
            if (cemeteryAI is null || cemeteryAI.m_graveCount > 0) // m_graveCount > 0 -> Cemetery
                continue;

            int budget = _economyManager.GetBudget(cemeteryAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);

            int cemeteryVehicles = ((productionRate * cemeteryAI.m_hearseCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Dead, ref count, ref cargo, ref capacity, ref outside);
            }
            else
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.DeadMove, ref count, ref cargo, ref capacity, ref outside);
            }

            crematoriumVehiclesTotal += cemeteryVehicles;
            crematoriumVehiclesInUse += count;
        }

        return GetUsagePercent(crematoriumVehiclesTotal, crematoriumVehiclesInUse);
    }

    public int? GetCityUnattractivenessPercent()
    {
        _immaterialResourceManager.CheckGlobalResource(ImmaterialResourceManager.Resource.Attractiveness, out int cityAttractivenessRaw);
        _immaterialResourceManager.CheckTotalResource(ImmaterialResourceManager.Resource.LandValue, out int landValueRaw);
        var cityAttractivenessAndLandValue = cityAttractivenessRaw + landValueRaw;
        var cityAttractiveness = 100 * cityAttractivenessAndLandValue / Mathf.Max(cityAttractivenessAndLandValue + 200, 200);

        return 100 - cityAttractiveness;
    }

    public int? GetCrematoriumPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetCremateCapacity(), globalDistrict.GetDeadCount());
    }

    public int? GetCrimeRatePercent()
    {
        return _districtManager.m_districts.m_buffer[0].m_finalCrimeRate;
    }

    public int? GetDisasterResponseVehiclesPercent()
    {
        var disasterResponseVehiclesTotal = 0;
        var disasterResponseVehiclesInUse = 0;

        var disasterBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Disaster);
        for (int i = 0; i < disasterBuildingIds.m_size; i++)
        {
            var buildingId = disasterBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var disasterResponseBuildingAI = building.Info.GetAI() as DisasterResponseBuildingAI;
            if (disasterResponseBuildingAI is null)
                continue;

            int budget = _economyManager.GetBudget(disasterResponseBuildingAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);

            disasterResponseVehiclesTotal += ((productionRate * disasterResponseBuildingAI.m_vehicleCount) + 99) / 100;
            int disasterVehicles = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Collapsed, ref disasterVehicles, ref cargo, ref capacity, ref outside);
            disasterResponseVehiclesInUse += disasterVehicles;
        }

        return GetUsagePercent(disasterResponseVehiclesTotal, disasterResponseVehiclesInUse);
    }

    public int? GetDisasterResponseHelicoptersPercent()
    {
        var disasterResponseHelicoptersTotal = 0;
        var disasterResponseHelicoptersInUse = 0;

        var disasterBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Disaster);
        for (int i = 0; i < disasterBuildingIds.m_size; i++)
        {
            var buildingId = disasterBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var disasterResponseBuildingAI = building.Info.GetAI() as DisasterResponseBuildingAI;
            if (disasterResponseBuildingAI is null)
                continue;

            int budget = _economyManager.GetBudget(disasterResponseBuildingAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);

            disasterResponseHelicoptersTotal += ((productionRate * disasterResponseBuildingAI.m_helicopterCount) + 99) / 100;
            int disasterHelicopters = 0;
            int cargo2 = 0;
            int capacity2 = 0;
            int outside2 = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Collapsed2, ref disasterHelicopters, ref cargo2, ref capacity2, ref outside2);
            disasterResponseHelicoptersInUse += disasterHelicopters;
        }

        return GetUsagePercent(disasterResponseHelicoptersTotal, disasterResponseHelicoptersInUse);
    }

    public int? GetDrinkingWaterPollutionPercent()
    {
        return _districtManager.m_districts.m_buffer[0].GetWaterPollution();
    }

    public int? GetElectricityPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(
            globalDistrict.GetElectricityCapacity(),
            globalDistrict.GetElectricityConsumption());
    }

    public int? GetElementarySchoolPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetEducation1Capacity(), globalDistrict.GetEducation1Need());
    }

    public int? GetFireDepartmentVehiclesPercent()
    {
        var fireDepartmentVehiclesTotal = 0;
        var fireDepartmentVehiclesInUse = 0;

        var fireDepartmentBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.FireDepartment);

        for (int i = 0; i < fireDepartmentBuildingIds.m_size; i++)
        {
            var buildingId = fireDepartmentBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var fireStationAI = building.Info.GetAI() as FireStationAI;
            if (fireStationAI is null)
                continue;

            int budget = _economyManager.GetBudget(fireStationAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int fireTrucks = ((productionRate * fireStationAI.m_fireTruckCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Fire, ref count, ref cargo, ref capacity, ref outside);

            fireDepartmentVehiclesTotal += fireTrucks;
            fireDepartmentVehiclesInUse += count;
        }

        return GetUsagePercent(fireDepartmentVehiclesTotal, fireDepartmentVehiclesInUse);
    }

    public int? GetFireHelicoptersPercent()
    {
        var fireHelicoptersTotal = 0;
        var fireHelicoptersInUse = 0;

        var fireDepartmentBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.FireDepartment);

        for (int i = 0; i < fireDepartmentBuildingIds.m_size; i++)
        {
            var buildingId = fireDepartmentBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var helicopterDepotAI = building.Info.GetAI() as HelicopterDepotAI;
            if (helicopterDepotAI is null)
                continue;

            int budget = _economyManager.GetBudget(helicopterDepotAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int fireHelicopters = ((productionRate * helicopterDepotAI.m_helicopterCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.ForestFire, ref count, ref cargo, ref capacity, ref outside);
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Fire2, ref count, ref cargo, ref capacity, ref outside);

            fireHelicoptersTotal += fireHelicopters;
            fireHelicoptersInUse += count;
        }

        return GetUsagePercent(fireHelicoptersTotal, fireHelicoptersInUse);
    }

    public int? GetFireHazardPercent()
    {
        _immaterialResourceManager.CheckTotalResource(ImmaterialResourceManager.Resource.FireHazard, out int fireHazard);

        return fireHazard;
    }

    public int? GetGarbageProcessingPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        var incineratorCapacity = globalDistrict.GetIncinerationCapacity();
        var incineratorAccumulation = globalDistrict.GetGarbageAccumulation();

        return GetUsagePercent(incineratorCapacity, incineratorAccumulation);
    }

    public int? GetGarbageProcessingVehiclesPercent()
    {
        var garbageProcessingVehiclesTotal = 0;
        var garbageProcessingVehiclesInUse = 0;

        var garbageBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Garbage);
        for (int i = 0; i < garbageBuildingIds.m_size; i++)
        {
            var buildingId = garbageBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var landfillSiteAI = building.Info.GetAI() as LandfillSiteAI;
            if (landfillSiteAI is null || landfillSiteAI.m_garbageConsumption <= 0) // m_garbageConsumption <= 0 -> Landfill
                continue;

            int budget = _economyManager.GetBudget(landfillSiteAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int garbageTruckVehicles = ((productionRate * landfillSiteAI.m_garbageTruckCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Garbage, ref count, ref cargo, ref capacity, ref outside);
            }
            else
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.GarbageMove, ref count, ref cargo, ref capacity, ref outside);
            }

            garbageProcessingVehiclesTotal += garbageTruckVehicles;
            garbageProcessingVehiclesInUse += count;
        }

        return GetUsagePercent(garbageProcessingVehiclesTotal, garbageProcessingVehiclesInUse);
    }

    public int? GetLandfillVehiclesPercent()
    {
        var landfillVehiclesTotal = 0;
        var landfillVehiclesInUse = 0;

        var garbageBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Garbage);
        for (int i = 0; i < garbageBuildingIds.m_size; i++)
        {
            var buildingId = garbageBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var landfillSiteAI = building.Info.GetAI() as LandfillSiteAI;
            if (landfillSiteAI is null || landfillSiteAI.m_garbageConsumption > 0) // m_garbageConsumption > 0 -> Incinerator
                continue;

            int budget = _economyManager.GetBudget(landfillSiteAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int garbageTruckVehicles = ((productionRate * landfillSiteAI.m_garbageTruckCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Garbage, ref count, ref cargo, ref capacity, ref outside);
            }
            else
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.GarbageMove, ref count, ref cargo, ref capacity, ref outside);
            }

            landfillVehiclesTotal += garbageTruckVehicles;
            landfillVehiclesInUse += count;
        }

        return GetUsagePercent(landfillVehiclesTotal, landfillVehiclesInUse);
    }

    public int? GetLibraryPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetLibraryCapacity(), globalDistrict.GetLibraryVisitorCount());
    }

    public int? GetGroundPollutionPercent()
    {
        return _districtManager.m_districts.m_buffer[0].GetGroundPollution();
    }

    public int? GetHealthCarePercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetHealCapacity(), globalDistrict.GetSickCount());
    }

    public int? GetHeatingPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetHeatingCapacity(), globalDistrict.GetHeatingConsumption());
    }

    public int? GetHighSchoolPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        var highSchoolCapacity = globalDistrict.GetEducation2Capacity();
        var highSchoolUsage = globalDistrict.GetEducation2Need();

        return GetUsagePercent(highSchoolCapacity, highSchoolUsage);
    }

    public int? GetLandfillPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        var garbageCapacity = globalDistrict.GetGarbageCapacity();
        var garbageAmout = globalDistrict.GetGarbageAmount();

        return GetUsagePercent(garbageCapacity, garbageAmout);
    }

    public int? GetNoisePollutionPercent()
    {
        _immaterialResourceManager.CheckTotalResource(ImmaterialResourceManager.Resource.NoisePollution, out int noisePollution);
        return noisePollution;
    }

    public int? GetParkMaintenanceVehiclesPercent()
    {
        var parkMaintenanceVehiclesTotal = 0;
        var parkMaintenanceVehiclesInUse = 0;

        var beautificationBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Beautification);

        for (int i = 0; i < beautificationBuildingIds.m_size; i++)
        {
            var buildingId = beautificationBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var maintenanceDepotAI = building.Info.GetAI() as MaintenanceDepotAI;
            if (maintenanceDepotAI is null)
                continue;

            var transferReason = GameMethods.GetTransferReason(maintenanceDepotAI);
            if (transferReason == TransferManager.TransferReason.None)
                continue;

            int budget = _economyManager.GetBudget(maintenanceDepotAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            if (transferReason == TransferManager.TransferReason.ParkMaintenance)
            {
                byte district = _districtManager.GetDistrict(building.m_position);
                DistrictPolicies.Services servicePolicies = _districtManager.m_districts.m_buffer[district].m_servicePolicies;
                if ((servicePolicies & DistrictPolicies.Services.ParkMaintenanceBoost) != DistrictPolicies.Services.None)
                {
                    productionRate *= 2;
                }
            }

            int trucks = ((productionRate * maintenanceDepotAI.m_maintenanceTruckCount) + 99) / 100;
            int truckCount = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, transferReason, ref truckCount, ref cargo, ref capacity, ref outside);

            parkMaintenanceVehiclesTotal += trucks;
            parkMaintenanceVehiclesInUse += truckCount;
        }

        return GetUsagePercent(parkMaintenanceVehiclesTotal, parkMaintenanceVehiclesInUse);
    }

    public int? GetPoliceHoldingCellsPercent()
    {
        var policeHoldingCellsTotal = 0;
        var policeHoldingCellsInUse = 0;

        var policeBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.PoliceDepartment);

        for (int i = 0; i < policeBuildingIds.m_size; i++)
        {
            var buildingId = policeBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var policeStationAI = building.Info.GetAI() as PoliceStationAI;

            // m_info.m_class.m_level >= ItemClass.Level.Level4 -> Prison
            if (policeStationAI is null || policeStationAI.m_info.m_class.m_level >= ItemClass.Level.Level4)
                continue;

            // PoliceStationAI.GetLocalizedStats
            uint num = building.m_citizenUnits;
            int cellsInUse = 0;
            while (num != 0)
            {
                uint nextUnit = _citizenManager.m_units.m_buffer[num].m_nextUnit;
                if ((_citizenManager.m_units.m_buffer[num].m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        uint citizen = _citizenManager.m_units.m_buffer[num].GetCitizen(j);
                        if (citizen != 0 && _citizenManager.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Visit)
                        {
                            cellsInUse++;
                        }
                    }
                }

                num = nextUnit;
            }

            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;

            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Crime, ref count, ref cargo, ref capacity, ref outside);

            policeHoldingCellsInUse += cellsInUse;
            policeHoldingCellsTotal += policeStationAI.JailCapacity;
        }

        return GetUsagePercent(policeHoldingCellsTotal, policeHoldingCellsInUse);
    }

    public int? GetPoliceVehiclesPercent()
    {
        var policeVehiclesTotal = 0;
        var policeVehiclesInUse = 0;

        var policeBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.PoliceDepartment);

        for (int i = 0; i < policeBuildingIds.m_size; i++)
        {
            var buildingId = policeBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var policeStationAI = building.Info.GetAI() as PoliceStationAI;

            // m_info.m_class.m_level >= ItemClass.Level.Level4 -> Prison
            if (policeStationAI is null || policeStationAI.m_info.m_class.m_level >= ItemClass.Level.Level4)
                continue;

            // PoliceStationAI.GetLocalizedStats
            uint num = building.m_citizenUnits;
            int cellsInUse = 0;
            while (num != 0)
            {
                uint nextUnit = _citizenManager.m_units.m_buffer[num].m_nextUnit;
                if ((_citizenManager.m_units.m_buffer[num].m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        uint citizen = _citizenManager.m_units.m_buffer[num].GetCitizen(j);
                        if (citizen != 0 && _citizenManager.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Visit)
                        {
                            cellsInUse++;
                        }
                    }
                }

                num = nextUnit;
            }

            int budget = _economyManager.GetBudget(policeStationAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int policeCars = ((productionRate * policeStationAI.PoliceCarCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;

            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Crime, ref count, ref cargo, ref capacity, ref outside);

            policeVehiclesTotal += policeCars;
            policeVehiclesInUse += count;
        }

        return GetUsagePercent(policeVehiclesTotal, policeVehiclesInUse);
    }

    public int? GetPoliceHelicoptersPercent()
    {
        var policeHelicoptersTotal = 0;
        var policeHelicoptersInUse = 0;

        var policeBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.PoliceDepartment);

        for (int i = 0; i < policeBuildingIds.m_size; i++)
        {
            var buildingId = policeBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var helicopterDepotAI = building.Info.GetAI() as HelicopterDepotAI;
            if (helicopterDepotAI is null)
                continue;

            int budget = _economyManager.GetBudget(helicopterDepotAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int policeHelicopters = ((productionRate * helicopterDepotAI.m_helicopterCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Crime, ref count, ref cargo, ref capacity, ref outside);

            policeHelicoptersTotal += policeHelicopters;
            policeHelicoptersInUse += count;
        }

        return GetUsagePercent(policeHelicoptersTotal, policeHelicoptersInUse);
    }

    public int? GetPrisonCellsPercent()
    {
        var prisonCellsTotal = 0;
        var prisonCellsInUse = 0;

        var policeBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.PoliceDepartment);

        for (int i = 0; i < policeBuildingIds.m_size; i++)
        {
            var buildingId = policeBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var policeStationAI = building.Info.GetAI() as PoliceStationAI;

            // m_info.m_class.m_level < ItemClass.Level.Level4 -> Police Station
            if (policeStationAI is null || policeStationAI.m_info.m_class.m_level < ItemClass.Level.Level4)
                continue;

            // PoliceStationAI.GetLocalizedStats
            uint num = building.m_citizenUnits;
            int cellsInUse = 0;
            while (num != 0)
            {
                uint nextUnit = _citizenManager.m_units.m_buffer[num].m_nextUnit;
                if ((_citizenManager.m_units.m_buffer[num].m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        uint citizen = _citizenManager.m_units.m_buffer[num].GetCitizen(j);
                        if (citizen != 0 && _citizenManager.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Visit)
                        {
                            cellsInUse++;
                        }
                    }
                }

                num = nextUnit;
            }

            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;

            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.CriminalMove, ref count, ref cargo, ref capacity, ref outside);

            prisonCellsTotal += policeStationAI.JailCapacity;
            prisonCellsInUse += cellsInUse;
        }

        return GetUsagePercent(prisonCellsTotal, prisonCellsInUse);
    }

    public int? GetPrisonVehiclesPercent()
    {
        var prisonVehiclesTotal = 0;
        var prisonVehiclesInUse = 0;

        var policeBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.PoliceDepartment);

        for (int i = 0; i < policeBuildingIds.m_size; i++)
        {
            var buildingId = policeBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var policeStationAI = building.Info.GetAI() as PoliceStationAI;

            // m_info.m_class.m_level < ItemClass.Level.Level4 -> Police Station
            if (policeStationAI is null || policeStationAI.m_info.m_class.m_level < ItemClass.Level.Level4)
                continue;

            // PoliceStationAI.GetLocalizedStats
            uint num = building.m_citizenUnits;
            int cellsInUse = 0;
            while (num != 0)
            {
                uint nextUnit = _citizenManager.m_units.m_buffer[num].m_nextUnit;
                if ((_citizenManager.m_units.m_buffer[num].m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        uint citizen = _citizenManager.m_units.m_buffer[num].GetCitizen(j);
                        if (citizen != 0 && _citizenManager.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Visit)
                        {
                            cellsInUse++;
                        }
                    }
                }

                num = nextUnit;
            }

            int budget = _economyManager.GetBudget(policeStationAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int policeCars = ((productionRate * policeStationAI.PoliceCarCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.CriminalMove, ref count, ref cargo, ref capacity, ref outside);

            prisonVehiclesTotal += policeCars;
            prisonVehiclesInUse += count;
        }

        return GetUsagePercent(prisonVehiclesTotal, prisonVehiclesInUse);
    }

    public int? GetTaxisPercent()
    {
        var taxisTotal = 0;
        var taxisInUse = 0;

        var publicTransportBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.PublicTransport);
        for (int i = 0; i < publicTransportBuildingIds.m_size; i++)
        {
            var buildingId = publicTransportBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var depotAI = building.Info.GetAI() as DepotAI;
            if (
                depotAI is null
                || depotAI.m_maxVehicleCount == 0
                || depotAI.m_transportInfo is null
                || depotAI.m_transportInfo.m_transportType != TransportInfo.TransportType.Taxi)
            {
                continue;
            }

            int budget = _economyManager.GetBudget(depotAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int taxiCount = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Taxi, ref taxiCount, ref cargo, ref capacity, ref outside);

            taxisTotal += ((productionRate * depotAI.m_maxVehicleCount) + 99) / 100;
            taxisInUse += taxiCount;
        }

        return GetUsagePercent(taxisTotal, taxisInUse);
    }

    public int? GetPostVansPercent()
    {
        var postVansTotal = 0;
        var postVansInUse = 0;

        var publicTransportBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.PublicTransport);
        for (int i = 0; i < publicTransportBuildingIds.m_size; i++)
        {
            var buildingId = publicTransportBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var postOfficeAI = building.Info.GetAI() as PostOfficeAI;
            if (postOfficeAI is null)
                continue;

            int budget = _economyManager.GetBudget(postOfficeAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int unsortedMail = 0;
            int sortedMail = 0;
            int unsortedCapacity = 0;
            int sortedCapacity = 0;
            int ownVanCount = 0;
            int ownTruckCount = 0;
            int import = 0;
            int export = 0;
            GameMethods.CalculateVehicles(buildingId, ref building, ref unsortedMail, ref sortedMail, ref unsortedCapacity, ref sortedCapacity, ref ownVanCount, ref ownTruckCount, ref import, ref export);

            // TODO: mail and stuff
            int num = building.m_customBuffer1 * 1000;
            int num2 = building.m_customBuffer2 * 1000;

            postVansTotal += ((productionRate * postOfficeAI.m_postVanCount) + 99) / 100;
            postVansInUse += ownVanCount;
        }

        return GetUsagePercent(postVansTotal, postVansInUse);
    }

    public int? GetPostTrucksPercent()
    {
        var postTrucksTotal = 0;
        var postTrucksInUse = 0;

        var publicTransportBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.PublicTransport);
        for (int i = 0; i < publicTransportBuildingIds.m_size; i++)
        {
            var buildingId = publicTransportBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var postOfficeAI = building.Info.GetAI() as PostOfficeAI;
            if (postOfficeAI is null)
                continue;

            int budget = _economyManager.GetBudget(postOfficeAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int unsortedMail = 0;
            int sortedMail = 0;
            int unsortedCapacity = 0;
            int sortedCapacity = 0;
            int ownVanCount = 0;
            int ownTruckCount = 0;
            int import = 0;
            int export = 0;
            GameMethods.CalculateVehicles(buildingId, ref building, ref unsortedMail, ref sortedMail, ref unsortedCapacity, ref sortedCapacity, ref ownVanCount, ref ownTruckCount, ref import, ref export);

            // TODO: mail and stuff
            int num = building.m_customBuffer1 * 1000;
            int num2 = building.m_customBuffer2 * 1000;

            postTrucksTotal += ((productionRate * postOfficeAI.m_postTruckCount) + 99) / 100;
            postTrucksInUse += ownTruckCount;
        }

        return GetUsagePercent(postTrucksTotal, postTrucksInUse);
    }

    public int? GetRoadMaintenanceVehiclesPercent()
    {
        var roadMaintenanceVehiclesTotal = 0;
        var roadMaintenanceVehiclesInUse = 0;

        var roadMaintenanceBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Road);

        for (int i = 0; i < roadMaintenanceBuildingIds.m_size; i++)
        {
            var buildingId = roadMaintenanceBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var maintenanceDepotAI = building.Info.GetAI() as MaintenanceDepotAI;
            if (maintenanceDepotAI is null)
                continue;

            int budget = _economyManager.GetBudget(maintenanceDepotAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int trucks = ((productionRate * maintenanceDepotAI.m_maintenanceTruckCount) + 99) / 100;
            int truckCount = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.RoadMaintenance, ref truckCount, ref cargo, ref capacity, ref outside);

            roadMaintenanceVehiclesTotal += trucks;
            roadMaintenanceVehiclesInUse += truckCount;
        }

        return GetUsagePercent(roadMaintenanceVehiclesTotal, roadMaintenanceVehiclesInUse);
    }

    public int? GetSnowDumpPercent()
    {
        var snowDumpStorageTotal = 0;
        var snowDumpStorageInUse = 0;

        var roadMaintenanceBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Road);

        for (int i = 0; i < roadMaintenanceBuildingIds.m_size; i++)
        {
            var buildingId = roadMaintenanceBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var snowDumpAI = building.Info.GetAI() as SnowDumpAI;
            if (snowDumpAI is null)
                continue;

            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Snow, ref count, ref cargo, ref capacity, ref outside);
            }
            else
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.SnowMove, ref count, ref cargo, ref capacity, ref outside);
            }

            snowDumpStorageTotal += snowDumpAI.m_snowCapacity;
            snowDumpStorageInUse += snowDumpAI.GetSnowAmount(buildingId, ref building);
        }

        return GetUsagePercent(snowDumpStorageTotal, snowDumpStorageInUse);
    }

    public int? GetSnowDumpVehiclesPercent()
    {
        var snowDumpVehiclesTotal = 0;
        var snowDumpVehiclesInUse = 0;

        var roadMaintenanceBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Road);

        for (int i = 0; i < roadMaintenanceBuildingIds.m_size; i++)
        {
            var buildingId = roadMaintenanceBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var snowDumpAI = building.Info.GetAI() as SnowDumpAI;
            if (snowDumpAI is null)
                continue;

            int budget = _economyManager.GetBudget(snowDumpAI.m_info.m_class);
            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            int snowTrucks = ((productionRate * snowDumpAI.m_snowTruckCount) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            if ((building.m_flags & Building.Flags.Downgrading) == Building.Flags.None)
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Snow, ref count, ref cargo, ref capacity, ref outside);
            }
            else
            {
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.SnowMove, ref count, ref cargo, ref capacity, ref outside);
            }

            snowDumpVehiclesTotal += snowTrucks;
            snowDumpVehiclesInUse += count;
        }

        return GetUsagePercent(snowDumpVehiclesTotal, snowDumpVehiclesInUse);
    }

    public int? GetSewageTreatmentPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetSewageCapacity(), globalDistrict.GetSewageAccumulation());
    }

    public int? GetTrafficJamPercent()
    {
        return (int)(100 - (float)_vehicleManager.m_lastTrafficFlow);
    }

    public int? GetUnemploymentPercent()
    {
        return _districtManager.m_districts.m_buffer[0].GetUnemployment();
    }

    public int? GetUnhappinessCommercialPercent()
    {
        return (int)(100 - (float)_districtManager.m_districts.m_buffer[0].m_commercialData.m_finalHappiness);
    }

    public int? GetUnhappinessIndustrialPercent()
    {
        return (int)(100 - (float)_districtManager.m_districts.m_buffer[0].m_industrialData.m_finalHappiness);
    }

    public int? GetUnhappinessOfficePercent()
    {
        return (int)(100 - (float)_districtManager.m_districts.m_buffer[0].m_officeData.m_finalHappiness);
    }

    public int? GetUnhappinessResidentialPercent()
    {
        return (int)(100 - (float)_districtManager.m_districts.m_buffer[0].m_residentialData.m_finalHappiness);
    }

    public int? GetUniversityPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetEducation3Capacity(), globalDistrict.GetEducation3Need());
    }

    public int? GetWaterPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetUsagePercent(globalDistrict.GetWaterCapacity(), globalDistrict.GetWaterConsumption());
    }

    public int? GetWaterPumpingServiceStoragePercent()
    {
        long waterSewageStorageTotal = 0;
        long waterSewageStorageInUse = 0;

        var waterBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Water);

        for (int i = 0; i < waterBuildingIds.m_size; i++)
        {
            var buildingId = waterBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var waterFacilityAI = building.Info.GetAI() as WaterFacilityAI;
            if (waterFacilityAI is null)
                continue;

            // WaterFacilityAI.GetLocalizedStats
            if (waterFacilityAI.m_waterIntake != 0 && waterFacilityAI.m_waterOutlet != 0 && waterFacilityAI.m_waterStorage != 0)
                continue;

            if (waterFacilityAI.m_sewageOutlet == 0 || waterFacilityAI.m_sewageStorage == 0 || waterFacilityAI.m_pumpingVehicles == 0)
                continue;

            waterSewageStorageInUse += (building.m_customBuffer2 * 1000) + building.m_sewageBuffer;
            waterSewageStorageTotal += waterFacilityAI.m_sewageStorage;

            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.FloodWater, ref count, ref cargo, ref capacity, ref outside);
        }

        return GetUsagePercent(waterSewageStorageTotal, waterSewageStorageInUse);
    }

    public int? GetWaterPumpingServiceVehiclesPercent()
    {
        int pumpingVehiclesTotal = 0;
        int pumpingVehiclesInUse = 0;

        var waterBuildingIds = _buildingManager.GetServiceBuildings(ItemClass.Service.Water);

        for (int i = 0; i < waterBuildingIds.m_size; i++)
        {
            var buildingId = waterBuildingIds[i];
            var building = _buildingManager.m_buildings.m_buffer[buildingId];
            var buildingInfo = building.Info;
            if (buildingInfo is null)
                continue;

            var waterFacilityAI = building.Info.GetAI() as WaterFacilityAI;
            if (waterFacilityAI is null)
                continue;

            // WaterFacilityAI.GetLocalizedStats
            if (waterFacilityAI.m_waterIntake != 0 && waterFacilityAI.m_waterOutlet != 0 && waterFacilityAI.m_waterStorage != 0)
                continue;

            if (waterFacilityAI.m_sewageOutlet == 0 || waterFacilityAI.m_sewageStorage == 0 || waterFacilityAI.m_pumpingVehicles == 0)
                continue;

            var budget = _economyManager.GetBudget(building.Info.m_class);
            var productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
            var pumpingVehicles = ((productionRate * waterFacilityAI.m_pumpingVehicles) + 99) / 100;
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.FloodWater, ref count, ref cargo, ref capacity, ref outside);

            pumpingVehiclesTotal += pumpingVehicles;
            pumpingVehiclesInUse += count;
        }

        return GetUsagePercent(pumpingVehiclesTotal, pumpingVehiclesInUse);
    }

    public int? GetWaterReserveTanksPercent()
    {
        ref District globalDistrict = ref _districtManager.m_districts.m_buffer[0];

        return GetAvailabilityPercent(globalDistrict.GetWaterStorageCapacity(), globalDistrict.GetWaterStorageAmount());
    }

    private static int? GetAvailabilityPercent(long capacity, long need)
    {
        if (capacity == 0)
            return null;

        if (need == 0)
            return 0;

        return (int)((1 - (need / (float)capacity)) * 100);
    }

    private static int? GetUsagePercent(long capacity, long usage)
    {
        if (capacity == 0)
            return null;

        if (usage == 0)
            return 0;

        return (int)(usage / (float)capacity * 100f);
    }
}
