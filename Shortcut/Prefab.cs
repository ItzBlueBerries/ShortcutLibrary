using MonomiPark.SlimeRancher.Regions;
using SRML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Usually <see cref="GameObject"/> / <see cref="UnityEngine.Object"/> related methods.
    /// </summary>
    public static class Prefab
    {
        /// <summary>
        /// Finds the <see cref="GameObject"/> in the game based on the given path.
        /// </summary>
        /// <param name="path">The path to the <see cref="GameObject"/> in-game.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject FindObject(string path) => GameObject.Find(path);

        /// <summary>
        /// Gets the <see cref="GameObject"/> prefab of an <see cref="Identifiable.Id"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to grab the prefab.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject GetPrefab(Identifiable.Id identifiable) => Director.Lookup.GetPrefab(identifiable);

        /// <summary>
        /// Quickly copies the <see cref="GameObject"/> prefab from the <see cref="Identifiable.Id"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to copy the prefab.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject QuickCopy(Identifiable.Id identifiable) => PrefabUtils.CopyPrefab(Director.Lookup.GetPrefab(identifiable));

        /// <summary>
        /// Copies the <see cref="GameObject"/> prefab given.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> prefab to copy.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject ObjectCopy(GameObject gameObject) => PrefabUtils.CopyPrefab(gameObject);

        /// <summary>
        /// Deeply copies the <see cref="UnityEngine.Object"/> given.
        /// </summary>
        /// <param name="obj">The <see cref="UnityEngine.Object"/> to deeply copy.</param>
        /// <returns><see cref="UnityEngine.Object"/></returns>
        public static UnityEngine.Object DeepCopy(UnityEngine.Object obj) => PrefabUtils.DeepCopyObject(obj);

        /// <summary>
        /// Instantiates the <see cref="UnityEngine.Object"/> given.
        /// </summary>
        /// <param name="obj">The <see cref="UnityEngine.Object"/> to instantiate.</param>
        /// <returns><see cref="UnityEngine.Object"/></returns>
        public static UnityEngine.Object Instantiate(UnityEngine.Object obj) => UnityEngine.Object.Instantiate(obj);

        /// <summary>
        /// Destroys the actor <see cref="GameObject"/> given.
        /// </summary>
        /// <param name="obj">The actor <see cref="GameObject"/> to destroy.</param>
        /// <param name="source">The source of which the actor <see cref="GameObject"/> was destroyed.</param>
        public static void DestroyActor(GameObject obj, string source = "Prefab.DestroyActor") => Destroyer.DestroyActor(obj, source);

        /// <summary>
        /// Destroys the <see cref="UnityEngine.Object"/> given.
        /// </summary>
        /// <param name="obj">The source of which the <see cref="UnityEngine.Object"/> was destroyed.</param>
        public static void DestroyObj(UnityEngine.Object obj) => UnityEngine.Object.Destroy(obj);
    }
}
