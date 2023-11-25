using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortcutLib.Extras;
using ShortcutLib.Shortcut;
using SRML;
using SRML.SR;
using SRML.Utils.Enum;
using UnityEngine;

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

        public override void PostLoad()
        {
            
        }
    }
}
