using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Library;

namespace ImprovedNPCAttributeDistribution.Patches
{
    [HarmonyPatch(typeof(HeroDeveloper), "SetupDefaultPoints")]
    internal class SetupDefaultPointsPatch
    {
        private static int DefaultAttributePointsAtTheStart = 15;
        // A property to determine the attribute points at the start based on settings
        private static int AttributePointsAtTheStart
        {
            get
            {
                if (Settings.Instance != null && Settings.AttributePointsMap.TryGetValue(Settings.Instance.BaseAttributePoints.SelectedValue, out int value))
                {
                    return value;
                }
                // If settings are not available, fall back to the vanilla attribute points value
                return Campaign.Current.Models.CharacterDevelopmentModel.AttributePointsAtStart;
            }
        }

        public static bool Prefix(HeroDeveloper __instance)
        {
            __instance.UnspentFocusPoints = (__instance.Hero.Level - 1) * Campaign.Current.Models.CharacterDevelopmentModel.FocusPointsPerLevel + Campaign.Current.Models.CharacterDevelopmentModel.FocusPointsAtStart;

            // Calculate unspent attribute points based on hero's level and custom or default attribute points value at level 1
            // Note: In the base game, it uses Hero.Level - 1, but it is corrected here for every n-Levels, instead of every n-Levels + 1, hence no subtraction.
            var unspentAttributePoints = __instance.Hero.Level / Campaign.Current.Models.CharacterDevelopmentModel.LevelsPerAttributePoint 
                + Campaign.Current.Models.CharacterDevelopmentModel.AttributePointsAtStart - DefaultAttributePointsAtTheStart // fix if AttributePointsAtStart gets modified, to still return the correct amount
                + AttributePointsAtTheStart;

            __instance.UnspentAttributePoints = MathF.Max(unspentAttributePoints, 0); // Make sure, we stay greater or equal than 0.

            // Indicate that the original method should not be executed (return false)
            return false;
        }
    }
}
