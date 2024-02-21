using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace ImprovedNPCAttributeDistribution.Extensions
{
    public static class DefaultCharacterDevelopmentModelExtensions
    {
        public static float GetSkillLearningLimitDistance(this DefaultCharacterDevelopmentModel model, Hero hero, SkillObject skill)
        {
            int attributeValue = hero.GetAttributeValue(skill.CharacterAttribute);
            int focus = hero.HeroDeveloper.GetFocus(skill);
            float skillValue = (float)hero.GetSkillValue(skill);
            return skillValue - model.CalculateLearningLimit(attributeValue, focus, null, false).ResultNumber;
        }
    }
}
