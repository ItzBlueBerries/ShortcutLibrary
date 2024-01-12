using ShortcutLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static vp_Message;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Methods for creating edible foods, such as birds or veggies/fruits.
    /// </summary>
    public static class Edible
    {
        /// <summary>
        /// Creates a starting base for a custom bird. (e.g. <see cref="Identifiable.Id.HEN"/>)
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Identifiable.Id"/> that the bird copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the bird.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the bird.</param>
        /// <param name="name">The name <see cref="string"/> of the bird.</param>
        /// <param name="rampRed">The ramp (Red) <see cref="Texture2D"/> of the bird.</param>
        /// <param name="rampGreen">The ramp (Green) <see cref="Texture2D"/> of the bird.</param>
        /// <param name="rampBlue">The ramp (Blue) <see cref="Texture2D"/> of the bird.</param>
        /// <param name="rampBlack">The ramp (Black) <see cref="Texture2D"/> of the bird.</param>
        /// <param name="vacColor">The vac color <see cref="Color"/> of the bird.</param>
        /// <param name="isChick">If the created bird is a chick. <see cref="bool"/></param>
        /// <param name="isElder">If the created bird is an elder. <see cref="bool"/></param>
        /// <returns><see cref="(GameObject, Material)"/></returns>
        public static (GameObject, Material) CreateBirdBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Sprite icon, string name, Texture2D rampRed, Texture2D rampGreen, Texture2D rampBlue, Texture2D rampBlack, Color vacColor, bool isChick = false, bool isElder = false)
        {
            GameObject prefab = Prefab.QuickCopy(baseIdentifiable);
            prefab.name = "bird" + name.Replace(" ", "");
            prefab.GetComponent<Identifiable>().id = identifiable;

            SkinnedMeshRenderer skinnedMeshRenderer = prefab.GetComponentInChildren<Animator>().transform.Find("mesh_body1").GetComponent<SkinnedMeshRenderer>();
            Material material = UnityEngine.Object.Instantiate(skinnedMeshRenderer.sharedMaterial);
            material.SetTexture("_RampRed", rampRed);
            material.SetTexture("_RampGreen", rampGreen);
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

            if (isElder)
            {
                Registry.AddIdentifiableToSilo(identifiable, SiloStorage.StorageType.ELDER);
                Registry.AddIdentifiableToSilo(identifiable, SiloStorage.StorageType.FOOD);
            }
            else
                Registry.AddIdentifiableToSilo(identifiable, SiloStorage.StorageType.FOOD);

            Registry.AddIdentifiableToSnare(identifiable);
            Registry.AddIdentifiableToDrone(identifiable);

            Registry.RegisterVaccable(identifiable, icon, vacColor, name.Replace(" ", ""));
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.RESOURCES, identifiable);

            Translate.Pedia("t." + identifiable.ToString().ToLower(), name);
            LookupRegistry.RegisterIdentifiablePrefab(prefab);
            return (prefab, skinnedMeshRenderer.sharedMaterial);
        }

        /// <summary>
        /// Creates a starting base for a custom food.
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Identifiable.Id"/> that the food copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the food.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the food.</param>
        /// <param name="name">The name <see cref="string"/> of the food.</param>
        /// <param name="rampRed">The ramp (Red) <see cref="Texture2D"/> of the food.</param>
        /// <param name="rampGreen">The ramp (Green) <see cref="Texture2D"/> of the food.</param>
        /// <param name="rampBlue">The ramp (Blue) <see cref="Texture2D"/> of the food.</param>
        /// <param name="rampBlack">The ramp (Black) <see cref="Texture2D"/> of the food.</param>
        /// <param name="vacColor">The vac color <see cref="Color"/> of the food.</param>
        /// <param name="isVeggie">If the created food is a veggie. <see cref="bool"/></param>
        /// <param name="isFruit">If the created food is a fruit. <see cref="bool"/></param>
        /// <returns><see cref="(GameObject, Material)"/></returns>
        public static (GameObject, Material) CreateFoodBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Sprite icon, string name, Texture2D rampRed, Texture2D rampGreen, Texture2D rampBlue, Texture2D rampBlack, Color vacColor, bool isVeggie = false, bool isFruit = false)
        {
            GameObject prefab = Prefab.QuickCopy(baseIdentifiable);

            string toString = identifiable.ToString();
            prefab.name = toString.ToUpper().Contains("FRUIT") ? "fruit" + toString.Replace(" ", "") :
                (toString.ToUpper().Contains("VEGGIE") ? "veggie" + toString.Replace(" ", "") : "food" + toString.Replace(" ", ""));

            prefab.GetComponent<Identifiable>().id = identifiable;

            MeshRenderer meshRenderer = GameObjectExtensions.FindChildWithPartialName(prefab, "model_").GetComponent<MeshRenderer>();
            Material material = (Material)Prefab.Instantiate(meshRenderer.sharedMaterial);
            material.SetTexture("_RampRed", rampRed);
            material.SetTexture("_RampGreen", rampGreen);
            material.SetTexture("_RampBlue", rampBlue);
            material.SetTexture("_RampBlack", rampBlack);
            meshRenderer.sharedMaterial = material;

            if (isVeggie)
                Identifiable.VEGGIE_CLASS.Add(identifiable);
            else if (isFruit)
                Identifiable.FRUIT_CLASS.Add(identifiable);
            else
                Identifiable.MEAT_CLASS.Add(identifiable);

            Identifiable.FOOD_CLASS.Add(identifiable);
            Identifiable.NON_SLIMES_CLASS.Add(identifiable);

            Registry.AddIdentifiableToAmmo(identifiable);
            Registry.AddIdentifiableToSilo(identifiable);
            Registry.AddIdentifiableToSnare(identifiable);
            Registry.AddIdentifiableToDrone(identifiable);

            Registry.RegisterVaccable(identifiable, icon, vacColor, name.Replace(" ", ""));
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.RESOURCES, identifiable);

            Translate.Pedia("t." + identifiable.ToString().ToLower(), name);
            LookupRegistry.RegisterIdentifiablePrefab(prefab);
            return (prefab, material);
        }
    }
}
