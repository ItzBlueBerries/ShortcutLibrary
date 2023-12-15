using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShortcutLib.Shortcut.FoodGroup;

namespace ShortcutLib.Harmony
{
    [HarmonyPatch(typeof(SlimeEat), nameof(SlimeEat.GetFoodGroupIds))]
    internal class SlimeEatGetFoodGroupIdsPatch
    {
        public static void Postfix(ref SlimeEat.FoodGroup group, ref Identifiable.Id[] __result)
        {
            if (__result.IsNull())
                return;

            foreach (var foodGroup in foodGroupsToPatch)
            {
                if (group == foodGroup.Key)
                {
                    foreach (var id in foodGroup.Value.Item2)
                    {
                        if (!__result.Contains(id))
                            __result = __result.AddToArray(id);
                    }
                }
            }
        }
    }
}
