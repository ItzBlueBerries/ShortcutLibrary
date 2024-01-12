using ShortcutLib.Components;
using SRML.SR.SaveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Methods relating to creating gordos.
    /// </summary>
    public static class Gordo
    {
        // internal static readonly Dictionary<Identifiable.Id, Vector3[]> gordoSpawnsToPatch = new();

        /// <summary>
        /// Gets a gordo prefab <see cref="GameObject"/> attached to the <see cref="Identifiable.Id"/> given.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to grab the gordo prefab <see cref="GameObject"/> from.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject GetGordo(Identifiable.Id identifiable) => Director.Lookup.GetGordo(identifiable);

        /*public static void ModifyGordoSpawns(Identifiable.Id identifiable, int spawnCount) =>
            gordoSpawnsToPatch.AddIfDoesNotContain(identifiable, new Vector3[spawnCount]);*/

        /// <summary>
        /// Positions a gordo in the world based on the parent <see cref="Transform"/> and position <see cref="Vector3"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the gordo.</param>
        /// <param name="parent">The parent <see cref="Transform"/> that the gordo should be parented to.</param>
        /// <param name="position">The position <see cref="Vector3"/> that the gordo should be positioned at.</param>
        /// <param name="rotationAngle">The rotation angle <see cref="float"/> of the gordo to rotate in the correct direction. Leave 0 if not needed.</param>
        /// <returns><see cref="string"/></returns>
        public static string PositionGordo(Identifiable.Id identifiable, Transform parent, Vector3 position, float rotationAngle)
        {
            GameObject instantiatedGordo = GetGordo(identifiable).InstantiateInactive(position, Quaternion.identity, parent, true);
            instantiatedGordo.transform.RotateAround(instantiatedGordo.transform.position, instantiatedGordo.transform.up, rotationAngle);

            var gordoEat = instantiatedGordo.GetComponent<GordoEat>();
            var persistentId = ModdedStringRegistry.ClaimID("gordo", instantiatedGordo.name.Replace("gordo", ""));
            gordoEat.director = gordoEat.GetComponentInParent<IdDirector>();
            gordoEat.director.persistenceDict.Add(gordoEat, persistentId);

            instantiatedGordo.SetActive(true);
            return persistentId;
        }

        /// <summary>
        /// Creates a starting base for a gordo.
        /// </summary>
        /// <param name="baseIdentifiable">The base <see cref="Identifiable.Id"/> that the gordo copies the prefab <see cref="GameObject"/> from.</param>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> of the gordo.</param>
        /// <param name="slimeIdentifiable">The slime <see cref="Identifiable.Id"/> that the gordo is based on.</param>
        /// <param name="icon">The icon <see cref="Sprite"/> of the gordo.</param>
        /// <param name="name">The name <see cref="string"/> of the gordo.</param>
        /// <param name="feedCount">The feed count <see cref="int"/> of the gordo.</param>
        /// <param name="nativeZones">The native zones <see cref="ZoneDirector.Zone[]"/> of the gordo.</param>
        /// <param name="gordoRewards">The list of rewards for popping the gordo. Anything unfilled from the maximum rewards is automatically replaced with slimes.</param>
        /// <returns><see cref="GameObject"/></returns>
        public static GameObject CreateGordoBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Identifiable.Id slimeIdentifiable, Sprite icon, string name, int feedCount, ZoneDirector.Zone[] nativeZones, List<GameObject> gordoRewards)
        {
            GameObject prefab = Prefab.ObjectCopy(GetGordo(baseIdentifiable));
            prefab.name = "gordo" + name.Replace(" ", "").Replace("Gordo", "");

            SlimeDefinition slimeDefinition = Slime.GetSlimeDefinition(slimeIdentifiable);
            Material slimeMaterial = slimeDefinition.AppearancesDefault[0].Structures[0].DefaultMaterials[0];
            SlimeFace slimeFace = slimeDefinition.AppearancesDefault[0].Face;

            GordoFaceComponents gordoFace = prefab.GetComponent<GordoFaceComponents>();
            gordoFace.strainEyes = slimeFace.ExpressionFaces.First(x => x.SlimeExpression == SlimeFace.SlimeExpression.Scared).Eyes;
            gordoFace.strainMouth = slimeFace.ExpressionFaces.First(x => x.SlimeExpression == SlimeFace.SlimeExpression.ChompClosed).Mouth;
            gordoFace.blinkEyes = slimeFace.ExpressionFaces.First(x => x.SlimeExpression == SlimeFace.SlimeExpression.Blink).Eyes;
            gordoFace.chompOpenMouth = slimeFace.ExpressionFaces.First(x => x.SlimeExpression == SlimeFace.SlimeExpression.ChompOpen).Mouth;
            gordoFace.happyMouth = slimeFace.ExpressionFaces.First(x => x.SlimeExpression == SlimeFace.SlimeExpression.Happy).Mouth;

            prefab.GetComponent<GordoEat>().slimeDefinition = slimeDefinition;
            prefab.GetComponent<GordoEat>().targetCount = feedCount;
            prefab.GetComponent<GordoRewards>().rewardPrefabs = gordoRewards.ToArray();
            prefab.GetComponent<GordoRewards>().slimePrefab = Prefab.GetPrefab(slimeIdentifiable);
            prefab.GetComponent<GordoIdentifiable>().id = identifiable;
            prefab.GetComponent<GordoIdentifiable>().nativeZones = nativeZones;

            GordoDisplayOnMap displayOnMap = prefab.GetComponent<GordoDisplayOnMap>();
            GameObject markerPrefab = Prefab.ObjectCopy(displayOnMap.markerPrefab.gameObject);
            markerPrefab.name = "Gordo" + name.Replace(" ", "").Replace("Gordo", "") + "Marker";
            markerPrefab.GetComponent<Image>().sprite = icon;

            displayOnMap.gordoEat = prefab.GetComponent<GordoEat>();
            displayOnMap.markerPrefab = markerPrefab.GetComponent<MapMarker>();

            GameObject slime_gordo = prefab.transform.Find("Vibrating/slime_gordo").gameObject;
            slime_gordo.GetComponent<SkinnedMeshRenderer>().sharedMaterial = slimeMaterial;

            Identifiable.GORDO_CLASS.Add(identifiable);
            Translate.Pedia("t." + identifiable.ToString().ToLower(), name);
            LookupRegistry.RegisterGordo(prefab);
            return prefab;
        }
    }
}
