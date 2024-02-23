using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Collections.Generic;

namespace ImprovedNPCAttributeDistribution.DistributionModels
{
    // The class holding a collection of attribute distribution models
    public static class AttributeDistributionModels
    {
        // Dictionary containing attribute distribution model names and their corresponding instances
        public static readonly Dictionary<string, AttributeDistributionModel> Models = new Dictionary<string, AttributeDistributionModel>
        {
            { "Vanilla", new VanillaAttributeDistributionModel() },
            { "Vanilla Enhanced", new EuclideanAttributeDistributionModel() },
            { "Optimized", new OptimizedAttributeDistributionModel() },
        };
    }

    // Abstract base class for attribute distribution models
    public abstract class AttributeDistributionModel
    {
        /// <summary>
        /// Gets the next attribute to upgrade for the specified hero and development model.
        /// </summary>
        /// <param name="hero">The hero for which to determine the next attribute to upgrade.</param>
        /// <param name="developmentModel">The DefaultCharacterDevelopmentModel used for the calculation.</param>
        /// <returns>The next attribute to upgrade.</returns>
        public abstract CharacterAttribute GetNextAttributeToUpgrade(Hero hero, DefaultCharacterDevelopmentModel developmentModel);
    }
}
