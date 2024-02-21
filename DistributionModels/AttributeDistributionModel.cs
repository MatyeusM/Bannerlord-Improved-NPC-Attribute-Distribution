using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Collections.Generic;

namespace ImprovedNPCAttributeDistribution.DistributionModels
{
    public static class AttributeDistributionModels
    {
        public static readonly Dictionary<string, AttributeDistributionModel> Models = new Dictionary<string, AttributeDistributionModel>
        {
            { "Vanilla", new VanillaAttributeDistributionModel() },
            { "Vanilla Enhanced", new EuclideanAttributeDistributionModel() },
            { "Optimized", new OptimizedAttributeDistributionModel() },
        };
    }

    public abstract class AttributeDistributionModel
    {
        public abstract CharacterAttribute GetNextAttributeToUpgrade(Hero hero, DefaultCharacterDevelopmentModel developmentModel);
    }
}
