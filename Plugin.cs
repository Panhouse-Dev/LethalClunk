using BepInEx;
using HarmonyLib;

namespace LcMetalBarAxle
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Lethal Company.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_NAME);
        private readonly BepInEx.Logging.ManualLogSource logger;

        public Plugin()
        {
            logger = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);
        }

        public void Awake()
        {
            harmony.PatchAll();
            logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
