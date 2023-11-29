using ShortcutLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Methods relating to plots & plot upgrades, this includes registering & creating entries for them.
    /// </summary>
    public static class Plot
    {
        /// <summary>
        /// Gets the <see cref="GameObject"/> prefab of the given <see cref="LandPlot.Id"/>.
        /// </summary>
        /// <param name="plotId">The <see cref="LandPlot.Id"/> to get the <see cref="GameObject"/> prefab from/</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject GetPlot(LandPlot.Id plotId) => Director.Lookup.GetPlotPrefab(plotId);

        /// <summary>
        /// Registers the <see cref="LandPlotRegistry.LandPlotShopEntry"/>.
        /// </summary>
        /// <param name="landPlotShopEntry">The <see cref="LandPlotRegistry.LandPlotShopEntry"/> to be registered.</param>
        public static void RegisterPlot(this LandPlotRegistry.LandPlotShopEntry landPlotShopEntry) => 
            LandPlotRegistry.RegisterPurchasableLandPlot(landPlotShopEntry);

        /// <summary>
        /// Registers the <see cref="LandPlotUpgradeRegistry.UpgradeShopEntry"/> into the <see cref="LandPlotUI"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="LandPlotUI"/> for the <see cref="LandPlotUpgradeRegistry.UpgradeShopEntry"/> to be registered in.</typeparam>
        /// <param name="upgradeShopEntry">The <see cref="LandPlotUpgradeRegistry.UpgradeShopEntry"/> to be registered.</param>
        public static void RegisterPlotUpgrade<T>(this LandPlotUpgradeRegistry.UpgradeShopEntry upgradeShopEntry) where T : LandPlotUI => 
            LandPlotUpgradeRegistry.RegisterPurchasableUpgrade<T>(upgradeShopEntry);

        /// <summary>
        /// Creates an <see cref="LandPlotRegistry.LandPlotShopEntry"/>.
        /// </summary>
        /// <param name="plotIdentifiable">The <see cref="LandPlot.Id"/> for the plot.</param>
        /// <param name="pediaIdentifiable">The <see cref="PediaDirector.Id"/> for the plot.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the plot.</param>
        /// <param name="plotName">The name <see cref="string"/> of the plot.</param>
        /// <param name="plotIntro">The intro <see cref="string"/> of the plot.</param>
        /// <param name="plotCost">The cost <see cref="int"/> of the plot.</param>
        /// <param name="isPlotUnlocked">If the plot is unlocked. <see cref="bool"/></param>
        /// <returns><see cref="LandPlotRegistry.LandPlotShopEntry"/></returns>
        public static LandPlotRegistry.LandPlotShopEntry CreatePlotEntry(LandPlot.Id plotIdentifiable, PediaDirector.Id pediaIdentifiable, Sprite icon, string plotName, string plotIntro, int plotCost, bool isPlotUnlocked = true)
        {
            LandPlotRegistry.LandPlotShopEntry landPlotShopEntry = new LandPlotRegistry.LandPlotShopEntry();
            landPlotShopEntry.icon = icon;
            landPlotShopEntry.cost = plotCost;
            landPlotShopEntry.plot = plotIdentifiable;
            landPlotShopEntry.mainImg = icon;
            landPlotShopEntry.pediaId = pediaIdentifiable;
            landPlotShopEntry.isUnlocked = () => isPlotUnlocked;

            Translate.Pedia(landPlotShopEntry.NameKey, plotName);
            Translate.Pedia(landPlotShopEntry.DescKey, plotIntro);
            LandPlotRegistry.RegisterPurchasableLandPlot(landPlotShopEntry);
            return landPlotShopEntry;
        }

        /// <summary>
        /// Creates an <see cref="LandPlotUpgradeRegistry.UpgradeShopEntry"/>.
        /// </summary>
        /// <param name="upgradeIdentifiable">The <see cref="LandPlot.Upgrade"/> for the upgrade.</param>
        /// <param name="landPlotPediaIdentifiable">The <see cref="PediaDirector.Id"/> of the plot that the upgrade is going to be in.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the upgrade.</param>
        /// <param name="upgradeName">The name <see cref="string"/> of the upgrade.</param>
        /// <param name="upgradeDescription">The description <see cref="string"/> of the upgrade.</param>
        /// <param name="upgradeCost">The cost <see cref="int"/> of the upgrade.</param>
        /// <param name="shouldHoldToPurchase">If the player should hold the button to purchase the upgrade. <see cref="bool"/></param>
        /// <param name="requiredUpgrade">The required <see cref="LandPlot.Upgrade"/> for this upgrade to be unlocked.</param>
        /// <returns><see cref="LandPlotUpgradeRegistry.UpgradeShopEntry"/></returns>
        public static LandPlotUpgradeRegistry.UpgradeShopEntry CreatePlotUpgradeEntry(LandPlot.Upgrade upgradeIdentifiable, PediaDirector.Id landPlotPediaIdentifiable, Sprite icon, string upgradeName, string upgradeDescription, int upgradeCost, bool shouldHoldToPurchase = false, LandPlot.Upgrade requiredUpgrade = LandPlot.Upgrade.NONE)
        {
            LandPlotUpgradeRegistry.UpgradeShopEntry upgradeShopEntry = new LandPlotUpgradeRegistry.UpgradeShopEntry();
            upgradeShopEntry.icon = icon;
            upgradeShopEntry.cost = upgradeCost;
            upgradeShopEntry.mainImg = icon;
            upgradeShopEntry.upgrade = upgradeIdentifiable;
            upgradeShopEntry.holdtopurchase = shouldHoldToPurchase;
            upgradeShopEntry.landplotPediaId = landPlotPediaIdentifiable;

            if (requiredUpgrade != LandPlot.Upgrade.NONE)
                upgradeShopEntry.isUnlocked = x => x.HasUpgrade(requiredUpgrade);
            upgradeShopEntry.isAvailable = x => !x.HasUpgrade(upgradeIdentifiable);

            Translate.Pedia(upgradeShopEntry.NameKey, upgradeName);
            Translate.Pedia(upgradeShopEntry.DescKey, upgradeDescription);
            return upgradeShopEntry;
        }
    }
}
