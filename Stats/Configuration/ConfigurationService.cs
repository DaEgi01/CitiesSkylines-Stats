using System.IO;
using System.Xml.Serialization;

namespace Stats.Configuration
{
    public class ConfigurationService
    {
        public ConfigurationService(string configurationFileFullName)
        {
            this.ConfigurationFileFullName = configurationFileFullName;
        }

        public string ConfigurationFileFullName { get; }

        public ConfigurationDto Load()
        {
            var serializer = new XmlSerializer(typeof(ConfigurationDto));
            using (var streamReader = new StreamReader(ConfigurationFileFullName))
            {
                return (ConfigurationDto)serializer.Deserialize(streamReader);
            }
        }

        public void Save(ConfigurationDto configuration)
        {
            var serializer = new XmlSerializer(typeof(ConfigurationDto));
            using (var streamWriter = new StreamWriter(ConfigurationFileFullName))
            {
                serializer.Serialize(streamWriter, configuration);
            }
        }
    }
}
