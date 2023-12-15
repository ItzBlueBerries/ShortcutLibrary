using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShortcutLib.Shortcut.FoodGroup;

namespace ShortcutLib.Harmony
{
    [HarmonyPatch(typeof(SlimeDiet), nameof(SlimeDiet.GetFoodCategoryMsg))]
    internal class SlimeDietGetFoodCategoryMsgPatch
    {
        public static bool Prefix(ref Identifiable.Id id, ref string __result)
        {
            foreach (var foodGroup in foodGroupsToPatch)
            {
                if (SlimeEat.GetFoodGroupIds(foodGroup.Key).Contains(id))
                {
                    __result = "m.foodgroup." + foodGroup.Key.ToString().ToLower().Replace(" ", "_");
                    return false;
                }
            }
            return true;
        }
    }
}
