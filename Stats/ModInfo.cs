using System;

namespace Stats
{
    public class ModInfo
    {
        private readonly string _systemName;
        private readonly string _displayName;
        private readonly string _version;
        private readonly ulong _workshopId;

        public ModInfo(string systemName, string displayName, string version, ulong workshopId)
        {
            _systemName = systemName ?? throw new ArgumentException(nameof(systemName));
            _displayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            _version = version ?? throw new ArgumentNullException(nameof(version));
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
