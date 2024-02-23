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
        // Default attribute distribution model to the Euclidean variant.
        private static AttributeDistributionModel defaultAttributeDistributionModel = new EuclideanAttributeDistributionModel();

        // Property to get the current attribute distribution model based on mod settings
        private static AttributeDistributionModel CurrentAttributeDistributionModel
        {
            get
            {
                // Check if the settings instance is not null and retrieve the attribute distribution model value
                if (Settings.Instance != null && AttributeDistributionModels.Models.TryGetValue(Settings.Instance.AttributePointSpendingModel.SelectedValue, out AttributeDistributionModel? value))
                {
                    // Return the value if it is not null, otherwise fallback to the default attribute distribution model
                    if (value != null) return value;
                }
                return defaultAttributeDistributionModel;
            }
        }

        public static bool Prefix(Hero hero, ref DefaultCharacterDevelopmentModel __instance, ref CharacterAttribute __result)
        {
            // Call the GetNextAttributeToUpgrade method of the current attribute distribution model
            __result = CurrentAttributeDistributionModel.GetNextAttributeToUpgrade(hero, __instance);
            return false;
        }
    }
}
