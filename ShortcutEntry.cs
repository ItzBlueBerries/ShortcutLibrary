global using SRML;
global using SRML.SR;
global using UnityEngine;
global using ShortcutLib.Extras;
global using ShortcutLib.Presets;
global using ShortcutLib.Shortcut;
global using static ShortcutLib.Extras.Debugging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRML.Utils.Enum;
using ShortcutLib.Extensions;

namespace ShortcutLib
{
    [EnumHolder]
    internal class Enums
    {
        // public static readonly Identifiable.Id TEST_SLIME;

        public static readonly AchievementsDirector.Achievement ACk;

        public static readonly AchievementsDirector.IntStat Stat;
    }

    internal class ShortcutEntry : ModEntryPoint
    {
        public override void PreLoad() => HarmonyInstance.PatchAll();

        public override void Load()
        {
            Achieve.AddAchievement("s", "w", Enums.ACk, AchievementRegistry.Tier.TIER1, new AchievementsDirector.CountTracker(SceneContext.Instance.AchievementsDirector, Enums.ACk, Enums.Stat, 5));
            SRCallbacks.OnZoneEntered += delegate (ZoneDirector.Zone zone, PlayerState playerState)
            {
                if (zone == ZoneDirector.Zone.REEF)
                    Achieve.AddStat(Enums.Stat, 1);
            };
        }

        /*public override void PostLoad()
        {
            SRCallbacks.OnSaveGameLoaded += delegate (SceneContext sceneContext)
            {
                Identifiable.Id.PINK_SLIME.GetSlimeDefinition().AppearancesDefault[0].ModifyEyes(SlimeFace.SlimeExpression.Scared, new SlimeFace.SlimeExpression[] { SlimeFace.SlimeExpression.Blink, SlimeFace.SlimeExpression.Scared });
                Identifiable.Id.PINK_SLIME.GetSlimeDefinition().AppearancesDefault[0].ModifyMouth(SlimeFace.SlimeExpression.Scared, new SlimeFace.SlimeExpression[] { SlimeFace.SlimeExpression.Blink, SlimeFace.SlimeExpression.Scared });
                Identifiable.Id.TABBY_SLIME.GetSlimeDefinition().AppearancesDefault[0].ModifyEyes(SlimeFace.SlimeExpression.Scared, new SlimeFace.SlimeExpression[] { SlimeFace.SlimeExpression.Blink, SlimeFace.SlimeExpression.Scared });
                Identifiable.Id.TABBY_SLIME.GetSlimeDefinition().AppearancesDefault[0].ModifyMouth(SlimeFace.SlimeExpression.Scared, new SlimeFace.SlimeExpression[] { SlimeFace.SlimeExpression.Blink, SlimeFace.SlimeExpression.Scared });
                ShortcutConsole.Log("This logged");
            };
        }*/
    }
}
