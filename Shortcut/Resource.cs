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
        /// <summary>
        /// Gets the prefab <see cref="GameObject"/> of a <see cref="SpawnResource.Id"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="SpawnResource.Id"/> to grab the prefab <see cref="GameObject"/> from.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject GetSpawnResource(SpawnResource.Id identifiable) => Director.Lookup.GetResourcePrefab(identifiable);

        /// <summary>
        /// Gets the <see cref="GadgetDefinition"/> of a <see cref="Gadget.Id"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Gadget.Id"/> to grab the <see cref="GadgetDefinition"/> from.</param>
        /// <returns><see cref="GadgetDefinition"/></returns>
        public static GadgetDefinition GetGadgetDefinition(Gadget.Id identifiable) => Director.Lookup.GetGadgetDefinition(identifiable);

        /// <summary>
        /// Creates a starting base for a custom crate.
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Identifiable.Id"/> that the crate copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the crate.</param>
        /// <param name="name">The name <see cref="string"/> of the crate.</param>
        /// <param name="spawnOptions">The spawn options <see cref="BreakOnImpact.SpawnOption[]"/> of the crate.</param>
        /// <param name="minSpawns">The minimum spawns <see cref="int"/> of the crate.</param>
        /// <param name="maxSpawns">The maximum spawns <see cref="int"/> of the crate.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject CreateCrateBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, string name, [Optional] List<BreakOnImpact.SpawnOption> spawnOptions, int minSpawns = 3, int maxSpawns = 6)
        {
            GameObject prefab = Prefab.QuickCopy(baseIdentifiable);
            prefab.name = "crate" + name.Replace(" ", "");

            prefab.GetComponent<Identifiable>().id = identifiable;
            prefab.GetComponent<BreakOnImpact>().minSpawns = minSpawns;
            prefab.GetComponent<BreakOnImpact>().maxSpawns = maxSpawns;
            prefab.GetComponent<BreakOnImpact>().spawnOptions = spawnOptions ?? prefab.GetComponent<BreakOnImpact>().spawnOptions;

            Identifiable.STANDARD_CRATE_CLASS.Add(identifiable);
            // PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.RESOURCES, identifiable);

            // Translate.Actor("l." + identifiable.ToString().ToLower(), name);
            LookupRegistry.RegisterIdentifiablePrefab(prefab);
            return prefab;
        }

        /// <summary>
        /// Creates a starting base for a custom resource.
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Identifiable.Id"/> that the resource copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the resource.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the resource.</param>
        /// <param name="name">The name <see cref="string"/> of the resource.</param>
        /// <param name="vacColor">The vac color <see cref="Color"/> of the resource.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject CreateResourceBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Sprite icon, string name, Color vacColor)
        {
            GameObject prefab = Prefab.QuickCopy(baseIdentifiable);
            prefab.name = "craft" + name.Replace(" ", "");

            prefab.GetComponent<Identifiable>().id = identifiable;

            Identifiable.CRAFT_CLASS.Add(identifiable);
            Identifiable.NON_SLIMES_CLASS.Add(identifiable);

            Registry.AddIdentifiableToAmmo(identifiable);
            Registry.AddIdentifiableToSilo(identifiable);
            Registry.AddIdentifiableToRefinery(identifiable);
            Registry.RegisterVaccable(identifiable, icon, vacColor, name.Replace(" ", "") + "Craft");
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.RESOURCES, identifiable);

            Translate.Pedia("t." + identifiable.ToString().ToLower(), name);
            LookupRegistry.RegisterIdentifiablePrefab(prefab);
            return prefab;
        }

        /// <summary>
        /// Creates a starting base for a custom spawn resource. (e.g. Garden Patches/Trees)
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="SpawnResource.Id"/> that the spawn resource copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="SpawnResource.Id"/> of the spawn resource.</param>
        /// <param name="foodIdentifiable">The food <see cref="Identifiable.Id"/> that grows from the spawn resource.</param>
        /// <param name="name">The name <see cref="string"/> of the spawn resource.</param>
        /// <param name="objectsToSpawn">The objects to spawn <see cref="GameObject[]"/> from the spawn resource.</param>
        /// <param name="minSpawns">The minimum spawns <see cref="int"/> of the spawn resource.</param>
        /// <param name="maxSpawns">The maximum spawns <see cref="int"/> of the spawn resource.</param>
        /// <param name="minSpawnTime">The minimum spawn time <see cref="float"/> of the spawn resource.</param>
        /// <param name="maxSpawnTime">The maximum spawn time <see cref="float"/> of the spawn resource.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject CreateSpawnResourceBase(SpawnResource.Id baseIdentifiable, SpawnResource.Id identifiable, Identifiable.Id foodIdentifiable, string name, GameObject[] objectsToSpawn, float minSpawns = 10, float maxSpawns = 20, float minSpawnTime = 5, float maxSpawnTime = 10)
        {
            GameObject prefab = Prefab.ObjectCopy(GetSpawnResource(baseIdentifiable));

            string toString = identifiable.ToString();
            prefab.name = toString.ToUpper().Contains("TREE") ? "tree" + name.Replace(" ", "") :
                (toString.ToUpper().Contains("PATCH") ? "patch" + name.Replace(" ", "") : "garden" + name.Replace(" ", ""));

            SpawnResource spawnResource = prefab.GetComponent<SpawnResource>();
            spawnResource.id = identifiable;
            spawnResource.ObjectsToSpawn = objectsToSpawn;

            spawnResource.MinObjectsSpawned = minSpawns;
            spawnResource.MaxObjectsSpawned = maxSpawns;
            spawnResource.MinNutrientObjectsSpawned = spawnResource.MinObjectsSpawned;

            spawnResource.MinSpawnIntervalGameHours = minSpawnTime;
            spawnResource.MaxSpawnIntervalGameHours = maxSpawnTime;

            foreach (GameObject sprout in prefab.FindChildren("Sprout"))
            {
                GameObject gameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(foodIdentifiable);
                sprout.GetComponent<MeshFilter>().sharedMesh = gameObject.GetComponentInChildren<MeshFilter>().sharedMesh;
                sprout.GetComponent<MeshRenderer>().sharedMaterial = gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            }

            foreach (Joint joint in spawnResource.SpawnJoints)
            {
                GameObject gameObject = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(foodIdentifiable);
                joint.gameObject.GetComponent<MeshFilter>().sharedMesh = gameObject.GetComponentInChildren<MeshFilter>().sharedMesh;
                joint.gameObject.GetComponent<MeshRenderer>().sharedMaterial = gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            }

            LookupRegistry.RegisterSpawnResource(prefab);
            return prefab;
        }

        public static GameObject CreateGadgetBase(Gadget.Id baseIdentifiable, Gadget.Id identifiable, PediaDirector.Id pediaLink, Sprite icon, string name, string description, int blueprintCost, int countLimit, int buyCountLimit, bool destroyOnRemoval, GadgetDefinition.CraftCost[] craftCosts)
        {
            GameObject prefab = Prefab.ObjectCopy(GetGadgetDefinition(baseIdentifiable).prefab);
            prefab.name = "gadget" + name.Replace(" ", "");

            prefab.GetComponent<Gadget>().id = identifiable;

            GadgetDefinition gadgetDefinition = Definition.CreateGadDefinition(
                identifiable,
                pediaLink,
                prefab,
                icon,
                name,
                countLimit,
                blueprintCost,
                buyCountLimit,
                destroyOnRemoval,
                false,
                null,
                craftCosts
            );

            Translate.CreateGadgetTranslation(identifiable)
                .SetNameTranslation(name)
                .SetDescriptionTranslation(description);
            LookupRegistry.RegisterGadget(gadgetDefinition);
            return prefab;
        }

        /// <summary>
        /// Quickly recolors an extractor, as it is a common custom gadget with lots of color properties.
        /// </summary>
        /// <param name="identifiable">The <see cref="Gadget.Id"/> of the extractor.</param>
        /// <param name="coreColors">The color array <see cref="Color[]"/> for the core colors of the extractor. Requires 8 colors exact.</param>
        /// <param name="ext23Colors">The color array <see cref="Color[]"/> for the ext23 colors of the extractor. Requires 8 colors exact.</param>
        /// <param name="extrColors">The color array <see cref="Color[]"/> for the extr colors of the extractor. Requires 8 colors exact.</param>
        /// <exception cref="ShortcutLibException">This can be thrown if you do not specify the requirement of 8 colors per color array.</exception>
        public static void QuickRecolorExtractor(Gadget.Id identifiable, Color[] coreColors, Color[] ext23Colors, Color[] extrColors)
        {
            if (coreColors.Length < 8 || coreColors.Length > 8)
                throw new ShortcutLibException("`8 colors` exact are required to be in the `coreColors` array while using `QuickRecolorExtractor`.");

            if (ext23Colors.Length < 8 || ext23Colors.Length > 8)
                throw new ShortcutLibException("`8 colors` exact are required to be in the `ext23Colors` array while using `QuickRecolorExtractor`.");

            if (extrColors.Length < 8 || extrColors.Length > 8)
                throw new ShortcutLibException("`8 colors` exact are required to be in the `extrColors` array while using `QuickRecolorExtractor`.");

            GameObject prefab = GetGadgetDefinition(identifiable).prefab;

            SkinnedMeshRenderer coreRenderer = prefab.transform.Find("core").gameObject.GetComponent<SkinnedMeshRenderer>();
            coreRenderer.material = (Material)Prefab.Instantiate(coreRenderer.material);

            GameObject ext23 = null;
            GameObject extr = null;

            foreach (Transform transform in prefab.transform)
            {
                if (!transform?.gameObject)
                    continue;

                if (transform.gameObject.name.Contains("ext_") && transform.gameObject.name.Contains("23") && transform.gameObject.activeSelf)
                {
                    ext23 = transform.gameObject;
                    break;
                }
            }

            foreach (Transform transform in prefab.transform)
            {
                if (!transform?.gameObject)
                    continue;

                if (transform.gameObject.name.Contains("ext_r") && transform.gameObject.activeSelf)
                {
                    extr = transform.gameObject;
                    break;
                }
                ShortcutConsole.Log(transform.gameObject.name);
            }

            SkinnedMeshRenderer ext23Renderer = ext23.GetComponent<SkinnedMeshRenderer>();
            ext23Renderer.material = (Material)Prefab.Instantiate(ext23Renderer.material);

            SkinnedMeshRenderer extrRenderer = null;
            if (extr)
            {
                extrRenderer = extr.GetComponent<SkinnedMeshRenderer>();
                extrRenderer.material = (Material)Prefab.Instantiate(extrRenderer.material);
            }

            // CORE
            // - 0
            coreRenderer.material.SetColor("_Color00", coreColors[0]);
            coreRenderer.material.SetColor("_Color01", coreColors[1]);
            // - 1
            coreRenderer.material.SetColor("_Color10", coreColors[2]);
            coreRenderer.material.SetColor("_Color11", coreColors[3]);
            // - 2
            coreRenderer.material.SetColor("_Color20", coreColors[4]);
            coreRenderer.material.SetColor("_Color21", coreColors[5]);
            // - 3
            coreRenderer.material.SetColor("_Color30", coreColors[6]);
            coreRenderer.material.SetColor("_Color31", coreColors[7]);

            // EXT23
            // - 0
            ext23Renderer.material.SetColor("_Color00", ext23Colors[0]);
            ext23Renderer.material.SetColor("_Color01", ext23Colors[1]);
            // - 1
            ext23Renderer.material.SetColor("_Color10", ext23Colors[2]);
            ext23Renderer.material.SetColor("_Color11", ext23Colors[3]);
            // - 2
            ext23Renderer.material.SetColor("_Color20", ext23Colors[4]);
            ext23Renderer.material.SetColor("_Color21", ext23Colors[5]);
            // - 3
            ext23Renderer.material.SetColor("_Color30", ext23Colors[6]);
            ext23Renderer.material.SetColor("_Color31", ext23Colors[7]);

            if (extr)
            {
                // EXTR
                // - 0
                extrRenderer.material.SetColor("_Color00", extrColors[0]);
                extrRenderer.material.SetColor("_Color01", extrColors[1]);
                // - 1
                extrRenderer.material.SetColor("_Color10", extrColors[2]);
                extrRenderer.material.SetColor("_Color11", extrColors[3]);
                // - 2
                extrRenderer.material.SetColor("_Color20", extrColors[4]);
                extrRenderer.material.SetColor("_Color21", extrColors[5]);
                // - 3
                extrRenderer.material.SetColor("_Color30", extrColors[6]);
                extrRenderer.material.SetColor("_Color31", extrColors[7]);
            }
        }
    }
}
