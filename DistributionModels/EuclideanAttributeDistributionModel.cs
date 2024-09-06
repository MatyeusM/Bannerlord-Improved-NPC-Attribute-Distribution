using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;
using ImprovedNPCAttributeDistribution.Extensions;
using System.Linq;

namespace ImprovedNPCAttributeDistribution.DistributionModels
{
    public class EuclideanAttributeDistributionModel : AttributeDistributionModel
    {
        /// <summary>
        /// Gets the next attribute to upgrade using the Euclidean attribute distribution model.
        /// </summary>
        /// <param name="hero">The hero for which to determine the next attribute to upgrade.</param>
        /// <param name="developmentModel">The DefaultCharacterDevelopmentModel used for the calculation.</param>
        /// <returns>The next attribute to upgrade.</returns>
        public override CharacterAttribute GetNextAttributeToUpgrade(Hero hero, DefaultCharacterDevelopmentModel developmentModel)
        {
            CharacterAttribute resultAttributeWithSaturation = Attributes.All.First();
            float highestScoreWithSaturation = float.MinValue;

            CharacterAttribute resultAttributeWithoutSaturation = Attributes.All.First();
            float highestScoreWithoutSaturation = float.MinValue;
            foreach (CharacterAttribute currentAttribute in Attributes.All)
            {
                int attributeValue = hero.GetAttributeValue(currentAttribute);

                if (attributeValue >= developmentModel.MaxAttribute)
                    continue;

                float attributeScore = 0f;
                bool isSaturated = true;

                if (attributeValue == 0)
                    attributeScore = float.MaxValue;
                else
                {
                    // Iterate through all skills of the current attribute to calculate the skill score.
                    // We copied the exact base game model, but instead, we square the result, leading to a more nuanced decision-making by the game, thus coming to a Euclidean metric.

                    // ISSUES:
                    // The Max(0f, 75 + x) leads to a bad saturation behavior where the game starts distributing randomly when too many attribute points for the skills are present, find a better way to deal with it.
                    // We want attributes to be distributed in a way, that low level attributes that can be picked until saturation, are picked. And then we dump the rest into the highest stats, relatively to what is needed.
                    // However, doing a 2-step calculation is not very efficient. I am certain there is a 1-step calculation that will allow the stats to be distributed that way.
                    foreach (SkillObject skill in currentAttribute.Skills)
                    {
                        float learningLimitDistance = developmentModel.GetSkillLearningLimitDistance(hero, skill);

                        // Saturation check
                        if (learningLimitDistance > 0) isSaturated = false;

                        float skillScore = MathF.Max(0f, 75f + learningLimitDistance);
                        attributeScore += skillScore * skillScore;
                    }

                    CharacterAttribute maxAttribute = hero.GetHighestAttribute(currentAttribute);
                    float normalizationFactor = (float)hero.GetAttributeValue(maxAttribute) / (float)attributeValue;
                    // We removed the square root here. We squared the previous value,
                    // so to keep the base game scaling intact, we must thus also remove the square-root here.
                    attributeScore *= normalizationFactor;
                }
                if (attributeScore > highestScoreWithSaturation && !isSaturated)
                {
                    highestScoreWithSaturation = attributeScore;
                    resultAttributeWithSaturation = currentAttribute;
                }
                if (attributeScore > highestScoreWithoutSaturation)
                {
                    highestScoreWithoutSaturation = attributeScore;
                    resultAttributeWithoutSaturation = currentAttribute;
                }
            }
            return highestScoreWithSaturation > float.MinValue ? resultAttributeWithSaturation : resultAttributeWithoutSaturation;
        }
    }
}
