using System.IO;
using System.Xml.Serialization;

namespace Stats.Configuration
{
    public class ConfigurationService
    {
        private readonly string configurationFileFullName;

        public ConfigurationService(string configurationFileFullName)
        {
            this.configurationFileFullName = configurationFileFullName;
        }

        public ConfigurationDto Load()
        {
            var serializer = new XmlSerializer(typeof(ConfigurationDto));
            using (var streamReader = new StreamReader(configurationFileFullName))
            {
                return (ConfigurationDto)serializer.Deserialize(streamReader);
            }
        }

        public void Save(ConfigurationDto configuration)
        {
            var serializer = new XmlSerializer(typeof(ConfigurationDto));
            using (var streamWriter = new StreamWriter(configurationFileFullName))
            {
                serializer.Serialize(streamWriter, configuration);
            }
        }
    }
}
