namespace Stats
{
    public class GameEngineService
    {
        public bool CheckIfMapHasSnowDumps()
        {
            var result = false;

            for (uint i = 0; i < PrefabCollection<BuildingInfo>.LoadedCount(); i++)
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
