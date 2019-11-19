using ColossalFramework;
using ColossalFramework.UI;
using Stats.Configuration;
using Stats.Localization;
using System;
using System.Linq;
using UnityEngine;

namespace Stats.Ui
{
    public class MainPanel : UIPanel
    {
        private UIDragHandleWithDragState uiDragHandle;
        private string modSystemName;
        private bool mapHasSnowDumps;
        private ConfigurationModel configuration;
        private LanguageResourceModel languageResource;
        private float secondsSinceLastUpdate;

        private ItemPanel[] itemPanelsInIndexOrder;
        private ItemPanel[] itemPanelsInDisplayOrder;

        public void Initialize(string modSystemName, bool mapHasSnowDumps, ConfigurationModel configuration, LanguageResourceModel languageResource)
        {
            this.modSystemName = modSystemName ?? throw new ArgumentNullException(nameof(modSystemName));
            this.mapHasSnowDumps = mapHasSnowDumps;
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (this.configuration.MainPanelColumnCount < 1)
                throw new ArgumentOutOfRangeException($"'{nameof(this.configuration.MainPanelColumnCount)}' parameter must be bigger or equal to 1.");
            this.languageResource = languageResource ?? throw new ArgumentNullException(nameof(languageResource));
            this.secondsSinceLastUpdate = this.configuration.MainPanelUpdateEveryXSeconds; //force an immediate update

            this.color = configuration.MainPanelBackgroundColor;
            this.name = modSystemName + "MainPanel";
            this.backgroundSprite = "GenericPanelLight";
            this.isInteractive = false;

            this.CreateAndAddDragHandle();
            this.CreateAndAddAllUiItems();
            this.UpdateLocalizedTooltips();

            this.relativePosition = this.configuration.MainPanelPosition;

            this.UpdateLayout();

            this.configuration.LayoutPropertyChanged += this.UpdateLayoutIfDirty;
            this.configuration.PositionChanged += this.UpdatePosition;
            this.languageResource.LanguageChanged += this.UpdateLocalizedTooltips;
            this.uiDragHandle.eventMouseUp += UiDragHandle_eventMouseUp;
        }

        public override void OnDestroy()
        {
            this.configuration.LayoutPropertyChanged -= this.UpdateLayoutIfDirty;
            this.configuration.PositionChanged -= this.UpdatePosition;
            this.languageResource.LanguageChanged -= this.UpdateLocalizedTooltips;
            this.uiDragHandle.eventMouseUp -= UiDragHandle_eventMouseUp;

            base.OnDestroy();
        }

        private void CreateAndAddDragHandle()
        {
            var dragHandle = this.AddUIComponent<UIDragHandleWithDragState>();
            dragHandle.name = this.modSystemName + "DragHandle";
            dragHandle.relativePosition = Vector2.zero;
            dragHandle.target = this;
            dragHandle.constrainToScreen = true;
            dragHandle.SendToBack();
            this.uiDragHandle = dragHandle;
        }

        private void CreateAndAddAllUiItems()
        {
            this.itemPanelsInIndexOrder = Item.AllItems
                .Select(i => this.configuration.GetConfigurationItem(i))
                .Select(ci => this.CreateUiItemAndAddButtons(ci))
                .OrderBy(ci => ci.ConfigurationItem.Item.Index)
                .ToArray();

            ValidateIndexes(this.itemPanelsInIndexOrder);

            if (!mapHasSnowDumps)
            {
                this.itemPanelsInIndexOrder[Item.SnowDump.Index].isVisible = false;
                this.itemPanelsInIndexOrder[Item.SnowDumpVehicles.Index].isVisible = false;
            }

            this.itemPanelsInDisplayOrder = this.itemPanelsInIndexOrder
                .OrderBy(x => x.ConfigurationItem.SortOrder)
                .ToArray();
        }

        private void ValidateIndexes(ItemPanel[] itemPanel)
        {
            for (int i = 0; i < itemPanel.Length; i++)
            {
                if (i != itemPanel[i].ConfigurationItem.Item.Index)
                {
                    throw new IndexesMessedUpException(i);
                }
            }
        }

