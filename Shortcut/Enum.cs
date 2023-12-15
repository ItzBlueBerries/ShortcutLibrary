using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Parsing <see cref="string"/> to <see cref="enum"/> methods.
    /// </summary>
    public static class EnumParse
    {
        /// <summary>
        /// Parses the <see cref="string"/> to an <see cref="Identifiable.Id"/> based on the name. Enum must exist.
        /// </summary>
        /// <param name="name">The name of the <see cref="Identifiable.Id"/> to be parsed.</param>
        /// <returns><see cref="Identifiable.Id"/></returns>
        public static Identifiable.Id ParseIdentifiable(string name) => (Identifiable.Id)Enum.Parse(typeof(Identifiable.Id), name);

        /// <summary>
        /// Parses the <see cref="string"/> to an <see cref="Gadget.Id"/> based on the name. Enum must exist.
        /// </summary>
        /// <param name="name">The name of the <see cref="Gadget.Id"/> to be parsed.</param>
        /// <returns><see cref="Gadget.Id"/></returns>
        public static Gadget.Id ParseGadget(string name) => (Gadget.Id)Enum.Parse(typeof(Gadget.Id), name);

        /// <summary>
        /// Parses the <see cref="string"/> to an <see cref="PediaDirector.Id"/> based on the name. Enum must exist.
        /// </summary>
        /// <param name="name">The name of the <see cref="PediaDirector.Id"/> to be parsed.</param>
        /// <returns><see cref="PediaDirector.Id"/></returns>
        public static PediaDirector.Id ParsePedia(string name) => (PediaDirector.Id)Enum.Parse(typeof(PediaDirector.Id), name);

        /// <summary>
        /// Parses the <see cref="string"/> to an <see cref="SpawnResource.Id"/> based on the name. Enum must exist.
        /// </summary>
        /// <param name="name">The name of the <see cref="SpawnResource.Id"/> to be parsed.</param>
        /// <returns><see cref="SpawnResource.Id"/></returns>
        public static SpawnResource.Id ParseSpawnResource(string name) => (SpawnResource.Id)Enum.Parse(typeof(SpawnResource.Id), name);

        /// <summary>
        /// Parses the <see cref="string"/> to an <see cref="LandPlot.Id"/> based on the name. Enum must exist.
        /// </summary>
        /// <param name="name">The name of the <see cref="LandPlot.Id"/> to be parsed.</param>
        /// <returns><see cref="LandPlot.Id"/></returns>
        public static LandPlot.Id ParseLandPlot(string name) => (LandPlot.Id)Enum.Parse(typeof(LandPlot.Id), name);

        /// <summary>
        /// Parses the <see cref="string"/> to an <see cref="SlimeEat.FoodGroup"/> based on the name. Enum must exist.
        /// </summary>
        /// <param name="name">The name of the <see cref="SlimeEat.FoodGroup"/> to be parsed.</param>
        /// <returns><see cref="SlimeEat.FoodGroup"/></returns>
        public static SlimeEat.FoodGroup ParseFoodGroup(string name) => (SlimeEat.FoodGroup)Enum.Parse(typeof(SlimeEat.FoodGroup), name);

        /// <summary>
        /// Parses the <see cref="string"/> to an <see cref="RanchDirector.Palette"/> based on the name. Enum must exist.
        /// </summary>
        /// <param name="name">The name of the <see cref="RanchDirector.Palette"/> to be parsed.</param>
        /// <returns><see cref="RanchDirector.Palette"/></returns>
        public static RanchDirector.Palette ParsePalette(string name) => (RanchDirector.Palette)Enum.Parse(typeof(RanchDirector.Palette), name);

        /// <summary>
        /// Parses the <see cref="string"/> to an <see cref="enum"/> based on the name. Enum must exist.
        /// </summary>
        /// <param>The name of the <see cref="enum"/> to be parsed.</param>
        /// <returns><see cref="enum"/></returns>
        public static T ParseEnum<T>(string name) => (T)Enum.Parse(typeof(T), name);
    }
}
