using HarmonyLib;
using MCM.Implementation;
using MCM.Internal.Extensions;
using TaleWorlds.MountAndBlade;


namespace ImprovedNPCAttributeDistribution
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            new Harmony("bannerlord.attrfix").PatchAll();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

        }
    }
}