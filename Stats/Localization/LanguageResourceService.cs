using ColossalFramework.Plugins;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Stats.Localization
{
    public class LanguageResourceService
    {
        private readonly string modSystemName;
        private readonly string workshopId;

        public LanguageResourceService(string modSystemName, string workshopId)
        {
            this.modSystemName = modSystemName ?? throw new ArgumentNullException(nameof(modSystemName));
            this.workshopId = workshopId ?? throw new ArgumentNullException(nameof(workshopId));
        }

        private string GetExpectedFileFullName(string languageTwoLetterCode)
        {
            var plugin = PluginManager.instance.GetPluginsInfo().Where(x => x.name == this.modSystemName || x.name == this.workshopId).FirstOrDefault();
            return Path.Combine(plugin.modPath,
                Path.Combine(
                    "Localization", $"language.{languageTwoLetterCode}.xml"
                )
            );
        }

        public LanguageResourceDto Load(string languageTwoLetterCode)
        {
            var fileFullName = this.GetExpectedFileFullName(languageTwoLetterCode);
            if (!File.Exists(fileFullName))
            {
                fileFullName = this.GetExpectedFileFullName("en");
            }

            if (!File.Exists(fileFullName))
            {
                throw new Exception("Localization file and english fallback file not found.");
            }

            var serializer = new XmlSerializer(typeof(LanguageResourceDto));
            using (var streamReader = new StreamReader(fileFullName))
            {
                return (LanguageResourceDto)serializer.Deserialize(streamReader);
            }
        }
    }
}
