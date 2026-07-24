using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using Game.Views.Profession;
using GameData.Domains.Taiwu.Profession;

namespace EasyFeatureExchange;

public static class HarmonyPatches
{
    private static Harmony? _harmony;

    public static void Install(string modId)
    {
        _harmony = new Harmony(modId);
        _harmony.PatchAll(typeof(HarmonyPatches));
        Debug.Log($"[EasyFeatureExchange] Harmony patches installed (modId: {modId})");
    }

    public static void Uninstall()
    {
        _harmony?.UnpatchSelf();
        Debug.Log("[EasyFeatureExchange] Harmony patches uninstalled");
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(ViewTravelingTaoistMonkSkill2), "UpdateExchangeBtnState")]
    public static void UpdateExchangeBtnState_Postfix(ViewTravelingTaoistMonkSkill2 __instance, bool confirmed)
    {
        if (confirmed) return;

        var t = Traverse.Create(__instance);
        var taiwuFeatures = t.Field("_taiwuFeaturesTobeExchange").GetValue<List<int>>();
        var targetFeatures = t.Field("_targetChrFeaturesTobeExchange").GetValue<List<int>>();
        var previewHealth = t.Field("_previewLeftMaxHealth").GetValue<short>();
        var confirmBtn = t.Field("confirmBtn").GetValue<Button>();

        if (confirmBtn == null || confirmBtn.interactable) return;

        bool hasSelection = (taiwuFeatures != null && taiwuFeatures.Count > 0) ||
                            (targetFeatures != null && targetFeatures.Count > 0);

        if (hasSelection && previewHealth > 0)
        {
            // Replace tooltip with neutral text
            var tooltip = confirmBtn.GetComponent<TooltipInvoker>();
            if (tooltip != null)
            {
                if (tooltip.RuntimeParam != null)
                {
                    tooltip.RuntimeParam.Set("arg0", "选择完成，可交换特性");
                }
                else
                {
                    tooltip.enabled = false;
                }
            }
            confirmBtn.interactable = true;
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ProfessionSkillArg), "Serialize")]
    public static void Serialize_Prefix(ProfessionSkillArg __instance)
    {
        if (__instance.BookIds == null)
            __instance.BookIds = new List<int>();
        if (__instance.EffectBlocks == null)
            __instance.EffectBlocks = new List<short>();
    }
}
