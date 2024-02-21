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

            foreach (SkillObject skillObject in Skills.All)
            {
                if (hero.HeroDeveloper.CanAddFocusToSkill(skillObject))
                {
                    float score = __instance.GetSkillLearningLimitDistance(hero, skillObject);
                    if (score > maxScore)
                    {
                        maxScore = score;
                        result = skillObject;
                    }
                }
            }

            bool wanderersLeaveNotNeededFocusPoints = Settings.Instance?.WanderersLeaveNotNeededFocusPointsForPlayers ?? false;
            __result = wanderersLeaveNotNeededFocusPoints && hero.IsWanderer && maxScore < 0 ? null : result;
            return false;
        }
    }
}