        private ItemPanel CreateUiItemAndAddButtons(ConfigurationItemModel configurationItem)
        {
            var uiItem = this.CreateAndAddItemPanel();
            uiItem.Initialize(this.configuration, configurationItem, this.languageResource);
            return uiItem;
        }

        private ItemPanel CreateAndAddItemPanel()
        {
            var itemPanel = this.AddUIComponent<ItemPanel>();
            itemPanel.width = this.configuration.ItemWidth;
            itemPanel.height = this.configuration.ItemHeight;
            itemPanel.zOrder = zOrder;
            return itemPanel;
        }

        private void UpdateLocalizedTooltips()
        {
            for (int i = 0; i < itemPanelsInIndexOrder.Length; i++)
            {
                itemPanelsInIndexOrder[i].UpdateLocalizedTooltips();
            }
        }

        private void UpdateLayoutIfDirty()
        {
            var anyItemPanelVisibilityChanged = false;
            for (int i = 0; i < this.itemPanelsInIndexOrder.Length; i++)
            {
                var itemPanel = this.itemPanelsInIndexOrder[i];
                if (itemPanel.ItemVisibility.VisibilityChanged)
                {
                    anyItemPanelVisibilityChanged = true;
                    break;
                }
            }

            if (anyItemPanelVisibilityChanged)
            {
                this.UpdateLayout();
            }
        }

        private void UpdateLayout()
        {
            this.UpdateItemsLayout();
            this.UpdatePanelSize();
        }

