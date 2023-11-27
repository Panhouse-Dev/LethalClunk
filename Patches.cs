using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;

namespace LethalClunk.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]
    internal class FlashlightBopItPatch
    {
        private static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static async void ReplaceLargeAxleSFX(GrabbableObject __instance)
        {
            var item = __instance.itemProperties;
            var audioClip = await LoadAudioClip("metal_bar.wav");
            if (item.itemName == "Large axle")
            {
                if (audioClip != null) { item.dropSFX = audioClip; }
            }
        }

        private static async Task<AudioClip?> LoadAudioClip(string clipName)
        {
            var fullPath = GetAssemblyFullPath(clipName);
            UnityWebRequest audioClipReq = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.WAV);
            await audioClipReq.SendWebRequest();
            if (audioClipReq.error != null) {
                logger.LogError(audioClipReq.error);
                logger.LogError($"Failed to load audio clip for {PluginInfo.PLUGIN_NAME}, sound will not be replaced");
                return null;
            }
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(audioClipReq);
            audioClip.name = Path.GetFileName(fullPath);
            return audioClip;
        }

        // TODO: Move to common code
        private static string GetAssemblyFullPath(string? additionalPath)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var relativePath = additionalPath != null ? Path.Combine(assemblyPath, @$".\{additionalPath}") : assemblyPath;
            return Path.GetFullPath(relativePath);
        }
    }
}