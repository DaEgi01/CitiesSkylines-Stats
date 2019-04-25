using ColossalFramework.Globalization;
using ColossalFramework.IO;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using Stats.Configuration;
using Stats.Localization;
using Stats.Ui;
using System.IO;
using UnityEngine;

namespace Stats
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        private ConfigurationService<ConfigurationDto> configurationService;
        private LanguageResourceService languageResourceService;
        private GameEngineService gameEngineService;

        private ConfigurationModel configuration;
        private LanguageResourceModel languageResource;

        private MainPanel mainPanel;

        public string SystemName => "Stats";
        public string Name => "Stats";
        public string Description => "Adds a configurable panel to display all vital city stats at a glance.";
        public string Version => "1.0.9";
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
            this.languageResourceService = new LanguageResourceService(this.SystemName, this.WorkshopId, PluginManager.instance);
            this.gameEngineService = new GameEngineService();

            this.configuration = File.Exists(configurationService.ConfigurationFileFullName)
                ? new ConfigurationModel(configurationService, configurationService.Load())
                : new ConfigurationModel(configurationService, new ConfigurationDto());

            //do not instantiate LocaleManager here.
            //LocaleManager.instance must be called later than during OnEnabled() at the first game start or will causes bugs
            if (LocaleManager.exists)
            {
                this.languageResource = new LanguageResourceModel(this.languageResourceService, LocaleManager.instance);
            }
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
            this.mainPanel.Initialize(this.SystemName, mapHasSnowDumps, this.configuration, this.languageResource);
            this.mainPanel.relativePosition = new Vector3(this.configuration.MainPanelPositionX, this.configuration.MainPanelPositionY);
        }

        private void DestroyMainPanel()
        {
            SaveMainPanelPosition();
            this.mainPanel.Disable();
            GameObject.Destroy(this.mainPanel.gameObject);
        }

        private void SaveMainPanelPosition()
        {
            this.configuration.MainPanelPositionX = this.mainPanel.relativePosition.x;
            this.configuration.MainPanelPositionY = this.mainPanel.relativePosition.y;
            this.configuration.Save();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            //Workaround for game start only, because here we can access LocaleManager.instance without issues.
            if (this.languageResource == null)
            {
                this.languageResource = new LanguageResourceModel(this.languageResourceService, LocaleManager.instance);
            }

            var modFullTitle = new ModFullTitle(this.Name, this.Version);
            var configurationPanel = new ConfigurationPanel(helper, modFullTitle, this.configuration, this.languageResource);
            configurationPanel.Initialize();
        }

        //TODO: (should be fixed, tests left) reset does not work properly, see unemployment critical threshold
        //TODO: (should be fixed, tests left) reset should also work for main panel position
        //TODO: postvans in use
        //TODO: posttrucks in use
        //TODO: disaster response vehicles and helis in use
        //TODO: evacuation buses in use
        //TODO: rework garbage: split unpicked garbage from incinerator and landfill reserves
        //TODO: rework healthcare: split sick citizen from hospital beds
        //TODO: split item configuration from main panel 
        //TODO: reset position button
        //TODO: color picker
        //TODO: add happiness values
        //TODO: maybe natural resources used
        //TODO: move itempanel logic out of mainpanel
        //TODO: performance (for example GetBudget can be done once)
        //TODO: refactoring
        //TODO: icons
        //TODO: values per building type
        //TODO: values per district
    }
}
