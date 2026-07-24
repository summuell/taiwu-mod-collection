using TaiwuModdingLib.Core.Plugin;

namespace EasyFeatureExchange;

[PluginConfig("EasyFeatureExchange.Frontend", "EasyFeatureExchange", "1.0.0")]
public sealed class Plugin : TaiwuRemakePlugin
{
    public override void Initialize()
    {
        HarmonyPatches.Install(ModIdStr);
    }

    public override void Dispose()
    {
        HarmonyPatches.Uninstall();
    }

    public override void OnModSettingUpdate()
    {
    }
}
