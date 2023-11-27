using System.IO;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;

namespace LcMetalBarAxle.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]
    internal class FlashlightBopItPatch
    {
        private static string clipName = "metal_bar.wav";
        private static string path = Path.Combine(Paths.PluginPath + $"\\{PluginInfo.PLUGIN_NAME}\\");
        private static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static async void ReplaceLargeAxleSFX(GrabbableObject __instance)
        {
            var item = __instance.itemProperties;
            if (item.itemName == "Large axle")
            {
                AudioClip audioClip = await LoadAudioClip(path + clipName);
                if (audioClip != null) {
                    item.dropSFX = audioClip;
                }
            }
        }

        private static async Task<AudioClip> LoadAudioClip(string filePath)
        {
            UnityWebRequest audioClipReq = UnityWebRequestMultimedia.GetAudioClip(filePath, AudioType.WAV);
            await audioClipReq.SendWebRequest();
            if (audioClipReq.error != null) {
                logger.LogError(audioClipReq.error);
                logger.LogError($"Failed to load audio clip for {PluginInfo.PLUGIN_NAME}, sound will not be replaced");
                return null;
            }
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(audioClipReq);
            audioClip.name = Path.GetFileName(filePath);
            return audioClip;
        }
    }
}