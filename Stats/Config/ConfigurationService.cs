using System;
using System.IO;
using System.Xml.Serialization;

namespace Stats.Config;

public sealed class ConfigurationService<T>
{
    private readonly XmlSerializer _xmlSerializer = new (typeof(T));

    public ConfigurationService(string configurationFileFullName)
    {
        if (configurationFileFullName is null)
            throw new ArgumentNullException(nameof(configurationFileFullName));

        ConfigurationFileFullName = configurationFileFullName;
    }

    public string ConfigurationFileFullName { get; }

    public T Load()
    {
        using var streamReader = new StreamReader(ConfigurationFileFullName);
        return (T)_xmlSerializer.Deserialize(streamReader);
    }

    public void Save(T configuration)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        using var streamWriter = new StreamWriter(ConfigurationFileFullName);
        _xmlSerializer.Serialize(streamWriter, configuration);
    }
}
