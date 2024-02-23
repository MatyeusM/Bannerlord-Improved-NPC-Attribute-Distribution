using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace ImprovedNPCAttributeDistribution.Extensions
{
    public static class DefaultCharacterDevelopmentModelExtensions
    {
        /// <summary>
        /// Gets the skill learning limit distance for the specified hero and skill.
        /// </summary>
        /// <remarks>
        /// This returns a 1-dimensional result. If considering multiple skills, please use at least a Euclidean metric sqrt(distance1^2 + distance2^2).
        /// </remarks>
        /// <param name="model">The DefaultCharacterDevelopmentModel to extend.</param>
        /// <param name="hero">The hero for which to calculate the skill learning limit distance.</param>
        /// <param name="skill">The skill for which to calculate the learning limit distance.</param>
        /// <returns>The skill learning limit distance.</returns>
        public static float GetSkillLearningLimitDistance(this DefaultCharacterDevelopmentModel model, Hero hero, SkillObject skill)
        {
            // Get the attribute value, focus, and skill value for the specified skill
            int attributeValue = hero.GetAttributeValue(skill.CharacterAttribute);
            int focus = hero.HeroDeveloper.GetFocus(skill);
            float skillValue = (float)hero.GetSkillValue(skill);

            // Calculate and return the skill learning limit distance
            return skillValue - model.CalculateLearningLimit(attributeValue, focus, null, false).ResultNumber;
        }
    }
}
