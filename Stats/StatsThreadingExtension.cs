using ICities;
using Stats.Config;
using Stats.Model;
using System;
using System.Collections;
using UnityEngine;

namespace Stats
{
    public class StatsThreadingExtension : MonoBehaviour
    {
        private Configuration configuration;
        private ItemsInIndexOrder itemsInIndexOrder;
        private GameEngineService gameEngineService;
        private bool mapHasSnowDumps;

        public void Initialize(
            Configuration configuration,
            ItemsInIndexOrder itemsInIndexOrder,
            GameEngineService gameEngineService,
            bool mapHasSnowDumps)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (itemsInIndexOrder is null)
            {
                throw new ArgumentNullException(nameof(itemsInIndexOrder));
            }

            if (gameEngineService is null)
            {
                throw new ArgumentNullException(nameof(gameEngineService));
            }

            this.configuration = configuration;
            this.itemsInIndexOrder = itemsInIndexOrder;
            this.gameEngineService = gameEngineService;
            this.mapHasSnowDumps = mapHasSnowDumps;

            this.StartCoroutine(KeepUpdatingUICoroutine());
        }

        private IEnumerator KeepUpdatingUICoroutine()
        {
            while (true)
            {
                yield return StartCoroutine(UpdateUICoroutine());
            }
        }

