using System.IO;
using System.Xml.Serialization;

namespace Stats.Config
{
    public class ConfigurationService<T>
    {
        public ConfigurationService(string configurationFileFullName)
        {
            this.ConfigurationFileFullName = configurationFileFullName;
        }

        public string ConfigurationFileFullName { get; }

        public T Load()
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var streamReader = new StreamReader(ConfigurationFileFullName))
            {
                return (T)serializer.Deserialize(streamReader);
            }
        }

        public void Save(T configuration)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var streamWriter = new StreamWriter(ConfigurationFileFullName))
            {
                serializer.Serialize(streamWriter, configuration);
            }
        }
    }
}
