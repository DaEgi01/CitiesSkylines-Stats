namespace Stats
{
    using System;
    using System.IO;
    using System.Linq;

    public class ModInfo
    {
        private readonly string _systemName;
        private readonly string _displayName;
        private readonly string _version;
        private readonly ulong _workshopId;

        public ModInfo(string systemName, string displayName, string version, ulong workshopId)
        {
            if (systemName is null)
                throw new ArgumentNullException(nameof(systemName));
            if (displayName is null)
                throw new ArgumentNullException(nameof(displayName));
            if (version is null)
                throw new ArgumentNullException(nameof(version));

            var systemNameCharArray = systemName.ToCharArray();
            var systemNameHasInvalidPathChars = Path.GetInvalidPathChars()
                .Any(c => systemNameCharArray.Contains(c));
            if (systemNameHasInvalidPathChars)
                throw new ArgumentException("Please only use valid path characters.", nameof(systemName));

            _systemName = systemName;
            _displayName = displayName;
            _version = version;
            _workshopId = workshopId;
        }

        public string SystemName => _systemName;
        public string DisplayName => _displayName;
        public string Version => _version;
        public ulong WorkshopId => _workshopId;

        public string GetDisplayNameWithVersion()
        {
            return _displayName + " v" + _version;
        }
    }
}
