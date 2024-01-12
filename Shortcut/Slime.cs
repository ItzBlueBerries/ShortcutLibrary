﻿using ShortcutLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Methods relating to creating slimes and plorts.
    /// </summary>
    public static class Slime
    {
        /// <summary>
        /// Deeply copies the <see cref="SlimeAppearance"/> attached to the <see cref="SlimeDefinition"/> given.
        /// </summary>
        /// <param name="slimeDefinition">The <see cref="SlimeDefinition"/> to grab the <see cref="SlimeAppearance"/> from.</param>
        /// <returns><see cref="SlimeAppearance"/></returns>
        public static SlimeAppearance CopySlimeAppearance(SlimeDefinition slimeDefinition) =>
            (SlimeAppearance)Prefab.Instantiate(slimeDefinition.AppearancesDefault[0]);

        /// <summary>
        /// Gets the <see cref="SlimeDefinition"/> of the <see cref="Identifiable.Id"/> given.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to grab the <see cref="SlimeDefinition"/> from.</param>
        /// <returns><see cref="SlimeDefinition"/></returns>
        public static SlimeDefinition GetSlimeDefinition(Identifiable.Id identifiable) => 
            SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(identifiable);

        /// <summary>
        /// Quickly recolors a plort.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the plort.</param>
        /// <param name="topColor">The top color <see cref="Color"/> of the plort.</param>
        /// <param name="middleColor">The middle color <see cref="Color"/> of the plort.</param>
        /// <param name="bottomColor">The bottom color <see cref="Color"/> of the plort.</param>
        /// <returns><see cref="Material"/></returns>
        public static Material QuickRecolorPlort(Identifiable.Id identifiable, Color topColor, Color middleColor, Color bottomColor)
        {
            GameObject prefab = Prefab.GetPrefab(identifiable);
            MeshRenderer plortRenderer = prefab.GetComponent<MeshRenderer>();
            Material plortMaterial = (Material)Prefab.Instantiate(plortRenderer.material);

            plortRenderer.material = plortMaterial;
            plortRenderer.material.SetColor("_TopColor", topColor);
            plortRenderer.material.SetColor("_MiddleColor", middleColor);
            plortRenderer.material.SetColor("_BottomColor", bottomColor);
            return plortMaterial;
        }

        /// <summary>
        /// Creates a starting base for a plort.
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Identifiable.Id"/> that the plort copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the plort.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the plort.</param>
        /// <param name="name">The name <see cref="string"/> of the plort.</param>
        /// <param name="saturation">The price saturation <see cref="float"/> of the plort.</param>
        /// <param name="value">The base price value <see cref="float"/> of the plort.</param>
        /// <param name="vacColor">The vac color <see cref="Color"/> of the plort.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject CreatePlortBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Sprite icon, string name, float saturation, float value, Color vacColor)
        {
            GameObject prefab = Prefab.QuickCopy(baseIdentifiable);
            prefab.name = "plort" + name.ToString().Replace(" ", "").Replace("Plort", "");

            prefab.GetComponent<Identifiable>().id = identifiable;

            Identifiable.PLORT_CLASS.Add(identifiable);
            Identifiable.NON_SLIMES_CLASS.Add(identifiable);

            Registry.AddIdentifiableToAmmo(identifiable);
            Registry.AddIdentifiableToSilo(identifiable, SiloStorage.StorageType.PLORT);
            Registry.AddIdentifiableToDrone(identifiable);
            Registry.RegisterVaccable(identifiable, icon, vacColor, name);

            Translate.Actor("l." + identifiable.ToString().ToLower(), name);
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, identifiable);
            LookupRegistry.RegisterIdentifiablePrefab(prefab);

            PlortRegistry.AddPlortEntry(identifiable);
            PlortRegistry.AddEconomyEntry(identifiable, value, saturation);
            return prefab;
        }
        /*
        public static (GameObject, SlimeDefinition, SlimeAppearance) CreateSlimeBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, string name, Sprite newIcon, Color vacColor, Color SplatColor1, [Optional] Color SplatColor2, [Optional] Color SplatColor3, [Optional] Color AmmoColor, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL)
        {
            SlimeDefinition slimeDefinition = ScriptableObject.CreateInstance<SlimeDefinition>();
            slimeDefinition.name = name.Replace("Slime", "");
            slimeDefinition.Name = slimeDefinition.name;
            slimeDefinition.IdentifiableId = identifiable;

            SlimeDefinition slimeDefinition = (SlimeDefinition)Prefab.Instantiate(GetSlimeDefinition(baseIdentifiable));
            SlimeAppearance slimeAppearance = (SlimeAppearance)Prefab.Instantiate(GetSlimeDefinition(baseIdentifiable).AppearancesDefault[0]);

            // DEFINITION
            slimeDefinition.BaseModule = Get<GameObject>("moduleSlimeStandard");
            slimeDefinition.BaseSlimes = new SlimeDefinition[0];
            slimeDefinition.SlimeModules = new GameObject[] { moduleSlimeSunBear };
            slimeDefinition.Sounds = Get<SlimeSounds>("Standard");
            slimeDefinition.NativeZones = new ZoneDefinition[] { Get<ZoneDefinition>("Luminous Strand") };
            slimeDefinition.showForZones = sunBearSlime.NativeZones;

            // PREFAB
            sunBearSlime.prefab = PrefabUtils.CopyPrefab(Get<GameObject>("slimePink"));
            sunBearSlime.prefab.name = "slimeSunBear";
            sunBearSlime.PrefabScale = 1.5f;
            sunBearSlime.prefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            sunBearSlime.prefab.AddComponent<SBGeneralizedBehaviour>();
            // sunBearSlime.prefab.AddComponent<SunBearEnvironmental>();
            sunBearSlime.prefab.AddComponent<SunBearReproduction>();
            // sunBearSlime.prefab.AddComponent<SunBearIsolation>();
            // sunBearSlime.prefab.AddComponent<SunBearTentacleBite>();
            sunBearSlime.prefab.AddComponent<SunBearProvide>();
            sunBearSlime.prefab.AddComponent<SunBearHarvest>();
            sunBearSlime.prefab.AddComponent<SunBearAttack>();
            sunBearSlime.prefab.AddComponent<SunBearSavage>();
            sunBearSlime.prefab.AddComponent<SunBearCache>();
            sunBearSlime.prefab.AddComponent<SunBearGoto>();

            sunBearSlime.prefab.AddComponent<GotoPlayer>().PlayerIdentifiableType = Get<IdentifiableType>("Player");
            sunBearSlime.prefab.AddComponent<AttackPlayer>()._playerIdentifiableType = Get<IdentifiableType>("Player");
            sunBearSlime.prefab.GetComponent<AttackPlayer>()._damageSource = LocalInstances.sunBearAttack;
            sunBearSlime.prefab.GetComponent<AttackPlayer>().DamagePerAttack = 30;
            /*sunBearSlime.prefab.GetComponent<SlimeFeral>().feralLifetimeHours = float.PositiveInfinity;
            sunBearSlime.prefab.GetComponent<SlimeFeral>().dynamicToFeral = false;*/
        /*
            sunBearSlime.prefab.GetComponent<Identifiable>().identType = sunBearSlime;
            sunBearSlime.prefab.GetComponent<SlimeEat>().SlimeDefinition = sunBearSlime;
            sunBearSlime.prefab.GetComponent<PlayWithToys>().SlimeDefinition = sunBearSlime;
            sunBearSlime.prefab.GetComponent<ReactToToyNearby>().SlimeDefinition = sunBearSlime;
            sunBearSlime.prefab.GetComponent<SlimeVarietyModules>().BaseModule = sunBearSlime.BaseModule;
            sunBearSlime.prefab.GetComponent<SlimeVarietyModules>().SlimeModules = sunBearSlime.SlimeModules;
            sunBearSlime.prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE;

            sunBearSlime.prefab.GetComponent<SlimeHealth>().MaxHealth = 40;
            sunBearSlime.prefab.GetComponent<FleeThreats>().FearProfile = LocalInstances.sunBearSlimeFearProfile;
            sunBearSlime.prefab.GetComponent<SlimeRandomMove>()._maxJump = 4;

            if (SunBearPreferences.IsRealisticMode())
            {
                sunBearSlime.prefab.GetComponent<SlimeRandomMove>().ScootSpeedFactor = 2.5f;
                sunBearSlime.prefab.GetComponent<GotoConsumable>().PursuitSpeedFactor = 2.5f;
            }
            else
            {
                sunBearSlime.prefab.GetComponent<SlimeRandomMove>().ScootSpeedFactor = 2;
                sunBearSlime.prefab.GetComponent<GotoConsumable>().PursuitSpeedFactor = 2;
            }

            GameObject instantiatedTriggers = UnityEngine.Object.Instantiate(sunBearTriggers);
            instantiatedTriggers.transform.parent = sunBearSlime.prefab.transform;

            if (SunBearPreferences.IsCasualMode())
                UnityEngine.Object.Destroy(sunBearSlime.prefab.transform.Find("SunBearTriggers(Clone)/SunBearProtectionTrigger").gameObject);

            foreach (Il2CppSystem.Type excludedComponent in grownExcludedComponents)
            {
                if (excludedComponent == null)
                    continue;

                if (sunBearSlime.prefab.GetComponent(excludedComponent))
                    UnityEngine.Object.Destroy(sunBearSlime.prefab.GetComponent(excludedComponent));
            }

            if (SunBearPreferences.IsCasualMode())
            {
                foreach (Il2CppSystem.Type excludedComponent in casualExcludedComponents)
                {
                    if (excludedComponent == null)
                        continue;

                    if (SunBearPreferences.IsCasualWSavageMode() && excludedComponent == Il2CppType.Of<SunBearSavage>())
                        continue;

                    if (sunBearSlime.prefab.GetComponent(excludedComponent))
                        UnityEngine.Object.Destroy(sunBearSlime.prefab.GetComponent(excludedComponent));
                }
            }

            if (SunBearPreferences.IsRealisticMode() && SunBearPreferences.IsRealisticWOSavageMode())
                UnityEngine.Object.Destroy(sunBearSlime.prefab.GetComponent<SunBearSavage>());

            UnityEngine.Object.Destroy(sunBearSlime.prefab.GetComponent<AweTowardsLargos>());
            UnityEngine.Object.Destroy(sunBearSlime.prefab.GetComponent<PinkSlimeFoodTypeTracker>());
            UnityEngine.Object.Destroy(sunBearSlime.prefab.GetComponent<ColliderTotemLinkerHelper>());
            UnityEngine.Object.Destroy(sunBearSlime.prefab.transform.FindChild("TotemLinker(Clone)").gameObject);

            // DIET
            sunBearSlime.Diet = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink")).Diet;
            sunBearSlime.Diet.MajorFoodGroups = new SlimeEat.FoodGroup[] { SlimeEat.FoodGroup.FRUIT };
            sunBearSlime.Diet.MajorFoodIdentifiableTypeGroups = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("FruitGroup") };
            sunBearSlime.Diet.ProduceIdents = new IdentifiableType[] { sunBearPlort };
            sunBearSlime.Diet.AdditionalFoodIdents = new IdentifiableType[]
            {
                                Get<IdentifiableType>("Chick"),
                                Get<IdentifiableType>("StonyChick"),
                                Get<IdentifiableType>("BriarChick"),
                                Get<IdentifiableType>("SeaChick"),
                                Get<IdentifiableType>("ThunderChick"),
                                Get<IdentifiableType>("PaintedChick"),
                                Get<IdentifiableType>("WildHoneyCraft"),
                                Get<IdentifiableType>("SunSapCraft")
            };
            sunBearSlime.Diet.FavoriteIdents = new IdentifiableType[] { Get<IdentifiableType>("WildHoneyCraft") };
            sunBearSlime.Diet.RefreshEatMap(SRSingleton<GameContext>.Instance.SlimeDefinitions, sunBearSlime);

            sunBearSlime.icon = LocalAssets.iconSlimeSunBearSpr;
            sunBearSlime.properties = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").properties);
            sunBearSlime.defaultPropertyValues = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink")).defaultPropertyValues;

            // APPEARANCE
            SlimeAppearance slimeAppearance = UnityEngine.Object.Instantiate(Get<SlimeAppearance>("PinkDefault"));
            SlimeAppearanceApplicator slimeAppearanceApplicator = sunBearSlime.prefab.GetComponent<SlimeAppearanceApplicator>();
            slimeAppearance.name = "SunBearDefault";
            slimeAppearanceApplicator.Appearance = slimeAppearance;
            slimeAppearanceApplicator.SlimeDefinition = sunBearSlime;

            // APPEARANCE STRUCTURES
            // EARS
            GameObject slimeAppearanceObject = new GameObject("sunbear_ears");
            slimeAppearanceObject.Prefabitize();
            slimeAppearanceObject.hideFlags |= HideFlags.HideAndDontSave;

            slimeAppearanceObject.AddComponent<SkinnedMeshRenderer>().sharedMesh = LocalAssets.sunBearEars;

            slimeAppearanceObject.AddComponent<SlimeAppearanceObject>().hideFlags |= HideFlags.HideAndDontSave;
            slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().RootBone = SlimeAppearance.SlimeBone.JIGGLE_TOP;
            slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().ParentBone = SlimeAppearance.SlimeBone.JIGGLE_BACK;
            slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().AttachedBones = new SlimeAppearance.SlimeBone[0].AddDefaultBones();
            slimeAppearanceObject.GetComponent<SlimeAppearanceObject>().IgnoreLODIndex = true;
            // UnityEngine.Object.DontDestroyOnLoad(slimeAppearanceObject.GetComponent<SlimeAppearanceObject>());

            slimeAppearance.Structures = slimeAppearance.Structures.ToArray().AddToArray(new SlimeAppearanceStructure(slimeAppearance.Structures[0]));

            slimeAppearance.Structures[2].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[2].Element.name = "SunBearEars";
            slimeAppearance.Structures[2].Element.Name = "Sun Bear Ears";
            slimeAppearance.Structures[2].Element.Prefabs = new SlimeAppearanceObject[] { slimeAppearanceObject.GetComponent<SlimeAppearanceObject>() };
            slimeAppearance.Structures[2].Element.Type = SlimeAppearanceElement.ElementType.EARS;
            slimeAppearance.Structures[2].SupportsFaces = false;

            // REST OF APPEARANCE
            Material slimeMaterial = UnityEngine.Object.Instantiate(slimeAppearance.Structures[0].DefaultMaterials[0]);
            slimeMaterial.hideFlags |= HideFlags.HideAndDontSave;
            slimeMaterial.name = "slimeSunBearBase";
            slimeMaterial.SetColor("_TopColor", sunBearPalette[0]);
            slimeMaterial.SetColor("_MiddleColor", sunBearPalette[1]);
            slimeMaterial.SetColor("_BottomColor", sunBearPalette[0]);
            slimeMaterial.SetColor("_SpecColor", sunBearPalette[1]);

            slimeMaterial.SetTexture("_ColorMask", LocalAssets.maskSunBearMulticolor);
            slimeMaterial.SetColor("_RedTopColor", sunBearPalette[0]);
            slimeMaterial.SetColor("_RedMiddleColor", sunBearPalette[1]);
            slimeMaterial.SetColor("_RedBottomColor", sunBearPalette[0]);
            slimeMaterial.SetColor("_GreenTopColor", sunBearPalette[2]);
            slimeMaterial.SetColor("_GreenMiddleColor", sunBearPalette[3]);
            slimeMaterial.SetColor("_GreenBottomColor", sunBearPalette[2]);

            var slimeKeywords = slimeMaterial.GetShaderKeywords().ToList();
            slimeKeywords.Remove("_BODYCOLORING_DEFAULT");
            slimeKeywords.Add("_BODYCOLORING_MULTI");
            slimeMaterial.SetShaderKeywords(slimeKeywords.ToArray());

            Material earsMaterial = UnityEngine.Object.Instantiate(slimeMaterial);
            earsMaterial.hideFlags |= HideFlags.HideAndDontSave;
            earsMaterial.name = "slimeSunBearEarsBase";
            earsMaterial.SetTexture("_ColorMask", LocalAssets.maskSunBearEarsMulticolor);
            earsMaterial.SetColor("_RedTopColor", sunBearPalette[0]);
            earsMaterial.SetColor("_RedMiddleColor", sunBearPalette[1]);
            earsMaterial.SetColor("_RedBottomColor", sunBearPalette[0]);

            slimeAppearance.Structures[0].DefaultMaterials[0] = slimeMaterial;
            slimeAppearance.Structures[2].DefaultMaterials[0] = earsMaterial;

            slimeAppearance._face = UnityEngine.Object.Instantiate(Get<SlimeAppearance>("TabbyDefault").Face);
            slimeAppearance.Face.name = "faceSlimeSunBear";

            SlimeExpressionFace[] expressionFaces = new SlimeExpressionFace[0];
            foreach (SlimeExpressionFace slimeExpressionFace in slimeAppearance.Face.ExpressionFaces)
            {
                Material slimeEyes = null;
                Material slimeMouth = null;

                if (slimeExpressionFace.Eyes)
                    slimeEyes = UnityEngine.Object.Instantiate(slimeExpressionFace.Eyes);
                if (slimeExpressionFace.Mouth)
                    slimeMouth = UnityEngine.Object.Instantiate(slimeExpressionFace.Mouth);

                if (slimeEyes)
                {
                    slimeEyes.SetColor("_EyeRed", sunBearPalette[4]);
                    slimeEyes.SetColor("_EyeGreen", Color.gray);
                    slimeEyes.SetColor("_EyeBlue", sunBearPalette[4]);
                }
                if (slimeMouth)
                {
                    slimeMouth.SetColor("_MouthTop", sunBearPalette[1]);
                    slimeMouth.SetColor("_MouthMid", Color.gray);
                    slimeMouth.SetColor("_MouthBot", sunBearPalette[1]);
                }
                slimeExpressionFace.Eyes = slimeEyes;
                slimeExpressionFace.Mouth = slimeMouth;
                expressionFaces = expressionFaces.AddToArray(slimeExpressionFace);
            }
            slimeAppearance.Face.ExpressionFaces = expressionFaces;
            slimeAppearance.Face.OnEnable();

            slimeAppearance._icon = sunBearSlime.icon;
            slimeAppearance._splatColor = sunBearPalette[0];
            slimeAppearance._colorPalette = new SlimeAppearance.Palette
            {
                Ammo = sunBearPalette[0],
                Top = sunBearPalette[0],
                Middle = sunBearPalette[1],
                Bottom = sunBearPalette[0]
            };
            sunBearSlime.AppearancesDefault = new SlimeAppearance[] { slimeAppearance };
            sunBearSlime.prefab.hideFlags |= HideFlags.HideAndDontSave;
#endregion

            if (!SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes.FirstOrDefault(x => x == sunBearSlime))
                SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes = SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes.AddItem(sunBearSlime).ToArray();
            SRSingleton<GameContext>.Instance.SlimeDefinitions._slimeDefinitionsByIdentifiable.TryAdd(sunBearSlime, sunBearSlime);
            return (slimeDefinition, slimeObject, slimeAppearance);
        }
        */
    }
}
