using System;
using UnityEngine;

namespace Stats
{
    public class GameEngineService
    {
        private readonly DistrictManager districtManager;
        private readonly BuildingManager buildingManager;
        private readonly EconomyManager economyManager;
        private readonly ImmaterialResourceManager immaterialResourceManager;
        private readonly CitizenManager citizenManager;
        private readonly VehicleManager vehicleManager;

        private readonly District allDistricts;

        public GameEngineService(
            DistrictManager districtManager,
            BuildingManager buildingManager,
            EconomyManager economyManager,
            ImmaterialResourceManager immaterialResourceManager,
            CitizenManager citizenManager,
            VehicleManager vehicleManager)
        {
            this.districtManager = districtManager ?? throw new ArgumentNullException(nameof(districtManager));
            this.buildingManager = buildingManager ?? throw new ArgumentNullException(nameof(buildingManager));
            this.economyManager = economyManager ?? throw new ArgumentNullException(nameof(economyManager));
            this.immaterialResourceManager = immaterialResourceManager ?? throw new ArgumentNullException(nameof(immaterialResourceManager));
            this.citizenManager = citizenManager ?? throw new ArgumentNullException(nameof(citizenManager));
            this.vehicleManager = vehicleManager ?? throw new ArgumentNullException(nameof(vehicleManager));

            this.allDistricts = districtManager.m_districts.m_buffer[0];
        }

        public int? GetAverageIllnessRate()
        {
            return this.allDistricts.GetSickCount() == 0
                ? null
                : (int?)(int)(100 - (float)this.allDistricts.m_residentialData.m_finalHealth);
        }

