using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using GameData.Common;
using GameData.Domains.Taiwu.Profession;

namespace EasyFeatureExchange.Backend;

[PluginConfig("EasyFeatureExchange.Backend", "EasyFeatureExchange", "1.0.0")]
public sealed class BackendPlugin : TaiwuRemakePlugin
{
    public override void Initialize()
    {
        BackendPatches.Install(ModIdStr);
    }

    public override void Dispose()
    {
        BackendPatches.Uninstall();
    }

    public override void OnModSettingUpdate()
    {
    }
}

internal static class BackendPatches
{
    private static Harmony? s_harmony;

    internal static void Install(string modId)
    {
        try
        {
            s_harmony = new Harmony(modId + ".Backend");

            var target = AccessTools.Method(
                typeof(ProfessionSkillHandle),
                "TravelingTaoistMonkSkill2",
                new[] { typeof(DataContext), typeof(ProfessionData), typeof(ProfessionSkillArg) }
            );

            if (target == null) return;

            var prefix = AccessTools.Method(typeof(BackendPatches), nameof(TravelingTaoistMonkSkill2_Prefix));
            if (prefix == null) return;

            s_harmony.Patch(target, new HarmonyMethod(prefix));
        }
        catch
        {
            // Silently handle - the error will show in logs if the patch fails
        }
    }

    internal static void Uninstall()
    {
        s_harmony?.UnpatchSelf();
    }

    private static bool TravelingTaoistMonkSkill2_Prefix(DataContext context, ProfessionData professionData, ProfessionSkillArg arg)
    {
        arg.BookIds ??= new List<int>();
        arg.EffectBlocks ??= new List<short>();
        return true;
    }
}
