
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Linq;

namespace ImprovedNPCAttributeDistribution.Extensions
{
    public static class HeroExtensions
    {
        public static CharacterAttribute GetHighestAttribute(this Hero hero, CharacterAttribute? exclusion = null)
        {
            CharacterAttribute highestAttribute = Attributes.All.First();
            int maxAttributeValue = 0;

            foreach (CharacterAttribute attribute in Attributes.All)
            {
                if (attribute == exclusion)
                    continue;

                int attributeValue = hero.GetAttributeValue(attribute);
                if (attributeValue > maxAttributeValue)
                {
                    maxAttributeValue = attributeValue;
                    highestAttribute = attribute;
                }
            }

            return highestAttribute;
        }
    }
}
