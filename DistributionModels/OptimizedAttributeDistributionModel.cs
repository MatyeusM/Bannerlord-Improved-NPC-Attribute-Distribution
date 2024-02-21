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
                        float skillScore = developmentModel.GetSkillLearningLimitDistance(hero, skill);
                        attributeScore += MathF.Sign(skillScore) * skillScore * skillScore;
                    }
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
