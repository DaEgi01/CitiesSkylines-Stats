namespace Stats
{
    public class ModFullTitle
    {
        private readonly string _modName;
        private readonly string _modVersion;

        public ModFullTitle(string modName, string modVersion)
        {
            _modName = modName ?? throw new System.ArgumentNullException(nameof(modName));
            _modVersion = modVersion ?? throw new System.ArgumentNullException(nameof(modVersion));
        }

        public static implicit operator string(ModFullTitle modFullTitle)
        {
            return modFullTitle._modName + " v" + modFullTitle._modVersion;
        }
    }
}
