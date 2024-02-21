using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

/* This is just plain wrong TaleWorlds on the UnspentAttributePoints...
 *  private void SetupDefaultPoints()
 *  {
 *	    this.UnspentFocusPoints = (this.Hero.Level - 1) * Campaign.Current.Models.CharacterDevelopmentModel.FocusPointsPerLevel + Campaign.Current.Models.CharacterDevelopmentModel.FocusPointsAtStart;
 *	    this.UnspentAttributePoints = (this.Hero.Level - 1) / Campaign.Current.Models.CharacterDevelopmentModel.LevelsPerAttributePoint + Campaign.Current.Models.CharacterDevelopmentModel.AttributePointsAtStart;
 *  }
 */

namespace ImprovedNPCAttributeDistribution.Patches
{
    // Harmony Patch on TaleWorlds.CampaignSystem.CharacterDevelopment.HeroDeveloper.SetupDefaultPoints
    [HarmonyPatch(typeof(HeroDeveloper), "SetupDefaultPoints")]
    internal class SetupDefaultPointsPatch
    {
        private static int AttributePointsAtTheStart
        {
            get
            {
                if (Settings.Instance != null && Settings.AttributePointsMap.TryGetValue(Settings.Instance.BaseAttributePoints.SelectedValue, out int value))
                {
                    return value;
                }
                return Campaign.Current.Models.CharacterDevelopmentModel.AttributePointsAtStart;
            }
        }

        public static bool Prefix(HeroDeveloper __instance)
        {
            __instance.UnspentFocusPoints = (__instance.Hero.Level - 1) * Campaign.Current.Models.CharacterDevelopmentModel.FocusPointsPerLevel + Campaign.Current.Models.CharacterDevelopmentModel.FocusPointsAtStart;
            __instance.UnspentAttributePoints = __instance.Hero.Level / Campaign.Current.Models.CharacterDevelopmentModel.LevelsPerAttributePoint + AttributePointsAtTheStart;
            return false;
        }
    }
}
