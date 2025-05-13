using BepInEx.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace CustomLevelNames
{
    internal class ConfigManager
    {
        public static ConfigEntry<bool> shuffleEachLevel;

        public static ConfigEntry<string> levelNames;
        public static ConfigEntry<string> shopName;
        public static ConfigEntry<string> arenaName;

        public static void Init()
        {
            shuffleEachLevel = CustomLevelNamesMod.instance.Config.Bind("General Settings", "shuffleEachLevel", false, "Shuffles custom names after each individual level instead of each full game.");

            levelNames = CustomLevelNamesMod.instance.Config.Bind("Name Settings", "levelNames", "", "The set of custom names for extraction levels. Documentation for this setting can be found in the README.");
            shopName = CustomLevelNamesMod.instance.Config.Bind("Name Settings", "shopName", "", "The set of custom names for the shop level. Documentation for this setting can be found in the README.");
            arenaName = CustomLevelNamesMod.instance.Config.Bind("Name Settings", "arenaName", "", "The set of custom names for the arena level. Documentation for this setting can be found in the README.");
        }

        public static List<string> GetLevelNames(string level)
        {
            List<string> newNames = new List<string>();
            if (level == "Arena" && arenaName.Value != "")
                newNames = arenaName.Value.ToUpper().Split(';').ToList();
            else if (level == "Shop" && shopName.Value != "")
                newNames = shopName.Value.ToUpper().Split(';').ToList();
            else
            {
                string[] nameSearch = levelNames.Value.Split(',').Where(x => x.ToUpper().StartsWith(level.ToUpper() + ":")).ToArray();
                for (int i = 0; i < nameSearch.Length; i++)
                {
                    nameSearch[i] = nameSearch[i].ToUpper().Replace(level.ToUpper() + ":", "");
                    string[] splitSearch = nameSearch[i].Split(';');
                    for (int j = 0; j < splitSearch.Length; j++)
                        newNames.Add(splitSearch[j]);
                }
            }
            return newNames;
        }
    }
}