        public override void Update()
        {
            if (this.configuration.MainPanelAutoHide && !this.containsMouse)
            {
                this.opacity = 0;
            }
            else
            {
                this.opacity = 1;
            }

            if (this.configuration.MainPanelUpdateEveryXSeconds == 0)
            {
                return;
            }

            this.secondsSinceLastUpdate += Time.deltaTime;
            if (this.secondsSinceLastUpdate < this.configuration.MainPanelUpdateEveryXSeconds)
            {
                return;
            }
            this.secondsSinceLastUpdate = 0;

            if (!Singleton<DistrictManager>.exists)
            {
                return;
            }

            var allDistricts = Singleton<DistrictManager>.instance.m_districts.m_buffer[0];

            if (this.configuration.GetConfigurationItem(Item.Electricity).Enabled)
            {
                var electricityCapacity = allDistricts.GetElectricityCapacity();
                var electricityConsumption = allDistricts.GetElectricityConsumption();
                this.itemPanelsInIndexOrder[Item.Electricity.Index].Percent = GetUsagePercent(electricityCapacity, electricityConsumption);
            }

            if (this.configuration.GetConfigurationItem(Item.Heating).Enabled)
            {
                var heatingCapacity = allDistricts.GetHeatingCapacity();
                var heatingConsumption = allDistricts.GetHeatingConsumption();
                this.itemPanelsInIndexOrder[Item.Heating.Index].Percent = GetUsagePercent(heatingCapacity, heatingConsumption);
            }

            if (this.configuration.GetConfigurationItem(Item.Water).Enabled)
            {
                var waterCapacity = allDistricts.GetWaterCapacity();
                var waterConsumption = allDistricts.GetWaterConsumption();
                this.itemPanelsInIndexOrder[Item.Water.Index].Percent = GetUsagePercent(waterCapacity, waterConsumption);
            }

            if (this.configuration.GetConfigurationItem(Item.SewageTreatment).Enabled)
            {
                var sewageCapacity = allDistricts.GetSewageCapacity();
                var sewageAccumulation = allDistricts.GetSewageAccumulation();
                this.itemPanelsInIndexOrder[Item.SewageTreatment.Index].Percent = GetUsagePercent(sewageCapacity, sewageAccumulation);
            }

            if (this.configuration.GetConfigurationItem(Item.WaterReserveTank).Enabled)
            {
                var waterStorageTotal = allDistricts.GetWaterStorageCapacity();
                var waterStorageInUse = allDistricts.GetWaterStorageAmount();
                this.itemPanelsInIndexOrder[Item.WaterReserveTank.Index].Percent = GetAvailabilityPercent(waterStorageTotal, waterStorageInUse);
            }

            if (this.configuration.GetConfigurationItem(Item.WaterPumpingServiceStorage).Enabled 
                || this.configuration.GetConfigurationItem(Item.WaterPumpingServiceVehicles).Enabled)
            {
                long waterSewageStorageTotal = 0;
                long waterSewageStorageInUse = 0;

                int pumpingVehiclesTotal = 0;
                int pumpingVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
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

                    var budget = Singleton<EconomyManager>.instance.GetBudget(building.Info.m_class);
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

                this.itemPanelsInIndexOrder[Item.WaterPumpingServiceVehicles.Index].Percent = GetUsagePercent(pumpingVehiclesTotal, pumpingVehiclesInUse);
                this.itemPanelsInIndexOrder[Item.WaterPumpingServiceStorage.Index].Percent = GetUsagePercent(waterSewageStorageTotal, waterSewageStorageInUse);
            }

            if (this.configuration.GetConfigurationItem(Item.Landfill).Enabled)
            {
                var garbageCapacity = allDistricts.GetGarbageCapacity();
                var garbageAmout = allDistricts.GetGarbageAmount();

                this.itemPanelsInIndexOrder[Item.Landfill.Index].Percent = GetUsagePercent(garbageCapacity, garbageAmout);
            }

            if (this.configuration.GetConfigurationItem(Item.GarbageProcessing).Enabled)
            {
                var incineratorCapacity = allDistricts.GetIncinerationCapacity();
                var incineratorAccumulation = allDistricts.GetGarbageAccumulation();
                this.itemPanelsInIndexOrder[Item.GarbageProcessing.Index].Percent = GetUsagePercent(incineratorCapacity, incineratorAccumulation);
            }

            if (this.configuration.GetConfigurationItem(Item.LandfillVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.GarbageProcessingVehicles).Enabled)
            {
                var landfillVehiclesTotal = 0;
                var landfillVehiclesInUse = 0;

                var garbageProcessingVehiclesTotal = 0;
                var garbageProcessingVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
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

                    int budget = Singleton<EconomyManager>.instance.GetBudget(buildingAi.m_info.m_class);
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

                this.itemPanelsInIndexOrder[Item.LandfillVehicles.Index].Percent = GetUsagePercent(landfillVehiclesTotal, landfillVehiclesInUse);
                this.itemPanelsInIndexOrder[Item.GarbageProcessingVehicles.Index].Percent = GetUsagePercent(garbageProcessingVehiclesTotal, garbageProcessingVehiclesInUse);
            }

            if (this.configuration.GetConfigurationItem(Item.ElementarySchool).Enabled)
            {
                var elementrySchoolCapacity = allDistricts.GetEducation1Capacity();
                var elementrySchoolUsage = allDistricts.GetEducation1Need();
                this.itemPanelsInIndexOrder[Item.ElementarySchool.Index].Percent = GetUsagePercent(elementrySchoolCapacity, elementrySchoolUsage);
            }

            if (this.configuration.GetConfigurationItem(Item.HighSchool).Enabled)
            {
                var highSchoolCapacity = allDistricts.GetEducation2Capacity();
                var highSchoolUsage = allDistricts.GetEducation2Need();
                this.itemPanelsInIndexOrder[Item.HighSchool.Index].Percent = GetUsagePercent(highSchoolCapacity, highSchoolUsage);
            }

            if (this.configuration.GetConfigurationItem(Item.University).Enabled)
            {
                var universityCapacity = allDistricts.GetEducation3Capacity();
                var universityUsage = allDistricts.GetEducation3Need();
                this.itemPanelsInIndexOrder[Item.University.Index].Percent = GetUsagePercent(universityCapacity, universityUsage);
            }

            if (this.configuration.GetConfigurationItem(Item.Healthcare).Enabled)
            {
                var healthcareCapacity = allDistricts.GetHealCapacity();
                var healthcareUsage = allDistricts.GetSickCount();
                this.itemPanelsInIndexOrder[Item.Healthcare.Index].Percent = GetUsagePercent(healthcareCapacity, healthcareUsage);
            }

            if (this.configuration.GetConfigurationItem(Item.AverageIllnessRate).Enabled)
            {
                if (allDistricts.GetSickCount() == 0)
                {
                    this.itemPanelsInIndexOrder[Item.AverageIllnessRate.Index].Percent = null;
                }
                else
                {
                    this.itemPanelsInIndexOrder[Item.AverageIllnessRate.Index].Percent = (int)(100 - (float)allDistricts.m_residentialData.m_finalHealth);
                }
            }

            if (this.configuration.GetConfigurationItem(Item.Cemetery).Enabled)
            {
                var deadCapacity = allDistricts.GetDeadCapacity();
                var deadAmount = allDistricts.GetDeadAmount();
                this.itemPanelsInIndexOrder[Item.Cemetery.Index].Percent = GetUsagePercent(deadCapacity, deadAmount);
            }

            if (this.configuration.GetConfigurationItem(Item.Crematorium).Enabled)
            {
                var cremateCapacity = allDistricts.GetCremateCapacity();
                var deadCount = allDistricts.GetDeadCount();
                this.itemPanelsInIndexOrder[Item.Crematorium.Index].Percent = GetUsagePercent(cremateCapacity, deadCount);
            }

            if (this.configuration.GetConfigurationItem(Item.HealthcareVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.MedicalHelicopters).Enabled
                || this.configuration.GetConfigurationItem(Item.CemeteryVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.CrematoriumVehicles).Enabled)
            {
                var healthcareVehiclesTotal = 0;
                var healthcareVehiclesInUse = 0;

                var medicalHelicoptersTotal = 0;
                var medicalHelicoptersInUse = 0;

                var cemeteryVehiclesTotal = 0;
                var cemeteryVehiclesInUse = 0;

                var crematoriumVehiclesTotal = 0;
                var crematoriumVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var healthcareBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.HealthCare);

                for (int i = 0; i < healthcareBuildingIds.m_size; i++)
                {
                    var buildingId = healthcareBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI();
                    switch (buildingAi)
                    {
                        case HospitalAI hospitalAI when
                            this.configuration.GetConfigurationItem(Item.HealthcareVehicles).Enabled:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(hospitalAI.m_info.m_class);
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
                        case HelicopterDepotAI helicopterDepotAI when
                            this.configuration.GetConfigurationItem(Item.MedicalHelicopters).Enabled:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(helicopterDepotAI.m_info.m_class);
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
                        case CemeteryAI cemeteryAI when 
                            this.configuration.GetConfigurationItem(Item.CemeteryVehicles).Enabled 
                            || this.configuration.GetConfigurationItem(Item.CrematoriumVehicles).Enabled:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(cemeteryAI.m_info.m_class);
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

                this.itemPanelsInIndexOrder[Item.HealthcareVehicles.Index].Percent = GetUsagePercent(healthcareVehiclesTotal, healthcareVehiclesInUse);
                this.itemPanelsInIndexOrder[Item.MedicalHelicopters.Index].Percent = GetUsagePercent(medicalHelicoptersTotal, medicalHelicoptersInUse);
                this.itemPanelsInIndexOrder[Item.CemeteryVehicles.Index].Percent = GetUsagePercent(cemeteryVehiclesTotal, cemeteryVehiclesInUse);
                this.itemPanelsInIndexOrder[Item.CrematoriumVehicles.Index].Percent = GetUsagePercent(crematoriumVehiclesTotal, crematoriumVehiclesInUse);
            }

            if (this.configuration.GetConfigurationItem(Item.TrafficJam).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.TrafficJam.Index].Percent = (int)(100 - (float)Singleton<VehicleManager>.instance.m_lastTrafficFlow);
            }

            if (this.configuration.GetConfigurationItem(Item.GroundPollution).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.GroundPollution.Index].Percent = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetGroundPollution();
            }

