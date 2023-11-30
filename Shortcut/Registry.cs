using ShortcutLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Lots of methods on registering things, including creating identifiables on runtime.
    /// </summary>
    public static class Registry
    {
        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to be bait for snares.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be added.</param>
        public static void AddIdentifiableToSnare(Identifiable.Id identifiable) => SnareRegistry.RegisterAsSnareable(identifiable);

        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to be a target for drones.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be added.</param>
        public static void AddIdentifiableToDrone(Identifiable.Id identifiable) => DroneRegistry.RegisterBasicTarget(identifiable);

        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to be a resource for the refinery.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be added.</param>
        public static void AddIdentifiableToRefinery(Identifiable.Id identifiable) => AmmoRegistry.RegisterRefineryResource(identifiable);

        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to silos.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be added.</param>
        /// <param name="type">The <see cref="SiloStorage.StorageType"/> for the <see cref="Identifiable.Id"/> being added.</param>
        public static void AddIdentifiableToSilo(Identifiable.Id identifiable, SiloStorage.StorageType type = SiloStorage.StorageType.NON_SLIMES) =>
            AmmoRegistry.RegisterSiloAmmo(type, identifiable);

        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to player ammo.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be added.</param>
        /// <param name="mode">The <see cref="PlayerState.AmmoMode"/> for the <see cref="Identifiable.Id"/> being added.</param>
        public static void AddIdentifiableToAmmo(Identifiable.Id identifiable, PlayerState.AmmoMode mode = PlayerState.AmmoMode.DEFAULT) =>
            AmmoRegistry.RegisterPlayerAmmo(mode, identifiable);

        /// <summary>
        /// Creates a <see cref="Gadget.Id"/> on runtime.
        /// </summary>
        /// <param name="name">The name <see cref="string"/> of the <see cref="Gadget.Id"/>.</param>
        /// <returns><see cref="Gadget.Id"/></returns>
        public static Gadget.Id CreateGadgetIdentifiable(string name) => 
            GadgetRegistry.CreateGadgetId(EnumPatcher.GetFirstFreeValue(typeof(Gadget.Id)), name);

        /// <summary>
        /// Creates a <see cref="LandPlot.Id"/> on runtime.
        /// </summary>
        /// <param name="name">The name <see cref="string"/> of the <see cref="LandPlot.Id"/>.</param>
        /// <returns><see cref="LandPlot.Id"/></returns>
        public static LandPlot.Id CreatePlotIdentifiable(string name) =>
            LandPlotRegistry.CreateLandPlotId(EnumPatcher.GetFirstFreeValue(typeof(LandPlot.Id)), name);

        /// <summary>
        /// Creates a <see cref="Identifiable.Id"/> on runtime.
        /// </summary>
        /// <param name="name">The name <see cref="string"/> of the <see cref="Identifiable.Id"/>.</param>
        /// <returns><see cref="Identifiable.Id"/></returns>
        public static Identifiable.Id CreateIdentifiable(string name) => 
            IdentifiableRegistry.CreateIdentifiableId(EnumPatcher.GetFirstFreeValue(typeof(Identifiable.Id)), name);

        /// <summary>
        /// Creates a <see cref="SpawnResource.Id"/> on runtime.
        /// </summary>
        /// <param name="name">The name <see cref="string"/> of the <see cref="SpawnResource.Id"/>.</param>
        /// <returns><see cref="SpawnResource.Id"/></returns>
        public static SpawnResource.Id CreateSpawnResourceIdentifiable(string name) =>
            SpawnResourceRegistry.CreateSpawnResourceId(EnumPatcher.GetFirstFreeValue(typeof(SpawnResource.Id)), name);

        /// <summary>
        /// Creates a <see cref="PlayerState.Upgrade"/> on runtime.
        /// </summary>
        /// <param name="name">The name <see cref="string"/> of the <see cref="PlayerState.Upgrade"/>.</param>
        /// <returns><see cref="PlayerState.Upgrade"/></returns>
        public static PlayerState.Upgrade CreatePlayerUpgradeIdentifiable(string name) =>
            PersonalUpgradeRegistry.CreatePersonalUpgrade(EnumPatcher.GetFirstFreeValue(typeof(PlayerState.Upgrade)), name);

        /// <summary>
        /// Registers an <see cref="Identifiable.Id"/> to be vaccable.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be registered.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> when vacced.</param>
        /// <param name="color">The color <see cref="Color"/> when vacced.</param>
        /// <param name="name">The name <see cref="string"/> of the <see cref="VacItemDefinition"/>.</param>
        /// <returns><see cref="VacItemDefinition"/></returns>
        public static VacItemDefinition RegisterVaccable(Identifiable.Id identifiable, Sprite icon, Color color, string name)
        {
            var vacDefinition = Definition.CreateVacDefinition(identifiable, icon, color, name);
            LookupRegistry.RegisterVacEntry(vacDefinition);
            return vacDefinition;
        }

        /// <summary>
        /// Registers an <see cref="Identifiable.Id"/> to be a purchasable toy.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be registered.</param>
        /// <param name="isUpgradedPurchasable">If this is supposed to be for the upgraded purchasable toys, registers as an upgraded purchasable toy. <see cref="bool"/></param>
        public static void RegisterPurchasableToy(Identifiable.Id identifiable, bool isUpgradedPurchasable = false)
        {
            if (isUpgradedPurchasable)
                ToyRegistry.RegisterUpgradedPurchasableToy(identifiable);
            else
                ToyRegistry.RegisterBasePurchasableToy(identifiable);
        }

        /// <summary>
        /// Registers an <see cref="Identifiable.Id"/> to be a <see cref="GardenCatcher.PlantSlot"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be registered.</param>
        /// <param name="plantedPrefab">The prefab <see cref="GameObject"/> of the planted plot.</param>
        /// <param name="deluxePlantedPrefab">The deluxe prefab <see cref="GameObject"/> of the planted plot. Can be set to null if there is none.</param>
        /// <returns><see cref="GardenCatcher.PlantSlot"/></returns>
        public static GardenCatcher.PlantSlot RegisterPlantSlot(Identifiable.Id identifiable, GameObject plantedPrefab, GameObject deluxePlantedPrefab)
        {
            GardenCatcher.PlantSlot plantSlot = new GardenCatcher.PlantSlot();
            plantSlot.id = identifiable;
            plantSlot.plantedPrefab = plantedPrefab;
            plantSlot.deluxePlantedPrefab = deluxePlantedPrefab;
            PlantSlotRegistry.RegisterPlantSlot(plantSlot);
            return plantSlot;
        }
    }
}
