using ColossalFramework.PlatformServices;
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
            if (modInfo is null)
                throw new ArgumentNullException(nameof(modInfo));

            if (pluginManager is null)
                throw new ArgumentNullException(nameof(pluginManager));

            _modInfo = modInfo;
            _pluginManager = pluginManager;
        }

        private string GetExpectedFileFullName(string languageTwoLetterCode)
        {
            var publishedFileId = new PublishedFileId(_modInfo.WorkshopId);
            var plugin = _pluginManager.GetPluginsInfo()
                .Where(x =>
                    x.name == _modInfo.DisplayName
                    || x.publishedFileID == publishedFileId
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

        public T? Load(string languageTwoLetterCode)
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
