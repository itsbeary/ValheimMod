using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace BearyValheim
{
    [BepInPlugin("beary.BearyValheim", "Beary Valheim", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class ValheimMod : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("beary.BearyValheim");

        void Awake()
        {
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Character), nameof(Character.Jump))]
        class Jump_Patch
        {
            static void Prefix(ref float ___m_jumpForce, ref float ___m_jumpStaminaUsage)
            {
                Debug.Log($"Jump force: {___m_jumpForce}");
                ___m_jumpStaminaUsage = 0;
                ___m_jumpForce = 50;
                Debug.Log($"Modified jump force: {___m_jumpForce}");
            }
        }
        [HarmonyPatch(typeof(Character), nameof(Character.SetHealth))]
        class Health_Patch
        {
            static void Prefix(ref float health)
            {
                health = 2;
            }
        }

        [HarmonyPatch(typeof(Character), nameof(Character.HaveStamina))]
        public static void HaveStamina(ref bool amount)
        {
            amount = true;
        }
    }
}