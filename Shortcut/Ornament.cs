using Microsoft.Win32;
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
    /// Method(s) for creating ornaments.
    /// </summary>
    public static class Ornament
    {
        /// <summary>
        /// Creates a starting base for a custom ornament.
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Identifiable.Id"/> that the ornament copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the ornament.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the ornament.</param>
        /// <param name="name">The name <see cref="string"/> of the ornament.</param>
        /// <param name="texture">The texture <see cref="Texture2D"/> of the ornament.</param>
        /// <param name="color">The color <see cref="Color"/> of the ornament.</param>
        /// <param name="vacColor">The vac color <see cref="Color"/> of the ornament.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject CreateOrnamentBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Sprite icon, string name, Texture2D texture, Color32 color, Color32 vacColor)
        {
            GameObject prefab = Prefab.ObjectCopy(Prefab.GetPrefab(baseIdentifiable));
            prefab.name = "ornament" + name.Replace(" ", "");
            prefab.GetComponent<Identifiable>().id = identifiable;

            GameObject model = prefab.transform.Find("model").gameObject;
            Material material = (Material)Prefab.Instantiate(model.GetComponent<MeshRenderer>().material);
            material.mainTexture = texture;
            material.color = color;
            model.GetComponent<MeshRenderer>().material = material;

            Registry.AddIdentifiableToAmmo(identifiable);
            Registry.AddIdentifiableToSilo(identifiable);
            Registry.RegisterVaccable(identifiable, icon, vacColor, name.Replace(" ", "") + "Ornament");
            Translate.Actor("l." + identifiable.ToString().ToLower(), name);

            LookupRegistry.RegisterIdentifiablePrefab(prefab);
            return prefab;
        }
    }
}
