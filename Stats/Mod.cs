using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.IO;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using Stats.Config;
using Stats.Localization;
using Stats.Ui;
using System.IO;
using UnityEngine;

namespace Stats
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        private readonly string fallbackLanguageTwoLetterCode = "en";

        private ConfigurationService<ConfigurationDto> configurationService;
        private LanguageResourceService<LanguageResourceDto> languageResourceService;
        private GameEngineService gameEngineService;

        private Configuration configuration;
        private LanguageResource languageResource;

        private MainPanel mainPanel;
        private ConfigurationPanel configurationPanel;

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
            this.languageResourceService = new LanguageResourceService<LanguageResourceDto>(
                this.SystemName,
                this.WorkshopId,
                PluginManager.instance
            );

            this.gameEngineService = new GameEngineService(
                DistrictManager.instance,
                BuildingManager.instance,
                EconomyManager.instance,
                ImmaterialResourceManager.instance,
                CitizenManager.instance,
                VehicleManager.instance
            );

            this.configuration = File.Exists(configurationService.ConfigurationFileFullName)
                ? new Configuration(configurationService, configurationService.Load())
                : new Configuration(configurationService, new ConfigurationDto());

            var playerLanguage = new SavedString(Settings.localeID, Settings.gameSettingsFile, DefaultSettings.localeID);

            Debug.Log("Initialize Dependencies Lang: " + playerLanguage);

            LocaleManager.defaultLanguage = playerLanguage; //necessary because LocaleManager.Constructor will use that value lol.
            LocaleManager.Ensure();
            this.languageResource = LanguageResource.Create(this.languageResourceService, playerLanguage, fallbackLanguageTwoLetterCode);

            LocaleManager.eventLocaleChanged += LocaleManager_eventLocaleChanged;
        }

        private void DestroyDependencies()
        {
            this.configurationService = null;
            this.languageResourceService = null;
            this.gameEngineService = null;

            this.configuration = null;
            this.languageResource = null;

            LocaleManager.eventLocaleChanged -= LocaleManager_eventLocaleChanged;
        }

        private void LocaleManager_eventLocaleChanged()
        {
            Debug.Log("LocaleManager_eventLocaleChanged called");

            var languageTwoLetterCode = LocaleManager.instance.language;

            Debug.Log("Language:" + languageTwoLetterCode);

            this.languageResource.LoadLanguage(languageTwoLetterCode);
            this.mainPanel.UpdateLocalization();
        }

        private void InitializeMainPanel()
        {
            var mapHasSnowDumps = this.gameEngineService.CheckIfMapHasSnowDumps();
            this.mainPanel = UIView.GetAView().AddUIComponent(typeof(MainPanel)) as MainPanel;
            this.mainPanel.Initialize(this.SystemName, mapHasSnowDumps, this.configuration, this.languageResource, this.gameEngineService, InfoManager.instance);
        }

        private void DestroyMainPanel()
        {
            GameObject.Destroy(this.mainPanel.gameObject);
            this.mainPanel = null;
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            var modFullTitle = new ModFullTitle(this.Name, this.Version);
            //bad workaround for the fact that OnSettingsUI is triggered before
            //eventLocaleChanged is triggered on language change.
            if (languageResource.CurrentLanguage != LocaleManager.instance.language)
            {
                languageResource.LoadLanguage(LocaleManager.instance.language);
            }
            
            this.configurationPanel = new ConfigurationPanel(
                helper,
                modFullTitle,
                configuration,
                languageResource
            );

            this.configurationPanel.Initialize();
            if (this.mainPanel != null)
            {
                this.configurationPanel.MainPanel = this.mainPanel;
            }
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