        public int? GetCemeteryPercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetDeadCapacity(),
                this.allDistricts.GetDeadAmount()
            );
        }

        public HealthCareVehiclesPercent GetHealthCareVehiclesPercent()
        {
            var healthcareVehiclesTotal = 0;
            var healthcareVehiclesInUse = 0;

            var medicalHelicoptersTotal = 0;
            var medicalHelicoptersInUse = 0;

            var cemeteryVehiclesTotal = 0;
            var cemeteryVehiclesInUse = 0;

            var crematoriumVehiclesTotal = 0;
            var crematoriumVehiclesInUse = 0;

            var healthcareBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.HealthCare);

            for (int i = 0; i < healthcareBuildingIds.m_size; i++)
            {
                var buildingId = healthcareBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI();
                switch (buildingAi)
                {
                    case HospitalAI hospitalAI:
                        {
                            int budget = economyManager.GetBudget(hospitalAI.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int healthcareVehicles = (productionRate * hospitalAI.AmbulanceCount + 99) / 100;
                            int count = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Sick, ref count, ref cargo, ref capacity, ref outside);

                            healthcareVehiclesTotal += healthcareVehicles;
                            healthcareVehiclesInUse += count;

                            break;
                        }
                    case HelicopterDepotAI helicopterDepotAI:
                        {
                            int budget = economyManager.GetBudget(helicopterDepotAI.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int medicalHelicopters = (productionRate * helicopterDepotAI.m_helicopterCount + 99) / 100;
                            int count = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Sick2, ref count, ref cargo, ref capacity, ref outside);

                            medicalHelicoptersTotal += medicalHelicopters;
                            medicalHelicoptersInUse += count;

                            break;
                        }
                    case CemeteryAI cemeteryAI:
                        {
                            int budget = economyManager.GetBudget(cemeteryAI.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);

                            int cemeteryVehicles = (productionRate * cemeteryAI.m_hearseCount + 99) / 100;
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

                            if (cemeteryAI.m_graveCount == 0) //crematory
                            {
                                crematoriumVehiclesTotal += cemeteryVehicles;
                                crematoriumVehiclesInUse += count;
                            }
                            else //cemetery
                            {
                                cemeteryVehiclesTotal += cemeteryVehicles;
                                cemeteryVehiclesInUse += count;
                            }

                            break;
                        }
                    default:
                        continue;
                }
            }

            return new HealthCareVehiclesPercent(
                GetUsagePercent(healthcareVehiclesTotal, healthcareVehiclesInUse),
                GetUsagePercent(medicalHelicoptersTotal, medicalHelicoptersInUse),
                GetUsagePercent(cemeteryVehiclesTotal, cemeteryVehiclesInUse),
                GetUsagePercent(crematoriumVehiclesTotal, crematoriumVehiclesInUse)
            );
        }

        public int? GetCityUnattractivenessPercent()
        {
            immaterialResourceManager.CheckGlobalResource(ImmaterialResourceManager.Resource.Attractiveness, out int cityAttractivenessRaw);
            immaterialResourceManager.CheckTotalResource(ImmaterialResourceManager.Resource.LandValue, out int landValueRaw);
            var cityAttractivenessAndLandValue = cityAttractivenessRaw + landValueRaw;
            var cityAttractiveness = 100 * cityAttractivenessAndLandValue / Mathf.Max(cityAttractivenessAndLandValue + 200, 200);

            return 100 - cityAttractiveness;
        }

        public int? GetCrematoriumPercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetCremateCapacity(),
                this.allDistricts.GetDeadCount()
            );
        }

        public int? GetCrimePercent()
        {
            return this.allDistricts.m_finalCrimeRate;
        }

        public DisasterResponseVehiclesPercent GetDisasterResponseVehiclesPercent()
        {
            var disasterResponseVehiclesTotal = 0;
            var disasterResponseVehiclesInUse = 0;

            var disasterResponseHelicoptersTotal = 0;
            var disasterResponseHelicoptersInUse = 0;

            var disasterBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Disaster);
            for (int i = 0; i < disasterBuildingIds.m_size; i++)
            {
                var buildingId = disasterBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI();
                if (buildingAi is DisasterResponseBuildingAI disasterResponseBuildingAi)
                {
                    int budget = economyManager.GetBudget(disasterResponseBuildingAi.m_info.m_class);
                    int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);

                    disasterResponseVehiclesTotal += (productionRate * disasterResponseBuildingAi.m_vehicleCount + 99) / 100;
                    int disasterVehicles = 0;
                    int cargo = 0;
                    int capacity = 0;
                    int outside = 0;
                    GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Collapsed, ref disasterVehicles, ref cargo, ref capacity, ref outside);
                    disasterResponseVehiclesInUse += disasterVehicles;

                    disasterResponseHelicoptersTotal += (productionRate * disasterResponseBuildingAi.m_helicopterCount + 99) / 100;
                    int disasterHelicopters = 0;
                    int cargo2 = 0;
                    int capacity2 = 0;
                    int outside2 = 0;
                    GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Collapsed2, ref disasterHelicopters, ref cargo2, ref capacity2, ref outside2);
                    disasterResponseHelicoptersInUse += disasterHelicopters;
                }
            }

            return new DisasterResponseVehiclesPercent(
                GetUsagePercent(disasterResponseVehiclesTotal, disasterResponseVehiclesInUse),
                GetUsagePercent(disasterResponseHelicoptersTotal, disasterResponseHelicoptersInUse)
            );
        }

        public int? GetDrinkingWaterPollutionPercent()
        {
            return this.allDistricts.GetWaterPollution();
        }

        public int? GetElectricityPercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetElectricityCapacity(),
                this.allDistricts.GetElectricityConsumption()
            );
        }

        public int? GetElementarySchoolPercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetEducation1Capacity(),
                this.allDistricts.GetEducation1Need()
            );
        }

        public FireDepartmentVehiclesPercent GetFireDepartmentVehiclesPercent()
        {
            var fireDepartmentVehiclesTotal = 0;
            var fireDepartmentVehiclesInUse = 0;

            var fireHelicoptersTotal = 0;
            var fireHelicoptersInUse = 0;

            var fireDepartmentBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.FireDepartment);

            for (int i = 0; i < fireDepartmentBuildingIds.m_size; i++)
            {
                var buildingId = fireDepartmentBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI();
                switch (buildingAi)
                {
                    case FireStationAI fireStationAI:
                        {
                            int budget = economyManager.GetBudget(fireStationAI.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int fireTrucks = (productionRate * fireStationAI.m_fireTruckCount + 99) / 100;
                            int count = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Fire, ref count, ref cargo, ref capacity, ref outside);

                            fireDepartmentVehiclesTotal += fireTrucks;
                            fireDepartmentVehiclesInUse += count;
                        }

                        break;
                    case HelicopterDepotAI helicopterDepotAI:
                        {
                            int budget = economyManager.GetBudget(helicopterDepotAI.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int fireHelicopters = (productionRate * helicopterDepotAI.m_helicopterCount + 99) / 100;
                            int count = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.ForestFire, ref count, ref cargo, ref capacity, ref outside);
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Fire2, ref count, ref cargo, ref capacity, ref outside);

                            fireHelicoptersTotal += fireHelicopters;
                            fireHelicoptersInUse += count;
                        }

                        break;
                    default:
                        continue;
                }
            }

            return new FireDepartmentVehiclesPercent(
                GetUsagePercent(fireDepartmentVehiclesTotal, fireDepartmentVehiclesInUse),
                GetUsagePercent(fireHelicoptersTotal, fireHelicoptersInUse)
            );
        }

        public int? GetFireHazardPercent()
        {
            immaterialResourceManager.CheckTotalResource(ImmaterialResourceManager.Resource.FireHazard, out int fireHazard);

            return fireHazard;
        }

        public int? GetGarbageProcessingPercent()
        {
            var incineratorCapacity = this.allDistricts.GetIncinerationCapacity();
            var incineratorAccumulation = this.allDistricts.GetGarbageAccumulation();

            return GetUsagePercent(incineratorCapacity, incineratorAccumulation);
        }

        public GarbageVehiclesPercent GetGarbageVehiclesPercent()
        {
            var garbageProcessingVehiclesTotal = 0;
            var garbageProcessingVehiclesInUse = 0;

            var landfillVehiclesTotal = 0;
            var landfillVehiclesInUse = 0;

            var garbageBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Garbage);
            for (int i = 0; i < garbageBuildingIds.m_size; i++)
            {
                var buildingId = garbageBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI() as LandfillSiteAI;
                if (buildingAi == null)
                {
                    continue;
                }

                int budget = economyManager.GetBudget(buildingAi.m_info.m_class);
                int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                int garbageTruckVehicles = (productionRate * buildingAi.m_garbageTruckCount + 99) / 100;
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

                if (buildingAi.m_garbageConsumption <= 0)
                {
                    landfillVehiclesTotal += garbageTruckVehicles;
                    landfillVehiclesInUse += count;
                }
                else
                {
                    garbageProcessingVehiclesTotal += garbageTruckVehicles;
                    garbageProcessingVehiclesInUse += count;
                }
            }

            return new GarbageVehiclesPercent(
                GetUsagePercent(garbageProcessingVehiclesTotal, garbageProcessingVehiclesInUse),
                GetUsagePercent(landfillVehiclesTotal, landfillVehiclesInUse)
            );
        }

        public int? GetGroundPollutionPercent()
        {
            return this.allDistricts.GetGroundPollution();
        }

        public int? GetHealthCarePercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetHealCapacity(),
                this.allDistricts.GetSickCount()
            );
        }

        public int? GetHeatingPercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetHeatingCapacity(),
                this.allDistricts.GetHeatingConsumption()
            );
        }

        public int? GetHighSchoolPercent()
        {
            var highSchoolCapacity = this.allDistricts.GetEducation2Capacity();
            var highSchoolUsage = this.allDistricts.GetEducation2Need();
            return GetUsagePercent(highSchoolCapacity, highSchoolUsage);
        }

        public int? GetLandfillPercent()
        {
            var garbageCapacity = this.allDistricts.GetGarbageCapacity();
            var garbageAmout = this.allDistricts.GetGarbageAmount();

            return GetUsagePercent(garbageCapacity, garbageAmout);
        }

        public int? GetNoisePollutionPercent()
        {
            immaterialResourceManager.CheckTotalResource(ImmaterialResourceManager.Resource.NoisePollution, out int noisePollution);
            return noisePollution;
        }

        public int? GetParkMaintenanceVehiclesPercent()
        {
            var parkMaintenanceVehiclesTotal = 0;
            var parkMaintenanceVehiclesInUse = 0;

            var beautificationBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Beautification);

            for (int i = 0; i < beautificationBuildingIds.m_size; i++)
            {
                var buildingId = beautificationBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI();
                if (buildingAi is MaintenanceDepotAI maintenanceDepotAi)
                {
                    var transferReason = GameMethods.GetTransferReason(maintenanceDepotAi);
                    if (transferReason == TransferManager.TransferReason.None)
                    {
                        continue;
                    }

                    int budget = economyManager.GetBudget(maintenanceDepotAi.m_info.m_class);
                    int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                    if (transferReason == TransferManager.TransferReason.ParkMaintenance)
                    {
                        byte district = districtManager.GetDistrict(building.m_position);
                        DistrictPolicies.Services servicePolicies = districtManager.m_districts.m_buffer[(int)district].m_servicePolicies;
                        if ((servicePolicies & DistrictPolicies.Services.ParkMaintenanceBoost) != DistrictPolicies.Services.None)
                        {
                            productionRate *= 2;
                        }
                    }
                    int trucks = (productionRate * maintenanceDepotAi.m_maintenanceTruckCount + 99) / 100;
                    int truckCount = 0;
                    int cargo = 0;
                    int capacity = 0;
                    int outside = 0;
                    GameMethods.CalculateOwnVehicles(buildingId, ref building, transferReason, ref truckCount, ref cargo, ref capacity, ref outside);

                    parkMaintenanceVehiclesTotal += trucks;
                    parkMaintenanceVehiclesInUse += truckCount;
                }
            }

            return GetUsagePercent(parkMaintenanceVehiclesTotal, parkMaintenanceVehiclesInUse);
        }

        public PoliceDepartmentVehiclesPercent GetPoliceDepartmentVehiclesPercent()
        {
            var policeHoldingCellsTotal = 0;
            var policeHoldingCellsInUse = 0;

            var policeVehiclesTotal = 0;
            var policeVehiclesInUse = 0;

            var policeHelicoptersTotal = 0;
            var policeHelicoptersInUse = 0;

            var prisonCellsTotal = 0;
            var prisonCellsInUse = 0;

            var prisonVehiclesTotal = 0;
            var prisonVehiclesInUse = 0;

            var policeBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.PoliceDepartment);

            for (int i = 0; i < policeBuildingIds.m_size; i++)
            {
                var buildingId = policeBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI();
                if (buildingAi == null)
                {
                    continue;
                }

                switch (buildingAi)
                {
                    case PoliceStationAI policeStationAi:
                        {
                            //PoliceStationAI.GetLocalizedStats
                            uint num = building.m_citizenUnits;
                            int cellsInUse = 0;
                            while (num != 0)
                            {
                                uint nextUnit = citizenManager.m_units.m_buffer[num].m_nextUnit;
                                if ((citizenManager.m_units.m_buffer[num].m_flags & CitizenUnit.Flags.Visit) != 0)
                                {
                                    for (int j = 0; j < 5; j++)
                                    {
                                        uint citizen = citizenManager.m_units.m_buffer[num].GetCitizen(j);
                                        if (citizen != 0 && citizenManager.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Visit)
                                        {
                                            cellsInUse++;
                                        }
                                    }
                                }
                                num = nextUnit;
                            }

                            int budget = economyManager.GetBudget(policeStationAi.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int policeCars = (productionRate * policeStationAi.PoliceCarCount + 99) / 100;
                            int count = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            if (policeStationAi.m_info.m_class.m_level < ItemClass.Level.Level4)
                            {
                                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Crime, ref count, ref cargo, ref capacity, ref outside);

                                policeHoldingCellsInUse += cellsInUse;
                                policeHoldingCellsTotal += policeStationAi.JailCapacity;

                                policeVehiclesTotal += policeCars;
                                policeVehiclesInUse += count;
                            }
                            else
                            {
                                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.CriminalMove, ref count, ref cargo, ref capacity, ref outside);

                                prisonCellsTotal += policeStationAi.JailCapacity;
                                prisonCellsInUse += cellsInUse;

                                prisonVehiclesTotal += policeCars;
                                prisonVehiclesInUse += count;
                            }
                        }

                        break;
                    case HelicopterDepotAI helicopterDepotAI:
                        {
                            int budget = economyManager.GetBudget(helicopterDepotAI.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int policeHelicopters = (productionRate * helicopterDepotAI.m_helicopterCount + 99) / 100;
                            int count = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Crime, ref count, ref cargo, ref capacity, ref outside);

                            policeHelicoptersTotal += policeHelicopters;
                            policeHelicoptersInUse += count;
                        }

                        break;
                    default:
                        continue;
                }
            }

            return new PoliceDepartmentVehiclesPercent(
                GetUsagePercent(policeHoldingCellsTotal, policeHoldingCellsInUse),
                GetUsagePercent(policeVehiclesTotal, policeVehiclesInUse),
                GetUsagePercent(policeHelicoptersTotal, policeHelicoptersInUse),
                GetUsagePercent(prisonCellsTotal, prisonCellsInUse),
                GetUsagePercent(prisonVehiclesTotal, prisonVehiclesInUse)
            );
        }

        public PostAndTaxiVehiclesPercent GetPostAndTaxiVehiclesPercent()
        {
            var taxisTotal = 0;
            var taxisInUse = 0;

            var postVansTotal = 0;
            var postVansInUse = 0;

            var postTrucksTotal = 0;
            var postTrucksInUse = 0;

            var publicTransportBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.PublicTransport);
            for (int i = 0; i < publicTransportBuildingIds.m_size; i++)
            {
                var buildingId = publicTransportBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI();
                switch (buildingAi)
                {
                    case DepotAI depotAi when
                        depotAi.m_transportInfo != null
                        && depotAi.m_maxVehicleCount != 0
                        && depotAi.m_transportInfo.m_transportType == TransportInfo.TransportType.Taxi:
                        {
                            int budget = economyManager.GetBudget(depotAi.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int taxiCount = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Taxi, ref taxiCount, ref cargo, ref capacity, ref outside);

                            taxisTotal += (productionRate * depotAi.m_maxVehicleCount + 99) / 100;
                            taxisInUse += taxiCount;

                            break;
                        }
                    case PostOfficeAI postOfficeAi:
                        {
                            int budget = economyManager.GetBudget(postOfficeAi.m_info.m_class);
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

                            //TODO: mail and stuff
                            int num = building.m_customBuffer1 * 1000;
                            int num2 = building.m_customBuffer2 * 1000;

                            postVansTotal += (productionRate * postOfficeAi.m_postVanCount + 99) / 100;
                            postVansInUse += ownVanCount;

                            postTrucksTotal += (productionRate * postOfficeAi.m_postTruckCount + 99) / 100;
                            postTrucksInUse += ownTruckCount;

                            break;
                        }
                    default:
                        continue;
                }
            }

            return new PostAndTaxiVehiclesPercent(
                GetUsagePercent(taxisTotal, taxisInUse),
                GetUsagePercent(postVansTotal, postVansInUse),
                GetUsagePercent(postTrucksTotal, postTrucksInUse)
            );
        }

        public RoadMaintenanceAndSnowDumpVehiclesPercent GetRoadMaintenanceAndSnowDumpVehiclesPercent()
        {
            var roadMaintenanceVehiclesTotal = 0;
            var roadMaintenanceVehiclesInUse = 0;

            var snowDumpStorageTotal = 0;
            var snowDumpStorageInUse = 0;

            var snowDumpVehiclesTotal = 0;
            var snowDumpVehiclesInUse = 0;

            var roadMaintenanceBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Road);

            for (int i = 0; i < roadMaintenanceBuildingIds.m_size; i++)
            {
                var buildingId = roadMaintenanceBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI();
                switch (buildingAi)
                {
                    case MaintenanceDepotAI maintenanceDepotAi:
                        {
                            int budget = economyManager.GetBudget(maintenanceDepotAi.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int trucks = (productionRate * maintenanceDepotAi.m_maintenanceTruckCount + 99) / 100;
                            int truckCount = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.RoadMaintenance, ref truckCount, ref cargo, ref capacity, ref outside);

                            roadMaintenanceVehiclesTotal += trucks;
                            roadMaintenanceVehiclesInUse += truckCount;
                        }

                        break;
                    case SnowDumpAI snowDumpAi:
                        {
                            int budget = economyManager.GetBudget(snowDumpAi.m_info.m_class);
                            int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                            int snowTrucks = (productionRate * snowDumpAi.m_snowTruckCount + 99) / 100;
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

                            snowDumpStorageTotal += snowDumpAi.m_snowCapacity;
                            snowDumpStorageInUse += snowDumpAi.GetSnowAmount(buildingId, ref building);

                            snowDumpVehiclesTotal += snowTrucks;
                            snowDumpVehiclesInUse += count;
                        }

                        break;
                    default:
                        continue;
                }
            }

            return new RoadMaintenanceAndSnowDumpVehiclesPercent(
                GetUsagePercent(roadMaintenanceVehiclesTotal, roadMaintenanceVehiclesInUse),
                GetUsagePercent(snowDumpStorageTotal, snowDumpStorageInUse),
                GetUsagePercent(snowDumpVehiclesTotal, snowDumpVehiclesInUse)
            );
        }

        public int? GetSewageTreatmentPercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetSewageCapacity(),
                this.allDistricts.GetSewageAccumulation()
            );
        }

        public int? GetTrafficJamPercent()
        {
            return (int)(100 - (float)vehicleManager.m_lastTrafficFlow);
        }

        public int? GetUnemploymentPercent()
        {
            return this.allDistricts.GetUnemployment();
        }

        public int? GetUniversityPercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetEducation3Capacity(),
                this.allDistricts.GetEducation3Need()
            );
        }

        public int? GetWaterPercent()
        {
            return GetUsagePercent(
                this.allDistricts.GetWaterCapacity(),
                this.allDistricts.GetWaterConsumption()
            );
        }

        public WaterPumpingServiceVehiclesPercent GetWaterPumpingServiceVehiclesPercent()
        {
            long waterSewageStorageTotal = 0;
            long waterSewageStorageInUse = 0;

            int pumpingVehiclesTotal = 0;
            int pumpingVehiclesInUse = 0;

            var waterBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Water);

            for (int i = 0; i < waterBuildingIds.m_size; i++)
            {
                var buildingId = waterBuildingIds[i];
                var building = buildingManager.m_buildings.m_buffer[buildingId];
                var buildingAi = building.Info?.GetAI() as WaterFacilityAI;
                if (buildingAi == null)
                {
                    continue;
                }

                //WaterFacilityAI.GetLocalizedStats
                if (buildingAi.m_waterIntake != 0 && buildingAi.m_waterOutlet != 0 && buildingAi.m_waterStorage != 0)
                {
                    continue;
                }

                if (buildingAi.m_sewageOutlet == 0 || buildingAi.m_sewageStorage == 0 || buildingAi.m_pumpingVehicles == 0)
                {
                    continue;
                }

                waterSewageStorageInUse += (building.m_customBuffer2 * 1000 + building.m_sewageBuffer);
                waterSewageStorageTotal += buildingAi.m_sewageStorage;

                var budget = economyManager.GetBudget(building.Info.m_class);
                var productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                var pumpingVehicles = (productionRate * buildingAi.m_pumpingVehicles + 99) / 100;
                int count = 0;
                int cargo = 0;
                int capacity = 0;
                int outside = 0;
                GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.FloodWater, ref count, ref cargo, ref capacity, ref outside);

                pumpingVehiclesTotal += pumpingVehicles;
                pumpingVehiclesInUse += count;
            }

            return new WaterPumpingServiceVehiclesPercent(
                GetUsagePercent(pumpingVehiclesTotal, pumpingVehiclesInUse),
                GetUsagePercent(waterSewageStorageTotal, waterSewageStorageInUse)
            );
        }

        public int? GetWaterReservePercent()
        {
            return GetAvailabilityPercent(
                this.allDistricts.GetWaterStorageCapacity(),
                this.allDistricts.GetWaterStorageAmount()
            );
        }

        public bool CheckIfMapHasSnowDumps()
        {
            var result = false;

            var loadedCount = PrefabCollection<BuildingInfo>.LoadedCount();
            for (uint i = 0; i < loadedCount; i++)
            {
                var bi = PrefabCollection<BuildingInfo>.GetLoaded(i);
                if (bi != null && bi.m_buildingAI is SnowDumpAI)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private int? GetAvailabilityPercent(long capacity, long need)
        {
            if (capacity == 0)
                return null;

            if (need == 0)
                return 0;

            return (int)((1 - need / (float)capacity) * 100);
        }

        private int? GetUsagePercent(long capacity, long usage)
        {
            if (capacity == 0)
                return null;

            if (usage == 0)
                return 0;

            return (int)(usage / (float)capacity * 100f);
        }
    }
}
