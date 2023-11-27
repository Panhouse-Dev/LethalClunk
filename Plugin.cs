using BepInEx;
using HarmonyLib;
using System.IO;

namespace LethalClunk
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Lethal Company.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_NAME);
        private readonly BepInEx.Logging.ManualLogSource logger;

        internal static Plugin? Instance;

        public Plugin()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            harmony.PatchAll();
            logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
