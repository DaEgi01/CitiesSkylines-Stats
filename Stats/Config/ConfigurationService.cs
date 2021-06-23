using System;
using System.IO;
using System.Xml.Serialization;

namespace Stats.Config
{
    public class ConfigurationService<T>
    {
        private readonly XmlSerializer _xmlSerializer = new (typeof(T));

        public ConfigurationService(string configurationFileFullName)
        {
            ConfigurationFileFullName = configurationFileFullName
                ?? throw new ArgumentNullException(nameof(configurationFileFullName));
        }

        public string ConfigurationFileFullName { get; }

        public T Load()
        {
            using var streamReader = new StreamReader(ConfigurationFileFullName);
            return (T)_xmlSerializer.Deserialize(streamReader);
        }

        public void Save(T configuration)
        {
            using var streamWriter = new StreamWriter(ConfigurationFileFullName);
            _xmlSerializer.Serialize(streamWriter, configuration);
        }
    }
}
