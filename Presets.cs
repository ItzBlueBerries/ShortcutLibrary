using MonomiPark.SlimeRancher.Regions;
using ShortcutLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static ShortcutLib.Shortcut;

namespace ShortcutLib.Presets
{
    public static class Sizes
    {
        public static readonly Vector3 DEFAULT_SIZE = new Vector3(1f, 1f, 1f);
        public static readonly Vector3 POGOFRUIT_SIZE = new Vector3(0.25f, 0.25f, 0.25f);
        public static readonly Vector3 CARROT_SIZE = new Vector3(2.0f, 2.0f, 2.0f);
        public static readonly Vacuumable.Size VAC_N = Vacuumable.Size.NORMAL;
        public static readonly Vacuumable.Size VAC_L = Vacuumable.Size.LARGE;
        public static readonly Vacuumable.Size VAC_G = Vacuumable.Size.GIANT;
    }

    public static class Cells
    {
        public static readonly string COSMETIC_CELL = "zoneREEF/cellReef_GordoIsland/Sector/Loot/treasurePodCosmetic";
        public static readonly string FOUNTAIN_CELL = "zoneREEF/cellReef_Intro/Sector/Resources/waterFountain01";
    }

    public class Rands
    {
        public static readonly Identifiable.Id RAND_SLIME = Identifiable.SLIME_CLASS.GetRandom();
        public static readonly Identifiable.Id RAND_LARGO = Identifiable.LARGO_CLASS.GetRandom();
        public static readonly double RAND_DOUBLE = new System.Random().NextDouble();

        public static int RAND_INT(int min = int.MinValue, int max = int.MaxValue)
        { return new System.Random().Next(min, max); }
    }
}

public static class ExtensionsMethods
{
    public static void GMeshProps(this GameObject gObject, bool hasIdentifiable, bool shouldParent, [Optional] Identifiable.Id meshId, [Optional] Transform meshParent, Vacuumable.Size vacSize = Vacuumable.Size.NORMAL)
    {
        if (hasIdentifiable)
        {
            gObject.AddComponent<RegionMember>();
            gObject.AddComponent<Rigidbody>();
            gObject.AddComponent<Identifiable>().id = meshId;
            gObject.AddComponent<Vacuumable>().size = vacSize;
        }

        if (shouldParent)
        {
            gObject.transform.parent = meshParent;
        }
    }

    public static Material GetSlimeMat(this SlimeDefinition slimeDefinition, int structureIndex)
    { return slimeDefinition.AppearancesDefault[0].Structures[structureIndex].DefaultMaterials[0]; }

    public static void SetMaterial(this GameObject gameObject, Material mat, bool skinnedRenderer)
    {
        if (skinnedRenderer)
            gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial = mat;
        else
            gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    public static void SetSlimeColor(this Material material, Color color1, Color color2, Color color3)
    { material.SetColor("_TopColor", color1); material.SetColor("_MiddleColor", color2); material.SetColor("_BottomColor", color3); }

    public static void LogComps(this GameObject gObject, bool logToFile = true)
    { Debugging.LogComponents(gObject, logToFile); }

    public static void LogContents(this HashSet<Identifiable.Id> hashset, bool logToFile = true)
    { Debugging.LogClassContents(hashset, logToFile); }

    public static void LogChilds(this GameObject gameObject, bool logToFile = true)
    { Debugging.LogChildren(gameObject, logToFile); }
}