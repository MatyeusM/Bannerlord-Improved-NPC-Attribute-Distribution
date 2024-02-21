using HarmonyLib;
using ImprovedNPCAttributeDistribution.DistributionModels;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;

namespace ImprovedNPCAttributeDistribution.Patches
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), nameof(DefaultCharacterDevelopmentModel.GetNextAttributeToUpgrade))]
    internal class GetNextAttributeToUpgradePatch
    {
        private static AttributeDistributionModel defaultAttributeDistributionModel = new EuclideanAttributeDistributionModel();

        private static AttributeDistributionModel CurrentAttributeDistributionModel
        {
            get
            {
                if (Settings.Instance != null && AttributeDistributionModels.Models.TryGetValue(Settings.Instance.AttributePointSpendingModel.SelectedValue, out AttributeDistributionModel? value))
                {
                    if (value != null) return value;
                }
                return defaultAttributeDistributionModel;
            }
        }

        public static bool Prefix(Hero hero, ref DefaultCharacterDevelopmentModel __instance, ref CharacterAttribute __result)
        {
            __result = CurrentAttributeDistributionModel.GetNextAttributeToUpgrade(hero, __instance);
            return false;
        }
    }
}
