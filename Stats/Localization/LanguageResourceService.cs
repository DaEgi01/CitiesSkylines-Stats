using ColossalFramework.Plugins;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Stats.Localization
{
    public class LanguageResourceService<T>
    {
        private readonly string _modSystemName;
        private readonly string _workshopId;
        private readonly PluginManager _pluginManager;

        public LanguageResourceService(string modSystemName, string workshopId, PluginManager pluginManager)
        {
            _modSystemName = modSystemName ?? throw new ArgumentNullException(nameof(modSystemName));
            _workshopId = workshopId ?? throw new ArgumentNullException(nameof(workshopId));
            _pluginManager = pluginManager ?? throw new ArgumentNullException(nameof(pluginManager));
        }

        private string GetExpectedFileFullName(string languageTwoLetterCode)
        {
            var plugin = _pluginManager.GetPluginsInfo()
                .Where(x =>
                    x.name == _modSystemName
                    || x.name == _workshopId
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
