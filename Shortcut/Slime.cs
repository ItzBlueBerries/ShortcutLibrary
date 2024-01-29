using ShortcutLib.Components;
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

        public static (GameObject, SlimeDefinition, SlimeAppearance) CreateSlimeBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, string name, Sprite newIcon, Color vacColor, Color SplatColor1, [Optional] Color SplatColor2, [Optional] Color SplatColor3, [Optional] Color AmmoColor, Vacuumable.Size vacSetting = Vacuumable.Size.NORMAL)
        {
            SlimeDefinition slimeDefinition = ScriptableObject.CreateInstance<SlimeDefinition>();
            slimeDefinition.name = name.Replace("Slime", "").Replace(" ", "");
            slimeDefinition.Name = slimeDefinition.name;
            slimeDefinition.IdentifiableId = identifiable;

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

            sunBearSlime.prefab.GetComponent<Identifiable>().identType = sunBearSlime;
            sunBearSlime.prefab.GetComponent<SlimeEat>().SlimeDefinition = sunBearSlime;
            sunBearSlime.prefab.GetComponent<PlayWithToys>().SlimeDefinition = sunBearSlime;
            sunBearSlime.prefab.GetComponent<ReactToToyNearby>().SlimeDefinition = sunBearSlime;
            sunBearSlime.prefab.GetComponent<SlimeVarietyModules>().BaseModule = sunBearSlime.BaseModule;
            sunBearSlime.prefab.GetComponent<SlimeVarietyModules>().SlimeModules = sunBearSlime.SlimeModules;
            sunBearSlime.prefab.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE;

            UnityEngine.Object.Destroy(sunBearSlime.prefab.GetComponent<PinkSlimeFoodTypeTracker>());

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

            // REST OF APPEARANCE
            Material slimeMaterial = UnityEngine.Object.Instantiate(slimeAppearance.Structures[0].DefaultMaterials[0]);
            slimeMaterial.hideFlags |= HideFlags.HideAndDontSave;
            slimeMaterial.name = "slimeSunBearBase";
            slimeMaterial.SetColor("_TopColor", sunBearPalette[0]);
            slimeMaterial.SetColor("_MiddleColor", sunBearPalette[1]);
            slimeMaterial.SetColor("_BottomColor", sunBearPalette[0]);
            slimeMaterial.SetColor("_SpecColor", sunBearPalette[1]);

            slimeAppearance.Structures[0].DefaultMaterials[0] = slimeMaterial;

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

            if (!SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes.FirstOrDefault(x => x == sunBearSlime))
                SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes = SRSingleton<GameContext>.Instance.SlimeDefinitions.Slimes.AddItem(sunBearSlime).ToArray();
            SRSingleton<GameContext>.Instance.SlimeDefinitions._slimeDefinitionsByIdentifiable.TryAdd(sunBearSlime, sunBearSlime);
            return (slimeDefinition, slimeObject, slimeAppearance);
        }
    }
}
