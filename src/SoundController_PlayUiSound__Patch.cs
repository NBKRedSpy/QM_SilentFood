using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QM_SilentFood
{
    //[HarmonyPatch(typeof(SoundController), nameof(SoundController.PlayUISound), new Type[] { typeof(AudioClip), typeof(bool), typeof(float) })]

    [HarmonyPatch(typeof(SoundController), nameof(SoundController.PlayUiSound))]
    [HarmonyPatch(new Type[] { typeof(AudioClip), typeof(bool), typeof(float) })]
    public static class SoundController_PlayUiSound__Patch
    {
        /// <summary>
        /// Key: Name of clip, Value: true if it is an eat sound.
        /// </summary>
        private static Dictionary<string, bool> SilentAudio = new Dictionary<string, bool>()
        {
            { "FoodFood", true },
            { "FoodDrinkNoGas", false},
            { "FoodDrink", false }
        };

        public static bool Prefix(AudioClip clip)
        {

            //Debug.LogWarning($"Audio Clip {clip.name}");


            if(SilentAudio.TryGetValue(clip.name, out bool isEating))
            {
                if (isEating && Plugin.Config.SilenceEating) return false;
                else if (Plugin.Config.SilenceDrinking) return false;
            }

            return true;
        }
    }
}
