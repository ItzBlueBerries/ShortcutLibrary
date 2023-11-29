using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Methods for creating a select of definitions.
    /// </summary>
    public static class Definition
    {
        /// <summary>
        /// Creates a <see cref="UpgradeDefinition"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="PlayerState.Upgrade"/> for the definition.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> for the definition.</param>
        /// <param name="cost">The cost <see cref="int"/> for the definition.</param>
        /// <param name="name">The name <see cref="string"/> for the definition.</param>
        /// <returns><see cref="UpgradeDefinition"/></returns>
        public static UpgradeDefinition CreateUpgradeDefinition(PlayerState.Upgrade identifiable, Sprite icon, int cost, string name)
        {
            UpgradeDefinition upgradeDefinition = ScriptableObject.CreateInstance<UpgradeDefinition>();

            upgradeDefinition.name = name;
            upgradeDefinition.icon = icon;
            upgradeDefinition.cost = cost;
            upgradeDefinition.upgrade = identifiable;

            return upgradeDefinition;
        }

        /// <summary>
        /// Creates a <see cref="LiquidDefinition"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> for the definition.</param>
        /// <param name="inFx">The inFX <see cref="GameObject"/> for the definition.</param>
        /// <param name="vacFailFx">The vacFailFX <see cref="GameObject"/> for the definition.</param>
        /// <param name="name">The naem <see cref="string"/> for the definition.</param>
        /// <returns><see cref="LiquidDefinition"/></returns>
        public static LiquidDefinition CreateLiquidDefinition(Identifiable.Id identifiable, GameObject inFx, GameObject vacFailFx, string name)
        {
            LiquidDefinition liquidDefinition = ScriptableObject.CreateInstance<LiquidDefinition>();

            liquidDefinition.name = name;
            liquidDefinition.id = identifiable;
            liquidDefinition.inFX = inFx;
            liquidDefinition.vacFailFX = vacFailFx;

            return liquidDefinition;
        }

        /// <summary>
        /// Creates a <see cref="ToyDefinition"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> for the definition.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> for the definition.</param>
        /// <param name="nameKey">The nameKey <see cref="string"/> for the definition.</param>
        /// <param name="cost">The cost <see cref="int"/> for the definition.</param>
        /// <param name="name">The name <see cref="string"/> for the definition.</param>
        /// <returns><see cref="ToyDefinition"/></returns>
        public static ToyDefinition CreateToyDefinition(Identifiable.Id identifiable, Sprite icon, string nameKey, int cost, string name)
        {
            ToyDefinition toyDefinition = ScriptableObject.CreateInstance<ToyDefinition>();

            toyDefinition.name = name;
            toyDefinition.icon = icon;
            toyDefinition.cost = cost;
            toyDefinition.toyId = identifiable;
            toyDefinition.nameKey = nameKey;

            return toyDefinition;
        }

        /// <summary>
        /// Creates a <see cref="VacItemDefinition"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> for the definition.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> for the definition.</param>
        /// <param name="color">The color <see cref="Color"/> for the definition.</param>
        /// <param name="name">The name <see cref="string"/> for the definition.</param>
        /// <returns><see cref="VacItemDefinition"/></returns>
        public static VacItemDefinition CreateVacDefinition(Identifiable.Id identifiable, Sprite icon, Color color, string name)
        {
            VacItemDefinition vacItemDefinition = ScriptableObject.CreateInstance<VacItemDefinition>();

            vacItemDefinition.name = name;
            vacItemDefinition.id = identifiable;
            vacItemDefinition.icon = icon;
            vacItemDefinition.color = color;

            return vacItemDefinition;
        }

        /// <summary>
        /// Creates a <see cref="GadgetDefinition"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Gadget.Id"/> for the definition.</param>
        /// <param name="pediaLink">The pediaLink <see cref="PediaDirector.Id"/> for the definition.</param>
        /// <param name="prefab">The <see cref="GameObject"/> for the definition.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> for the definition.</param>
        /// <param name="name">The name <see cref="string"/> for the definition.</param>
        /// <param name="countLimit">The countLimit <see cref="int"/> for the definition.</param>
        /// <param name="blueprintCost">The blueprintCost <see cref="int"/> for the definition.</param>
        /// <param name="buyCountLimit">The buyCountLimit <see cref="int"/> for the definition.</param>
        /// <param name="destroyOnRemoval">If it is destroyed on removal. <see cref="bool"/></param>
        /// <param name="buyInPairs">If it is bought in pairs. <see cref="bool"/></param>
        /// <param name="countOtherIds">The countOtherIds <see cref="Gadget.Id[]"/> for the definition.</param>
        /// <param name="craftCosts">The craftCosts <see cref="GadgetDefinition.CraftCost[]"/> for the definition.</param>
        /// <returns><see cref="GadgetDefinition"/></returns>
        public static GadgetDefinition CreateGadDefinition(Gadget.Id identifiable, PediaDirector.Id pediaLink, GameObject prefab, Sprite icon, string name, int countLimit, int blueprintCost, int buyCountLimit, bool destroyOnRemoval, bool buyInPairs, Gadget.Id[] countOtherIds, GadgetDefinition.CraftCost[] craftCosts)
        {
            GadgetDefinition gadgetDefinition = ScriptableObject.CreateInstance<GadgetDefinition>();

            gadgetDefinition.name = name;
            gadgetDefinition.id = identifiable;
            gadgetDefinition.icon = icon;
            gadgetDefinition.prefab = prefab;
            gadgetDefinition.pediaLink = pediaLink;
            gadgetDefinition.buyInPairs = buyInPairs;
            gadgetDefinition.craftCosts = craftCosts;
            gadgetDefinition.countLimit = countLimit;
            gadgetDefinition.countOtherIds = countOtherIds;
            gadgetDefinition.blueprintCost = blueprintCost;
            gadgetDefinition.buyCountLimit = buyCountLimit;
            gadgetDefinition.destroyOnRemoval = destroyOnRemoval;

            return gadgetDefinition;
        }

        // Honestly I suggest just making this yourself
        /*public static SlimeDefinition CreateSlimeDefinition(string Name, Identifiable.Id IdentifiableId, bool IsLargo, bool CanLargofy, SlimeAppearance[] AppearancesDefault, SlimeDiet Diet, Identifiable.Id[] FavoriteToys, GameObject BaseModule, SlimeDefinition[] BaseSlimes, GameObject[] SlimeModules, SlimeSounds Sounds, float PrefabScale, [Optional] List<SlimeAppearance> AppearancesDynamic, bool dynamicAppearance = false)
        {
            SlimeDefinition slimeDefinition = ScriptableObject.CreateInstance<SlimeDefinition>();

            slimeDefinition.name = Name;
            slimeDefinition.Name = Name;
            slimeDefinition.IdentifiableId = IdentifiableId;
            slimeDefinition.IsLargo = IsLargo;
            slimeDefinition.CanLargofy = CanLargofy;
            slimeDefinition.AppearancesDefault = AppearancesDefault;
            slimeDefinition.Diet = Diet;
            slimeDefinition.FavoriteToys = FavoriteToys;
            slimeDefinition.BaseModule = BaseModule;
            slimeDefinition.BaseSlimes = BaseSlimes;
            slimeDefinition.SlimeModules = SlimeModules;
            slimeDefinition.Sounds = Sounds;
            slimeDefinition.PrefabScale = PrefabScale;
            if (dynamicAppearance)
            {
                slimeDefinition.AppearancesDynamic = AppearancesDynamic;
            }

            return slimeDefinition;
        }*/
    }
}
