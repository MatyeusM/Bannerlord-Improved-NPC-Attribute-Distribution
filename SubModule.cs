using HarmonyLib;
using TaleWorlds.MountAndBlade;


namespace ImprovedNPCAttributeDistribution
{
    /// SubModule class is the entry point for the mod and extends MBSubModuleBase.
    public class SubModule : MBSubModuleBase
    {
        /// Called when the mod is loaded.
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            new Harmony("bannerlord.attrfix").PatchAll();
        }

        /// Called when the mod is unloaded.
        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        /// Called before the initial module screen is set as root.
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }
    }
}