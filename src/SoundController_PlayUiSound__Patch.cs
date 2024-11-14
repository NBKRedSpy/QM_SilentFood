using HarmonyLib;
using MGSC;
using System;
using UnityEngine;

namespace QM_SilentFood
{
    //[HarmonyPatch(typeof(SoundController), nameof(SoundController.PlayUISound), new Type[] { typeof(AudioClip), typeof(bool), typeof(float) })]


    [HarmonyPatch(typeof(SoundController), nameof(SoundController.PlayUiSound))]
    [HarmonyPatch(new Type[] { typeof(AudioClip), typeof(bool), typeof(float) })]
    public static class SoundController_PlayUiSound__Patch
    {
        public static void Prefix()
        {
            if (!Plugin.IsInited)
            {
                //Was unable to set after the config was loaded, so doing it here.
                Plugin.IsInited = true;

                if (Plugin.Config.SilenceEating) SingletonMonoBehaviour<SoundsStorage>.Instance.EatSound = null;
                if (Plugin.Config.SilenceDrinking) SingletonMonoBehaviour<SoundsStorage>.Instance.DrinkSound = null;
            }

        }
    }
}
