global using SRML;
global using SRML.SR;
global using UnityEngine;
global using ShortcutLib.Presets;
global using ShortcutLib.Shortcut;
global using static ShortcutLib.Extras.Debugging;

using System;
using System.Collections.Generic;
using System.Linq;
using SRML.Utils.Enum;
using ShortcutLib.Components;

namespace ShortcutLib
{
    [EnumHolder]
    internal class Enums
    {
        // public static readonly Identifiable.Id TEST_SLIME;

        public static readonly AchievementsDirector.Achievement ACk;

        public static readonly AchievementsDirector.IntStat Stat;

        public static readonly SlimeEat.FoodGroup TEST;

        public static readonly Identifiable.Id CRATE_TEST_01;

        public static readonly Identifiable.Id TEST_FRUIT;

        public static readonly Identifiable.Id TEST_HEN;

        public static readonly SpawnResource.Id TEST_PATCH;

        public static readonly Gadget.Id TEST_EXTRACTOR;

        public static readonly Identifiable.Id TEST_GORDO;

        public static readonly Identifiable.Id TEST_PLORT;
    }

    internal class ShortcutEntry : ModEntryPoint
    {
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();

            Enums.TEST.Register("Test", new HashSet<Identifiable.Id>() { Identifiable.Id.ROOSTER });
            Enums.TEST.AddTo(Identifiable.Id.HEN);
            Enums.TEST.AddMany(new() { Identifiable.Id.POGO_FRUIT, Identifiable.Id.GINGER_VEGGIE });
            /*Enums.TEST.RemoveFrom(Identifiable.Id.POGO_FRUIT);
            Enums.TEST.RemoveMany(new List<Identifiable.Id>() { Identifiable.Id.GINGER_VEGGIE });*/

            foreach (var foodGroup in FoodGroup.foodGroupsToPatch)
                Translate.UI("m.foodgroup." + foodGroup.Key.ToString().ToLower().Replace(" ", "_"), foodGroup.Value.Item1);
        }

        public override void Load()
        {
            Achieve.AddAchievement("s", "w", Enums.ACk, AchievementRegistry.Tier.TIER1, new AchievementsDirector.CountTracker(SceneContext.Instance.AchievementsDirector, Enums.ACk, Enums.Stat, 5));
            SRCallbacks.OnZoneEntered += delegate (ZoneDirector.Zone zone, PlayerState playerState)
            {
                if (zone == ZoneDirector.Zone.REEF)
                    Achieve.AddStat(Enums.Stat, 1);
            };
            Identifiable.Id.PINK_SLIME.GetSlimeDefinition().Diet.MajorFoodGroups = new SlimeEat.FoodGroup[] { Enums.TEST };
            Resource.CreateCrateBase(Identifiable.Id.CRATE_DESERT_01, Enums.CRATE_TEST_01, "Test", new List<BreakOnImpact.SpawnOption>()
            {
                new BreakOnImpact.SpawnOption()
                {
                    spawn = Prefab.GetPrefab(Identifiable.Id.POGO_FRUIT),
                    weight = 1
                }
            });
            // Resource.CreateResourceBase(Identifiable.Id.POGO_FRUIT, Enums.TEST_FRUIT, null, "Test", Color.white);
            Edible.CreateFoodBase(Identifiable.Id.POGO_FRUIT, Enums.TEST_FRUIT, null, "Test", null, null, null, null, Color.white, false, true);
            Edible.CreateBirdBase(Identifiable.Id.HEN, Enums.TEST_HEN, null, "Test Hen", null, null, null, null, Color.black);
            var g = Resource.CreateSpawnResourceBase(SpawnResource.Id.POGO_TREE, Enums.TEST_PATCH, Enums.TEST_FRUIT, "Test", new GameObject[]
            {
                Prefab.GetPrefab(Enums.TEST_FRUIT)
            });
            Registry.RegisterPlantSlot(Enums.TEST_FRUIT, g, null);
            Resource.CreateGadgetBase(Gadget.Id.EXTRACTOR_PUMP_NOVICE, Enums.TEST_EXTRACTOR, PediaDirector.Id.EXTRACTORS, null, "Test", "Test", 1, 0, 0, false, new GadgetDefinition.CraftCost[]
            {
                new GadgetDefinition.CraftCost()
                {
                    amount = 1,
                    id = Identifiable.Id.POGO_FRUIT
                }
            });
            Gadget.EXTRACTOR_CLASS.Add(Enums.TEST_EXTRACTOR);
            Resource.QuickRecolorExtractor(Enums.TEST_EXTRACTOR, new Color[]
            {
                Color.white,
                Color.white,
                Color.white,
                Color.white,

                Color.white,
                Color.white,
                Color.white,
                Color.white,
            },
            new Color[]
            {
                Color.magenta,
                Color.magenta,
                Color.magenta,
                Color.magenta,

                Color.magenta,
                Color.magenta,
                Color.magenta,
                Color.magenta,
            },
            new Color[]
            {
                Color.yellow,
                Color.yellow,
                Color.yellow,
                Color.yellow,

                Color.yellow,
                Color.yellow,
                Color.yellow,
                Color.yellow,
            });

            Gordo.CreateGordoBase(Identifiable.Id.PINK_GORDO, Enums.TEST_GORDO, Identifiable.Id.PINK_SLIME, null, "Test Gordo", 1, new ZoneDirector.Zone[] { ZoneDirector.Zone.REEF }, new List<GameObject>());
            /*Gordo.ModifyGordoSpawns(Enums.TEST_GORDO, 6);
            Gordo.ModifyGordoSpawns(Identifiable.Id.PINK_GORDO, 50);
            Gordo.ModifyGordoSpawns(Identifiable.Id.TABBY_GORDO, 13);*/
            SRCallbacks.PreSaveGameLoad += delegate (SceneContext sceneContext)
            {
                Gordo.PositionGordo(Enums.TEST_GORDO, Prefab.FindObject("zoneREEF/cellReef_GordoIsland/Sector").transform, new Vector3(-98.1f, 6.4f, 11.5f), 0);
            };
            Slime.CreatePlortBase(Identifiable.Id.PINK_PLORT, Enums.TEST_PLORT, null, "Test Plort", 0, 100, Color.white);
            Slime.QuickRecolorPlort(Enums.TEST_PLORT, Color.white, Color.magenta, Color.yellow);
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
