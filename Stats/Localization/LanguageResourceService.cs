using ColossalFramework.Plugins;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Stats.Localization
{
    public class LanguageResourceService<T>
    {
        private readonly ModInfo _modInfo;
        private readonly PluginManager _pluginManager;

        public LanguageResourceService(ModInfo modInfo, PluginManager pluginManager)
        {
            _modInfo = modInfo ?? throw new ArgumentNullException(nameof(modInfo));
            _pluginManager = pluginManager ?? throw new ArgumentNullException(nameof(pluginManager));
        }

        private string GetExpectedFileFullName(string languageTwoLetterCode)
        {
            var plugin = _pluginManager.GetPluginsInfo()
                .Where(x =>
                    x.name == _modInfo.SystemName
                    || x.name == _modInfo.Version.ToString()
                )
                .FirstOrDefault();

            return Path.Combine(
                plugin.modPath,
                Path.Combine(
                    "Localization",
                    $"language.{languageTwoLetterCode}.xml"
                )
            );
        }

        public T Load(string languageTwoLetterCode)
        {
            if (languageTwoLetterCode is null)
            {
                throw new ArgumentNullException(nameof(languageTwoLetterCode));
            }

            var fileFullName = GetExpectedFileFullName(languageTwoLetterCode);
            if (!File.Exists(fileFullName))
            {
                return default;
            }

            var serializer = new XmlSerializer(typeof(T));
            using (var streamReader = new StreamReader(fileFullName))
            {
                return (T)serializer.Deserialize(streamReader);
            }
        }
    }
}
