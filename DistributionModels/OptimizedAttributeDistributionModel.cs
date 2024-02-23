using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;
using ImprovedNPCAttributeDistribution.Extensions;
using System.Linq;

namespace ImprovedNPCAttributeDistribution.DistributionModels
{
    public class OptimizedAttributeDistributionModel : AttributeDistributionModel
    {
        /// <summary>
        /// Gets the next attribute to upgrade using the Optimized attribute distribution model.
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

                if (attributeValue >= developmentModel.MaxAttribute)
                    continue;

                float attributeScore = 0f;
                if (attributeValue == 0)
                {
                    attributeScore = float.MaxValue;
                }
                else
                {
                    foreach (SkillObject skill in currentAttribute.Skills)
                    {
                        // Iterate through all skills of the current attribute to calculate the skill score.
                        // Similar to the base game model, but we square the result similar to euclidean and keep the sign.
                        // This distributes all attribute points relative to the amount of skills invested in a specific attribute.
                        float skillScore = developmentModel.GetSkillLearningLimitDistance(hero, skill);
                        attributeScore += MathF.Sign(skillScore) * skillScore * skillScore;
                    }
                    // We drop the normalization factor, investing into attributes without skill points is not needed in an optimized scenario.
                    // We rely on focus points to fix this folly.
                }
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
