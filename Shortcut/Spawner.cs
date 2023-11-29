using MonomiPark.SlimeRancher.Regions;
using SRML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Methods related to spawning, like adding slimes to spawn in existing zones.
    /// </summary>
    public static class Spawner
    {
        /// <summary>
        /// Creates a starting base for a custom <see cref="DirectedSlimeSpawner"/>.
        /// </summary>
        /// <param name="parent">The parent <see cref="GameObject"/> of the spawner.</param>
        /// <param name="position">The position <see cref="Vector3"/> of the spawner.</param>
        /// <param name="rotation">The rotation <see cref="Quaternion"/> of the spawner.</param>
        /// <param name="spawnRadius">The radius <see cref="float"/> of the spawner.</param>
        /// <param name="spawnDelayFactor">The spawn delay <see cref="float"/> of the spawner.</param>
        /// <param name="enableStacking">If stacking / toteming is enabled when the slimes spawn. <see cref="bool"/></param>
        /// <returns><see cref="(GameObject, DirectedSlimeSpawner, DirectedActorSpawner.SpawnConstraint[])"/></returns>
        public static (GameObject, DirectedSlimeSpawner, DirectedActorSpawner.SpawnConstraint[]) CreateSlimeSpawnerBase(GameObject parent, Vector3 position, Quaternion rotation, float spawnRadius = 1, float spawnDelayFactor = 1, bool enableStacking = true)
        {
            /*DirectedActorSpawner.SpawnConstraint[] constraints = new DirectedActorSpawner.SpawnConstraint[]
            {
                new DirectedActorSpawner.SpawnConstraint()
                {
                    window = new DirectedActorSpawner.TimeWindow()
                    {
                        timeMode = timeMode,
                        endHour = maxSpawnTime,
                        startHour = minSpawnTime
                    },
                    slimeset = new SlimeSet()
                    {
                        members = members
                    },
                    feral = spawnFeral,
                    maxAgitation = spawnAgitated,
                    weight = defaultWeight
                }
            };*/

            GameObject nodeSlime = GameObjectUtils.InstantiateInactive(GameObject.Find(Paths.SLIME_SPAWNER_PATH));
            nodeSlime.transform.parent = parent.transform;
            nodeSlime.transform.position = position;
            nodeSlime.transform.rotation = rotation;

            var directedSlimeSpawner = nodeSlime.GetComponent<DirectedSlimeSpawner>();
            directedSlimeSpawner.radius = spawnRadius;
            directedSlimeSpawner.enableToteming = enableStacking;
            directedSlimeSpawner.spawnDelayFactor = spawnDelayFactor;
            directedSlimeSpawner.allowDirectedSpawns = true;

            nodeSlime.SetActive(true);
            return (nodeSlime, directedSlimeSpawner, directedSlimeSpawner.constraints);
        }

        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to <see cref="DirectedSlimeSpawner"/>s in a specified <see cref="ZoneDirector.Zone"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be spawned.</param>
        /// <param name="zoneIdentifiable">The <see cref="ZoneDirector.Zone"/> for the <see cref="Identifiable.Id"/> to spawn in.</param>
        /// <param name="weight">The weight / chance <see cref="float"/> of the spawn.</param>
        public static void AddSlimeToZone(Identifiable.Id identifiable, ZoneDirector.Zone zoneIdentifiable, float weight)
        {
            SRCallbacks.PreSaveGameLoad += (SceneContext sceneContext) =>
            {
                foreach (DirectedSlimeSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>())
                {
                    if (spawner.gameObject.GetComponentInChildren<ZoneDirector>(true).zone != zoneIdentifiable)
                        return;

                    foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                    {
                        var members = constraint.slimeset.members.ToList();
                        members.AddIfDoesNotContain(new SlimeSet.Member
                        {
                            prefab = Director.Lookup.GetPrefab(identifiable),
                            weight = weight
                        });
                        constraint.slimeset.members = members.ToArray();
                    }
                }
            };
        }

        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to <see cref="DirectedActorSpawner"/>s in a specified <see cref="ZoneDirector.Zone"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be spawned.</param>
        /// <param name="zoneIdentifiable">The <see cref="ZoneDirector.Zone"/> for the <see cref="Identifiable.Id"/> to spawn in.</param>
        /// <param name="weight">The weight / chance <see cref="float"/> of the spawn.</param>
        public static void AddActorToZone(Identifiable.Id identifiable, ZoneDirector.Zone zoneIdentifiable, float weight)
        {
            SRCallbacks.PreSaveGameLoad += (SceneContext sceneContext) =>
            {
                foreach (DirectedActorSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedActorSpawner>())
                {
                    if (spawner.gameObject.GetComponentInChildren<ZoneDirector>(true).zone != zoneIdentifiable)
                        return;

                    foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                    {
                        var members = constraint.slimeset.members.ToList();
                        members.AddIfDoesNotContain(new SlimeSet.Member
                        {
                            prefab = Director.Lookup.GetPrefab(identifiable),
                            weight = weight
                        });
                        constraint.slimeset.members = members.ToArray();
                    }
                }
            };
        }

        /// <summary>
        /// Adds an <see cref="Identifiable.Id"/> to <see cref="DirectedAnimalSpawner"/>s in a specified <see cref="ZoneDirector.Zone"/>.
        /// </summary>
        /// <param name="identifiable">The <see cref="Identifiable.Id"/> to be spawned.</param>
        /// <param name="zoneIdentifiable">The <see cref="ZoneDirector.Zone"/> for the <see cref="Identifiable.Id"/> to spawn in.</param>
        /// <param name="weight">The weight / chance <see cref="float"/> of the spawn.</param>
        public static void AddAnimalToZone(Identifiable.Id identifiable, ZoneDirector.Zone zoneIdentifiable, float weight)
        {
            SRCallbacks.PreSaveGameLoad += (SceneContext sceneContext) =>
            {
                foreach (DirectedAnimalSpawner spawner in UnityEngine.Object.FindObjectsOfType<DirectedAnimalSpawner>())
                {
                    if (spawner.gameObject.GetComponentInChildren<ZoneDirector>(true).zone != zoneIdentifiable)
                        return;

                    foreach (DirectedActorSpawner.SpawnConstraint constraint in spawner.constraints)
                    {
                        var members = constraint.slimeset.members.ToList();
                        members.AddIfDoesNotContain(new SlimeSet.Member
                        {
                            prefab = Director.Lookup.GetPrefab(identifiable),
                            weight = weight
                        });
                        constraint.slimeset.members = members.ToArray();
                    }
                }
            };
        }
    }
}
