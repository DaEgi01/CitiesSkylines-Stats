using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Stats.Icons
{
    public class IconSetService
    {
        public ReadOnlyCollection<IconSet> LoadIconSets(string iconSetsDirectoryPath)
        {
            var result = new List<IconSet>();

            var iconSetDirectories = Directory.GetDirectories(iconSetsDirectoryPath);
            foreach (var iconSetDirectory in iconSetDirectories)
            {
                result.Add(LoadIconSet(iconSetDirectory));
            }

            return new ReadOnlyCollection<IconSet>(result);
        }

        private IconSet LoadIconSet(string iconSetDirectoryPath)
        {
            var iconSetMetaData = iconSetDirectoryPath.Split('-');
            var iconSetAuthor = iconSetMetaData[0].Trim();
            var iconSetName = iconSetMetaData[1].Trim();
            var icons = Directory.GetFiles(iconSetDirectoryPath, "*.png");
            
            return new IconSet(iconSetAuthor, iconSetName, icons);
        }
    }
}
