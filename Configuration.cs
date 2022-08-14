using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRML.Config.Attributes;
using SRML.SR;
using UnityEngine;
using static ShortcutLib.Shortcut;

[ConfigFile("shlib.config")]
internal class Configuration
{
    public static readonly bool ABNORMAL_SIZES = false;
    
    static public void LoadAbnormalSizes()
    {
        // TARR DON'T EAT
        SlimeDefinition tarrDefinition = Slime.GetSlimeDef(Identifiable.Id.TARR_SLIME);
        tarrDefinition.Diet.MajorFoodGroups = new SlimeEat.FoodGroup[] { };
        tarrDefinition.Diet.Produces = new Identifiable.Id[] { };
        tarrDefinition.Diet.AdditionalFoods = new Identifiable.Id[] { };
        tarrDefinition.Diet.EatMap?.Clear();
        // SLIMES
        foreach (Identifiable.Id slime in Identifiable.SLIME_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(slime);

            prefab.transform.localScale *= 2;
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
            UnityEngine.Object.Destroy(prefab.GetComponent<FleeThreats>());
        }
        // LARGOS
        foreach (Identifiable.Id largo in Identifiable.LARGO_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(largo);

            prefab.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            UnityEngine.Object.Destroy(prefab.GetComponent<FleeThreats>());
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, prefab);
        }
        // MEAT
        foreach (Identifiable.Id meat in Identifiable.MEAT_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(meat);

            prefab.transform.localScale *= 2;
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
        }
        // TARR
        foreach (Identifiable.Id tarr in Identifiable.TARR_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(tarr);

            prefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, prefab);
        }
        // CHICKS
        foreach (Identifiable.Id chicks in Identifiable.CHICK_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(chicks);

            prefab.transform.localScale *= 2;
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
        }
        // ELDERS
        foreach (Identifiable.Id elders in Identifiable.ELDER_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(elders);

            prefab.transform.localScale *= 2;
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
        }
        // PLORTS
        foreach (Identifiable.Id plorts in Identifiable.PLORT_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(plorts);

            prefab.transform.localScale *= 2;
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
        }
        // CRATES
        foreach (Identifiable.Id crates in Identifiable.STANDARD_CRATE_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(crates);

            prefab.transform.localScale *= 2;
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
        }
        // VEGGIES
        foreach (Identifiable.Id veggies in Identifiable.VEGGIE_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(veggies);

            prefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
        }
        // FRUIT
        foreach (Identifiable.Id fruit in Identifiable.FRUIT_CLASS)
        {
            GameObject prefab = Prefab.GetPrefab(fruit);

            prefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
        }
    }
}