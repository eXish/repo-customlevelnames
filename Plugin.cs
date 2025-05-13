using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace CustomLevelNames
{
    [BepInPlugin(mGUID, mName, mVersion)]
    public class CustomLevelNamesMod : BaseUnityPlugin
    {
        const string mGUID = "eXish.CustomLevelNames";
        const string mName = "CustomLevelNames";
        const string mVersion = "1.0.0";

        readonly Harmony harmony = new Harmony(mGUID);

        internal static CustomLevelNamesMod instance;
        internal static ManualLogSource log;
        internal static bool logLoadedLevels = true;
        internal static bool readyToShuffle = true;

        void Awake()
        {
            if (instance == null)
                instance = this;

            log = Logger;

            ConfigManager.Init();

            harmony.PatchAll();

            log.LogInfo($"{mName}-{mVersion} loaded!");
        }
    }
}
