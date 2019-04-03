namespace Stats
{
    public class GameEngineService
    {
        public bool CheckIfMapHasSnowDumps()
        {
            var result = false;

            var loadedCount = PrefabCollection<BuildingInfo>.LoadedCount();
            for (uint i = 0; i < loadedCount; i++)
            {
                var bi = PrefabCollection<BuildingInfo>.GetLoaded(i);
                if (bi != null && bi.m_buildingAI is SnowDumpAI)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}
