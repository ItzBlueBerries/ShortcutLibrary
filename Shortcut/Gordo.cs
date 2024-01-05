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
    public static class Gordo
    {
        internal static readonly Dictionary<Identifiable.Id, Vector3[]> gordoSpawnsToPatch = new();

        public static GameObject GetGordo(Identifiable.Id identifiable) => Director.Lookup.GetGordo(identifiable);

        public static void ModifyGordoSpawns(Identifiable.Id identifiable, int spawnCount) =>
            gordoSpawnsToPatch.AddIfDoesNotContain(identifiable, new Vector3[spawnCount]);

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

        public static GameObject CreateGordoBase(Identifiable.Id baseIdentifiable, Identifiable.Id identifiable, Identifiable.Id slimeIdentifiable, Sprite icon, string name, int feedCount, List<GameObject> gordoRewards, ZoneDirector.Zone[] nativeZones)
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
