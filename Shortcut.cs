// default
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// my imports
using UnityEngine;
using SRML.Utils;
using SRML.SR;
using MonomiPark.SlimeRancher.Regions;
using SRML.SR.Translation;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using SRML.SR.Utils;

namespace ShortcutLib
{
    /// <summary>
    /// Shortcut Class [SHLIB]
    /// </summary>
    public class Shortcut
    {
        /// <summary>
        /// EatMap Methods [SHLIB]
        /// </summary>
        public static class EatMap
        {
            public static void CreateBecomeMap(Identifiable.Id slimePrefab, Identifiable.Id toBecome, Identifiable.Id whatItEats, float minDrive = 1f, float extraDrive = 0f, SlimeEmotions.Emotion slimeDriver = SlimeEmotions.Emotion.AGITATION)
            {
                SlimeDefinition slimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimePrefab);

                slimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry()
                {
                    becomesId = toBecome,
                    driver = slimeDriver,
                    minDrive = minDrive,
                    eats = whatItEats,
                    extraDrive = extraDrive
                });
            }

            public static void CreateProduceMap(Identifiable.Id slimePrefab, Identifiable.Id toProduce, Identifiable.Id whatItEats, float minDrive = 1f, float extraDrive = 0f, bool isFavorite = false, int favProduceCount = 2, SlimeEmotions.Emotion slimeDriver = SlimeEmotions.Emotion.AGITATION)
            {
                SlimeDefinition slimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimePrefab);

                slimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry()
                {
                    producesId = toProduce,
                    driver = slimeDriver,
                    minDrive = minDrive,
                    eats = whatItEats,
                    isFavorite = isFavorite,
                    favoriteProductionCount = favProduceCount,
                    extraDrive = extraDrive
                });
            }
        }

        /// <summary>
        /// Toy Methods [SHLIB]
        /// </summary>
        public static class Toy
        {
            public static ToyDefinition GetToyDef(Identifiable.Id toyId)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetToyDefinition(toyId); }

            public static GameObject CreateToy(Identifiable.Id toyPrefab, Identifiable.Id newToyID, string newToyName, Sprite newToyIcon, Material toyMaterial, int toyCost = 500, Vacuumable.Size vacSetting = Vacuumable.Size.LARGE, bool isUpgradableToy = true)
            {
                GameObject ToyPrefab = Prefab.QuickPrefab(toyPrefab);

                ToyPrefab.GetComponent<Identifiable>().id = newToyID;
                ToyPrefab.GetComponent<Vacuumable>().size = vacSetting;

                ToyPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial = (Material)Prefab.Instantiate(toyMaterial);

                Translate.TranslatePedia("t." + newToyID.ToString().ToLower(), newToyName);
                LookupRegistry.RegisterIdentifiablePrefab(ToyPrefab);
                LookupRegistry.RegisterToy(newToyID, newToyIcon, toyCost, newToyName);
                ToyRegistry.RegisterBasePurchasableToy(newToyID);
                if (isUpgradableToy)
                {
                    ToyRegistry.RegisterUpgradedPurchasableToy(newToyID);
                }

                return ToyPrefab;
            }
        }

        /// <summary>
        /// Structure Methods [SHLIB]
        /// </summary>
        public static class Structure
        {
            public static SlimeAppearanceStructure AddStructure(SlimeDefinition additionalSlimeDefinition, int structureNum)
            { return new SlimeAppearanceStructure(additionalSlimeDefinition.GetAppearanceForSet(SlimeAppearance.AppearanceSaveSet.CLASSIC).Structures[structureNum]); }

            public static Material ColorRadStructure(Identifiable.Id slimePrefab, Identifiable.Id slimeMaterial, Color middleColor, Color edgeColor, [Optional] Vector3 auraSize, int materialStructureNum, int structureNum)
            {
                SlimeDefinition slimeDefinition = Slime.GetSlimeDef(slimePrefab);
                SlimeAppearance slimeAppearance = Slime.GetSlimeApp(slimeDefinition);
                slimeDefinition.AppearancesDefault[0] = slimeAppearance;

                SlimeAppearanceObject slimeAppearanceObject = PrefabUtils.CopyPrefab(slimeAppearance.Structures[structureNum].Element.Prefabs[0].gameObject).GetComponent<SlimeAppearanceObject>();

                if (auraSize == new Vector3(0, 0, 0))
                {
                    slimeAppearanceObject.transform.localScale /= 1f;
                }
                else
                {
                    slimeAppearanceObject.transform.localScale = auraSize;
                }

                Material material = (Material)Prefab.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimeMaterial).AppearancesDefault[0].Structures[materialStructureNum].DefaultMaterials[0]);
                material.SetColor("_MiddleColor", middleColor);
                material.SetColor("_EdgeColor", edgeColor);
                slimeAppearance.Structures[structureNum].DefaultMaterials[0] = material;
                slimeAppearance.Structures[structureNum].Element.Prefabs[0] = slimeAppearanceObject;

                return slimeAppearance.Structures[0].DefaultMaterials[0];
            }

            public static Material ColorGlitchStructure(Identifiable.Id slimePrefab, Identifiable.Id slimeMaterial, Color bottomColor, int materialStructureNum, int structureNum, [Optional] Vector3 trailSize)
            {
                SlimeDefinition slimeDefinition = Slime.GetSlimeDef(slimePrefab);
                SlimeAppearance slimeAppearance = Slime.GetSlimeApp(slimeDefinition);
                slimeDefinition.AppearancesDefault[0] = slimeAppearance;

                SlimeAppearanceObject slimeAppearanceObject = PrefabUtils.CopyPrefab(slimeAppearance.Structures[structureNum].Element.Prefabs[0].gameObject).GetComponent<SlimeAppearanceObject>();

                if (trailSize == new Vector3(0, 0, 0))
                {
                    slimeAppearanceObject.transform.localScale /= 0.5f;
                }
                else
                {
                    slimeAppearanceObject.transform.localScale = trailSize;
                }

                Material material = (Material)Prefab.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimeMaterial).AppearancesDefault[0].Structures[materialStructureNum].DefaultMaterials[0]);
                material.SetColor("_BottomColor", bottomColor);
                slimeAppearance.Structures[structureNum].DefaultMaterials[0] = material;
                return slimeAppearance.Structures[0].DefaultMaterials[0];
            }

            public static Material ColorStructure(Identifiable.Id slimePrefab, Identifiable.Id slimeMaterial, Color topColor, Color middleColor, Color bottomColor, [Optional] Color specColor, int materialStructureNum, int structureNum, [Optional] Vector3 structureSize)
            {
                SlimeDefinition slimeDefinition = Slime.GetSlimeDef(slimePrefab);
                SlimeAppearance slimeAppearance = Slime.GetSlimeApp(slimeDefinition);
                slimeDefinition.AppearancesDefault[0] = slimeAppearance;

                SlimeAppearanceObject slimeAppearanceObject = PrefabUtils.CopyPrefab(slimeAppearance.Structures[structureNum].Element.Prefabs[0].gameObject).GetComponent<SlimeAppearanceObject>();

                if (structureSize == new Vector3(0, 0, 0))
                {
                    slimeAppearanceObject.transform.localScale /= 1f;
                }
                else
                {
                    slimeAppearanceObject.transform.localScale = structureSize;
                }

                Material material = (Material)Prefab.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimeMaterial).AppearancesDefault[0].Structures[materialStructureNum].DefaultMaterials[0]);
                material.SetColor("_TopColor", topColor);
                material.SetColor("_MiddleColor", middleColor);
                material.SetColor("_BottomColor", bottomColor);
                if (specColor == new Color32(0,0,0,0))
                    material.SetColor("_SpecColor", middleColor);
                material.SetColor("_SpecColor", specColor);
                slimeAppearance.Structures[structureNum].DefaultMaterials[0] = material;
                return slimeAppearance.Structures[0].DefaultMaterials[0];
            }
        }

        /// <summary>
        /// Resource Methods [SHLIB]
        /// </summary>
        public static class Resource
        {
            public static GadgetDefinition GetGadgetDef(Gadget.Id gadgetId)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(gadgetId); }

            public static GameObject CreateMeat(Identifiable.Id meatPrefab, Identifiable.Id meatID, string meatName, Color vacColor, Sprite icon, string meatAnimator, Texture2D rampGreen, Texture2D rampRed, Texture2D rampBlue, Texture2D rampBlack, float transformChance = 5f, bool isChick = false, bool isElder = false)
            {
                GameObject MeatPrefab = Prefab.QuickPrefab(meatPrefab);
                MeatPrefab.name = meatName;

                SkinnedMeshRenderer mRenderMeat = MeatPrefab.transform.Find(meatAnimator).gameObject.GetComponent<SkinnedMeshRenderer>();
                Material MeatMat = UnityEngine.Object.Instantiate<Material>(mRenderMeat.sharedMaterial);
                MeatMat.SetTexture("_RampGreen", rampGreen);
                MeatMat.SetTexture("_RampRed", rampRed);
                MeatMat.SetTexture("_RampBlue",rampBlue);
                MeatMat.SetTexture("_RampBlack", rampBlack);
                mRenderMeat.sharedMaterial = MeatMat;

                MeatPrefab.GetComponent<Identifiable>().id = meatID;
                if (!isChick && !isElder)
                { MeatPrefab.GetComponent<TransformChanceOnReproduce>().transformChance = transformChance; }
                else
                { UnityEngine.Object.Destroy(MeatPrefab.GetComponent<TransformChanceOnReproduce>()); UnityEngine.Object.Destroy(MeatPrefab.GetComponent<Reproduce>()); };
                MeatPrefab.transform.Find(meatAnimator).gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial = MeatMat;

                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, MeatPrefab);
                LookupRegistry.RegisterVacEntry(meatID, vacColor, icon);
                LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(meatID, vacColor, icon));

                if (isChick)
                {
                    Identifiable.FOOD_CLASS.Add(meatID);
                    Identifiable.CHICK_CLASS.Add(meatID);
                    Identifiable.NON_SLIMES_CLASS.Add(meatID);
                }
                else if (isElder)
                {
                    Identifiable.FOOD_CLASS.Add(meatID);
                    Identifiable.MEAT_CLASS.Add(meatID);
                    Identifiable.ELDER_CLASS.Add(meatID);
                    Identifiable.NON_SLIMES_CLASS.Add(meatID);
                }
                else
                {
                    Identifiable.FOOD_CLASS.Add(meatID);
                    Identifiable.MEAT_CLASS.Add(meatID);
                    Identifiable.NON_SLIMES_CLASS.Add(meatID);
                };

                Translate.TranslatePedia("t." + meatID.ToString().ToLower(), meatName);
                LookupRegistry.RegisterIdentifiablePrefab(MeatPrefab);
                Registry.RegisterPedia(PediaDirector.Id.RESOURCES, meatID);

                return MeatPrefab;
            }

            public static GameObject CreateFood(Identifiable.Id cropPrefab, Identifiable.Id newCropID, string newCropName, Sprite icon, Texture2D rampGreen, Texture2D rampRed, Texture2D rampBlue, Texture2D rampBlack, Color vacColor, [Optional] Vector3 foodSize, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL, bool isVeggie = false, bool isFruit = false, bool isPogoFruit = false, bool isCarrot = false)
            {
                Vector3 pogofruitDefault = new Vector3(0.25f, 0.25f, 0.25f);
                Vector3 carrotDefault = new Vector3(2.0f, 2.0f, 2.0f);
                GameObject CropPrefab = Prefab.QuickPrefab(cropPrefab);
                CropPrefab.name = newCropName;

                CropPrefab.GetComponent<Identifiable>().id = newCropID;
                CropPrefab.GetComponent<Vacuumable>().size = vacSetting;
                GameObject Model = GameObjectExtensions.FindChildWithPartialName(CropPrefab, "model_", false);

                MeshRenderer CropRender = Model.GetComponent<MeshRenderer>();
                Material CropMat = (Material)Prefab.Instantiate(CropRender.sharedMaterial);
                CropMat.SetTexture("_RampGreen", rampGreen);
                CropMat.SetTexture("_RampRed", rampRed);
                CropMat.SetTexture("_RampBlue", rampBlue);
                CropMat.SetTexture("_RampBlack", rampBlack);
                CropRender.sharedMaterial = CropMat;

                if (isPogoFruit)
                    Model.transform.localScale = pogofruitDefault;
                else if (isCarrot)
                    Model.transform.localScale = carrotDefault;
                else
                    Model.transform.localScale = foodSize;

                if (foodSize == new Vector3(0, 0, 0) && !isPogoFruit && !isCarrot)
                    Model.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, CropPrefab);
                LookupRegistry.RegisterVacEntry(newCropID, vacColor, icon);
                LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(newCropID, vacColor, icon));

                if (isVeggie)
                { Identifiable.FOOD_CLASS.Add(newCropID); Identifiable.VEGGIE_CLASS.Add(newCropID); }
                else if (isFruit)
                { Identifiable.FOOD_CLASS.Add(newCropID); Identifiable.FRUIT_CLASS.Add(newCropID); }
                else { Identifiable.FOOD_CLASS.Add(newCropID); }

                Translate.TranslatePedia("t." + newCropID.ToString().ToLower(), newCropName);
                LookupRegistry.RegisterIdentifiablePrefab(CropPrefab);
                Registry.RegisterPedia(PediaDirector.Id.RESOURCES, newCropID);

                return CropPrefab;
            }

            public static GameObject CreateGarden(SpawnResource.Id spawnResourcePrefab, SpawnResource.Id newSpawnResourceID, string newSpawnResourceName, GameObject[] spawnOptions, [Optional] GameObject[] additionalSpawnOptions, Identifiable.Id newFoodID, Identifiable.Id newSproutID = Identifiable.Id.NONE, bool additionalFoods = false, float minSpawn = 10f, float maxSpawn = 20f, float minSpawnTime = 5f, float maxSpawnTime = 10f, float bonusFoodChance = 1f, int minBonusSpawn = 3)
            {
                GameObject SpawnPrefab = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetResourcePrefab(spawnResourcePrefab));
                SpawnPrefab.name = newSpawnResourceName;
                SpawnResource SpawnResource = SpawnPrefab.GetComponent<SpawnResource>();
                SpawnResource.id = newSpawnResourceID;
                SpawnResource.ObjectsToSpawn = spawnOptions;
                if (additionalFoods)
                {
                    SpawnResource.BonusObjectsToSpawn = additionalSpawnOptions;
                    SpawnResource.BonusChance = bonusFoodChance;
                    SpawnResource.minBonusSelections = minBonusSpawn;
                }
                SpawnResource.MinObjectsSpawned = minSpawn;
                SpawnResource.MaxObjectsSpawned = maxSpawn;
                SpawnResource.MinNutrientObjectsSpawned = SpawnResource.MaxObjectsSpawned;
                SpawnResource.MinSpawnIntervalGameHours = minSpawnTime;
                SpawnResource.MaxSpawnIntervalGameHours = maxSpawnTime;

                foreach (GameObject sprout in SpawnPrefab.FindChildren("Sprout"))
                {
                    if (newSproutID == Identifiable.Id.NONE)
                    {
                        GameObject SproutGameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newFoodID).FindChildWithPartialName("model_", false);
                        sprout.GetComponent<MeshFilter>().sharedMesh = SproutGameObject.GetComponent<MeshFilter>().sharedMesh;
                        sprout.GetComponent<MeshRenderer>().sharedMaterial = SproutGameObject.GetComponent<MeshRenderer>().sharedMaterial;
                    }
                    else
                    {
                        GameObject SproutGameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newSproutID).FindChildWithPartialName("model_", false);
                        sprout.GetComponent<MeshFilter>().sharedMesh = SproutGameObject.GetComponent<MeshFilter>().sharedMesh;
                        sprout.GetComponent<MeshRenderer>().sharedMaterial = SproutGameObject.GetComponent<MeshRenderer>().sharedMaterial;
                    }
                }
                foreach (Joint joint in SpawnResource.SpawnJoints)
                {
                    GameObject FoodGameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newFoodID).FindChildWithPartialName("model_", false);
                    joint.gameObject.GetComponent<MeshFilter>().sharedMesh = FoodGameObject.GetComponent<MeshFilter>().sharedMesh;
                    joint.gameObject.GetComponent<MeshRenderer>().sharedMaterial = FoodGameObject.GetComponent<MeshRenderer>().sharedMaterial;
                }

                return SpawnPrefab;
            }

            public static GameObject CreateCrate(Identifiable.Id cratePrefab, Identifiable.Id newCrateID, string newCrateName, [Optional] Color crateColor, Identifiable.Id crateMaterial = Identifiable.Id.CRATE_REEF_01, Texture2D crateTexture = null, bool textureCrate = false, int minSpawn = 3, int maxSpawn = 6, Vacuumable.Size vacSetting = Vacuumable.Size.LARGE)
            {
                GameObject CratePrefab = Prefab.QuickPrefab(cratePrefab);
                CratePrefab.name = newCrateName;

                CratePrefab.GetComponent<Identifiable>().id = newCrateID;
                CratePrefab.GetComponent<Vacuumable>().size = vacSetting;
                CratePrefab.GetComponent<BreakOnImpact>().minSpawns = minSpawn;
                CratePrefab.GetComponent<BreakOnImpact>().maxSpawns = maxSpawn;

                GameObject crateMatPrefab = Prefab.QuickPrefab(crateMaterial);
                CratePrefab.GetComponent<MeshRenderer>().material = UnityEngine.Object.Instantiate<Material>(crateMatPrefab.GetComponent<MeshRenderer>().material);
                CratePrefab.GetComponent<MeshRenderer>().material.SetColor("_Color", crateColor);

                if (textureCrate)
                { CratePrefab.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", crateTexture); }

                Identifiable.STANDARD_CRATE_CLASS.Add(newCrateID);
                Translate.TranslateActor("l." + newCrateID.ToString().ToLower(), newCrateName);
                LookupRegistry.RegisterIdentifiablePrefab(CratePrefab);
                Registry.RegisterPedia(PediaDirector.Id.RESOURCES, newCrateID);

                return CratePrefab;
            }

            public static GameObject CreateBottledResource(Identifiable.Id resourcePrefab, Identifiable.Id newResourceID, string newResourceName, Sprite resourceIcon, Material outsideMaterial, Material insideMaterial, Color vacColor, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL)
            {
                GameObject resourceObject = Prefab.QuickPrefab(resourcePrefab);
                resourceObject.GetComponent<Identifiable>().id = newResourceID;
                resourceObject.name = newResourceName;

                resourceObject.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().material = insideMaterial;
                resourceObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = outsideMaterial;

                resourceObject.GetComponent<Vacuumable>().size = vacSetting;

                Identifiable.CRAFT_CLASS.Add(newResourceID);
                Identifiable.NON_SLIMES_CLASS.Add(newResourceID);
                Translate.TranslatePedia("t." + newResourceID.ToString().ToLower(), newResourceName);
                LookupRegistry.RegisterIdentifiablePrefab(resourceObject);
                Registry.RegisterPedia(PediaDirector.Id.RESOURCES, newResourceID);
                AmmoRegistry.RegisterSiloAmmo((SiloStorage.StorageType x) => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.CRAFTING, newResourceID);
                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newResourceID));
                AmmoRegistry.RegisterRefineryResource(newResourceID);
                LookupRegistry.RegisterVacEntry(newResourceID, vacColor, resourceIcon);

                return resourceObject;
            }

            public static Extractor AddResourceToEx(Gadget.Id extractorId, Identifiable.Id resourceId, float weight, bool restrictZone = false, ZoneDirector.Zone zone = ZoneDirector.Zone.NONE)
            {
                Extractor component = SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(extractorId).prefab.GetComponent<Extractor>();
                Extractor.ProduceEntry item = new Extractor.ProduceEntry();

                if (!restrictZone)
                {
                    item = new Extractor.ProduceEntry
                    {
                        id = resourceId,
                        weight = weight,
                        spawnFX = component.produces[0].spawnFX
                    };
                } else if (restrictZone)
                {
                    item = new Extractor.ProduceEntry
                    {
                        id = resourceId,
                        weight = weight,
                        restrictZone = restrictZone,
                        zone = zone,
                        spawnFX = component.produces[0].spawnFX
                    };
                }

                List<Extractor.ProduceEntry> list = new List<Extractor.ProduceEntry>(); //Create a new list
                foreach (Extractor.ProduceEntry item2 in component.produces) //For each thing it produces
                {
                    list.Add(item2); //Add it to the list
                }
                list.Add(item); //Add your custom 'Extractor.ProduceEntry' to the list
                component.produces = list.ToArray(); //Set the new list

                return component;
            }

            public static GameObject CreateExtractor(Gadget.Id gadgetPrefab, Gadget.Id newGadgetID, string newGadgetName, Sprite newIcon, string newGadgetDescription, int extractorCost, int extractorBuyLimit, GadgetDefinition.CraftCost[] craftCosts, Extractor.ProduceEntry[] extractorProduces, ProgressDirector.ProgressType zoneUnlock, float unlockTime = 3f, int cycles = 3, int minProduce = 3, int maxProduce = 6, float timePerCycle = 12, bool infiniteCycles = false)
            {

                GameObject GadgetPrefab = Prefab.ObjectPrefab(Resource.GetGadgetDef(gadgetPrefab).prefab);
                GadgetPrefab.name = newGadgetName;

                GameObject fx = GadgetPrefab.GetComponent<Extractor>().produces[0].spawnFX;

                GadgetPrefab.GetComponent<Gadget>().id = newGadgetID;
                GadgetPrefab.GetComponent<Extractor>().produceMin = minProduce;
                GadgetPrefab.GetComponent<Extractor>().produceMax = maxProduce;
                GadgetPrefab.GetComponent<Extractor>().hoursPerCycle = timePerCycle;
                GadgetPrefab.GetComponent<Extractor>().produces = extractorProduces;
                GadgetPrefab.GetComponent<Extractor>().produces[0].spawnFX = fx;

                if (!infiniteCycles)
                { GadgetPrefab.GetComponent<Extractor>().cycles = cycles; }
                else { GadgetPrefab.GetComponent<Extractor>().infiniteCycles = infiniteCycles; }

                Sprite GadgetIcon = newIcon;
                LookupRegistry.RegisterGadget(new GadgetDefinition
                {
                    prefab = GadgetPrefab,
                    id = newGadgetID,
                    pediaLink = PediaDirector.Id.EXTRACTORS,
                    blueprintCost = extractorCost,
                    buyCountLimit = extractorBuyLimit,
                    icon = GadgetIcon,
                    craftCosts = craftCosts
                });

                Gadget.EXTRACTOR_CLASS.Add(newGadgetID);

                new GadgetTranslation(newGadgetID).SetNameTranslation(newGadgetName).SetDescriptionTranslation(newGadgetDescription);

                GadgetRegistry.RegisterBlueprintLock(newGadgetID, x => x.CreateBasicLock(newGadgetID, Gadget.Id.NONE, zoneUnlock, unlockTime));

                return GadgetPrefab;
            }

            public static void ColorExtractor(Gadget.Id newExtractorId, Color firstColor, Color secondColor, Color thirdColor, Color fourthColor, bool isApiary = false, bool isPump = false, bool isDrill = false)
            {
                GameObject NewGadgetPrefab = Prefab.ObjectPrefab(GetGadgetDef(newExtractorId).prefab);

                string ext23 = "none";
                string extr = "none";

                if (isApiary)
                { ext23 = "ext_apiary23"; extr = "ext_r1"; }

                if (isPump)
                { ext23 = "ext_pump23"; extr = "ext_r3"; }

                if (isDrill)
                { ext23 = "ext_drill23"; extr = "ext_r2"; }

                if (firstColor == null && secondColor == null && thirdColor == null && fourthColor == null)
                { firstColor = Color.white; secondColor = Color.black; thirdColor = Color.white; fourthColor = Color.black; }

                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material);

                // recolor x4 shader thingy (core)
                // red
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", firstColor);
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", firstColor);
                // green
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", secondColor);
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", secondColor);
                // blue
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", thirdColor);
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", thirdColor);
                // black
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", fourthColor);
                NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", fourthColor);

                // recolor x4 shader thingy (ext)
                // red
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", firstColor);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", firstColor);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", firstColor);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", firstColor);
                // green
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", secondColor);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", secondColor);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", secondColor);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", secondColor);
                // blue
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", thirdColor);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", thirdColor);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", thirdColor);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", thirdColor);
                // black
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", fourthColor);
                NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", fourthColor);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", fourthColor);
                NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", fourthColor);
            }
        }

        /// <summary>
        /// Spawner Methods [SHLIB]
        /// </summary>
        public static class Spawner
        {
            public static void SlimeSpawner(Identifiable.Id slimeBeingSpawned, float weight, ZoneDirector.Zone location)
            {
                SRCallbacks.PreSaveGameLoad += (s =>
                {
                    foreach (DirectedSlimeSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>()
                        .Where(ss =>
                        {
                            ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                            return zone == location;
                        }))
                    {
                        foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                        {
                            List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(slimeBeingSpawned),
                                weight = weight // The higher the value is the more often your slime will spawn
                            }
                        };
                            constraint.slimeset.members = members.ToArray();
                        }
                    }
                });
            }

            public static void ActorSpawner(Identifiable.Id actorBeingSpawned, float weight, ZoneDirector.Zone location)
            {
                SRCallbacks.PreSaveGameLoad += (s =>
                {
                    foreach (DirectedActorSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedActorSpawner>()
                        .Where(ss =>
                        {
                            ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                            return zone == location;
                        }))
                    {
                        foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                        {
                            List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(actorBeingSpawned),
                                weight = weight // The higher the value is the more often your slime will spawn
                            }
                        };
                            constraint.slimeset.members = members.ToArray();
                        }
                    }
                });
            }

            public static void MeatSpawner(Identifiable.Id meatBeingSpawned, float weight, ZoneDirector.Zone location)
            {
                SRCallbacks.PreSaveGameLoad += (s =>
                {
                    foreach (DirectedAnimalSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedAnimalSpawner>()
                        .Where(ss =>
                        {
                            ZoneDirector.Zone zone = ss.GetComponentInParent<Region>(true).GetZoneId();
                            return zone == location;
                        }))
                    {
                        foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                        {
                            List<SlimeSet.Member> members = new List<SlimeSet.Member>(constraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = GameContext.Instance.LookupDirector.GetPrefab(meatBeingSpawned),
                                weight = weight // The higher the value is the more often your slime will spawn
                            }
                        };
                            constraint.slimeset.members = members.ToArray();
                        }
                    }
                });
            }

            public static List<SpawnResource> ReplaceFoodSpawner(Identifiable.Id foodPrefab, GameObject foodObject)
            {
                GameObject FoodPrefab = Prefab.GetPrefab(foodPrefab);
                List<SpawnResource> spawnResources = new List<SpawnResource>();
                SRCallbacks.OnSaveGameLoaded += context =>
                {
                    foreach (var spawnResource in UnityEngine.Object.FindObjectsOfType<SpawnResource>())
                    {
                        if (spawnResource.ObjectsToSpawn.Contains(FoodPrefab))
                        {

                            spawnResource.ObjectsToSpawn = new GameObject[] { foodObject };
                            spawnResources.Add(spawnResource);
                        }
                    }
                };

                return spawnResources;
            }
        }

        /// <summary>
        /// Prefab Methods [SHLIB]
        /// </summary>
        public static class Prefab
        {
            public static GameObject GetPrefab(Identifiable.Id objectToGet)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(objectToGet); }

            public static GameObject QuickPrefab(Identifiable.Id objectToCopy)
            { return PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(objectToCopy)); }

            public static GameObject ObjectPrefab(GameObject objectToCopy)
            { return PrefabUtils.CopyPrefab(objectToCopy); }

            public static UnityEngine.Object DeepPrefab(UnityEngine.Object objectToCopy)
            { return PrefabUtils.DeepCopyObject(objectToCopy); }

            public static UnityEngine.Object Instantiate(UnityEngine.Object objectToCopy)
            { return UnityEngine.Object.Instantiate(objectToCopy); }
        }

        /// <summary>
        /// EnumParse Methods [SHLIB]
        /// </summary>
        public static class EnumP
        {
            public static Identifiable.Id ParseID(string enumId)
            { return (Identifiable.Id)Enum.Parse(typeof(Identifiable.Id), enumId); }

            public static Gadget.Id ParseGad(string enumId)
            { return (Gadget.Id)Enum.Parse(typeof(Gadget.Id), enumId); }

            public static PediaDirector.Id ParsePedia(string enumId)
            { return (PediaDirector.Id)Enum.Parse(typeof(PediaDirector.Id), enumId); }
        }

        /// <summary>
        /// Slime Methods [SHLIB]
        /// </summary>
        public static class Slime
        {
            public static GameObject GetGordo(Identifiable.Id gordoPrefab)
            { return SRSingleton<GameContext>.Instance.LookupDirector.GetGordo(gordoPrefab); }

            public static SlimeDefinition GetSlimeDef(Identifiable.Id slimePrefab)
            { return SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimePrefab); }

            public static SlimeAppearance GetSlimeApp(SlimeDefinition slimeDefinition)
            { return (SlimeAppearance)Prefab.DeepPrefab(slimeDefinition.AppearancesDefault[0]); }

            public static (SlimeDefinition, GameObject, SlimeAppearance) CreateSlime(Identifiable.Id slimePrefab, Identifiable.Id slimeObjectPrefab, Identifiable.Id newSlimeID, string newSlimeName, Sprite newIcon, Color vacColor, Color SplatColor1, [Optional] Color SplatColor2, [Optional] Color SplatColor3, [Optional] Color AmmoColor, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL)
            {
                GameObject slimeObject = Prefab.QuickPrefab(slimeObjectPrefab);
                SlimeDefinition preSlimeDefinition = GetSlimeDef(slimePrefab);
                SlimeDefinition slimeDefinition = (SlimeDefinition)Prefab.DeepPrefab(preSlimeDefinition);
                SlimeAppearance slimeAppearance = GetSlimeApp(preSlimeDefinition);

                if (SplatColor2 == new Color32(0, 0, 0, 0))
                { SplatColor2 = SplatColor1; }
                else if (SplatColor3 == new Color32(0, 0, 0, 0))
                { SplatColor3 = SplatColor1; }
                else if (AmmoColor == new Color32(0, 0, 0, 0))
                { AmmoColor = vacColor; }

                slimeAppearance.Face.OnEnable();
                slimeAppearance.Icon = newIcon;
                slimeAppearance.ColorPalette = new SlimeAppearance.Palette
                {
                    Top = SplatColor1,
                    Middle = SplatColor2,
                    Bottom = SplatColor3,
                    Ammo = AmmoColor
                };

                slimeDefinition.name = newSlimeName;
                slimeDefinition.AppearancesDefault = new SlimeAppearance[1];
                slimeDefinition.IdentifiableId = newSlimeID;
                slimeDefinition.Diet.EatMap?.Clear();
                slimeDefinition.AppearancesDefault[0] = slimeAppearance;

                slimeObject.name = newSlimeName;
                slimeObject.GetComponent<PlayWithToys>().slimeDefinition = slimeDefinition;
                slimeObject.GetComponent<SlimeAppearanceApplicator>().SlimeDefinition = slimeDefinition;
                slimeObject.GetComponent<SlimeEat>().slimeDefinition = slimeDefinition;
                slimeObject.GetComponent<Identifiable>().id = newSlimeID;
                slimeObject.GetComponent<Vacuumable>().size = vacSetting;
                UnityEngine.Object.Destroy(slimeObject.GetComponent<PinkSlimeFoodTypeTracker>());

                TranslationPatcher.AddPediaTranslation("t." + newSlimeID.ToString().ToLower(), newSlimeName);
                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, slimeObject);
                LookupRegistry.RegisterVacEntry(newSlimeID, vacColor, newIcon);
                LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(newSlimeID, vacColor, newIcon));

                SceneContext.Instance.SlimeAppearanceDirector.RegisterDependentAppearances(slimeDefinition, slimeDefinition.AppearancesDefault[0]);
                SceneContext.Instance.SlimeAppearanceDirector.UpdateChosenSlimeAppearance(slimeDefinition, slimeDefinition.AppearancesDefault[0]);

                Identifiable.SLIME_CLASS.Add(newSlimeID);
                slimeObject.GetComponent<SlimeAppearanceApplicator>().Appearance = slimeAppearance;
                LookupRegistry.RegisterIdentifiablePrefab(slimeObject);
                SlimeRegistry.RegisterSlimeDefinition(slimeDefinition);

                return (slimeDefinition, slimeObject, slimeAppearance);
            }

            public static Material ColorSlime(Identifiable.Id slimePrefab, Identifiable.Id slimeMaterial, Color Color1, Color Color2, Color Color3, Color Color4, float Shininess = 1f, float Gloss = 1f)
            {
                SlimeAppearance slimeAppearance = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimePrefab).AppearancesDefault[0];

                Material material = UnityEngine.Object.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slimeMaterial).AppearancesDefault[0].Structures[0].DefaultMaterials[0]);
                material.SetColor("_TopColor", Color1);
                material.SetColor("_MiddleColor", Color2);
                material.SetColor("_BottomColor", Color3);
                material.SetColor("_SpecColor", Color4);
                material.SetFloat("_Shininess", Shininess);
                material.SetFloat("_Gloss", Gloss);
                slimeAppearance.Structures[0].DefaultMaterials[0] = material;

                return slimeAppearance.Structures[0].DefaultMaterials[0];
            }

            public static GameObject CreatePlort(Identifiable.Id plortPrefab, Identifiable.Id newPlortID, string newPlortName, Sprite newIcon, Color32 vacColor, Identifiable.Id plortMaterial = Identifiable.Id.PINK_PLORT, float plortPrice = 12, float plortSaturation = 5, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL)
            {
                GameObject plortObject = Prefab.QuickPrefab(plortPrefab);
                plortObject.name = newPlortName;

                plortObject.GetComponent<Identifiable>().id = newPlortID;
                plortObject.GetComponent<Vacuumable>().size = vacSetting;

                GameObject plortMatPrefab = Prefab.QuickPrefab(plortMaterial);
                plortObject.GetComponent<MeshRenderer>().material = UnityEngine.Object.Instantiate<Material>(plortMatPrefab.GetComponent<MeshRenderer>().material);

                Translate.TranslateActor("l." + newPlortID.ToString().ToLower(), newPlortName);
                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, plortObject);
                AmmoRegistry.RegisterSiloAmmo(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.PLORT, newPlortID);
                LookupRegistry.RegisterVacEntry(newPlortID, vacColor, newIcon);
                LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(newPlortID, vacColor, newIcon));

                Identifiable.PLORT_CLASS.Add(newPlortID);
                Identifiable.NON_SLIMES_CLASS.Add(newPlortID);
                LookupRegistry.RegisterIdentifiablePrefab(plortObject);
                PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, newPlortID);
                PlortRegistry.AddEconomyEntry(newPlortID, plortPrice, plortSaturation);
                PlortRegistry.AddPlortEntry(newPlortID);
                DroneRegistry.RegisterBasicTarget(newPlortID);

                return plortObject;
            }

            public static GameObject ColorPlort(Identifiable.Id plortPrefab, Color Color1, Color Color2, Color Color3, [Optional] Color RockColor1, [Optional] Color RockColor2, [Optional] Color RockColor3, bool hasRocks = false)
            {
                GameObject PlortPrefab = Prefab.GetPrefab(plortPrefab);

                PlortPrefab.GetComponent<MeshRenderer>().material.SetColor("_TopColor", Color1);
                PlortPrefab.GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", Color2);
                PlortPrefab.GetComponent<MeshRenderer>().material.SetColor("_BottomColor", Color3);

                if (hasRocks)
                {
                    PlortPrefab.transform.Find("rocks").GetComponent<MeshRenderer>().material.SetColor("_TopColor", RockColor1);
                    PlortPrefab.transform.Find("rocks").GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", RockColor2);
                    PlortPrefab.transform.Find("rocks").GetComponent<MeshRenderer>().material.SetColor("_BottomColor", RockColor3);
                }

                return PlortPrefab;
            }

            public static (SlimeDefinition, GameObject) CreateGordo(Identifiable.Id gordoPrefab, Identifiable.Id basePrefab, Identifiable.Id newGordoID, Sprite newGordoIcon, string newGordoName, string mapMarkerName, ZoneDirector.Zone gordoZone, int feedCount, GameObject gordoReward1, GameObject gordoReward2, GameObject gordoReward3, Vacuumable.Size vacSetting = Vacuumable.Size.LARGE)
            {
                GameObject GordoPrefab = Prefab.ObjectPrefab(Slime.GetGordo(gordoPrefab));
                GordoPrefab.name = newGordoName;
                GameObject BasePrefab = Prefab.QuickPrefab(basePrefab);
                BasePrefab.GetComponent<Vacuumable>().size = vacSetting;
                SlimeDefinition BasePrefabDef = GetSlimeDef(basePrefab);

                Material ModelMat = BasePrefabDef.AppearancesDefault[0].Structures[0].DefaultMaterials[0];

                SlimeEyeComponents baseSlimeEyes = BasePrefab.GetComponent<SlimeEyeComponents>();
                SlimeMouthComponents baseSlimeMouth = BasePrefab.GetComponent<SlimeMouthComponents>();

                GordoFaceComponents GordoFace = GordoPrefab.GetComponent<GordoFaceComponents>();
                GordoFace.strainEyes = baseSlimeEyes.scaredEyes;
                GordoFace.strainMouth = baseSlimeMouth.chompClosedMouth;
                GordoFace.blinkEyes = baseSlimeEyes.blinkEyes;
                GordoFace.chompOpenMouth = baseSlimeMouth.chompOpenMouth;
                GordoFace.happyMouth = baseSlimeMouth.happyMouth;

                GordoDisplayOnMap disp = GordoPrefab.GetComponent<GordoDisplayOnMap>();
                GameObject MarkerPrefab = Prefab.ObjectPrefab(disp.markerPrefab.gameObject);
                MarkerPrefab.name = mapMarkerName;
                MarkerPrefab.GetComponent<Image>().sprite = newGordoIcon;
                
                disp.markerPrefab = MarkerPrefab.GetComponent<MapMarker>();

                GordoIdentifiable iden = GordoPrefab.GetComponent<GordoIdentifiable>();
                iden.id = newGordoID;
                iden.nativeZones = new ZoneDirector.Zone[1] { gordoZone };

                GordoEat eat = GordoPrefab.GetComponent<GordoEat>();
                SlimeDefinition oldDefinition = (SlimeDefinition)Prefab.DeepPrefab(eat.slimeDefinition);

                oldDefinition.AppearancesDefault = BasePrefabDef.AppearancesDefault;
                oldDefinition.Diet = BasePrefabDef.Diet;
                oldDefinition.IdentifiableId = newGordoID;
                oldDefinition.name = newGordoName;
                eat.slimeDefinition = oldDefinition;
                eat.targetCount = feedCount;

                List<GameObject> rewards = new List<GameObject>()
                {
                    gordoReward1,
                    gordoReward2,
                    gordoReward3
                };

                GordoRewards gordoRewards = GordoPrefab.GetComponent<GordoRewards>();
                gordoRewards.rewardPrefabs = rewards.ToArray();
                gordoRewards.slimePrefab = GameContext.Instance.LookupDirector.GetPrefab(basePrefab);
                gordoRewards.rewardOverrides = new GordoRewards.RewardOverride[0];

                GameObject child = GordoPrefab.transform.Find("Vibrating/slime_gordo").gameObject;
                SkinnedMeshRenderer render = child.GetComponent<SkinnedMeshRenderer>();
                render.sharedMaterial = ModelMat;
                render.sharedMaterials[0] = ModelMat;
                render.material = ModelMat;
                render.materials[0] = ModelMat;

                Translate.TranslatePedia("t." + newGordoID.ToString().ToLower(), newGordoName);
                Identifiable.GORDO_CLASS.Add(newGordoID);
                LookupRegistry.RegisterGordo(GordoPrefab);

                return (null, null);
            }
        }

        /// <summary>
        /// Largo Methods [SHLIB]
        /// </summary>
        public static class Largo
        {
            public static (SlimeDefinition, GameObject, SlimeAppearance) CreateLargo(string largoName, Identifiable.Id firstSlime, Identifiable.Id secondSlime, Identifiable.Id newLargoID, float minDrive = 0.5f, int largoSize = 1, SlimeRegistry.LargoProps largoProperties = SlimeRegistry.LargoProps.NONE, SlimeEmotions.Emotion largoDriver = SlimeEmotions.Emotion.AGITATION)
            {
                Translate.TranslateActor("l." + newLargoID.ToString().ToLower(), largoName);
                SlimeRegistry.CraftLargo(newLargoID, firstSlime, secondSlime, largoProperties, out SlimeDefinition largoDefinition, out SlimeAppearance largoAppearance, out GameObject largoObject);
                largoDefinition.name = largoName;

                if (largoSize == 0)
                    largoObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                else
                    largoObject.transform.localScale *= largoSize;

                SlimeDefinition firstSlimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(firstSlime);
                SlimeDefinition secondSlimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(secondSlime);

                firstSlimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry()
                {
                    becomesId = newLargoID,
                    driver = largoDriver,
                    minDrive = minDrive,
                    eats = secondSlimeDefinition.Diet.Produces[0]
                });

                secondSlimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry()
                {
                    becomesId = newLargoID,
                    driver = largoDriver,
                    minDrive = minDrive,
                    eats = firstSlimeDefinition.Diet.Produces[0]
                });

                return (largoDefinition, largoObject, largoAppearance);
            }
        }

        /// <summary>
        /// Translate Methods [SHLIB]
        /// </summary>
        public static class Translate
        {
            public static void TranslateActor(string actorKey, string actorTranslated)
            { TranslationPatcher.AddActorTranslation(actorKey, actorTranslated); }

            public static void TranslatePedia(string pediaKey, string pediaTranslated)
            { TranslationPatcher.AddPediaTranslation(pediaKey, pediaTranslated); }

            public static void TranslateUI(string uiKey, string uiTranslated)
            { TranslationPatcher.AddUITranslation(uiKey, uiTranslated); }

            public static void TranslateKey(string bundle, string key, string value)
            { TranslationPatcher.AddTranslationKey(bundle, key, value); }

            public static void TranslateAchieve(string achieveKey, string achieveTranslated)
            { TranslationPatcher.AddAchievementTranslation(achieveKey, achieveTranslated); }

            public static void TranslateExchange(string exchangeKey, string exchangeTranslated)
            { TranslationPatcher.AddExchangeTranslation(exchangeKey, exchangeTranslated); }

            public static void TranslateGlobal(string globalKey, string globalTranslated)
            { TranslationPatcher.AddGlobalTranslation(globalKey, globalTranslated); }

            public static void TranslateMail(string mailKey, string mailTranslated)
            { TranslationPatcher.AddMailTranslation(mailKey, mailTranslated); }

            public static void TranslateSRMLError(MessageDirector.Lang language, string errorKey, string errorTranslated)
            { TranslationPatcher.AddSRMLErrorUITranslation(language, errorKey, errorTranslated); }

            public static void TranslateTutorial(string tutorialKey, string tutorialTranslated)
            { TranslationPatcher.AddTutorialTranslation(tutorialKey, tutorialTranslated); }

            public static void CreateSlimepedia(Identifiable.Id id, PediaDirector.Id entry, Sprite icon, string pediaTitle, string pediaIntro, string pediaDiet, string pediaFavorite, string pediaSlimeology, string pediaRisks, string pediaPlortonomics)
            {
                PediaRegistry.RegisterIdEntry(entry, icon);
                PediaRegistry.RegisterIdentifiableMapping((PediaDirector.Id)1, id);
                PediaRegistry.RegisterIdentifiableMapping(entry, id);
                PediaRegistry.SetPediaCategory(entry, (PediaRegistry.PediaCategory)1);
                new SlimePediaEntryTranslation(entry)
                    .SetTitleTranslation(pediaTitle)
                    .SetIntroTranslation(pediaIntro)
                    .SetDietTranslation(pediaDiet)
                    .SetFavoriteTranslation(pediaFavorite)
                    .SetSlimeologyTranslation(pediaSlimeology)
                    .SetRisksTranslation(pediaRisks)
                    .SetPlortonomicsTranslation(pediaPlortonomics);
            }

            public static void CreateResourcePedia(Identifiable.Id id, PediaDirector.Id entry, Sprite icon, string pediaTitle, string pediaIntro, string pediaResourceType, string pediaFavoredBy, string pediaDescription)
            {
                PediaRegistry.RegisterIdEntry(entry, icon);
                PediaRegistry.RegisterIdentifiableMapping((PediaDirector.Id)2, id);
                PediaRegistry.RegisterIdentifiableMapping(entry, id);
                PediaRegistry.SetPediaCategory(entry, (PediaRegistry.PediaCategory)2);
                new SlimePediaEntryTranslation(entry).SetTitleTranslation(pediaTitle).SetIntroTranslation(pediaIntro);
                TranslationPatcher.AddPediaTranslation("m.resource_type." + entry.ToString().ToLower(), pediaResourceType);
                TranslationPatcher.AddPediaTranslation("m.favored_by." + entry.ToString().ToLower(), pediaFavoredBy);
                TranslationPatcher.AddPediaTranslation("m.desc." + entry.ToString().ToLower(), pediaDescription);
            }
        }

        /// <summary>
        /// Fashion Pod Methods [SHLIB]
        /// </summary>
        public static class FashionP
        {
            public static GameObject CreateFashion(Sprite icon, string fashionName, Identifiable.Id fashionPrefab, Identifiable.Id fashionId, GameObject clipOnPrefab, Color vacColor, Fashion.Slot slot = Fashion.Slot.FRONT)
            {
                GameObject FashionPrefab = Prefab.QuickPrefab(fashionPrefab);

                FashionPrefab.GetComponent<Identifiable>().id = fashionId;
                FashionPrefab.GetComponent<Fashion>().slot = slot;

                FashionPrefab.GetComponent<Fashion>().attachPrefab = clipOnPrefab;
                foreach (Image image in FashionPrefab.GetComponentsInChildren<Image>())
                { image.sprite = icon; }

                Identifiable.FASHION_CLASS.Add(fashionId);
                LookupRegistry.RegisterIdentifiablePrefab(FashionPrefab);
                LookupRegistry.RegisterVacEntry(fashionId, vacColor, icon);
                AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, FashionPrefab);
                Translate.TranslateActor("l." + fashionId.ToString().ToLower(), fashionName);

                return FashionPrefab;
            }

            public static GameObject CreatePod(Gadget.Id podPrefab, Gadget.Id newPodID, string newPodName, string newPodDesc, Sprite newIcon, Identifiable.Id fashionId, ProgressDirector.ProgressType zoneUnlock, GadgetDefinition.CraftCost[] craftCost, float unlockTime = 3f, int podCost = 1000, int podLimit = 20)
            {
                GameObject PodPrefab = Prefab.ObjectPrefab(Resource.GetGadgetDef(podPrefab).prefab);
                PodPrefab.name = newPodName;

                PodPrefab.GetComponent<Gadget>().id = newPodID;
                PodPrefab.GetComponent<FashionPod>().fashionId = fashionId;

                GadgetRegistry.RegisterBlueprintLock(newPodID, (GadgetDirector x) => x.CreateBasicLock(newPodID, Gadget.Id.NONE, zoneUnlock, unlockTime));
                LookupRegistry.RegisterGadget(new GadgetDefinition
                {
                    prefab = PodPrefab,
                    id = newPodID,
                    blueprintCost = podCost,
                    buyCountLimit = podLimit,
                    icon = newIcon,
                    pediaLink = PediaDirector.Id.CURIOS,
                    craftCosts = craftCost
                });

                Gadget.FASHION_POD_CLASS.Add(newPodID);

                PodPrefab.transform.Find("model_fashionPod").GetComponent<MeshRenderer>().material.mainTexture = newIcon.texture;
                GadgetTranslationExtensions.GetTranslation(newPodID).SetNameTranslation(newPodName).SetDescriptionTranslation(newPodDesc);

                return PodPrefab;
            }
        }

        /// <summary>
        /// Registry Methods [SHLIB]
        /// </summary>
        public static class Registry
        {
            public static void RegisterGadget(Gadget.Id enumId, string enumName)
            { enumId = GadgetRegistry.CreateGadgetId(typeof(Gadget.Id), enumName); }

            public static void RegisterIdent(Identifiable.Id enumId, string enumName)
            { enumId = IdentifiableRegistry.CreateIdentifiableId(typeof(Identifiable.Id), enumName); }

            public static void RegisterPedia(PediaDirector.Id pedia, Identifiable.Id id)
            { PediaRegistry.RegisterIdentifiableMapping(pedia, id); }

            public static void RegisterSpawn(GameObject toRegister)
            { LookupRegistry.RegisterSpawnResource(toRegister); }

            public static void RegisterFoodGroup(Identifiable.Id id, SlimeEat.FoodGroup foodGroup)
            { FoodGroupRegistry.RegisterToFoodGroup(id, foodGroup); }

            public static void UnregisterFoodGroup(Identifiable.Id id, SlimeEat.FoodGroup foodGroup)
            { FoodGroupRegistry.UnregisterFromFoodGroup(id, foodGroup); }

            public static void RegisterSnare(Identifiable.Id id)
            { SnareRegistry.RegisterAsSnareable(id); }

            public static void RegisterFarmSlot([Optional] GameObject toRegister, [Optional] GameObject toRegisterDeluxe, Identifiable.Id foodId)
            {
                if (!toRegister && !toRegisterDeluxe)
                    throw new ArgumentNullException("At least one of toRegister or toRegisterDeluxe needs to be registered for a new farm slot.");

                if (toRegister)
                {
                    PlantSlotRegistry.RegisterPlantSlot(new GardenCatcher.PlantSlot()
                    {
                        id = foodId,
                        plantedPrefab = toRegister
                    });
                } else if (toRegisterDeluxe)
                {
                    PlantSlotRegistry.RegisterPlantSlot(new GardenCatcher.PlantSlot()
                    {
                        id = foodId,
                        deluxePlantedPrefab = toRegisterDeluxe
                    });
                }
            }
        }
    }
}