        private IEnumerator UpdateUICoroutine()
        {
            if (this.configuration.GetConfigurationItemData(ItemData.AverageIllnessRate).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.AverageIllnessRate.Index].Percent =
                    this.gameEngineService.GetAverageIllnessRate();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Cemetery).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Cemetery.Index].Percent =
                    this.gameEngineService.GetCemeteryPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.CemeteryVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.CrematoriumVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.HealthcareVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.MedicalHelicopters).enabled)
            {
                var healthCareVehiclesPercent = this.gameEngineService.GetHealthCareVehiclesPercent();

                this.itemsInIndexOrder.Items[ItemData.CemeteryVehicles.Index].Percent =
                    healthCareVehiclesPercent.cemeteryVehiclesPercent;
                this.itemsInIndexOrder.Items[ItemData.CrematoriumVehicles.Index].Percent =
                    healthCareVehiclesPercent.crematoriumVehiclesPercent;
                this.itemsInIndexOrder.Items[ItemData.HealthcareVehicles.Index].Percent =
                    healthCareVehiclesPercent.healthCareVehiclesPercent;
                this.itemsInIndexOrder.Items[ItemData.MedicalHelicopters.Index].Percent =
                    healthCareVehiclesPercent.medicalHelicoptersPercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.CityUnattractiveness).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.CityUnattractiveness.Index].Percent =
                    this.gameEngineService.GetCityUnattractivenessPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Crematorium).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Crematorium.Index].Percent =
                    this.gameEngineService.GetCrematoriumPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.CrimeRate).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.CrimeRate.Index].Percent =
                    this.gameEngineService.GetCrimePercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.DisasterResponseVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.DisasterResponseHelicopters).enabled)
            {
                var disasterReponseVehiclesPercent = this.gameEngineService.GetDisasterResponseVehiclesPercent();

                this.itemsInIndexOrder.Items[ItemData.DisasterResponseVehicles.Index].Percent =
                    disasterReponseVehiclesPercent.disasterResponseVehicles;
                this.itemsInIndexOrder.Items[ItemData.DisasterResponseHelicopters.Index].Percent =
                    disasterReponseVehiclesPercent.disasterResponseHelicopters;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.DrinkingWaterPollution).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.DrinkingWaterPollution.Index].Percent =
                    this.gameEngineService.GetDrinkingWaterPollutionPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Electricity).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Electricity.Index].Percent =
                    this.gameEngineService.GetElectricityPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.ElementarySchool).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.ElementarySchool.Index].Percent =
                    this.gameEngineService.GetElementarySchoolPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.FireDepartmentVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.FireHelicopters).enabled)
            {
                var fireDepartmentVehiclesPercent = this.gameEngineService.GetFireDepartmentVehiclesPercent();

                this.itemsInIndexOrder.Items[ItemData.FireDepartmentVehicles.Index].Percent =
                    fireDepartmentVehiclesPercent.fireDepartmentVehiclesPercent;
                this.itemsInIndexOrder.Items[ItemData.FireHelicopters.Index].Percent =
                    fireDepartmentVehiclesPercent.fireHelicoptersPercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.FireHazard).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.FireHazard.Index].Percent =
                    this.gameEngineService.GetFireHazardPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.GarbageProcessing).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.GarbageProcessing.Index].Percent =
                    this.gameEngineService.GetGarbageProcessingPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.GarbageProcessingVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.LandfillVehicles).enabled)
            {
                var garbageVehiclesPercent = this.gameEngineService.GetGarbageVehiclesPercent();

                this.itemsInIndexOrder.Items[ItemData.GarbageProcessingVehicles.Index].Percent =
                    garbageVehiclesPercent.garbageProcessingVehicles;
                this.itemsInIndexOrder.Items[ItemData.LandfillVehicles.Index].Percent =
                    garbageVehiclesPercent.landfillVehicles;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.GroundPollution).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.GroundPollution.Index].Percent =
                    this.gameEngineService.GetGroundPollutionPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Healthcare).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Healthcare.Index].Percent =
                    this.gameEngineService.GetHealthCarePercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Heating).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Heating.Index].Percent =
                    this.gameEngineService.GetHeatingPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.HighSchool).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.HighSchool.Index].Percent =
                    this.gameEngineService.GetHighSchoolPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Landfill).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Landfill.Index].Percent =
                    this.gameEngineService.GetLandfillPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.NoisePollution).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.NoisePollution.Index].Percent =
                    this.gameEngineService.GetNoisePollutionPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.ParkMaintenanceVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.ParkMaintenanceVehicles.Index].Percent =
                    this.gameEngineService.GetParkMaintenanceVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.PoliceHoldingCells).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PoliceVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PrisonCells).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PrisonVehicles).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PoliceHelicopters).enabled)
            {
                var policeDepartmentVehiclesPercent = this.gameEngineService.GetPoliceDepartmentVehiclesPercent();

                this.itemsInIndexOrder.Items[ItemData.PoliceHelicopters.Index].Percent =
                    policeDepartmentVehiclesPercent.policeHelicoptersPercent;
                this.itemsInIndexOrder.Items[ItemData.PoliceHoldingCells.Index].Percent =
                    policeDepartmentVehiclesPercent.policeHoldingCellsPercent;
                this.itemsInIndexOrder.Items[ItemData.PoliceVehicles.Index].Percent =
                    policeDepartmentVehiclesPercent.policeVehiclesPercent;
                this.itemsInIndexOrder.Items[ItemData.PrisonCells.Index].Percent =
                    policeDepartmentVehiclesPercent.prisonCellsPercent;
                this.itemsInIndexOrder.Items[ItemData.PrisonVehicles.Index].Percent =
                    policeDepartmentVehiclesPercent.prisonVehiclesPercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Taxis).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PostVans).enabled
                || this.configuration.GetConfigurationItemData(ItemData.PostTrucks).enabled
                )
            {
                var postAndTaxiVehiclesPercent = this.gameEngineService.GetPostAndTaxiVehiclesPercent();

                this.itemsInIndexOrder.Items[ItemData.Taxis.Index].Percent =
                    postAndTaxiVehiclesPercent.taxisPercent;
                this.itemsInIndexOrder.Items[ItemData.PostVans.Index].Percent =
                    postAndTaxiVehiclesPercent.postVansPercent;
                this.itemsInIndexOrder.Items[ItemData.PostTrucks.Index].Percent =
                    postAndTaxiVehiclesPercent.postTrucksPercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.RoadMaintenanceVehicles).enabled
                || (this.mapHasSnowDumps && this.configuration.GetConfigurationItemData(ItemData.SnowDump).enabled)
                || (this.mapHasSnowDumps && this.configuration.GetConfigurationItemData(ItemData.SnowDumpVehicles).enabled))
            {
                var roadMaintenanceAndSnowDumpVehiclesPercent =
                    this.gameEngineService.GetRoadMaintenanceAndSnowDumpVehiclesPercent();

                this.itemsInIndexOrder.Items[ItemData.RoadMaintenanceVehicles.Index].Percent =
                    roadMaintenanceAndSnowDumpVehiclesPercent.roadMaintenanceVehiclesPercent;

                if (this.mapHasSnowDumps)
                {
                    this.itemsInIndexOrder.Items[ItemData.SnowDump.Index].Percent =
                        roadMaintenanceAndSnowDumpVehiclesPercent.snowDumpPercent;
                    this.itemsInIndexOrder.Items[ItemData.SnowDumpVehicles.Index].Percent =
                        roadMaintenanceAndSnowDumpVehiclesPercent.snowDumpVehiclesPercent;
                }
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.SewageTreatment).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.SewageTreatment.Index].Percent =
                    this.gameEngineService.GetSewageTreatmentPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.TrafficJam).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.TrafficJam.Index].Percent =
                    this.gameEngineService.GetTrafficJamPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Unemployment).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Unemployment.Index].Percent =
                    this.gameEngineService.GetUnemploymentPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.University).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.University.Index].Percent =
                    this.gameEngineService.GetUniversityPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Water).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Water.Index].Percent =
                    this.gameEngineService.GetWaterPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.WaterPumpingServiceStorage).enabled
                || this.configuration.GetConfigurationItemData(ItemData.WaterPumpingServiceVehicles).enabled)
            {
                var waterPumpingServiceVehiclesPercent =
                    this.gameEngineService.GetWaterPumpingServiceVehiclesPercent();

                this.itemsInIndexOrder.Items[ItemData.WaterPumpingServiceVehicles.Index].Percent =
                    waterPumpingServiceVehiclesPercent.waterPumpingServiceVehiclesPercent;
                this.itemsInIndexOrder.Items[ItemData.WaterPumpingServiceStorage.Index].Percent =
                    waterPumpingServiceVehiclesPercent.waterPumpingServiceStoragePercent;
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.WaterReserveTank).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.WaterReserveTank.Index].Percent =
                    this.gameEngineService.GetWaterReservePercent();
                yield return new WaitForEndOfFrame();
            }

            //wait at least one frame, even if all Items are off.
            yield return new WaitForEndOfFrame();
        }

        public void OnAfterSimulationFrame()
        {
        }

        public void OnAfterSimulationTick()
        {
        }

        public void OnBeforeSimulationFrame()
        {
        }

        public void OnBeforeSimulationTick()
        {
        }

        public void OnCreated(IThreading threading)
        {
        }

        public void OnReleased()
        {
        }

        public void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
        }
    }
}
