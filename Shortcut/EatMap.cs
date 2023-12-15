using SRML.SR.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Creating <see cref="SlimeDiet.EatMapEntry"/> methods.
    /// </summary>
    public static class EatMap
    {
        /*public static void AddEntry(SlimeDefinition slimeDefinition, SlimeDiet.EatMapEntry eatMapEntry) => slimeDefinition.AddEatMapEntry(eatMapEntry);

        public static void RemoveEntry(SlimeDefinition slimeDefinition, SlimeDiet.EatMapEntry eatMapEntry)
        {
            if (slimeDefinition.Diet.IsNull())
                return;

            var firstOrDefault = slimeDefinition.Diet.EatMap.FirstOrDefault(x => x.eats == eatMapEntry.eats);
            if (firstOrDefault.IsNotNull())
            {
                slimeDefinition.Diet.RefreshEatMap(SRSingleton<GameContext>.Instance.SlimeDefinitions, slimeDefinition);
                slimeDefinition.Diet.EatMap.Remove(firstOrDefault);
            }
        }*/

        /// <summary>
        /// Creates an <see cref="SlimeDiet.EatMapEntry"/> for becoming another <see cref="Identifiable.Id"/>.
        /// </summary>
        /// <param name="toBecome">The <see cref="Identifiable.Id"/> to become.</param>
        /// <param name="whatItEats">The <see cref="Identifiable.Id"/> to eat for this to occur.</param>
        /// <param name="minDrive">The <see cref="float"/> of which determines the minimum drive for eating the other <see cref="Identifiable.Id"/>.</param>
        /// <param name="extraDrive">The extra <see cref="float"/> of which determines the drive for eating the other <see cref="Identifiable.Id"/>.</param>
        /// <param name="driver">The <see cref="SlimeEmotions.Emotion"/> that will influence the drive for eating the other <see cref="Identifiable.Id"/>.</param>
        /// <returns><see cref="SlimeDiet.EatMapEntry"/></returns>
        public static SlimeDiet.EatMapEntry CreateBecomeEntry(Identifiable.Id toBecome, Identifiable.Id whatItEats, float minDrive = 0.5f, float extraDrive = 0, SlimeEmotions.Emotion driver = SlimeEmotions.Emotion.AGITATION)
        {
            SlimeDiet.EatMapEntry eatMapEntry = new SlimeDiet.EatMapEntry()
            {
                becomesId = toBecome,
                driver = driver,
                minDrive = minDrive,
                eats = whatItEats,
                extraDrive = extraDrive
            };

            return eatMapEntry;
        }

        /// <summary>
        /// Creates an <see cref="SlimeDiet.EatMapEntry"/> for producing another <see cref="Identifiable.Id"/>.
        /// </summary>
        /// <param name="toProduce">The <see cref="Identifiable.Id"/> to produce.</param>
        /// <param name="whatItEats">The <see cref="Identifiable.Id"/> to eat for this to occur.</param>
        /// <param name="isFavorite">If the <see cref="Identifiable.Id"/> being eaten is a favorite food. <see cref="bool"/></param>
        /// <param name="favProduceCount">The amount <see cref="int"/> being produced when eating a favorite food.</param>
        /// <param name="minDrive">The <see cref="float"/> of which determines the minimum drive for eating the other <see cref="Identifiable.Id"/>.</param>
        /// <param name="extraDrive">The extra <see cref="float"/> of which determines the drive for eating the other <see cref="Identifiable.Id"/>.</param>
        /// <param name="driver">The <see cref="SlimeEmotions.Emotion"/> that will influence the drive for eating the other <see cref="Identifiable.Id"/>.</param>
        /// <returns><see cref="SlimeDiet.EatMapEntry"/></returns>
        public static SlimeDiet.EatMapEntry CreateProduceEntry(Identifiable.Id toProduce, Identifiable.Id whatItEats, bool isFavorite = false, int favProduceCount = 2, float minDrive = 0.5f, float extraDrive = 0, SlimeEmotions.Emotion driver = SlimeEmotions.Emotion.AGITATION)
        {
            SlimeDiet.EatMapEntry eatMapEntry = new SlimeDiet.EatMapEntry()
            {
                producesId = toProduce,
                driver = driver,
                minDrive = minDrive,
                eats = whatItEats,
                isFavorite = isFavorite,
                favoriteProductionCount = favProduceCount,
                extraDrive = extraDrive
            };

            return eatMapEntry;
        }
    }
}
