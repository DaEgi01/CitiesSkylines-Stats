using Stats.Config;
using Stats.Model;
using System;
using System.Collections;
using UnityEngine;

namespace Stats
{
    public class UpdateAllItemsBehaviour : MonoBehaviour
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

            if (this.configuration.GetConfigurationItemData(ItemData.CemeteryVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.CemeteryVehicles.Index].Percent =
                    this.gameEngineService.GetCemeteryVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.CrematoriumVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.CrematoriumVehicles.Index].Percent =
                    this.gameEngineService.GetCrematoriumVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.HealthcareVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.HealthcareVehicles.Index].Percent =
                    this.gameEngineService.GetHealthCareVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.MedicalHelicopters).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.MedicalHelicopters.Index].Percent =
                    this.gameEngineService.GetMedicalHelicoptersPercent();
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

            if (this.configuration.GetConfigurationItemData(ItemData.DisasterResponseVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.DisasterResponseVehicles.Index].Percent =
                    this.gameEngineService.GetDisasterResponseVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.DisasterResponseHelicopters).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.DisasterResponseHelicopters.Index].Percent =
                    this.gameEngineService.GetDisasterResponseHelicoptersPercent();
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

            if (this.configuration.GetConfigurationItemData(ItemData.FireDepartmentVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.FireDepartmentVehicles.Index].Percent =
                    this.gameEngineService.GetFireDepartmentVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.FireHelicopters).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.FireHelicopters.Index].Percent =
                    this.gameEngineService.GetFireHelicoptersPercent();
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

            if (this.configuration.GetConfigurationItemData(ItemData.GarbageProcessingVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.GarbageProcessingVehicles.Index].Percent =
                    this.gameEngineService.GetGarbageProcessingPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.LandfillVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.LandfillVehicles.Index].Percent =
                    this.gameEngineService.GetLandfillVehiclesPercent();
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

            if (this.configuration.GetConfigurationItemData(ItemData.PoliceHoldingCells).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.PoliceHoldingCells.Index].Percent =
                    this.gameEngineService.GetPoliceHoldingCellsPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.PoliceVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.PoliceVehicles.Index].Percent =
                    this.gameEngineService.GetPoliceVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.PrisonCells).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.PrisonCells.Index].Percent =
                    this.gameEngineService.GetPrisonCellsPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.PrisonVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.PrisonVehicles.Index].Percent =
                    this.gameEngineService.GetPrisonVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.PoliceHelicopters).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.PoliceHelicopters.Index].Percent =
                    this.gameEngineService.GetPoliceHelicoptersPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.Taxis).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.Taxis.Index].Percent =
                    this.gameEngineService.GetTaxisPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.PostVans).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.PostVans.Index].Percent =
                    this.gameEngineService.GetPostVansPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.PostTrucks).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.PostTrucks.Index].Percent =
                    this.gameEngineService.GetPostTrucksPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.RoadMaintenanceVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.RoadMaintenanceVehicles.Index].Percent =
                    this.gameEngineService.GetRoadMaintenanceVehiclesPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.SnowDump).enabled && mapHasSnowDumps)
            {
                this.itemsInIndexOrder.Items[ItemData.SnowDump.Index].Percent =
                    this.gameEngineService.GetSnowDumpPercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.SnowDumpVehicles).enabled && mapHasSnowDumps)
            {
                this.itemsInIndexOrder.Items[ItemData.SnowDumpVehicles.Index].Percent =
                    this.gameEngineService.GetSnowDumpVehiclesPercent();
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

            if (this.configuration.GetConfigurationItemData(ItemData.WaterPumpingServiceStorage).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.WaterPumpingServiceStorage.Index].Percent =
                    this.gameEngineService.GetWaterPumpingServiceStoragePercent();
                yield return new WaitForEndOfFrame();
            }

            if (this.configuration.GetConfigurationItemData(ItemData.WaterPumpingServiceVehicles).enabled)
            {
                this.itemsInIndexOrder.Items[ItemData.WaterPumpingServiceVehicles.Index].Percent =
                    this.gameEngineService.GetWaterPumpingServiceVehiclesPercent();
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
    }
}
