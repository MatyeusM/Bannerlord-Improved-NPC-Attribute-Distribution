using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;
using ImprovedNPCAttributeDistribution.Extensions;
using System.Linq;

namespace ImprovedNPCAttributeDistribution.DistributionModels
{
    public class VanillaAttributeDistributionModel : AttributeDistributionModel
    {
        /// <summary>
        /// Gets the next attribute to upgrade using the base game attribute distribution model.
        /// </summary>
        /// <param name="hero">The hero for which to determine the next attribute to upgrade.</param>
        /// <param name="developmentModel">The DefaultCharacterDevelopmentModel used for the calculation.</param>
        /// <returns>The next attribute to upgrade.</returns>
        public override CharacterAttribute GetNextAttributeToUpgrade(Hero hero, DefaultCharacterDevelopmentModel developmentModel)
        {
            CharacterAttribute resultAttribute = Attributes.All.First();
            float highestScore = float.MinValue;
            foreach (CharacterAttribute currentAttribute in Attributes.All)
            {
                int attributeValue = hero.GetAttributeValue(currentAttribute);

                // Skip the iteration if the attribute value is at its maximum
                if (attributeValue >= developmentModel.MaxAttribute)
                    continue;

                float attributeScore = 0f;
                if (attributeValue == 0) // We should not have attributes with 0 value, so maximize it.
                {
                    attributeScore = float.MaxValue;
                }
                else
                {
                    foreach (SkillObject skill in currentAttribute.Skills)
                    {
                        // Iterate through all skills of the current attribute to calculate the skill score.
                        // Base game uses here an offset of 75. Trying to get NPCs to consider going up to 75 above their learning limit with their attributes.
                        // However, for the consideration on which attribute to upgrade among concurring ones, it is absolutely useless.
                        // All it does is just adding 225 to the final score. If you remove the 0 lower limit, just comparing the scores with values should give you the same result;
                        // apart from a really weird "randomized" behavior starting at values between 0 and 75.
                        float skillScore = MathF.Max(0f, 75f + developmentModel.GetSkillLearningLimitDistance(hero, skill));
                        // Plainly add them together.
                        attributeScore += skillScore;
                    }
                    // Normalize the score, so prefering lower attributes instead of higher ones.
                    CharacterAttribute maxAttribute = hero.GetHighestAttribute(currentAttribute);
                    float normalizationFactor = (float)hero.GetAttributeValue(maxAttribute) / (float)attributeValue;
                    attributeScore *= MathF.Sqrt(normalizationFactor);
                }
                // Update the result attribute if the current attribute score is higher
                if (attributeScore > highestScore)
                {
                    highestScore = attributeScore;
                    resultAttribute = currentAttribute;
                }

            }
            return resultAttribute;
        }
    }
}
