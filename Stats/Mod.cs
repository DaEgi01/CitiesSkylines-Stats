﻿using System.IO;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.IO;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using JetBrains.Annotations;
using Stats.Config;
using Stats.Localization;
using Stats.Ui;
using UnityEngine;

namespace Stats;

public sealed class Mod : LoadingExtensionBase, IUserMod
{
    private readonly ModInfo _modInfo = new ("Stats", "Stats", "1.4.2", 1410077595);
    private readonly string _fallbackLanguageTwoLetterCode = "en";

    private LanguageResourceService<LanguageResourceDto>? _languageResourceService;
    private LanguageResource? _languageResource;

    private ConfigurationService<ConfigurationDto>? _configurationService;
    private Configuration? _configuration;
    private ConfigurationPanel? _configurationPanel;

    private MainPanel? _mainPanel;

    public string Name => _modInfo.DisplayName;
    public string Description => "Adds a configurable panel to display all vital city stats at a glance.";

    [UsedImplicitly]
    public void OnEnabled()
    {
        InitializeDependencies();

        if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
        {
            InitializeMainPanel();
        }
    }

    [UsedImplicitly]
    public void OnDisabled()
    {
        if (LoadingManager.exists && LoadingManager.instance.m_loadingComplete)
        {
            DestroyMainPanel();
        }

        DestroyDependencies();
    }

    public override void OnLevelLoaded(LoadMode mode)
    {
        if (!(mode == LoadMode.LoadGame || mode == LoadMode.NewGame || mode == LoadMode.NewGameFromScenario))
        {
            return;
        }

        InitializeMainPanel();
        _configurationPanel.InitializeColorFields();
    }

    public override void OnLevelUnloading()
    {
        DestroyMainPanel();
    }

    [UsedImplicitly]
    public void OnSettingsUI(UIHelperBase helper)
    {
        _configurationPanel.Initialize(helper);
        if (_mainPanel != null)
        {
            _configurationPanel.MainPanel = _mainPanel;
        }
    }

    private void InitializeDependencies()
    {
        var configurationFileFullName = Path.Combine(DataLocation.localApplicationData, _modInfo.SystemName + ".xml");
        _configurationService = new ConfigurationService<ConfigurationDto>(configurationFileFullName);
        _languageResourceService = new LanguageResourceService<LanguageResourceDto>(
            _modInfo,
            PluginManager.instance);

        _configuration = File.Exists(_configurationService.ConfigurationFileFullName)
            ? new Configuration(_configurationService, _configurationService.Load())
            : new Configuration(_configurationService, new ConfigurationDto());

        var playerLanguage = new SavedString(Settings.localeID, Settings.gameSettingsFile, DefaultSettings.localeID);
        LocaleManager.defaultLanguage = playerLanguage; // necessary because LocaleManager.Constructor will use that value lol.
        LocaleManager.Ensure();
        _languageResource = LanguageResource.Create(_languageResourceService, playerLanguage, _fallbackLanguageTwoLetterCode);

        LocaleManager.eventUIComponentLocaleChanged += LocaleManager_eventUIComponentLocaleChanged;

        _configurationPanel = new ConfigurationPanel(_modInfo, _configuration, _languageResource);
    }

    private void DestroyDependencies()
    {
        LocaleManager.eventUIComponentLocaleChanged -= LocaleManager_eventUIComponentLocaleChanged;

        _configurationService = null;
        _languageResourceService = null;

        _configuration = null;
        _languageResource = null;

        _configurationPanel = null;
    }

    private void LocaleManager_eventUIComponentLocaleChanged()
    {
        var languageTwoLetterCode = LocaleManager.instance.language;
        _languageResource?.LoadLanguage(languageTwoLetterCode);

        if (_mainPanel is null)
            return;

        _mainPanel.UpdateLocalization();
    }

    private void InitializeMainPanel()
    {
        var gameEngineService = new GameEngineService(
            DistrictManager.instance,
            BuildingManager.instance,
            EconomyManager.instance,
            ImmaterialResourceManager.instance,
            CitizenManager.instance,
            VehicleManager.instance);

        var percentStringCache = new PercentStringCache(0, 999);

        _mainPanel = (MainPanel)UIView.GetAView().AddUIComponent(typeof(MainPanel));
        _mainPanel!.Initialize(
            _modInfo.SystemName,
            _configuration,
            _languageResource,
            gameEngineService,
            InfoManager.instance,
            percentStringCache);
        if (_configurationPanel != null)
        {
            _configurationPanel.MainPanel = _mainPanel;
        }
    }

    private void DestroyMainPanel()
    {
        if (_configurationPanel != null)
        {
            _configurationPanel.MainPanel = null;
        }

        if (_mainPanel is null)
            return;

        GameObject.Destroy(_mainPanel.gameObject);
        _mainPanel = null;
    }

    // TODO: add missing translations
    // TODO: mail itself
    // TODO: evacuation buses in use
    // TODO: rework garbage: split unpicked garbage from incinerator and landfill reserves
    // TODO: rework healthcare: split sick citizen from hospital beds
    // TODO: color picker
    // TODO: maybe natural resources used
    // TODO: unselect main menu if another service is selected but does not fit the click on the item
    // TODO: performance (for example GetBudget can be done once)
    // TODO: performance create only the ItemPanels that are enabled
    // TODO: refactoring
    // TODO: icons
    // TODO: values per building type
    // TODO: values per district
}
