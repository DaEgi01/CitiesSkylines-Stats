namespace Stats.Localization
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("LanguageResource")]
    public class LanguageResourceDto
    {
        public string? LanguageTwoLetterCode { get; set; }
        public string? Version { get; set; }
        [XmlArray("LocalizedItems")]
        [XmlArrayItem("LocalizedItem")]
        public List<LanguageResourceItemDto>? LocalizedItems { get; set; }
    }
}