            if (this.configuration.GetConfigurationItem(Item.DrinkingWaterPollution).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.DrinkingWaterPollution.Index].Percent = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetWaterPollution();
            }

            if (this.configuration.GetConfigurationItem(Item.NoisePollution).Enabled)
            {
                Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.NoisePollution, out int noisePollution);
                this.itemPanelsInIndexOrder[Item.NoisePollution.Index].Percent = noisePollution;
            }

            if (this.configuration.GetConfigurationItem(Item.FireHazard).Enabled)
            {
                Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.FireHazard, out int fireHazard);
                this.itemPanelsInIndexOrder[Item.FireHazard.Index].Percent = fireHazard;
            }

            if (this.configuration.GetConfigurationItem(Item.FireDepartmentVehicles).Enabled || this.configuration.GetConfigurationItem(Item.FireHelicopters).Enabled)
            {
                var fireDepartmentVehiclesTotal = 0;
                var fireDepartmentVehiclesInUse = 0;

                var fireHelicoptersTotal = 0;
                var fireHelicoptersInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var fireDepartmentBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.FireDepartment);

                for (int i = 0; i < fireDepartmentBuildingIds.m_size; i++)
                {
                    var buildingId = fireDepartmentBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI();
                    switch (buildingAi)
                    {
                        case FireStationAI fireStationAI when
                            this.configuration.GetConfigurationItem(Item.FireDepartmentVehicles).Enabled:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(fireStationAI.m_info.m_class);
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
                        case HelicopterDepotAI helicopterDepotAI when
                            this.configuration.GetConfigurationItem(Item.FireHelicopters).Enabled:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(helicopterDepotAI.m_info.m_class);
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

                this.itemPanelsInIndexOrder[Item.FireDepartmentVehicles.Index].Percent = GetUsagePercent(fireDepartmentVehiclesTotal, fireDepartmentVehiclesInUse);
                this.itemPanelsInIndexOrder[Item.FireHelicopters.Index].Percent = GetUsagePercent(fireHelicoptersTotal, fireHelicoptersInUse);
            }

            if (this.configuration.GetConfigurationItem(Item.CrimeRate).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.CrimeRate.Index].Percent = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalCrimeRate;
            }

            if (this.configuration.GetConfigurationItem(Item.PoliceHoldingCells).Enabled
                || this.configuration.GetConfigurationItem(Item.PoliceVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.PrisonCells).Enabled
                || this.configuration.GetConfigurationItem(Item.PrisonVehicles).Enabled
                || this.configuration.GetConfigurationItem(Item.PoliceHelicopters).Enabled)
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

                var buildingManager = Singleton<BuildingManager>.instance;
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
                        case PoliceStationAI policeStationAi when
                            this.configuration.GetConfigurationItem(Item.PoliceHelicopters).Enabled
                            || this.configuration.GetConfigurationItem(Item.PoliceVehicles).Enabled
                            || this.configuration.GetConfigurationItem(Item.PrisonCells).Enabled
                            || this.configuration.GetConfigurationItem(Item.PrisonVehicles).Enabled:
                            {
                                //PoliceStationAI.GetLocalizedStats
                                var instance = Singleton<CitizenManager>.instance;
                                uint num = building.m_citizenUnits;
                                int num2 = 0;
                                int cellsInUse = 0;
                                while (num != 0)
                                {
                                    uint nextUnit = instance.m_units.m_buffer[num].m_nextUnit;
                                    if ((instance.m_units.m_buffer[num].m_flags & CitizenUnit.Flags.Visit) != 0)
                                    {
                                        for (int j = 0; j < 5; j++)
                                        {
                                            uint citizen = instance.m_units.m_buffer[num].GetCitizen(j);
                                            if (citizen != 0 && instance.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Visit)
                                            {
                                                cellsInUse++;
                                            }
                                        }
                                    }
                                    num = nextUnit;
                                    if (++num2 > 524288)
                                    {
                                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                        break;
                                    }
                                }

                                int budget = Singleton<EconomyManager>.instance.GetBudget(policeStationAi.m_info.m_class);
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
                        case HelicopterDepotAI helicopterDepotAI when
                            this.configuration.GetConfigurationItem(Item.PoliceHelicopters).Enabled:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(helicopterDepotAI.m_info.m_class);
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

                this.itemPanelsInIndexOrder[Item.PoliceHoldingCells.Index].Percent = GetUsagePercent(policeHoldingCellsTotal, policeHoldingCellsInUse);
                this.itemPanelsInIndexOrder[Item.PoliceVehicles.Index].Percent = GetUsagePercent(policeVehiclesTotal, policeVehiclesInUse);
                this.itemPanelsInIndexOrder[Item.PoliceHelicopters.Index].Percent = GetUsagePercent(policeHelicoptersTotal, policeHelicoptersInUse);
                this.itemPanelsInIndexOrder[Item.PrisonCells.Index].Percent = GetUsagePercent(prisonCellsTotal, prisonCellsInUse);
                this.itemPanelsInIndexOrder[Item.PrisonVehicles.Index].Percent = GetUsagePercent(prisonVehiclesTotal, prisonVehiclesInUse);
            }

            if (this.configuration.GetConfigurationItem(Item.Unemployment).Enabled)
            {
                this.itemPanelsInIndexOrder[Item.Unemployment.Index].Percent = allDistricts.GetUnemployment();
            }

            if (
                this.configuration.GetConfigurationItem(Item.RoadMaintenanceVehicles).Enabled
                || (this.mapHasSnowDumps && this.configuration.GetConfigurationItem(Item.SnowDump).Enabled)
                || (this.mapHasSnowDumps && this.configuration.GetConfigurationItem(Item.SnowDumpVehicles).Enabled)
            )
            {
                var roadMaintenanceVehiclesTotal = 0;
                var roadMaintenanceVehiclesInUse = 0;

                var snowDumpStorageTotal = 0;
                var snowDumpStorageInUse = 0;

                var snowDumpVehiclesTotal = 0;
                var snowDumpVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var roadMaintenanceBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Road);

                for (int i = 0; i < roadMaintenanceBuildingIds.m_size; i++)
                {
                    var buildingId = roadMaintenanceBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI();
                    switch (buildingAi)
                    {
                        case MaintenanceDepotAI maintenanceDepotAi when 
                            this.configuration.GetConfigurationItem(Item.RoadMaintenanceVehicles).Enabled:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(maintenanceDepotAi.m_info.m_class);
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
                        case SnowDumpAI snowDumpAi when
                            this.mapHasSnowDumps
                            && (
                                this.configuration.GetConfigurationItem(Item.SnowDump).Enabled
                                || this.configuration.GetConfigurationItem(Item.SnowDumpVehicles).Enabled
                            ):
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(snowDumpAi.m_info.m_class);
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

                this.itemPanelsInIndexOrder[Item.RoadMaintenanceVehicles.Index].Percent = GetUsagePercent(roadMaintenanceVehiclesTotal, roadMaintenanceVehiclesInUse);

                if (this.mapHasSnowDumps)
                {
                    this.itemPanelsInIndexOrder[Item.SnowDump.Index].Percent = GetUsagePercent(snowDumpStorageTotal, snowDumpStorageInUse);
                    this.itemPanelsInIndexOrder[Item.SnowDumpVehicles.Index].Percent = GetUsagePercent(snowDumpVehiclesTotal, snowDumpVehiclesInUse);
                }
            }

            if (this.configuration.GetConfigurationItem(Item.ParkMaintenanceVehicles).Enabled)
            {
                var parkMaintenanceVehiclesTotal = 0;
                var parkMaintenanceVehiclesInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
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

                        int budget = Singleton<EconomyManager>.instance.GetBudget(maintenanceDepotAi.m_info.m_class);
                        int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);
                        if (transferReason == TransferManager.TransferReason.ParkMaintenance)
                        {
                            DistrictManager instance = Singleton<DistrictManager>.instance;
                            byte district = instance.GetDistrict(building.m_position);
                            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[(int)district].m_servicePolicies;
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

                this.itemPanelsInIndexOrder[Item.ParkMaintenanceVehicles.Index].Percent = GetUsagePercent(parkMaintenanceVehiclesTotal, parkMaintenanceVehiclesInUse);
            }

            if (this.configuration.GetConfigurationItem(Item.CityUnattractiveness).Enabled)
            {
                Singleton<ImmaterialResourceManager>.instance.CheckGlobalResource(ImmaterialResourceManager.Resource.Attractiveness, out int cityAttractivenessRaw);
                Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.LandValue, out int landValueRaw);
                var cityAttractivenessAndLandValue = cityAttractivenessRaw + landValueRaw;
                var cityAttractiveness = 100 * (cityAttractivenessAndLandValue) / Mathf.Max(cityAttractivenessAndLandValue + 200, 200);

                this.itemPanelsInIndexOrder[Item.CityUnattractiveness.Index].Percent = (100 - cityAttractiveness);
            }

            if (this.configuration.GetConfigurationItem(Item.Taxis).Enabled || this.configuration.GetConfigurationItem(Item.PostVans).Enabled || this.configuration.GetConfigurationItem(Item.PostTrucks).Enabled)
            {
                var taxisTotal = 0;
                var taxisInUse = 0;

                var postVansTotal = 0;
                var postVansInUse = 0;

                var postTrucksTotal = 0;
                var postTrucksInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var publicTransportBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.PublicTransport);
                for (int i = 0; i < publicTransportBuildingIds.m_size; i++)
                {
                    var buildingId = publicTransportBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI();
                    switch (buildingAi)
                    {
                        case DepotAI depotAi when
                            this.configuration.GetConfigurationItem(Item.Taxis).Enabled
                            && depotAi.m_transportInfo != null
                            && depotAi.m_maxVehicleCount != 0
                            && depotAi.m_transportInfo.m_transportType == TransportInfo.TransportType.Taxi:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(depotAi.m_info.m_class);
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
                        case PostOfficeAI postOfficeAi when
                            this.configuration.GetConfigurationItem(Item.PostVans).Enabled
                            || this.configuration.GetConfigurationItem(Item.PostTrucks).Enabled:
                            {
                                int budget = Singleton<EconomyManager>.instance.GetBudget(postOfficeAi.m_info.m_class);
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

                                //TODO mail and stuff
                                int num = building.m_customBuffer1 * 1000;
                                int num2 = building.m_customBuffer2 * 1000;

                                if (this.configuration.GetConfigurationItem(Item.PostVans).Enabled)
                                {
                                    postVansTotal += (productionRate * postOfficeAi.m_postVanCount + 99) / 100;
                                    postVansInUse += ownVanCount;
                                }

                                if (this.configuration.GetConfigurationItem(Item.PostTrucks).Enabled)
                                {
                                    postTrucksTotal += (productionRate * postOfficeAi.m_postTruckCount + 99) / 100;
                                    postTrucksInUse += ownTruckCount;
                                }

                                break;
                            }
                        default:
                            continue;
                    }
                }

                this.itemPanelsInIndexOrder[Item.Taxis.Index].Percent = GetUsagePercent(taxisTotal, taxisInUse);
                this.itemPanelsInIndexOrder[Item.PostVans.Index].Percent = GetUsagePercent(postVansTotal, postVansInUse);
                this.itemPanelsInIndexOrder[Item.PostTrucks.Index].Percent = GetUsagePercent(postTrucksTotal, postTrucksInUse);
            }

            if (this.configuration.GetConfigurationItem(Item.DisasterResponseVehicles).Enabled || this.configuration.GetConfigurationItem(Item.DisasterResponseHelicopters).Enabled)
            {
                var disasterResponseVehiclesTotal = 0;
                var disasterResponseVehiclesInUse = 0;

                var disasterResponseHelicoptersTotal = 0;
                var disasterResponseHelicoptersInUse = 0;

                var buildingManager = Singleton<BuildingManager>.instance;
                var publicTransportBuildingIds = buildingManager.GetServiceBuildings(ItemClass.Service.Disaster);
                for (int i = 0; i < publicTransportBuildingIds.m_size; i++)
                {
                    var buildingId = publicTransportBuildingIds[i];
                    var building = buildingManager.m_buildings.m_buffer[buildingId];
                    var buildingAi = building.Info?.GetAI();
                    if (buildingAi is DisasterResponseBuildingAI disasterResponseBuildingAi)
                    {
                        int budget = Singleton<EconomyManager>.instance.GetBudget(disasterResponseBuildingAi.m_info.m_class);
                        int productionRate = PlayerBuildingAI.GetProductionRate(100, budget);

                        if (this.configuration.GetConfigurationItem(Item.DisasterResponseVehicles).Enabled)
                        {
                            disasterResponseVehiclesTotal += (productionRate * disasterResponseBuildingAi.m_vehicleCount + 99) / 100;
                            int disasterVehicles = 0;
                            int cargo = 0;
                            int capacity = 0;
                            int outside = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Collapsed, ref disasterVehicles, ref cargo, ref capacity, ref outside);

                            disasterResponseVehiclesInUse += disasterVehicles;
                        }

                        if (this.configuration.GetConfigurationItem(Item.DisasterResponseHelicopters).Enabled)
                        {
                            disasterResponseHelicoptersTotal += (productionRate * disasterResponseBuildingAi.m_helicopterCount + 99) / 100;
                            int disasterHelicopters = 0;
                            int cargo2 = 0;
                            int capacity2 = 0;
                            int outside2 = 0;
                            GameMethods.CalculateOwnVehicles(buildingId, ref building, TransferManager.TransferReason.Collapsed2, ref disasterHelicopters, ref cargo2, ref capacity2, ref outside2);

                            disasterResponseHelicoptersInUse += disasterHelicopters;
                        }
                    }
                }

                this.itemPanelsInIndexOrder[Item.DisasterResponseVehicles.Index].Percent = GetUsagePercent(disasterResponseVehiclesTotal, disasterResponseVehiclesInUse);
                this.itemPanelsInIndexOrder[Item.DisasterResponseHelicopters.Index].Percent = GetUsagePercent(disasterResponseHelicoptersTotal, disasterResponseHelicoptersInUse);
            }

            this.UpdateLayout();
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

            return (int)((usage / (float)capacity) * 100);
        }

        private void UpdateItemsLayout()
        {
            var lastLayoutPosition = Vector2.zero;
            int index = 0;

            for (int i = 0; i < this.itemPanelsInDisplayOrder.Length; i++)
            {
                var currentItem = this.itemPanelsInDisplayOrder[i];
                if (!currentItem.isVisible)
                {
                    continue;
                }

                var layoutPosition = new Vector2(index % this.configuration.MainPanelColumnCount, index / this.configuration.MainPanelColumnCount);

                currentItem.relativePosition = CalculateRelativePosition(layoutPosition);
                currentItem.AdjustButtonAndUiItemSize();

                lastLayoutPosition = CalculateNextLayoutPosition(lastLayoutPosition);
                index++;
            }
        }

        private Vector2 CalculateNextLayoutPosition(Vector2 position)
        {
            if (position.x < this.configuration.MainPanelColumnCount - 1)
            {
                return new Vector2(position.x + 1, position.y);
            }
            else
            {
                return new Vector2(0, position.y + 1);
            }
        }

        private Vector3 CalculateRelativePosition(Vector2 layoutPosition)
        {
            var posX = (layoutPosition.x + 1) * this.configuration.ItemPadding + layoutPosition.x * this.configuration.ItemWidth;
            var posY = (layoutPosition.y + 1) * this.configuration.ItemPadding + layoutPosition.y * this.configuration.ItemHeight;

            return new Vector3(posX, posY);
        }

        private void UpdatePanelSize()
        {
            var visibleItemCount = this.itemPanelsInIndexOrder.Where(x => x.isVisible)
                .Count();
            if (visibleItemCount > 0)
            {
                this.isVisible = true;
            }
            else
            {
                this.isVisible = false;
                return;
            }

            var newWidth = this.CalculatePanelWidth(visibleItemCount);
            var newHeight = this.CalculatePanelHeight(visibleItemCount);

            this.width = newWidth;
            this.height = newHeight;

            this.uiDragHandle.width = newWidth;
            this.uiDragHandle.height = newHeight;
        }

        private void UpdatePosition()
        {
            if (uiDragHandle.IsDragged)
            {
                return;
            }

            this.relativePosition = this.configuration.MainPanelPosition;
        }

        private float CalculatePanelWidth(int visibleItemCount)
        {
            if (visibleItemCount < this.configuration.MainPanelColumnCount)
            {
                return (visibleItemCount + 1) * this.configuration.ItemPadding + visibleItemCount * this.configuration.ItemWidth;
            }
            else
            {
                return (this.configuration.MainPanelColumnCount + 1) * this.configuration.ItemPadding + this.configuration.MainPanelColumnCount * this.configuration.ItemWidth;
            }
        }

        private float CalculatePanelHeight(int visibleItemCount)
        {
            var rowCount = Mathf.CeilToInt(visibleItemCount / (float)this.configuration.MainPanelColumnCount);
            return (rowCount + 1) * this.configuration.ItemPadding + rowCount * this.configuration.ItemHeight;
        }

        private void UiDragHandle_eventMouseUp(UIComponent component, UIMouseEventParameter eventParam)
        {
            SaveMainPanelPosition();
        }

        private void SaveMainPanelPosition()
        {
            this.configuration.MainPanelPosition = this.relativePosition;
            this.configuration.Save();
        }
    }
}
