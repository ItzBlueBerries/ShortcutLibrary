using ShortcutLib.Components;
using SRML.SR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// <see cref="AchievementsDirector.Achievement"/> related methods, including adding new achievements.
    /// </summary>
    public static class Achieve
    {
        /// <summary>
        /// Awards an <see cref="AchievementsDirector.Achievement"/>.
        /// </summary>
        /// <param name="achievement">The <see cref="AchievementsDirector.Achievement"/> to be rewarded.</param>
        /// <returns><see cref="bool"/></returns>
        public static bool Award(AchievementsDirector.Achievement achievement) => Director.Achieve.AwardAchievement(achievement);

        /// <summary>
        /// Adds to an <see cref="AchievementsDirector.IntStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.IntStat"/> to be added to.</param>
        /// <param name="amount">The amount <see cref="int"/> to be added to the stat.</param>
        public static void AddStat(AchievementsDirector.IntStat stat, int amount) => Director.Achieve.AddToStat(stat, amount);

        /// <summary>
        /// Adds to an <see cref="AchievementsDirector.EnumStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.EnumStat"/> to be added to.</param>
        /// <param name="value">The enum <see cref="Enum"/> to be added to the stat.</param>
        public static void AddStat(AchievementsDirector.EnumStat stat, Enum value) => Director.Achieve.AddToStat(stat, value);

        /// <summary>
        /// Adds to an <see cref="AchievementsDirector.GameIntStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.GameIntStat"/> to be added to.</param>
        /// <param name="amount">The amount <see cref="int"/> to be added to the stat.</param>
        public static void AddStat(AchievementsDirector.GameIntStat stat, int amount) => Director.Achieve.AddToStat(stat, amount);

        /// <summary>
        /// Adds to an <see cref="AchievementsDirector.GameIdDictStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.GameIdDictStat"/> to be added to.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be added to.</param>
        /// <param name="amount">The amount <see cref="int"/> to be added to the stat.</param>
        public static void AddStat(AchievementsDirector.GameIdDictStat stat, Identifiable.Id identifiable, int amount) => Director.Achieve.AddToStat(stat, identifiable, amount);

        /// <summary>
        /// Sets an <see cref="AchievementsDirector.BoolStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.BoolStat"/> to be set to.</param>
        public static void SetStat(AchievementsDirector.BoolStat stat) => Director.Achieve.SetStat(stat);

        /// <summary>
        /// Sets an <see cref="AchievementsDirector.GameFloatStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.GameFloatStat"/> to be set to.</param>
        /// <param name="value">The value <see cref="float"/> to be set.</param>
        public static void SetStat(AchievementsDirector.GameFloatStat stat, float value) => Director.Achieve.SetStat(stat, value);

        /// <summary>
        /// Sets an <see cref="AchievementsDirector.GameDoubleStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.GameDoubleStat"/> to be set to.</param>
        /// <param name="value">The value <see cref="double"/> to be set.</param>
        public static void SetStat(AchievementsDirector.GameDoubleStat stat, double value) => Director.Achieve.SetStat(stat, value);

        /// <summary>
        /// Gets an <see cref="AchievementsDirector.IntStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.IntStat"/> to get.</param>
        /// <returns><see cref="int?"/></returns>
        public static int? GetStat(AchievementsDirector.IntStat stat) => Director.Achieve.GetStat(stat);

        /// <summary>
        /// Gets an <see cref="AchievementsDirector.GameIntStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.GameIntStat"/> to get.</param>
        /// <returns><see cref="int"/></returns>
        public static int GetStat(AchievementsDirector.GameIntStat stat) => Director.Achieve.GetGameIntStat(stat);

        /// <summary>
        /// Gets an <see cref="AchievementsDirector.GameIdDictStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.GameIdDictStat"/> to get.</param>
        /// <returns><see cref="Dictionary{TKey, TValue}"/></returns>
        public static Dictionary<Identifiable.Id, int> GetStat(AchievementsDirector.GameIdDictStat stat) => Director.Achieve.GetGameIdDictStat(stat);

        /// <summary>
        /// Resets an <see cref="AchievementsDirector.IntStat"/>.
        /// </summary>
        /// <param name="stat">The <see cref="AchievementsDirector.IntStat"/> to be reset.</param>
        public static void ResetStat(AchievementsDirector.IntStat stat) => Director.Achieve.ResetStat(stat);

        /// <summary>
        /// Adds a new <see cref="AchievementsDirector.Achievement"/> into the game.
        /// </summary>
        /// <param name="name">The name <see cref="string"/> of the <see cref="AchievementsDirector.Achievement"/>.</param>
        /// <param name="description">The description <see cref="string"/> of the <see cref="AchievementsDirector.Achievement"/>.</param>
        /// <param name="achievement">The <see cref="AchievementsDirector.Achievement"/> to be added.</param>
        /// <param name="tier">The tier <see cref="AchievementRegistry.Tier"/> of the <see cref="AchievementsDirector.Achievement"/>.</param>
        /// <param name="tracker">The tracker for the <see cref="AchievementsDirector.Achievement"/>.</param>
        public static void AddAchievement(string name, string description, AchievementsDirector.Achievement achievement, AchievementRegistry.Tier tier, AchievementsDirector.Tracker tracker)
        {
            AchievementRegistry.RegisterModdedAchievement(achievement, tracker, tier);
            Translate.Achieve("t." + achievement.ToString().ToLower(), name);
            Translate.Achieve("m.reqmt." + achievement.ToString().ToLower(), description);
        }
    }
}
