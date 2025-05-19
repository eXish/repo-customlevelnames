using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLevelNames.Patches
{
    [HarmonyPatch(typeof(RunManager))]
    internal class RunManagerPatch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        static void AwakePatch(RunManager __instance)
        {
            if ((SemiFunc.RunIsLevel() || SemiFunc.RunIsArena() || SemiFunc.RunIsShop()) && CustomLevelNamesMod.readyToShuffle)
            {
                bool didShuffle = false;
                CustomLevelNamesMod.log.LogInfo("Shuffling all level names...");
                List<string> arenaNames = ConfigManager.GetLevelNames("Arena");
                List<string> shopNames = ConfigManager.GetLevelNames("Shop");
                if (arenaNames.Count > 0)
                {
                    string oldName = __instance.levelArena.NarrativeName.ToUpper();
                    __instance.levelArena.NarrativeName = arenaNames[Random.Range(0, arenaNames.Count)];
                    if (oldName != __instance.levelArena.NarrativeName)
                    {
                        didShuffle = true;
                        CustomLevelNamesMod.log.LogInfo($"{oldName} is now known as {__instance.levelArena.NarrativeName}.");
                    }
                }
                if (shopNames.Count > 0)
                {
                    string oldName = __instance.levelShop.NarrativeName.ToUpper();
                    __instance.levelShop.NarrativeName = shopNames[Random.Range(0, shopNames.Count)];
                    if (oldName != __instance.levelShop.NarrativeName)
                    {
                        didShuffle = true;
                        CustomLevelNamesMod.log.LogInfo($"{oldName} is now known as {__instance.levelShop.NarrativeName}.");
                    }
                }
                for (int i = 0; i < __instance.levels.Count; i++)
                {
                    List<string> levelNames = ConfigManager.GetLevelNames(__instance.levels[i].ResourcePath);
                    if (levelNames.Count > 0)
                    {
                        string oldName = __instance.levels[i].NarrativeName.ToUpper();
                        __instance.levels[i].NarrativeName = levelNames[Random.Range(0, levelNames.Count)];
                        if (oldName != __instance.levels[i].NarrativeName)
                        {
                            didShuffle = true;
                            CustomLevelNamesMod.log.LogInfo($"{oldName} is now known as {__instance.levels[i].NarrativeName}.");
                        }
                    }
                }
                if (!didShuffle)
                    CustomLevelNamesMod.log.LogInfo("No level names were changed.");
                if (!ConfigManager.shuffleEachLevel.Value)
                    CustomLevelNamesMod.readyToShuffle = false;
            }
            if (SemiFunc.RunIsArena())
                CustomLevelNamesMod.readyToShuffle = true;
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdatePatch(RunManager __instance)
        {
            if (CustomLevelNamesMod.logLoadedLevels && GameDirector.instance.currentState == GameDirector.gameState.Main)
            {
                CustomLevelNamesMod.logLoadedLevels = false;
                string[] allLevels = new string[__instance.levels.Count];
                for (int i = 0; i < allLevels.Length; i++)
                    allLevels[i] = __instance.levels[i].NarrativeName + " (" + __instance.levels[i].ResourcePath + ")";
                CustomLevelNamesMod.log.LogInfo($"Found {allLevels.Length} extraction levels: {allLevels.Join()}");
            }
        }
    }
}