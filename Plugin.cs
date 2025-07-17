using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;

namespace CustomLevelNames
{
    [BepInPlugin(mGUID, mName, mVersion)]
    public class CustomLevelNamesMod : BaseUnityPlugin
    {
        const string mGUID = "eXish.CustomLevelNames";
        const string mName = "CustomLevelNames";
        const string mVersion = "1.0.6";

        readonly Harmony harmony = new Harmony(mGUID);

        internal static CustomLevelNamesMod instance;
        internal static ManualLogSource log;
        internal static Dictionary<string, string> storedLevelNames = new Dictionary<string, string>();
        internal static bool doInitialSetup = true;
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
