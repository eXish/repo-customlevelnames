using HarmonyLib;

namespace CustomLevelNames.Patches
{
    [HarmonyPatch(typeof(LevelGenerator))]
    internal class LevelGeneratorPatch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        static void AwakePatch()
        {
            if (CustomLevelNamesMod.doInitialSetup && RunManager.instance != null)
            {
                CustomLevelNamesMod.doInitialSetup = false;
                string[] allLevels = new string[RunManager.instance.levels.Count];
                for (int i = 0; i < allLevels.Length; i++)
                {
                    allLevels[i] = RunManager.instance.levels[i].NarrativeName + " (" + RunManager.instance.levels[i].ResourcePath + ")";
                    CustomLevelNamesMod.storedLevelNames.Add(RunManager.instance.levels[i].NarrativeName, RunManager.instance.levels[i].NarrativeName.ToUpper());
                }
                CustomLevelNamesMod.storedLevelNames.Add("Disposal Arena", "DISPOSAL ARENA");
                CustomLevelNamesMod.storedLevelNames.Add("Service Station", "SERVICE STATION");
                CustomLevelNamesMod.log.LogInfo($"Found {allLevels.Length} extraction levels: {allLevels.Join()}");
            }
        }
    }
}