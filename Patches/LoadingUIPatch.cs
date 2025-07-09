using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLevelNames.Patches
{
    [HarmonyPatch(typeof(LoadingUI))]
    internal class LoadingUIPatch
    {
        [HarmonyPatch("LevelAnimationStart")]
        [HarmonyPostfix]
        static void LevelAnimationStartPatch(LoadingUI __instance)
        {
            if ((SemiFunc.RunIsLevel() || SemiFunc.RunIsArena() || SemiFunc.RunIsShop()) && CustomLevelNamesMod.readyToShuffle)
            {
                bool didShuffle = false;
                CustomLevelNamesMod.log.LogInfo("Shuffling all level names...");
                List<string> arenaNames = ConfigManager.GetLevelNames("Arena");
                List<string> shopNames = ConfigManager.GetLevelNames("Shop");
                if (arenaNames.Count > 0)
                {
                    string oldName = CustomLevelNamesMod.storedLevelNames["Disposal Arena"];
                    CustomLevelNamesMod.storedLevelNames["Disposal Arena"] = arenaNames[Random.Range(0, arenaNames.Count)];
                    if (oldName != CustomLevelNamesMod.storedLevelNames["Disposal Arena"])
                    {
                        didShuffle = true;
                        CustomLevelNamesMod.log.LogInfo($"{oldName} is now known as {CustomLevelNamesMod.storedLevelNames["Disposal Arena"]}.");
                    }
                }
                if (shopNames.Count > 0)
                {
                    string oldName = CustomLevelNamesMod.storedLevelNames["Service Station"];
                    CustomLevelNamesMod.storedLevelNames["Service Station"] = shopNames[Random.Range(0, shopNames.Count)];
                    if (oldName != CustomLevelNamesMod.storedLevelNames["Service Station"])
                    {
                        didShuffle = true;
                        CustomLevelNamesMod.log.LogInfo($"{oldName} is now known as {CustomLevelNamesMod.storedLevelNames["Service Station"]}.");
                    }
                }
                for (int i = 0; i < RunManager.instance.levels.Count; i++)
                {
                    List<string> levelNames = ConfigManager.GetLevelNames(RunManager.instance.levels[i].ResourcePath);
                    if (levelNames.Count > 0)
                    {
                        string oldName = CustomLevelNamesMod.storedLevelNames[RunManager.instance.levels[i].NarrativeName];
                        CustomLevelNamesMod.storedLevelNames[RunManager.instance.levels[i].NarrativeName] = levelNames[Random.Range(0, levelNames.Count)];
                        if (oldName != CustomLevelNamesMod.storedLevelNames[RunManager.instance.levels[i].NarrativeName])
                        {
                            didShuffle = true;
                            CustomLevelNamesMod.log.LogInfo($"{oldName} is now known as {CustomLevelNamesMod.storedLevelNames[RunManager.instance.levels[i].NarrativeName]}.");
                        }
                    }
                }
                if (!didShuffle)
                    CustomLevelNamesMod.log.LogInfo("No level names were changed.");
                if (!ConfigManager.shuffleEachLevel.Value)
                    CustomLevelNamesMod.readyToShuffle = false;
            }
            __instance.levelNameText.text = CustomLevelNamesMod.storedLevelNames[RunManager.instance.levelCurrent.NarrativeName];
            if (SemiFunc.RunIsArena())
                CustomLevelNamesMod.readyToShuffle = true;
        }
    }
}
