using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Base.Global;
using MCM.Common;
using System.Collections.Generic;
using ImprovedNPCAttributeDistribution.DistributionModels;

namespace ImprovedNPCAttributeDistribution
{
    internal class Settings : AttributeGlobalSettings<Settings>
    {
        public static readonly Dictionary<string, int> AttributePointsMap = new Dictionary<string, int>
        {
            { "Minimum", 6 },
            { "Weak", 12 },
            { "Vanilla", 15 },
            { "Player", 18 },
            { "Advanced", 21 },
            { "Strong", 24 },
            { "Super Human", 27 },
        };

        private const string HeadingAttributes = "Attributes";
        private const string HeadingFocusPoints = "Focus Points";

        public override string Id => "ImprovedNPCAttributeDistribution_v1";
        public override string DisplayName => "Improved NPC Attribute Distribution";
        public override string FolderName => "AttrFix";
        public override string FormatType => "xml";

        [SettingPropertyDropdown("Attribute Point Spending Model", Order = 0, RequireRestart = false, HintText = "Sets the model that is used to distribute Attribute Points to Attributes for NPCs, for details on each model check the documentation.")]
        [SettingPropertyGroup(HeadingAttributes)]
        public Dropdown<string> AttributePointSpendingModel { get; set; } = new Dropdown<string>(AttributeDistributionModels.Models.Keys, selectedIndex: 1);

        [SettingPropertyBool("NPC Base Attribute Points", Order = 1, RequireRestart = false, HintText = "Lowers or increases the amount of attribute points NPCs have available at Level 1.")]
        [SettingPropertyGroup(HeadingAttributes)]
        public Dropdown<string> BaseAttributePoints { get; set; } = new Dropdown<string>(AttributePointsMap.Keys, selectedIndex: 2);

        [SettingPropertyBool("Wanderers only spend necessary Focus Points", Order = 0, RequireRestart = false, HintText = "Wanderers will not spend unneeded focus points.")]
        [SettingPropertyGroup(HeadingFocusPoints)]
        public bool WanderersLeaveNotNeededFocusPointsForPlayers { get; set; } = false;
    }
}
