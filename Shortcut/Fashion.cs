using Mono.Cecil;
using ShortcutLib.Components;
using SRML.SR.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Methods for creating fashions.
    /// </summary>
    public static class FashionP
    {
        /// <summary>
        /// Creates a starting base for a custom fashion.
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Identifiable.Id"/> that the fashion copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the fashion.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the fashion.</param>
        /// <param name="name">The name <see cref="string"/> of the fashion.</param>
        /// <param name="clipOnPrefab">The clip-on prefab <see cref="GameObject"/> of which is attached once the fashion is set onto something.</param>
        /// <param name="vacColor">The vac color <see cref="Color"/> of the fashion.</param>
        /// <param name="fashionSlot">The fashion slot <see cref="Fashion.Slot"/> of the fashion.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject CreateFashionBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Sprite icon, string name, GameObject clipOnPrefab, Color vacColor, Fashion.Slot fashionSlot = Fashion.Slot.TOP)
        {
            GameObject prefab = Prefab.QuickCopy(baseIdentifiable);
            prefab.name = "fashion" + name.Replace(" ", "");

            prefab.GetComponent<Identifiable>().id = identifiable;
            prefab.GetComponent<Fashion>().slot = fashionSlot;
            prefab.GetComponent<Fashion>().attachPrefab = clipOnPrefab;
            prefab.GetComponentsInChildren<Image>().ForEach(x => x.sprite = icon);

            Identifiable.FASHION_CLASS.Add(identifiable);

            Registry.AddIdentifiableToAmmo(identifiable);
            Registry.RegisterVaccable(identifiable, icon, vacColor, name.Replace(" ", "") + "Fashion");
            Translate.Actor("l." + identifiable.ToString().ToLower(), name);

            LookupRegistry.RegisterIdentifiablePrefab(prefab);
            return prefab;
        }

        /// <summary>
        /// Creates a starting base for a custom fashion pod.
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Gadget.Id"/> that the fashion pod copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Gadget.Id"/> of the fashion pod.</param>
        /// <param name="fashionIdentifiable">The fashion <see cref="Identifiable.Id"/> that is obtained from the fashion pod.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the fashion pod.</param>
        /// <param name="name">The name <see cref="string"/> of the fashion pod.</param>
        /// <param name="description">The description <see cref="string"/> of the fashion pod.</param>
        /// <param name="craftCosts"></param>
        /// <param name="unlockTime"></param>
        /// <param name="podCost"></param>
        /// <param name="podLimit"></param>
        /// <returns></returns>
        public static (GameObject, GadgetDefinition) CreatePodBase(Gadget.Id baseIdentifiable, Gadget.Id identifiable, Identifiable.Id fashionIdentifiable, Sprite icon, string name, string description, GadgetDefinition.CraftCost[] craftCosts, float unlockTime = 3, int podCost = 1000, int podLimit = 20)
        {
            GameObject prefab = Prefab.ObjectCopy(Resource.GetGadgetDefinition(baseIdentifiable).prefab);
            prefab.name = name + name.Replace(" ", "");

            prefab.GetComponent<Gadget>().id = identifiable;
            prefab.GetComponent<FashionPod>().fashionId = fashionIdentifiable;
            prefab.transform.Find("model_fashionPod").GetComponent<MeshRenderer>().material.mainTexture = icon.texture;

            GadgetDefinition definition = Definition.CreateGadDefinition(identifiable, PediaDirector.Id.CURIOS, prefab, icon,
                "FashionPod" + name, podLimit, podCost, podLimit, false, false, null, craftCosts);

            Gadget.FASHION_POD_CLASS.Add(identifiable);
            LookupRegistry.RegisterGadget(definition);
            GadgetTranslationExtensions.GetTranslation(identifiable).SetNameTranslation(name).SetDescriptionTranslation(description);
            return (prefab, definition);
        }
    }
}
