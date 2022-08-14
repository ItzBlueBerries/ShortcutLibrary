using HarmonyLib;
using ShortcutLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[HarmonyPatch(typeof(SlimeEat))]
[HarmonyPatch("GetFoodGroupIds")]
internal class FoodGroupPatch
{
    public static void Postfix(ref Identifiable.Id[] __result, SlimeEat.FoodGroup group)
    {
        if ((__result == null))
            return;
        List<Identifiable.Id> foodGroupIds = __result.ToList();
        if (group == SlimeEat.FoodGroup.MEAT) //Change to whatever foodgroup you want    
        {
            foreach (Identifiable.Id id in Identifiable.MEAT_CLASS)
            {
                foreach (Identifiable.Id id2 in Identifiable.CHICK_CLASS)
                {
                    foodGroupIds.Remove(id2);
                    break;
                }
                foodGroupIds.Add(id);
                break;
            }
        }
        else if (group == SlimeEat.FoodGroup.NONTARRGOLD_SLIMES)
        {
            foreach (Identifiable.Id id in Identifiable.SLIME_CLASS)
            {
                if (id == Identifiable.Id.TARR_SLIME)
                    foodGroupIds.Remove(Identifiable.Id.TARR_SLIME);
                else
                    foodGroupIds.Add(id);
                break;
            }
        }
        else if (group == SlimeEat.FoodGroup.PLORTS)
        {
            foreach (Identifiable.Id id in Identifiable.PLORT_CLASS)
            {
                foodGroupIds.Add(id);
                break;
            }
        } 
        else if (group == SlimeEat.FoodGroup.FRUIT)
        {
            foreach (Identifiable.Id id in Identifiable.FRUIT_CLASS)
                foodGroupIds.Add(id);
        } 
        else if (group == SlimeEat.FoodGroup.VEGGIES)
        {
            foreach (Identifiable.Id id in Identifiable.VEGGIE_CLASS)
                foodGroupIds.Add(id);
        }
        __result = foodGroupIds.ToArray();
    }
}