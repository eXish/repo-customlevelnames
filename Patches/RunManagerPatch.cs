using HarmonyLib;

namespace CustomLevelNames.Patches
{
    [HarmonyPatch(typeof(RunManager))]
    internal class RunManagerPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdatePatch(RunManager __instance)
        {
            if (CustomLevelNamesMod.doInitialSetup && GameDirector.instance.currentState == GameDirector.gameState.Main)
            {
                CustomLevelNamesMod.doInitialSetup = false;
                string[] allLevels = new string[__instance.levels.Count];
                for (int i = 0; i < allLevels.Length; i++)
                {
                    allLevels[i] = __instance.levels[i].NarrativeName + " (" + __instance.levels[i].ResourcePath + ")";
                    CustomLevelNamesMod.storedLevelNames.Add(__instance.levels[i].NarrativeName, __instance.levels[i].NarrativeName.ToUpper());
                }
                CustomLevelNamesMod.storedLevelNames.Add("Disposal Arena", "DISPOSAL ARENA");
                CustomLevelNamesMod.storedLevelNames.Add("Service Station", "SERVICE STATION");
                CustomLevelNamesMod.log.LogInfo($"Found {allLevels.Length} extraction levels: {allLevels.Join()}");
            }
        }
    }
}