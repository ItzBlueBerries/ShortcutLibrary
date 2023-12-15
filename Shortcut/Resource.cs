using ShortcutLib.Components;
using SRML.SR.Translation;
using SRML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    public static class Resource
    {
        public static GameObject GetSpawnResource(SpawnResource.Id identifiable) => Director.Lookup.GetResourcePrefab(identifiable);

        public static GadgetDefinition GetGadgetDefinition(Gadget.Id identifiable) => Director.Lookup.GetGadgetDefinition(identifiable);

        public static GameObject CreateBirdBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Sprite icon, string name, Texture2D rampGreen, Texture2D rampRed, Texture2D rampBlue, Texture2D rampBlack, Color vacColor, float transformChance = 0.05f, bool isChick = false, bool isElder = false)
        {
            GameObject prefab = Prefab.QuickCopy(baseIdentifiable);
            prefab.name = "bird" + name.Replace(" ", "");
            prefab.GetComponent<Identifiable>().id = identifiable;

            SkinnedMeshRenderer skinnedMeshRenderer = prefab.GetComponentInChildren<Animator>().transform.Find("mesh_body1").GetComponent<SkinnedMeshRenderer>();
            Material material = UnityEngine.Object.Instantiate(skinnedMeshRenderer.sharedMaterial);
            material.SetTexture("_RampGreen", rampGreen);
            material.SetTexture("_RampRed", rampRed);
            material.SetTexture("_RampBlue", rampBlue);
            material.SetTexture("_RampBlack", rampBlack);
            skinnedMeshRenderer.sharedMaterial = material;

            if (isChick)
                Identifiable.CHICK_CLASS.Add(identifiable);
            else if (isElder)
            {
                Identifiable.MEAT_CLASS.Add(identifiable);
                Identifiable.ELDER_CLASS.Add(identifiable);
            }
            else
                Identifiable.MEAT_CLASS.Add(identifiable);

            Identifiable.FOOD_CLASS.Add(identifiable);
            Identifiable.NON_SLIMES_CLASS.Add(identifiable);

            Registry.AddIdentifiableToAmmo(identifiable);
            Registry.RegisterVaccable(identifiable, icon, vacColor, name);
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.RESOURCES, identifiable);

            Translate.Pedia("t." + identifiable.ToString().ToLower(), name);
            LookupRegistry.RegisterIdentifiablePrefab(prefab);
            return prefab;
        }

        /*public static (GameObject, Material) CreateFood(Identifiable.Id cropPrefab, Identifiable.Id newCropID, string newCropName, Sprite icon, Texture2D rampGreen, Texture2D rampRed, Texture2D rampBlue, Texture2D rampBlack, Color vacColor, [Optional] Vector3 foodSize, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL, bool isVeggie = false, bool isFruit = false, bool isPogoFruit = false, bool isCarrot = false)
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

            return (CropPrefab, CropMat);
        }

        public static GameObject CreateGarden(SpawnResource.Id spawnResourcePrefab, SpawnResource.Id newSpawnResourceID, string newSpawnResourceName, GameObject[] spawnOptions, [Optional] GameObject[] additionalSpawnOptions, Identifiable.Id newFoodID, bool additionalFoods = false, float minSpawn = 10f, float maxSpawn = 20f, float minSpawnTime = 5f, float maxSpawnTime = 10f, float bonusFoodChance = 1f, int minBonusSpawn = 3)
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
                GameObject SproutGameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newFoodID);
                sprout.GetComponent<MeshFilter>().sharedMesh = SproutGameObject.GetComponentInChildren<MeshFilter>().sharedMesh;
                sprout.GetComponent<MeshRenderer>().sharedMaterial = SproutGameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            }
            foreach (Joint joint in SpawnResource.SpawnJoints)
            {
                GameObject FoodGameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(newFoodID);
                joint.gameObject.GetComponent<MeshFilter>().sharedMesh = FoodGameObject.GetComponentInChildren<MeshFilter>().sharedMesh;
                joint.gameObject.GetComponent<MeshRenderer>().sharedMaterial = FoodGameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            }

            return SpawnPrefab;
        }

        public static GameObject CreateCrate(Identifiable.Id cratePrefab, Identifiable.Id newCrateID, string newCrateName, [Optional] Color crateColor, Identifiable.Id crateMaterial = Identifiable.Id.CRATE_REEF_01, Texture2D crateTexture = null, bool hasCustomMaterial = false, bool textureCrate = false, int minSpawn = 3, int maxSpawn = 6, Vacuumable.Size vacSetting = Vacuumable.Size.LARGE)
        {
            GameObject CratePrefab = Prefab.QuickPrefab(cratePrefab);
            CratePrefab.name = newCrateName;

            CratePrefab.GetComponent<Identifiable>().id = newCrateID;
            CratePrefab.GetComponent<Vacuumable>().size = vacSetting;
            CratePrefab.GetComponent<BreakOnImpact>().minSpawns = minSpawn;
            CratePrefab.GetComponent<BreakOnImpact>().maxSpawns = maxSpawn;

            if (hasCustomMaterial)
            {
                GameObject crateMatPrefab = Prefab.QuickPrefab(crateMaterial);
                CratePrefab.GetComponent<MeshRenderer>().material = UnityEngine.Object.Instantiate(crateMatPrefab.GetComponent<MeshRenderer>().material);
                CratePrefab.GetComponent<MeshRenderer>().material.SetColor("_Color", crateColor);
            }

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
            }
            else if (restrictZone)
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
            GadgetDefinition GadgetDef = ScriptableObject.CreateInstance<GadgetDefinition>();
            GadgetDef.prefab = GadgetPrefab;
            GadgetDef.id = newGadgetID;
            GadgetDef.pediaLink = PediaDirector.Id.EXTRACTORS;
            GadgetDef.blueprintCost = extractorCost;
            GadgetDef.buyCountLimit = extractorBuyLimit;
            GadgetDef.icon = GadgetIcon;
            GadgetDef.craftCosts = craftCosts;

            LookupRegistry.RegisterGadget(GadgetDef);

            Gadget.EXTRACTOR_CLASS.Add(newGadgetID);

            new GadgetTranslation(newGadgetID).SetNameTranslation(newGadgetName).SetDescriptionTranslation(newGadgetDescription);

            GadgetRegistry.RegisterBlueprintLock(newGadgetID, x => x.CreateBasicLock(newGadgetID, Gadget.Id.NONE, zoneUnlock, unlockTime));

            return GadgetPrefab;
        }

        public static void ColorExtractor(Gadget.Id newExtractorId, Color32[] mainColors, Color32[] extColors1, Color32[] extColors2, int extrIndex, bool isApiary = false, bool isPump = false, bool isDrill = false)
        {
            if (mainColors.Length < 8)
                throw new NullReferenceException("Please have at least 8 colors in your Main Colors Array. (This includes Ext Colors)");
            if (mainColors.Length > 8)
                throw new NullReferenceException("Please don't have more than 8 colors in your Main Colors Array. (This includes Ext Colors)");
            if (extColors1.Length < 8)
                throw new NullReferenceException("Please have at least 8 colors in your Ext Colors (1) Array. (This includes Main/Ext Colors)");
            if (extColors1.Length > 8)
                throw new NullReferenceException("Please don't have more than 8 colors in your Ext Colors (1) Array. (This includes Main/Ext Colors)");
            if (extColors2.Length < 8)
                throw new NullReferenceException("Please have at least 8 colors in your Ext Colors (2) Array. (This includes Main/Ext Colors)");
            if (extColors2.Length > 8)
                throw new NullReferenceException("Please don't have more than 8 colors in your Ext Colors (2) Array. (This includes Main/Ext Colors)");

            GameObject NewGadgetPrefab = GetGadgetDef(newExtractorId).prefab;

            string ext23 = "none";
            string extr;

            string[] index = new string[]
            {
                "ext_r1",
                "ext_r2",
                "ext_r3"
            };

            extr = index[extrIndex];

            if (isApiary)
            { ext23 = "ext_apiary23"; }

            if (isPump)
            { ext23 = "ext_pump23"; }

            if (isDrill)
            { ext23 = "ext_drill23"; }

            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material);
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material = (Material)Prefab.Instantiate(NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material);

            // recolor x4 shader thingy (core)
            // red
            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", mainColors[0]);
            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", mainColors[1]);
            // green
            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", mainColors[2]);
            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", mainColors[3]);
            // blue
            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", mainColors[4]);
            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", mainColors[5]);
            // black
            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", mainColors[6]);
            NewGadgetPrefab.transform.Find("core").GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", mainColors[7]);

            // recolor x4 shader thingy (ext)
            // red
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", extColors1[0]);
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", extColors1[1]);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color00", extColors2[0]);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color01", extColors2[1]);
            // green
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", extColors1[2]);
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", extColors1[3]);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color10", extColors2[2]);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color11", extColors2[3]);
            // blue
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", extColors1[4]);
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", extColors1[5]);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color20", extColors2[4]);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color21", extColors2[5]);
            // black
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", extColors1[6]);
            NewGadgetPrefab.transform.Find(ext23).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", extColors1[7]);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color30", extColors2[6]);
            NewGadgetPrefab.transform.Find(extr).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color31", extColors2[7]);
        }*/
    }
}
