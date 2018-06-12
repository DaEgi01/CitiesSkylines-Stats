namespace Stats
{
    public class ModFullTitle
    {
        private readonly string modName;
        private readonly string modVersion;

        public ModFullTitle(string modName, string modVersion)
        {
            this.modName = modName ?? throw new System.ArgumentNullException(nameof(modName));
            this.modVersion = modVersion ?? throw new System.ArgumentNullException(nameof(modVersion));
        }

        public static implicit operator string(ModFullTitle modFullTitle)
        {
            return modFullTitle.modName + " v" + modFullTitle.modVersion;
        }
    }
}
