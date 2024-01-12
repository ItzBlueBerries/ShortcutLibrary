using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShortcutLib.Shortcut.Gordo;

namespace ShortcutLib.Harmony
{
    /*[HarmonyPatch(typeof(GordoRewardsBase), nameof(GordoRewardsBase.GiveRewards))]
    internal class GordoRewardsBaseGiveRewardsPatch
    {
        private static Vector3[] defaultGordoSpawns = GordoRewardsBase.spawns;
        private static System.Random random = new();

        private static readonly float[] offsets = new float[]
        {
            0,
            0.5235988f,
            -0.5235988f
        };

        private static readonly float[] x_axis = new float[]
        {
            0,
            0.5f
        };

        private static readonly float[] y_axis = new float[]
        {
            0,
            0.866f,
            -0.866f
        };

        public static void Prefix(GordoRewardsBase __instance)
        {
            var identifiable = __instance.GetComponent<GordoIdentifiable>().id;
            if (gordoSpawnsToPatch.ContainsKey(identifiable))
                GordoRewardsBase.spawns = PopulateSpawnPoints(gordoSpawnsToPatch[identifiable]);
        }

        public static void Postfix(GordoRewardsBase __instance)
        {
            var identifiable = __instance.GetComponent<GordoIdentifiable>().id;
            if (gordoSpawnsToPatch.ContainsKey(identifiable))
                GordoRewardsBase.spawns = defaultGordoSpawns;
        }

        /*public static Vector3[] PopulateSpawnPoints(Vector3[] toPopulate)
        {
            if (toPopulate.Length >= 1)
                toPopulate[0] = Vector3.zero;
            if (toPopulate.Length >= 2)
            {
                for (int i = 1; i < toPopulate.Length; i++)
                {
                    float angle = 6.2831855f * i / toPopulate.Length + offsets[random.Next(0, offsets.Length)];
                    float x = x_axis[random.Next(0, x_axis.Length)];
                    float y = y_axis[random.Next(0, y_axis.Length)];
                    toPopulate[i] = new Vector3(Mathf.Cos(angle) * x, y, Mathf.Sin(angle) * x);
                }
            }
            return toPopulate;
        }*/

        /*public static Vector3[] PopulateSpawnPoints(Vector3[] toPopulate)
        {
            if (toPopulate.Length >= 1)
                toPopulate[0] = Vector3.zero;
            if (toPopulate.Length >= 2)
            {
                int firstSegment = toPopulate.Length / 2;
                int secondSegment = firstSegment / 2;
                int thirdSegment = secondSegment / 2;

                ShortcutConsole.Log(firstSegment);
                ShortcutConsole.Log(secondSegment);
                ShortcutConsole.Log(thirdSegment);

                for (int i = 1; i < firstSegment; i++)
                {
                    float angle = 6.2831855f * i / firstSegment + offsets[0];
                    float x = x_axis[0];
                    float y = y_axis[0];
                    toPopulate[i] = new Vector3(Mathf.Cos(angle) * x, y, Mathf.Sin(angle) * x);
                }

                for (int i = 0; i < secondSegment; i++)
                {
                    float angle = 6.2831855f * i / secondSegment + offsets[1];
                    float x = x_axis[1];
                    float y = y_axis[1];
                    toPopulate[firstSegment + i] = new Vector3(Mathf.Cos(angle) * x, y, Mathf.Sin(angle) * x);
                }

                for (int i = 0; i < thirdSegment; i++)
                {
                    float angle = 6.2831855f * i / thirdSegment + offsets[2];
                    float x = x_axis[1];
                    float y = y_axis[2];
                    toPopulate[firstSegment + secondSegment + i] = new Vector3(Mathf.Cos(angle) * x, y, Mathf.Sin(angle) * x);
                }
            }
            return toPopulate;
        }
    }*/
}
