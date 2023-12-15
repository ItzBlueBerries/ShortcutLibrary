using HarmonyLib;
using ShortcutLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Methods for creating <see cref="SlimeEat.FoodGroup"/> and adding to them, etc.
    /// </summary>
    public static class FoodGroup
    {
        internal static readonly Dictionary<SlimeEat.FoodGroup, (string, HashSet<Identifiable.Id>)> foodGroupsToPatch = new();

        /// <summary>
        /// Registers a <see cref="SlimeEat.FoodGroup"/> with the <see cref="Identifiable.Id"/>s provided.
        /// </summary>
        /// <param name="groupIdentifiable">The <see cref="SlimeEat.FoodGroup"/> being registered.</param>
        /// <param name="name">The name <see cref="string"/> of the <see cref="SlimeEat.FoodGroup"/>.</param>
        /// <param name="identifiables">The <see cref="Identifiable.Id"/>s in the <see cref="SlimeEat.FoodGroup"/>.</param>
        public static void Register(this SlimeEat.FoodGroup groupIdentifiable, string name, HashSet<Identifiable.Id> identifiables)
        {
            SlimeEat.foodGroupIds.AddIfDoesNotContain(groupIdentifiable, identifiables.ToArray());
            foodGroupsToPatch.AddIfDoesNotContain(groupIdentifiable, (name, identifiables));
        }

        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to a <see cref="SlimeEat.FoodGroup"/>.
        /// </summary>
        /// <param name="groupIdentifiable">The <see cref="SlimeEat.FoodGroup"/> being added to.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> being added to the <see cref="SlimeEat.FoodGroup"/>.</param>
        public static void AddTo(this SlimeEat.FoodGroup groupIdentifiable, Identifiable.Id identifiable)
        {
            var array = SlimeEat.foodGroupIds?[groupIdentifiable];
            if (array.IsNull())
                return;
            SlimeEat.foodGroupIds[groupIdentifiable] = array.AddToArray(identifiable);
        }

        /// <summary>
        /// Adds multiple <see cref="Identifiable.Id"/>s to a <see cref="SlimeEat.FoodGroup"/>.
        /// </summary>
        /// <param name="groupIdentifiable">The <see cref="SlimeEat.FoodGroup"/> being added to.</param>
        /// <param name="identifiables">The <see cref="Identifiable.Id"/>s being added to the <see cref="SlimeEat.FoodGroup"/>.</param>
        public static void AddMany(this SlimeEat.FoodGroup groupIdentifiable, List<Identifiable.Id> identifiables)
        {
            foreach (var id in identifiables)
                groupIdentifiable.AddTo(id);
        }

        /// <summary>
        /// Removes an <see cref="Identifiable.Id"/> from a <see cref="SlimeEat.FoodGroup"/>.
        /// </summary>
        /// <param name="groupIdentifiable">The <see cref="SlimeEat.FoodGroup"/> being removed from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> being removed from the <see cref="SlimeEat.FoodGroup"/>.</param>
        public static void RemoveFrom(this SlimeEat.FoodGroup groupIdentifiable, Identifiable.Id identifiable)
        {
            var list = SlimeEat.foodGroupIds?[groupIdentifiable]?.ToList();
            if (list.IsNull())
                return;
            list.Remove(identifiable);
            SlimeEat.foodGroupIds[groupIdentifiable] = list.ToArray();
        }

        /// <summary>
        /// Removes multiple <see cref="Identifiable.Id"/>s from a <see cref="SlimeEat.FoodGroup"/>.
        /// </summary>
        /// <param name="groupIdentifiable">The <see cref="SlimeEat.FoodGroup"/> being removed from.</param>
        /// <param name="identifiables">The <see cref="Identifiable.Id"/>s being removed from the <see cref="SlimeEat.FoodGroup"/>.</param>
        public static void RemoveMany(this SlimeEat.FoodGroup groupIdentifiable, List<Identifiable.Id> identifiables)
        {
            foreach (var id in identifiables)
                groupIdentifiable.RemoveFrom(id);
        }
    }
}
