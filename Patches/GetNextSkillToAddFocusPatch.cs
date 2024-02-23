using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.GameComponents;
using HarmonyLib;
using ImprovedNPCAttributeDistribution.Extensions;

namespace ImprovedNPCAttributeDistribution.Patches
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), nameof(DefaultCharacterDevelopmentModel.GetNextSkillToAddFocus))]
    internal class GetNextSkillToAddFocusPatch
    {
        public static bool Prefix(Hero hero, ref DefaultCharacterDevelopmentModel __instance, ref SkillObject? __result)
        {
            SkillObject? result = null;
            float maxScore = float.MinValue;

            // Iterate through all skills to find the one with the highest score
            foreach (SkillObject skillObject in Skills.All)
            {
                // Skip to the next iteration if we can't add focus points
                if (!hero.HeroDeveloper.CanAddFocusToSkill(skillObject))
                    continue;

                // Calculate the score for the skill, simple distance check. No need to square in a 1-dimensional environment
                float score = __instance.GetSkillLearningLimitDistance(hero, skillObject);
                if (score > maxScore) // update if higher
                {
                    maxScore = score;
                    result = skillObject;
                }
            }

            bool wanderersLeaveNotNeededFocusPoints = Settings.Instance?.WanderersLeaveNotNeededFocusPointsForPlayers ?? false;
            // Return null, thus skip any spending of further points if the option is enabled and if wanderers are already above the learning limit.
            __result = wanderersLeaveNotNeededFocusPoints && hero.IsWanderer && maxScore < 0 ? null : result;
            return false;
        }
    }
}
