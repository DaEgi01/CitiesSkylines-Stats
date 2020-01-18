using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.IO;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using Stats.Config;
using Stats.Localization;
using Stats.Model;
using Stats.Ui;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Stats
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        private readonly string localizationFallbackLanguageCode = "de";

        private ConfigurationService<ConfigurationDto> configurationService;
        private LanguageResourceService<LanguageResourceDto> languageResourceService;
        private GameEngineService gameEngineService;

        private Configuration configuration;
        private LanguageResource languageResource;

        private ItemsInIndexOrder itemsInIndexOrder;
        private MainPanel mainPanel;

        public string SystemName => "Stats";
        public string Name => "Stats";
        public string Description => "Adds a configurable panel to display all vital city stats at a glance.";
        public string Version => "1.1.0";
        public string WorkshopId => "1410077595";

        public void OnEnabled()
        {
            this.InitializeDependencies();

            if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
            {
                this.InitializeMainPanel();
            }
        }

        public void OnDisabled()
        {
            if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
            {
                this.DestroyMainPanel();
            }

            this.DestroyDependencies();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (!(mode == LoadMode.LoadGame || mode == LoadMode.NewGame || mode == LoadMode.NewGameFromScenario))
            {
                return;
            }

            this.InitializeMainPanel();
        }

        public override void OnLevelUnloading()
        {
            this.DestroyMainPanel();
        }

        private void InitializeDependencies()
        {
            var configurationFileFullName = Path.Combine(DataLocation.localApplicationData, SystemName + ".xml");
            this.configurationService = new ConfigurationService<ConfigurationDto>(configurationFileFullName);
            this.languageResourceService = new LanguageResourceService<LanguageResourceDto>(this.SystemName, this.WorkshopId, PluginManager.instance);

            this.gameEngineService = new GameEngineService(Singleton<DistrictManager>.instance,
                Singleton<BuildingManager>.instance,
                Singleton<EconomyManager>.instance,
                Singleton<ImmaterialResourceManager>.instance,
                Singleton<CitizenManager>.instance,
                Singleton<VehicleManager>.instance
            );

            this.configuration = File.Exists(configurationService.ConfigurationFileFullName)
                ? new Configuration(configurationService, configurationService.Load())
                : new Configuration(configurationService, new ConfigurationDto());

            //do not instantiate LocaleManager here.
            //LocaleManager.instance must be called later than during OnEnabled() at the first game start or will causes bugs
            if (LocaleManager.exists)
            {
                this.languageResource = LanguageResource.Create(this.languageResourceService, LocaleManager.instance, LocaleManager.instance.language, localizationFallbackLanguageCode);
            }

            Func<bool> getHideItemsBelowThreshold = () => this.configuration.MainPanelHideItemsBelowThreshold;
            Func<bool> getHideItemsNotAvailable = () => this.configuration.MainPanelHideItemsNotAvailable;

            this.itemsInIndexOrder = new ItemsInIndexOrder(
                ItemData.AllItems
                    .Select(itemData =>
                    {
                        var configurationItemData = this.configuration.GetConfigurationItemData(itemData);
                        var getPercentFunc = this.gameEngineService.GetPercentFunc(itemData);
                        return new Item(
                            itemData,
                            getHideItemsBelowThreshold,
                            getHideItemsNotAvailable,
                            getPercentFunc,
                            configurationItemData.enabled,
                            configurationItemData.criticalThreshold,
                            configurationItemData.sortOrder
                        );
                    })
                );
        }

        private void DestroyDependencies()
        {
            this.configurationService = null;
            this.languageResourceService = null;
            this.gameEngineService = null;

            this.configuration = null;
            this.languageResource.Dispose();
            this.languageResource = null;
        }

        private void InitializeMainPanel()
        {
            var mapHasSnowDumps = this.gameEngineService.CheckIfMapHasSnowDumps();
            this.mainPanel = UIView.GetAView().AddUIComponent(typeof(MainPanel)) as MainPanel;
            this.mainPanel.gameObject.SetActive(false);
            this.mainPanel.Initialize(this.SystemName, mapHasSnowDumps, this.configuration, this.languageResource, this.gameEngineService, this.itemsInIndexOrder);
            var updateAllItemsBehaviour = this.mainPanel.gameObject.AddComponent<UpdateAllItemsBehaviour>();
            updateAllItemsBehaviour.Initialize(this.configuration, this.itemsInIndexOrder, this.gameEngineService, this.mainPanel, this.gameEngineService.CheckIfMapHasSnowDumps());
            this.mainPanel.gameObject.SetActive(true);
        }

        private void DestroyMainPanel()
        {
            this.mainPanel.Disable();
            GameObject.Destroy(this.mainPanel.gameObject);
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            //Workaround for game start only, because here we can access LocaleManager.instance without issues.
            if (this.languageResource == null)
            {
                this.languageResource = LanguageResource.Create(this.languageResourceService, LocaleManager.instance, LocaleManager.instance.language, localizationFallbackLanguageCode);
            }

            var modFullTitle = new ModFullTitle(this.Name, this.Version);
            new ConfigurationPanel(
                helper,
                modFullTitle,
                configuration,
                languageResource,
                itemsInIndexOrder
            ).Initialize();
        }

        //TODO: postvans in use - to be tested, add translations
        //TODO: posttrucks in use - to be tested, add translations
        //TODO: disaster response vehicles - to be tested, add translations
        //TODO: disaster response helicopters - to be tested, add translation
        //TODO: mail itself
        //TODO: evacuation buses in use
        //TODO: rework garbage: split unpicked garbage from incinerator and landfill reserves
        //TODO: rework healthcare: split sick citizen from hospital beds
        //TODO: split item configuration from main panel 
        //TODO: color picker
        //TODO: add happiness values
        //TODO: maybe natural resources used
        //TODO: move itempanel logic out of mainpanel
        //TODO: unselect main menu if another service is selected but does not fit the click on the item
        //TODO: performance (for example GetBudget can be done once)
        //TODO: refactoring
        //TODO: icons
        //TODO: values per building type
        //TODO: values per district
    }
}
